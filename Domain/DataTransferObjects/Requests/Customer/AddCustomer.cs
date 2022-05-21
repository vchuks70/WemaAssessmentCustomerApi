using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTransferObjects.Requests.Customer
{
  public  class AddCustomer
    {
        [Required]
        [Phone(ErrorMessage  = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int StateOfResidenceId   { get; set; }
        [Required]
        public int LocalId { get; set; }
    }
}
