<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="medidores.aspx.cs" Inherits="Medicion.medidores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
   

            <ol class="breadcrumb breadcrumb-verde" style="color: #454545;">
                <li>GESTIÓN DE CARGAS</li>
                <li><a href="consultascargas.aspx" style="color: #454545;">Consultas</a></li>
                <li>Medidores</li>
            </ol>
            
            <h4  style=" text-align:center; color:#427314"> Medidores de Punto de Carga</h4>

<%--    	
    <div class=" text-right">
		<a href="#" class="btn btn-warning btn-sm" id="btnSubirArchivo" aria-label="Left Align" title="Subir Archivos" data-toggle="tooltip" data-placement="top"><img src="img/cloud-upload.png"></a>
		<a href="#" onserverclick="historicorpuclic" id="historicorpu" runat="server" class="btn btn-info btn-sm" aria-label="Left Align" title="Historial RPU" data-toggle="tooltip" data-placement="top"><img src="img/history.png"></a>
		<a href="consultascargas.aspx" class="btn btn-success btn-sm" aria-label="Left Align" title="Medidores" data-toggle="tooltip" data-placement="top"  ><img src="img/smartmeter.png"></a>
    </div>--%>

 
       
    <div class="clearfix"></div>

    <!-- header  -->	
    <div class="panel panel-success" >
        <div class="panel-heading">    
            Punto de Carga
        </div>
        <div class="panel-body">    
            <div class="form-group">
                <div class="col-xs-12 col-md-3">
                    <label for="txtGroup">Grupo</label>
	                <asp:TextBox  runat="server" class="form-control" id="txtGroup" disabled></asp:TextBox>
                </div>
                <div class="form-group col-xs-12 col-md-7">
                    <label for="txtLoadingCharge">Descripción</label>
	        <asp:TextBox  runat="server" class="form-control" id="txtLoadingCharge" disabled></asp:TextBox>
                </div>
                <div class="form-group col-xs-12 col-md-2">
                    <label for="exampleInputEmail1">RPU</label>
	        <asp:TextBox  runat="server" class="form-control" id="txtRPU" disabled></asp:TextBox>
                </div>
            </div>

            
                    <div class="form-group">
                        <div class="col-xs-12 col-md-4">
                            <label for="exampleInputEmail1">Estatus</label>
                            <div class="selectContainer">                            
                                <asp:DropDownList ID="ddlEstatusRup" runat="server" CssClass="form-control" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group col-xs-12 col-md-4">
                            <label class="control-label">Gestor Medición</label>                            
                            <div class="selectContainer">                            
                                <asp:DropDownList ID="ddlGestorMedicion" runat="server" CssClass="form-control" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group col-xs-12 col-md-4">
                            <label class="control-label">Gestor Comercial</label>                            
                            <div class="selectContainer">                            
                                <asp:DropDownList ID="ddlGestorComercial" runat="server" CssClass="form-control" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                          <div class="form-group">
                        <div class="form-group col-xs-12 col-md-4">                         
                            <asp:CheckBox type="checkbox" ID="ChkPrelacion"  runat="server" />
                            <label class="control-label">Prelación</label>
                        </div>
                    </div>
                    </div>

        </div>
    </div>
	
    <br /> 
            	<div class="clearfix"></div>

            <div class="alert alert-success" role="alert" id="msgExito" runat="server">
                ¡La actualizacion se realizo con exito!                
            </div>
    
            <div class="clearfix"></div>
            
            <div class="alert alert-danger" role="alert" id="msgError" runat="server">
                <asp:Label ID="LblError" runat="server" > ¡Ocurrio un error al guaradar los datos!</asp:Label>
            </div>


	<div class="clearfix"></div>
            		                
                                <div class="col-xs-12 col-xs-12">        
                                    <div class="col-xs-8"></div>            
	                                <div class="col-xs-2"> 		                         
	                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Guardar" OnClick="btnSave_Click" />
                                    </div>    
                                    <div class="col-xs-2">  	       
                                        <asp:Button id="btnregresar2"  runat="server" OnClick="btnregresar2_Click" cssclass="btn btn-primary btn-sm btn-block" Text="Regresar" /> 
           
                                    </div>
                                 </div>	
    <div class="clearfix"></div>
      <br /> 
              <br /> 

    <!-- Medidores  -->	
    <div class="panel panel-success" >
        <div class="panel-heading">    
          Medidores
        </div>
        <div class="panel-body"> 
    <%--<div class="container">--%>
        <%--<h3>Edición RUP</h3>--%>
            <!-- New TAB -->
            <%--<div class="panel panel-default col-xs-12 col-md-12" style="padding: 10px; margin: 10px">--%>
                <div id="Tabs" >
                    <ul class="nav nav-tabs" >
                        <li class="active"><a data-toggle="tab" href="#personal" >Medidores</a></li>
                        <li><a data-toggle="tab" href="#employment" >Convenios</a></li>
                    </ul>
                    <div class="tab-content" style="padding-top: 20px">
                        <div   class="tab-pane fade in active" id="personal">
                            <!-- begin tab personal -->
