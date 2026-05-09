Imports System.Data.SqlClient

Public Class BitacoraHelper

    Public Shared Sub RegistrarAccion(
        tablaAfectada As String,
        registroId As Integer,
        accion As String,
        usuario As String
    )

        Try

            Using conexionDB As SqlConnection = Conexion.ObtenerConexion()

                conexionDB.Open()

                Dim query As String = "
                    INSERT INTO BitacoraGestionClientes
                    (
                        TablaAfectada,
                        IDRegistroAfectado,
                        Accion,
                        Usuario
                    )
                    VALUES
                    (
                        @TablaAfectada,
                        @IDRegistroAfectado,
                        @Accion,
                        @Usuario
                    )
                "

                Using cmd As New SqlCommand(query, conexionDB)

                    cmd.Parameters.AddWithValue("@TablaAfectada", tablaAfectada)
                    cmd.Parameters.AddWithValue("@IDRegistroAfectado", registroId)
                    cmd.Parameters.AddWithValue("@Accion", accion)
                    cmd.Parameters.AddWithValue("@Usuario", usuario)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class