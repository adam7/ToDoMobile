using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace ToDoMobile.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private const string RavenSessionKey = "Raven.Session";
        private static DocumentStore _documentStore;

        public MvcApplication()
        {
            BeginRequest += (sender, args) => HttpContext.Current.Items[RavenSessionKey] = _documentStore.OpenSession();
            EndRequest += (o, eventArgs) =>
            {
                // If there aren't any errors save any document changes that have been made
                if (Context.Error == null)
                {
                    CurrentSession.SaveChanges();
                }
                var disposable = HttpContext.Current.Items[RavenSessionKey] as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            };
        }

        public static IDocumentSession CurrentSession
        {
            get { return HttpContext.Current.Items[RavenSessionKey] as IDocumentSession; }
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
            _documentStore = new EmbeddableDocumentStore
            {
                ConnectionStringName = "Local"
            };
            _documentStore.Conventions.IdentityPartsSeparator = "_";
            _documentStore.Conventions.MaxNumberOfRequestsPerSession = 30;
            _documentStore.Initialize();

            IndexCreation.CreateIndexes(typeof(MvcApplication).Assembly, _documentStore);

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}