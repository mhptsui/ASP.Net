using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models {
    public class Cart {
        public int CartId { get; set; }

        [Required]
        public string UserName { get; set; }
        public List<OrderDetail> CartItems { get; set; }
    }
}