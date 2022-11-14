using Business.Abstract;
using Business.Concrete;
using Castle.Components.DictionaryAdapter.Xml;
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
    public class OverlappingResultTests : TestBase
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
        [InlineData("ice", "device", "")]
        [InlineData("intercities", "ice", "i")]
        [InlineData("ice", "intercities", "i")]
        [InlineData("client", "ice", "i")]
        [InlineData("ice", "client", "c")]
        [InlineData("orange", "rhinoceros", "r")]
        [InlineData("rhinoceros", "orange", "o")]
        [InlineData("client", "cientl", "c")]
        [InlineData("ABCDEBEC", "BCBEACAE", "BC")]
        [InlineData("ABABCBAABBDACBDADACD", "CDDAABACBCADCBDACDAB", "CD")]
        public void Match(string primary, string secondary,string expectedoverlappingWord)
        {
            Arrange();

            var actualyoverllapingword = _service.FindOverlappingWord(primary,secondary);
            

            Assert.Equal(expectedoverlappingWord,actualyoverllapingword);
        }

        [Theory]
        [InlineData("device", "vice", "ice")]
        [InlineData("ice", "device", "ice")]
        public void Fail(string primary, string secondary, string expectedoverlappingWord)
        {
            Arrange();

            var actualyoverllapingword = _service.FindOverlappingWord(primary, secondary);


            Assert.NotEqual(expectedoverlappingWord, actualyoverllapingword);
        }




    }
}
