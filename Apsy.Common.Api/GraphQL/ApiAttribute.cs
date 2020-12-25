﻿using System;

namespace Apsy.Common.Api.Graph
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
