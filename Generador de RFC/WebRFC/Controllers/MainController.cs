using Business;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace WebRFC.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult GoToGenerate()
        {
            return View("GenerateView");
        }

        public ActionResult Create(E_RFC user) //Crea un registro de usuario, en la capa negocio se genera el RFC
        {
            B_RFC creator =  new B_RFC(); //Objeto de la capa de negocio con las herramientas

            try
            {
                creator.Create(user);
                return RedirectToAction("GoToRFC");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("GenerateView");
            }
        }

        public ActionResult GoToRFC()
        {
            B_RFC getRfc = new B_RFC();
            try
            {
                string rfc = getRfc.GetLastRFC();
                ViewData["rfc"] = rfc;
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("RFCView");
        }

        public ActionResult GoToRecords()
        {
            List<E_RFC> records = new List<E_RFC>();
            B_RFC businessTool = new B_RFC();
            int count;

            try
            {
                records = businessTool.GetAllUsers();
                count = businessTool.CountUsers();
                ViewData["userscount"] = $"Existen {count} registros en la base de datos.";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View("RecordView", records);
        }

        public ActionResult Search(string text)
        {
            B_RFC searcher = new B_RFC();
            List<E_RFC> users = new List<E_RFC>();
            int count;

            try
            {
                users = searcher.Search(text);
                count = searcher.CountUsers();
                ViewData["userscount"] = $"Existen {count} registros en la base de datos.";
                return View("RecordView", users);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GoToRecords");
            }
        }

        public ActionResult GoToEdit(int id)
        {
            E_RFC user = new E_RFC();
            B_RFC getUserTool = new B_RFC();
            try
            {
                user = getUserTool.GetByID(id);
                return View("EditView", user);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GoToRecords");
            }
        }

        public ActionResult Edit(E_RFC user, bool? confirmation = null)
        {
            if (confirmation == null)
            {
                TempData["error"] = "No se confirmó la acción";
                return RedirectToAction("GoToRecords");
            }
            else
            {
                B_RFC updaterTool = new B_RFC();

                try
                {
                    updaterTool.Edit(user);
                    TempData["success"] = $"El usuario {user.GetName} con ID: {user.GetIdRFC},ha sido actualizado exitosamente.";
                    return RedirectToAction("GoToRecords");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("GoToRecords");
                }
            }
        }

        public ActionResult GoToDelete(int id)
        {
            E_RFC user = new E_RFC();

            try
            {
                B_RFC getUser = new B_RFC();
                user = getUser.GetByID(id);
                return View("DeleteView", user);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GoToRecords");
            }
        }

        public ActionResult Delete(int id, bool? confirmation = null)
        {
            if (confirmation == null)
            {
                TempData["error"] = "No se confirmó la acción";
                return RedirectToAction("GoToRecords");
            }
            else
            {
                B_RFC deleter = new B_RFC();

                try
                {
                    deleter.DeleteById(id);
                    TempData["success"] = $"La el usuario con el ID: {id} se ha eliminado satisfactoriamente";
                    return RedirectToAction("GoToRecords");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("GoToRecords");
                }
            }
        }
        public ActionResult About()
        {
            return View("AlexandrosView");
        }
    }
};