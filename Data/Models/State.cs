using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
  public  class State
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Local> Locals { get; set; }

        public State()
        {
            Locals = new HashSet<Local>();
        }
    }
}
