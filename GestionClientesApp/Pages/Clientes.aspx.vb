Imports System.Data
Imports System.Data.SqlClient

Partial Class Clientes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("Usuario") Is Nothing Then
            Response.Redirect("Login.aspx")
        End If

        If Not IsPostBack Then
            CargarClientes()
        End If

    End Sub

    Private Sub CargarClientes()

        Try

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Dim query As String = "
                    SELECT *
                    FROM Clientes
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

        Try

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Dim query As String = ""

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

                    'If Not String.IsNullOrEmpty(hfIdCliente.Value) Then

                    '    cmd.Parameters.AddWithValue("@IdCliente", hfIdCliente.Value)

                    'End If

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

            LimpiarFormulario()

            CargarClientes()

        Catch ex As Exception

            lblMensaje.Text = ex.Message

        End Try

    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs)

        Try

            If String.IsNullOrEmpty(hfIdCliente.Value) Then

                lblMensaje.Text = "Seleccione un cliente"

                Exit Sub

            End If

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Dim query As String = "
                DELETE FROM Clientes
                WHERE IdCliente = @IdCliente
            "

                Using cmd As New SqlCommand(query, conexionDB)

                    cmd.Parameters.AddWithValue("@IdCliente", hfIdCliente.Value)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            lblMensaje.Text = "Cliente eliminado correctamente"

            LimpiarFormulario()

            CargarClientes()

        Catch ex As Exception

            lblMensaje.Text = ex.Message

        End Try

    End Sub

    Protected Sub gvClientes_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim fila = gvClientes.SelectedRow

        hfIdCliente.Value = gvClientes.DataKeys(fila.RowIndex).Value.ToString()

        txtNombre.Text = fila.Cells(2).Text
        txtApellido.Text = fila.Cells(3).Text
        txtCorreo.Text = fila.Cells(4).Text
        txtTelefono.Text = fila.Cells(5).Text
        txtDireccion.Text = fila.Cells(6).Text

    End Sub
End Class