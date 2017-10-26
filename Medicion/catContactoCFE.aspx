<%@ Page Title="Catalogo de Contactos de la CFE"  Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catContactoCFE.aspx.cs" Inherits="Medicion.catContactoCFE" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
        <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
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
                        <asp:DropDownList ID="ddl_Divisiones" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <cc1:CascadingDropDown ID="cdl_Divisiones" TargetControlID="ddl_Divisiones" PromptText="Seleciona una División"
                        PromptValue="" ServicePath="WebService/wsDivision.asmx" ServiceMethod="getDivisiones" runat="server"
                        Category="Id" LoadingText="Cargando..." />            
                    </div>
	                <div class="form-group col-xs-12 col-xs-4">
                        <label class="control-label">Zona</label>               
                        <asp:DropDownList ID="ddl_Zonas" runat="server"  CssClass="form-control">
                        </asp:DropDownList>
                        <cc1:CascadingDropDown ID="cdl_Zonas" TargetControlID="ddl_Zonas" PromptText="Selecciona una Zona"
                            PromptValue="" ServicePath="WebService/wsZone.asmx" ServiceMethod="getZonas" runat="server"
                            Category="Id" ParentControlID="ddl_Divisiones" LoadingText="Cargando..." />
                    </div>
                 
                <div class="form-group col-xs-12 col-xs-2" ">
                    </div>

	             <div class="form-group col-xs-12 col-xs-2" style="margin-top:15px;">
                        <p></p>
                        <asp:Button ID="btnSearch"  runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Buscar"    OnClick="btnSearch_Click" /> 
	             </div>  
                    </div>  
            </div>      

    	    <div class="col-xs-12 col-md-12 text-right">
                 <asp:LinkButton id="btnaddContac" name="" OnClick="btnaddContac_Click" class="btn btn-warning" runat="server" Text="">
             Agregar <span class='glyphicon glyphicon-plus'></span>
        </asp:LinkButton>
                <%--<button type="button" id="btnaddContac" class="btn btn-warning" onclick="limpiarText"  data-toggle="modal" data-placement="top" title="Agregar nueva zona" data-target="#NewDivisionModal">
                    Agregar &nbsp; &nbsp;<span class='glyphicon glyphicon-plus'></span>                
                </button>  --%>      
            </div>
    
            <br />
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
                <button type="button"  id="btnCloseNewx" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="Grupo_add">Agregar un Nuevo Contacto</h4>
              </div>
              <div class="modal-body" >    
                  
                    <div class="form-group col-xs-12 col-md-6">
				        <label class="control-label">División</label>
				        <asp:DropDownList ID="ddl_Divisiones_New" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <cc1:CascadingDropDown ID="cdl_Divisiones_New" TargetControlID="ddl_Divisiones_New" PromptText="Seleciona una División"
                        PromptValue="" ServicePath="WebService/wsDivision.asmx" ServiceMethod="getDivisiones_New" runat="server"
                        Category="Id" LoadingText="Cargando..." />            
			        </div>
                    <div class="form-group col-xs-12 col-md-6">
                        <label class="control-label">Zona</label>
                        <asp:DropDownList ID="ddl_Zonas_New" runat="server"  CssClass="form-control">
                        </asp:DropDownList>
                        <cc1:CascadingDropDown ID="cdl_Zonas_New" TargetControlID="ddl_Zonas_New" PromptText="Selecciona una Zona"
                            PromptValue="" ServicePath="WebService/wsZone.asmx" ServiceMethod="getZonas" runat="server"
                            Category="Id" ParentControlID="ddl_Divisiones_New" LoadingText="Cargando..." />                     
                    </div>	

                    <div class="form-group col-xs-12 col-md-4">
		                <label class="control-label">Titulo</label>
		                <div class="selectContainer">		                    
						    <asp:TextBox runat="server" ID="cmbTitle" CssClass="form-control text-uppercase"></asp:TextBox>  
		                </div>
		            </div>
                    <div class="form-group col-xs-12 col-md-8">
	                    <label for="txtName">Nombre</label>
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control text-uppercase" maxlength="49" ></asp:TextBox>  
	                </div>

                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtFN">Apellido Paterno</label>
	                    <asp:TextBox class="form-control text-uppercase" id="txtFN" runat="server" maxlength="49"  ></asp:TextBox>
	                </div>               
                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtLN">Apellido Materno</label>
	                    <asp:TextBox  class="form-control text-uppercase" id="txtLN" runat="server" maxlength="49" ></asp:TextBox>
	                </div>
                  
                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtCharge">Puesto</label>
	                    <asp:TextBox class="form-control text-uppercase" id="txtCharge" runat="server"  maxlength="149" ></asp:TextBox>
	                </div>
                    <div class="form-group col-xs-12 col-md-6">
	                    <label for="txtEmail">Correo Electrónico</label>
                        
                        <asp:TextBox id="txtEmail" runat="server" class="form-control text-uppercase"></asp:TextBox>
                      
	                </div>                  

                    <div class="form-group col-xs-12 col-md-4">
	                    <label for="txtWorkTel">Tel. de Oficina</label>
	                    <asp:TextBox class="form-control" id="txtWorkTel" runat="server"  onkeyup="this.value=this.value.replace(/[^\d]/,'')"  maxlength="15" ></asp:TextBox>
	                </div>
                    <div class="form-group col-xs-12 col-md-3">
	                    <label for="txtExt">Extensión</label>
	                    <asp:TextBox class="form-control" id="txtExt" runat="server" onkeyup="this.value=this.value.replace(/[^\d]/,'')" maxlength="15" ></asp:TextBox>
	                </div>
                    <div class="form-group col-xs-12 col-md-5">
	                    <label for="txtCel">Tel. Movil</label>
	                    <asp:TextBox type="text" class="form-control" id="txtCel" runat="server" onkeyup="this.value=this.value.replace(/[^\d]/,'')"  maxlength="15" ></asp:TextBox>
	                </div>                                  

              </div>             
              <div class="clearfix"></div>
              <div class="modal-footer">
                <button type="button" class="btn btn-default" id="btnCloseNew">Cerrar</button>
                  <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="Button1_Click"/>
               <%-- <asp:Button ID="btnAdd" runat="server"  OnClick="btnAddCOntac_Click1" />      --%>          
              </div>
              <div class="clearfix"></div>
              <div class="alert alert-success" role="alert" id="msgErrNewDivision" runat="server"></div>
          </div>
        </div>
    </div>
    <!--- end New modal -->
      
    <!---- EDIT --->
    <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header text-center btn-primary">
                <button type="button"  id="btnCloseEditx"class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="H1">Editar Contacto CFE</h4>
              </div>
              <div class="modal-body " id="edit-modal-body"></div>
              <div class="clearfix"></div>
              <div class="modal-footer">
                <button type="button"  id="btnCloseEdit" class="btn btn-default" data-dismiss="modal">Cerrar</button>
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
                    <button type="button" id="btnCloseDeletex"  class="close" data-dismiss="modal"> <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>

                    </button>
                        <h4 class="modal-title text-center " id="H2">¿Eliminar éste Contacto de CFE?</h4>

                </div>
                <div class="modal-body" id="delete-modal-body"></div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" id="btnCloseDelete" class="btn btn-default" data-dismiss="modal" >Cancelar</button>
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
            // $("#msgErrNewDivision").hide();
            $("#" + nombreModal).modal({
                show: true,
                // backdrop: 'static',
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


            //$('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 8 });
            /// EDIT Division
            $('#EditContactCFE').click(function () {
                var Id = $("#Id").val();
                var Division = $("#División").val();
                var Zona = $("#Zona").val();

                var Titulo = $("#Titulo").val();
                var Nombre = $("#Nombre").val();
                var ApPaterno = $("#ApPaterno").val();
                var ApMaterno = $("#ApMaterno").val();

                var Correo = $("#Correo").val();
                var Puesto = $("#Puesto").val();
                 
                var TelTrabajo = $("#Telefono").val();
                var Ext = $("#Extension").val();
                var Celular = $("#Celular").val();
                 
                if ((!Titulo && Titulo.length == 0)) {
                    swal(
                        '',
                        'Falta agregar el titulo',
                        'warning'
                    );
                    return false;
                }
                //if (/^[a-zA-Z0-9- ]*$/.test(Titulo) == false) {
                //    swal(
                //        '',
                //        'No puedes agregar caracteres especiales en el titulo',
                //        'warning'
                //    );
                //    return false;
                //}

                if ((!Nombre && Nombre.length == 0)) {
                    swal(
                        '',
                        'Falta agregar el nombe',
                        'warning'
                    );
                    return false;
                }
                if ((!ApPaterno && ApPaterno.length == 0)) {
                    swal(
                        '',
                        'Falta agregar el apellido paterno',
                        'warning'
                    );
                    return false;
                }
                //if (/^[a-zA-Z0-9- ]*$/.test(ApPaterno) == false) {
                //    swal(
                //        '',
                //        'No puedes agregar caracteres especiales en el apellido paterno',
                //        'warning'
                //    );
                //    return false;
                //}

                //if ((!ApMaterno && ApMaterno.length == 0)) {
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

                if ((!Correo && Correo.length == 0)) {
                    swal(
                        '',
                        'Falta agregar el correo',
                        'warning'
                    );
                    return false;
                }

                if ((!Puesto && Puesto.length == 0)) {
                    swal(
                        '',
                        'Falta agregar el puesto',
                        'warning'
                    );
                    return false;
                }
                if (/^[a-zA-Z0-9- ]*$/.test(Puesto) == false) {
                    swal(
                        '',
                        'No puedes agregar caracteres especiales en el puesto',
                        'warning'
                    );
                    return false;
                }  
                 
                if ((!TelTrabajo && TelTrabajo.length == 0)) {
                    swal(
                        '',
                        'Falta agregar el número de teléfono de oficina',
                        'warning'
                    );
                    return false;
                }
                if (/^[a-zA-Z0-9- ]*$/.test(TelTrabajo) == false) {
                    swal(
                        '',
                        'No puedes agregar caracteres especiales en el teléfono de oficina',
                        'warning'
                    );
                    return false;
                }
                if ((!Ext && Ext.length == 0)) {
                    swal(
                        '',
                        'Falta agregar la extensión',
                        'warning'
                    );
                    return false;
                }
                if (/^[a-zA-Z0-9- ]*$/.test(Ext) == false) {
                    swal(
                        '',
                        'No puedes agregar caracteres especiales en la extensión',
                        'warning'
                    );
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
                    url: "./WebService/wsContactCFE.asmx/EditContactCFE",
                    data: "{ strID: '" + Id + "', strTitulo: '" + Titulo + "', strDivision: '" + Division + "', strZona : '" + Zona + "', strCorreo : '" + Correo + "', strNombre : '" + Nombre + "', strApPaterno : '" + ApPaterno + "', strApMaterno : '" + ApMaterno + "', strTelTrabajo : '" + TelTrabajo + "', strExt: '" + Ext + "', strCelular : '" + Celular + "', strPuesto: '" + Puesto + "' }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                                             
                        var sResp = r.d;
                        var aResp = sResp.split('-')
                        if (aResp[0] == '1')
                        {

                            swal({
                                title: '',
                                text: aResp[1] ,
                                type: 'success',
                                showCancelButton: false,
                                confirmButtonColor: '#5C881A',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Aceptar'
                            }).then(function () {
                    
                                })
                            //__doPostBack("MiFuncion", "");
                            setTimeout(function () {
                                __doPostBack("MiFuncion", "");

                            }, 2000);
                        }
                        else {

                            //swal(
                            //    '',
                            //    '<strong>' + aResp[1] + '</strong>',
                            //    'error'
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
                return false;

            });
            // END EDIT Division

            // Begin DELETE
            $("#btnDelete").click(function () {
                var ID = $("#Id_delete").val();
                var Nombre = $("#Nombre_delete").val();
                var ApPaterno = $("#ApPaterno").val();
                var ApMaterno = $("#ApMaterno").val();
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
                swal({
                    title: '¿Está seguro de eliminar el contacto?',
                    text: Nombre,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Aceptar'
                }).then(function () {
                    $.ajax({
                        type: "POST",
                        url: "./WebService/wsContactCFE.asmx/DeleteContactCFE",
                        data: "{strId: '" + ID + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
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


    <script src="js/Business/ContactCFE.js" type="text/javascript"></script>

</asp:Content>

