using System.Linq;
using System.Threading.Tasks;
using FlashCard.Data;
using Microsoft.AspNetCore.Mvc;
using FlashCard.Data.Models;

namespace FlashCard.Service.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class FlashCardController : ControllerBase
    {
        private readonly FlashCardDbContext _db;

        public FlashCardController(FlashCardDbContext dbContext)
        {
            _db = dbContext;
        }
        /// <summary>
        /// Get all FlashCards
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          var cards = _db.Questions.ToList();
          if(cards.Count != 0)
          {
            // randomize cards
            return await Task.FromResult(Ok(cards));
          }
          return await Task.FromResult(NoContent());
        }
        /// <summary>
        /// Get specified FlashCard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
          Question q = _db.Questions.Find(id);
          if(q != null)
          {
            return await Task.FromResult(Ok(q));
          }
          return await Task.FromResult(NotFound());
        }
        /// <summary>
        /// Post a FlashCard
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(Question q)
        {
          if(ModelState.IsValid)
          {
            _db.Questions.Add(q);
            _db.SaveChanges();
            return await Task.FromResult(Ok(q));
          }
          return await Task.FromResult(BadRequest(q));
        }
        /// <summary>
        /// Update the specified FlashCard
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Question q)
        {
          if(ModelState.IsValid)
          {
            Question question = _db.Questions.FirstOrDefault(quest => quest.QuestionId == q.QuestionId);
            if(question == null)
              return await Task.FromResult(NotFound());
            question.QuestionText = q.QuestionText;
            question.Answer = q.Answer;
            question.Subject = q.Subject;
            question.Difficulty = q.Difficulty;
            _db.SaveChanges();
            return await Task.FromResult(NoContent());
          }
          return await Task.FromResult(BadRequest(q));
        }
        /// <summary>
        /// Delete a specific FlashCard
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
          Question q = _db.Questions.Find(id);
          if(q != null)
          {
            _db.Questions.Remove(q);
            _db.SaveChanges();
            return await Task.FromResult(NoContent());
          }
          return await Task.FromResult(NotFound());
        }
        
    }
}