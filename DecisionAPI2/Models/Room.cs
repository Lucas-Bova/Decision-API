using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace DecisionAPI2.Models
{
    public class Room
    {
        [Key]
        public int Room_Id { get; set; }
        public int PropIndex { get; set; } //proportion index
        public int PerIndex { get; set; } //person index
        public Guid Guid { get; set; }
        public ICollection<Option> Options { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}