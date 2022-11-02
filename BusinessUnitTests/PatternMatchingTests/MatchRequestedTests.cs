using Business.Abstract;
using Business.Concrete;
using Domain.PatternMatching.Request;
using Moq;
using Shared.CuttingConcerns.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessUnitTests.PatternMatchingTests
{
    public class MatchRequestedTests : TestBase
    {
        private IPatternMatchingService _service;
        private PatternMatchingRequest _request;
        public override void Arrange()
        {
            _service = new PatternMatchingService();
            _request = new PatternMatchingRequest();
        }


        [Fact]

        public async void IsValid()
        {
            Arrange();
            _request.Text = "device";
            _request.Word = "ice";

           var result = await _service.MatchAsync(_request);

            var hasElement = result.Occurences?.Any(x => x.Occurences.Count > 0);
            Assert.NotNull(result);
            Assert.True(result.Occurences.Count > 0);
            Assert.True(result.Overlappings.Count> 0);
            Assert.True(hasElement);
        }
        [Fact]
        public async void IsNotValid()
        {
            Arrange();
            _request.Text = "a";
            _request.Word = "b";

            var result = await _service.MatchAsync(_request);

            Assert.NotNull(result);
            Assert.True(result.Occurences.Count== 0);
            Assert.True(result.Overlappings.Count == 0);
        }

    }
}
