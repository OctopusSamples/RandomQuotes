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
        #region Slack Magic Token

        public const string SlackMagicToken = "xoxp-275917411184-276618957860-779198091763-a27f41a0d8d0bcf81427162ead680b48";

        #endregion
        
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Suggest(string newAuthorName, string newQuoteText)
        {
            var slackHelper = new SlackHelper(SlackMagicToken);
            slackHelper.SendMessageToSlack($"New quote suggestion:'{newQuoteText}'' by '{newAuthorName}'.");

            ViewData["SuggestionAdded"] = "true";
            
            return RedirectToAction(nameof(Index));
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
