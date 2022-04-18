using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiveBrother.Models
{
    public class Post
    {
        [Key]
        public int PotsId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Published { get; set; }
        public string Imgage { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int CatId { get; set; }
        public virtual Category Cat { get; set; }
    }
}
