using System.Collections.Generic;
using FubuCore.Binding;

namespace FubuMVC.Json
{
    public class NewtonSoftBindingReader<T> : Core.Resources.Conneg.IReader<T>
    {
        private readonly NewtonSoftJsonReader _reader;
        private readonly IObjectResolver _resolver;

        public NewtonSoftBindingReader(NewtonSoftJsonReader reader, IObjectResolver resolver)
        {
            _reader = reader;
            _resolver = resolver;
        }

        public IEnumerable<string> Mimetypes
        {
            get
            {
                yield return "application/json";
                yield return "text/json";
            }
        }

        public T Read()
        {
            var json = _reader.GetInputText();

            return ReadFromJson(json);
        }

        public T ReadFromJson(string json)
        {
            var values = new JObjectValues(json);

            return (T)_resolver.BindModel(typeof(T), values).Value;
        }

        public T Read(string mimeType)
        {
            return Read();
        }
    }
}