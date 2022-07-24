using System.Collections.Generic;

namespace biblioteca.Models {
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }

        public ICollection<Loan> Loans { get; set; }
        //  public int AmountBorrowed { get; set; }

        public Book()
        {

        }

        public Book(int id, string name, int amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }
    }
}
