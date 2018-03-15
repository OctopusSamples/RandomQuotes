using Microsoft.AspNetCore.Mvc;
using RandomQuotes.Data.Models;

namespace RandomQuotes.Web.SharingModule.Controllers
{
    public class SharingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Share([Bind("QuoteText, AuthorName, SharingContactName, SharingContactEmail")] FavoriteQuoteViewModel favoriteQuote)
        {
            // TODO: Save Shared Quote for review ... 
            return RedirectToAction("Thanks");
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}