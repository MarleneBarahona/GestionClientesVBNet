<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Clientes.aspx.vb" Inherits="GestionClientesApp.Clientes" ResponseEncoding="utf-8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Clientes</title>
    <link href="../Content/Site.css" rel="stylesheet" />
</head>
<body>
    <div class="contenedor">
    <form id="form1" runat="server">
        
        <div style="width:900px; margin:auto; margin-top:30px;">
            <div class="header">
                
                <h2>Gestión de Clientes</h2>

               <div class="usuarioHeader">

        <asp:Label
            ID="lblUsuario"
            runat="server">
        </asp:Label>

        <asp:Button
            ID="btnCerrarSesion"
            runat="server"
            Text="Cerrar Sesión"
            CssClass="boton btnCerrarSesion"
            OnClick="btnCerrarSesion_Click" />

    </div>
            </div>

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
                            Width="610px">
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
                OnClick="btnGuardar_Click" 
                CssClass="boton"/>

            <asp:Button 
                ID="btnNuevo" 
                runat="server" 
                Text="Nuevo"
                OnClick="btnNuevo_Click"
                CssClass="boton" />

            <br /><br />
            <asp:Label 
                ID="lblMensaje" 
                runat="server"
                ForeColor="Red">
            </asp:Label>
            <asp:GridView 
                ID="gvClientes" 
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="IdCliente"
                Width="100%"
                OnRowCommand="gvClientes_RowCommand"
                CssClass="gridview">

                <Columns>
                    <asp:BoundField DataField="IdCliente" HeaderText="ID" />
                     <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="btnEditar"
                                runat="server"
                                Text="✏ Editar"
                                CommandName="Editar"
                                CommandArgument='<%# Container.DataItemIndex %>'
                                CssClass="btnEditar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="btnEliminar"
                                runat="server"
                                Text="Eliminar"
                                CommandName="Eliminar"
                                CommandArgument='<%# Container.DataItemIndex %>'
                                CssClass="btnEliminar"
                                OnClientClick="return confirm('¿Desea eliminar este cliente?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />

                </Columns>

            </asp:GridView>

            <br />

         <%--   <asp:Button 
                ID="btnEliminar2" 
                runat="server" 
                Text="Eliminar"
                OnClick="btnEliminar_Click" />

            <br /><br />--%>

        </div>

    </form>
</div>
</body>
</html>