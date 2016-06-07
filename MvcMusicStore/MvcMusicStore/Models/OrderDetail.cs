using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models {
    public class OrderDetail {
        public int OrderDetailId { get; set; }
        public int CartId { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int Quantity { get; set; }
    }
}