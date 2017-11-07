<%@ Page Title="Catalálogo de Grupos" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catGrupo.aspx.cs" Inherits="Medicion.Weconsultagrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">

    <ol class="breadcrumb breadcrumb-verde" style="color: #454545;">
        <li>Catálogos</li>
        <li>Grupos</li>
    </ol>

    <h4 style="text-align: center; color: #427314">Catálogo de Grupos</h4>
    <div class="clearfix"></div>
    <br />

    <div class="panel panel-success">
        <div class="panel-body">
            <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">Gestor Medición</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cmbGMedicion" runat="server" CssClass="form-control" AutoPostBack="false">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">Gestor Comercial</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cmbGComercial" runat="server" CssClass="form-control">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-xs-12 col-xs-2">
            </div>
            <div class="form-group col-xs-12 col-xs-2">
                <br>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm btn-block" OnClick="btnSearch_Click" Text="Buscar" />
            </div>
        </div>
    </div>
    <div class="col-xs-12 col-md-12 text-right">
        <button type="button" class="btn btn-warning" onclick="limpiar()" data-toggle="modal" data-placement="top" title="Agregar nuevo grupo" data-target="#NewGroupModal">
            Agregar &nbsp; &nbsp;<span class='glyphicon glyphicon-plus'></span>
        </button>
    </div>

    <div class="clearfix"></div>

    <div class="col-xs-12 col-md-12">
        <div class="table-responsive" runat="server">
            <table id="mytable" class="table table-striped table-hover ">
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server">
                    <link href="css/sweetalert2.min.css" rel="stylesheet" />
                    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
                    <script src="js/sweetalert2.js" type="text/javascript"></script>
                    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
                    <script src="js/jquery.min.js"></script>
                </asp:PlaceHolder>
            </table>
        </div>
        <!-- end table responsive -->
    </div>

    <div class="clearfix"></div>

    <div class="col-md-12 text-center">
        <ul class="pagination pull-right" id="myPager"></ul>
    </div>

    <!---- EDIT --->
    <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content"></div>
        </div>
        <div class="modal-dialog">
            <div class="modal-content"></div>
        </div>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button" id="btnCloseEditX" class="close" data-dismiss="modal">
                        <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title text-center " id="H1">Editar Grupo</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group col-xs-12 col-xs-12">
                        <label class="control-label">Gestor Medición</label>
                        <div class="selectContainer">
                            <asp:DropDownList ID="cmbGMed2" runat="server" CssClass="form-control" AutoPostBack="false">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group col-xs-12 col-xs-12">
                        <label class="control-label">Gestor Comercial</label>
                        <div class="selectContainer">
                            <asp:DropDownList ID="cmbGComer2" runat="server" CssClass="form-control">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-body" id="edit_modal-body"></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseEdit">Cancelar</button>
                    <button type="button" id="Aceptar" class="btn btn-success" data-dismiss="modal">Actualizar</button>
                    <button type="button" id="Eliminar" class="btn btn-danger" data-dismiss="modal">Eliminar</button>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgExito">
                </div>
            </div>
        </div>
    </div>
    <!-- end modal EDIT --->

    <!---- delete modal --->
    <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content"></div>
        </div>
        <div class="modal-dialog">
            <div class="modal-content"></div>
        </div>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-danger">
                    <button type="button" id="btnCloseDeletex" class="close" data-dismiss="modal">
                        <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title text-center " id="H2">¿Eliminar éste grupo?</h4>
                </div>
                <div class="modal-body" id="delete_modal-body"></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseDelete">Cancelar</button>
                    <button type="button" id="btnDelete" class="btn btn-danger" data-dismiss="modal">Eliminar</button>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgdeletealter">
                </div>
            </div>
        </div>
    </div>
    <!--- end delete modal -->

    <!--- New Group modal -->
    <div class="modal fade" id="NewGroupModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header text-center" style="background-color: #A4BA08; color: white !important; text-align: center;">
                    <button type="button" id="btnCloseNewx" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="Grupo_add">Agregar Nuevo Grupo</h4>
                </div>
                <div class="modal-body">
                    <label for="NewGrouptxt">Descripción</label>
                    <input type="text" class="form-control text-uppercase" maxlength="149" id="NewGrouptxt" />
                </div>
                <div class="form-group col-xs-12 col-xs-12">
                    <label class="control-label">Gestor Medición</label>
                    <div class="selectContainer">
                        <asp:DropDownList ID="cmbGMed" runat="server" 
                            AutoPostBack="false" CssClass="form-control">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-xs-12 col-xs-12">
                    <label class="control-label">Gestor Comercial</label>
                    <div class="selectContainer">
                        <asp:DropDownList ID="cmbGComercial2" runat="server" CssClass="form-control">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group col-xs-12 col-md-6">
                    <label for="txtFecIniOper">Inicio de Operaciones</label>
                    <input type="date" class="form-control" name="txtFecIniOperNew" id="txtFecIniOperNew" data-date="" data-date-format="DD MM YYYY" value="" />
                    <%--	                <div class="input-group date form_date " id="FecIniOper" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                <input id="txtFecIniOper" runat="server" name="txtFecIniOper" type="text" class="form-control "/>
		                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
		                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
	                </div>	--%>
                </div>
                <div class="clearfix"></div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCloseNew">Cerrar</button>
                    <a href="#" class="btn btn-success" id="btnAddGroup">Agregar</a>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgErrNewGroup">
                </div>
            </div>
        </div>
    </div>

    <!--- end New modal -->
    <script type="text/javascript">
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

                    if ((columnHeader == 'Id') || (columnHeader == 'IdDivision') || (columnHeader == 'IdEstatus') || (columnHeader == 'Carga')) {
                        formGroup = $('<div class="form-group col-xs-12 col-md-8" style="display:none;"></div>');
                        formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '"  disabled/>');
                    }
                    else if ((columnHeader == 'IdGrupo') || (columnHeader == 'IdCentral') || (columnHeader == 'FechaCreacion') || (columnHeader == 'Fecha de creación') || (columnHeader == 'Fecha de Creación')) {
                        formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<input type="text" class="form-control" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + ' " readonly disabled/>');
                    }
                    else if (columnHeader == 'Observaciones') {
                        //formGroup.append('<div class="clearfix"></div><label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<textarea class="form-control inputstl" rows="10" id="observations" name="' + columnValues[i] + '">' + Value + '</textarea>');
                    }
                    else if ((columnHeader == 'Inicio de operaciones') || (columnHeader == 'Inicio de Operaciones')) {
                        //formGroup.append('<div class="clearfix"></div><label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<input type="date" class="form-control" name="txtFecIniOperE" id="txtFecIniOperE" data-date="" data-date-format="DD MM YYYY" value="' + Value + '"  />');
                        //formGroup.append('<div class="input-group date form_date" id="FecIniOperE" data-date="' + Value + '" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">' +
                        //                     '<input  id="txtFecIniOperE" name="" value="' + Value + '"  runat="server" type="text" class="form-control " ></input>' +
                        //                     '<span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>'+
                        //                     '<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>'+
                        //                 '</div>');
                    }
                    else if (columnHeader == 'Descripción') {
                        formGroup.append('<label for="' + columnHeader + '">' + columnHeader + '</label>');
                        formGroup.append('<input type="text" class="form-control text-uppercase" name="' + columnHeader + '" id="' + columnHeader + '" value="' + Value + '" MaxLength="149"/>');
                    }
                    else if (columnHeader == 'Gestor Medición') {
                        var ddlMed = document.getElementById("<%=cmbGMed2.ClientID%>");

                        var TextMed = ddlMed.options[ddlMed.selectedIndex].text;
                        var ValueMed = ddlMed.options[ddlMed.selectedIndex].value;
                        for (var x = 0; x < ddlMed.length - 1; x++) {
                            if (Value == ddlMed.options[x].text)

                                ddlMed.selectedIndex = x;
                        }
                    }
                    else if (columnHeader == 'Gestor Comercial') {
                        var ddlComer = document.getElementById("<%=cmbGComer2.ClientID%>");
                        for (var x = 0; x < ddlComer.length - 1; x++) {
                            if (Value == ddlComer.options[x].text)

                                ddlComer.selectedIndex = x;
                        }
                    }
                    modalForm.append(formGroup);

                }
            });
            modalBody.append(modalForm);

            $('#edit_modal-body').html(modalBody);
        });


        function limpiar() {
            document.getElementById("NewGrouptxt").value = "";
            document.getElementById("txtFecIniOperNew").value = "";
        }
        $("#msgExito").hide();
        $("#msgdeletealter").hide();
        $("#msgErrNewGroup").hide();
        // $('[data-toggle="tooltip"]').tooltip();


        $(document).ready(function () {
            $("#btnCloseNew").click(function () {
                //document.location.reload();
                $("#btnCloseNewx").click();
            });
            $("#btnCloseDeletex").on("click", function (event) {
               // event.prevenDefault();
                //$('#frmnew').trigger("reset");
               // $('#frmnew')[0].reset();
            });
            $("#btnCloseDelete").click(function () {
                //document.location.reload();
                $("#btnCloseDeletex").click();
                $('#frmnew').trigger("reset");
            });
            $("#btnCloseEdit").click(function () {
                //document.location.reload();
                $("#btnCloseEditX").click();
            });
            $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 13 });

            $("#btnAddGroup").click(function () {

                //alert('btnAddGroup');

                //var IdGrupo = $("#IdGrupo").val();
                var New_Group_val = $("#NewGrouptxt").val();
                //var New_GroupIniOper_val = $("#txtFecIniOper").context.fileCreatedDate;
                var New_GroupIniOper_val = $("#txtFecIniOper").val();
                New_GroupIniOper_val = $("#txtFecIniOperNew").val();

                if ((!New_Group_val && New_Group_val.length == 0)) {
                    //$("#msgErrNewGroup").html("Falta agregar la descripción del Grupo");
                    //$("#msgErrNewGroup").addClass("alert alert-danger text-center");
                    //$("#msgErrNewGroup").show();
                    swal(
                        '',
                        'Falta agregar la descripción del grupo',
                        'warning'
                    );
                    return false;
                }
                var str = $('#Search').val();
                //if (/^[a-zA-Z0-9- ]*$/.test(New_Group_val) == false) {
                //    $("#msgErrNewGroup").html("No puedes agregar caracteres especiales en la descripción del grupo");
                //    $("#msgErrNewGroup").addClass("alert alert-danger text-center");
                //    $("#msgErrNewGroup").show();
                //    return false;
                //}

                if ((!New_GroupIniOper_val && New_GroupIniOper_val.length == 0)) {
                    //$("#msgErrNewGroup").html("Falta agregar la fecha de inicio de operaciones del Grupo");
                    //$("#msgErrNewGroup").addClass("alert alert-danger text-center");
                    //$("#msgErrNewGroup").show();
                    swal(
                        '',
                        'Falta agregar la fecha de inicio de operaciones del grupo',
                        'warning'
                    );
                    return false;
                }

                //alert("{ strNewGroup: " + New_Group_val + "}");
                var ddlMed = document.getElementById("<%=cmbGMed.ClientID%>");

                var TextMed = ddlMed.options[ddlMed.selectedIndex].text;
                var ValueMed = ddlMed.options[ddlMed.selectedIndex].value;
                var ddlComer = document.getElementById("<%=cmbGComercial2.ClientID%>");

                var TextComer = ddlComer.options[ddlComer.selectedIndex].text;
                var ValueComer = ddlComer.options[ddlComer.selectedIndex].value;
                console.log(TextMed);
                console.log(TextComer);

                if (TextMed == "--Seleccione un gestor--") {
                    swal(
                        '',
                        'Debe seleccionar un gestor de medición',
                        'warning'
                    );

                    return ;
                }
                if (TextComer == "--Seleccione un gestor--") {
                    swal(
                        '',
                        'Debe seleccionar un gestor comercial',
                        'warning'
                    );
                    return ;
                }
                $.ajax({
                    type: "POST",
                    url: "./WebService/wsGroup.asmx/NewGroup",
                    data: "{ InicioOperaciones: '" + New_GroupIniOper_val + "', Grupo: '" + New_Group_val + "', IdMed: '" + ValueMed + "', IdComer: '" + ValueComer + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        var sResp = r.d;
                        var aResp = sResp.split('-')

                        if (aResp[0] == '1') {
                            //$("#msgErrNewGroup").removeAttr("style");
                            //$("#msgErrNewGroup").html("<strong>" + aResp[1] + "</strong> .");
                            //$("#msgErrNewGroup").addClass("alert alert-success text-center");
                            //$("#msgErrNewGroup").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                            //$("#msgErrNewGroup").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'success'
                            );
                            setTimeout(function () {
                                window.location.reload(1);
                            }, 3000);
                        }
                        else {
                            //$("#msgErrNewGroup").html(aResp[1]);
                            //$("#msgErrNewGroup").addClass("alert alert-danger  text-center");
                            //$("#msgErrNewGroup").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'error'
                            );
                        }
                    },
                    error: function (r) {
                        //$("#msgErrNewGroup").html(r.responseText);
                        //$("#msgErrNewGroup").addClass("alert alert-danger");
                        //$("#msgErrNewGroup").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    },
                    failure: function (r) {
                        //alert(r.responseText);
                        //$("#msgErrNewGroup").html(r.responseText);
                        //$("#msgErrNewGroup").addClass("alert alert-danger");
                        //$("#msgErrNewGroup").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    }
                });
                return false;
            });
            $("#btnDelete").click(function () {

                var IdGrupo_delete = $("#Id_delete").val();
                var Grupo = $("#Descripción").val();
                if ((!IdGrupo_delete && IdGrupo_delete.length == 0)) {
                    //$("#msgdeletealter").html("Falta agregar el ID Grupo");
                    //$("#msgdeletealter").addClass("alert alert-danger text-center");
                    //$("#msgdeletealter").show();
                    swal(
                        '',
                        'Falta agregar el ID Grupo',
                        'warning'
                    );
                    return false;
                }
                swal({
                    title: '¿Está seguro de eliminar el grupo?',
                    text: Grupo,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Aceptar'
                }).then(function () {
                    $.ajax({
                        type: "POST",
                        url: "./WebService/wsGroup.asmx/DeleteGroup",
                        data: "{ IdGrupo_delete: " + IdGrupo_delete + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            swal(
                                '',
                                '<strong>' + r.d + '</strong>',
                                'success'
                            );
                            setTimeout(function () {
                                window.location.reload(1);
                            }, 4000);
                        },
                        error: function (r) {
                            swal(
                                '',
                                '<strong>' + r.responseText + '</strong>',
                                'error'
                            );
                            //$("#msgdeletealter").html(r.responseText);
                            //$("#msgdeletealter").addClass("alert alert-danger");
                            //$("#msgdeletealter").show();

                        },
                        failure: function (r) {
                            //alert(r.responseText);
                            //$("#msgdeletealter").html(r.responseText);
                            //$("#msgdeletealter").addClass("alert alert-danger");
                            //$("#msgdeletealter").show();
                            swal(
                                '',
                                '<strong>' + r.responseText + '</strong>',
                                'error'
                            );
                        }
                    });
                })


                return false;
            });
            $('#Aceptar').click(function () {

                var IdGrupo = $("#Id").val();
                var Grupo = $("#Descripción").val();
                //var New_GroupIniOper_val = $("#txtFecIniOperE").context.fileCreatedDate;
                var New_GroupIniOper_val = $("#txtFecIniOperE").val();


                if ((!IdGrupo && IdGrupo.length == 0)) {
                    //$("#msgExito").html("Falta agregar el ID Grupo");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar el ID Grupo',
                        'warning'
                    );
                    return false;
                }
                if ((!New_GroupIniOper_val && New_GroupIniOper_val.length == 0)) {
                    swal(
                        '',
                        'Falta agregar la fecha de inicio de operaciones del grupo',
                        'warning'
                    );
                    return false;
                }
                if ((!Grupo && Grupo.length == 0)) {
                    //$("#msgExito").html("Falta agregar el Grupo");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar el Grupo',
                        'warning'
                    );
                    return false;
                }
                //if (/^[a-zA-Z0-9- ]*$/.test(Grupo) == false) {                 
                //    $("#msgExito").html("No puedes agregar characteres especiales en la descripción del grupo");
                //    $("#msgExito").addClass("alert alert-danger text-center");
                //    $("#msgExito").show();
                //    return false;
                //}      
                var ddlMed2 = document.getElementById("<%=cmbGMed2.ClientID%>");
                var ValueMed2 = ddlMed2.options[ddlMed2.selectedIndex].value;

                var ddlComer2 = document.getElementById("<%=cmbGComer2.ClientID%>");
               
                var ValueComer2 = ddlComer2.options[ddlComer2.selectedIndex].value;
                console.log(ValueMed2);
                console.log(ValueComer2);

                //if (ValueMed2 = "--Seleccione un gestor--") {
                //    swal(
                //        '',
                //        'Debe seleccionar un gestor de medición',
                //        'warning'
                //    );
                    
                //    return false;
                //} else if (ValueComer2 = "--Seleccione un gestor--") {
                //    swal(
                //        '',
                //        'Debe seleccionar un gestor comercial',
                //        'warning'
                //    );
                //    return false;
                //}
                $.ajax({
                    type: "POST",
                    url: "./WebService/wsGroup.asmx/UpdateGroup",
                    data: "{ InicioOperaciones: '" + New_GroupIniOper_val + "',  Grupo: '" + Grupo + "', IdGrupo: '" + IdGrupo + "', IdMed: '" + ValueMed2 + "', IdComer: '" + ValueComer2 + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {

                        var sResp = r.d;
                        var aResp = sResp.split('-')

                        if (aResp[0] == '1') {
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'success'
                            );
                            setTimeout(function () {
                                window.location.reload(1);
                            }, 4000);
                        }
                        else {
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'error'
                            );
                        }
                    },
                    error: function (r) {
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    },
                    failure: function (r) {
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    }
                });
                return false;
            });
        });


    </script>


    <script src="js/Business/pagination.js" type="text/javascript"></script>
    <script src="js/Business/Grupos.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/bootstrap-datetimepicker.js" charset="UTF-8"></script>
    <script type="text/javascript" src="js/bootstrap-datetimepicker.es.js" charset="UTF-8"></script>
    <link href="Styles/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-select.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.selectpicker').selectpicker({
            style: 'btn-Default',
            size: 5
        });
        $('#SubirArchivo').hide();
        $('#btnSubirArchivo').click(function () {
            $('#SubirArchivo').toggle();
            $("#btnBrowser").focusin();
        });

        $('.form_date').datetimepicker({
            language: 'es',
            pickerPosition: "bottom-left",
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            todayBtn: "linked",
            forceParse: 0,
            format: "dd-mm-yyyy",
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            }
        });
    </script>

</asp:Content>
