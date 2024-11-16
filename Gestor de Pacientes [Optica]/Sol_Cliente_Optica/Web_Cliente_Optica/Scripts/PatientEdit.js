/**
 * @author Alexandros Estrella de la Mañana <alejandro.gh98@outlook.com>
 * @link https://github.com/alexandro-morningstar GitHub
 * ____________________________________________________________________________________
 *|                                                                                    |
 *|   JS con jQuery y AJAX para habililitar modal de formulario y edición de paciente  |
 *| ___________________________________________________________________________________|
 */

// --------------- Definición y llamada de funciones ---------------
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
        jQuery: jQuery("#AnotherDiseases").val()
    };

    jQuery.ajax({
        type: "post",
        url: "/Home/EditPatient",
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