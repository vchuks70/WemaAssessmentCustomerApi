using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
   public class Local
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public int StateId { get; set; }
        [JsonIgnore]
        public State State { get; set; }
        public string LocalName { get; set; }
    }
}
