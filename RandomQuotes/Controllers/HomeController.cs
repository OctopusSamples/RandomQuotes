using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RandomQuotes.Data;
using RandomQuotes.Models;

namespace RandomQuotes.Controllers
{
    public class HomeController : Controller
    {
        private Random _random;
        private readonly QuoteContext _quoteContext;

        public HomeController(QuoteContext quoteContext)
        {
            _quoteContext = quoteContext;
        }

        public IActionResult Index()
        {
            var index = _random.Next(_quoteContext.Quotes.Count());
            var quote = _quoteContext.Quotes.Find(index);
            return View(quote);
        }

        [HttpPost]
        public IActionResult ReloadPage()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
