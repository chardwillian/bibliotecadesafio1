using System.Collections.Generic;


namespace biblioteca.Models.ViewModels
{
    public class LoanFormViewModel
    {
        public Loan Loan { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
