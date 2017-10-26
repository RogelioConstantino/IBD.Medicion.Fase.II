<%@ Page Title="Carga de Convenios de Transmisión" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="porteoautomatico.aspx.cs" Inherits="Medicion.porteoautomatico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <ol class="breadcrumb breadcrumb-verde" style="color: #454545;">
        <li>Convenios de Transmisión</li>
        <li>Importar Archivo</li>
    </ol>

    <h4 style="text-align: center; color: #427314">Carga de Convenios de Transmisión</h4>

    <%--    <div class="form-group col-xs-12 col-md-12">
        <div class=" text-right">
            <p>
	            <a href="consultascargas.aspx" class="btn btn-success btn-sm" aria-label="Left Align" title="Medidores" data-toggle="tooltip" data-placement="top"  >
                    <img src="img/smartmeter.png"/>
	            </a>
            </p>
        </div>
    </div>--%>

    <div class="clearfix"></div>
    <div class="panel panel-success" id="pnlPermiso" runat="server">
        <div class="panel-body">

            <!-- sel. central -->
            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">Central</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cmbCentral" runat="server" CssClass="form-control" OnSelectedIndexChanged="cmbCentral_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
               <div class="form-group col-xs-12 col-md-2">
                <label class="control-label">Convenio</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cmbConvenio" runat="server" CssClass="form-control" OnSelectedIndexChanged="cmbConvenio_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-xs-12 col-md-12 ui-corner-bottom">
                <div class="alert alert-danger" runat="server" id="pnlValidaciones" visible="true">
                    Se debe seleccionar una Central.
                </div>
            </div>
               <div class="form-group col-xs-12 col-md-12">
        <div class="panel panel-danger" id="pnlErrorLoad" runat="server" visible="true">
            <div class="panel-heading">Resultado de carga:</div>
            <div class="panel-body">
                <div runat="server" id="pnlErrorLoadDiv" visible="true"></div>
            </div>
            <asp:Label ID="pnlErrorLoad_Label" runat="server" Visible="false" Font-Bold="True"></asp:Label>
        </div>
      </div>
            <!-- browse archivo -->
            <div class="form-group col-xs-12 col-md-12" runat="server" id="pnlBrowseFile" visible="true">
                <div class="panel panel-success">
                    <div class="panel-heading">Archivo de excel de puntos de carga</div>
                    <div class="panel-body">
                        <div runat="server" id="ErrorMsg"></div>
                        <span class="help-block">
                            <%-- <h4>ARCHIVO DE EXCEL PUNTO DE CARGA</h4>--%>
                        </span>
                        <div class="form-group">
                            <%-- <label class="control-label col-sm-10">                    </label>    --%>
                            <div class="col-sm-10">
                                <asp:FileUpload ID="FileUpload1" runat="server" accept=".xls, .xlsx" class="btn btn-success btnbrowser col-sm-10" />
                                <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                                    ControlToValidate="FileUpload1"
                                    ErrorMessage="Sólo se permiten archivos .xlsx ó .xls"
                                    ValidationExpression="(.*?)\.(xlsx|xls|XLSX|XLS)$">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Leer Archivo" OnClick="btnUpload_Click" Enabled="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <%--        <div class="col-xs-6 col-md-8">
                <!--<h4>ARCHIVO DE EXCEL PUNTO DE CARGA</h4>
                <div class="input-group">
                    <label class="input-group-btn">
                        <span class="btn btn-primary btnbrowser">
                            Browse&hellip; <input type="file" style="display: none;" multiple>
                        </span>
                    </label>
                    <input type="text" class="form-control" readonly>
                </div>
                -->
                    <div class="clearfix"></div>            
    </div>--%>

    <div class="clearfix"></div>

    <div class="form-group col-xs-12 col-md-12">

        <div class="panel panel-success" id="pnlFileLoad" runat="server" visible="true">
            <div class="panel-heading">
                Archivo leído: 
                <asp:Label ID="lblFileLoad" runat="server" Visible="true" Font-Bold="True"></asp:Label>
            </div>
        </div>

        <div class="clearfix"></div>

        <!-- MUESTRA LOS DATOS DEL ARCHIVO DE EXCEL -->
        <div class="panel panel-success" id="pnlMuestraDatos" runat="server" visible="true">
            <div class="panel-body">
                <div id="" class="col-md-12">
                    <div class="table-responsive">

                        <table id="mytable" class="table table-hover table-striped ">
                            <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>
                        </table>

                    </div>
                </div>
            </div>
        </div>

    </div>

    <div class="form-group col-xs-12 col-md-12">
        <!-- Paginacion -->
        <div class="col-xd-12 text-center">
            <ul class="pagination pull-right" id="myPager"></ul>
        </div>
    </div>

    <div class="form-group col-xs-12 col-md-12">
        <div class="panel panel-success" id="pnlSuccessLoad" runat="server" visible="true">
            <div class="panel-heading">Mensaje:</div>
            <div class="panel-body">
                <div runat="server" id="Div2" visible="true">
                    <strong>Sin permisos para esta sección.
                    <br />
                    </strong>
                </div>
                <asp:Label ID="Label1" runat="server" Visible="true" Font-Bold="True"></asp:Label>
            </div>

        </div>
    </div>
    <!-- Botones -->
    <div class="col-xs-6 col-md-2">
        <!-- Indicates a successful or positive action 
            <button type="button" class="btn btn-success btn-lg btn-block">Guardar</button>-->
      <%--  <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-sm btn-block" OnClientClick="Confirmar()" Text="Guardar"  Enabled="false" />
      --%> 
        <input id="Button1" runat="server" type="button" class="btn btn-success btn-sm btn-block" onclick="Confirmar()"  value="Guardar" />
        <asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label><br />
    </div>
    
    <div class="col-xs-6 col-md-2">
        <!-- Indicates a successful or positive action -->
        <%--<button type="button" class="btn btn-danger btn-sm btn-block"  ID="" runat="server" Enabled="false">Cancelar</button>--%>
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm btn-block" Text="Cancelar" Enabled="false" OnClick="btncancel_Click" />
    </div>
       <div class="col-xs-6 col-md-2">
        <!-- Indicates a successful or positive action -->
        <%--<button type="button" class="btn btn-danger btn-sm btn-block"  ID="" runat="server" Enabled="false">Cancelar</button>--%>
       <%-- <asp:Button ID="Button22" runat="server" CssClass="btn btn-primary btn-sm btn-block" Text="Actualizar último convenio" Enabled="false" Visible="true" OnClick="btncancel_Click" />
        --%>
        <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-primary btn-sm btn-block" Text="Regesar" Enabled="false" Visible="false" OnClick="btncancel_Click" />
    </div>
 
    <script type="text/javascript">
        function Confirmar() {
            var ddlReport = document.getElementById("<%=cmbConvenio.ClientID%>");

            var Text = ddlReport.options[ddlReport.selectedIndex].text;
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            console.log(Text);
            if (Text == "Nuevo Convenio") {
                swal({
                    title: '¿Está seguro de continuar?',
                    text: 'Se generará un nuevo convenio a la central seleccionada', type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    cancelButtonText: 'Cancelar',
                    confirmButtonText: 'Continuar'
                }).then(function () {
                    llamarServidor();
                });
            } else {
                swal({
                    title: '¿Está seguro de continuar?',
                    text: 'Se actualizará el convenio seleccionado', type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    cancelButtonText: 'Cancelar',
                    confirmButtonText: 'Continuar'
                }).then(function () {
                    llamarServidor();
                });
            }
           
        }
        function llamarServidor() {

            __doPostBack("MiFuncion", "");

        }
            //var confirm_value = document.createElement("INPUT");
            //confirm_value.type = "hidden";
            //confirm_value.name = "confirm_value";
            //if (confirm("¿Confirmar carga de archivo para la central selecionada?")) {
            //    confirm_value.value = "Yes";
            //} else {
            //    confirm_value.value = "No";
            //}
            //document.forms[0].appendChild(confirm_value);

    </script>

    <script>
        $.fn.pageMe = function (opts) {
            var $this = this,
                defaults = {
                    perPage: 10,
                    showPrevNext: false,
                    hidePageNumbers: false
                },
                settings = $.extend(defaults, opts);

            var listElement = $this;
            var perPage = settings.perPage;
            var children = listElement.children();
            var pager = $('.pager');

            if (typeof settings.childSelector != "undefined") {
                children = listElement.find(settings.childSelector);
            }

            if (typeof settings.pagerSelector != "undefined") {
                pager = $(settings.pagerSelector);
            }

            var numItems = children.size();
            var numPages = Math.ceil(numItems / perPage);

            pager.data("curr", 0);

            if (settings.showPrevNext) {
                $('<li><a href="#" class="prev_link btn-info">«</a></li>').appendTo(pager);
            }

            var curr = 0;
            while (numPages > curr && (settings.hidePageNumbers == false)) {
                $('<li><a href="#" class="page_link">' + (curr + 1) + '</a></li>').appendTo(pager);
                curr++;
            }

            if (settings.showPrevNext) {
                $('<li><a href="#" class="next_link">»</a></li>').appendTo(pager);
            }

            pager.find('.page_link:first').addClass('active');
            pager.find('.prev_link').hide();
            if (numPages <= 1) {
                pager.find('.next_link').hide();
            }
            pager.children().eq(1).addClass("active");

            children.hide();
            children.slice(0, perPage).show();

            pager.find('li .page_link').click(function () {
                var clickedPage = $(this).html().valueOf() - 1;
                goTo(clickedPage, perPage);
                return false;
            });
            pager.find('li .prev_link').click(function () {
                previous();
                return false;
            });
            pager.find('li .next_link').click(function () {
                next();
                return false;
            });

            function previous() {
                var goToPage = parseInt(pager.data("curr")) - 1;
                goTo(goToPage);
            }

            function next() {
                goToPage = parseInt(pager.data("curr")) + 1;
                goTo(goToPage);
            }

            function goTo(page) {
                var startAt = page * perPage,
                    endOn = startAt + perPage;

                children.css('display', 'none').slice(startAt, endOn).show();

                if (page >= 1) {
                    pager.find('.prev_link').show();
                }
                else {
                    pager.find('.prev_link').hide();
                }

                if (page < (numPages - 1)) {
                    pager.find('.next_link').show();
                }
                else {
                    pager.find('.next_link').hide();
                }

                pager.data("curr", page);
                pager.children().removeClass("active");
                pager.children().eq(page + 1).addClass("active");

            }
        };

        $(document).ready(function () {

            $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 10 });

        });

        $("#resultado").hide();
        $(function () {

            // We can attach the `fileselect` event to all file inputs on the page
            $(document).on('change', ':file', function () {
                var input = $(this),
                    numFiles = input.get(0).files ? input.get(0).files.length : 1,
                    label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
                input.trigger('fileselect', [numFiles, label]);

                //$("#btnUpload").prop("disabled", false);
                //alert('1');
                //$("#btnUpload").attr('disabled', false);
                //alert('2');
                //$("#btnUpload").removeAttr("disabled");
                //alert('3');
                //$("#btnUpload").prop("disabled", true);
            });

            // We can watch for our custom `fileselect` event like this
            $(document).ready(function () {
                $(':file').on('fileselect', function (event, numFiles, label) {

                    var input = $(this).parents('.input-group').find(':text'),
                        log = numFiles > 1 ? numFiles + ' files selected' : label;

                    if (input.length) {
                        input.val(log);
                        $("#resultado").show();
                        //alert('ok1');
                    } else {
                        //if (log) alert(log);
                        //alert('ok2');
                    }

                });
            });
            $(function () {
                $('[data-toggle="tooltip"]').tooltip()
                //alert('ok1');
            })
        });
    </script>

</asp:Content>
