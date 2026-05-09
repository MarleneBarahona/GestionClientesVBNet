Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text.RegularExpressions

Partial Class Clientes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Valida sesion. Regresa al Login en dado caso no hay sesion vigente
        If Session("Usuario") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            CargarClientes()
        End If
        'Response.Write(Seguridad.ObtenerSHA256("admin123")) quitarlo al subir a git
    End Sub

    Private Sub CargarClientes()

        Try

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Dim query As String = "
                    SELECT *
                    FROM Clientes
                    WHERE Activo = 1
                    ORDER BY IdCliente DESC
                "

                Using cmd As New SqlCommand(query, conexionDB)

                    Dim dt As New DataTable()

                    Dim da As New SqlDataAdapter(cmd)

                    da.Fill(dt)

                    gvClientes.DataSource = dt
                    gvClientes.DataBind()

                End Using

            End Using

        Catch ex As Exception

            lblMensaje.Text = ex.Message

        End Try

    End Sub
    Private Sub LimpiarFormulario()

        hfIdCliente.Value = ""

        txtNombre.Text = ""
        txtApellido.Text = ""
        txtCorreo.Text = ""
        txtTelefono.Text = ""
        txtDireccion.Text = ""

    End Sub
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        'Valida campo Nombre no esté vacío
        If txtNombre.Text.Trim() = "" Then
            txtNombre.BorderColor = Drawing.Color.Red
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Ingrese el nombre"
            Exit Sub
        Else
            txtNombre.BorderColor = Drawing.Color.Empty
        End If

        'Valida campo Apellido no esté vacío
        If txtApellido.Text.Trim() = "" Then
            txtApellido.BorderColor = Drawing.Color.Red
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Ingrese el apellido"
            Exit Sub
        Else
            txtApellido.BorderColor = Drawing.Color.Empty
        End If

        'Valida campo Correo no esté vacío
        If txtCorreo.Text.Trim() = "" Then
            txtCorreo.BorderColor = Drawing.Color.Red
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Ingrese el correo electronico"
            Exit Sub
        Else
            txtCorreo.BorderColor = Drawing.Color.Empty
        End If

        'Valida campo Correo cumple con formato minimo
        If Not txtCorreo.Text.Contains("@") Then
            txtCorreo.BorderColor = Drawing.Color.Red
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Correo electronico inválido"
            Exit Sub
        Else
            txtCorreo.BorderColor = Drawing.Color.Empty
        End If

        'Valida campo Telefono no esté vacío
        If txtTelefono.Text.Trim() = "" Then
            txtTelefono.BorderColor = Drawing.Color.Red
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Ingrese el número de telefono"
            Exit Sub
        Else
            txtTelefono.BorderColor = Drawing.Color.Empty
        End If

        'Valida campo Telefono cumple con formato
        If Not Regex.IsMatch(txtTelefono.Text.Trim(), "^\d{4}-\d{4}$") Then
            txtTelefono.BorderColor = Drawing.Color.Red
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Formato de telefono inválido. Use ####-####"
            Exit Sub

        End If

        'Valida campo Direccion no esté vacío
        If txtDireccion.Text.Trim() = "" Then
            txtDireccion.BorderColor = Drawing.Color.Red
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Ingrese la dirección"
            Exit Sub
        Else
            txtDireccion.BorderColor = Drawing.Color.Empty
        End If

        'Guardar cambios en BD
        Try

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Dim query As String = ""

                'Valida si no hay IdCliente, es porque es nuevo
                If String.IsNullOrEmpty(hfIdCliente.Value) Then

                    query = "
                    INSERT INTO Clientes
                    (
                        Nombre,
                        Apellido,
                        Correo,
                        Telefono,
                        Direccion
                    )
                    VALUES
                    (
                        @Nombre,
                        @Apellido,
                        @Correo,
                        @Telefono,
                        @Direccion
                    )


                    SELECT SCOPE_IDENTITY()
                "

                    'Valida si sí hay IdCliente, es porque es ya existente
                Else

                    query = "
                    UPDATE Clientes
                    SET
                        Nombre = @Nombre,
                        Apellido = @Apellido,
                        Correo = @Correo,
                        Telefono = @Telefono,
                        Direccion = @Direccion,
                        FechaUltimaModificacion = GETDATE()
                    WHERE IdCliente = @IdCliente
                "

                End If

                Using cmd As New SqlCommand(query, conexionDB)

                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim())
                    cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim())
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim())
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim())
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim())

                    If String.IsNullOrEmpty(hfIdCliente.Value) Then

                        Dim idGenerado As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                        BitacoraHelper.RegistrarAccion(
                            "Clientes",
                            idGenerado,
                            "INSERT",
                            Session("Usuario").ToString()
                        )

                    Else

                        cmd.Parameters.AddWithValue("@IdCliente", hfIdCliente.Value)

                        cmd.ExecuteNonQuery()

                        BitacoraHelper.RegistrarAccion(
                            "Clientes",
                            Convert.ToInt32(hfIdCliente.Value),
                            "UPDATE",
                            Session("Usuario").ToString()
                        )

                    End If
                    'cmd.ExecuteNonQuery()

                End Using

            End Using

            lblMensaje.Text = "Registro guardado correctamente"
            lblMensaje.ForeColor = Drawing.Color.Green

            LimpiarFormulario()

            CargarClientes()

        Catch ex As Exception

            lblMensaje.Text = ex.Message

        End Try

    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs)

        LimpiarFormulario()

    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs)

        Try

            If String.IsNullOrEmpty(hfIdCliente.Value) Then

                lblMensaje.Text = "Seleccione un cliente"

                Exit Sub

            End If

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                'Hace un procesa de desactivacion del cliente, no lo elimina por completo de la BD
                Dim query As String = "
                    UPDATE Clientes
                    SET Activo = 0,
                    FechaUltimaModificacion = GETDATE()
                    WHERE IdCliente = @IdCliente
                    "

                Using cmd As New SqlCommand(query, conexionDB)

                    cmd.Parameters.AddWithValue("@IdCliente", hfIdCliente.Value)

                    cmd.ExecuteNonQuery()

                    BitacoraHelper.RegistrarAccion(
                        "Clientes",
                        Convert.ToInt32(hfIdCliente.Value),
                        "DELETE",
                        Session("Usuario").ToString()
                    )
                End Using

            End Using

            lblMensaje.Text = "Cliente eliminado correctamente"
            lblMensaje.ForeColor = Drawing.Color.Green

            LimpiarFormulario()

            CargarClientes()

        Catch ex As Exception

            lblMensaje.Text = ex.Message

        End Try

    End Sub

    Protected Sub gvClientes_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim fila = gvClientes.SelectedRow

        hfIdCliente.Value = gvClientes.DataKeys(fila.RowIndex).Value.ToString()

        txtNombre.Text = Server.HtmlDecode(fila.Cells(2).Text)
        txtApellido.Text = Server.HtmlDecode(fila.Cells(3).Text)
        txtCorreo.Text = Server.HtmlDecode(fila.Cells(4).Text)
        txtTelefono.Text = Server.HtmlDecode(fila.Cells(5).Text)
        txtDireccion.Text = Server.HtmlDecode(fila.Cells(6).Text)

    End Sub

    Protected Sub gvClientes_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim fila As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

        hfIdCliente.Value = gvClientes.DataKeys(fila.RowIndex).Value.ToString()

        If e.CommandName = "Editar" Then
            txtNombre.Text = Server.HtmlDecode(fila.Cells(3).Text)
            txtApellido.Text = Server.HtmlDecode(fila.Cells(4).Text)
            txtCorreo.Text = Server.HtmlDecode(fila.Cells(5).Text)
            txtTelefono.Text = Server.HtmlDecode(fila.Cells(6).Text)
            txtDireccion.Text = Server.HtmlDecode(fila.Cells(7).Text)
        ElseIf e.CommandName = "Eliminar" Then

            Try
                Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                    conexionDB.Open()

                    'Hace un procesa de desactivacion del cliente, no lo elimina por completo de la BD
                    Dim query As String = "
                    UPDATE Clientes
                    SET Activo = 0,
                    FechaUltimaModificacion = GETDATE()
                    WHERE IdCliente = @IdCliente
                    "

                    Using cmd As New SqlCommand(query, conexionDB)

                        cmd.Parameters.AddWithValue("@IdCliente", hfIdCliente.Value)

                        cmd.ExecuteNonQuery()

                        BitacoraHelper.RegistrarAccion(
                            "Clientes",
                            Convert.ToInt32(hfIdCliente.Value),
                            "DELETE",
                            Session("Usuario").ToString()
                        )
                    End Using

                End Using

                lblMensaje.Text = "Cliente eliminado correctamente"
                lblMensaje.ForeColor = Drawing.Color.Green

                CargarClientes()

            Catch ex As Exception

                lblMensaje.Text = ex.Message

            End Try
        End If
    End Sub
    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)

        Session.Clear()

        Session.Abandon()

        Response.Redirect("Login.aspx")

    End Sub

End Class