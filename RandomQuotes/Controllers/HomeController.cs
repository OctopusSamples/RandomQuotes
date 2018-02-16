using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandomQuotes.Data;
using RandomQuotes.Models;

namespace RandomQuotes.Controllers
{
    public class HomeController : Controller
    {
        private readonly Random _random = new Random();
        private readonly QuoteContext _quoteContext;

        public HomeController(QuoteContext quoteContext)
        {
            _quoteContext = quoteContext;
        }

        public async Task<IActionResult> Index()
        {
            var index = _random.Next(1, _quoteContext.Quotes.Count() - 1);
            return View(await _quoteContext.Quotes.Include(x => x.Author).Where(q => q.ID == index).FirstOrDefaultAsync());
        }

        public async Task<IActionResult> QuotesByAuthor(int authorId)
        {
            return View(await _quoteContext.Quotes.Include(x => x.Author).Where(q => q.AuthorID == authorId).ToListAsync());
        }

        public async Task<IActionResult> ReloadPage()
        {
            return await Task.Run<ActionResult>(() => RedirectToAction("Index", "Home"));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
