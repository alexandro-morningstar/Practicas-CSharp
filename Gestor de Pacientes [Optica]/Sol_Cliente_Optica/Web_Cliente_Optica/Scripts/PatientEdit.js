/**
 * @author Alexandros Estrella de la Mañana <alejandro.gh98@outlook.com>
 * @link https://github.com/alexandro-morningstar GitHub
 * ____________________________________________________________________________________
 *|                                                                                    |
 *|   JS con jQuery y AJAX para habililitar modal de formulario y edición de paciente  |
 *| ___________________________________________________________________________________|
 */

// --------------- Definición y llamada de funciones ---------------
function fillDropdownForm(selectedDiseases, allDiseases) {
    /*--------- Llenar DropdownList ---------*/
    let options = `<option disabled selected>Padecimientos</option>`;
    jQuery.each(allDiseases, function (i, element) {
        options += `
            <option class="disease-option" value="${element.DiseaseId}">${element.Disease}</option>
        `;
    });

    jQuery("#diseaseDropdown").empty(); //Limpiamos por si acaso
    jQuery("#diseaseDropdown").html(options); //Insertamos el HTML en el elemento.

    /*--------- Generar las opciones actuales ---------*/
    for (const selectedDisease of selectedDiseases) {
        jQuery("#temporal-diseases").append(
            `<li data-id="${selectedDisease.DiseaseId}"> VER COMO INSERTAR AQUI EL NOMBRE DEL PADECIMIENTO <button onclick="removeDisease(${selectedDisease.DiseaseId})">Eliminar</button></li>`
        );
    };

    // --------------------------- AQUI NOS QUEDAMOS----------------------------------
    

    /*--------- DropdownList Diseases Behavior ---------*/
    jQuery("#diseaseDropdown").on("change", function () {
        let selectedOption = jQuery(this).find("option:selected"); //Buscamos la información/referenciamos de la opción selecionada.
        let diseaseId = selectedOption.val(); //Almacenamos el ID de la opción seleccionada.
        let diseaseName = selectedOption.text(); //Almacenamos el nombre la opción seleccionada.

        if (!selectedDiseases.includes(diseaseId)) { //Si la opción no ha sido seleccionada ya (si no existe en la actual lista de selectedDiseases), la agregamos.

            selectedDiseases.push(diseaseId); //Agregamos el ID de la opción a la lista.

            jQuery("#temporal-diseases").append(
                `<li data-id="${diseaseId}">${diseaseName} <button onclick="removeDisease(${diseaseId})">Eliminar</button></li>`
            );

            selectedOption.prop('disabled', true); //Deshabilitamos la opción seleccionada dentro del dropdown.
        };

        jQuery(this).val('Padecimientos (opcional)'); //Asigna el texto de la opción actual.
    });

    /*--------------- Llamada Onclick para remover un elemento de la lista en pantalla de padecimientos. ---------------*/
    window.removeDisease = function (diseaseId) {
        selectedDiseases = selectedDiseases.filter(id => id !== diseaseId.toString()); //Remueve la enfermedad del array de seleccionados (el array almacena strings, hay que convertir el id primero).
        jQuery(`#temporal-diseases li[data-id='${diseaseId}']`).remove(); //Remueve el elemento de la lista en pantalla
        jQuery(`#diseaseDropdown option[value='${diseaseId}']`).prop('disabled', false); //Habilita de nuevo la opción en el DropdownList
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

// --------------- Eventos de click ---------------
jQuery("#cancelButton").click( () => {
    cancel();
});

jQuery("#editButton").click(() => {
    edit();
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

    /*--------- DropdownList Diseases ---------*/
    // Especificar que los campos estarán habilitados.
    // Setear información en el campo correspondiente. (Agregar opciones de dropdown?)


    // Mostrar el modal.
    jQuery("#patient-diseases-modal").modal("show");

    console.log("Este es el objeto completo de padecimientos: ", currentDiseases);
    console.log("Esta es la lista unicamente con los IDs:  ", selectedDiseases);
};