<%--                                <div id="SubirArchivo">
						            <br/>
						                <div class="col-lg-6 col-sm-6 col-6">
				                            <p><asp:FileUpload ID="fileUpload" CssClass="btn btn-success" multiple="true" runat="server" /></p>
                                            <p>
                                                <p><asp:Button ID="btUpload" CssClass="btn btn-success" Text ="Subir Archivos" OnClick="Upload_Files" runat="server" /></p>
                                            </p>
                                            <p><asp:label id="lblFileList" runat="server"></asp:label></p>
                                            <p><asp:Label ID="lblUploadStatus" runat="server"></asp:Label></p>
                                            <p><asp:Label ID="lblFailedStatus" runat="server"></asp:Label></p>
				                        </div>
						            <br />                                                        
					            </div>--%>
                            <!-- end div subir archivo -->

<%--					            <div class="clearfix"></div>
					            <div class="form-group col-xs-12 col-md-3 ">
		                            <label class="control-label">ESTATUS</label>
		                            <div class="selectContainer ">
		                                <div class="selectContainer">
                                           <asp:DropDownList ID="cmbStatus" runat="server" CssClass="form-control" >
                                                <asp:ListItem Text="" Value=""></asp:ListItem>   
                                                
                                            </asp:DropDownList>  
                                        </div>
		                            </div>
		                        </div>
                                <div class="form-group col-xs-12 col-md-8 "> 
                                   <h4> <div class="alert alert-warning text-center" role="alert" id="lblStatus" runat="server"></div> </h4> 
                                </div>--%>
                        


		                        <div class="clearfix"></div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM1" />
		                                ¿Requiere contacto eléctrico?
		                              </label>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date " data-date="" id="FecPrev1" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev1" runat="server" name="" type="text" class="form-control " readonly ></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>                           
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date " id="FecIns1" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecInst1" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						    
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" ToolTip="Observaciones MAX:999 caracteres" runat="server" class="form-control text-uppercase" rows="3" id="txtObs1" MaxLength="999"></asp:TextBox>                            
		                        </div>

					            <div class="clearfix"></div>

					            <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM2" />
		                                ¿Contacto terminado?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev2" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                        <input id="txtFecPrevs2" runat="server" name="" type="text" class="form-control " readonly></input>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						    
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecInst2" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecInsts2" runat="server" name="" type="text" class="form-control " readonly></input>
                                
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						    
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs2" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>

					            <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM3" />
		                                ¿Requiere nodo de red?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev3" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev3" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						    
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns3" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns3" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						    
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs3" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>

		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM4" />
		                                ¿Nodo terminado?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev4" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev4" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						            <input type="hidden" id="dtp_input2" value="" /><br/>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns4" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns4" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						   
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs4" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>

		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM5" />
		                                ¿Es medidor principal?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev5" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev5" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns5" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns5" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs5" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>
		            
		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM6" />
		                                ¿Entregado?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev6" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev6" runat="server" name="" type="text" class="form-control " readonly></input>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns6" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns6" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs6" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>
		            
		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM7" />
		                                ¿Tiene medidor respaldo?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev7" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev7" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
						    
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns7" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns7" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs7" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>
		            
		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM8" />
		                                ¿Entregado?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev8" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev8" runat="server" name="" type="text" class="form-control " readonly></input>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns8" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns8" runat="server" name="" type="text" class="form-control " readonly></input>
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs8" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>
		            
		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM9" />
		                                ¿Carta sesión recibida?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev9" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev9" runat="server" name="" type="text" class="form-control " readonly></input>		                        
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns9" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns9" runat="server" name="" type="text" class="form-control " readonly></input>		                        
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs9" MaxLength="999"></asp:TextBox>
		                        </div>
					           
                                <div class="clearfix"></div>

		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM10" />
		                                ¿Medidor instalado?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev10" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev10" runat="server" name="" type="text" class="form-control " readonly></input>		                        
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns10" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns10" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs10" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>
		           
		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM11" />
		                                ¿Medidor con perfil?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev11" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev11" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns11" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns11" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs11" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>

		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM12" />
		                                ¿Requiere libranza?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev12" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev12" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns12" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns12" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs12" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>

		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM13" />
		                                ¿Carta compromiso firmada?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha  estimada</label>
		                            <div class="input-group date form_date" id="FecPrev13" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev13" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha carta firmada </label>
		                            <div class="input-group date form_date" id="FecIns13" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns13" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs13" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>

		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM14" />
		                                ¿Requiere reubicación?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev14" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev14" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns14" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns14" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs14" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>

		                        <div class="form-group col-xs-12 col-md-3">
		                            <div class="checkbox">
		                              <label style="font-weight:bold;">
		                                <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM15" />
		                                ¿Requiere gabinete?
		                              </label>
		                            </div>
		                        </div>
		                        <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha prevista</label>
		                            <div class="input-group date form_date" id="FecPrev15" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecPrev15" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label class="control-label">Fecha de instalación </label>
		                            <div class="input-group date form_date" id="FecIns15" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
		                                <input id="txtFecIns15" runat="server" name="" type="text" class="form-control " readonly></input>	
		                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
							            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
		                            </div>
		                        </div>
					            <div class="form-group col-xs-12 col-md-3">
		                            <label for="estatus">Observaciones</label>
		                            <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" rows="3" id="txtObs15" MaxLength="999"></asp:TextBox>
		                        </div>

					            <div class="clearfix"></div>
		                
                                <div class="col-xs-12 col-xs-12">        
                                    <div class="col-xs-8"></div>            
	                                <div class="col-xs-2"> 		                         
	                                    <asp:Button ID="btnAddZone" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Guardar" OnClick="btnSave_Click" />
                                    </div>    
                                    <div class="col-xs-2"> 
                                        <asp:Button id="btnregresar1" runat="server" OnClick="btnregresar1_Click" CssClass="btn btn-primary btn-sm btn-block" Text="Regresar" />
                                       
                                    </div>
                                 </div>		                

                        </div><!-- end tab personal -->

                        <div  class="tab-pane fade" id="employment">

                            <div class="clearfix"></div>

                            <div class="col-xs-12 col-md-12">
                                <div id="Div1" class="table-responsive" runat="server">
                                    <table id="mytable" class="table table-bordered table-striped table-hover">
                                        <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server">

                                        </asp:PlaceHolder>  
                                    </table>
                    
                                </div> <!-- end table responsive -->
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-12 text-center">
                                <ul class="pagination pull-right" id="myPager"></ul>
                            </div>


                            <div class="col-xs-12 col-xs-12">
                              <%--  <div class="col-xs-8"></div>    
	                            <div class="col-xs-2">                              
	                                 <input type="button" id="btnSaveAgreement" class="btn btn-success btn-sm btn-block" value="Guardar" />
                                </div>   
                                <div class="col-xs-2">                
                                    <button type="button"  onclick="window.history.back();" class="btn btn-primary btn-sm btn-block">Regresar</button>
                                </div>--%>
                            </div>   

                            <div class="clearfix"></div>
                            <div class="alert alert-success" role="alert" id="msginsert">
	                            <!-- Indicates a successful or positive action --> 
                        </div> <!-- end tab employee-->

                            <div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false">
                            <div class="modal-header">
                                <h1>Salvando cambios...</h1>
                            </div>
                            <div class="modal-body">
                                <div class="progress progress-striped active">
                                    <div class="bar" style="width: 100%;"></div>
                                </div>
                            </div>
                        </div>

                        </div>
            		    
                 </div>
              </div>
        <%--  </div>   --%>     
  <%--  </div>--%>
    </div>

	<asp:HiddenField ID="TabName" runat="server" />

    <script src="js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>	
    <script src="js/bootstrap-datetimepicker.es.js" type="text/javascript"></script>
    <script src="js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script src="js/Business/pagination.js" type="text/javascript"></script>
    
    <script>
        $(document).ready(function(){
            $(".nav-tabs a").click(function(){
                $(this).tab('show');
            });
        });
    </script>

    <script type="text/javascript">
    $('#SubirArchivo').hide();
           $('#btnSubirArchivo').click(function () 
           {
               $('#SubirArchivo').toggle();
               $("#btnBrowser").focusin();
           });
           $("#msginsert").hide();
        $(document).ready(function () {
            $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 8 });

            $("#btnSaveAgreement").click(function () {
                var intChecked = 0;
                var strRPU = $("#<%= txtRPU.ClientID %>").val();
                var strEmail = "<%= Session["email"]%>";
                
                var strAgreementselected = '';
                if ((!strRPU && strRPU.trim().length == 0)) {
                    //$("#msginsert").html("Falta seleccionar el RPU");
                    //$("#msginsert").addClass("alert alert-danger text-center");
                    //$("#msginsert").show();
                    swal(
                        '',
                        'Falta seleccionar el RPU',
                        'warning'
                    );
                    return false;
                }
                
                var charge = [];
                var i=0;

//                $(".clsnmbr").each(function(){
//                 var elemento= this;
//                 
//                 if(elemento.id != '')
//                 {
//                    alert("elemento.id="+ elemento.id + ", elemento.value=" + elemento.value); 
//                    charge.push({
//                        "product":[ [{"Agreement":elemento.id},{"Charge":elemento.value}]]
//                    });
//                    alert(charge[0][0]);
//                 }
//                 
//                });
//                $("input:number[class=clsnmbr]").each(function () {
//                    
//                    alert($(this).prop("id"));
//                
//                });
                $("input:checkbox[class=chk]").each(function () {

                    var strCh;
                    if ($(this).prop('checked') == true) 
                    {
                        intChecked = "1";

                    }
                    else 
                    {
                        intChecked = "0";
                    }
                    strAgreementselected = $(this).attr("id");
                    $(".clsnmbr").each(function(){
                         var elemento= this;
                         var strelementselected = "str" + strAgreementselected;
                         var strAgr = elemento.id;

                         if(elemento.value != '')
                         {
                            //alert(strAgreementselected);
                            if(strelementselected == strAgr)
                            {
                                
                                strCh = elemento.value;
                            }
                            
                            
                         }
                 
                        });
                    $.ajax({
                        type: "POST",
                        url: "/WebService/wsMeters.asmx/NewAgreemet",
                        data: "{ strAgreementselected : '" + strAgreementselected + "', strRPU: " + strRPU + ", strChecked : '" + intChecked + "', strEmail: '" + strEmail +"', strCharge : '" + strCh + "'}",
                        
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {

                            //$("#msginsert").removeAttr("style");
                            //$("#msginsert").html("<strong>" + r.d + "</strong> .");
                            //$("#msginsert").addClass("alert alert-success text-center");
                            //$("#msginsert").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                            //$("#msginsert").show();
                            swal(
                                '',
                                '<strong>' + r.d + '</strong>',
                                'success'
                            );
                            setTimeout(function () {
                                window.location.reload(1);
                            }, 2000);
                        },
                        error: function (r) {

                            //$("#msginsert").html(r.responseText);
                            //$("#msginsert").addClass("alert alert-danger");
                            //$("#msginsert").show();
                            swal(
                                '',
                                '<strong>' + r.responseText + '</strong>',
                                'error'
                            );


                        },
                        failure: function (r) {
                            //alert(r.responseText);
                            //$("#msginsert").html(r.responseText);
                            //$("#msginsert").addClass("alert alert-danger");
                            //$("#msginsert").show();
                            swal(
                                '',
                                '<strong>' + r.responseText + '</strong>',
                                'error'
                            );
                        }
                    });
                    
                });
                //$("input:checkbox[class=chk]:checked").each(function () {  });
                return false;
            });
           
            $('[data-toggle="tooltip"]').tooltip();
   
            $('.form_date').datetimepicker({
                language: 'es',
                pickerPosition: "bottom-left",
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 4,
                todayBtn: "linked",
                forceParse: 0,
                format: "dd-mm-yyyy"
            });
            
           

        });
        //$(function () 
        //{
        //    var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
            
        //    //$('#Tabs a[href="#' + tabName + '"]').tab('show');
        //    $("#Tabs a").click(function () 
        //    {
        //        $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
        //    });
        //});
        
       
    
       </script>
     
        
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
