<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="comunicaciones.aspx.cs" Inherits="Medicion.comunicaciones" %>

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
            <script type="text/javascript">

                function changeCommunicationType() {

                    //alert('changeCommunicationType');

                    var ddl = document.getElementById("<%=cmbCommunicationType.ClientID%>");

                    var Text = ddl.options[ddl.selectedIndex].text;
                    var Value = ddl.options[ddl.selectedIndex].value;

                    document.getElementById("<%=div_CommunicationType_IP_1.ClientID%>").style.display = 'none';
                    document.getElementById("<%=div_CommunicationType_IP_2.ClientID%>").style.display = 'none';
                    document.getElementById("<%=div_CommunicationType_IP_3.ClientID%>").style.display = 'none';
                    document.getElementById("<%=div_CommunicationType_IP_4.ClientID%>").style.display = 'none';



                    document.getElementById("<%=div_CommunicationType_4G_0.ClientID%>").style.display = 'none';
                    document.getElementById("<%=div_CommunicationType_4G_00.ClientID%>").style.display = 'none';

                    document.getElementById("<%=div_CommunicationType_4G_1.ClientID%>").style.display = 'none';
                    document.getElementById("<%=div_CommunicationType_4G_SubirArchivo.ClientID%>").style.display = 'none';
                    document.getElementById("<%=div_CommunicationType_4G_SubirArchivo.ClientID%>").style.display = 'none';

                    if (Value == '4') {
                        document.getElementById("<%=div_CommunicationType_4G_0.ClientID%>").style.display = 'block';
                        document.getElementById("<%=div_CommunicationType_4G_00.ClientID%>").style.display = 'block';

                        document.getElementById("<%=div_CommunicationType_4G_1.ClientID%>").style.display = 'block';
                        document.getElementById("<%=div_CommunicationType_4G_SubirArchivo.ClientID%>").style.display = 'block';
                        document.getElementById("<%=div_CommunicationType_4G_SubirArchivo.ClientID%>").style.display = 'block';
                    }
                    else if (Value == '1') {
                        document.getElementById("<%=div_CommunicationType_IP_1.ClientID%>").style.display = 'block';
                        document.getElementById("<%=div_CommunicationType_IP_2.ClientID%>").style.display = 'block';
                        document.getElementById("<%=div_CommunicationType_IP_3.ClientID%>").style.display = 'block';
                        document.getElementById("<%=div_CommunicationType_IP_4.ClientID%>").style.display = 'block';
                    }

                }

            </script>


            <ol class="breadcrumb breadcrumb-verde" style="color: #454545;">
                <li>GESTIÓN DE CARGAS</li>
                <li><a href="consultascargas.aspx" style="color: #454545;">Consultas</a></li>
                <li>Comunicaciones</li>
            </ol>

            <h4 style="text-align: center; color: #427314">Comunicaciones de Punto de Carga</h4>

            <%--            <div class=" text-right">
                <a href="#" class="btn btn-warning btn-sm" id="btnSubirArchivo" aria-label="Left Align" title="Subir Archivos" data-toggle="tooltip" data-placement="top">
                    <img src="img/cloud-upload.png"></a>
                <a href="#" onserverclick="historicorpuclic" id="historicorpu" runat="server" class="btn btn-info btn-sm" aria-label="Left Align" title="Historial RPU" data-toggle="tooltip" data-placement="top">
                    <img src="img/history.png"></a>
                <a href="consultascargas.aspx" class="btn btn-success btn-sm" aria-label="Left Align" title="Medidores">
                    <img src="img/smartmeter.png"></a>
            </div>--%>

            <br />

            <div class="clearfix"></div>

            <!-- header  -->
            <div class="panel panel-success">
                <div class="panel-heading">
                    Punto de carga
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-xs-12 col-md-3">
                            <label for="exampleInputEmail1">Grupo</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtGroup" disabled="disabled"></asp:TextBox>
                        </div>
                        <div class="form-group col-xs-12 col-md-7">
                            <label for="exampleInputEmail1">Descripción</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtLoadingCharge" disabled="disabled"></asp:TextBox>
                        </div>
                        <div class="form-group col-xs-12 col-md-2">
                            <label for="exampleInputEmail1">RPU</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtRPU" disabled="disabled"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlGestorMedicion"  runat="server" CssClass="form-control" AutoPostBack="false">
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
                    </div>
                    <%--se agrega una columna para el nuevo campo--%>
                     <div class="form-group">
                                 <div class="form-group col-xs-12 col-md-10">
		                            <label for="estatus">Observación Comunicacion-punto de carga</label>
		                            <asp:TextBox TextMode="multiline" ToolTip="Observación MAX:999 caracteres" runat="server" class="form-control text-uppercase" rows="2" id="txtComentComunicacion" MaxLength="999"></asp:TextBox>                            
		                        </div>
                        <div class="form-group col-xs-12 col-md-2">  
                            <br />
                            <label class="control-label">Prelación</label>

                            <asp:CheckBox type="checkbox" ID="ChkPrelacion"  runat="server" />
                        </div>
                    </div>
                </div>
            </div>


            <%--            <div id="SubirArchivo">
                <br />
                <div class="col-lg-6 col-sm-6 col-6">
                    <p>
                        <asp:FileUpload ID="fileUploadxxx" CssClass="btn btn-success" multiple="true" runat="server" /></p>
                    <p>
                    <p>
                            <asp:Button ID="btUploadxx" CssClass="btn btn-success" Text="Subir Archivos" OnClick="Upload_Files" runat="server" /></p>
                    </p>
                    <p>
                        <asp:Label ID="lblFileListxx" runat="server"></asp:Label></p>
                    <p>
                        <asp:Label ID="lblUploadStatusxx" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblFailedStatusxx" runat="server"></asp:Label>
                    </p>
                </div>
                <br />

            </div>
            <!--- end div subir archivo --> --%>

            <div class="clearfix"></div>
            <div class="alert alert-success" role="alert" id="msgExito" runat="server">
                ¡La actualizacion se realizo con exito!                
            </div>
            <div class="clearfix"></div>
            <div class="alert alert-danger" role="alert" id="msgError" runat="server">
                <asp:Label ID="LblError" runat="server"> ¡Ocurrio un error al guardar los datos!</asp:Label>
            </div>

            <div class="clearfix"></div>

            <%--            <div class="col-xs-12 col-xs-12">
                <div class="col-xs-8"></div>
                <!-- Botones -->
                <div class="col-xs-2">
                   
                    <asp:Button ID="Button1" runat="server" class="btn btn-success btn-sm btn-block" Text="Guardar" OnClick="btnSave_Click" />
                </div>
                <div class="col-xs-2">
                   
                    <button type="button" onclick="window.history.back();" class="btn btn-primary btn-sm btn-block">Regresar</button>
                </div>
            </div>--%>

            <br />
            <div class="clearfix"></div>
            <br />


            <div class="panel panel-success">
                <div class="panel-heading">
                    Comunicaciones
                </div>
                <div class="panel-body">

                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="form-group col-xs-12 col-md-3">
                                <div class="checkbox">
                                    <label style="font-weight: bold;">
                                        <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM1" />
                                        ¿Requiere TC y TP?
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha prevista</label>
                                <div class="input-group date form_date " data-date="" id="FecPrev1" data-date-format="dd/MM/yyyy" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecPrev1" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha de instalación </label>
                                <div class="input-group date form_date " id="FecIns1" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecInst1" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-5">
                                <label for="estatus">Observaciones</label>
                                <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs1" MaxLength="999"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="form-group col-xs-12 col-md-3">
                                <div class="checkbox">
                                    <label style="font-weight: bold;">
                                        <asp:CheckBox runat="server" AutoPostBack="false" ID="chkPM2" />
                                        ¿Requiere base 13 terminales?
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha prevista</label>
                                <div class="input-group date form_date" id="FecPrev2" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecPrev2" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha de instalación </label>
                                <div class="input-group date form_date" id="FecInst2" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecInst2" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-5">
                                <label for="estatus">Observaciones</label>
                                <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs2" MaxLength="999"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="form-group col-xs-12 col-md-3">
                                <label class="control-label">¿Medidor actual?</label>
                                <%--  <asp:TextBox  runat="server" class="form-control" id="txtActualMeter"></asp:TextBox>--%>
                                <div class="selectContainer">

                                    <asp:DropDownList ID="cmbActualMeter" runat="server" CssClass="form-control" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha prevista</label>
                                <div class="input-group date form_date" id="FecPrev3" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecPrev3" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha de instalación </label>
                                <div class="input-group date form_date" id="FecIns3" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecIns3" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-5">
                                <label for="estatus">Observaciones</label>
                                <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs3" MaxLength="999"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="panel panel-success">
                        <div class="panel-body">

                            <div class="form-group col-xs-12 col-md-3">
                                <label class="control-label">Tipo medidor</label>
                                <div class="selectContainer">
                                    <asp:DropDownList ID="cmbMeterType" runat="server" CssClass="form-control" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha prevista</label>
                                <div class="input-group date form_date" id="FecPrev4" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecPrev4" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                                <input type="hidden" id="Hidden1" value="" /><br />
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha de instalación </label>
                                <div class="input-group date form_date" id="FecIns4" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecIns4" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-5">
                                <label for="estatus">Observaciones</label>
                                <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs4" MaxLength="999"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="panel panel-success">
                        <div class="panel-body">

                            <div class="form-group col-xs-12 col-md-3">
                                <label class="control-label">¿Medidor requerido?</label>
                                <%--<asp:TextBox ID="txtRequiredMeter" runat="server" class="form-control"></asp:TextBox>--%>
                                <div class="selectContainer">
                                    <asp:DropDownList ID="cmbRequiredMeter" runat="server" CssClass="form-control" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha prevista</label>
                                <div class="input-group date form_date" id="FecPrev5" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecPrev5" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha de instalación </label>
                                <div class="input-group date form_date" id="FecIns5" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecIns5" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-5">
                                <label for="estatus">Observaciones</label>
                                <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs5" MaxLength="999"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="panel panel-success">
                        <div class="panel-body">

                            <div class="form-group col-xs-12 col-md-3">
                                <label class="control-label">Clase A/B</label>
                                <div class="selectContainer">
                                    <asp:DropDownList ID="cmbCommunicationClass" runat="server" CssClass="form-control" AutoPostBack="False">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha prevista</label>
                                <div class="input-group date form_date" id="FecPrev6" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecPrev6" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha de instalación </label>
                                <div class="input-group date form_date" id="FecIns6" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecIns6" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-5">
                                <label for="estatus">Observaciones</label>
                                <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs6" MaxLength="999"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>

            <div class="panel panel-success">
                <div class="panel-heading">
                    Medidor
                </div>
                <div class="panel-body">

                    <div class="clearfix"></div>
                    <div class="panel panel-success">
                        <div class="panel-body">
                            <div class="form-group col-xs-12 col-md-3">
                                <label class="control-label">Tipo de Comunicación</label>
                                <div class="selectContainer">
                                    <asp:DropDownList ID="cmbCommunicationType" runat="server" CssClass="form-control" AutoPostBack="false"
                                        onchange="changeCommunicationType()">
                                        <%--OnSelectedIndexChanged="cmbCommunicationType_SelectedIndexChanged"--%>
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha prevista</label>
                                <div class="input-group date form_date" id="FecPrev7" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecPrev7" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                                <input type="hidden" id="Hidden2" value="" /><br />
                            </div>
                            <div class="form-group col-xs-12 col-md-2">
                                <label class="control-label">Fecha de instalación </label>
                                <div class="input-group date form_date" id="FecIns7" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                    <input id="txtFecIns7" runat="server" name="" type="text" class="form-control " readonly></input>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                    <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                </div>
                            </div>
                            <div class="form-group col-xs-12 col-md-5">
                                <label for="estatus">Observaciones</label>
                                <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs7" MaxLength="999"></asp:TextBox>
                            </div>

                            <div id="div_CommunicationType_IP_1" runat="server" class="form-group col-xs-12 col-md-3" visible="true">
                            </div>

                            <div id="div_CommunicationType_IP_2" runat="server" class="form-group col-xs-12 col-md-3" visible="true">
                                <label class="control-label">IP</label>
                                <asp:TextBox runat="server" class="form-control" Rows="3" ID="txtIP"></asp:TextBox>
                            </div>
                            <div id="div_CommunicationType_IP_3" runat="server" class="form-group col-xs-12 col-md-3">
                                <label class="control-label">Mascara de subred</label>
                                <asp:TextBox runat="server" class="form-control" Rows="3" ID="txtMascara"></asp:TextBox>
                            </div>
                            <div id="div_CommunicationType_IP_4" runat="server" class="form-group col-xs-12 col-md-3">
                                <label for="estatus">Puerta de enlace</label>
                                <asp:TextBox runat="server" class="form-control" Rows="3" ID="txtPuertaEnlace"></asp:TextBox>
                            </div>

                            <div id="div_CommunicationType_4G_00" runat="server" class="form-group col-xs-12 col-md-3" visible="true">
                            </div>
                            <div id="div_CommunicationType_4G_0" runat="server" class="form-group col-xs-12 col-md-9" visible="true">

                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        Archivos Cargados
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-xs-9 col-md-9">
                                            <div id="Div1" class="table-responsive" runat="server">
                                                <table id="mytable" class="table table-bordered table-striped table-hover">
                                                    <asp:PlaceHolder ID="DBDataPlaceHolderArchivos" runat="server"></asp:PlaceHolder>
                                                </table>
                                            </div>
                                            <!-- end table responsive -->
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-md-12 text-center">
                                            <ul class="pagination pull-right" id="myPager"></ul>
                                        </div>

                                        <div id="div_CommunicationType_4G_1" runat="server" class="form-group col-xs-12 col-md-0" visible="true">
                                        </div>
                                        <div id="div_CommunicationType_4G_SubirArchivo" runat="server" class="form-group col-md-12" visible="true">
                                            <div class="col-sm-8">
                                                <asp:FileUpload ID="fileUpload" CssClass="btn btn-success" multiple="true" runat="server" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btUpload" runat="server" CssClass="btn btn-success" Text="Subir Archivos" OnClick="Upload_Files" />
                                            </div>
                                        </div>
                                        <!-- end div subir archivo -->
                                        <div id="div_CommunicationType_4G_3" runat="server" class="col-lg-12 col-sm-6 col-3">
                                            <asp:Label ID="lblFileList" runat="server"></asp:Label>
                                            <asp:Label ID="lblUploadStatus" runat="server"></asp:Label>
                                            <asp:Label ID="lblFailedStatus" runat="server"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="panel panel-success">
                            <div class="panel-body">

                                <div class="form-group col-xs-12 col-md-3">
                                    <label class="control-label">Prueba de comunicación Local</label>
                                    <div class="selectContainer">
                                        <asp:DropDownList ID="cmbLocalCommunication" runat="server" CssClass="form-control" AutoPostBack="False">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-md-2">
                                    <label class="control-label">Fecha prevista</label>
                                    <div class="input-group date form_date" id="FecPrev8" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                        <input id="txtFecPrev8" runat="server" name="" type="text" class="form-control " readonly></input>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                        <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-md-2">
                                    <label class="control-label">Fecha de instalación </label>
                                    <div class="input-group date form_date" id="FecIns8" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                        <input id="txtFecIns8" runat="server" name="" type="text" class="form-control " readonly></input>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                        <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-md-5">
                                    <label for="estatus">Observaciones</label>
                                    <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs8" MaxLength="999"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="panel panel-success">
                            <div class="panel-body">

                                <div class="form-group col-xs-12 col-md-3">
                                    <label class="control-label">Prueba de comunicación CFE</label>
                                    <div class="selectContainer">
                                        <asp:DropDownList ID="cmbCFECommunication" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-md-2">
                                    <label class="control-label">Fecha prevista</label>
                                    <div class="input-group date form_date" id="FecPrev9" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                        <input id="txtFecPrev9" runat="server" name="" type="text" class="form-control " readonly></input>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                        <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-md-2">
                                    <label class="control-label">Fecha de instalación </label>
                                    <div class="input-group date form_date" id="txtdivFecIns9" data-date="" data-date-format="dd MM yyyy" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd">
                                        <input id="txtFecIns9" runat="server" name="" type="text" class="form-control " readonly></input>
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                        <%--<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>--%>
                                    </div>
                                </div>
                                <div class="form-group col-xs-12 col-md-5">
                                    <label for="estatus">Observaciones</label>
                                    <asp:TextBox TextMode="multiline" runat="server" ToolTip="Observaciones MAX:999 caracteres" class="form-control text-uppercase" Rows="3" ID="txtObs9" MaxLength="999"></asp:TextBox>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

                <div class="clearfix"></div>

                <div class="col-xs-12 col-xs-12">
                    <div class="col-xs-8"></div>
                    <!-- Botones -->
                    <div class="col-xs-2">
                        <!-- Indicates a successful or positive action -->
                        <asp:Button ID="btnAddCommunnication" runat="server" class="btn btn-success btn-sm btn-block" Text="Guardar" OnClick="btnSave_Click" />
                    </div>
                    <div class="col-xs-2">
                        <!-- Indicates a successful or positive action -->

             <a href="javascript:window.history.back();" class="btn btn-primary btn-sm btn-block">&laquo; Regresar</a>

                        <%--<asp:Button type="button" id="Button2"   runat="server" OnClick="btnBack_Click" class="btn btn-primary btn-sm btn-block" Text="Regresar"/>--%>
                    </div>
                </div>

                <script type="text/javascript" src="js/bootstrap-datetimepicker.js" charset="UTF-8"></script>
                <script type="text/javascript" src="js/bootstrap-datetimepicker.es.js" charset="UTF-8"></script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btUpload" />
        </Triggers>
    </asp:UpdatePanel>


    <br />
    <div class="clearfix"></div>
    <br />

    <br />
    <br />
    <br />
    <br />

    <link href="Styles/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
  
    <script src="js/bootstrap-select.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.selectpicker').selectpicker({
            style: 'btn-Default',
            size: 5
        });
        $('#SubirArchivo').hide();
        $('#btnSubirArchivo').click(function () {
            $('#SubirArchivo').toggle();
            $("#btnBrowser").focusin();
        });

        $('.form_date').datetimepicker({
            language: 'es',
            pickerPosition: "bottom-left",
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            todayBtn: "linked",
            forceParse: 0,
            format: "dd-mm-yyyy",
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            }
        });
    </script>

</asp:Content>
