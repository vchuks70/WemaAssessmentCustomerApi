using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
   public class Otp:BaseModel
    {
        public string Value { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool isUsed { get; set; }
        public bool isDisabled { get; set; }    
        public DateTime? DateUsed { get; set; }
        public DateTime? validilityTime { get; set; }
    }   
}
            