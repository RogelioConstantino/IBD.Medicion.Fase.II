<%@ Page Title="Consulta de Puntos de Carga" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="consultascargas.aspx.cs" Inherits="Medicion.consultascargas" EnableEventValidation="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    
    <ol class="breadcrumb breadcrumb-verde" style="color: #454545;   ">
	  <li>Gestión de Cargas</li>
	  <li>Consultas</li>
	</ol>
    		
    <h4  style=" text-align:center; color:#427314"> Consulta de Puntos de Carga</h4>

	<div class="clearfix"></div>
    

    <div class="panel panel-success">
        <div class="panel-body">
            
                
           <%-- <!-- sel. central -->
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">Central</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cmbCentral" runat="server" CssClass="form-control"  AutoPostBack="false">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>--%>
            </div>

                        <!-- sel. Gestor medicion -->
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">Gestor de Medición</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cboGestorMedicion" runat="server" CssClass="form-control" AutoPostBack="false">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
             

            <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">Grupo</label>                
                    <asp:DropDownList ID="cmbGroup" runat="server" CssClass="form-control" AutoPostBack="false"  >
                        <asp:ListItem Text="" Value=""></asp:ListItem>                
                    </asp:DropDownList>                    
            </div>
	       <%-- <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">Punto de Carga</label>                
                            <asp:DropDownList ID="ddl" CssClass="form-control" runat="server">
                            </asp:DropDownList>                    
	        </div>--%>
            <div class="form-group col-xs-12 col-xs-10">
		         <br/>
            </div>
	        <div class="form-group col-xs-12 col-xs-2">
		         <br/>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Buscar" OnClick="btnSearch_Click" />   
	        </div> 
        </div>
   </div>  
    
   	
    <div class="clearfix"></div>
    <div class="form-group col-xs-12 col-md-12">
        <div class="alert alert-danger" style="display:none" role="alert" id="msgErrNew" runat="server"> </div>
	</div>

    <div class="clearfix"></div>
	  
    <div class="form-group  col-xs-12 col-md-12">
        <div id="Div1" class="table-responsive" runat="server">
            <table id="mytable" class="table table-striped table-hover ">            
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
            </table>
        </div>        
        <div class="clearfix"></div>
        <div class="col-md-12 text-center">
            <ul class="pagination pull-right" id="myPager"></ul>
        </div>  
    </div>
            
			<!-- Large modal -->


			<!-- Large modal -->
			<div  id="winModal_Contacto" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
			  <div class="modal-dialog modal-lg" role="document">
			    <div class="modal-content">
					<div class="modal-header text-center"  style="    background-color: #A4BA08; color: white !important; text-align: center; ">
				        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				        <h4 class="modal-title text-center" id="myModalLabel">CONTACTOS CFE</h4>
				    </div>
                                        
                    <div class="modal-body">

			      	    <div class="table-responsive">
						    <table class="table table-hover table-striped ">
							    <thead>
	                                <tr class="filters">
	                                    <th class="text-center">Nombre	</th>
	                                    <th class="text-center">Telefono	</th>
	                                    <th class="text-center">Extencion	</th>
	                                    <th class="text-center">Celular	</th>
	                                    <th class="text-center">Correo Electrónico</th>                                        
	                                </tr>
	                            </thead>
							    <tbody>
								    <tr>
									    <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
								    </tr>
							    </tbody>
						    </table>
					    </div>

                    </div>

			    </div>
			  </div>
			</div>
      
   
     <script src="js/Business/pagination.js" type="text/javascript"></script>     

	<script type="text/javascript">
	    $(document).ready(function () {
	        $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 10 });
	        ////$('.filterable .btn-filter').click(function () {
	        ////    var $panel = $(this).parents('.filterable'),
            ////    $filters = $panel.find('.filters input'),
            ////    $tbody = $panel.find('.table tbody');
	        ////    if ($filters.prop('disabled') == true) {
	        ////        $filters.prop('disabled', false);
	        ////        $filters.first().focus();
	        ////    } else {
	        ////        $filters.val('').prop('disabled', true);
	        ////        $tbody.find('.no-result').remove();
	        ////        $tbody.find('tr').show();
	        ////    }
	        ////});

	        ////$('.filterable .filters input').keyup(function (e) {
	        ////    /* Ignore tab key */
	        ////    var code = e.keyCode || e.which;
	        ////    if (code == '9') return;
	        ////    /* Useful DOM data and selectors */
	        ////    var $input = $(this),
            ////    inputContent = $input.val().toLowerCase(),
            ////    $panel = $input.parents('.filterable'),
            ////    column = $panel.find('.filters th').index($input.parents('th')),
            ////    $table = $panel.find('.table'),
            ////    $rows = $table.find('tbody tr');
	        ////    /* Dirtiest filter function ever ;) */
	        ////    var $filteredRows = $rows.filter(function () {
	        ////        var value = $(this).find('td').eq(column).text().toLowerCase();
	        ////        return value.indexOf(inputContent) === -1;
	        ////    });
	        ////    /* Clean previous no-result if exist */
	        ////    $table.find('tbody .no-result').remove();
	        ////    /* Show all rows, hide filtered ones (never do that outside of a demo ! xD) */
	        ////    $rows.show();
	        ////    $filteredRows.hide();
	        ////    /* Prepend no-result row if all rows are filtered */
	        ////    if ($filteredRows.length === $rows.length) {
	        ////        $table.find('tbody').prepend($('<tr class="no-result text-center"><td colspan="' + $table.find('.filters th').length + '">No hay resultados </td></tr>'));
	        ////    }
	        ////});

	      
	        ////$('#ddl').on('focus', selectAllOnFocus);
	    });


	    $(".btn[data-target='.bs-example-modal-lg']").click(function () {

	        var columnHeadings = $("thead th").map(function () {
	            return $(this).text();
	        }).get();

	        columnHeadings.pop();
	        var columnValues = $(this).parent().siblings().map(function () {
	            return $(this).text();
	        }).get();
           
	        $.ajax({
	            type: "POST",
	            url: "/WebService/wsContactCFE.asmx/getContactCFE_byRUP",
	            data: "{ strRPU: '" + columnValues[4] + "'}",
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            success: function (r) {

	                $('.modal-body').html(r.d);

	            },
	            error: function (r) {

	                $('.modal-body').html('<div class="table-responsive"><table class="table table-hover table-striped "><thead><tr class="filters"><th class="text-center">Nombre</th><th class="text-center">Teléfono</th><th class="text-center">Extención</th><th class="text-center">Celular</th><th class="text-center">Correo Electrónico</th></tr></thead><tbody><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table></div>');

	                //alert('ws error: ' + r.d);
	            },
	            failure: function (r) {
	                //alert('ws failure: ' + r.d);
	                $('.modal-body').html('<div class="table-responsive"><table class="table table-hover table-striped "><thead><tr class="filters"><th class="text-center">Nombre</th><th class="text-center">Teléfono</th><th class="text-center">Extención</th><th class="text-center">Celular</th><th class="text-center">Correo Electrónico</th></tr></thead><tbody><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table></div>');
	            }
	        });

	    });

	    ////function selectAllOnFocus(e) {
	    ////    if (e.type == "mouseup") { // Prevent default and detach the handler
	    ////        console.debug("Mouse is up. Preventing default.");
	    ////        e.preventDefault();
	    ////        $(e.target).off('mouseup', selectAllOnFocus);
	    ////        return;
	    ////    }
	    ////    $(e.target).select();
	    ////    console.debug("Selecting all text");
	    ////    $(e.target).on('mouseup', selectAllOnFocus);
	    ////}

   </script>
    
</asp:Content>
