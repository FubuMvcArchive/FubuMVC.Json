﻿using System;
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
        public void not_included_without_attribute_by_default()
        {
            var graph = BehaviorGraph.BuildFrom(x => x.Actions.IncludeType<JsonBindingEndpoint>());
            var chain = graph.BehaviorFor<JsonBindingEndpoint>(x => x.get_something());

            new JsonBindingSettings()
                .ShouldBeIncluded(chain)
                .ShouldBeFalse();
        }

        [Test]
        public void includes_actions_with_the_JsonBinding_attribute()
        {
            var graph = BehaviorGraph.BuildFrom(x => x.Actions.IncludeType<JsonBindingEndpoint>());
            var chain = graph.BehaviorFor<JsonBindingEndpoint>(x => x.post_something());

            new JsonBindingSettings()
                .ShouldBeIncluded(chain)
                .ShouldBeTrue();
        }
    }

    public class JsonBindingEndpoint
    {
        public string get_something()
        {
            throw new NotImplementedException();
        }

        [JsonBinding]
        public string post_something()
        {
            throw new NotImplementedException();
        }
        
        public string post_something_ignore()
        {
            throw new NotImplementedException();
        }
    }
}