using System;
using FubuCore.Conversion;
using FubuMVC.StructureMap;
using FubuTestingSupport;
using NUnit.Framework;
using Newtonsoft.Json;

namespace FubuMVC.Json.Tests
{
	[TestFixture]
	public class when_deserializing_an_object
	{
		private NewtonSoftJsonSerializer theSerializer;
		private string theInput;
		private ParentType theObject;

		[SetUp]
		public void SetUp()
		{
			var container = StructureMapContainerFacility.GetBasicFubuContainer(x =>
			                                                                    	{
			                                                                    		x.For<IObjectConverterFamily>().Add<StatelessComplexTypeConverter>();
			                                                                    		x.For<JsonConverter>().Add<ComplexTypeConverter>();
			                                                                    	});
			theInput = "{\"Name\":\"Test\",\"Child\":\"x:123\"}";
			theSerializer = container.GetInstance<NewtonSoftJsonSerializer>();

			theObject = theSerializer.Deserialize<ParentType>(theInput);
		}

		[Test]
		public void uses_the_object_converter()
		{
			theObject.Name.ShouldEqual("Test");
			theObject.Child.ShouldEqual(new ComplexType {Key = "x", Value = "123"});
		}
	}

	public class StatelessComplexTypeConverter : StatelessConverter<ComplexType>
	{
		protected override ComplexType convert(string text)
		{
			var values = text.Split(new[] { ":" }, StringSplitOptions.None);
			return new ComplexType {Key = values[0], Value = values[1]};
		}
	}
}