using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using Web_Cliente_Optica.Models;

namespace Web_Cliente_Optica.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Comprueba que exista un usuario loggeado en la sesión.
        /// Obtiene una vista superficial de todos los registros de pacientes.
        /// </summary>
        /// <returns>Vista con modelo PatientsOverview</returns>
        [HttpGet]
        public ActionResult Dashboard()
        {
            if (Session["currentUser"] == null) //Comprobar que exista un usuario loggeado (que exista en el SessionStorage)
            {
                TempData["error"] = "No se enviaron datos de inicio de sesión";
                return RedirectToAction("Login", "Login");
            }

            List<PatientsOverview> patients = new List<PatientsOverview>();
            
            try
            {
                var currentUser = (Users)Session["currentUser"]; //Almacenamos el objeto User (usuario actual) del Session en una variable para poder acceder a sus propiedades.

                using (HttpClient dashboardConnection = new HttpClient())
                {
                    dashboardConnection.BaseAddress = new Uri("http://localhost:54263");
                    var dashboardRequest = dashboardConnection.GetAsync("api/Values/PatientsOverview").Result;

                    if (dashboardRequest.IsSuccessStatusCode)
                    {
                        string dashboardResultString = dashboardRequest.Content.ReadAsStringAsync().Result;
                        patients = JsonConvert.DeserializeObject<List<PatientsOverview>>(dashboardResultString);

                        ViewBag.IsAdmin = currentUser.IsAdmin; //Generamos una ViewBag llamada IsAdmin que almacena el valor de la propiedad IsAdmin del objeto Users (usuario actual).
                        //TempData["success"] = "Mensaje de control: La información se ha recuperado con éxito desde el servicio REST";
                        return View("Dashboard", patients);
                    }

                    else throw new Exception("Error en la conexión al servicio REST");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Algo ha salido mal: {ex.Message} ";
                return View("Dashboard", patients);
            }
        }

        /// <summary>
        /// Comprueba que exista un usuario loggeado en la sesión.
        /// Obtiene una vista superficial de todos los registros de pacientes filtrados por el texto ingresado (Nombre(s) y/o apellido(s)).
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>Vista con modelo PatientsOverview (filtados por la busqueda).</returns>
        [HttpGet]
        public ActionResult Search(string searchString)
        {
            if (Session["currentUser"] == null) //Comprobar que exista un usuario loggeado (que exista en el SessionStorage)
            {
                TempData["error"] = "No se enviaron datos de inicio de sesión";
                return RedirectToAction("Login", "Login");
            }

            if (searchString != "")
            {
                List<PatientsOverview> patients = new List<PatientsOverview>();

                try
                {
                    var currentUser = (Users)Session["currentUser"]; //Almacenamos el objeto User (usuario actual) del Session en una variable para poder acceder a sus propiedades.

                    searchString = searchString.Replace(' ', '&');

                    using (HttpClient searchConnection = new HttpClient())
                    {
                        searchConnection.BaseAddress = new Uri("http://localhost:54263");
                        var searchRequest = searchConnection.GetAsync($"api/Values/Search?searchString={searchString}").Result;

                        if (searchRequest.IsSuccessStatusCode)
                        {
                            string searchResultString = searchRequest.Content.ReadAsStringAsync().Result;
                            patients = JsonConvert.DeserializeObject<List<PatientsOverview>>(searchResultString);

                            ViewBag.IsSearch = true;
                            ViewBag.IsAdmin = currentUser.IsAdmin;
                            return View("Dashboard", patients);
                        }
                        else throw new Exception("Error en la conexión al servicio REST");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Algo ha salido mal: {ex.Message}";
                    return RedirectToAction("Dashboard");
                }
            }

            else
            {
                return RedirectToAction("Dashboard");
            }
            
        }

        /// <summary>
        /// Obtiene la lista de padecimientos y formulario para registrar a un nuevo paciente.
        /// Comprueba que exista un usuario loggeado en la sesión.
        /// </summary>
        /// <returns>Vista con formulario de registro y lista con modelo Diseases.</returns>
        [HttpGet]
        public ActionResult AddUser()
        {
            if (Session["currentUser"] == null)
            {
                TempData["error"] = "No se enviaron datos de inicio de sesión";
                return RedirectToAction("Login", "Login");
            }

            else
            {
                List<Diseases> allDiseases = new List<Diseases>();
                try
                {
                    using (HttpClient getDiseasesConnection = new HttpClient())
                    {
                        getDiseasesConnection.BaseAddress = new Uri("http://localhost:54263");
                        var diseasesRequest = getDiseasesConnection.GetAsync("api/Values/GetDiseases").Result;

                        if (diseasesRequest.IsSuccessStatusCode)
                        {
                            //Recuperamos la lista de diseases
                            string diseasesResultString = diseasesRequest.Content.ReadAsStringAsync().Result;
                            allDiseases = JsonConvert.DeserializeObject<List<Diseases>>(diseasesResultString);

                            ViewBag.DiseasesList = allDiseases;
                            return View("Create");
                        }
                        else throw new Exception("Error en la conexión al servicio REST");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Dashboard");
                }
            }
        }

        [HttpGet]
        public JsonResult GetJsonDiseases()
        {
            List<Diseases> allDiseases = new List<Diseases>();
            try
            {
                using (HttpClient getDiseasesConnection = new HttpClient())
                {
                    getDiseasesConnection.BaseAddress = new Uri("http://localhost:54263");
                    var diseasesRequest = getDiseasesConnection.GetAsync("api/Values/GetDiseases").Result;

                    if (diseasesRequest.IsSuccessStatusCode)
                    {
                        string diseasesResultString = diseasesRequest.Content.ReadAsStringAsync().Result;
                        allDiseases = JsonConvert.DeserializeObject<List<Diseases>>(diseasesResultString);

                        return Json(new { status = "ok", alldiseases = allDiseases }, JsonRequestBehavior.AllowGet);
                    }
                    else { throw new Exception("Error en la conexión al servicio REST"); }
                }

            }
            catch (Exception ex)
            {
                return Json(new { status = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddMedicalRecord(int patientID, string patientName, string patientLastName)
        {
            if (Session["currentUser"] == null)
            {
                TempData["error"] = "No se enviaron datos de inicio de sesión";
                return RedirectToAction("Login", "Login");
            }

            else
            {
                List<Diopters> dioptersMR = new List<Diopters>();
                string[] patient = { patientName, patientLastName }; //Array que contiene el nombre y apellido para mostrarlo a través de un viewbag en la vista de historia clinica (medical record).
                try
                {
                    using (HttpClient getDioptersMRConnection = new HttpClient())
                    {
                        getDioptersMRConnection.BaseAddress = new Uri("http://localhost:54263");
                        var dioptersRequest = getDioptersMRConnection.GetAsync("api/Values/GetDioptersMR").Result;

                        if (dioptersRequest.IsSuccessStatusCode)
                        {
                            string dioptersResultString = dioptersRequest.Content.ReadAsStringAsync().Result;
                            dioptersMR = JsonConvert.DeserializeObject<List<Diopters>>(dioptersResultString);

                            //Ordenamos la lista de dioptrias
                            //var orderedDioptersMR = dioptersMR.OrderBy(d => d.DiopterValue == 0 ? 0 : (d.DiopterValue > 0 ? 1 : -1)).ThenBy(d => Math.Abs(d.DiopterValue));

                            ViewBag.Patient = patient;
                            ViewBag.Id = patientID;
                            ViewBag.DioptersMRList = dioptersMR;
                            //ViewBag.DioptersMRList = orderedDioptersMR.ToList();
                            return View("Medical_Record");
                        }

                        else throw new Exception("Error en la conexión al servicio REST");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Dashboard");
                }
            }
        }

        /// <summary>
        /// Solicitud POST para registrar un nuevo paciente.
        /// Envía un objeto parcialmente llenado de tipo Patients y una lista JSON con los posibles padecimientos seleccionados.
        /// </summary>
        /// <param name="newPatient"></param>
        /// <param name="listOfDiseases"></param>
        /// <returns>Void</returns>
        [HttpPost]
        public ActionResult AddUser(Patients newPatient, string listOfDiseases)
        {
            try
            {
                using (HttpClient addUserConnection = new HttpClient())
                {
                    addUserConnection.BaseAddress = new Uri("http://localhost:54263");
                    var addRequest = addUserConnection.PostAsJsonAsync($"api/Values?listOfDiseases={listOfDiseases}", newPatient).Result;
                    if (addRequest.IsSuccessStatusCode)
                    {
                        TempData["success"] = $"Mensaje de control: el cliente {newPatient.FirstName} se registró satisfactoriamente";
                        return RedirectToAction("Dashboard");
                    }

                    else throw new Exception("Error en la conexión al servicio REST");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("AddUser");
            }
        }

        [HttpPost]
        public ActionResult AddMedicalRecord(LeftEyesRx lefteye, RightEyesRx righteye)
        {
            try
            {
                MedicalRecord medicalRecord = new MedicalRecord //"Empaquetamos" los dos objetos en uno para mandarlo en la solicitud POST. (Se desempaquetará en el servicio REST).
                {
                    LeftEye = lefteye,
                    RightEye = righteye
                };

                using (HttpClient addMRConnection = new HttpClient())
                {
                    addMRConnection.BaseAddress = new Uri("http://localhost:54263");
                    var addMRRequest = addMRConnection.PostAsJsonAsync("api/Values/AddMedicalRecord", medicalRecord).Result;
                    if (addMRRequest.IsSuccessStatusCode)
                    {
                        TempData["success"] = $"Mensaje de control: la historia clinica se ha creado con éxito";
                        return RedirectToAction("Dashboard");
                    }

                    else throw new Exception("Error en la conexión al servicio REST");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("AddMedicalRecord");
            }
        }

        [HttpPost]
        public JsonResult PatientEdit(Patients patient)
        {
            try
            {
                using (HttpClient patientEditConnection = new HttpClient())
                {
                    patientEditConnection.BaseAddress = new Uri("http://localhost:54263");
                    var patientEditRequest = patientEditConnection.PostAsJsonAsync("api/Values/PatientEdit", patient).Result;

                    if (patientEditRequest.IsSuccessStatusCode)
                    {
                        return Json(new { status = "ok", id = patient.PatientId }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("Error en la conexión al servicio REST");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DiseasesUpdate(string update, int patientId)
        {
            try
            {
                using (HttpClient diseasesUpdateConnection = new HttpClient())
                {
                    diseasesUpdateConnection.BaseAddress = new Uri("http://localhost:54263");
                    var diseasesUpdateRequest = diseasesUpdateConnection.PostAsJsonAsync("api/Values/DiseasesUpdate", update).Result;

                    if (diseasesUpdateRequest.IsSuccessStatusCode)
                    {
                        return Json(new { status = "ok", id = patientId }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("Error en la conexión al servicio REST");
                    } // AQUI NOS QUEDAMOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOSSSS
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPatientDetails(int patientID)
        {
            if (Session["currentUser"] == null)
            {
                TempData["error"] = "No se enviaron datos de inicio de sesión";
                return RedirectToAction("Login", "Login");
            }

            else
            {
                PatientDetails patientDetails = new PatientDetails();
                try
                {
                    using (HttpClient detailsClient = new HttpClient())
                    {
                        detailsClient.BaseAddress = new Uri("http://localhost:54263");
                        var detailsRequest = detailsClient.GetAsync($"api/Values/GetPatientDetails?patientID={patientID}").Result;

                        if (detailsRequest.IsSuccessStatusCode)
                        {
                            string detailsResultString = detailsRequest.Content.ReadAsStringAsync().Result;
                            patientDetails = JsonConvert.DeserializeObject<PatientDetails>(detailsResultString);

                            return View("PatientDetails", patientDetails);
                        }
                        else throw new Exception("Error en la conexión al servicio REST");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToAction("Dashboard");
                }
            }
        }
    }
}