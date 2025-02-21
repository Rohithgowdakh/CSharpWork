using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StockDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly StockMarketContext _context;

        public SampleController(StockMarketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFunds()
        {
            var funds = await _context.Funds.ToListAsync();
            return Ok(funds);
        }
    }
}
