<%@ Page Title="Resumen de Cargas" Language="C#" MasterPageFile="~/Reportes.Master" AutoEventWireup="true" CodeBehind="resumengeneral.aspx.cs" Inherits="Medicion.resumengeneral" enableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
        <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    <ol class="breadcrumb breadcrumb-verde" style="color:#454545;">
          <li>Reportes</li>
          <li>Resumen de Cargas</li>
    </ol>

    <h4  style=" text-align:center; color:#427314"> Resumen de Cargas</h4>    
    
    
            <div class="panel panel-success">
                <div class="panel-body">

                    <div class="row">                    
                        <div class="col-sm-3 ">
                            <div class="form-group col-lg-12 col-md-3">
                                <label class="control-label">Gestor de Medición</label>
                                <div class="selectContainer">
                                    <asp:DropDownList ID="cboGestorMedicion" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboGestorMedicion_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>     
                        
                         <div class="col-sm-3 ">
                            <div class="form-group col-lg-12 col-md-3">
                                <label class="control-label">Grupo</label>                        
                                <asp:DropDownList ID="ddl_Grupos" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                           <%--     <cc1:CascadingDropDown ID="cddl_Grupos" TargetControlID="ddl_Grupos" PromptText="Seleccione un Grupo"
                            PromptValue="" ServicePath="WebService/wsGroup.asmx" ServiceMethod="getGrupos" runat="server"
                            Category="Id" LoadingText="Cargando..." /> --%>
                            </div>
                        </div>
                                          
                         <div class="col-sm-3 ">
                             <div class="form-group col-lg-12 col-md-3">
                                <label class="control-label">Central</label>                       
                                <asp:DropDownList ID="ddl_Central" runat="server"  CssClass="form-control">
                                </asp:DropDownList>
                                <cc1:CascadingDropDown ID="cddl_Central" TargetControlID="ddl_Central" PromptText="Seleccione una Central"
                                PromptValue="" ServicePath="WebService/wsCentral.asmx" ServiceMethod="getCentrales" runat="server"
                                Category="Id"  LoadingText="Cargando..." />
                            </div>
                        </div>
                                        
                         <div class="col-sm-3 ">
                            <div class="form-group col-lg-12 col-md-3">
                            <label class="control-label">Estatus de convenios</label>                        
                            <asp:DropDownList ID="ddl_Estaus" runat="server"  CssClass="form-control">
                            </asp:DropDownList>
                            <cc1:CascadingDropDown ID="cddl_Estaus" TargetControlID="ddl_Estaus" PromptText="Seleccione un Estatus"
                                PromptValue="" ServicePath="WebService/wsConvenio.asmx" ServiceMethod="getConvenioEstatus" runat="server"
                                Category="Id"  LoadingText="Cargando..." />
                                    </div>
                            </div>
                                          
                   </div>       
                                  
                    <div class="row">   
                        <div class="form-group col-xs-12 col-md-3 col-lg-offset-2">
                            <div class="checkbox">
                                <label>
                                <input type="checkbox" value="1" id="toggleConvenios" runat="server" />Ocultar Información de Convenios</label>
                            </div>
                        </div>
                        <div class="form-group col-xs-12 col-md-3">
                            <div class="checkbox">
                                <label>
                                <input type="checkbox" value="" id="toggleComunicacion" runat="server"/>Ocultar Información de Comunicación</label>
                            </div>
                        </div>
                        <div class="form-group col-xs-12 col-md-3">
                            <div class="checkbox">
                                <label>
                                <input type="checkbox" value="" id="toggleMedicion" runat="server"/>Ocultar Información de Medición</label>
                            </div>
                        </div>
                    </div>

                     <div class="row">  
                     
	                <div class="col-sm-2 col-lg-offset-8">                       
                        <asp:Button ID="btnSearch"  runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Buscar"   OnClick="btnSearch_Click"  /> 
	                </div> 
	                <div class="col-xs-2 ">   
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-info  btn-sm btn-block" data-toggle="tooltip" data-placement="top"  aria-label="rigth Align" title=" Exportar a Excel " Text="Exportar a Excel" OnClick="btnSave_Click" />
                     </div>  

                    </div>  
              
      </div> 
                  </div> 
    
    
    <div class="clearfix"></div>		    






    <div class="form-group col-xs-12 col-md-3 text-right">    		 
        <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-warning right" data-toggle="tooltip" data-placement="top"  aria-label="rigth Align" title=" Actualizar" Text="Actualizar" OnClick="Button1_Click" />--%>
                <%--<span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span>--%>             

    
    </div>


    <div class="clearfix"></div>

	<div class="col-xs-12 col-md-12 col-lg-12" style="padding:0px">        
        <div id="Div1" class="table-responsive docs-table" runat="server" >
            <table id="mytable" class="table-responsive "
                data-toggle="table" data-show-toggle="true" data-show-columns="true" 
                data-search="false" data-striped="true" data-show-export="false" 
                data-page-list="[10, 25, 50, 100, ALL]">
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
            </table>
          
        </div> <!-- end table responsive -->
     </div>

     <div class="clearfix"></div>

  <%--  <div class="col-md-12 text-center">
        <ul class="pagination pull-right" id="myPager"></ul>
    </div> --%>
	

    <%--<script src="js/Business/pagination.js" type="text/javascript"></script>--%>
	<script type="text/javascript">
	    $(document).ready(function () {
	      
	      //  $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 5 });
	        $('#togglemedidores').click(function () {
	            $('td[id=medidoresrow]').toggle();
	            $('th[id=medidoresrow]').toggle();
	        });
	        //togglecomuni
	        $('#togglecomuni').click(function () {
	            $('td[id=comunirow]').toggle();
	            $('th[id=comunirow]').toggle();
	        });



	    });

	    $(document).ready(function () {

	        // Setup - add a text input to each footer cell
	        $('#mytable tfoot th').each(function (i) {
	            var title = $('#mytable thead th').eq($(this).index()).text();
	            $(this).html('<input type="text" placeholder="Buscar' + title + '" data-index="' + i + '" />');
	        });

	        //var table = $('#mytable').DataTable({
	        //    scrollY: "500px",
	        //    scrollX: true,
	        //    scrollCollapse: true,
	        //    paging: false,
         //       sort: false,
         //       search : false,
                
	        //    fixedColumns:   {
         //           leftColumns: 3
         //       }
                 
	        //});
            $('#mytable').DataTable(
                {
                    scrollY: "500px",
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
                    sort: false,
                    search: false,

                    fixedColumns: {
                        leftColumns: 3
                    },

                    "language": {
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sProcessing": "<div class='preloader-wrapper small active'><div class='spinner-layer spinner-green-only'><div class='circle-clipper left'><div class='circle'></div></div><div class='gap-patch'><div class='circle'></div></div><div class='circle-clipper right'><div class='circle'></div></div></div></div>",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible",
                        "sInfoEmpty": "",
                        "sSearch": "Buscar:",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        }
                    },
                    "paging": false,
                    "ordering": false,
                    "info": true
                });


	        // Filter event handler
	        $(table.table().container()).on('keyup', 'tfoot input', function () {
	            table
                    .column($(this).data('index'))
                    .search(this.value)
                    .draw();

            });
            
	    });
	   
</script>

</asp:Content>
