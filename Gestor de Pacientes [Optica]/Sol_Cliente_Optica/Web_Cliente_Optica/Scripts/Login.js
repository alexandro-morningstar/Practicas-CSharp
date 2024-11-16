/**
 * @author Alexandros Estrella de la Mañana <alejandro.gh98@outlook.com>
 * @link https://github.com/alexandro-morningstar GitHub
 * _______________________________________________________
 *|                                                       |
 *|   JS para comprobación de datos de inicio de sesión.  |
 *| ______________________________________________________|
 */
//--------------- Declaración de variables globales ---------------
const userRegex = /^[a-zA-Z0-9\-_\.]+$/; //Expresión regular para evaluar la entrada del campo "usuario" (que no contenga nada que no sean letras mayusculas, minusculas (sin tildes ni acentos), numeros o simbolos . _ -).
var jsAlert;
var login;
var usernameTextBox; //Referencia al elemento completo
var passwordTextBox; //Referencia al elemento completo


//--------------- Definición y llamada de funciones ---------------

/**
 * Revisa que no se hayan enviado campos vacíos en el formulario y que el username tenga un formato correcto.
 * @param { Event } event
 */
function checkLoginInfo(event) {
    event.preventDefault();
    const formData = new FormData(event.target);
    let usernameValue = formData.get("username"); //Referencia al valor del campo capturado.
    let passwordValue = formData.get("password"); //Referencia al valor del campo capturado.

    //Revisamos cada campo de manera individual para ser especificos con el campo vacío.
    if (usernameValue != "") { //Revisa que la string no esté vacía (nunca recibe null).
        if (userRegex.test(usernameValue)) { //True == no se encontraron caracteres prohibidos.
            if (passwordValue != "") { //Revisa que la string no esté vacía (nunca recibe null).
                login.submit();
            } else {
                jsAlert.innerHTML = '<span class="alert alert-danger">Los campos marcados con * son obligatorios.</span>';
                usernameTextBox.focus();
            };
        } else {
            jsAlert.innerHTML = '<span class="alert alert-danger">Formato de usuario inválido, revisa tu entrada.</span>';
            usernameTextBox.focus();
        };
    } else {
        jsAlert.innerHTML = '<span class="alert alert-danger">Los campos marcados con * son obligatorios.</span>';
        passwordTextBox.focus();
    };
};

/**
 * Se ejecuta despues de la carga del DOM, captura los elementos HTML que se usarán a lo largo del script y llama la función que revisa los campos del formulario login.
 */
function init() {
    //--------- Captura de elementos HTML ---------
    jsAlert = document.getElementById("JSAlert");
    login = document.getElementById("loginForm");
    usernameTextBox = document.getElementById("username");
    passwordTextBox = document.getElementById("password");

    //--------- Asignación de eventos ---------
    login.addEventListener("submit", checkLoginInfo);
};

//--------------- Carga del DOM ---------------
document.addEventListener("DOMContentLoaded", init);