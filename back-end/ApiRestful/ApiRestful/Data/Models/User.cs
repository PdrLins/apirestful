using System;
using System.Collections.Generic;

namespace ApiRestful.Models
{
    public class User
    {
        
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        public ICollection<UserContact> Phones { get; set; }
      

    }
}
