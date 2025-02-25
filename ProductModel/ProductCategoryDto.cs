using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductModel
{
    public class ProductCategoryDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryCode { get; set; }
    }
}
