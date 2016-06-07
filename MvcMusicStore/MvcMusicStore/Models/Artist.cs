using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models {
    public class Artist {
        public virtual int ArtistId { get; set; }

        [Required]
        [DisplayName("Artist")]
        public virtual string Name { get; set; }
    }
}