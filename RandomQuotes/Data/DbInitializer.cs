using System.Linq;
using RandomQuotes.Models;

namespace RandomQuotes.Data
{
    public class DbInitializer
    {
        public static void Initialize(QuoteContext context)
        {
            context.Database.EnsureCreated();

            if (context.Quotes.Any())
            {
                return; // DB has already been seeded
            }

            var authors = new[]
            {
                new Author { Name = "Rob Siltanen" },
                new Author { Name = "Albert Einstein" },
                new Author { Name = "Charles Eames" },
                new Author { Name = "Henry Ford" },
                new Author { Name = "Antoine de Saint-Exupery" },
                new Author { Name = "Salvador Dali" },
                new Author { Name = "M.C. Escher" },
                new Author { Name = "Paul Rand" },
                new Author { Name = "Elon Musk" },
                new Author { Name = "Jessica Hische" },
                new Author { Name = "Mark Weiser" },
                new Author { Name = "Pablo Picasso" },
                new Author { Name = "Charles Mingus" },
            };

            foreach (var a in authors)
            {
                context.Authors.Add(a);
            }

            var quotes = new[]
            {
                new Quote { Author = authors[0], QuoteText = "Here's to the crazy ones. The misfits. The rebels. The troublemakers. The round pegs in the square holes. The ones who see things differently. They're not fond of rules. And they have no respect for the status quo. You can quote them, disagree with them, glorify or vilify them. About the only thing you can't do is ignore them. Because they change things. They push the human race forward. And while some may see them as the crazy ones, we see genius. Because the people who are crazy enough to think they can change the world, are the ones who do."},
                new Quote { Author = authors[1], QuoteText = "Everything should be made as simple as possible, but not simpler."},
                new Quote { Author = authors[2], QuoteText = "Here is one of the few effective keys to the design problem: the ability of the designer to recognize as many of the constraints as possible; his willingness and enthusiasm for working within these constraints."},
                new Quote { Author = authors[3], QuoteText = "If I had asked people what they wanted, they would have said faster horses."},
                new Quote { Author = authors[4], QuoteText = "Perfection is achieved not when there is nothing more to add, but when there is nothing left to take away."},
                new Quote { Author = authors[5], QuoteText = "Have no fear of perfection, you’ll never reach it."},
                new Quote { Author = authors[6], QuoteText = "Only those who attempt the absurd will achieve the impossible."},
                new Quote { Author = authors[7], QuoteText = "Design is so simple. That’s why it’s so complicated."},
                new Quote { Author = authors[8], QuoteText = "Any product that needs a manual to work is broken."},
                new Quote { Author = authors[9], QuoteText = "The work you do while you procrastinate is probably the work you should be doing for the rest of your life."},
                new Quote { Author = authors[7], QuoteText = "Simplicity is not the goal. It is the by-product of a good idea and modest expectations."},
                new Quote { Author = authors[10], QuoteText = "The most profound technologies are those that disappear. They weave themselves into the fabric of everyday life until they are indistinguishable from it."},
                new Quote { Author = authors[11], QuoteText = "Every child is an artist, the problem is staying an artist when you grow up."},
                new Quote { Author = authors[12], QuoteText = "Creativity is more than just being different. Anybody can plan weird; that’s easy. What’s hard is to be as simple as Bach. Making the simple, awesomely simple, that’s creativity."},
            };
            
            foreach (Quote q in quotes)
            {
                context.Quotes.Add(q);
            }
            context.SaveChanges();

        }
    }
}
