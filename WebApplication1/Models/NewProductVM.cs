using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class NewProductVM
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0,9999)]
        public Nullable<decimal> Price { get; set; }
    }
}