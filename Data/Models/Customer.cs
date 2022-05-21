using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
  public  class Customer: BaseModel
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public bool isVerified { get; set; }
        [Required]
        public State StateOfResidence { get; set; }
        [Required]
        public Local LGA { get; set; }
        [JsonIgnore]
        public ICollection<Otp> Otps { get; set; }  

        public Customer()
        {
            Otps = new HashSet<Otp>();
        }
    }
}
        