using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Policies;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime.Formatters;

namespace FubuMVC.Json
{
    [ConfigurationType(ConfigurationType.Conneg)]
    public class ApplyJsonBindingPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var settings = graph.Settings.Get<JsonBindingSettings>();
            var filter = settings.ExcludeChains.As<IChainFilter>();

            graph
                .Behaviors
                .Where(x => !filter.Matches(x))
                .Each(chain =>
                {
                    chain.ApplyConneg();
                    chain.Output.AddFormatter<JsonFormatter>();

                    chain.Input.ClearAll();
                    chain.Input.Readers.Prepend(new NewtonSoftReaderNode(chain.InputType()));
                });
        }
    }
}