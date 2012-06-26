using FubuMVC.Core;
using FubuMVC.Core.Behaviors;

namespace FubuMVC.Json
{
	public class JsonFubuRegistryExtension : FubuRegistry
	{
		public JsonFubuRegistryExtension()
		{
			Services(x =>
			         	{
			         		x.ReplaceService<IJsonReader, NewtonSoftJsonReader>();
			         		x.AddService<IJsonSerializer, NewtonSoftJsonSerializer>();
			         	});
		}
	}
}