<%@ Page Title="Catálogo de Centrales" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catCentrales.aspx.cs" Inherits="Medicion.Centrales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>


    <style type="text/css">
        .ShowHideButton {
            border: solid 1px #404040;
            font-family: Courier New;
            background-color: #808080;
            cursor:move;
        }
    </style>
    <script type="text/javascript">
        function ToggleVisiblity(ShowHideButton, GridPanel) {
            var GridPanel = self.document.getElementById(GridPanel);
            if (GridPanel.style.display == 'none') {
                ShowHideButton.innerText = '-';
                GridPanel.style.display = 'block';
            }
            else {
                ShowHideButton.innerText = '+';
                GridPanel.style.display = 'none';
            }
        }

    </script>


  <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <ol class="breadcrumb breadcrumb-verde" style="color: #454545;">
        <li>Catálogos</li>
        <li>Centrales</li>
    </ol>

    <h4 style="text-align: center; color: #427314">Catálogo de Centrales</h4>

    <div class="col-xs-12 col-md-12 text-right">
        <p>
            <button type="button" class="btn btn-warning" onclick="limpiar()" data-toggle="modal" data-placement="top" title="Nuevo" data-target="#NewCentralModal">
                Agregar &nbsp; &nbsp;<span class='glyphicon glyphicon-plus'></span>
            </button>
        </p>
    </div>

    <div class="clearfix"></div>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>



        

    <div class="col-xs-12 col-xd-12">
        <div class="table-responsive" runat="server">
            <table id="mytable" class="table table-striped table-hover table-condensed">
                <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>

            </table>
               <asp:GridView runat="server" ID="GridView1"  CssClass="table  table-striped table-hover " OnRowDataBound="GridView1_RowDataBound" BorderStyle="None"  BorderColor="White" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" DataKeyNames="IdCentral">
                  <Columns >
                      <asp:TemplateField HeaderText="Central">
                          <ItemTemplate>
                              <a href="#">
                              <asp:Label runat="server" ID="btnShowHide" Text="+"
                                  CssClass="" />&nbsp;</a>
                              
                    <asp:Label runat="server" ID="lblName" Font-Bold="true"
                        Text='<%# DataBinder.Eval(Container.DataItem, "Código")%>'>
                    </asp:Label><br>

                              <asp:Panel runat="server" ID="pnlGrid" CssClass="" Style="display: none;width:100%; margin-left: 5px;">
                                  <asp:GridView runat="server" ID="GridView2" CssClass="table  table-striped table-hover " OnRowDataBound="GridView2_RowDataBound" BorderColor="White"  AutoGenerateColumns="false" DataKeyNames="IdConvenio">
                                        <Columns >
                      <asp:TemplateField HeaderText="Convenio">
                          <ItemTemplate>
                                 <a href="#">
                              <asp:Label runat="server" ID="btnShowHide2" Text="+"
                                  CssClass="" />&nbsp;</a>
                    <asp:Label runat="server" ID="lblName2" Font-Bold="true"
                        Text='<%# DataBinder.Eval(Container.DataItem, "Convenio")%>'>
                    </asp:Label><br>

                              <asp:Panel runat="server" ID="pnlGrid2" CssClas="panel " Style="display: none;width:100%; margin-left: 5px;">
                                  <asp:GridView runat="server" ID="GridView3" CssClass="table  table-striped table-hover " BorderStyle="None" BorderColor="White" AutoGenerateColumns="true">
                                  </asp:GridView>
                              </asp:Panel>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
                                      <Columns>
                      <asp:BoundField DataField="Num de Cargas" HeaderText="Num de Cargas"  Visible="true"/>
                  </Columns>
                                           <Columns>
                      <asp:BoundField DataField="Carga total" HeaderText="Carga total"  Visible="true"/>
                  </Columns>
                                        <Columns>
                      <asp:BoundField DataField="Estatus" HeaderText="Estatus"  Visible="true"/>
                  </Columns>
                                  </asp:GridView>
                              </asp:Panel>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
                  <Columns>
                      <asp:BoundField DataField="carga" HeaderText="Carga"  Visible="true"/>
                  </Columns>
                 <Columns>
                      <asp:TemplateField HeaderText="Editar">

                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" OnClick="btnEditar_Click" runat="server" ImageUrl="~/images/edit.png"  Width="25px" Height="25px" ImageAlign="Middle"
                                    CausesValidation="false"  />
                              
                            </ItemTemplate>
                      
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="Eliminar">

                            <ItemTemplate >
                                <asp:ImageButton ID="btnEliminar" OnClick="btnEliminar_Click" runat="server" ImageUrl="~/images/wswsw.png" Width="25px" Height="25px" ImageAlign="Middle"
                                    CausesValidation="false"  />
                              
                            </ItemTemplate>
                      
                        </asp:TemplateField>
                 </Columns>

              </asp:GridView>
        </div>



    </div>

         <%--   </ContentTemplate>

    </asp:UpdatePanel>--%>

    <div class="clearfix"></div>

    <div class="col-md-12 text-center">
        <ul class="pagination pull-right" id="myPager"></ul>
    </div>





    </div>
   
           
    <!---- EDIT --->
    <div class="modal fade bs-example-modal-sm" id="edit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header btn-primary">
                    <button type="button" id="btnCloseEditx" class="close" data-dismiss="modal">
                        <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>
                    </button>
                    <h4 class="modal-title text-center " id="H1">Editar Central</h4>
                </div>
                <div class="modal-body" id="edit-modal-body">
                    <form id="foma1">
                       <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdIdEdit"></asp:HiddenField>

                    <div class="form-group col-xs-12 col-md-12">
                        <label for="txtConvenio">Central</label>
                        <asp:TextBox ClientIDMode="Static" runat="server" ID="txtConvenioEdit" name="txtConvenio" CssClass="form-control text-uppercase" MaxLength="6" ReadOnly="false"></asp:TextBox>
                    </div>
                    <div class="form-group col-xs-12 col-md-12">
                        <label for="txtDescripcion">Descripción</label>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDescripcionEdit" CssClass="form-control text-uppercase" MaxLength="49"></asp:TextBox>
                    </div>
                 </form>
                </div>

                <div class="clearfix"></div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseEdit">Cancelar</button>
                    <button type="button" id="Aceptar" class="btn btn-success" data-dismiss="modal">Actualizar</button>
                </div>

                <div class="clearfix"></div>

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
                    <button type="button" id="btnCloseDeletex" class="close" data-dismiss="modal">
                        <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title text-center " id="H2">¿Eliminar ésta Central?</h4>

                </div>
                <div class="modal-body" id="delete-modal-body">
                      <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdIddelet"></asp:HiddenField>

                    <div class="form-group col-xs-12 col-md-12">
                        <label for="txtConvenio">Central</label>
                        <asp:TextBox ClientIDMode="Static" runat="server" ID="txtConveniodelet" name="txtConveniodelet" CssClass="form-control text-uppercase" MaxLength="6" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-xs-12 col-md-12">
                        <label for="txtDescripcion">Descripción</label>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtdescripciondelet" CssClass="form-control text-uppercase" MaxLength="49" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" id="btnCloseDelete">Cancelar</button>
                    <button type="button" id="btnDelete" class="btn btn-danger" data-dismiss="modal">Eliminar</button>
                </div>
                <div class="clearfix"></div>
