using FubuJson;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Json
{
    public class NewtonSoftJsonWriter : IJsonWriter
    {
        private readonly IJsonSerializer _serializer;
        private readonly IOutputWriter _writer;

        public NewtonSoftJsonWriter(IJsonSerializer serializer, IOutputWriter writer)
        {
            _serializer = serializer;
            _writer = writer;
        }

        public void Write(object output)
        {
            Write(output, MimeType.Json.ToString());
        }

        public virtual void Write(object output, string mimeType)
        {
            var json = _serializer.Serialize(output);
            _writer.Write(mimeType, json);
        }
    }
}