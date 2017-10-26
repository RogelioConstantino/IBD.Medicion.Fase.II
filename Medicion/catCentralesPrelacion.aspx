<%@ Page Title="Prelación de Centrales" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="catCentralesPrelacion.aspx.cs" Inherits="Medicion.catCentralesPrelacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceHolder1" runat="server">

        <style type="text/css">
                body
                {
                    font-family: Arial;
                    font-size: 11pt;
                }
                table
                {
                    border: 0px solid #ccc;
                    border-collapse: collapse;
                }
                table th
                {
                    background-color: white;
                    color: #333;
                    font-weight: bold;
                    border-bottom:2px solid #d4d4d4;
                    border-top:0px;
                    border-left:0px;
                    border-right:0px;
                }
                
                table td
                {
                    padding: 5px;
                    border: 0px solid #ccc;
                    border-bottom:1px solid #dddddd;
                    border-Top:1px solid #dddddd;
                }
                .selected
                {
                    background-color: #666;
                    color: #fff;
                }
    </style>

    <ol class="breadcrumb breadcrumb-verde" style="color:#454545;">
      <li>Catálogos</li>
      <li>Centrales</li>
        <li>Prelación</li>
    </ol>
    
    <h4  style=" text-align:center; color:#427314"> Prelación de Centrales</h4>
    <center>
    <asp:GridView ID="gvLocations" runat="server" AutoGenerateColumns="false" Width="90%"  AlternatingRowStyle-BackColor="#f9f9f9" HeaderStyle-BackColor="White"   SelectedRowStyle-BackColor="#f5f5f5" RowStyle-Height="40px" >
        <Columns>
            <asp:TemplateField HeaderText="Cve" ItemStyle-Width="30">
                <ItemTemplate>
                    <%# Eval("Código") %>
                    <input type="hidden" name="Código" value='<%# Eval("Código") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Descripción" HeaderText="Central" ItemStyle-Width="350" />
            <asp:BoundField DataField="OrdenPre" HeaderText="Prelación" ItemStyle-Width="100" />
        </Columns>
    </asp:GridView>
        </center>
    <br />    

    <div class="form-group col-xs-12 col-xs-12" >  
       
            <asp:Label ID="lblMsg" runat="server" > </asp:Label> 
       
    </div>

    <div class="form-group col-xs-12 col-xs-6" style="margin-top:15px;">  
        </div>
    

    <div class="form-group col-xs-12 col-xs-4" style="margin-top:15px;">  
    <asp:Button ID="btnUpdate"  runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Actualizar Prelación" OnClick="Unnamed_Click" /> 
    </div>  
<div class="form-group col-xs-12 col-xs-2" style="margin-top:15px;">  
        </div>

   <script type="text/javascript">
        $(function () {
            $("[id*=gvLocations]").sortable({
                items: 'tr:not(tr:first-child)',
                cursor: 'pointer',
                axis: 'y',
                dropOnEmpty: false,
                start: function (e, ui) {
                    ui.item.addClass("selected");
                },
                stop: function (e, ui) {
                    ui.item.removeClass("selected");
                },
                receive: function (e, ui) {
                    $(this).find("tbody").append(ui.item);
                }
            });
        });
    </script>

</asp:Content>
