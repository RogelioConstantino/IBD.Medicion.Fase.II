$(".btn[data-target='#delete']").click(function () {
    $("#msgdeletealter").hide();
    $("#Aceptar").hide();
    $("#Eliminar").show();
    
    var columnHeadings = $("thead th").map(function () {
        return $(this).text();
    }).get();

    columnHeadings.pop();
    var columnValues = $(this).parent().siblings().map(function () {
        return $(this).text();
    }).get();

    var campos = columnValues.length;

    var modalBody = $('<div id="modalContent"></div>');
    var modalForm = $('<form role="form"></form>');

    $.each(columnHeadings, function (i, columnHeader) {

        if (i <= campos) {

            var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
            var formGroupCentral = $('<div ></div>');

            if ((columnHeader == 'Id')
             || (columnHeader == 'IdGestorRol')
             || (columnHeader == 'IdGestorTipo')
             || (columnHeader == 'cve')
             || (columnHeader == 'Nombre')
             || (columnHeader == 'ApPaterno')
             || (columnHeader == 'ApMaterno')
             || (columnHeader == 'FechaCreacion')
             || (columnHeader == 'Fecha de Creación')
             || (columnHeader == 'NumeroEmpleado')) {
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');

                modalForm.append(formGroup);
            }
            else 
            {
                if (columnHeader != '') 
                {
                    if (columnHeader == 'Nombre Completo')
                    {
                        formGroup = $('<div class="form-group col-xs-12 col-md-12" ></div>');
                        formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="NombreCompleto" value="' + columnValues[i] + '" disabled/>');
                    }
                    else if ((columnHeader == 'Núm de Empleado') ) {
                        formGroup = $('<div class="form-group col-xs-12 col-md-6" ></div>');
                        formGroup.append('<label for="' + columnHeader + '">' + "Núm. de Empleado" + '</label>');
                        formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" disabled/>');
                    }
                    else if ( (columnHeader == 'Iniciales')) {
                        formGroup = $('<div class="form-group col-xs-12 col-md-6" ></div>');
                        formGroup.append('<label for="' + columnHeader + '">' + "Iniciales" + '</label>');
                        formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" disabled/>');
                    }
                    else if (columnHeader == 'Rol') {
                        formGroup = $('<div class="form-group col-xs-12 col-md-6" ></div>');
                        formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" disabled/>');
                    }
                    else {
                        formGroup = $('<div class="form-group col-xs-12 col-md-6" ></div>');
                        formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" disabled/>');
                    }
                }
            }
            modalForm.append(formGroup);            
        }
    });
    modalBody.append(modalForm);
    $('#delete-modal-body').html(modalBody);
});

$(".btn[data-target='#edit']").click(function () {

    $("#msgExito").hide();
    $("#Eliminar").hide();
    $("#Aceptar").show();

    var columnHeadings = $("thead th").map(function () {
        return $(this).text();
    }).get();

    columnHeadings.pop();
    var columnValues = $(this).parent().siblings().map(function () {
        return $(this).text();
    }).get();

    var campos = columnValues.length;

    var modalBody = $('<div id="modalContent"></div>');
    var modalForm = $('<form role="form"></form>');

    $.each(columnHeadings, function (i, columnHeader) {
        var formGroup = $('<div class="form-group col-xs-12 col-md-8"></div>');

        var Value = columnValues[i];
        Value = $.trim(Value);

        if (i <= campos) {

            if ((columnHeader == 'Id')
                || (columnHeader == 'IdGestorTipo')
                || (columnHeader == 'IdGestorRol')
                || (columnHeader == 'FechaCreacion')
                || (columnHeader == 'NumeroEmpleado')
                || (columnHeader == 'Fecha de Creación')
                || (columnHeader == 'Id')
                || (columnHeader == 'cve')
                || (columnHeader == 'Nombre Completo')
                || (columnHeader == 'IdGestor'))
            {
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  disabled/>');
            }           
            else if ((columnHeader == 'Tipo') || (columnHeader == 'Rol') ) {
                var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + ' " readonly disabled/>');
            }
            else if (columnHeader == 'ApPaterno') {
                var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + "Apellido Paterno" + '</label>');
                formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + '"  maxlength="49" />');
            }
            else if (columnHeader == 'ApMaterno') {

                var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + "Apellido Materno" + '</label>');
                formGroup.append('<input type="text"  class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + '"  maxlength="69" />');
                //if (columnValues[i] =="") {
                //    $('#ApMaterno').val = "";
                //}
            }
            else if ((columnHeader == 'Iniciales')) {
                var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + "Iniciales" + '" id="' + "Iniciales" + '" value="' + columnValues[i] + '"maxlength="6" />');
            }
            else if (columnHeader == 'Núm de Empleado') {
                var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + "NoEmp" + '" id="' + "NoEmp" + '" value="' + columnValues[i] + '"   maxlength="20" />');
            }
            else {
                if (columnHeader != '') {
                    var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
                    formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                    formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '" />');
                }
            }
            modalForm.append(formGroup);
        }
    });
    modalBody.append(modalForm);
    $('#edit-modal-body').html(modalBody);
});


$('.modal-footer .btn-primary').click(function () {
    //$('form[name="modalForm"]').submit();

});


$(document).on('blur', '#NoEmp', function () {
    var txt = $(this).val();
    $('#NoEmp').val(txt);
}); 

$(document).on('blur', '#Nombre', function () {
    var txt = $(this).val();
    $('#Nombre').val(txt);
});
$(document).on('blur', '#ApPaterno', function () {
    var txt = $(this).val();
    $('#ApPaterno').val(txt);
});
$(document).on('blur', '#ApMaterno', function () {
    var txt = $(this).val();
    $('#ApMaterno').val(txt);
});

$(document).on('blur', '#NumeroEmpleado', function () {
    var txt = $(this).val();
    $('#NumeroEmpleado').val(txt);
});

$(document).on('blur', '#Iniciales', function () {
    var txt = $(this).val();
    $('#Iniciales').val(txt);
});
