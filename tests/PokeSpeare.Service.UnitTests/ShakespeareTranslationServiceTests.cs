using Moq;
using Moq.Protected;
using NUnit.Framework;
using PokeSpeare.Service.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PokeSpeare.Service.UnitTests
{
    public class ShakespeareTranslationServiceTests
    {
        private ShakespeareTranslationService _shakespeareTranslationService;
        private const string TextToTranslate = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";
        private const string TranslatedText = "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";

        [Test]
        public async Task ShouldGetShakespeareTranslation()
        {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'success':{'total':1},'contents':{'translated':'Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.','text':'You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.','translation':'shakespeare'}}"),
                })
                .Verifiable();

            var client = new HttpClient(mockHandler.Object);
            _shakespeareTranslationService = new ShakespeareTranslationService(client);

            var result = await _shakespeareTranslationService.Translate(TextToTranslate);

            Assert.IsTrue(string.Equals(result, TranslatedText));
        }

        [Test]
        public async Task ShouldReturnOriginalStringWhenInvalidOperationExceptionIsRaised()
        {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .Throws<InvalidOperationException>()
                //.ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound))
                .Verifiable();

            var client = new HttpClient(mockHandler.Object);
            _shakespeareTranslationService = new ShakespeareTranslationService(client);

            var result = await _shakespeareTranslationService.Translate(TextToTranslate);

            Assert.IsTrue(string.Equals(result, TextToTranslate));
        }

        [Test]
        public async Task ShouldReturnOriginalStringWhenHttpRequestExceptionIsRaised()
        {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .Throws<HttpRequestException>()
                //.ReturnsAsync(new HttpResponseMessage(HttpStatusCode.NotFound))
                .Verifiable();

            var client = new HttpClient(mockHandler.Object);
            _shakespeareTranslationService = new ShakespeareTranslationService(client);

            var result = await _shakespeareTranslationService.Translate(TextToTranslate);

            Assert.IsTrue(string.Equals(result, TextToTranslate));
        }
    }
}