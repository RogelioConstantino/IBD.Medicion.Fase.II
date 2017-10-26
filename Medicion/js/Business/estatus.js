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

            if ( (columnHeader == 'Cve')  ) {
                formGroup.append('<label for="' + columnHeader + '">' + 'Clave del Estatus' + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');
                modalForm.append(formGroup);
            }
            else if ((columnHeader == 'FechaCreacion') ||(columnHeader == 'IdEstatus') ) {
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
            if ((columnHeader == 'Clave')  ) {
                formGroup.append('<label for="' + columnHeader + '">' + 'Clave' + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + 'Clave' + '" id="' + columnHeader + '" value="' + Value + ' "  maxlength="3" disabled/>');
            } else if ((columnHeader == 'FechaCreacion') || (columnHeader == 'Fecha de Creación') || (columnHeader == 'IdEstatus') ) {
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + ' " readonly disabled/>');
            }
            else {
                if (columnHeader != '') {
                    formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                    formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  maxlength="149" />');
                }
            }
            modalForm.append(formGroup);
        }
    });
    modalBody.append(modalForm);
    $('#edit-modal-body').html(modalBody);
});



$(".btn[data-target='#deletexx']").click(function () {
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

            if (columnHeader == 'IdEstatus') {
                formGroup.append('<label for="' + columnHeader + '">' + 'Clave' + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + ' "  disabled/>');
                modalForm.append(formGroup);
            }
            else if ( (columnHeader == 'Id') ||  (columnHeader == 'FechaCreacion') ) {
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<input type="hidden"  name="' + columnHeader + '" id="' + columnHeader + '_delete" value="' + columnValues[i] + '"  />');
                modalForm.append(formGroup);
            }
            else {
                if ( columnHeader != '') {
                    formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                    formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + columnValues[i] + '" disabled/>');
                }
                modalForm.append(formGroup);
            }
        }
    });
    modalBody.append(modalForm);
    $('.modal-body').html(modalBody);
});


$(".btn[data-target='#editxx']").click(function () {

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

            if ((columnHeader == 'FechaCreacion')) {   //(columnHeader == 'Id') || (columnHeader == 'IdEstatus') 
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  disabled/>');
            }
        else 
                if ( (columnHeader == 'Fecha de creación')) {
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + ' " readonly disabled/>');
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
    $('.modal-body').html(modalBody);
});


$(document).on('blur', '#Cve', function () {
    var txt = $(this).val();
    $('#Cve').val(txt);
});

$(document).on('blur', '#Estatus', function () {
    var txt = $(this).val();
    $('#Estatus').val(txt);
});


$(document).on('blur', '#IdEstatus', function () {
    var txt = $(this).val();
    $('#IdEstatus').val(txt);
});


