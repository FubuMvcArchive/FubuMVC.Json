using FubuMVC.Core;

namespace FubuMVC.Json
{
	public class JsonFubuRegistryExtension : IFubuRegistryExtension
	{
		public void Configure(FubuRegistry registry)
		{
			registry.Services<JsonServiceRegistry>();
		}
	}
}