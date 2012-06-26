namespace FubuMVC.Json
{
	public interface IJsonSerializer
	{
		string Serialize(object target);
		T Deserialize<T>(string input);
	}
}