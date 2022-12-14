using Business.Abstract;
using Domain.Consistency;
using Domain.PatternMatching.Request;
using Domain.PatternMatching.Result;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace Sendsteps.TechnicalChallenge.PatternMatcher.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatternMatchingController : ControllerBase
    {
        

        private readonly ILogger<PatternMatchingController> _logger;
        private readonly IPatternMatchingService _patternMatchingService;

        public PatternMatchingController(ILogger<PatternMatchingController> logger, IPatternMatchingService patternMatchingService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _patternMatchingService = patternMatchingService ?? throw new ArgumentNullException(nameof(patternMatchingService));
        }

        [HttpPost]
        public async Task<ActionResult<MatchingResult>> Match([FromBody]PatternMatchingRequest request)
        {
            try
            {
                _logger.LogInformation(Request.HttpInfo($"Executing the request for words : '{request.Primary} and {request.Secondary}'"));
                request.Primary.Trim();
                request.Secondary.Trim();
                var result = await _patternMatchingService.MatchAsync(request);
                return Ok(new GenericHttpResponse<MatchingResult>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(Request.HttpException(ex));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}