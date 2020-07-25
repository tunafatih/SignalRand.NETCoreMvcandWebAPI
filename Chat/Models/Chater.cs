using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Chater
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string User { get; set; }
        [Required]
        [MaxLength(200)]
        public string Message { get; set; }
    }
}
