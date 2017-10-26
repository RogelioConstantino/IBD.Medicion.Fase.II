<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="convenios.aspx.cs" Inherits="Medicion.convenios" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" OnLoad="UpdatePanel1_Load">


          <ContentTemplate>--%>

    <ol class="breadcrumb breadcrumb-verde" style="color: #454545;">
        <li>Convenios de Transmisión</li>
        <li>Gestión de Convenios</li>
    </ol>

    <h4 style="text-align: center; color: #427314">Gestión de Convenios de Transmisión</h4>

    <div class="clearfix"></div>
    <div class="panel panel-success">
        <div class="panel-body">

            <div class="form-group col-xs-12 col-md-4">
                <label class="control-label">Central</label>
                <div class="selectContainer">
                    <asp:DropDownList ID="cmbCentral" runat="server" CssClass="form-control">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group col-xs-12 col-xs-6">

                <div class="alert alert-success alert-dismissible" id="msgExito" runat="server" name="msgExito" role="alert">
                    <button type="button" class="close" id="btnCancel2" runat="server" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    Información actualizada correctamente</div>
                <div class="alert alert-danger alert-dismissible" id="msgErrNew" runat="server" name="msgErrNew" role="alert">
                    <button type="button" id="btnCancel1" runat="server" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>Error:</strong> El campo Descripción de convenio no debe estar vacío</div>
            </div>

            <div class="form-group col-xs-12 col-xs-2 text-right">
                <br />
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Buscar" OnClick="btnSearch_Click" />
            </div>

        </div>
    </div>

    <div class="clearfix"></div>

    <div class="form-group  col-xs-12 col-md-12">

        <div id="Div1" class="table-responsive" runat="server">
            <table id="mytable" class="table table-striped table-hover ">
            </table>
        </div>

        <div class="row ">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 center-block">
                <asp:GridView ID="gvConvenios" runat="server" Width="90%" AllowPaging="true" OnPageIndexChanging="gvConvenios_PageIndexChanging" BorderStyle="None" BorderColor="White" CssClass="table  table-striped table-hover " ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" DataKeyNames="Id" PageSize="4">
                    <FooterStyle BorderStyle="None" />
                    <PagerSettings Mode="Numeric" PageButtonCount="4" FirstPageText="Primera Pagina" LastPageText="Última página" NextPageText="Siguiente página" PreviousPageText="Anterior página" />
                    <%--Paginador...--%>
                    <HeaderStyle HorizontalAlign="Center" BorderStyle="None" />
                    <Columns>
                        <asp:BoundField HeaderText="Convenio" DataField="Convenio" />
                        <asp:BoundField HeaderText="Descripción" DataField="Descripción" />
                        <asp:BoundField HeaderText="Estatus" DataField="Estatus" />
                        <asp:BoundField HeaderText="Fecha Creación" DataField="Fecha de Creación" />
                        <asp:TemplateField>

                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/images/edit.png"  Width="25px" Height="25px" ImageAlign="Middle"
                                    CausesValidation="false" OnClick="btnEliminar_Click" />
                            </ItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>


        <div class="clearfix"></div>

        <div class="col-md-12 text-center">
            <ul class="pagination pull-right" id="myPager"></ul>
        </div>

    </div>

    <!-- EDIT -->
    <div class="modal fade" id="edit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button" id="btnCloseEditx" class="close" data-dismiss="modal">
                        <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title text-center " id="H1">Editar Convenios de Transmisión</h4>
                </div>
                <div class="modal-body" id="edit-modal-body">

                    <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdId"></asp:HiddenField>

                    <div class="form-group col-xs-12 col-md-6">
                        <label for="txtConvenio">Convenio</label>
                        <asp:TextBox ClientIDMode="Static" runat="server" ID="txtConvenio" name="txtConvenio" CssClass="form-control text-uppercase" MaxLength="6" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-xs-12 col-md-6">
                        <label for="txtDescripcion">Descripción</label>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDescripcion" CssClass="form-control text-uppercase" MaxLength="49"></asp:TextBox>
                    </div>

                    <div class="form-group col-xs-12 col-md-8">
                        <label class="control-label">Estatus</label>
                        <asp:DropDownList ID="ddl_Estatus" ClientIDMode="Static" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <%-- <cc1:CascadingDropDown ID="cddl_Estatus" TargetControlID="ddl_Estatus" PromptText="Seleciona un Estatsus"
                        PromptValue="" ServicePath="WebService/wsConvenio.asmx" ServiceMethod="getConvenioEstatus" runat="server"
                        Category="Id" LoadingText="Cargando..." /> --%>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseEdit">Cancelar</button>
                    <%--<asp:Button runat="server" id="btnAceptarEdit" class="btn btn-success glyphicon-floppy-save"  OnClick="btnEditAceptar_Click" Text="Guardar" />                        --%>
                    <%-- <button type="button" id="EditConvenio" class="btn btn-primary" data-dismiss="modal" onclick="" >Actualizar</button>
                    --%>
                    <asp:Button ID="Button1" CssClass="btn btn-primary" OnClick="Button1_Click" runat="server" Text="Actualizar" />
                    <%-- <asp:Button ID="EditConvenio"   runat="server" Text="Actualizar"></asp:Button> --%>
                </div>
                <div class="clearfix"></div>

            </div>
        </div>
    </div>

    <%-- </ContentTemplate>
          <Triggers>
             <asp:AsyncPostBackTrigger ControlID="EditConvenio" EventName="Click" />
          </Triggers>

     </asp:UpdatePanel>--%>
    <!-- end modal EDIT -->

    <script src="js/Business/pagination.js" type="text/javascript"></script>

    <script type="text/javascript">

        function CerrarError() {
            $("#msgExito").hide();
            $("#msgErrNew").hide();
        }
        function mostrarModal(nombreModal) {
            $("#" + nombreModal).modal({
                show: true,
                backdrop: 'static',
                keyboard: false
            });
            $("#msgExito").hide();

        }
    </script>
    <script type="text/javascript">

               $(document).ready(function () {


                   $("#btnCloseEdit").click(function () {
                       $("#btnCloseEditx").click();
                   });

                   $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 6 });

                   /// EDIT Division

                   function EditarConvenio() {


                   }
                   //$('#EditConvenio').click(function () {

                   //    var Id = $("#hdId").val();
                   //    var IdEstatus = $("#ddl_Estatus").val();
                   //    var Descripcion = $("#txtDescripcion").val();

                   //    if (!IdEstatus ) {
                   //        $("#msgExito").html("Se debe selecionar un Estatus");
                   //        $("#msgExito").addClass("alert alert-danger text-center");
                   //        $("#msgExito").show();
                   //        return false;
                   //    }
                   //    if ((!Descripcion && Descripcion.length == 0)) {
                   //        $("#msgExito").html("Falta Capturar la Descipción");                     
                   //        $("#msgExito").addClass("alert alert-danger text-center");
                   //        $("#msgExito").show();
                   //        return false;
                   //    }

                   //    $.ajax({
                   //        type: "POST",
                   //        url: "./WebService/wsConvenio.asmx/Edit",
                   //        data: "{ strID:'" + Id + "', strIdEstatus:'" + IdEstatus + "', strDescripcion:'" + Descripcion + "'}",
                   //        contentType: "application/json; charset=utf-8",
                   //        dataType: "json",
                   //        success: function (r) {
                   //            var sResp = r.d;
                   //            var aResp = sResp.split('-')
                   //            if (aResp[0] == '1') {

                   //                $("#msgExito").removeAttr("style");
                   //                $("#msgExito").html("<strong>" + aResp[1] + "</strong> .");
                   //                $("#msgExito").addClass("alert alert-success text-center");
                   //                $("#msgExito").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                   //                $("#msgExito").show();


                   //                setTimeout(function () {
                   //                    window.location.reload(1);

                   //                }, 2000);  

                   //            }
                   //            else {
                   //                $("#msgExito").html(aResp[1]);
                   //                $("#msgExito").addClass("alert alert-danger  text-center");
                   //                $("#msgExito").show();
                   //            }

                   //            //var dato = $("#btnSearch");
                   //            //if (dato) {
                   //            //    dato.click();
                   //            //}

                   //        },
                   //        error: function (r) {
                   //            $("#msgExito").html(r.responseText);
                   //            $("#msgExito").addClass("alert alert-danger");
                   //            $("#msgExito").show();
                   //        },
                   //        failure: function (r) {
                   //            //alert(r.responseText);
                   //            $("#msgExito").html(r.responseText);
                   //            $("#msgExito").addClass("alert alert-danger");
                   //            $("#msgExito").show();
                   //        }
                   //    });



                   //    function ActualizarGrid() {
                   //        $.ajax({
                   //            type: "POST",
                   //            url: "convenios.aspx/Actualizar",
                   //            data: "",
                   //            contentType: "application/json; charset=utf-8",
                   //            dataType: "json",
                   //            //success: function (datos) {
                   //            //    alert(datos.d);
                   //            //}
                   //        }); 
                   //    }

                   //    return false;
                   //});
                   // END EDIT

               });

    </script>

    <%--  <script src="js/Business/convenios.js" type="text/javascript"></script>--%>
</asp:Content>
