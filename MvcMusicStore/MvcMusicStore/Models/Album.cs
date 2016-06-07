using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models {
    public class Album {
        public virtual int AlbumId { get; set; }

        [Required(ErrorMessage = "Please enter the genre")]
        public virtual int GenreId { get; set; }
        
        [Required(ErrorMessage = "Please enter the artist")]
        public virtual int ArtistId { get; set; }

        [Required(ErrorMessage="Please enter album title")]
        [DisplayName("Album Title")]
        public virtual string Title { get; set; }

        [Required(ErrorMessage="Please enter the price")]
        public virtual decimal Price { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Genre Genre { get; set; }
    }
}