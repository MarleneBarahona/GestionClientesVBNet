Imports System.Data.SqlClient

Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Response.Write("Conexión exitosa")

            End Using

        Catch ex As Exception

            Response.Write(ex.Message)

        End Try

    End Sub

End Class