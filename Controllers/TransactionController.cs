using expense_tracker_api.Domain;
using expense_tracker_api.DTOs;
using expense_tracker_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker_api.Controllers
{

    [ApiController]
    [Route("/api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionDbContext _db;
        public TransactionController(TransactionDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {

            //filtration
            
            //end

            var transactions = await _db.Transactions
                .Select(t => new TransactionDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Amount = t.Amount,
                    Date = t.Date,
                    CategoryId = t.CategoryId
                })
                .ToListAsync(); 

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var transaction = await _db.Transactions
                .Where(t => t.Id == id)
                .Select(t => new TransactionDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Amount = t.Amount,
                    Date = t.Date,
                    CategoryId = t.CategoryId
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionDTO request)
        {
            if (request == null)
                return BadRequest();

            var transaction = new Transaction
            {
                Title = request.Title,
                Amount = request.Amount,
                CategoryId = request.CategoryId
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();

            var responseDto = new TransactionDTO
            {
                Id = transaction.Id,       
                Title = transaction.Title,
                Amount = transaction.Amount,
                Date = transaction.Date,   
                CategoryId = transaction.CategoryId
            };

            return Ok(responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id) 
        {
            var transaction = await _db.Transactions.FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(UpdateTransactionDTO request, Guid id)
        {
            if (request == null)
                return BadRequest();

            var transaction = await _db.Transactions.FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            transaction.Title = request.Title;
            transaction.Amount = request.Amount;

            await _db.SaveChangesAsync();

            var responseDTO = new TransactionDTO
            {
                Id = transaction.Id,
                Title = transaction.Title,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
            };

            return Ok(responseDTO);
        }
    }
}
