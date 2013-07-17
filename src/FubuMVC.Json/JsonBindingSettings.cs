using FubuCore;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Policies;

namespace FubuMVC.Json
{
    [ApplicationLevel]
    public class JsonBindingSettings
    {
        private readonly ChainPredicate _inclusions = new ChainPredicate();

        public JsonBindingSettings()
        {
            _inclusions.Matching<JsonBindingAttributeFilter>();
            //_inclusions.Matching<IgnoreNonHttpPostRoutesFilter>();
        }

        public ChainPredicate Include
        {
            get { return _inclusions; }
        }

        public bool ShouldBeIncluded(BehaviorChain chain)
        {
            return _inclusions.As<IChainFilter>().Matches(chain);
        }
    }
}