using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (!typeMap.ContainsKey(tmodel))
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
            foreach (var kv in typeMap)
            {
                var graphApi = Activator.CreateInstance(kv.Value);
                services.AddSingleton(kv.Value, graphApi);
            }

            foreach (var kv in inputMap)
            {
                var graphInputApi = Activator.CreateInstance(kv.Value);
                services.AddSingleton(kv.Value, graphInputApi);
            }

            foreach (var kv in enumMap)
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

                var isRequired = Attribute.IsDefined(exposedPropery, typeof(RequiredAttribute));

                var parameter = Expression.Parameter(typeof(T), "type");
                var memberExpression = Expression.Property(parameter, exposedPropery.Name);
                var fieldExpression = Expression.Lambda(memberExpression, parameter);

                if (exposedPropery.PropertyType.IsEnum)
                {
                    GraphBuilder.RegisterEnum(exposedPropery.PropertyType);
                }

                switch (exposedPropery.PropertyType.Name)
                {
                    case "String":
                        graph.Field((Expression<Func<T, string>>)fieldExpression, !isRequired, typeof(StringGraphType));
                        break;
                    case "Int32":
                        graph.Field((Expression<Func<T, int>>)fieldExpression, !isRequired, typeof(IntGraphType));
                        break;
                    case "List`1":
                        graph.Field((dynamic)fieldExpression, !isRequired, GetListGraphType(
                            exposedPropery.PropertyType.GetGenericArguments()[0], isInputType));
                        break;
                    case "Nullable`1":
                        graph.Field((dynamic)fieldExpression, !isRequired, GetNullableGraphType(
                            exposedPropery.PropertyType.GetGenericArguments()[0], isInputType));
                        break;
                    case "Double":
                        graph.Field((Expression<Func<T, double>>)fieldExpression, !isRequired, typeof(FloatGraphType));
                        break;
                    case "DateTime":
                        graph.Field((Expression<Func<T, DateTime>>)fieldExpression, !isRequired, typeof(DateTimeGraphType));
                        break;
                    case "Boolean":
                        graph.Field((Expression<Func<T, bool>>)fieldExpression, !isRequired, typeof(BooleanGraphType));
                        break;
                    default:
                        if (exposedPropery.PropertyType.IsEnum)
                        {
                            graph.Field((dynamic)fieldExpression, !isRequired,
                                GraphBuilder.GetEnumType(exposedPropery.PropertyType));
                        }
                        else // a custom type
                        {
                            graph.Field((dynamic)fieldExpression, !isRequired,
                                isInputType ? GraphBuilder.GetGraphInputType(exposedPropery.PropertyType) :
                                    GraphBuilder.GetGraphType(exposedPropery.PropertyType));
                        }
                        break;
                }
            }
        }

        private static Type GetListGraphType(Type innerType, bool getInputType)
        {
            var nullableType = typeof(ListGraphType<>);
            var graphType = getInputType ? GraphBuilder.GetGraphInputType(innerType) : GraphBuilder.GetGraphType(innerType);
            var genericType = nullableType.MakeGenericType(graphType);
            return genericType;
        }

        private static Type GetNullableGraphType(Type innerType, bool getInputType)
        {
            var listType = getInputType ? typeof(InputObjectGraphType<>) : typeof(ObjectGraphType<>);
            var genericType = listType.MakeGenericType(innerType);
            return genericType;
        }

        private static Type GetEnumType(Type enumType)
        {
            return enumMap[enumType];
        }

        private static Type GetGraphType(Type modelType)
        {
            return typeMap[modelType];
        }

        private static Type GetGraphInputType(Type modelType)
        {
            return inputMap[modelType];
        }
    }
}
