using System;
using System.Linq;
using System.Text;
using BookShop.Models;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);
                var result = RemoveBooks(db);
                Console.WriteLine($"{result} books were deleted");
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Copies<4200).ToArray();

            int count = books.Length;
            context.Books.RemoveRange(books);

            context.SaveChanges();

            return count;
        }
    }
}
