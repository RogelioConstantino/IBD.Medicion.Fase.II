<%@ Page Title="Iberdrola-Medición-Inicio de sessión" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Medicion._Default" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
	<title>Inicio de sesión- Sistema de Gestión de Medición - Iberdrola fnirnfioer</title>
	<meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
      
        <link rel="icon" href="favicon.ico" />
        <link rel="shortcut icon" href="favicon.ico"/>

    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <link href="css/sweetalert2.min.css" rel="stylesheet" />
    <script src="js/sweetalert2.min.js" type="text/javascript"></script>
    <script src="js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/sweetalert2.common.js" type="text/javascript"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="Styles/bootstrap.min.css">
    <link rel="stylesheet" href="Styles/style.css" >

    <link href="css/bootstrap-glyphicons.css" rel="stylesheet"/>
		
   <script src="js/typeahead.boundle.js"></script>
</head>
<body class="full">
	<form id="form1" runat="server">
		<div class="container" >
    		<div class="col-md-12 col-xs-12 centered">
	            <div class="panel-heading">
                    <h2 class="text-center">Sistema de Gestión de Medición----</h2>                
	                <h4 class="text-center">LA NUEVA ENERGÍA DE MÉXICO</h4>
	                <p class="text-center">Iniciar sesión</p>
	            </div>
            	<div class="panel-body">

                    <div class="form-group">                        
                        <div class="input-group col-sm-offset-4 col-sm-4">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:TextBox  ID="txtUsrEmail" runat="server" cssClass="form-control" ToolTip="Usuario" placeholder="Usuario" ></asp:TextBox>
                        </div>                        
                    </div>                    
                    <div class="form-group">                                            
                        <div class="input-group col-sm-offset-4 col-sm-4">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                            <asp:TextBox ID="txtPassword" runat="server" cssClass="form-control " ToolTip="Contraseña" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                        </div>                        
                    </div>                                                                  
                    <div class="form-group">        
                      <div class="input-group col-sm-offset-7 col-sm-2">
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-lg btn-success " ToolTip="Entrar" onclick="Button1_Click" Text="Entrar" />
                      </div>
                    </div>
                    
                    <div class="clearfix"></div>
                    <br />
                    <div class="form-group">        
                        <div class="col-sm-offset-4 col-sm-4">
    					    <div runat="server" id="ErrorMsg"  class="alert-danger" ></div>
                        </div>
                    </div>

    	        </div>
	        </div>
        </div>
    </form>
    
    <script>
        //$("#ErrorMsg").hide();
    </script>

</body>
</html>
