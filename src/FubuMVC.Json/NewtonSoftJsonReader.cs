using System.Collections.Generic;
using System.IO;
using System.Text;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using Newtonsoft.Json;

namespace FubuMVC.Json
{
    public class NewtonSoftJsonReader : IJsonReader
    {
    	private readonly IStreamingData _data;
        private readonly IRequestHeaders _headers;
        private readonly IEnumerable<JsonConverter> _converters;

        public NewtonSoftJsonReader(IStreamingData data, IRequestHeaders headers, IEnumerable<JsonConverter> converters)
        {
        	_data = data;
            _headers = headers;
            _converters = converters;
        }

        public T Read<T>()
        {
            string inputText = GetInputText();

            var serializer = new JsonSerializer(){
                TypeNameHandling = TypeNameHandling.All
            };

            serializer.Converters.AddRange(_converters);
            var stringReader = new StringReader(inputText);
            var jsonReader = new JsonTextReader(stringReader);
            var returnValue = serializer.Deserialize<T>(jsonReader);
            return returnValue;
        }

        public string GetInputText()
        {
			Encoding encoding = Encoding.UTF8;
            _headers.Value<string>(HttpRequestHeaders.ContentEncoding, x => encoding = Encoding.GetEncoding(x));
            _headers.Value<string>("x-encoding", x => encoding = Encoding.GetEncoding(x));

            var reader = new StreamReader(_data.Input, encoding);

            return reader.ReadToEnd();
        }
    }
}