using FubuJson;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Json.Tests
{
	[TestFixture]
	public class when_rendering_json_from_a_model_object_with_overriden_output_mimetype : InteractionContext<NewtonSoftJsonWriter>
	{
		private ParentType theOutput;
		private InMemoryOutputWriter theOutputWriter;
		private string theJson;

		protected override void beforeEach()
		{
			theOutput = new ParentType
			            	{
			            		Name = "Josh",
			            		Child = new ComplexType { Key = "Test", Value = "Value" }
			            	};

			theJson = "Hello";

			theOutputWriter = new InMemoryOutputWriter();
			Services.Inject<IOutputWriter>(theOutputWriter);

			MockFor<IJsonSerializer>().Stub(x => x.Serialize(theOutput)).Return(theJson);

			ClassUnderTest.Write(theOutput, MimeType.Json.ToString());
		}


		[Test]
		public void writes_the_json_mime_type()
		{
			theOutputWriter.ContentType.ShouldEqual(MimeType.Json.ToString());
		}

		[Test]
		public void writes_the_json_from_the_json_serializer()
		{
			theOutputWriter.ToString().TrimEnd().ShouldEqual(theJson);
		}
	}
}