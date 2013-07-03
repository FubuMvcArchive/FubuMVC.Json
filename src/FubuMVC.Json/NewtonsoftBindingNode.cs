using System;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuMVC.Media;

namespace FubuMVC.Json
{
    public class NewtonSoftBindingNode : IMediaReaderNode
    {
        private readonly Type _inputType;

        public NewtonSoftBindingNode(Type inputType)
        {
            _inputType = inputType;
        }

        public ObjectDef ToObjectDef()
        {
            return new ObjectDef(typeof(NewtonSoftBindingReader<>), _inputType);
        }

        public Type InputType
        {
            get { return _inputType; }
        }
    }
}