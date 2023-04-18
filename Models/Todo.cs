// Local directives
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }
        [Required]
        [Column(TypeName="nvarchar(250)")]
        public string TodoTitle { get; set; } = "";
    }
}