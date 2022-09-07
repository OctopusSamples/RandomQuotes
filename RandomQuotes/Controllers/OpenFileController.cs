using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;


namespace RandomQuotes.Controllers
{
    public class OpenFileController : Controller
    {
        // testing normal: /path_traversal?name=file.tt
        // testing exploit: /path_traversal?name=../file.txt
        [HttpGet("path_traversal")]
        public IActionResult Get(string name)
        {
            using var fstre = new FileStream(name, FileMode.Open, FileAccess.Read);
            using var sree = new StreamReader(fstre, Encoding.UTF8);
            string Fcontent = sree.ReadToEnd();
            return Ok(Fcontent);
        }
    }
}