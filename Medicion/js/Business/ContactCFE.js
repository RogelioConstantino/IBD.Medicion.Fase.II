$(".btn[data-target='#delete']").click(function () {
    $("#msgdeletealter").hide();
    $("#Aceptar").hide();
    $("#Eliminar").show();
    //console.log("entra delete");
    var columnHeadings = $("thead th").map(function () {
        return $(this).text();
    }).get();
    columnHeadings.pop();
    var columnValues = $(this).parent().siblings().map(function () {
        return $(this).text();
    }).get();

    var modalBody = $('<div id="modalContent"></div>');
    var modalForm = $('<form role="form"></form>');
    $.each(columnHeadings, function (i, columnHeader) {

        var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');

        if (columnHeader == 'Division') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');
        }
        else if ((columnHeader == 'IdDivision')
            || (columnHeader == 'IdZona')
            || (columnHeader == 'Id')
            || (columnHeader == 'ApPaterno')
            || (columnHeader == 'ApMaterno')
            || (columnHeader == 'Nombre')
            || (columnHeader == 'Titulo')
            || (columnHeader == 'Nombre')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-6" style="display:none;"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');
        }
        else if (columnHeader == 'Zona') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');

        }
        else if (columnHeader == 'Correo') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');

        }
        else {

            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="'+ columnHeader +'" value="' + columnValues[i] + '" disabled/>');
        }
        modalForm.append(formGroup);
    });
    modalBody.append(modalForm);
    $('#delete-modal-body').html(modalBody);
});

$(".btn[data-target='#edit']").click(function () {
    $("#msgExito").hide();
    $("#Eliminar").hide();
    $("#Aceptar").show();
    //console.log("entra edit");
    var columnHeadings = $("thead th").map(function () {
        return $(this).text();
    }).get();
    columnHeadings.pop();
    var columnValues = $(this).parent().siblings().map(function () {
        return $(this).text();
    }).get();

    var modalBody = $('<div id="modalContent"></div>');
    var modalForm = $('<form role="form"></form>');

    $.each(columnHeadings, function (i, columnHeader) {

        var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
        
        if ((columnHeader == 'IdDivision') || (columnHeader == 'IdZona') || (columnHeader == 'Id') || (columnHeader == 'Nombre Completo')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-6" style="display:none;"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + ' "  />');
        }
        else if ((columnHeader == 'Division') || (columnHeader == 'División') ) {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + ' "  disabled/>');
        }
        else if (columnHeader == 'Zona') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + ' "  disabled/>');
        }
        else if (columnHeader == 'Titulo') {
            var formGroup = $('<div class="form-group col-xs-12 col-md-4"></div>');
            formGroup.append('<label for="' + 'Titulo' + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + 'Titulo' + '" id="' + 'Titulo' + '" value="' + columnValues[i].toString().trim() + '"  maxlength="49"  />');

            //formGroup.append('<div class="selectContainer"></div>');
            //formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            //formGroup.append('<select class="form-control" id="Titulo" name="size" runat="server">' +
            //                    ' <option></option>' +
            //                    ' <option value="ING" ' + (columnValues[i] == 'ING'?'Selected':'') + '  >ING.</option>' +
            //                    ' <option value="LIC" ' + (columnValues[i] == 'LIC' ? 'Selected' : '') + ' >LIC.</option>' +
            //                '</select>');
        }
        else if (columnHeader == 'Nombre') {
            var formGroup = $('<div class="form-group col-xs-12 col-md-8"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + '" maxlength="49" />');
        }
        else if (columnHeader == 'Apellido Paterno') {
            var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + "Apellido paterno" + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + 'ApPaterno' + '" id="' + 'ApPaterno' + '" value="' + columnValues[i].toString().trim() + '"  maxlength="49" />');
        }
        else if (columnHeader == 'Apellido Materno') {
            var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + "Apellido materno" + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + 'ApMaterno' + '" id="' + 'ApMaterno' + '" value="' + columnValues[i].toString().trim() + '"  maxlength="49" />');
        }
        else if ((columnHeader == 'Teléfono de Oficina') || (columnHeader == 'Tel. Trabajo')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-4"></div>');
            formGroup.append('<label for="' + "Telefono" + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + "Telefono" + '" id="' + "Telefono" + '" value="' + columnValues[i].toString().trim() + '"  maxlength="15" />');
        }
        else if ((columnHeader == 'Extensión') || (columnHeader == 'Ext')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-4"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + 'Extension' + '" id="' + 'Extension' + '" value="' + columnValues[i].toString().trim() + '" />');
        }
        else if ( (columnHeader == 'Celular')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-4"></div>');
            formGroup.append('<label for="' + "Celular" + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="Celular" id="Celular" value="' + columnValues[i].toString().trim() + '"  maxlength="15" />');
        }
        else if ((columnHeader == 'Correo')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-4"></div>');
            formGroup.append('<label for="' +  columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="email"  class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + '"  maxlength="149" />');
        }
        else if ((columnHeader == 'Correo')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-4"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="email"  class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + '"  maxlength="149" />');
        }
        else if (columnHeader == 'Puesto') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + ' "   maxlength="149" />');
        }
        else {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i].toString().trim() + '" />');
        }
        modalForm.append(formGroup);
    });
    modalBody.append(modalForm);
    $('#edit-modal-body').html(modalBody);
});


$('.modal-footer .btn-primary').click(function () {
    //$('form[name="modalForm"]').submit();

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
$(document).on('blur', '#Correo', function () {
    var txt = $(this).val();
    $('#Correo').val(txt);
});
$(document).on('blur', '#Puesto', function () {
    var txt = $(this).val();
    $('#Puesto').val(txt);
});
$(document).on('blur', '#Telefono', function () {
    var txt = $(this).val();
    $('#Telefono').val(txt);
});
$(document).on('blur', '#Ext', function () {
    var txt = $(this).val();
    $('#Ext').val(txt);
});
$(document).on('blur', '#Celular', function () {
    var txt = $(this).val();
    $('#Celular').val(txt);
});



$("textarea#Observaciones").removeAttr('disabled');
$(document).on('blur', 'textarea#Observaciones', function () {
    var txt = $(this).val();
    $('textarea#Observaciones').val(txt);
});