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
            var filter = settings.Include.As<IChainFilter>();

            graph
                .Behaviors
                .Where(filter.Matches)
                .Each(chain =>
                {
                    chain.ApplyConneg();
                    //chain.Output.AddFormatter<JsonFormatter>();

                    var defaultJson = chain
                        .Input
                        .Readers
                        .OfType<ReadWithFormatter>()
                        .SingleOrDefault(x => x.FormatterType == typeof (JsonFormatter));

                    if (defaultJson != null)
                    {
                        defaultJson.Remove();
                    }

                    chain.Input.Readers.Prepend(new NewtonSoftReaderNode(chain.InputType()));
                });
        }
    }
}