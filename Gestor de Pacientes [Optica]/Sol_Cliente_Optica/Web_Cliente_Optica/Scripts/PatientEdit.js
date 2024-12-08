/**
 * @author Alexandros Estrella de la Mañana <alejandro.gh98@outlook.com>
 * @link https://github.com/alexandro-morningstar GitHub
 * ____________________________________________________________________________________
 *|                                                                                    |
 *|   JS con jQuery y AJAX para habililitar modal de formulario y edición de paciente  |
 *| ___________________________________________________________________________________|
 */

// --------------- Variables globales ---------------
var previousDiseases = []; //Contiene una lista con los IDs de los padecimientos anteriores.
var newDiseases = []; //Contiene una lista con los IDs de los nuevos padecimientos.
// ---- ¿¿selectedDiseases se actualiza desde si mismo en el parámetro de la función fillDropdownForm?? ----

// --------------- Definición y llamada de funciones ---------------
function fillDropdownForm(selectedDiseases, allDiseases) {
    previousDiseases = selectedDiseases; //Este llenado solo ocurre al llenar el Dropdown, por lo que no debería alterarase con el comportamiento de selectedDiseases
    //console.log("selectedDiseases desde fillDropdownForm", selectedDiseases);
    //console.log("allDiseases desde fillDropdownForm", allDiseases);
    /*--------- Llenar DropdownList ---------*/
    let options = `<option disabled selected>Padecimientos</option>`;
    jQuery.each(allDiseases, function (i, element) {
        options += `
            <option class="disease-option" value="${element.DiseaseId}">${element.Disease}</option>
        `;
    });

    jQuery("#diseaseDropdown").empty(); //Limpiamos por si acaso
    jQuery("#diseaseDropdown").html(options); //Insertamos el HTML en el elemento.

    //for (let clave in allDiseases) {
    //    console.log(`Padecimiento: ${allDiseases[clave]["DiseaseId"]}`);
    //    console.log(`Padecimiento: ${allDiseases[clave]["Disease"]}`);
    //};

    /*--------- Generar las opciones actuales ---------*/
    for (let disease in allDiseases) {
        if (selectedDiseases.includes(allDiseases[disease]["DiseaseId"])) {
            jQuery("#temporal-diseases").append(
                `<li data-id="${allDiseases[disease]["DiseaseId"]}"> ${allDiseases[disease]["Disease"]} <button onclick="removeDisease(${allDiseases[disease]["DiseaseId"]})">Eliminar</button></li>`
            );
            $("#diseaseDropdown").find(`option[value="${allDiseases[disease]["DiseaseId"]}"]`).prop('disabled', true); //Deshabilitar opciones ya seleccionadas (traidas del paciente)
        };
    };

    /*--------- DropdownList Diseases Behavior ---------*/
    jQuery("#diseaseDropdown").off("change"); //Eliminar manejadores existentes para evitar duplicados al abrir y cerrar el modal sin registrar cambios.
    jQuery("#diseaseDropdown").on("change", function () {
        let selectedOption = jQuery(this).find("option:selected"); //Buscamos la información/referenciamos de la opción selecionada.
        let diseaseId = selectedOption.val(); //Almacenamos el ID de la opción seleccionada.
        let diseaseName = selectedOption.text(); //Almacenamos el nombre la opción seleccionada.

        if (!selectedDiseases.includes(diseaseId)) { //Si la opción no ha sido seleccionada ya (si no existe en la actual lista de selectedDiseases), la agregamos.

            selectedDiseases.push(Number(diseaseId)); //Agregamos el ID de la opción a la lista.
            newDiseases = [...selectedDiseases]; //Actualiza (reemplaza) el contenido de newDiseases con la versión más nueva de selectedDiseases.

            jQuery("#temporal-diseases").append(
                `<li data-id="${diseaseId}">${diseaseName} <button onclick="removeDisease(${diseaseId})">Eliminar</button></li>`
            );

            selectedOption.prop('disabled', true); //Deshabilitamos la opción seleccionada dentro del dropdown.

            console.log("Padecimientos nuevos", newDiseases);
            console.log("Padecimientos previos", previousDiseases);
        };

        jQuery(this).val('Padecimientos (opcional)'); //Asigna el texto de la opción actual.
    });

    /*--------------- Llamada Onclick para remover un elemento de la lista en pantalla de padecimientos. ---------------*/
    window.removeDisease = function (diseaseId) {
        selectedDiseases = selectedDiseases.filter(id => id !== Number(diseaseId)); //Remueve la enfermedad del array de seleccionados (el array almacena strings, hay que convertir el id primero).
        newDiseases = [...selectedDiseases]; //Actualiza (reemplaza) el contenido de newDiseases con la versión más nueva de selectedDiseases.

        jQuery(`#temporal-diseases li[data-id='${diseaseId}']`).remove(); //Remueve el elemento de la lista en pantalla
        jQuery(`#diseaseDropdown option[value='${diseaseId}']`).prop('disabled', false); //Habilita de nuevo la opción en el DropdownList

        console.log(newDiseases);
    };
};

function cancel() {
    //Limpiar campos
    jQuery("#PatientId").val("");
    jQuery("#FirstName").val("");
    jQuery("#MiddleName").val("");
    jQuery("#LastName").val("");
    jQuery("#ContactNumber").val("");
    jQuery("#Age").val("");
    jQuery("#AnotherDiseases").val("");

    //Ocultar Modal
    jQuery("#patient-modal").modal("toggle");
};

