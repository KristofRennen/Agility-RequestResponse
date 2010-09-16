using System;
using Agility.RequestResponse.BackEnd;
using Agility.RequestResponse.Common;
using NUnit.Framework;

namespace Agility.RequestResponse.Tests
{
    [TestFixture]
    public class RequestHandlerTests
    {
        [Test]
        public void Handle_SayHelloToIsEmpty_ReturnsHelloExclamationMark()
        {
            var request = new SayHelloRequest {SayHelloTo = ""};
            var response = new SayHelloRequestHandler().Handle(request) as SayHelloResponse;

            Assert.IsNotNull(response);
            Assert.AreEqual("Hello !", response.Message);
        }

        [Test]
        public void Handle_SayHelloToKristof_ReturnsHelloKristofExclamationMark()
        {
            var request = new SayHelloRequest { SayHelloTo = "Kristof" };
            var response = new SayHelloRequestHandler().Handle(request) as SayHelloResponse;

            Assert.IsNotNull(response);
            Assert.AreEqual("Hello Kristof!", response.Message);
        }

        [Test]
        public void Handle_SayHelloRequestThroughBase_ReturnsCorrectResponse()
        {
            Request request = new SayHelloRequest { SayHelloTo = "Kristof" };
            var response = new SayHelloRequestHandler().Handle(request);

            Assert.IsNotNull(response);
            Assert.IsTrue(response is SayHelloResponse);
            Assert.AreEqual("Hello Kristof!", ((SayHelloResponse) response).Message);
        }

        [Test]
        public void Handle_ThrowsException_ReturnsResponseWithErrors()
        {
            var request = new SayHelloRequest { SayHelloTo = "Kristof" };
            var response = new ExceptionRequestHandler().Handle(request) as SayHelloResponse;

            Assert.IsNotNull(response);
            Assert.IsNull(response.Message);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(1, response.Errors.Count);
        }
    }

    class SayHelloRequest : Request
    {
        public string SayHelloTo { get; set; }
    }

    class SayHelloResponse : Response
    {
        public string Message { get; set; }
    }

    class SayHelloRequestHandler : RequestHandler<SayHelloRequest, SayHelloResponse>
    {
        protected override SayHelloResponse Handle(SayHelloRequest request)
        {
            var response = CreateResponse();

            response.Message = string.Format("Hello {0}!", request.SayHelloTo);

            return response;
        }
    }

    class ExceptionRequestHandler : RequestHandler<SayHelloRequest, SayHelloResponse>
    {
        protected override SayHelloResponse Handle(SayHelloRequest request)
        {
            throw new NotImplementedException();
        }
    }
}