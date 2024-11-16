using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web_Cliente_Optica.Models;
using Web_Cliente_Optica;

namespace Web_Cliente_Optica
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Configurar Unity
            UnityConfigC.RegisterComponents();

            // Configura las áreas y las rutas
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
