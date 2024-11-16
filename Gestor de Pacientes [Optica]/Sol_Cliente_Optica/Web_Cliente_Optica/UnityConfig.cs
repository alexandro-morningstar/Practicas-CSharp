using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.IO;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Lifetime;
using Web_Cliente_Optica.Controllers;

namespace Web_Cliente_Optica
{
    public static class UnityConfigC
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //Ruta donde se guardan los logs
            var logFilePath = Path.Combine("C:\\Users\\Aleja\\OneDrive\\Documentos\\opticalogs.txt");

            // Configuración de LoggerFactory
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Log en consola
                builder.AddDebug();   // Log en la ventana de salida de Visual Studio
                builder.AddProvider(new FileLoggerProvider(logFilePath)); // Log en archivo...
                builder.SetMinimumLevel(LogLevel.Information); // Nivel mínimo de log
            });

            // Registro del LoggerFactory
            container.RegisterInstance<ILoggerFactory>(loggerFactory, new ContainerControlledLifetimeManager());

            // Binding para ILogger<T>
            container.RegisterType(typeof(ILogger<>), typeof(Logger<>), new ContainerControlledLifetimeManager());

            // Otras configuraciones de servicios
            container.RegisterType<LoginController>(); // Asegúrate de registrar tu controlador

            // Establecer el Dependency Resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}