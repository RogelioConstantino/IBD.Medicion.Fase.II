
$(".btn[data-target='#edit']").click(function () {    
    $("#Aceptar").show();
    $('#msgExito').InnerText = "";    
    $("#msgExito").hide();

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

            if ( (columnHeader == 'IdCentral')                
                || (columnHeader == 'FechaCreacion')                
                || (columnHeader == 'Fecha de Creación')
                ) {
                formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  disabled/>');
            }
            else if ((columnHeader == 'Id')) {                
                var textbox0 = document.getElementById('hdId');
                textbox0.value = Value;
            }
            else if ((columnHeader == 'Convenio')) {           
                var textbox1 = document.getElementById('txtConvenio');                
                textbox1.value = Value;
            }
            else if ((columnHeader == 'Descripción')) {                
                var textbox2 = document.getElementById('txtDescripcion');
                textbox2.value = Value;
            }
            else if ((columnHeader == 'Estatus')) {
               //// $('#ddl_Estatus option:selected').text(Value);    
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
    //$('#edit-modal-body').html(modalBody);
});

$(document).on('blur', '#txtConvenio', function () {
    var txt = $(this).val();
    $('#txtConvenio').val(txt);
});

$('.modal-footer .btn-primary').click(function () {
    //$('form[name="modalForm"]').submit();

});