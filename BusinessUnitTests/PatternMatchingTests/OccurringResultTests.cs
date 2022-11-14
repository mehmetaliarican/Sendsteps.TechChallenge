using Business.Concrete;
using Domain.PatternMatching.Request;
using Domain.PatternMatching.Result;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.CuttingConcerns.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUnitTests.PatternMatchingTests
{
    public class OccurringResultTests : TestBase
    {
        private PatternMatchingService _service;
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
        public void Match(string primary, string secondary, string[] expectedWords)
        {
            Arrange();

            var actualoccurringwords = _service.FindOccurringWords(primary, secondary);
            actualoccurringwords.AddRange(_service.FindOccurringWords(secondary, primary));
            var expected = expectedWords.Any(x => actualoccurringwords.Contains(x));

            Assert.True(expected);
        }

        public void Fail(string primary, string secondary, string expectedoverlappingWord)
        {
            //Arrange();

            Assert.False(false);
        }

    }
}
