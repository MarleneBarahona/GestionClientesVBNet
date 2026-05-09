<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="GestionClientesApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="../Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="width:300px; margin:auto; margin-top:100px;">

            <h2>Iniciar Sesión</h2>

            <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
            <br />

            <asp:TextBox ID="txtUsuario" runat="server" Width="250px"></asp:TextBox>

            <br /><br />

            <asp:Label ID="lblPassword" runat="server" Text="Contraseña"></asp:Label>
            <br />

            <asp:TextBox 
                ID="txtPassword" 
                runat="server" 
                TextMode="Password"
                Width="250px">
            </asp:TextBox>

            <br /><br />

            <asp:Button 
                ID="btnLogin" 
                runat="server" 
                Text="Ingresar"
                OnClick="btnLogin_Click" />

            <br /><br />

            <asp:Label 
                ID="lblMensaje" 
                runat="server" 
                ForeColor="Red">
            </asp:Label>

        </div>

    </form>
</body>
</html>