function edit() {
    //Armar el objeto de paciente en formato JSON
    let patient = {
        PatientId: jQuery("#PatientId").val(),
        FirstName: jQuery("#FirstName").val(),
        MiddleName: jQuery("#MiddleName").val(),
        LastName: jQuery("#LastName").val(),
        ContactNumber: jQuery("#ContactNumber").val(),
        Age: jQuery("#Age").val(),
        IdGender: jQuery("input[name='IdGender']:checked").val(),
        AnotherDiseases: jQuery("#AnotherDiseases").val()
    };

    jQuery.ajax({
        type: "post",
        url: "/Home/PatientEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        data: JSON.stringify(patient), //Convertir el objeto patient a string.
        success: function (response) {
            if (response.status = "ok") {
                window.alert("Por fin sirvió esta MIERDA");
                //Limpiar campos
                jQuery("#PatientId").val("");
                jQuery("#FirstName").val("");
                jQuery("#MiddleName").val("");
                jQuery("#LastName").val("");
                jQuery("#ContactNumber").val("");
                jQuery("#Age").val("");
                jQuery("#AnotherDiseases").val("");

                //Ocultar Modal
                jQuery("#patient-modal").modal("toggle");

                //Recargar la página para volver a solicitar los datos del paciente (actualizados)
                window.location.href = `/Home/GetPatientDetails?patientID=${response.id}`;
                
            } else {
                window.alert(response.status);
            };
        },
        error: function (objXMLHttpRequest) {
            window.alert(`Algo salió mal!: ${objXMLHttpRequest}`);
        }
    });
};

function editDiseases() {
    //Armar objeto con dos listas de IDs: "padecimientos anteriores" y "padecimientos nuevos"
    console.log(`Estos son los padecimientos anteriores: ${previousDiseases}`);
    console.log(`Estos son los nuevos padecimientos: ${newDiseases}`);

    // @@@@@@@@@ AQUI NOS QUEDAMOS: AGREGAR METODO PARA REALIZAR EL SUBMIR AHORA SI CON AMBAS LISTAS YA BIEN MANEJADAS @@@@@@@@@@@@@@@@@@@@@@
};

function editDiseasesCancel() {
    //Limpiar campos (Dropdown y elementos li)
    jQuery("#diseaseDropdown").empty();
    jQuery("#temporal-diseases").empty();
    var previousDiseases = [];
    var newDiseases = [];

    //Ocultar Modal
    jQuery("#patient-diseases-modal").modal("toggle");
};

// --------------- Eventos de click ---------------
jQuery("#cancelButton").click( () => {
    cancel();
});

jQuery("#editButton").click( () => {
    edit();
});

jQuery("#editDiseasesCancelButton").click( () => {
    editDiseasesCancel();
});

jQuery("#editDiseasesButton").click( () => {
    editDiseases();

    //console.log(`Estos son los padecimientos anteriores: ${previousDiseases}`);
    //console.log(`Estos son los nuevos padecimientos: ${newDiseases}`);
    /*jQuery("#patient-diseases-modal").modal("toggle");*/
});

// --------------- Onclick directo de elemento html ---------------
function getPatientButton(patientObject) {
    const patient = jQuery(patientObject).data('patient'); //Obtiene el JSON del objeto desde el atributo data-patient. NOTA: Al ser un JSON válido no es necesario usar JSON.parse porque en automatico se toma como un objeto JS.

    //Especificar que los campos estarán habilitados.
    jQuery("#FirstName").prop("disabled", false);
    jQuery("#MiddleName").prop("diabled", false);
    jQuery("#LastName").prop("diabled", false);
    jQuery("#ContactNumber").prop("diabled", false);
    jQuery("#Age").prop("diabled", false);
    jQuery("#AnotherDiseases").prop("diabled", false);

    //Setear información en el campo correspondiente.
    jQuery("#PatientId").val(patient["PatientId"]);
    jQuery("#FirstName").val(patient["FirstName"]);
    jQuery("#MiddleName").val(patient["MiddleName"]);
    jQuery("#LastName").val(patient["LastName"]);
    jQuery("#ContactNumber").val(patient["ContactNumber"]);
    jQuery("#Age").val(patient["Age"]);
    jQuery(`input[name="IdGender"][value="${patient["IdGender"]}"]`).prop("checked", true);
    jQuery("#AnotherDiseases").val(patient["AnotherDiseases"]);

    // Mostrar el modal.
    jQuery("#patient-modal").modal("show");

    console.log(patient);
};

function getCurrentDiseasesButton(diseasesObject) {
    const currentDiseases = jQuery(diseasesObject).data('diseases'); //Obtiene el JSON del objeto Diseases desde el atributo data-diseases. NOTA: Al ser un JSON válido, no es necesario usar JSON.parse porque en automático se toma como un objeto JS.

    // Definir la lista de los IDs de los padecimientos ya asociados al paciente.
    /* 
    var selectedDiseases = [];
    for (const disease of currentDiseases) {
        selectedDiseases.push(disease.DiseaseId);
    };
    */
    var selectedDiseases = currentDiseases.map(d => d.DiseaseId);

    //Limpiar campos (Dropdown y elementos li)
    jQuery("#diseaseDropdown").empty();
    jQuery("#temporal-diseases").empty();
    var previousDiseases = [];
    var newDiseases = [];

    /*--------- Llenar DropdownList ---------*/
    jQuery.ajax({
        type: "Get",
        url: "GetJsonDiseases",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.status == "ok") {
                fillDropdownForm(selectedDiseases, response.alldiseases);
            } else {
                alert(`Ocurrión un error al intentar obtener la información: ${response.status}`);
            };
        },
        error: function (objXMLHttpRequest) {
            window.alert(`Ocurrió un error inesperado: ${objXMLHttpRequest}`);
        }
    });

    // Mostrar el modal.
    jQuery("#patient-diseases-modal").modal("show");

    console.log("Este es el objeto completo de padecimientos: ", currentDiseases);
    console.log("Esta es la lista unicamente con los IDs:  ", selectedDiseases);
};