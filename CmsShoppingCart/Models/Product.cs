using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum legth is 2")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minimum legth is 4")]
        public string Description { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public double Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
      
        [ForeignKey("CategoryId")]
        public virtual Categories Categories{get;set;}

    }
}
