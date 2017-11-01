<%@ Page Title="Catálogo de Zonas" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catZona.aspx.cs" Inherits="Medicion.zona" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <ol class="breadcrumb breadcrumb-verde" style="color:#454545;">
      <li>Catálogos</li>
      <li>Zonas</li>
    </ol>

    <h4  style=" text-align:center; color:#427314"> Catálogo de Zonas</h4>
    
	<div class="clearfix"></div>
    <br />

    <div class="panel panel-success">
        <div class="panel-body">
            <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">División</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cmbDivision" runat="server" CssClass="form-control" AutoPostBack="True" onselectedindexchanged="itemSelected">
                        <asp:ListItem Text="" Value=""></asp:ListItem>                
                    </asp:DropDownList>            
                </div>        
            </div>
	        <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">Zona</label>
                <div class="selectContainer">
                   <asp:DropDownList ID="cmbZone" runat="server" CssClass="form-control" >
                        <asp:ListItem Text="" Value=""></asp:ListItem>                
                    </asp:DropDownList>  
                </div>
            </div>	
                <div class="form-group col-xs-12 col-xs-2">
                    </div>	
	        <div class="form-group col-xs-12 col-xs-2">
		        <br>		
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Buscar" OnClick="btnSearch_Click" />   
	        </div>  
        </div>  
    </div>

	<div class="col-xs-12 col-md-12 text-right">
     
        <asp:LinkButton id="btnAddZona" name="btnAddZona" OnClick="btnAddZona_Click" class="btn btn-warning" runat="server" Text="">
             Agregar <span class='glyphicon glyphicon-plus'></span>
        </asp:LinkButton>
       <%-- <button type="button"     data-toggle="modal" data-placement="top" title="" data-target="#NewDivisionModal">--%>
                                
       <%-- </button> --%>       
    </div>
    <br />
    <div class="clearfix"></div>
    <br />
    <div class="form-group col-xs-12 col-md-12">
        <div class="alert alert-danger" style="display:none" role="alert" id="msgErrNew" runat="server"> </div>
	</div>

     <div class="clearfix"></div>
    <div class="form-group  col-xs-12 col-md-12">
        <div id="Div1" class="table-responsive" runat="server">
            <table id="mytable" class="table table-bordred table-striped ">
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
            </table>
                    
        </div> <!-- end table responsive -->
        <div class="clearfix"></div>
        <div class="col-md-12 text-center">
            <ul class="pagination pull-right" id="myPager"></ul>
        </div>  
    </div>
    <!--- New Group modal -->
    <div class="modal fade" id="NewDivisionModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header text-center "  style="    background-color: #A4BA08; color: white !important; text-align: center; ">
                <button type="button"  id="btnCloseNewx" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="Grupo_add">Agregar una Nueva Zona</h4>
              </div>
              <div class="modal-body">              
                
                <div class="form-group col-xs-12 col-md-12">
                    <label class="control-label">División</label>
                    <div class="selectContainer">
                        <asp:DropDownList ID="cmbNewDivision" runat="server" CssClass="form-control" AutoPostBack="false">
                            <asp:ListItem Text="" Value=""></asp:ListItem>                
                        </asp:DropDownList>            
                    </div>        
                </div>
                <div class="form-group col-xs-12 col-md-4">
                    <label class="control-label">Clave de la Zona</label>
                    <div class="selectContainer">
                        <asp:TextBox runat="server" ID="txtNewCveZone" CssClass="form-control text-uppercase" MaxLength="3"></asp:TextBox>           
                    </div>        
                </div>
                <div class="form-group col-xs-12 col-md-8">
                    <label class="control-label">Zona</label>
                    <div class="selectContainer">
                        <asp:TextBox runat="server" ID="txtNewZone" CssClass="form-control text-uppercase"></asp:TextBox>           
                    </div>        
                </div>                
                <div class="form-group col-xs-12 col-md-12">
                    <label class="control-label">Observaciones</label>
                  <div class="selectContainer">
                      <asp:TextBox id="txtNewObservations" TextMode="multiline" Columns="50" Rows="5" runat="server" CssClass="form-control  text-uppercase " />
                  </div>
                </div>                                                  
              
              
              </div>
              

              <div class="clearfix"></div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCloseNew">Cerrar</button>
                <asp:Button ID="btnAddZone" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAddZone_Click" />
              </div>
              <div class="clearfix"></div>
              <div class="alert alert-success" role="alert" id="msgErrNewDivision" name="msgErrNewDivision" runat="server"></div>
          </div>
        </div>
    </div>
    <!--- end New modal -->

    <!---- EDIT --->
    <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header text-center btn-primary">
                <button type="button" id="btnCloseEditx" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H1">Editar Zona</h4>
              </div>
              <div class="modal-body" id="edit-modal-body"></div>
              <div class="clearfix"></div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCloseEdit">Cerrar</button>
                <button type="button" id="EditZone" class="btn btn-primary" data-dismiss="modal" >Actualizar</button>
              </div>
              <div class="clearfix"></div>
              <div class="alert alert-success" role="alert" id="msgExito"></div>
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
                    <button type="button" id="btnCloseDeletex" class="close" data-dismiss="modal"> <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>

                    </button>
                        <h4 class="modal-title text-center " id="H2">¿Eliminar ésta Zona?</h4>

                </div>
                <div class="modal-body" id="delete-modal-body"></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCloseDelete" >Cancelar</button>
                    <button type="button" id="btnDelete" class="btn btn-danger" data-dismiss="modal" >Eliminar</button>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgdeletealter">
                  
                </div>
            </div>
        </div>
    </div>
    <!--- end delete modal -->              

    <script src="js/Business/pagination.js" type="text/javascript"></script>              
	<script type="text/javascript">
      
        function mostrarModal(nombreModal) {
            $("#msgErrNewDivision").hide();
            $("#" + nombreModal).modal({
                show: true,
                //backdrop: 'static',
                keyboard: false
            });
        }
        function myFunc() {
            $("#NewDivisionModal").on('hidden.bs.modal', function (e) {
                // Invoke your server side code here.
            });
            $("#NewDivisionModal").modal("hide");
            $("#btnCloseNew").click(function () {
                //document.location.reload();
                $("#btnCloseNewx").click();
            });
        };
        function OcultaModal(nombreModal) {
           // $('#NewDivisionModal').modal('hide');
            var $modal = $('#NewDivisionModal');

            //when hidden
            $modal.on('hidden.bs.modal', function (e) {
                return this.render(); //DOM destroyer
            });

            $modal.modal('hide'); 
        }
	   // $('[data-toggle="tooltip"]').tooltip();
	    $(document).ready(function () {
	        $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 13 });
           
	        /// EDIT Division
	        $('#EditZone').click(function () {
	            var Division = $("#IdDivision").val();
	            var IdZona = $("#Id").val();
	            var CveZona = $("#Clave").val();
	            var Zona = $("#Zona").val();
	            var Observaciones = $("textarea#Observaciones").val();

	            if ((!Division && Division.trim().length == 0)) {
	                //$("#msgExito").html("Falta agregar la Division");
	                //$("#msgExito").addClass("alert alert-danger text-center");
                 //   $("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar la Division',
                        'warning'
                    );
	                return false;
	            }
	            if ((!CveZona && CveZona.length == 0)) {
	                //$("#msgExito").html("Falta agregar la clave de la zona");
	                //$("#msgExito").addClass("alert alert-danger text-center");
                 //   $("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar la clave de la zona',
                        'warning'
                    );
	                return false;
	            }
	            //if (/^[a-zA-Z0-9- ]*$/.test(CveZona) == false) {
	            //    $("#msgErrNewGroup").html("No puedes agregar characteres especiales en la clave de la zona");
	            //    $("#msgErrNewGroup").addClass("alert alert-danger text-center");
	            //    $("#msgErrNewGroup").show();
	            //    return false;
	            //}
	            if ((!Zona && Zona.length == 0)) {
	                //$("#msgExito").html("Falta agregar la descripción de la zona");
	                //$("#msgExito").addClass("alert alert-danger text-center");
                 //   $("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar la descripción de la zona',
                        'warning'
                    );
	                return false;
	            }
	            //if (/^[a-zA-Z0-9- ]*$/.test(Zona) == false) {
	            //    $("#msgErrNewGroup").html("No puedes agregar characteres especiales en al descripción de la zona");
	            //    $("#msgErrNewGroup").addClass("alert alert-danger text-center");
	            //    $("#msgErrNewGroup").show();
	            //    return false;
	            //}

	            $.ajax({
	                type: "POST",
	                url: "./WebService/wsZone.asmx/EditZone",
	                data: "{ strDivision: '" + Division + "', strIdZone: '" + IdZona + "', strCveZone: '" + CveZona + "', strZone: '" + Zona + "', strObservations : '" + Observaciones + "'}",
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
                         //   $("#msgExito").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'success'
                            );
	                        setTimeout(function () {
	                            window.location.reload(1);
	                        }, 2000);
	                    }
	                    else {
	                        //$("#msgExito").html(aResp[1]);
	                        //$("#msgExito").addClass("alert alert-danger  text-center");
                         //   $("#msgExito").show();
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
                     //   $("#msgExito").show();
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
                     //   $("#msgExito").show();
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

	            var Division_delete = $("#IdDivision_delete").val();
	            var Zona_delete = $("#Zona_delete").val();
                var Zona = $("#Clave_delete").val();
	            Zona_delete = $("#Id_delete").val();
	            if ((!Zona_delete && Zona_delete.trim().length == 0)) {
	                //$("#msgdeletealter").html("Falta agregar la Zona");
	                //$("#msgdeletealter").addClass("alert alert-danger text-center");
                 //   $("#msgdeletealter").show();
                    swal(
                        '',
                        'Falta agregar la Zona',
                        'warning'
                    );
	                return false;
	            }

	           // alert(Division_delete);
	            //if ((!Division_delete && Division_delete.trim().length == 0)) {
	            //    $("#msgdeletealter").html("Falta agregar la Division");
	            //    $("#msgdeletealter").addClass("alert alert-danger text-center");
	            //    $("#msgdeletealter").show();
	            //    return false;
	            //}
	            //if ((!Zona_delete && Zona_delete.trim().length == 0)) {
	            //    $("#msgdeletealter").html("Falta agregar la Zona");
	            //    $("#msgdeletealter").addClass("alert alert-danger text-center");
	            //    $("#msgdeletealter").show();
	            //    return false;
	            //}
                swal({
                    title: '¿Está seguro de eliminar la zona?',
                    text: Zona,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Aceptar'
                }).then(function () {
                    $.ajax({
                        type: "POST",
                        url: "./WebService/wsZone.asmx/DeleteZone",
                        data: "{ strDivision: '" + Division_delete + "', strZone: '" + Zona_delete + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {

                            //$("#msgdeletealter").removeAttr("style");
                            //$("#msgdeletealter").html("<strong>" + r.d + "</strong> .");
                            //$("#msgdeletealter").addClass("alert alert-success text-center");
                            //$("#msgdeletealter").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                            //$("#msgdeletealter").show();
                            swal(
                                '',
                                '<strong>' + r.d + '</strong>',
                                'success'
                            );
                            setTimeout(function () {
                                __doPostBack("MiFuncion", "");

                            }, 2000);
                        },
                        error: function (r) {

                            //$("#msgdeletealter").html(r.responseText);
                            //$("#msgdeletealter").addClass("alert alert-danger");
                            //$("#msgdeletealter").show();
                            swal(
                                '',
                                '<strong>' + r.responseText + '</strong>',
                                'error'
                            );
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
                });
	            return false;
            });

	        // END DELETE
            // Add Division
            //$("#btnAddZona").click(function () {
            //    $("#txtNewCveZone").val = "";
            //    swal(
            //        '',
            //        '<strong>' + r.responseText + '</strong>',
            //        'error'
            //    );

            //})
	        $("#btnCloseNew").click(function () {
	            //document.location.reload();
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
	    });
	</script>
    <script src="js/Business/ModalZone.js" type="text/javascript"></script>
</asp:Content>
