//using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web_Cliente_Optica.Encrypt_Tool;
using Web_Cliente_Optica.Models;

namespace Web_Cliente_Optica.Controllers
{
    public class LoginController : Controller //Este controlador contiene todo lo relacionado a Log In y Log Out de los usuarios.
    {
        //Interfaz Ilogger para este controlador (Como estamos en .NET 4.8 esto depende de UnityConfig.cs, FileLoggerProvider.cs y FileLogger.cs)
        private readonly ILogger<LoginController> _loginLogger;
        public LoginController(ILogger<LoginController> loginLogger)
        {
            _loginLogger = loginLogger;
        }

        /// <summary>
        /// Obtiene la vista con el formulario para inicio de sesión.
        /// </summary>
        /// <returns>Void</returns>
        [HttpGet]
        public ActionResult Login()
        {
            Session["currentUser"] = null; //Asegura que aunque se haga un "regresar", si esta lleva al login, se elimine la info del usuario actual.
            return View();
        }

        /// <summary>
        /// Solicitud POST para el inicio de sesión. Envía dos strings (username y password).
        /// Genera un log de intento de inicio de sesión o inicio de sesión exitoso.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Vista Dashboard con un objeto Users en sesión.</returns>
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Users currentUser = new Users();
            try
            {
                using (HttpClient loginConnection = new HttpClient())
                {
                    loginConnection.BaseAddress = new Uri("http://localhost:54263");
                    var loginRequest = loginConnection.GetAsync($"api/Values?username={username}&password={password}").Result;

                    if (loginRequest.IsSuccessStatusCode)
                    {
                        string userResultString = loginRequest.Content.ReadAsStringAsync().Result;
                        currentUser = JsonConvert.DeserializeObject<Users>(userResultString);

                        if (currentUser.UserId != 0) //Una instancia de usuario siempre es definida al inicio del método; si no hubo coindicencias para el inicio de sesión el "usuarioActual" tendría un UserId=0, lo que significaría que las credenciales ingresadas son incorrectas.
                        {
                            _loginLogger.LogInformation($"Inicio de sesión de usuario:\"{username}\" el: [{DateTime.Now}]"); //Creación de log [Inicio de sesión exitoso]
                            Session["currentUser"] = currentUser;
                            //TempData["success"] = $"Mensaje de control: Inicio de sesión para el usuario: {currentUser.UserId} fue exitoso";
                            return RedirectToAction("Dashboard", "Home");
                        }
                        else
                        {
                            _loginLogger.LogInformation($"Intento de inicio de sesión con las credenciales: Username=\"{username}\" y Password=\"{password}\" [{DateTime.Now}]"); //Creación de log [Intento de inicio de sesión fallido] ¿Incluir o cifrar contraseña erronea?
                            TempData["failedLogin"] = ("Usuario y/o contraseña incorrectos, inténtelo de nuevo.");
                            return RedirectToAction("Login");
                        }
                    }
                    else throw new Exception("Error en la conexión al servicio REST");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Login"); //El login no depende de un modelo, por lo que siempre podemos retornar aquí ante un error grave y mostrar la descripción del mismo.
            }
        }

        /// <summary>
        /// Elimina al usuario en sesión.
        /// </summary>
        /// <returns>Vista Login</returns>
        [HttpGet]
        public ActionResult Logout()
        {
            Session["currentUser"] = null;
            return RedirectToAction("Login");
        }
    }
}