<%--                <div class="alert alert-success" role="alert" id="msgdeletealter">
                </div>--%>
            </div>
        </div>
    </div>
    <!--- end delete modal -->

    <!--- New Central modal -->
    <div class="modal fade" id="NewCentralModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header text-center" style="background-color: #A4BA08; color: white !important; text-align: center;">
                    <button type="button" id="btnCloseNewx" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="Central_add">Agregar Nueva Central</h4>
                </div>
                <div class="modal-body ">
                    <label for="NewCodeCentraltxt">Código</label>
                    <input type="text" class="form-control text-uppercase" id="NewCodeCentraltxt" maxlength="15" />
                </div>
                <div class="modal-body">
                    <label for="NewCentraltxt">Descripción</label>
                    <input type="text" class="form-control text-uppercase" id="NewCentraltxt" maxlength="149" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" id="btnCloseNew">Cerrar</button>
                    <a href="#" class="btn btn-success" id="btnAddCentral">Agregar</a>
                </div>
                <div class="clearfix"></div>
                <div class="alert alert-success" role="alert" id="msgErrNewCentral">
                </div>
            </div>
        </div>
    </div>
    <!--- end New modal -->

    <script type="text/javascript">


        function mostrarModal(nombreModal) {
            $("#" + nombreModal).modal({
                show: true,
                backdrop: 'static',
                keyboard: false
            });
            $("#msgExito").hide();

        }
        function ocultarModal(nombreModal) {
            $("#" + nombreModal).modal('hide');

            $("#" + nombreModal).modal({
                hide: true,
            });
        }
        function limpiar() {
            document.getElementById("NewCodeCentraltxt").value = "";
            document.getElementById("NewCentraltxt").value = "";
        }
        $("#msgExito").hide();
        $("#msgdeletealter").hide();
        $("#msgErrNewCentral").hide();
        //$('[data-toggle="tooltip"]').tooltip();

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

            $('#myTable').pageMe({ pagerSelector: '#myPager', showPrevNext: true, hidePageNumbers: false, perPage: 6 });

            $("#btnAddCentral").click(function () {
                //var New_Central_val = $("#NewCentraltxt").val().trim();
                var New_Central_val = $("#NewCentraltxt").val();
                var CodeCentral = $("#NewCodeCentraltxt").val();

                if ((!CodeCentral && CodeCentral.length == 0)) {
                    //$("#msgErrNewCentral").html("Falta agregar el código de la central");
                    //$("#msgErrNewCentral").addClass("alert alert-danger text-center");
                    //$("#msgErrNewCentral").show();
                    swal(
                        '',
                        'Falta agregar el código de la central',
                        'warning'
                    );
                    return false;
                }
                var str = $('#Search').val();
                //if (/^[a-zA-Z0-9- ]*$/.test(CodeCentral) == false) {
                //    $("#msgErrNewCentral").html("No puedes agregar characteres especiales el el código de la central");
                //    $("#msgErrNewCentral").addClass("alert alert-danger text-center");
                //    $("#msgErrNewCentral").show();

                //    return false;
                //}

                if ((!New_Central_val && New_Central_val.length == 0)) {
                    //$("#msgErrNewCentral").html("Falta agregar la descripción de la central");
                    //$("#msgErrNewCentral").addClass("alert alert-danger text-center");
                    //$("#msgErrNewCentral").show();
                    swal(
                        '',
                        'Falta agregar la descripción de la central',
                        'warning'
                    );
                    return false;
                }
                var str = $('#Search').val();
                //if (/^[a-zA-Z0-9- ]*$/.test(New_Central_val) == false) {
                //    $("#msgErrNewCentral").html("No puedes agregar characteres especiales en la descripción de la central.");
                //    $("#msgErrNewCentral").addClass("alert alert-danger text-center");
                //    $("#msgErrNewCentral").show();
                //    return false;
                //}

                //alert("{ strNewCentral: " + New_Central_val + "}");
                $.ajax({
                    type: "POST",
                    url: "./WebService/wsCentral.asmx/NewCentral",
                    data: "{  CodeCentral: '" + CodeCentral + "', Central: '" + New_Central_val + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {

                        var sResp = r.d;
                        var aResp = sResp.split('-')

                        if (aResp[0] == '1') {
                            //$("#msgErrNewCentral").removeAttr("style");
                            //$("#msgErrNewCentral").html("<strong>" + aResp[1] + "</strong> .");
                            //$("#msgErrNewCentral").addClass("alert alert-success text-center");
                            //$("#msgErrNewCentral").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                            //$("#msgErrNewCentral").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'success'
                            );
                            setTimeout(function () {
                                window.location.reload(1);
                            }, 4000);
                        }
                        else {
                            //$("#msgErrNewCentral").html(aResp[1]);
                            //$("#msgErrNewCentral").addClass("alert alert-danger  text-center");
                            //$("#msgErrNewCentral").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'error'
                            );

                        }


                    },
                    error: function (r) {
                        //$("#msgErrNewCentral").html(r.responseText);
                        //$("#msgErrNewCentral").addClass("alert alert-danger");
                        //$("#msgErrNewCentral").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    },
                    failure: function (r) {
                        //alert(r.responseText);
                        //$("#msgErrNewCentral").html(r.responseText);
                        //$("#msgErrNewCentral").addClass("alert alert-danger");
                        //$("#msgErrNewCentral").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    }
                });
                return false;
            });
            $("#btnDelete").click(function () {

                var IdCentral_delete = $("#hdIddelet").val();
                var Central = $("#txtConveniodelet").val();
                if ((!IdCentral_delete && IdCentral_delete.length == 0)) {
                    //$("#msgdeletealter").html("Falta agregar el ID Central");
                    //$("#msgdeletealter").addClass("alert alert-danger text-center");
                    //$("#msgdeletealter").show();
                    swal(
                        '',
                        'Falta agregar el ID Central',
                        'warning'
                    );
                    return false;
                }
                swal({
                    title: '¿Está seguro de eliminar la central?',
                    text: Central,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#5C881A',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Aceptar'
                }).then(function () {
                    $.ajax({
                        type: "POST",
                        url: "./WebService/wsCentral.asmx/DeleteCentral",
                        data: "{ IdCentral_delete: " + IdCentral_delete + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {

                            //$("#msgdeletealter").removeAttr("style");
                            //$("#msgdeletealter").html("<strong>" + r.d + "</strong> .");
                            //$("#msgdeletealter").addClass("alert alert-success text-center");
                            //$("#msgdeletealter").removeClass("alert alert-danger text-center").addClass("alert alert-success text-center");
                            //$("#msgdeletealter").show();
                            swal(
                                '',
                                '<strong>' + r.d + '</strong>',
                                'success'
                            );
                            $("#delete").modal('hide');

                            setTimeout(function () {
                                location.reload("catCentrales.aspx");
                            }, 2000);
                        },
                        error: function (r) {

                            //$("#msgdeletealter").html(r.responseText);
                            //$("#msgdeletealter").addClass("alert alert-danger");
                            //$("#msgdeletealter").show();
                            swal(
                                '',
                                '<strong>' + r.responseText + '</strong>',
                                'error'
                            );
                        },
                        failure: function (r) {
                            //alert(r.responseText);
                            //$("#msgdeletealter").html(r.responseText);
                            //$("#msgdeletealter").addClass("alert alert-danger");
                            //$("#msgdeletealter").show();
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
            $('#Aceptar').click(function () {
                var IdCentral = $("#hdIdEdit").val();
                var Central = $("#txtDescripcionEdit").val();
                // Central = $.trim(Central);
                var CodCentral = $("#txtConvenioEdit").val();

                if ((!IdCentral && IdCentral.length == 0)) {
                    //$("#msgExito").html("Falta agregar el ID Central");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar el ID Central',
                        'warning'
                    );
                    return false;
                }
                if ((!CodCentral && CodCentral.length == 0)) {
                    //$("#msgExito").html("Falta agregar la Central");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar un código a la central',
                        'warning'
                    );
                    return false;
                }
                if ((!Central && Central.length == 0)) {
                    //$("#msgExito").html("Falta agregar la Central");
                    //$("#msgExito").addClass("alert alert-danger text-center");
                    //$("#msgExito").show();
                    swal(
                        '',
                        'Falta agregar una descripción a la central',
                        'warning'
                    );
                    return false;
                }
                //if (/^[a-zA-Z0-9- ]*$/.test(Central) == false) {
                //    $("#msgExito").html("No puedes agregar characteres especiales");
                //    $("#msgExito").addClass("alert alert-danger text-center");
                //    $("#msgExito").show();
                //    return false;
                //}
                $.ajax({
                    type: "POST",
                    url: "./WebService/wsCentral.asmx/UpdateCentral",
                    data: "{CodCentral:'" + CodCentral + "', Central: '" + Central + "', IdCentral: " + IdCentral + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {

                        var sResp = r.d;
                        var aResp = sResp.split('-')

                        if (aResp[0] == '1') {
                        
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'success'
                            );
                           
                                $("#edit").modal('hide');
                               // llamarServidor();
                               
                                setTimeout(function () {
                                    location.reload("catCentrales.aspx");

                                }, 2000);
                            //$("#edit").modal('hide');
                            //$("#edit").modal('hide');                                                   
                        }
                        else {

                            //$("#msgExito").html(aResp[1]);
                            //$("#msgExito").addClass("alert alert-danger  text-center");
                            //$("#msgExito").show();
                            swal(
                                '',
                                '<strong>' + aResp[1] + '</strong>',
                                'error'
                            );
                        }
                    },
                    error: function (r) {
                        //$("#msgExito").html(r.responseText);
                        //$("#msgExito").addClass("alert alert-danger");
                        //$("#msgExito").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    },
                    failure: function (r) {
                        //alert(r.responseText);
                        //$("#msgExito").html(r.responseText);
                        //$("#msgExito").addClass("alert alert-danger");
                        //$("#msgExito").show();
                        swal(
                            '',
                            '<strong>' + r.responseText + '</strong>',
                            'error'
                        );
                    }

                });
                
                return false;
            });
        });
        
    </script>


    <script src="js/Business/pagination.js" type="text/javascript"></script>
    <%--<script src="js/Business/Centrales.js" type="text/javascript"></script>--%>

</asp:Content>
