using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using MvcMusicStore.Models;

namespace MvcMusicStore {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MusicStoreDbInitializer : DropCreateDatabaseAlways<MusicStoreDB> {
        protected override void Seed(MusicStoreDB context) {
            var zard = new Artist { Name = "Zard" };
            var glay = new Artist { Name = "Glay" };
            var jpop = new Genre { Name = "J-pop" };
            var rock = new Genre { Name = "Rock" };

            context.Artists.Add(zard);
            context.Artists.Add(new Artist { Name = "Mr Children" });
            context.Artists.Add(glay);
            context.Artists.Add(new Artist { Name = "Every Little Thing" });

            context.Genres.Add(new Genre { Name = "Country" });

            context.Albums.Add(new Album {
                Artist = zard,
                Genre = jpop,
                Price = 29.99m,
                Title = "Oh My Love"
            });
            context.Albums.Add(new Album {
                Artist = zard,
                Genre = jpop,
                Price = 29.99m,
                Title = "Forever"
            });
            context.Albums.Add(new Album {
                Artist = glay,
                Genre = rock,
                Price = 29.99m,
                Title = "Curtain Call"
            });
            context.Albums.Add(new Album {
                Artist = glay,
                Genre = rock,
                Price = 29.99m,
                Title = "However"
            });
            context.Albums.Add(new Album {
                Artist = glay,
                Genre = rock,
                Price = 29.99m,
                Title = "Desire"
            });
            base.Seed(context);
        }
    }

    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            Database.SetInitializer(new MusicStoreDbInitializer());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}