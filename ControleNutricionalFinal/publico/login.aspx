<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ControleNutricionalFinal.login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />
    <title>Login</title>

    <!-- CSS  -->
    <link href="css/materialize.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection" />
</head>
<body>

    <nav class=" teal darken-1" role="navigation">
        <div class="nav-wrapper container">
            <a id="logo-container" href="index.html" class="brand-logo">Home</a>
           
        </div>
    </nav>
    <div class="section no-pad-bot" id="index-banner">
        <div class="container">
            <br>
            <br>

            <div class="card-panel ">
                <form id="form1" runat="server">
                    <div>
                        <div class="row">
                            <div class="input-field col s12">
                                <i class="mdi-action-account-circle prefix"></i>
                                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="red-text lighten-1"
                                    ErrorMessage="E-mail obrigatório" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s12">
                                <i class="mdi-communication-vpn-key prefix"></i>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />

                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="red-text lighten-1" ErrorMessage="Senha obrigatória" />

                            </div>
                        </div>
                      
                            <div class="center">
                                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                    <p class="red-text lighten-1 center">
                                        <asp:Literal runat="server" ID="FailureText" />
                                    </p>
                                </asp:PlaceHolder>
                                <button runat="server" onserverclick="LogIn" class="btn">
                                    Log In
                                    <i class="mdi-content-send right"></i>
                                </button>


                            </div>
                        
                    </div>
                </form>
            </div>
            <br>
            <br>
        </div>
    </div>

    <!--  Scripts-->
    <script src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script src="js/materialize.js"></script>
    <script src="js/init.js"></script>
</body>
</html>
