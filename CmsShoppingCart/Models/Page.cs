using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class Page
    {
        public int PageId { get; set; }
        [Required,MinLength(2,ErrorMessage ="Minimum legnth is 2")]
        public string PageTitle { get; set; }
        
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minimum legnth is 4")]
        public string Content { get; set; }
        public int Sorting { get; set; }

    }
}
