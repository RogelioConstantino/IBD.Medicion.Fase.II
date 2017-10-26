$(".btn[data-target='#delete']").click(function () {
    $("#msgdeletealter").hide();
    $("#Aceptar").hide();
    $("#Eliminar").show();
    //console.log('entra delete');
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

            if ((columnHeader == 'IdGrupo') || (columnHeader == 'IdDivision') || (columnHeader == 'IdEstatus')) {
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');
                modalForm.append(formGroup);
            }
            else if ((columnHeader == 'Id') || (columnHeader == 'IdCentral') || (columnHeader == 'FechaCreacion') || (columnHeader == 'Inicio de Operaciones')) {
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<input type="hidden"  name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + '"  />');
                modalForm.append(formGroup);
            }
            else {
                if (columnHeader != '') {
                    formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                    formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" disabled/>');
                }
                modalForm.append(formGroup);
            }
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

            if ((columnHeader == 'Id') || (columnHeader == 'IdCentral')   ) {
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  disabled/>');
            }
            else if ((columnHeader == 'FechaCreacion') || (columnHeader == 'Fecha de Creación') || (columnHeader == 'Fecha de creación') || (columnHeader == 'Carga')) {
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + ' " readonly disabled/>');
            }
            else if (columnHeader == 'Inicio de operaciones') {                
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="date" class="form-control" name="txtFecIniOperE" id="txtFecIniOperE" data-date="" data-date-format="DD MM YYYY" value="' + Value + '"  />');                
            }
            else if (columnHeader == 'Descripción') {
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  maxlength="149"  />');
            }
            else if (columnHeader == 'Código') {
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  maxlength="15"  />');
            }
            else {
                if (columnHeader != '') {
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
    $('form[name="modalForm"]').submit();

});

$(document).on('blur', '#Grupo', function () {
    var txt = $(this).val();
    $('#Grupo').val(txt);
});
$(document).on('blur', '#Division', function () {
    var txt = $(this).val();
    $('#Division').val(txt);
});
$(document).on('blur', '#Estatus', function () {
    var txt = $(this).val();
    $('#Estatus').val(txt);
});
$(document).on('blur', '#CveDivision', function () {
    var txt = $(this).val();
    $('#CveDivision').val(txt);
});

$(document).on('blur', '#txtFecIniOperE', function () {
    var txt = $(this).val();
    $('#txtFecIniOperE').val(txt);
});

$(document).on('blur', '#Descripción', function () {
    var txt = $(this).val();
    $('#Descripción').val(txt);
});



$(document).on('blur', '#Cve', function () {
    var txt = $(this).val();
    $('#Cve').val(txt);
});

$("#Division").removeAttr('disabled');
$("#NewDivisiontxt").removeAttr('disabled');
$("#NewDivisiontxt").val('');

$("#CveDivision").removeAttr('disabled');
$("#NewCveDivisiontxt").removeAttr('disabled');
$("#NewCveDivisiontxt").val('');

$(document).on('blur', '#cmbDivision', function () {
    var txt = $(this).val();
    $('#cmbDivision').val(txt);
});
