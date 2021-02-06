using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Message
    {
        [Key]
        public string clientuniqueid { get; set; }
        public string connectionid { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        [NotMapped]
        public string type { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
    }
}
