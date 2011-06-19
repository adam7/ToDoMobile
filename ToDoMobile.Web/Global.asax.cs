using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Client;
using Raven.Client.Document;

namespace ToDoMobile.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string RavenSessionKey = "Raven.Session";
        private static DocumentStore documentStore;

        public static IDocumentSession CurrentSession
        {
            get { return HttpContext.Current.Items[RavenSessionKey] as IDocumentSession; }
        }

        public MvcApplication()
        {
            // Open a RavenDB session and store it in the CurrentSession at the beginning of each request
            BeginRequest += (sender, args) => HttpContext.Current.Items[RavenSessionKey] = documentStore.OpenSession();

            // Dispose the RavenDB session at the end of each request 
            EndRequest += (o, eventArgs) =>
            {
                var disposable = HttpContext.Current.Items[RavenSessionKey] as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            };
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    "Default", // Route name
                    "{controller}/{action}/{id}", // URL with parameters
                    new { controller = "ToDo", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            documentStore = new EmbeddableDocumentStore
            {
                ConnectionStringName = "Local"
            };
            documentStore.Conventions.IdentityPartsSeparator = "_";
            documentStore.Initialize();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}