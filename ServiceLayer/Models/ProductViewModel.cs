using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Declareation { get; set; }
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string PictureTxt { get; set; }
    }
}
