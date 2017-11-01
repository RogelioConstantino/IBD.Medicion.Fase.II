<%@ Page Title="Catálogo de Gestores de Medición y Comercial" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catGestores.aspx.cs" Inherits="Medicion.catGestores"   EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
         <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <%--<script src="js/jquery.min.js"></script>--%>        
            <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
    
    <ol class="breadcrumb breadcrumb-verde"  style="color:#454545;">
      <li>Catálogos</li>
      <li>Gestores</li>
    </ol>

    <h4  style=" text-align:center; color:#427314"> Catálogo de Gestores de Medición y Comercial</h4>

    <div class="col-xs-12 col-md-12 text-right">
        <p>

              <asp:LinkButton id="btnaddGestor2" name="btnAddZona" OnClick="btnaddGestor2_Click" class="btn btn-warning" runat="server" Text="">
             Agregar <span class='glyphicon glyphicon-plus'></span>
        </asp:LinkButton>
          <%--  <button type="button"  id="btnaddGestor" class="btn btn-warning"  data-toggle="modal" data-placement="top" title="Nuevo" data-target="#NewCentralModal">
                Agregar &nbsp; &nbsp;<span class='glyphicon glyphicon-plus'></span>                
            </button> --%>       
        </p>
    </div> 
    
     <div class="clearfix"></div>
	
    <div class="form-group  col-xs-12 col-md-12">
        <div id="Div2" class="table-responsive" runat="server">
            <table id="mytable" class="table table-striped table-hover">
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
            </table>    
        </div> 
    </div>

    <div class="clearfix"></div>
    
    <div class="col-md-12 text-center">
        <ul class="pagination pull-right" id="myPager"></ul>
    </div> 




    <!--- New Central modal -->
    <div class="modal fade" id="NewCentralModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
          <div class="modal-dialog" role="document">
            <div class="modal-content">                
              <div class="modal-header text-center"  style="    background-color: #A4BA08; color: white !important; text-align: center; ">
                <button type="button"  id="btnCloseNewx" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="Central_add">Agregar un Nuevo Gestor</h4>
              </div>
              <div class="modal-body">                  
                  <div class="form-group col-xs-12 col-md-6">
                        <label class="control-label">Tipo</label>
				        <asp:DropDownList ID="ddl_Tipo_New" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <cc1:CascadingDropDown ID="cdl_Tipo_New" TargetControlID="ddl_Tipo_New" PromptText="Seleciona un Tipo de Gestor"
                        PromptValue="" ServicePath="WebService/wsGestor.asmx" ServiceMethod="getTipo_New" runat="server"
                        Category="Id" LoadingText="Cargando..." />  		                
                   </div>
                   <div class="form-group col-xs-12 col-md-6">
                        <label class="control-label">Rol</label>              
                        <asp:DropDownList ID="ddl_Rol_New" runat="server"  CssClass="form-control">
                        </asp:DropDownList>
                        <cc1:CascadingDropDown ID="cdl_Rol_New" TargetControlID="ddl_Rol_New" PromptText="Selecciona un Rol de Gestor"
                            PromptValue="" ServicePath="WebService/wsGestor.asmx" ServiceMethod="getRol_New" runat="server"
                            Category="Id" ParentControlID="ddl_Tipo_New" LoadingText="Cargando..." />                     
                    </div>
                    <div class="form-group col-xs-12 col-md-6">
                        <label for="txtNumempleado">Número de Empleado</label>
	                    <asp:TextBox type="text" class="form-control" id="txtNumempleado" runat="server" onkeyup="this.value=this.value.replace(/[^\d]/,'')" MaxLength="20"></asp:TextBox>
	                </div>
                  <div class="form-group col-xs-12 col-md-6">
                        <label for="txtIniciales">Iniciales</label>
                        <asp:TextBox runat="server" ID="txtIniciales" CssClass="form-control text-uppercase" MaxLength="6"></asp:TextBox>  
	                </div>                
                    <div class="form-group col-xs-12 col-md-6">
                        <label for="txtName">Nombre</label>
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control text-uppercase" MaxLength="49"></asp:TextBox>  
	                </div>
                    <div class="form-group col-xs-12 col-md-6">
                        <label for="txtAP">Apellido Paterno</label>
	                    <asp:TextBox class="form-control text-uppercase" id="txtAP" runat="server" MaxLength="49" ></asp:TextBox>
	                </div>
                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtAM">Apellido Materno</label>
	                    <asp:TextBox  class="form-control text-uppercase" id="txtAM" runat="server" MaxLength="49"></asp:TextBox>
	                </div>                      
                  </div>
                <div class="clearfix"></div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCloseNew">Cerrar</button>                        
                        <asp:Button ID="btnAddGestor" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAddGestor_Click" />                
                    </div>
                    <div class="clearfix"></div>
                    
                    <div class="alert alert-success" role="alert" id="msgErrNew"  runat="server" ></div>

          </div>
        </div>
    </div>
    <!--- end New modal -->
    

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
                    <button type="button"  id="btnCloseDeletex" class="close" data-dismiss="modal" > <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>

                    </button>
                        <h4 class="modal-title text-center " id="H2">¿Eliminar este Gestor?</h4>
                </div>
                <div class="modal-body" id="delete-modal-body"></div>
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



    <!---- EDIT --->
    <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">    
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header btn-primary">
                    <button type="button"  id="btnCloseEditx" class="close" data-dismiss="modal" id="btnCloseEditarW"> 
                        <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>                        
                    </button>
                    <h4 class="modal-title text-center " id="H1">Editar Gestor</h4>
                </div>
                <div class="modal-body" id="edit-modal-body"></div>

                <div class="clearfix"></div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseEdit" >Cancelar</button>
                    <button type="button" id="Aceptar" class="btn btn-success" data-dismiss="modal" >Aceptar</button>
                    <button type="button" id="Eliminar" class="btn btn-danger" data-dismiss="modal" >Eliminar</button>
                </div>

                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgExito">
                  
                </div>

            </div>
        </div>
    </div>
    <!-- end modal EDIT --->   
    



    <script src="js/Business/pagination.js" type="text/javascript"></script>    
    
     <script type="text/javascript">
         function mostrarModal(nombreModal) {
             $("#" + nombreModal).modal({
                 show: true,
                 //backdrop: 'static',
                 keyboard: false
             });
         }
         $(document).ready(function () {

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

             

         $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 13 });
            

             /// EDIT Division
         $('#Aceptar').click(function () {
             var Id = $("#Id").val();
             var Tipo = $("#Tipo").val();
             var Rol = $("#Rol").val();
                          
             var Nombre = $("#Nombre").val();
             var ApPaterno = $("#ApPaterno").val();
             var ApMaterno = $("#ApMaterno").val();
            
             var Numero = $("#NoEmp").val();
             
             var Iniciales = $("#Iniciales").val();
             //var Iniciales = $("#cve").val();

             //if ((!Rol && Rol.trim().length == 0)) {
             //    $("#msgExito").html("Falta agregar el rol de gestor");
             //    $("#msgExito").addClass("alert alert-danger text-center");
             //    $("#msgExito").show();
             //    return false;
             //}
             //if (/^[a-zA-Z0-9- ]*$/.test(Rol) == false) {
             //    $("#msgExito").html("No puedes agregar characteres especiales en el rol de gestor");
             //    $("#msgExito").addClass("alert alert-danger text-center");
             //    $("#msgExito").show();
             //    return false;
             //}

             if ((!Nombre && Nombre.trim().length == 0)) {
                 swal(
                     '',
                     'Falta agregar el nombe',
                     'warning'
                 );
                 return false;
             }
             if (/^[a-zA-Z0-9- ]*$/.test(Nombre) == false) {
                 swal(
                     '',
                     'No puedes agregar caracteres especiales en el nombre',
                     'warning'
                 );
                 return false;
             }

             if ((!ApPaterno && ApPaterno.trim().length == 0)) {
                 swal(
                     '',
                     'Falta agregar el apellido paterno',
                     'warning'
                 );
                 return false;
             }
             if (/^[a-zA-Z0-9- ]*$/.test(ApPaterno) == false) {
                 swal(
                     '',
                     'No puedes agregar caracteres especiales en el apellido paterno',
                     'warning'
                 );
                 return false;
             }

             //if ((!ApMaterno && ApMaterno.trim().length == 0)) {
             //    swal(
             //        '',
             //        'Falta agregar el apellido materno',
             //        'warning'
             //    );
             //    return false;
             //}
             //if (/^[a-zA-Z0-9- ]*$/.test(ApMaterno) == false) {
             //    swal(
             //        '',
             //        'No puedes agregar caracteres especiales en el apellido materno',
             //        'warning'
             //    );
             //    return false;
             //}

             if ((!Iniciales && Iniciales.trim().length == 0)) {
                 swal(
                     '',
                     'Falta agregar las iniciales',
                     'warning'
                 );
                 return false;
             }
             if (/^[a-zA-Z0-9- ]*$/.test(Iniciales) == false) {
                 swal(
                     '',
                     'No puedes agregar caracteres especiales en las iniciales',
                     'warning'
                 );
                 return false;
             }

             if ((!Numero && Numero.trim().length == 0)) {
                 swal(
                     '',
                     'Falta agregar el número de empleado',
                     'warning'
                 );
                 return false;
             }
             if (/^[a-zA-Z0-9- ]*$/.test(Numero) == false) {
                 swal(
                     '',
                     'No puedes agregar caracteres especiales en número de empleado',
                     'warning'
                 );
                 return false;
             }

             $.ajax({
                 type: "POST",
                 url: "./WebService/wsGestor.asmx/Edit",
                 data: "{ id: '" + Id + "', tipo: '" + Tipo + "', strRol: '" + Rol + "', strNombre : '" + Nombre + "', strApPaterno : '" + ApPaterno + "', strApMaterno : '" + ApMaterno + "', strNumeroEmpleado: '" + Numero + "', strIniciales:'" + Iniciales + "'  }",
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
                             __doPostBack("MiFuncion", "");
                         }, 2000);
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
             // END EDI




         // Begin DELETE
         $("#btnDelete").click(function () {
             var ID = $("#Id_delete").val();  
             var nombre = $("#NombreCompleto").val();
             swal({
                 title: '¿Está seguro de eliminar el gestor?',
                 text: nombre,
                 type: 'warning',
                 showCancelButton: true,
                 confirmButtonColor: '#5C881A',
                 cancelButtonColor: '#d33',
                 confirmButtonText: 'Aceptar'
             }).then(function () {
                 $.ajax({
                     type: "POST",
                     url: "./WebService/wsGestor.asmx/Delete",
                     data: "{strGestor: '" + ID + "' }",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (r) {

                         var sResp = r.d;
                         var aResp = sResp.split('-')

                         if (aResp[0] == '1') {
                             swal(
                                 '',
                                 "El contacto se ha eliminado de la base de datos",
                                 'success'
                             );
                             setTimeout(function () {
                                 __doPostBack("MiFuncion", "");

                             }, 2000);
                         }
                         else {
                             //swal(
                             //    '',
                             //    '<strong>' + sResp[1] + '</strong>',
                             //    'warning'
                             //);
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
             })
             return false;
         });
         // END DELETE


         });

    </script>

    <script src="js/Business/Gestor.js" type="text/javascript"></script>

</asp:Content>
 
