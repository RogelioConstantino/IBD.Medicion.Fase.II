<%@ Page Title="Catalogo de Divisiones" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catDivision.aspx.cs" Inherits="Medicion.division" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
                         <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <script src="js/jquery.min.js"></script>
    <ol class="breadcrumb breadcrumb-verde" " style="color:#454545;">
      <li>Catálogos</li>
      <li>Divisiones </li>
    </ol>

    <h4  style=" text-align:center; color:#427314"> Catálogo de Divisiones</h4>

   <div class="col-xs-12 col-md-12 text-right">
        <button type="button" class="btn btn-warning" onclick="limpiar()"  data-toggle="modal" data-placement="top" title="Agregar nueva dvisión " data-target="#NewDivisionModal">
                Agregar &nbsp; &nbsp;<span class='glyphicon glyphicon-plus'></span>
                
        </button>        
    </div>
    <div class="clearfix"></div>
    <div class="col-xs-12 col-md-12">
        <div id="Div1" class="table-responsive" runat="server">
            <table id="mytable" class="table table-striped table-hover">
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server">

                </asp:PlaceHolder>  
            </table>
                    
        </div> <!-- end table responsive -->
    </div>
        <div class="clearfix"></div>
    <div class="col-md-12 text-center">
        <ul class="pagination pull-right" id="myPager"></ul>
    </div> 
    <!--- New Group modal -->
    <div class="modal fade" id="NewDivisionModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              
              <div class="modal-header text-center" style="    background-color: #A4BA08; color: white !important; text-align: center; ">
                <button type="button" id="btnCloseNewx" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="Grupo_add"  >Agregar una Nueva División</h4>
              </div>
              
              <div class="modal-body">
                <label for="NewCveDivisiontxt">Clave</label>
                <input type="text" class="form-control text-uppercase" id="NewCveDivisiontxt"  maxlength="2"/>
              </div>

              <div class="modal-body">
                <label for="NewDivisiontxt">Descripción Divisional</label>
                <input type="text" class="form-control text-uppercase" id="NewDivisiontxt"  maxlength="149" />
              </div>

              <div class="modal-footer">
                <button type="button" class="btn btn-default" id="btnCloseNew">Cerrar</button>
                <a href="#" class="btn btn-success" id="btnAddGroup">Agregar</a>     
              </div>

              <div class="clearfix"></div>
              <div class="alert alert-success" role="alert" id="msgErrNewDivision">

            </div>
          </div>
        </div>
    </div>
    <!--- end New modal -->

   <!---- EDIT --->
    <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content"></div>
        </div>
        <div class="modal-dialog">
            <div class="modal-content"></div>
        </div>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button"  id="btnCloseEditx" class="close" data-dismiss="modal"> <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>                        
                    </button>
                        <h4 class="modal-title text-center " id="H1">Editar División</h4>
                </div>
                <div class="modal-body" id="edit_modal-body" ></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseEdit">Cancelar</button>
                    <button type="button" id="Editar" class="btn btn-success" data-dismiss="modal" >Aceptar</button>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgExito">  </div>
            </div>
        </div>
    </div>
    <!-- end modal EDIT --->

    <!---- delete modal --->
     <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="myModalLabel3" aria-hidden="true">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-danger">
                    <button type="button" id="btnCloseDeletex"  class="close" data-dismiss="modal"> <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>

                    </button>
                        <h4 class="modal-title text-center " id="H2">¿Eliminar ésta División?</h4>

                </div>
                <div class="modal-body" id="delete_modal-body" ></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseDelete" >Cancelar</button>
                    <button type="button" id="btnDelete" class="btn btn-danger" data-dismiss="modal" >Eliminar</button>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgdeletealter">
                  
                </div>
            </div>
        </div>
    </div>
    <!--- end delete modal -->   
    <script src="js/Business/Divisiones.js" type="text/javascript"></script>
    <script src="js/Business/pagination.js" type="text/javascript"></script>     
   
    <script type="text/javascript">

        function limpiar() {
            document.getElementById("NewCveDivisiontxt").value = "";
            document.getElementById("NewDivisiontxt").value = "";
        }
        $("#msgErrNewDivision").hide();
       // $('[data-toggle="tooltip"]').tooltip();
        $(document).ready(function () {
            $("#btnCloseNew").click(function () {
                // document.location.reload();
                $("#btnCloseNewx").click();
            });
            $("#btnCloseDelete").click(function () {
                //document.location.reload();
                $("#btnCloseDeletex").click();
            });
            $("#btnCloseEdit").click(function () {
                //document.location.reload();
                $("#btnCloseEditx").click();
            });

            $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 13 });
            //New Group
            $("#btnAddGroup").click(function () {

                var New_CveDivision_val = $("#NewCveDivisiontxt").val();
                var New_Division_val = $("#NewDivisiontxt").val();
                
                if ((!New_CveDivision_val && New_CveDivision_val.trim().length == 0)) {
                    swal(
                        '',
                        'Falta agregar la clave de la División',
                        'warning'
                    );
                    return false;
                }
                //if (/^[a-zA-Z0-9- ]*$/.test(New_CveDivision_val) == false) {
                //    $("#msgErrNewDivision").html("No puedes agregar characteres especiales en la clave de la división");
                //    $("#msgErrNewDivision").addClass("alert alert-danger text-center");
                //    $("#msgErrNewDivision").show();
                //    return false;
                //}

                if ((!New_Division_val && New_Division_val.trim().length == 0)) {
                    swal(
                        '',
                        'Falta agregar la Descripción de la División',
                        'warning'
                    );
                    return false;
                }
                //var str = $('#Search').val();
                //if (/^[a-zA-Z0-9- ]*$/.test(New_Division_val) == false) {
                //    $("#msgErrNewDivision").html("No puedes agregar characteres especiales en la descripción de la división.");
                //    $("#msgErrNewDivision").addClass("alert alert-danger text-center");
                //    $("#msgErrNewDivision").show();
                //    return false;
                //}


                $.ajax({
                    type: "POST",
                    url: "./WebService/wsDivision.asmx/NewDivision",
                    data: "{ CveDivision: '" + New_CveDivision_val + "', Division: '" + New_Division_val + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {                        
                        var sResp = r.d;
                        var aResp = sResp.split('-')
                        if (aResp[0] == '1')
                        {
                            //$("#msgErrNewDivision").removeAttr("style");
                            //$("#msgErrNewDivision").html("<strong>" + aResp[1] + "</strong> .");
                            //$("#msgErrNewDivision").addClass("alert alert-success text-center");
                            //$("#msgErrNewDivision").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                            //$("#msgErrNewDivision").show();

                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'success'
                            );
                            setTimeout(function () {
                                window.location.reload(1);
                            }, 3000);
                        }
                        else
                        {
                            //$("#msgErrNewDivision").html(aResp[1]);
                            //$("#msgErrNewDivision").addClass("alert alert-danger  text-center");
                            //$("#msgErrNewDivision").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'error'
                            );
                        }

                    },
                    error: function (r) {
                        //$("#msgErrNewDivision").html(r.responseText);
                        //$("#msgErrNewDivision").addClass("alert alert-danger");
                        //$("#msgErrNewDivision").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    },
                    failure: function (r) {
                        //alert(r.responseText);
                        //$("#msgErrNewDivision").html(r.responseText);
                        //$("#msgErrNewDivision").addClass("alert alert-danger");
                        //$("#msgErrNewDivision").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    }
                });
                return false;
            });
            // END NEW Division

            /// EDIT Division
            $('#Editar').click(function () {

                var IdDivision = $("#Id").val();
                var CveDivision = $("#Clave").val();
                var Division = $("#Descripción").val();

                if ((!IdDivision && IdDivision.length == 0)) {
                    //$("#msgExito").html("Falta agregar el id de la División");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar el ID de la División',
                        'warning'
                    );

                    return false;
                }

                if ((!CveDivision && CveDivision.trim().length == 0)) {
                    //$("#msgExito").html("Falta agregar la clave de la división.");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar la clave de la división.',
                        'warning'
                    );
                    return false;
                }
                //if (/^[a-zA-Z0-9- ]*$/.test(CveDivision) == false) {
                //    $("#msgExito").html("No puedes agregar characteres especiales en la clave de la división");
                //    $("#msgExito").addClass("alert alert-danger text-center");
                //    $("#msgExito").show();
                //    return false;
                //}
                if ((!Division && Division.trim().length == 0)) {
                    //$("#msgExito").html("Falta agregar la descripción de la División");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar la descripción de la División',
                        'warning'
                    );
                    return false;
                }
                //if (/^[a-zA-Z0-9- ]*$/.test(Division) == false) {
                //    $("#msgExito").html("No puedes agregar characteres especiales en la descripción de la División.");
                //    $("#msgExito").addClass("alert alert-danger text-center");
                //    $("#msgExito").show();
                //    return false;
                //}

                $.ajax({
                    type: "POST",
                    url: "./WebService/wsDivision.asmx/EditDivision",
                    data: "{ CveDivision: '" + CveDivision + "', Division: '" + Division + "', IdDivision: " + IdDivision + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                                            
                        var sResp = r.d;
                        var aResp = sResp.split('-')

                        if (aResp[0] == '1')
                        {
                        //$("#msgExito").removeAttr("style");
                        //$("#msgExito").html("<strong>" + aResp[1] + "</strong> .");
                        //$("#msgExito").addClass("alert alert-success text-center");
                        //$("#msgExito").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                        //$("#msgExito").show();
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
                            //$("#msgExito").html(aResp[1]);
                            //$("#msgExito").addClass("alert alert-danger  text-center");
                            //$("#msgExito").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'error'
                            );
                        }
                    },
                    error: function (r) {
                        //$("#msgExito").html(r.responseText);
                        //$("#msgExito").addClass("alert alert-danger");
                        //$("#msgExito").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    },
                    failure: function (r) {
                        //alert(r.responseText);
                        //$("#msgExito").html(r.responseText);
                        //$("#msgExito").addClass("alert alert-danger");
                        //$("#msgExito").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    }
                });
                return false;
            });
            // END EDIT Division
            // Begin DELETE
            $("#btnDelete").click(function () {
                var IdDivision_delete = $("#Id_delete").val();
                var CveDivision = $("#Clave").val();
                if ((!IdDivision_delete && IdDivision_delete.trim().length == 0)) {
                    swal(
                        '',
                        'Falta agregar el ID División',
                        'warning'
                    );

                    return false;
                }
                swal({
                    title: '¿Está seguro de eliminar la división?',
                    text: CveDivision,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Aceptar'
                }).then(function () {
                    $.ajax({
                        type: "POST",
                        url: "./WebService/wsDivision.asmx/DeleteDivision",
                        data: "{ intIdDivision_delete: " + IdDivision_delete + "}",
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
                            //alert(r.responseText);
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
        });
        
        
    </script>  
</asp:Content>
