using System.IO;
using System.Text;
using FubuJson;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;

namespace FubuMVC.Json
{
    public class NewtonSoftJsonReader : IJsonReader
    {
    	private readonly IStreamingData _data;
        private readonly IRequestHeaders _headers;
    	private readonly IJsonSerializer _serializer;

        public NewtonSoftJsonReader(IStreamingData data, IRequestHeaders headers, IJsonSerializer serializer)
        {
        	_data = data;
            _headers = headers;
        	_serializer = serializer;
        }

        public T Read<T>()
        {
            string inputText = GetInputText();
        	return _serializer.Deserialize<T>(inputText);
        }

		// Leave this here for testing
        public virtual string GetInputText()
        {
			Encoding encoding = Encoding.UTF8;
            _headers.Value<string>(HttpRequestHeaders.ContentEncoding, x => encoding = Encoding.GetEncoding(x));
            _headers.Value<string>("x-encoding", x => encoding = Encoding.GetEncoding(x));

            var reader = new StreamReader(_data.Input, encoding);

            return reader.ReadToEnd();
        }
    }
}