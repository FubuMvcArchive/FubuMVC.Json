using FubuCore;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Json
{
    [ApplicationLevel]
    public class JsonBindingSettings
    {
        private readonly ChainPredicate _exclusions = new ChainPredicate();

        public JsonBindingSettings()
        {
            _exclusions.Matching<IgnoreJsonBindingFilter>();
            _exclusions.Matching<IgnoreNonHttpPostRoutesFilter>();
        }

        public ChainPredicate ExcludeChains
        {
            get { return _exclusions; }
        }

        public bool ShouldBeExcluded(BehaviorChain chain)
        {
            return _exclusions.As<IChainFilter>().Matches(chain);
        }
    }
}