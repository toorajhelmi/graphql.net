using System;

namespace Apsy.Elemental.Core.Graph
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class ApiAttribute : Attribute
    {
        public bool Mutable { get; set; }

        public ApiAttribute(bool mutable = true)
        {
            Mutable = mutable;
        }
    }
}
