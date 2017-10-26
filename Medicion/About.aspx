<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="Medicion.About" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<%--Aqui dentro van los objetos de nuestro web forma--%>
	 <ol class="breadcrumb breadcrumb-verde">
          <li><a href="#">PUNTOS DE CARGA</a></li>
          <li><a href="#">NUEVO</a></li>
        </ol>
        <form>
            <div class="form-group col-xs-6 col-md-4">
                <label class="control-label">GRUPO</label>
                <div class="selectContainer">
                    <select class="form-control" name="size">
                        <option value="">DANA</option>
                        <option value="s">CERVECERIA</option>
                        <option value="m">CIE PEMSA</option>
                        <option value="l">LALA</option>
                        <option value="xl">ELEMENTIA</option>
                        <option value="xl">KCM</option>
                        <option value="xl">DENSO</option>
                    </select>
                </div>
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">PUNTOS DE CARGAS</label>
                <div class="selectContainer">
                    <select class="form-control" name="size">
                        <option value="">Dana de México Corporación S. de R.L. de C.V.  Dana de México Corporación Queretaro</option>
                        <option value="s">Cervecería Cuauhtémoc Moctezuma, S. A. de C. V. ( Planta Orizaba)</option>
                        <option value="m">CIE Celaya, S.A.P.I. de C.V., Planta Celaya</option>
                        <option value="l">Comercializadora de Lácteos y Derivados, S.A. de C.V., Planta Aguascalientes</option>
                        <option value="xl">Compañía Mexicana de Concretos Pretensados COMECOP, S.A. de C.V., Planta Tizayuca</option>
                        <option value="xl">Grupo Papelero Scribe, S.A. de C.V. Planta Morelia</option>
                        <option value="xl">Denso México, S.A. DE C.V., Denso Apodaca </option>
                    </select>
                </div>
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">RPU</label>
                <div class="selectContainer">
                    <select class="form-control" name="size">
                        <option value="">077080804809</option>
                        <option value="s">872940105631</option>
                        <option value="m">336080601849</option>
                        <option value="l">065011213787</option>
                        <option value="xl">096091203321</option>
                        <option value="xl">983040501758</option>
                        <option value="xl">613100300887</option>
                    </select>
                </div>
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label for="exampleInputEmail1">TARIFA</label>
                <input type="tarifa" class="form-control" id="tarifa" placeholder="">
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label for="exampleInputEmail1">CAPACIDAD MW</label>
                <input type="capacidadmw" class="form-control" id="capacidadmw" placeholder="">
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label for="exampleInputEmail1">DEMANDA CONTRATADA</label>
                <input type="DemandaContratada" class="form-control" id="DemandaContratada" placeholder="">
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">DIVISION CFE</label>
                <div class="selectContainer">
                    <select class="form-control" name="size">
                        <option value="">División Bajío</option>
                        <option value="s">División Oriente</option>
                        <option value="m">División Valle de México Sur</option>
                        <option value="l">División Bajío</option>
                        <option value="xl">División Valle de México Norte</option>
                        <option value="xl">División Jalisco</option>
                        <option value="xl">División Norte</option>
                    </select>
                </div>
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">GESTOR MEDICION</label>
                <div class="selectContainer">
                    <select class="form-control" name="size">
                        <option value="">ERICK</option>
                        <option value="s">MARIO</option>
                        <option value="m">ANGEL</option>
                        <option value="l">JUAN</option>
                        <option value="xl">CARLOS</option>
                        
                    </select>
                </div>
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">GESTOR COMERCIAL</label>
                <div class="selectContainer">
                    <select class="form-control" name="size">
                        <option value="">VSP</option>
                        <option value="s">PRV</option>
                        <option value="m">MLP</option>
                        <option value="l">FMG</option>
                        <option value="xl">RRM</option>
                        
                    </select>
                </div>
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label for="exampleInputEmail1">CENTRAL</label>
                <input type="DemandaContratada" class="form-control" id="DemandaContratada" placeholder="PARA QUE SE VA A USAR?????">
            </div>
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">ESTATUS</label>
                <div class="selectContainer">
                    <select class="form-control" name="size">
                        <option value="">1</option>
                        <option value="s">??</option>
                        <option value="m">??</option>
                        <option value="l">??</option>
                       
                        
                    </select>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-xs-12 col-md-4">
                <div class="table-responsive">
                    <table id="mytable" class="table table-bordred table-striped">
                       <thead>
                        <th>ASIGNAR</th>
                        <th>PLANTA</th>
                       </thead>
                    <tbody>
                        <tr>
                            <td><input type="checkbox" class="checkthis" /></td>
                            <td>IEM 16</td>
                        </tr>
                         <tr>
                            <td><input type="checkbox" class="checkthis" /></td>
                            <td>ETK 2</td>


                        </tr>
                         <tr>
                            <td><input type="checkbox" class="checkthis" /></td>
                            <td>LAL_4</td>

                        </tr>
                         <tr>
                            <td><input type="checkbox" class="checkthis" /></td>
                            <td>PEM 6 </td>
                        </tr>   
                         <tr>
                            <td><input type="checkbox" class="checkthis" /></td>
                            <td>PIER II 2</td>

                         </tr>
                        <tr>
                            <td><input type="checkbox" class="checkthis" /></td>
                            <td>PIER II 2</td>

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
                </div> <!-- end table responsive -->
                
            </div>
            <div class="clearfix"></div>
            
            <div class="col-xs-6 col-md-4">
                <!-- Indicates a successful or positive action -->
                <button type="button" class="btn btn-success btn-lg btn-block">Guardar</button>
                
            </div>
            <div class="col-xs-6 col-md-4">
                <!-- Indicates a successful or positive action -->
                <button type="button" class="btn btn-danger btn-lg btn-block">Cancelar</button>
            </div>
			
        </form>
			
        <br><br><br><br><br>
</asp:Content>