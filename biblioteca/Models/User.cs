using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Matrícula")]
        public string Registration { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public User()
        {

        }

        public User(int id, string name, string registration)
        {
            Id = id;
            Name = name;
            Registration = registration;
        }
    }
}
