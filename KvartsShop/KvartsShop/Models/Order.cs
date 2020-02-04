using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KvartsShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}