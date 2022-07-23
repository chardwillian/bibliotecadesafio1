using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biblioteca.Models;
using biblioteca.Models.Enums;

namespace biblioteca.Data
{
    public class SeedingService
    {
        private BibliotecaContext _context;

        public SeedingService(BibliotecaContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.User.Any() || 
               _context.Book.Any() ||
               _context.Loan.Any())
            {
                return;  // O banco já foi populado.
            }

            User user1 = new User(1, "Chard Willian", "2018108000");
            User user2 = new User(2, "Novais Mendonça", "2020105000");
            User user3 = new User(3, "Mariazinha maria", "2015133000");
            User user4 = new User(4, "Joãozinho joão", "2017744400");
            User user5 = new User(5, "Zé josé", "2018232300");

            Book book1 = new Book(1, "Harry Poter", 3);
            Book book2 = new Book(2, "Senhor dos Anéis", 1);
            Book book3 = new Book(3, "Segurança de redes", 10);
            Book book4 = new Book(4, "Javascript", 7);

            Loan loan1 = new(1, new DateTime(2022, 7, 14, 14, 30, 10), 1, LoanStatus.Activated, user1, book2);
            Loan loan2 = new(2, new DateTime(2022, 7, 14, 11, 10, 05), 1, LoanStatus.Disabled, user4, book4);

            _context.User.AddRange(user1, user2, user3, user4, user5);

            _context.Book.AddRange(book1, book2, book3, book4);

            _context.Loan.AddRange(loan1, loan2);

            _context.SaveChanges();
        }
    }
}
