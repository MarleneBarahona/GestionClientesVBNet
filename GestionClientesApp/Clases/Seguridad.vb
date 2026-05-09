Imports System.Security.Cryptography
Imports System.Text

Public Class Seguridad

    Public Shared Function ObtenerSHA256(texto As String) As String

        Using sha256 As SHA256 = SHA256.Create()

            Dim bytes As Byte() = Encoding.UTF8.GetBytes(texto)

            'Genera Hash
            Dim hash As Byte() = sha256.ComputeHash(bytes)

            Dim resultado As New StringBuilder()

            'Recorre y onvierte Byte por Byte en Hexa
            For Each b As Byte In hash
                resultado.Append(b.ToString("x2"))
            Next

            Return resultado.ToString()

        End Using

    End Function

End Class