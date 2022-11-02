using Business.Abstract;
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
    public class CaseNullValueUnitTests : TestBase
    {
        private Mock<IPatternMatchingService> _mockService;
        public override void Arrange()
        {
            _mockService = new Mock<IPatternMatchingService>();
           
        }


        [Theory]
        [MemberData("TestFailData")]
        public void ShouldFailUponNullValue(PatternMatchingRequest request)
        {
            Arrange();

            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _mockService.Object.MatchAsync(request);
            });
        }

        private static IEnumerable<object[]> TestFailData()
        {
            var firstCase= new object[]
            {
                new PatternMatchingRequest {  Text = "", Word = ""}
            };

            var secondcase = new object[]
           {
                new PatternMatchingRequest {  Text = null, Word = null}
           };

           var thirdCase = new object[]
           {
                null
           };

            return new[] { firstCase,secondcase,thirdCase };
        }
    }
}
