using System;
using FubuCore.Reflection;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Json.Tests
{
    [TestFixture]
    public class JsonBindingSettingsTester
    {
        [Test]
        public void has_to_be_application_level()
        {
            typeof(JsonBindingSettings).HasAttribute<ApplicationLevelAttribute>()
                .ShouldBeTrue();
        }

        [Test]
        public void excludes_for_gets_always_false_with_no_exclusions()
        {
            var graph = BehaviorGraph.BuildFrom(x => x.Actions.IncludeType<JsonBindingEndpoint>());
            var chain = graph.BehaviorFor<JsonBindingEndpoint>(x => x.get_something());

            new JsonBindingSettings()
                .ShouldBeExcluded(chain)
                .ShouldBeTrue();
        }

        [Test]
        public void automatically_excludes_the_IgnoreJsonBinding_attribute()
        {
            var graph = BehaviorGraph.BuildFrom(x => x.Actions.IncludeType<JsonBindingEndpoint>());
            var chain = graph.BehaviorFor<JsonBindingEndpoint>(x => x.post_something_ignore());

            new JsonBindingSettings()
                .ShouldBeExcluded(chain)
                .ShouldBeTrue();
        }

        [Test]
        public void custom_exclude()
        {
            var graph = BehaviorGraph.BuildFrom(x => x.Actions.IncludeType<JsonBindingEndpoint>());
            var chain = graph.BehaviorFor<JsonBindingEndpoint>(x => x.post_something());

            var settings = new JsonBindingSettings();
            settings.ExcludeChains.ChainMatches(x => x.FirstCall().Method.Name == "post_something");
            settings.ShouldBeExcluded(chain).ShouldBeTrue();
        }
    }

    public class JsonBindingEndpoint
    {
        public string get_something()
        {
            throw new NotImplementedException();
        }

        public string post_something()
        {
            throw new NotImplementedException();
        }

        [IgnoreJsonBinding]
        public string post_something_ignore()
        {
            throw new NotImplementedException();
        }
    }
}