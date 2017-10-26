<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="cargasautomaticas.aspx.cs" Inherits="Medicion.cargasautomaticas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">

        <ol class="breadcrumb breadcrumb-verde" style="color: #454545;">
          <li>Puntos de carga</li>
          <li>Desde excel</li>
        </ol>

		<div class=" text-right">                  
				<a href="consultascargas.aspx" class="btn btn-success btn-sm" data-toggle="tooltip" data-placement="top" aria-label="Left Align" title="Medidores" ><img src="img/smartmeter.png"></a>
		</div>

        <div class="col-lg-6 col-sm-6 col-12">
            <h4>ARCHIVO DE EXCEL PUNTO DE CARGA</h4>
            <div class="input-group">
                <label class="input-group-btn">
                    <span class="btn btn-primary btnbrowser">
                        Browse&hellip; <input type="file" style="display: none;" multiple>
                    </span>
                </label>
                <input type="text" class="form-control" readonly>
            </div>
            <span class="help-block">
                Seleccionar el archivo a importar
            </span>
        </div>
        <div class="clearfix"></div>
        <!-- MUESTRA LOS DATOS DEL ARCHIVO DE EXCEL -->
        <div id="resultado" class="col-md-12">
            <div class="table-responsive">
                <table id="mytable" class="table table-bordred table-striped">
                   <thead>
                    <th><input type="checkbox" id="checkall" /></th>
                    <th>GRUPO</th>
                    <th>PUNTO DE CARGA</th>
                    <th>RPU</th>
                    <th>TARIFA</th>
                    <th>CAPACIDAD MW</th>
                    <th>DEMANDA CONTRATADA</th>
                    <th>DIVISION CFE</th>
                    <th>GESTOR MEDICION</th>
                    <th>GESTOR COMERCIAL</th>
                   </thead>
                <tbody>

                    <tr>
                        <td><input type="checkbox" class="checkthis" /></td>
                        <td>DANA</td>
                        <td>Dana de México Corporación S. de R.L. de C.V.  Dana de México Corporación Queretaro</td>
                        <td>077 080 804 809</td>
                        <td>HS</td>
                        <td>8.000</td>
                        <td>19330</td>
                        <td>División Bajío</td>
                        <td>Erick</td>
                        <td>VSP</td>
                    </tr>

                    <tr>
                        <td><input type="checkbox" class="checkthis" /></td>
                        <td>DANA</td>
                        <td>Dana de México Corporación S. de R.L. de C.V.  Dana de México Corporación Queretaro</td>
                        <td>077 080 804 809</td>
                        <td>HS</td>
                        <td>8.000</td>
                        <td>19330</td>
                        <td>División Bajío</td>
                        <td>Erick</td>
                        <td>VSP</td>
                    </tr>


                     <tr>
                        <td><input type="checkbox" class="checkthis" /></td>
                        <td>DANA</td>
                        <td>Dana de México Corporación S. de R.L. de C.V.  Dana de México Corporación Queretaro</td>
                        <td>077 080 804 809</td>
                        <td>HS</td>
                        <td>8.000</td>
                        <td>19330</td>
                        <td>División Bajío</td>
                        <td>Erick</td>
                        <td>VSP</td>
                    </tr>
                    <tr>
                        <td><input type="checkbox" class="checkthis" /></td>
                        <td>DANA</td>
                        <td>Dana de México Corporación S. de R.L. de C.V.  Dana de México Corporación Queretaro</td>
                        <td>077 080 804 809</td>
                        <td>HS</td>
                        <td>8.000</td>
                        <td>19330</td>
                        <td>División Bajío</td>
                        <td>Erick</td>
                        <td>VSP</td>
                    </tr>
                    <tr>
                        <td><input type="checkbox" class="checkthis" /></td>
                        <td>DANA</td>
                        <td>Dana de México Corporación S. de R.L. de C.V.  Dana de México Corporación Queretaro</td>
                        <td>077 080 804 809</td>
                        <td>HS</td>
                        <td>8.000</td>
                        <td>19330</td>
                        <td>División Bajío</td>
                        <td>Erick</td>
                        <td>VSP</td>
                    </tr>
                    <tr>
                        <td><input type="checkbox" class="checkthis" /></td>
                        <td>DANA</td>
                        <td>Dana de México Corporación S. de R.L. de C.V.  Dana de México Corporación Queretaro</td>
                        <td>077 080 804 809</td>
                        <td>HS</td>
                        <td>8.000</td>
                        <td>19330</td>
                        <td>División Bajío</td>
                        <td>Erick</td>
                        <td>VSP</td>
                    </tr>
                </tbody>

            </table>
                <div class="clearfix"></div>
                    <ul class="pagination pull-right">
                      <li class="disabled"><a href="#"><span class="glyphicon glyphicon-chevron-left"></span></a></li>
                      <li class="active"><a href="#">1</a></li>
                      <li><a href="#">2</a></li>
                      <li><a href="#">3</a></li>
                      <li><a href="#">4</a></li>
                      <li><a href="#">5</a></li>
                      <li><a href="#"><span class="glyphicon glyphicon-chevron-right"></span></a></li>
                    </ul>
            </div> <!-- table 2 responsive end -->
            <div class="clearfix"></div>
            <div class="col-xs-6 col-md-4">
                <!-- Indicates a successful or positive action -->
                <button type="button" class="btn btn-success btn-lg btn-block">Guardar</button>
                
            </div>
            <div class="col-xs-6 col-md-4">
                <!-- Indicates a successful or positive action -->
                <button type="button" class="btn btn-danger btn-lg btn-block">Cancelar</button>
            </div>
            
        </div>
        <br><br><br><br><br>
	<script>
	    $("#resultado").hide();
	    $(function () {

	        // We can attach the `fileselect` event to all file inputs on the page
	        $(document).on('change', ':file', function () {
	            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
	            input.trigger('fileselect', [numFiles, label]);
	        });

	        // We can watch for our custom `fileselect` event like this
	        $(document).ready(function () {
	            $(':file').on('fileselect', function (event, numFiles, label) {

	                var input = $(this).parents('.input-group').find(':text'),
                      log = numFiles > 1 ? numFiles + ' files selected' : label;

	                if (input.length) {
	                    input.val(log);
	                    $("#resultado").show();
	                } else {
	                    if (log) alert(log);
	                }

	            });
	        });
	        $(function () {
	            $('[data-toggle="tooltip"]').tooltip()
	        })
	    });
   </script>
</asp:Content>
