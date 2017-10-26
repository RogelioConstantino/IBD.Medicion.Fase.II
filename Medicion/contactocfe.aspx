<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="contactocfe.aspx.cs" Inherits="Medicion.contactocfe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <ol class="breadcrumb breadcrumb-verde"  style="color:#454545;">
      <li>Catálogos</li>
      <li>Contactos CFE</li>
    </ol>

    <h4  style=" text-align:center; color:#427314"> Catálogo de Contactos de CFE</h4>

    <div class="panel panel-success">
        <div class="panel-body">
            <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">División</label>
               <%-- <div class="selectContainer">--%>
                    <asp:DropDownList ID="ddl_Divisiones" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <cc1:CascadingDropDown ID="cdl_Divisiones" TargetControlID="ddl_Divisiones" PromptText="Seleciona una División"
                    PromptValue="" ServicePath="WebService/wsDivision.asmx" ServiceMethod="getDivisiones" runat="server"
                    Category="Id" LoadingText="Cargando..." />

                    <asp:DropDownList ID="cmbSearchDivision" runat="server" CssClass="form-control" AutoPostBack="True" onselectedindexchanged="itemSelectedSearch">
                        <asp:ListItem Text="" Value=""></asp:ListItem>                
                    </asp:DropDownList>                              
                <%--</div> --%>       
            </div>
	        <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">Zona</label>
                <div class="selectContainer">                   
                    <asp:DropDownList ID="cmbSearchZone" runat="server" CssClass="form-control" >
                        <asp:ListItem Text="" Value=""></asp:ListItem>                
                    </asp:DropDownList>  
                </div>
            </div>	
            <div class="form-group col-xs-12 col-xs-2">
            </div>	
	        <div class="form-group col-xs-12 col-xs-2" style="margin-top:15px;">
                <p></p>		                    
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Buscar" OnClick="btnSearch_Click" /> 
	        </div>  
        </div>  
    </div>

	<div class="col-xs-12 col-md-12 text-right">
        <button type="button" class="btn btn-warning"  data-toggle="modal" data-placement="top" title="Agregar nueva zona" data-target="#NewDivisionModal">
                Agregar &nbsp; &nbsp;<span class='glyphicon glyphicon-plus'></span>                
        </button>        
    </div>

        <%--<br />--%>
    <div class="clearfix"></div>
    <br />
    <div class="form-group col-xs-12 col-md-12">
        <div class="alert alert-danger" style="display:none" role="alert" id="msgErrNew" runat="server"> </div>
	</div>
  
       
    <div class="clearfix"></div>

    <div class="form-group col-xs-12 col-md-12">
        <div class="alert alert-danger" style="display:none" role="alert" id="msgErrorSearch" runat="server"> </div>
    </div>

    <div class="clearfix"></div>

    <div class="form-group  col-xs-12 col-md-12">
        <div id="Div2" class="table-responsive" runat="server">
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
    <div class="modal fade" id="NewDivisionModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header text-center "  style="    background-color: #A4BA08; color: white !important; text-align: center; ">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="Grupo_add">Agregar una nueva Zona</h4>
              </div>
              <div class="modal-body">    


                    <div class="form-group col-xs-12 col-md-6">
				        <label class="control-label">División</label>
				        <div class="selectContainer">
					        <asp:DropDownList ID="cmbNewDivision" runat="server" CssClass="form-control" AutoPostBack="True" onselectedindexchanged="itemSelected">
						        <asp:ListItem Text="" Value=""></asp:ListItem>                
					        </asp:DropDownList>            
				        </div>
			        </div>
                    <div class="form-group col-xs-12 col-md-6">
                        <label class="control-label">Zona</label>
                        <div class="selectContainer">
                           <asp:DropDownList ID="cmbNewZone" runat="server" CssClass="form-control" >
                                <asp:ListItem Text="" Value=""></asp:ListItem>                
                            </asp:DropDownList>  
                        </div>
                    </div>	

                    <div class="form-group col-xs-12 col-md-4">
		                <label class="control-label">Titulo</label>
		                <div class="selectContainer">
		                    <select class="form-control" id="cmbTitle" name="size" runat="server">
						        <option></option>
		                        <option value="ING">ING.</option>
		                        <option value="LIC">LIC.</option>
		                    </select>
		                </div>
		            </div>
                    <div class="form-group col-xs-12 col-md-8">
	                    <label for="txtName">Nombre</label>
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control text-uppercase"></asp:TextBox>  
	                </div>

                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtFN">Apellido paterno</label>
	                    <asp:TextBox class="form-control text-uppercase" id="txtFN" runat="server" ></asp:TextBox>
	                </div>
                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtLN">Apellido materno</label>
	                    <asp:TextBox  class="form-control text-uppercase" id="txtLN" runat="server"></asp:TextBox>
	                </div>
                  
                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtCharge">Puesto</label>
	                    <asp:TextBox class="form-control text-uppercase" id="txtCharge" runat="server" ></asp:TextBox>
	                </div>
                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtEmail">Correo Electrónico</label>
	                    <asp:TextBox class="form-control text-uppercase" id="txtEmail"  runat="server"></asp:TextBox>
	                </div>

                  
                    <div class="form-group col-xs-12 col-md-4">
	                    <label for="txtWorkTel">Tel. de oficina</label>
	                    <asp:TextBox class="form-control" id="txtWorkTel" runat="server"  onkeyup="this.value=this.value.replace(/[^\d]/,'')"></asp:TextBox>
	                </div>
                    <div class="form-group col-xs-12 col-md-3">
	                    <label for="txtExt">Extensión</label>
	                    <asp:TextBox class="form-control" id="txtExt" runat="server" onkeyup="this.value=this.value.replace(/[^\d]/,'')"></asp:TextBox>
	                </div>

                    <div class="form-group col-xs-12 col-md-5">
	                    <label for="txtCel">Tel. movil</label>
	                    <asp:TextBox type="text" class="form-control" id="txtCel" runat="server" onkeyup="this.value=this.value.replace(/[^\d]/,'')"></asp:TextBox>
	                </div>






              </div>             
              <div class="clearfix"></div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" id="btnCloseNew">Cerrar</button>
                <asp:Button ID="btnAddZone" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnSave_Click" />
              </div>
              <div class="clearfix"></div>
              <div class="alert alert-success" role="alert" id="msgErrNewDivision"></div>
          </div>
        </div>
    </div>
    <!--- end New modal -->
        
    <!---- EDIT --->
    <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header text-center btn-primary">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H1">Editar Contacto CFE</h4>
              </div>
              <div class="modal-body "></div>
              <div class="clearfix"></div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                <button type="button" id="EditContactCFE" class="btn btn-primary" data-dismiss="modal" >Editar</button>
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
                    <button type="button" class="close" data-dismiss="modal"> <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>

                    </button>
                        <h4 class="modal-title text-center " id="H2">¿Eliminar éste Contacto de CFE?</h4>

                </div>
                <div class="modal-body"></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" >Cancelar</button>
                    <button type="button" id="btnDelete" class="btn btn-danger" data-dismiss="modal" >Eliminar</button>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgdeletealter">
                  
                </div>
            </div>
        </div>
    </div>
    <!--- end delete modal -->  

    <asp:HiddenField ID="TabName" runat="server" />
    <script src="js/Business/pagination.js" type="text/javascript"></script>
    
     <script type="text/javascript">
         $(function () {
             var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
             //$('#Tabs a[href="#' + tabName + '"]').tab('show');
             $("#Tabs a").click(function () {
                 $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
             });
         });
         $('[data-toggle="tooltip"]').tooltip();
         $(document).ready(function () {
             $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 8 });
             /// EDIT Division
             $('#EditContactCFE').click(function () {
                 var Id = $("#Id").val();
                 var Division = $("#Division").val();
                 var Zona = $("#Zona").val();

                 var Titulo = $("#Titulo").val();
                 var Nombre = $("#Nombre").val();
                 var ApPaterno = $("#ApPaterno").val();
                 var ApMaterno = $("#ApMaterno").val();

                 var Correo = $("#Correo").val();
                 var Puesto = $("#Puesto").val();
                 
                 var TelTrabajo = $("#Telefono").val();
                 var Ext = $("#Ext").val();
                 var Celular = "00";// $("#Celular").val();
                 
                 if ((!Titulo && Titulo.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar el titulo");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if (/^[a-zA-Z0-9- ]*$/.test(Titulo) == false) {
                     $("#msgExito").html("No puedes agregar characteres especiales en el titulo");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }

                 if ((!Nombre && Nombre.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar el nombe");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if (/^[a-zA-Z0-9- ]*$/.test(Nombre) == false) {
                     $("#msgExito").html("No puedes agregar characteres especiales en el nombre");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }

                 if ((!ApPaterno && ApPaterno.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar el apellido paterno");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if (/^[a-zA-Z0-9- ]*$/.test(ApPaterno) == false) {
                     $("#msgExito").html("No puedes agregar characteres especiales en el apellido paterno");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }

                 if ((!ApMaterno && ApMaterno.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar el apellido Materno");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if (/^[a-zA-Z0-9- ]*$/.test(ApMaterno) == false) {
                     $("#msgExito").html("No puedes agregar characteres especiales en el apellido Materno");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }

                 if ((!Correo && Correo.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar el correo");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 //if (/^[a-zA-Z0-9-@]*$/.test(Correo) == false) {
                 //    $("#msgExito").html("No puedes agregar characteres especiales en el correo");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}

                 if ((!Puesto && Puesto.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar el puesto");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if (/^[a-zA-Z0-9- ]*$/.test(Puesto) == false) {
                     $("#msgExito").html("No puedes agregar characteres especiales en el puesto");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }  
                 
                 if ((!TelTrabajo && TelTrabajo.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar el numero de teléfono de oficina");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if (/^[a-zA-Z0-9- ]*$/.test(TelTrabajo) == false) {
                     $("#msgExito").html("No puedes agregar characteres especiales en el teléfono de oficina");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if ((!Ext && Ext.trim().length == 0)) {
                     $("#msgExito").html("Falta agregar la extensión");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }
                 if (/^[a-zA-Z0-9- ]*$/.test(Ext) == false) {
                     $("#msgExito").html("No puedes agregar characteres especiales en la extensión");
                     $("#msgExito").addClass("alert alert-danger text-center");
                     $("#msgExito").show();
                     return false;
                 }

                 //if ((!Celular && Celular.trim().length == 0)) {
                 //    $("#msgExito").html("Falta agregar el número de celular");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}
                 //if (/^[a-zA-Z0-9- ]*$/.test(Celular) == false) {
                 //    $("#msgExito").html("No puedes agregar characteres especiales en el numero de celular");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}
                 

                 //if ((!Correo && Correo.trim().length == 0)) {
                 //    $("#msgExito").html("Falta agregar el Correo");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}
                 //if ((!Division && Division.trim().length == 0)) {
                 //    $("#msgExito").html("Falta agregar la Division");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}
                 //if ((!Zona && Zona.trim().length == 0)) {
                 //    $("#msgExito").html("Falta agregar la División");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}
                 //if (/^[a-zA-Z0-9- ]*$/.test(Division) == false) {
                 //    $("#msgExito").html("No puedes agregar characteres especiales");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}
                 //if (/^[a-zA-Z0-9- ]*$/.test(Zona) == false) {
                 //    $("#msgExito").html("No puedes agregar characteres especiales");
                 //    $("#msgExito").addClass("alert alert-danger text-center");
                 //    $("#msgExito").show();
                 //    return false;
                 //}

                 $.ajax({
                     type: "POST",
                     url: "/WebService/wsContactCFE.asmx/EditContactCFE",
                     data: "{ strID: '" + Id + "', strTitulo: '" + Titulo + "', strDivision: '" + Division + "', strZona : '" + Zona + "', strCorreo : '" + Correo + "', strNombre : '" + Nombre + "', strApPaterno : '" + ApPaterno + "', strApMaterno : '" + ApMaterno + "', strTelTrabajo : '" + TelTrabajo + "', strExt: '" + Ext + "', strCelular : '" + Celular + "', strPuesto: '" + Puesto + "' }",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (r) {
                         $("#msgExito").removeAttr("style");
                         $("#msgExito").html("<strong>" + r.d + "</strong> .");
                         $("#msgExito").addClass("alert alert-success text-center");
                         $("#msgExito").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                         $("#msgExito").show();
                         setTimeout(function () {
                             window.location.reload(1);
                         }, 2000);
                     },
                     error: function (r) {
                         $("#msgExito").html(r.responseText);
                         $("#msgExito").addClass("alert alert-danger");
                         $("#msgExito").show();
                     },
                     failure: function (r) {
                         //alert(r.responseText);
                         $("#msgExito").html(r.responseText);
                         $("#msgExito").addClass("alert alert-danger");
                         $("#msgExito").show();
                     }
                 });
                 return false;
             });
             // END EDIT Division

             // Begin DELETE
             $("#btnDelete").click(function () {
                 var ID = $("#Id_delete").val();
                 //var Division = $("#Division_delete").val();
                 //var Zona = $("#Zona_delete").val();
                 //var Correo = $("#Correo_delete").val();
                 //alert(Correo);
                 //if (($.trim(Correo) === "")) {
                 //    $("#msgdeletealter").html("Falta agregar el Correo");
                 //    $("#msgdeletealter").addClass("alert alert-danger text-center");
                 //    $("#msgdeletealter").show();
                 //    return false;
                 //}
                 //if (($.trim(Division) === "")) {
                 //    $("#msgdeletealter").html("Falta agregar la Division");
                 //    $("#msgdeletealter").addClass("alert alert-danger text-center");
                 //    $("#msgdeletealter").show();
                 //    return false;
                 //}
                 //if ($.trim(Zona) === "") {
                 //    $("#msgdeletealter").html("Falta agregar la Zona");
                 //    $("#msgdeletealter").addClass("alert alert-danger text-center");
                 //    $("#msgdeletealter").show();
                 //    return false;
                 //}
                 //if (/^[a-zA-Z0-9- ]*$/.test(Division) == false) {
                 //    $("#msgdeletealter").html("No puedes agregar characteres especiales");
                 //    $("#msgdeletealter").addClass("alert alert-danger text-center");
                 //    $("#msgdeletealter").show();
                 //    return false;
                 //}
                 //if (/^[a-zA-Z0-9- ]*$/.test(Zona) == false) {
                 //    $("#msgdeletealter").html("No puedes agregar characteres especiales");
                 //    $("#msgdeletealter").addClass("alert alert-danger text-center");
                 //    $("#msgdeletealter").show();
                 //    return false;
                 //}

                 $.ajax({
                     type: "POST",
                     url: "/WebService/wsContactCFE.asmx/DeleteContactCFE",
                     data: "{strId: '" + ID + "' }",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (r) {
                         $("#msgdeletealter").removeAttr("style");
                         $("#msgdeletealter").html("<strong>" + r.d + "</strong> .");
                         $("#msgdeletealter").addClass("alert alert-success text-center");
                         $("#msgdeletealter").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                         $("#msgdeletealter").show();
                         setTimeout(function () {
                             window.location.reload(1);
                         }, 2000);
                     },
                     error: function (r) {
                         $("#msgdeletealter").html(r.responseText);
                         $("#msgdeletealter").addClass("alert alert-danger");
                         $("#msgdeletealter").show();
                     },
                     failure: function (r) {
                         //alert(r.responseText);
                         $("#msgdeletealter").html(r.responseText);
                         $("#msgdeletealter").addClass("alert alert-danger");
                         $("#msgdeletealter").show();
                     }
                 });
                 return false;
             });
             // END DELETE

         });
    </script>

    <script src="js/Business/ContactCFE.js" type="text/javascript"></script>

</asp:Content>
