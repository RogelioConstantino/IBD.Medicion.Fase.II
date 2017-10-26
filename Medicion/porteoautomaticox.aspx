<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="porteoautomatico.aspx.cs" Inherits="Medicion.porteoautomatico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <ol class="breadcrumb breadcrumb-verde">
          <li><a href="#">PUNTOS DE CARGA</a></li>
          <li><a href="#">DESDE EXCEL</a></li>
        </ol>
		<div class=" text-right">   
			<a href="consultascargas.aspx" class="btn btn-success btn-sm" aria-label="Left Align" title="Medidores" data-toggle="tooltip" data-placement="top"  ><img src="img/smartmeter.png"></a>

		</div>
        <div class="clearfix"></div>
        <div class="col-xs-6 col-md-8">
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
            <div runat="server" id="ErrorMsg"></div>
            <span class="help-block">
                <h4>ARCHIVO DE EXCEL PUNTO DE CARGA</h4>
            </span>
            <div class="input-group">
                <label class="input-group-btn">
                    <span class="btn btn-primary btnbrowser">
                        <asp:FileUpload ID="FileUpload1" runat="server" />                                       
                    </span>
                    
                </label>
                <div class="col-xs-6 col-md-4">
                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Leer Archivo" OnClick="btnUpload_Click" />   
                </div>
            </div>
            
        </div>
        <p></p>
         
        <div class="clearfix"></div>
        <!-- MUESTRA LOS DATOS DEL ARCHIVO DE EXCEL -->
        <div id="" class="col-xs-12  col-md-12">
            <div class="table-responsive">
                <table id="mytable" class="table table-bordred table-striped">
                  <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
                </table>
                
            </div> <!-- table 2 responsive end -->
            <div class="clearfix"></div>
             <div class="col-md-12 text-center">
                    <ul class="pagination pull-right" id="myPager"></ul>
                </div>  
            <div class="col-xs-6 col-md-3">
                <!-- Indicates a successful or positive action 
                <button type="button" class="btn btn-success btn-lg btn-block">Guardar</button>-->
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-sm btn-block" Text="Guardar" OnClick="btnSave_Click" />   
                <asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label><br />
            </div>
            <div class="col-xs-6 col-md-2">
                <!-- Indicates a successful or positive action -->
                <button type="button" class="btn btn-danger btn-sm btn-block">Cancelar</button>
            </div>
           

        </div>

	<script type="text/javascript">
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
	                    //if (log) alert(log);
	                }

	            });
	        });
	        $(function () {
	            $('[data-toggle="tooltip"]').tooltip()
	        })
	    });
   </script>
</asp:Content>
