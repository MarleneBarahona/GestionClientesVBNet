Imports System.Data.SqlClient
Imports GestionClientesApp

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)

        Try
            lblMensaje.Text = ""

            txtUsuario.BorderColor = Drawing.Color.Empty
            txtPassword.BorderColor = Drawing.Color.Empty

            'Valida Usuario no esté vacío
            If txtUsuario.Text.Trim() = "" Then

                txtUsuario.BorderColor = Drawing.Color.Red

                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Ingrese el usuario"

                Exit Sub

            End If
            'Valida Contraseña no esté vacío
            If txtPassword.Text.Trim() = "" Then

                txtPassword.BorderColor = Drawing.Color.Red

                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Ingrese la contraseña"

                Exit Sub

            End If

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Dim query As String = "
                    SELECT Usuario
                    FROM Usuarios
                    WHERE Usuario = @Usuario
                    AND Contrasena = @Password
                "

                Using cmd As New SqlCommand(query, conexionDB)

                    cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim())
                    Dim passwordHash As String =
                    Seguridad.ObtenerSHA256(txtPassword.Text.Trim())

                    cmd.Parameters.AddWithValue("@Password", passwordHash)

                    Dim resultado = cmd.ExecuteScalar()

                    'Si credenciales son correctas, redirige a pagina Clientes
                    If resultado IsNot Nothing Then

                        Session("Usuario") = resultado.ToString()

                        Response.Redirect("Clientes.aspx")

                    Else
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Text = "Usuario o contraseña incorrectos"
                    End If

                End Using

            End Using

        Catch ex As Exception

            lblMensaje.Text = ex.Message

        End Try

    End Sub

End Class