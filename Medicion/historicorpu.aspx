<%@ Page Title="Consulta de Histórico de Puntos de Carga" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="historicorpu.aspx.cs" Inherits="Medicion.historicorpu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <style>

.nav-tabs { border-bottom: 2px solid #DDD; }
    .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover { border-width: 0; }
    .nav-tabs > li > a { border: none; color: #666; }
        .nav-tabs > li.active > a, .nav-tabs > li > a:hover { border: none; color: #427314 !important; background: transparent; }
        .nav-tabs > li > a::after { content: ""; background: #427314; height: 2px; position: absolute; width: 100%; left: 0px; bottom: -1px; transition: all 250ms ease 0s; transform: scale(0); }
    .nav-tabs > li.active > a::after, .nav-tabs > li:hover > a::after { transform: scale(1); }
.tab-nav > li > a::after { background: #21527d none repeat scroll 0% 0%; color: #fff; }
.tab-pane { padding: 15px 0; }
.tab-content{padding:20px}

        .clickable{
    cursor: pointer;   
}

    </style>
    <ol class="breadcrumb breadcrumb-verde" style="color: #454545; ">
	  <li>Gestión de Cargas</li>
	  <li>Histórico</li>
	</ol>
    
    <h4  style=" text-align:center; color:#427314"> Consulta de Histórico de Puntos de Carga</h4>

	<div class="clearfix"></div>

    <div class="panel panel-success">
        <div class="panel-body">
            <div class="form-group col-xs-12 col-xs-4">
                <label class="control-label">Grupo</label>                
                    <asp:DropDownList ID="cmbGroup" runat="server" ToolTip="Grupos" CssClass="form-control" AutoPostBack="True" 
                        onselectedindexchanged="itemSelected">
                        <asp:ListItem Text="" Value=""></asp:ListItem>                
                    </asp:DropDownList>                    
            </div>
	        <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">Punto de Carga</label>                
                            <asp:DropDownList ID="ddl" ToolTip="Puntos de carga" CssClass="form-control" runat="server">
                            </asp:DropDownList>                    
	        </div>
            <div class="form-group col-xs-12 col-xs-2">
		         <br/>
                </div>
	        <div class="form-group col-xs-12 col-xs-2">
		         <br/>
                <asp:Button ID="btnSearch" runat="server" ToolTip="Buscar" CssClass="btn btn-success btn-sm btn-block" Text="Buscar" OnClick="btnSearch_Click" />   
	        </div> 
        </div>
    </div> 

    <div class="clearfix"></div>

    <div class="form-group col-xs-12 col-md-12">
        <div class="alert alert-danger" style="display:none" role="alert" id="msgErrNew" runat="server"> </div>
    </div>

    <div class="clearfix"></div>

    <%--<div class="form-group col-xs-12 col-md-3">
        <div class="checkbox">
            <label>
            <input type="checkbox" value="" id="togglegenerales"/>
            Ocultar Arhivos Importados
            </label>
        </div>
    </div>--%>

		<div class="clearfix"></div>
        <!-- dos panel -->

   
        <div class="panel panel-default col-xs-12 col-md-12" style="padding: 10px; margin: 10px">
            <div id="Tabs" role="tabpanel">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist">
                    <li><a href="#PuntosdeCarga" title="Puntos de carga"   aria-controls="PuntosdeCarga"   role="tab" data-toggle="tab" style="font-size:16px !important; font-weight:bold;">Punto de Carga</a></li>
                    <li><a href="#Convenios" title="Convenios"        aria-controls="Convenios"       role="tab" data-toggle="tab" style="font-size:16px !important; font-weight:bold;">Convenios</a></li>
                    <li><a href="#Comunicacion" title="Comunicaciones"     aria-controls="Comunicacion"    role="tab" data-toggle="tab" style="font-size:16px !important; font-weight:bold;">Comunicación</a></li>
                    <li><a href="#Medidores" title="Medidores"       aria-controls="Medidores"       role="tab" data-toggle="tab" style="font-size:16px !important; font-weight:bold;">Medidores</a></li>                    
                </ul>
                <!-- Tab panes -->
                <div class="tab-content" style="padding-top: 20px">
                    
                    <div role="tabpanel" class="tab-pane active" id="PuntosdeCarga">                         
				            <div class="form-group  col-xs-12 col-md-12">
                                <div id="Div1" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                    <table id="mytable" class="table table-bordred table-striped" data-toggle="table" data-show-toggle="true" data-show-columns="true" data-search="true" data-striped="true" data-show-export="true" >
                                        <asp:PlaceHolder ID="DBDataPlaceHolderPuntosdeCarga" runat="server"></asp:PlaceHolder>  
                                    </table>                    
                                </div> <!-- end table responsive -->
                                <div class="clearfix"></div>
                                <div class="col-md-12 text-center">
                                    <ul class="pagination pull-right" id="myPagerPuntosdeCarga"></ul>
                                </div>  
                            </div>
			            
                    </div><!-- end tab Medidores -->

                    <div role="tabpanel" class="tab-pane" id="Convenios">
                        <div class="form-group  col-xs-12 col-md-12">
                            <div id="Div3" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="myTableConvenios" class="table table-bordred table-striped" data-toggle="table" data-show-toggle="true" data-show-columns="true" data-search="true" data-striped="true" data-show-export="true">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderConvenios" runat="server"></asp:PlaceHolder>  
                                </table>                    
                            </div> <!-- end table responsive -->
                            <div class="clearfix"></div>
                            <div class="col-md-12 text-center">
                                <ul class="pagination pull-right" id="myPagerConvenios"></ul>
                            </div>  
                        </div>
                    </div> 
                    
                    <div role="tabpanel" class="tab-pane" id="Medidores">
                        <div class="form-group  col-xs-12 col-md-12">

<%--                            <div id="Div4" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="myTableMedidores" class="table table-bordred table-striped" data-toggle="table" data-show-toggle="true" data-show-columns="true" data-search="true" data-striped="true" data-show-export="true">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderMedidores" runat="server"></asp:PlaceHolder>  
                                </table>                    
                            </div> 
                            <div class="clearfix"></div>
                            <div class="col-md-12 text-center">
                                <ul class="pagination pull-right" id="myPagerMedidores"></ul>
                            </div>  --%>
                        
                                                
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse1">¿Requiere contacto eléctrico?</a>   
                                    <asp:Label ID="lblPReguntaMed1" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse1" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div id="Div2" class="table-responsive" data-show-columns="true" data-id-field="id" runat="server">
                                        <table id="TablerMedidores10" class="table table-bordred table-striped"  data-show-columns="true"  data-striped="true" >
                                            <asp:PlaceHolder ID="PlaceHolderrMedidores10" runat="server"></asp:PlaceHolder>  
                                        </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse2">
                                    ¿Contacto terminado?
                                    </a>   
                                    <asp:Label ID="Label1" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse2" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div14" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores20" class="table table-bordred table-striped" data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores11" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse3">
                             ¿Requiere nodo de red?</a>   
                                    <asp:Label ID="Label2" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse3" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div15" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores30" class="table table-bordred table-striped"" data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores12" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse4">
                             ¿Nodo terminado?</a>   
                                    <asp:Label ID="Label3" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse4" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div16" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores40" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores13" runat="server"></asp:PlaceHolder>  
                                </table>                     
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                        

                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse5">
                            ¿Es medidor principal?</a>   
                                    <asp:Label ID="Label4" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse5" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div17" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores50" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores14" runat="server"></asp:PlaceHolder>  
                                </table>                      
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse6">
                           ¿Entregado?</a>   
                                    <asp:Label ID="Label5" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse6" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div18" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores60" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores15" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse7">
                             ¿Tiene medidor respaldo?</a>   
                                    <asp:Label ID="Label6" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse7" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div19" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores70" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores16" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse8">
                            ¿Entregado?</a>   
                                    <asp:Label ID="Label7" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse8" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div20" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores80" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores17" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse9">
                            ¿Carta sesión recibida?</a>   
                                    <asp:Label ID="Label8" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse9" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div21" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores901" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores18" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse10">
                            ¿Medidor instalado? </a>   
                                    <asp:Label ID="Label9" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse10" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div4" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores902" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores19" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse11">
                             ¿Medidor con perfil?</a>   
                                    <asp:Label ID="Label10" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse11" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div22" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores903" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores20" runat="server"></asp:PlaceHolder>  
                                </table>                     
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse12">
                             ¿Requiere libranza?</a>   
                                    <asp:Label ID="Label11" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse12" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div23" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores904" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores21" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse13">
                             ¿Carta compromiso firmada?</a>   
                                    <asp:Label ID="Label12" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse13" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div24" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores905" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores22" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse14">
                            ¿Requiere reubicación? </a>   
                                    <asp:Label ID="Label13" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse14" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div25" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores906" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores23" runat="server"></asp:PlaceHolder>  
                                </table>                     
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse15">
                            ¿Requiere gabinete? </a>   
                                    <asp:Label ID="Label14" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse15" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div26" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="TablerMedidores907" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolderrMedidores24" runat="server"></asp:PlaceHolder>  
                                </table>                     
                                    </div> 
                                </div> 
                        </div>
                    </div>
                                                                                                                                                                         
                        
                        </div>
                    </div> 

                    <div role="tabpanel" class="tab-pane" id="Comunicacion">
                        <div class="form-group  col-xs-12 col-md-12">
          <%--                  <div id="Div2" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table1" class="table table-bordred table-striped" data-toggle="table" data-show-toggle="true" data-show-columns="true" data-search="true" data-striped="true" data-show-export="true">
                                    <asp:PlaceHolder ID="DBDataPlaceHolderCommunication" runat="server"></asp:PlaceHolder>  
                                </table>                    
                            </div> 
                            <div class="clearfix"></div>
                            <div class="col-md-12 text-center">
                                <ul class="pagination pull-right" id="myPagerCom"></ul>
                            </div>--%>  
                            
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse16">
                            ¿Requiere TC y TP?</a>   
                                    <asp:Label ID="Label15" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse16" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div5" class="table-responsive" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table10" class="table table-bordred table-striped"  data-show-columns="true"  data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse17">
                            ¿Requiere base 13 terminales?</a>   
                                    <asp:Label ID="Label16" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse17" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div6" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table20" class="table table-bordred table-striped" data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse18">
                            ¿Medidor actual?</a>   
                                    <asp:Label ID="Label17" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse18" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div7" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table30" class="table table-bordred table-striped"" data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse19">
                                        Tipo medidor</a>   
                                    <asp:Label ID="Label18" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse19" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div8" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table40" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        

                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse20">
                            ¿Medidor requerido?</a>   
                                    <asp:Label ID="Label19" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse20" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div9" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table50" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder5" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                        
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse21">
                            Clase A/B</a>   
                                    <asp:Label ID="Label20" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse21" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div10" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table60" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder6" runat="server"></asp:PlaceHolder>  
                                </table>                    
                                    </div> 
                                </div> 
                        </div>
                    </div>
                                            
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse25">
                            Tipo de Comunicación</a>   
                                    <asp:Label ID="Label23" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse25" class="panel-collapse collapse">
                        <div class="panel-body">
                            <div id="Div11" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table70" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder7" runat="server"></asp:PlaceHolder>  
                                </table> 
                            </div>
                        
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Archivos cargados
                                </div>                    
                                <div class="panel-body">
                        
                                    <div class="col-xs-9 col-md-9">
                                        <div id="Div27" class="table-responsive" runat="server">
                                             
                                            <table id="mytableFiles" class="table table-bordered table-striped table-hover">
                                                <asp:PlaceHolder ID="DBDataPlaceHolderArchivos" runat="server"></asp:PlaceHolder>  
                                            </table>
                    
                                        </div> <!-- end table responsive -->
                                    </div>
                                      
                                </div>
                            </div>
                        </div>
                                </div> 
                    </div>
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse22">
                            Prueba de comunicación Local</a>   
                                    <asp:Label ID="Label21" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse22" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div12" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table80" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder8" runat="server"></asp:PlaceHolder>  
                                </table>                   
                                    </div> 
                                </div> 
                        </div>
                    </div>
                    
                    <div class="clearfix"></div>                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse23">
                            Prueba de comunicación CFE</a>   
                                    <asp:Label ID="Label22" runat="server" ></asp:Label> 
                                </h3>  
                        </div>
                        <div id="collapse23" class="panel-collapse collapse">
                                <div class="panel-body">
                            <div id="Div13" class="table-responsive docs-table" data-show-columns="true" data-id-field="id" runat="server">
                                <table id="Table90" class="table table-bordred table-striped" " data-show-columns="true" data-striped="true" >
                                    <asp:PlaceHolder ID="PlaceHolder9" runat="server"></asp:PlaceHolder>  
                                </table>                      
                                    </div> 
                                </div> 
                        </div>
                    </div>
                                       
                    <div class="clearfix"></div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!-- end dos panels -->

		<div class="clearfix"></div>
		<!-- MODAL shoe details -->
			<div class="modal" id="my_modal">
			  <div class="modal-dialog">
			    <div class="modal-content">
			      <div class="modal-header ">
					
			        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
			          
						 <h4><input type="text" class="modal-title col-xs-12 col-md-6" name="titleid"  disabled="false"></h4>
			      </div>
			      <div class="modal-body">
			       
					
					<div class="form-group col-xs-12 col-md-4">
		                <label for="exampleInputEmail1">Fecha Instalacion</label>
		                <input type="text" class="form-control" name="fi"  disabled="true">
		            </div>
					<div class="form-group col-xs-12 col-md-4">
		                <label for="exampleInputEmail1">Fecha Prevista</label>
		                <input type="text" class="form-control" name="fp" disabled="true">
		            </div>
					<div class="clearfix"></div>
					<div class="form-group col-xs-12 col-md-12">
		                <label for="exampleInputEmail1">Observaciones</label>
						<textarea class="form-control" rows="3" name="obs" disabled="true" ></textarea>
		            </div>
					
					
			      </div>
			      <div class="modal-footer">
			        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
			      </div>
			    </div>
			  </div>
			</div>
            
		<!-- end modal show details -->
        <script src="js/Business/pagination.js" type="text/javascript"></script>  
		<script>

		    $(document).on('click', '.panel-heading span.clickable', function (e) {
		        var $this = $(this);
		        if (!$this.hasClass('panel-collapsed')) {
		            $this.parents('.panel').find('.panel-body').slideUp();
		            $this.addClass('panel-collapsed');
		            $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
		        } else {
		            $this.parents('.panel').find('.panel-body').slideDown();
		            $this.removeClass('panel-collapsed');
		            $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
		        }
		    })

		    $(document).ready(function () {


		        $(".btn-pref .btn").click(function () {
		            $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
		            // $(".tab").addClass("active"); // instead of this do the below 
		            $(this).removeClass("btn-default").addClass("btn-primary");
		        });

		        $('#mTablePuntosdeCarga').pageMe({ pagerSelector: '#myPagerPuntosdeCarga', showPrevNext: true, hidePageNumbers: false, perPage: 5 });
		        $('#myTableConvenios').pageMe({ pagerSelector: '#myPagerConvenios', showPrevNext: true, hidePageNumbers: false, perPage: 5 });
		        $('#myTableMedidores').pageMe({ pagerSelector: '#myPagerMedidores', showPrevNext: true, hidePageNumbers: false, perPage: 5 });
		        $('#myTableCom').pageMe({ pagerSelector: '#myPagerCom', showPrevNext: true, hidePageNumbers: false, perPage: 5 });
		        
		        $('#generalesrow').hide();
		        $('#my_modal').on('show.bs.modal', function (e) {
		            var obsId = $(e.relatedTarget).data('obs-id');
		            $(e.currentTarget).find('textarea[name="obs"]').val(obsId);
		            var fiId = $(e.relatedTarget).data('fi-id');
		            $(e.currentTarget).find('input[name="fi"]').val(fiId);
		            var fpId = $(e.relatedTarget).data('fp-id');
		            $(e.currentTarget).find('input[name="fp"]').val(fpId);
		            var titleid = $(e.relatedTarget).data('title-id');
		            $(e.currentTarget).find('input[name="titleid"]').val(titleid);
		        });
		        $(function () {
		            $('[data-toggle="tooltip"]').tooltip()
		        });
		        $('#togglegenerales').click(function () {
		            $('td[id=hidefile]').toggle();
		            $('th[id=hidefile]').toggle();
		        });
		        $('.selectpicker').selectpicker({
		            style: 'btn-Default',
		            size: 15
		        });
		        $(function () {
		            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
		            $('#Tabs a[href="#' + tabName + '"]').tab('show');
		            $("#Tabs a").click(function () {
		                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
		            });
		        });
		    });
		    
		</script>

    <link href="Styles/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-select.min.js" type="text/javascript"></script>
</asp:Content>
