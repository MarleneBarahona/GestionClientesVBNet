<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Clientes.aspx.vb" Inherits="GestionClientesApp.Clientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Clientes</title>
</head>
<body>

    <form id="form1" runat="server">

        <div style="width:900px; margin:auto; margin-top:30px;">

            <h2>Gestión de Clientes</h2>

            <hr />

            <table>

                <tr>
                    <td>Nombre:</td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </td>

                    <td>Apellido:</td>
                    <td>
                        <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>Correo eletrónico:</td>
                    <td>
                        <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
                    </td>

                    <td>Teléfono:</td>
                    <td>
                        <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>Dirección:</td>
                    <td colspan="3">
                        <asp:TextBox 
                            ID="txtDireccion" 
                            runat="server"
                            Width="400px">
                        </asp:TextBox>
                    </td>
                </tr>

            </table>

            <br />

            <asp:HiddenField ID="hfIdCliente" runat="server" />

            <asp:Button 
                ID="btnGuardar" 
                runat="server" 
                Text="Guardar"
                OnClick="btnGuardar_Click" />

            <asp:Button 
                ID="btnNuevo" 
                runat="server" 
                Text="Nuevo"
                OnClick="btnNuevo_Click" />

            <br /><br />

            <asp:GridView 
                ID="gvClientes" 
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="IdCliente"
                Width="100%"
                AutoGenerateSelectButton="True"
                OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">

                <Columns>

                    <asp:BoundField DataField="IdCliente" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />

                </Columns>

            </asp:GridView>

            <br />

            <asp:Button 
                ID="btnEliminar" 
                runat="server" 
                Text="Eliminar"
                OnClick="btnEliminar_Click" />

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