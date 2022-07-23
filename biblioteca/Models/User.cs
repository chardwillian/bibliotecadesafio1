using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace biblioteca.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }

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
