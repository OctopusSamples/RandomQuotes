using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomQuotes.Models;

namespace RandomQuotes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var quote = Quote.GetRandomQuote();
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
