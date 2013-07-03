using System;
using System.Linq;
using FubuCore.Descriptions;
using FubuCore.Reflection;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Json
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreJsonBindingAttribute : Attribute
    { 
    }

    [Title("Any action with the [IgnoreJsonBinding] attribute")]
    public class IgnoreJsonBindingFilter : IChainFilter
    {
        public bool Matches(BehaviorChain chain)
        {
            return chain.Calls.Any(ActionIsExempted);
        }

        public static bool ActionIsExempted(ActionCall call)
        {
            if (call.HasAttribute<IgnoreJsonBindingAttribute>()) return true;

            if (call.InputType() != null && call.InputType().HasAttribute<IgnoreJsonBindingAttribute>())
            {
                return true;
            }

            return false;
        }
    }

    [Title("Any action that does not respond to an HTTP POST")]
    public class IgnoreNonHttpPostRoutesFilter : IChainFilter
    {
        public bool Matches(BehaviorChain chain)
        {
            if (chain.Route == null) return false;

            return !chain.Route.RespondsToMethod("POST");
        }
    }
}