using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webApi_test.Models
{
    public class BookingModel
    {   
        [Key]
        public Guid Id { get; set; }

        public int ResId { get; set; }

        [ForeignKey("ResId")]
        public virtual ResourceModel ResourceModel { get; set; }

        [MaxLength(450)][Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        [DataType("time")]
        public DateTime StartTime { get; set; }

        [DataType("time")]
        public DateTime EndTime { get; set; }

        [DataType("date")]
        public DateTime BookingDate { get; set; }


    }
}
