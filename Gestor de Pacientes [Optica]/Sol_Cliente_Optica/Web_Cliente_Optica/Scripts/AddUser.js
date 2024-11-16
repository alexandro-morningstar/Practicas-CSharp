/**
 * @author Alexandros Estrella de la Mañana <alejandro.gh98@outlook.com>
 * @link https://github.com/alexandro-morningstar GitHub
 * _______________________________________________________________________________________________________________
 *|                                                                                                               |
 *|   JS (jQuery) para comprobación de datos de formulario de registro de pacientes y registro de padecimientos.  |
 *| ______________________________________________________________________________________________________________|
 */

function init() {
    /*--------------- Declaración de variables globales ---------------*/
    var selectedDiseases = [];
    const onlyLettersRegex = /^\s*[a-zA-ZáéíóúÁÉÍÓÚ]*(?:\s+[a-zA-ZáéíóúÁÉÍÓÚ]*)*\s*$/;
    /*Nota: la expresión regular es tan compleja porque permite espacios vacíos al inicio y final de palabras, así como entre ellas,
    en la capa de negocios de la API se trimmea, esto para facilitar la captura de datos y hacer amigable la aplicación.*/
    //const onlyLettersRegex = /^[a-zA-ZáéíóúÁÉÍÓÚ]*$/; //Solo permite letras mayusculas y minusculas (vocales con acento) y string vacias para campos opcionales.

    /*--------------- Funciones llamadas de eventos ---------------*/

    /*--------- Submit Form Paciente ---------*/
    jQuery("#patient-form").on("submit", function (event) {

        event.preventDefault();

        jQuery("#idsArray").val(JSON.stringify(selectedDiseases)); //En un input hidden metemos selectedDiseases como JSON String para que lo capture el controlador.

        //Captura de entradas del formulario, se almacenan solo los campos que debe ser comprobados.
        const formPatientData = new FormData(event.target);
        let firstname = formPatientData.get("FirstName");
        let middlename = formPatientData.get("MiddleName");
        let lastname = formPatientData.get("LastName");
        let contactnumber = formPatientData.get("ContactNumber");
        let age = formPatientData.get("Age");
        let gender = formPatientData.get("IdGender")

        if (firstname != "" && lastname != "" && age != "" && gender != "") { //Comprobamos que campos obligatorios no estén vacíos (strings vacias en el JSON).
            if (onlyLettersRegex.test(firstname) && onlyLettersRegex.test(middlename) && onlyLettersRegex.test(lastname)) { //Comprobamos las entradas correspondientes nombres y apellidos
                if (contactnumber != "") { //Si existe numero de contacto...
                    if (contactnumber.toString().length == 10) { //... comprobamos que su longitud sea de 10 caracteres.
                        event.target.submit(); //Si todo es correcto, realizamos el submit.
                    } else {
                        jQuery("#null-advertisement").append(
                            `<span style="color: red;">El numero de contacto deber ser de 10 digitos consecutivos.<span/>`
                        );
                        return false;
                    };
                } else {
                    event.target.submit(); //Si todo es correcto, realizamos el submit.
                };
                
            } else {
                jQuery("#null-advertisement").append(
                    `<span style="color: red;">Los campos de nombres y apellidos solo aceptan letras mayusculas, minusculas y vocales con acento.<span/>`
                );
                return false;
            };
        } else {
            jQuery("#null-advertisement").append(
                `<span style="color: red;">Los campos marcados con * son obligatorios.<span/>`
            );
            return false;
        }
    });

    /*--------- DropdownList Diseases ---------*/
    jQuery("#diseaseDropdown").on("change", function () { //Evento cuando se selecciona una enfermedad.
        let selectedOption = jQuery(this).find("option:selected"); //Buscamos la información/referenciamos de la opción seleccionada
        let diseaseId = selectedOption.val(); //Almacenamos el ID de la opción seleccionada.
        let diseaseName = selectedOption.text(); //Almacenamos nombre de la opción seleccionada.
        
        if (!selectedDiseases.includes(diseaseId)) { //Si la opción no ha sido seleccionada ya, la agregamos.

            selectedDiseases.push(diseaseId); //Agregamos el ID de la opción al array

            jQuery("#temporal-diseases").append( //Agregar la opción a la lista en pantalla
                `<li data-id="${diseaseId}">${diseaseName} <button onclick="removeDisease(${diseaseId})">Eliminar</button></li>` //EDITAR ESTO PARA PERSONALIZAR Y SABER COMO MANEJARLO
            );
            
            selectedOption.prop('disabled', true); //Deshabilitamos la opción seleccionada dentro del dropdown
        }

        jQuery(this).val('Padecimientos (opcional)'); //Asigna el texto de la opción actual, TRATAR DE QUE REFERENCIE A LA OPCIÓN POR DEFECTO
    });

    /*--------------- Funciones llamadas de onclick ---------------*/
    window.removeDisease = function (diseaseId) { //Función que elimina las enfermedades ya seleccionadas.
        selectedDiseases = selectedDiseases.filter(id => id !== diseaseId.toString()); //Remueve la enfermedad del array de seleccionados (el array almacena strings, hay que comvertir el id primero)
        jQuery(`#temporal-diseases li[data-id='${diseaseId}']`).remove(); //Remueve el elemento de la lista en pantalla
        jQuery(`#diseaseDropdown option[value='${diseaseId}']`).prop('disabled', false); //Habilita de nuevo la opción en el DropdownList
    };
};

/*--------------- Carga del DOM ---------------*/
jQuery(document).ready(function () {
    init();
});