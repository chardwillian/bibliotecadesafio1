using biblioteca.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Display(Name= "Inicio empréstimo")]
        public DateTime StartLoan { get; set; }
        [Display(Name = "Quantidade")]
        public int Amount { get; set; }
        public LoanStatus Status { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Loan()
        {

        }

        public Loan(int id, DateTime startLoan, int amount, LoanStatus status, User user, Book book)
        {
            Id = id;
            StartLoan = startLoan;
            Amount = amount;
            Status = status;
            User = user;
            Book = book;
        }
    }
}
