using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Json.Tests
{
	public class ServicesRegistrationTester
	{
		private ServiceGraph services;

		[SetUp]
		public void SetUp()
		{
			var registry = new FubuRegistry();
			registry.Services<JsonServiceRegistry>();

			services = BehaviorGraph.BuildFrom(registry).Services;
		}

		[Test]
		public void NewtonsoftJsonReader_is_registered()
		{
			services.DefaultServiceFor<IJsonReader>()
				.Type.ShouldEqual(typeof(NewtonSoftJsonReader));
		}

		[Test]
		public void NewtonsoftJsonSerializer_is_registered()
		{
			services.DefaultServiceFor(typeof(IJsonSerializer))
				.Type.ShouldEqual(typeof(NewtonSoftJsonSerializer));
		} 
	}
}