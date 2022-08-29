using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models {
    public class Book
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Quantidade")]
        public int Amount { get; set; }
        public ICollection<Loan> Loans { get; set; }
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
