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
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public ActionResult<Question> GetQuestions()
        {
            try
            {
                List<Question> questions = _questionService.GetQuestions();

                return Ok(questions);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Question> CreateQuestion([FromBody] Question newQuestion)
        {
            try
            {
                Question question = _questionService.CreateQuestion(newQuestion);

                return Ok(question);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
