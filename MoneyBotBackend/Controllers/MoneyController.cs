using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBotBackend.DbContext;
using MoneyBotBackend.Models;
using MoneyBotBackend.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyBotBackend.Controllers
{
    [Route("api/[contorller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private MoneyBotContext _context;
        public MoneyController(MoneyBotContext context)
        {
            _context = context;
        }
        [HttpPost("add/{userId}")]
        public async Task<ActionResult> AddMoneyOperation([FromQuery] int userId, [FromBody] MoneyOperation moneyOperation)
        {
            Money money = new Money()
            {
                Sum = moneyOperation.Sum,
                Operation = moneyOperation.Operation,
                DateTime = moneyOperation.DateTime,
                UserId = userId
            };
            _context.Add(money);

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Money>>> GetMoneyOperations(int userId)
        {
            List<Money> operations =  await _context.Moneys.AsNoTracking().Where(h => h.UserId == userId).ToListAsync();

            if (operations == null) return NotFound();

            return operations;
        }
        [HttpGet("{userId}/{skipCount}/{countMoneyOperations}")]
        public async Task<ActionResult<List<Money>>> GetNewOperations(int userId, int skipCount, int countMoneyOperations)
        {
         bool isExistUser =  await _context.Users.AnyAsync(h => h.Id == userId);
            
            if (isExistUser) return NotFound();

           List<Money> operations = await _context.Moneys.AsNoTracking().Where(h => h.UserId == userId).Skip(skipCount).Take(countMoneyOperations).ToListAsync();
            
            if (operations == null) return NotFound();

            return operations;
        }


    }
}
