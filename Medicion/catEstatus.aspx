<%@ Page Title="Catálogo de Estatus" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catEstatus.aspx.cs" Inherits="Medicion.estatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <script src="js/jquery.min.js"></script>
    <ol class="breadcrumb breadcrumb-verde" style="color:#454545;">
        <li>Catálogos</li>
        <li>Estatus</li>
    </ol>

    <h4  style=" text-align:center; color:#427314"> Catálogo de Estatus</h4>


    <div class="col-xs-12 col-md-12 text-right">
        <button type="button" class="btn btn-warning" onclick="limpiar()"  data-toggle="modal" data-placement="top" title="Nuevo" data-target="#NewEstatusModal">
                Agregar &nbsp; &nbsp;<span class='glyphicon glyphicon-plus'></span>
                
        </button>        
    </div>

    <div class="clearfix"></div>
    <div class="col-xs-12 col-md-12">
        <div id="Div1" class="table-responsive" runat="server">
            <table id="mytable" class="table table-bordred table-striped">
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
            </table>
                    
        </div> <!-- end table responsive -->
        </div>

    <div class="clearfix"></div>
    <div class="col-md-12 text-center">
        <ul class="pagination pull-right" id="myPager"></ul>
    </div> 
    
    <!--- New Group modal -->
    <div class="modal fade" id="NewEstatusModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
          <div class="modal-dialog" role="document">
            <div class="modal-content">              
              <div class="modal-header text-center "  style="    background-color: #A4BA08; color: white !important; text-align: center; ">
                <button type="button" id="btnCloseNewX" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="Grupo_add">Agregar un Nuevo Estatus</h4>
              </div>
              <div class="modal-body" id="new-modal-body">
                <label for="NewCveEstatustxt">Clave</label>
                <input type="text" class="form-control"  id="NewCveEstatustxt"  name="NewCveEstatustxt"  onkeyup="this.value=this.value.replace(/[^\d]/,'')" maxlength="2" />
              </div>
              <div class="modal-body">
                <label for="NewEstatustxt">Descripción</label>
                <input type="text" class="form-control text-uppercase" id="NewStatustxt" name="NewStatustxt" />
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" id="btnCloseNew">Cerrar</button>
                <a href="#" class="btn btn-success" id="btnAddStatus">Agregar</a>     
              </div>
              <div class="clearfix"></div>
              <div class="alert alert-success" role="alert" id="msgErrNew">
            </div>
          </div>
        </div>
    </div>
    <!--- end New modal -->

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
                    <button type="button" class="close"  id="btnCloseEditX" data-dismiss="modal"> <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>
                        
                    </button>
                        <h4 class="modal-title text-center " id="H1">Editar Estatus</h4>

                </div>
                <div class="modal-body"id="edit-modal-body"></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseEdit">Cancelar</button>
                    <button type="button" id="Editar" class="btn btn-success" data-dismiss="modal" >Aceptar</button>
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
                    <button type="button" class="close" id="btnCloseDeleteX" data-dismiss="modal"> <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>

                    </button>
                        <h4 class="modal-title text-center " id="H2">¿Eliminar éste Estatus?</h4>

                </div>
                <div class="modal-body"id="delete-modal-body"></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseDelete">Cancelar</button>
                    <button type="button" id="btnDelete" class="btn btn-danger" data-dismiss="modal" >Eliminar</button>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgdeletealter">
                  
                </div>
            </div>
        </div>
    </div>
    <!--- end delete modal -->  

    <script src="js/Business/estatus.js" type="text/javascript"></script>
    <script src="js/Business/pagination.js" type="text/javascript"></script> 
    <script type="text/javascript">
        function limpiar() {
            document.getElementById("NewCveEstatustxt").value = "";
            document.getElementById("NewStatustxt").value = "";
        }
        $("#msgErrNew").hide();
       // $('[data-toggle="tooltip"]').tooltip();
        $("#NewStatustxt").keydown(function (e) {
            //console.log('entra keypress');
            if (e.keyCode == 13) {
                $('input[name = btnAddStatus]').click();
            }
        });
        $('body').bind('keypress', function (event) {
            if (event.keyCode === 13) {
                $("#btnAddStatus").trigger('click');
            }
        });
        $(document).ready(function () {
            $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 8 });
            //New Status
            
            $("#btnAddStatus").click(function () {
                var New_CveStatus_val = $("#NewCveEstatustxt").val();
                var New_Status_val = $("#NewStatustxt").val();
                
                New_Status_val = $.trim(New_Status_val );
                New_CveStatus_val = $.trim(New_CveStatus_val);

                if ((!New_CveStatus_val && New_CveStatus_val.length == 0)) {
                    swal(
                        '',
                        'Falta agregar la clave',
                        'warning'
                    );
                    return false;
                }

                if ((!New_Status_val && New_Status_val.length == 0)) {
                    swal(
                        '',
                        'Falta agregar la descripción del estatus',
                        'warning'
                    );
                    return false;
                }
                var str = $('#Search').val();
                if (/^[a-zA-Z0-9- ]*$/.test(New_Status_val) == false) {
                    swal(
                        '',
                        'No puedes agregar caracteres especiales',
                        'warning'
                    );
                    return false;
                }


                $.ajax({
                    type: "POST",
                    url: "./WebService/wsStatus.asmx/NewStatus",
                    data: "{ Cve: '" + New_CveStatus_val + "', Status: '" + New_Status_val + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                                             
                        var sResp = r.d;
                        var aResp = sResp.split('-')

                        if (aResp[0] == '1')
                        {
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
            // END NEW Status

            /// EDIT Status
            $('#Editar').click(function () {
                var IdStatus = $("#IdEstatus").val();
                var CveStatus = $("#Clave").val();
                var Status = $("#Estatus").val();
                
                if ((!CveStatus && CveStatus.length == 0)) {
                    swal(
                        '',
                        'Falta agregar la clave',
                        'warning'
                    );
                    return false;
                }
                if ((!Status && Status.length == 0)) {
                    swal(
                        '',
                        'Falta agregar la descripción del estatus',
                        'warning'
                    );
                    return false;
                }
                if (/^[a-zA-Z0-9- ]*$/.test(Status) == false) {
                    swal(
                        '',
                        'No puedes agregar caracteres especiales',
                        'warning'
                    );
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "./WebService/wsStatus.asmx/EditStatus",
                    data: "{ Status: '" + Status + "', IdStatus: " + IdStatus + ", Cve:'" + CveStatus + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                                              
                        var sResp = r.d;
                        var aResp = sResp.split('-')

                        if (aResp[0] == '1')
                        {
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
            // END EDIT Status
            // Begin DELETE
            $("#btnDelete").click(function () {

                var IdStatus_delete = $("#IdEstatus_delete").val();
                var CveStatus = $("#Clave").val();
                if ((!IdStatus_delete && IdStatus_delete.trim().length == 0)) {
                    swal(
                        '',
                        'Falta agregar el Id del estatus',
                        'warning'
                    );
                    return false;
                }
                swal({
                    title: '¿Está seguro de eliminar el estatus?',
                    text: 'Clave: ' + CveStatus,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Aceptar'
                }).then(function () {
                    $.ajax({
                        type: "POST",
                        url: "./WebService/wsStatus.asmx/DeleteStatus",
                        data: "{ intIdStatus_delete: " + IdStatus_delete + "}",
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
                            }, 3000);
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
                })
                return false;
            });
            // END DELETE

            $("#btnCloseNew").click(function () {
                //document.location.reload();
                $("#btnCloseNewX").click();
            });
            $("#btnCloseDelete").click(function () {
                //document.location.reload();
                $("#btnCloseDeleteX").click();
            });
            $("#btnCloseEdit").click(function () {
                //document.location.reload();
                //$("#Aceptar").hide();
                $("#btnCloseEditX").click();
            });

        });
    </script>
</asp:Content>
