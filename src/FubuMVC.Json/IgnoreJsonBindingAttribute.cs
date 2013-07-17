using System;
using System.Linq;
using FubuCore.Descriptions;
using FubuCore.Reflection;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Json
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JsonBindingAttribute : Attribute
    { 
    }

    [Title("Any action with the [JsonBinding] attribute")]
    public class JsonBindingAttributeFilter : IChainFilter
    {
        public bool Matches(BehaviorChain chain)
        {
            return chain.Calls.Any(ActionIsIncluded);
        }

        public static bool ActionIsIncluded(ActionCall call)
        {
            if (call.HasAttribute<JsonBindingAttribute>()) return true;

            if (call.InputType() != null && call.InputType().HasAttribute<JsonBindingAttribute>())
            {
                return true;
            }

            return false;
        }
    }
}