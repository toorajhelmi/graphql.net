using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Apsy.Elemental.Core.Graph
{
    public static class GraphBuilder
    {
        private static Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();
        private static Dictionary<Type, Type> inputMap = new Dictionary<Type, Type>();
        private static Dictionary<Type, Type> enumMap = new Dictionary<Type, Type>();

        public static void RegisterEnum(Type enumType)
        {
            if (!enumMap.ContainsKey(enumType))
            {
                var graphApiEnumGenericType = typeof(GraphEnumApi<>);
                var graphApiEnumType = graphApiEnumGenericType.MakeGenericType(enumType);
                enumMap.Add(enumType, graphApiEnumType);
            }
        }

        public static void Register<TModel>(bool mutable = false)
        {
            var tmodel = typeof(TModel);
            Register(tmodel, mutable);
        }

        public static void Register(Type tmodel, bool mutable = false)
        {
            if (!mutable && !typeMap.ContainsKey(tmodel))
            {
                var graphApiGenericType = typeof(GraphApi<>);
                var graphApiType = graphApiGenericType.MakeGenericType(tmodel);
                typeMap.Add(tmodel, graphApiType);
            }

            if (mutable && !inputMap.ContainsKey(tmodel))
            {
                var graphApiInputGenericType = typeof(GraphInputApi<>);
                var graphApiInputType = graphApiInputGenericType.MakeGenericType(tmodel);
                inputMap.Add(tmodel, graphApiInputType);
            }
        }

        public static void BuildApi(IServiceCollection services)
        {
            foreach (var kv in typeMap.ToList())
            {
                var graphApi = Activator.CreateInstance(kv.Value);
                services.AddSingleton(kv.Value, graphApi);
            }

            foreach (var kv in inputMap.ToList())
            {
                var graphInputApi = Activator.CreateInstance(kv.Value);
                services.AddSingleton(kv.Value, graphInputApi);
            }

            foreach (var kv in enumMap.ToList())
            {
                var graphEnumApi = Activator.CreateInstance(kv.Value);
                services.AddSingleton(kv.Value, graphEnumApi);
            }
        }

        public static void AddFields<T>(ComplexGraphType<T> graph, bool skipMutableFields = false)
        {
            bool isInputType = graph is IInputObjectGraphType;

            foreach (var exposedPropery in typeof(T).GetProperties().Where(
                p => Attribute.IsDefined(p, typeof(ApiAttribute))))
            {
                if (skipMutableFields)
                {
                    var mutable = (Attribute.GetCustomAttribute(exposedPropery, typeof(ApiAttribute)) as ApiAttribute).Mutable;

                    if (!mutable)
                    {
                        continue;
                    }
                }

                var underlyingType = exposedPropery.PropertyType;
                var isNullable = Nullable.GetUnderlyingType(underlyingType) != null;

                if (isNullable)
                {
                    underlyingType = Nullable.GetUnderlyingType(exposedPropery.PropertyType);
                }

                var parameter = Expression.Parameter(typeof(T), "type");
                var memberExpression = Expression.Property(parameter, exposedPropery.Name);
                var fieldExpression = Expression.Lambda(memberExpression, parameter);

                if (underlyingType.IsEnum)
                {
                    RegisterEnum(underlyingType);
                }

                switch (underlyingType.Name)
                {
                    case "String":
                        graph.Field((Expression<Func<T, string>>)fieldExpression, isNullable, typeof(StringGraphType));
                        break;
                    case "Int32":
                        if (isNullable)
                            graph.Field((Expression<Func<T, int?>>)fieldExpression, isNullable, typeof(IntGraphType));
                        else
                            graph.Field((Expression<Func<T, int>>)fieldExpression, isNullable, typeof(IntGraphType));
                        break;
                    case "Double":
                        if (isNullable)
                            graph.Field((Expression<Func<T, double?>>)fieldExpression, isNullable, typeof(FloatGraphType));
                        else
                            graph.Field((Expression<Func<T, double>>)fieldExpression, isNullable, typeof(FloatGraphType));
                        break;
                    case "DateTime":
                        if (isNullable)
                            graph.Field((Expression<Func<T, DateTime?>>)fieldExpression, isNullable, typeof(DateTimeGraphType));
                        else
                            graph.Field((Expression<Func<T, DateTime>>)fieldExpression, isNullable, typeof(DateTimeGraphType));
                        break;
                    case "Boolean":
                        if (isNullable)
                            graph.Field((Expression<Func<T, bool?>>)fieldExpression, isNullable, typeof(BooleanGraphType));
                        else
                            graph.Field((Expression<Func<T, bool>>)fieldExpression, isNullable, typeof(BooleanGraphType));
                        break;
                    case "List`1":
                        graph.Field((dynamic)fieldExpression, isNullable, GetListGraphType(underlyingType.GetGenericArguments()[0], isInputType));
                        break;
                    default:
                        if (underlyingType.IsEnum)
                        {
                            graph.Field((dynamic)fieldExpression, isNullable, GetEnumType(underlyingType, isInputType));
                        }
                        else // custom type
                        {
                            //graph.Field((dynamic)fieldExpression, isNullable, GetCustomGraphType(underlyingType, isInputType));
                            graph.Field((dynamic)fieldExpression, !isNullable,
                                isInputType ? GetGraphInputType(exposedPropery.PropertyType, isInputType) :
                                GetGraphType(exposedPropery.PropertyType, isInputType));
                        }
                        break;
                }
            }
        }

        private static Type GetListGraphType(Type innerType, bool mutable)
        {
            var listType = typeof(ListGraphType<>);
            var graphType = mutable ? GetGraphInputType(innerType, mutable) : GetGraphType(innerType, mutable);
            var genericType = listType.MakeGenericType(graphType);
            return genericType;
        }

        private static Type GetCustomGraphType(Type innerType, bool mutable)
        {
            var customType = mutable ? typeof(InputObjectGraphType<>) : typeof(ObjectGraphType<>);
            var genericType = customType.MakeGenericType(innerType);
            return genericType;
        }

        private static Type GetEnumType(Type enumType, bool mutable)
        {
            if (!enumMap.ContainsKey(enumType))
            {
                Register(enumType, mutable);
            }

            return enumMap[enumType];
        }

        private static Type GetGraphType(Type modelType, bool mutable)
        {
            if (!typeMap.ContainsKey(modelType))
            {
                Register(modelType, mutable);
            }

            return typeMap[modelType];
        }

        private static Type GetGraphInputType(Type modelType, bool mutable)
        {
            if (!inputMap.ContainsKey(modelType))
            {
                Register(modelType, mutable);
            }

            return inputMap[modelType];
        }
    }
}
