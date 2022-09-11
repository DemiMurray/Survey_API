using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey_API.Models;
using Survey_API.Services;

namespace Survey_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyService _surveyService;

        public SurveyController(SurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet]
        public ActionResult<Survey> GetSurveys()
        {
            try
            {
                List<Survey> surveys = _surveyService.GetSurveys();

                return Ok(surveys);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Survey> CreateSurvey([FromBody] Survey newSurvey)
        {
            try
            {
                Survey survey = _surveyService.CreateSurvey(newSurvey);

                return Ok(survey);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
