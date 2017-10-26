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

    var modalBody = $('<div id="modalContent"></div>');
    var modalForm = $('<form role="form"></form>');
    $.each(columnHeadings, function (i, columnHeader) {

        var formGroup = $('<div class="form-group col-xs-12 col-md-6"></div>');
        if (columnHeader == 'IdGrupo') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');

        }
        else if ((columnHeader == 'IdDivision') || (columnHeader == 'IdZona') || (columnHeader == 'Id')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-6" style="display:none;"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');

        }
        else if (columnHeader == 'IdEstatus') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');

        }
        else if ((columnHeader == 'Division') || (columnHeader == 'División')) {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');

        }
        else if (columnHeader == 'CveZona') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled />');
        }
        else if ((columnHeader == 'Cve') || (columnHeader == 'Clave') ){
            formGroup.append('<label for="' + columnHeader + '">' + "Clave de la zona" + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled />');
        }
        else if (columnHeader == 'Zona') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled />');

        }
        else if (columnHeader == 'Observaciones') {
            formGroup.append('<label  for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<textarea  class="form-control" rows="10" id="' + columnHeader + '_delete" disabled  > ' + columnValues[i] + '</textarea>');

        }
        else {

            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" disabled/>');
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
        if ((columnHeader == 'Id') || (columnHeader == 'IdGrupo') || (columnHeader == 'IdDivision') || (columnHeader == 'IdEstatus')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-6" style="display:none;"></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + ' "  disabled/>');
        }        
        else if ((columnHeader == 'Fecha de Creación') || (columnHeader == 'Fecha de creación') || (columnHeader == 'Division') || (columnHeader == 'División')) {
            var formGroup = $('<div class="form-group col-xs-12 col-md-6" ></div>');
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + ' "  disabled/>');
        }        
        else if ((columnHeader == 'Cve') || (columnHeader == 'Clave')) {
            formGroup.append('<label for="' + columnHeader + '">' + "Clave de la Zona" + '</label>');
            formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" maxlength=3  />');
        }
        else if (columnHeader == 'CveZona') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" maxlength=3  />');
        }
        else if (columnHeader == 'Zona') {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" maxlength=149 />');
        }
        else if (columnHeader == 'Observaciones') {
            formGroup.append('<label  for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<textarea  class="form-control text-uppercase" rows="10" id="' + columnHeader + '"  > ' + columnValues[i] + '</textarea>');
        }
        else {
            formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
            formGroup.append('<input type="text"  class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" />');

        }
        modalForm.append(formGroup);
    });
    modalBody.append(modalForm);    
    $('#edit-modal-body').html(modalBody);
});


$('.modal-footer .btn-primary').click(function () {
    //$('form[name="modalForm"]').submit();

});

$("textarea#Observaciones").removeAttr('disabled');
$(document).on('blur', 'textarea#Observaciones', function () {

    var txt = $(this).val();

    $('textarea#Observaciones').val(txt);
});

$(document).on('blur', '#CveZona', function () {
    var txt = $(this).val();
    $('#CveZona').val(txt);
});

$(document).on('blur', '#Cve', function () {
    var txt = $(this).val();
    $('#Cve').val(txt);
}); 

$(document).on('blur', '#Zona', function () {
    var txt = $(this).val();
    $('#Zona').val(txt);
});