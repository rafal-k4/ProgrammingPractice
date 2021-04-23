using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticeCase.StudyCases.UnitTests
{
    class MockingHttpClient_And_HttpWebRequest : IStudy
    {
        public void Execute()
        {
            var responseMessage = "test";
            var mockedResponse = WayToMock_HttpWebRequest(responseMessage);

            var mockedHttpClient = WayToMock_HttpClient(responseMessage);
            var responseFromHttpClient = mockedHttpClient.GetAsync("http://test.com").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Debug.Assert(responseMessage == mockedResponse);
            Debug.Assert(responseMessage == responseFromHttpClient);
        }

        private string WayToMock_HttpWebRequest(string responseMessage)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(responseMessage);
            streamWriter.Flush();
            memoryStream.Position = 0;


            var httpWebResponseMock = new Mock<HttpWebResponse>();
            httpWebResponseMock.Setup(x => x.GetResponseStream()).Returns(memoryStream);
            httpWebResponseMock.Setup(x => x.StatusCode).Returns(HttpStatusCode.OK);

            var httpWebRequestMock = new Mock<HttpWebRequest>();
            httpWebRequestMock.Setup(x => x.GetResponse()).Returns(httpWebResponseMock.Object);


            var resultStream = httpWebRequestMock.Object.GetResponse().GetResponseStream();

            using (StreamReader streamReader = new StreamReader(resultStream))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        private HttpClient WayToMock_HttpClient(string responseMessage)
        {
            // how to mock HttpCLient article: https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseMessage)
                })
                .Verifiable();

            return new HttpClient(handlerMock.Object);
        }
    }
}
