using Business.Abstract;
using Business.Concrete;
using Domain.PatternMatching.Request;
using Domain.PatternMatching.Result;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using Shared.CuttingConcerns.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUnitTests.PatternMatchingTests
{
    public class MatchingValueTests : TestBase
    {
        private PatternMatchingService _service;
        private PatternMatchingRequest _request;
        private MatchingResult _result;
        private Mock<ILogger<PatternMatchingService>> _loggerMock;
        protected override void Arrange()
        {
            _loggerMock = new Mock<ILogger<PatternMatchingService>>();
            _loggerMock.Setup(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                )
             );
            _service = new PatternMatchingService(_loggerMock.Object);
            _request = new PatternMatchingRequest();
        }

        [Theory]
        [InlineData("device", "ice","ice")]
        [InlineData("batman", "bat","bat")]
        [InlineData("ice", "device","")]
        [InlineData("intercities", "ice","i")]
        [InlineData("ice", "intercities","i")]
        [InlineData("client", "ice","i")]
        [InlineData("ice", "client","c")]
        [InlineData("orange", "rhinoceros","r")]
        [InlineData("rhinoceros", "orange","o")]
        [InlineData("client", "cientl","c")]
        [InlineData("ABCDEBEC", "BCBEACAE","BC")]
        [InlineData("ABABCBAABBDACBDADACD", "CDDAABACBCADCBDACDAB", "CD")]
        public async void ShouldMatchOverlappings(string primary, string secondary,string matched)
        {
            Arrange();
            _request.Primary = primary;
            _request.Secondary = secondary;

            var result = await _service.MatchAsync(_request);

            Assert.Equal(matched, result.Value);
        }

        [Theory]
        [InlineData("device", "ice", new[] { "ice" })]
        [InlineData("batman", "bat", new[] { "bat" })]
        [InlineData("ice", "device", new[] { "ice" })]
        [InlineData("intercities", "ice", new[] { "ice" })]
        [InlineData("ice", "intercities", new[] { "ice" })]
        [InlineData("client", "ice", new[] { "ie", "ce" })]
        [InlineData("ice", "client", new[] { "ie", "ce" })]
        [InlineData("orange", "rhinoceros", new[] { "rne" })]
        [InlineData("rhinoceros", "orange", new[] { "rne" })]
        [InlineData("client", "cientl", new[] { "cient" })]
        [InlineData("ABCDEBEC", "BCBEACAE", new[] { "BCBEC" })]
        //[InlineData("ABABCBAABBDACBDADACD", "CDDAABACBCADCBDACDAB", new[] { "ABABCADCBDADA" })]
        public async void ShouldMatchOccurencedItems(string primary, string secondary, string[] occurences)
        {
            {
                Arrange();
                _request.Primary = primary;
                _request.Secondary = secondary;
                _result = new MatchingResult(primary, secondary);

                await _service.SetOccurencesAsync(_result,_request);

                var expected = _result.Occurrences.All(x => occurences.Contains(x));

                Assert.True(expected);

            }
        }
    }
}
