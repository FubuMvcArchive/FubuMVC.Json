﻿using System;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Json
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JsonBindingAttribute : ModifyChainAttribute
    {
        public override void Alter(ActionCall call)
        {
            call.ParentChain().Input.Add(typeof(NewtonSoftBindingReader<>));
        }
    }
}