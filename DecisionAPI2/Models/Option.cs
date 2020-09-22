using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DecisionAPI2.Models
{
    public class Option
    {
        [Key]
        public int Option_Id { get; set; }
        public string OptionName { get; set; }
        public int VoteCount { get; set; }
        [ForeignKey("RoomModel")]
        public int Room_Id { get; set; }

        public Room RoomModel { get; set; }
    }
}