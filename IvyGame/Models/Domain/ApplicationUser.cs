using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IvyGame.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(string firstName, string lastName, string email, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            UserName = email;
        }

        [MaxLength(50)]
        public string FirstName { get; protected set; }

        [MaxLength(50)]
        public string LastName { get; protected set; }

        [MaxLength(200)]
        public string Address { get; protected set; }
    }
}
