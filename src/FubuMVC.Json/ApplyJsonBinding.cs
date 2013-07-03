﻿using FubuMVC.Core;

namespace FubuMVC.Json
{
    public class ApplyJsonBinding : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<ApplyJsonBindingPolicy>();
            registry.Services<JsonServiceRegistry>();
        }
    }
}