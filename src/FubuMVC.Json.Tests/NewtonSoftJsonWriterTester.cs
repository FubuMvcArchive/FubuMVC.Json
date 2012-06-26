using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Json.Tests
{
	[TestFixture]
	public class WhenRenderingJsonFromAModelObjectUsingDefaultMimeType : InteractionContext<NewtonSoftJsonWriter>
	{
		private ParentType theOutput;

		protected override void beforeEach()
		{
			theOutput = new ParentType
			{
				Name = "Josh",
				Child = new ComplexType { Key = "Test", Value = "Value"}
			};

			Services.PartialMockTheClassUnderTest();
			ClassUnderTest.Expect(x => x.Write(theOutput, MimeType.Json.ToString()));
			ClassUnderTest.Write(theOutput);
		}


		[Test]
		public void writes_the_json_mime_type()
		{
			ClassUnderTest.AssertWasCalled(x => x.Write(theOutput, MimeType.Json.ToString()));
		}
	}
}