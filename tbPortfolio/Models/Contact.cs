using System;
using System.ComponentModel.DataAnnotations;

namespace tbPortfolio.Controllers
{

    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //[Required]
        //public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}