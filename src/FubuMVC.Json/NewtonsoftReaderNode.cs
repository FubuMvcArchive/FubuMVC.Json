using System;
using System.Collections.Generic;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Json
{
    public class NewtonSoftReaderNode : ReaderNode
    {
        private readonly Type _inputType;

        public NewtonSoftReaderNode(Type inputType)
        {
            _inputType = inputType;
        }

        protected override ObjectDef toReaderDef()
        {
            return new ObjectDef(typeof(NewtonSoftBindingReader<>), _inputType);
        }

        public override Type InputType
        {
            get { return _inputType; }
        }

        public override IEnumerable<string> Mimetypes
        {
            get
            {
                yield return MimeType.Json.Value;
                yield return "text/json";
            }
        }
    }
}