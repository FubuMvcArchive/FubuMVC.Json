using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FubuMVC.Json
{
	public class NewtonSoftJsonSerializer : IJsonSerializer
	{
		private readonly IEnumerable<JsonConverter> _converters;
		private readonly Lazy<JsonSerializer> _serializer;

		public NewtonSoftJsonSerializer(IEnumerable<JsonConverter> converters)
		{
			_converters = converters;
			_serializer = new Lazy<JsonSerializer>(() =>
			                                       	{
			                                       		var jsonSerializer = new JsonSerializer {
															TypeNameHandling = TypeNameHandling.All
			                                       		};
														jsonSerializer.Converters.AddRange(_converters);
														return jsonSerializer;
			                                       	});
		}

		private JsonSerializer serializer
		{
			get { return _serializer.Value; }
		}

		public string Serialize(object target)
		{
			var stringWriter = new StringWriter();
			var writer = new JsonTextWriter(stringWriter);

			serializer.Serialize(writer, target);

			return stringWriter.ToString();
		}

		public T Deserialize<T>(string input)
		{
			return serializer.Deserialize<T>(new JsonTextReader(new StringReader(input)));
		}
	}
}