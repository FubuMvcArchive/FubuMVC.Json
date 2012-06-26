using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;

namespace FubuMVC.Json
{
	public class JsonServiceRegistry : ServiceRegistry
	{
		public JsonServiceRegistry()
		{
			ReplaceService<IJsonReader, NewtonSoftJsonReader>();
			SetServiceIfNone<IJsonSerializer, NewtonSoftJsonSerializer>();
		}
	}
}