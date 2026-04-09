Imports System
Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Module ModuloDeteccion

    ''' <summary>
    ''' Verifica si el proveedor ACE está disponible y prepara la conexión
    ''' </summary>
    Public Function VerificarYPrepararConexion(ByRef connectionString As String) As Boolean
        Try
            If ProveedorACEDisponible() Then
                Return True
            End If

            If TryAlternativeProvider(connectionString) Then
                Return True
            End If

            MostrarErrorInstalacion()
            Return False

        Catch ex As Exception
            Console.WriteLine($"[Detección] Error: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Verifica si el proveedor ACE.OLEDB.12.0 puede ser instanciado
    ''' </summary>
    Private Function ProveedorACEDisponible() As Boolean
        Try
            Using testConn As New OleDbConnection(
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\dummy.accdb;")
                Return True
            End Using
        Catch ex As OleDbException
            If ex.ErrorCode = -2147467259 Then Return False
            Return True
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Intenta usar proveedor alternativo Jet 4.0 para archivos .mdb
    ''' </summary>
    Private Function TryAlternativeProvider(ByRef connStr As String) As Boolean
        Try
            If connStr.Contains(".accdb") Then Return False

            Dim altConn As String = connStr.Replace(
                "Provider=Microsoft.ACE.OLEDB.12.0",
                "Provider=Microsoft.Jet.OLEDB.4.0")

            Using testConn As New OleDbConnection(altConn)
                testConn.Open()
                testConn.Close()
                connStr = altConn
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Muestra mensaje de error con instrucciones para el usuario
    ''' </summary>
    Private Sub MostrarErrorInstalacion()
        Dim arquitectura As String = If(IntPtr.Size = 8, "64-bit", "32-bit")
        Dim sistema As String = If(Environment.Is64BitOperatingSystem, "64-bit", "32-bit")

        MessageBox.Show(
            $"⚠️ No se detectó el motor de base de datos requerido.%0A%0A" &
            $"Detalles:%0A" &
            $"• Sistema: {sistema}%0A" &
            $"• Aplicación: {arquitectura}%0A%0A" &
            $"Solución:%0A" &
            $"1. Instalar 'Microsoft Access Database Engine 2016'%0A" &
            $"2. Usar versión que coincida con la aplicación ({arquitectura})%0A%0A" &
            $"Descarga: https://www.microsoft.com/es-es/download/details.aspx?id=54920",
            "Motor no encontrado",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning)
    End Sub

    ''' <summary>
    ''' Función de diagnóstico para modo desarrollo (opcional)
    ''' </summary>
    Public Function ObtenerDiagnosticoArquitectura() As String
        Return $"SO:{If(Environment.Is64BitOperatingSystem, "64", "32")} | " &
               $"Proc:{If(IntPtr.Size = 8, "64", "32")} | " &
               $"ACE:{If(ProveedorACEDisponible(), "OK", "NO")}"
    End Function

End Module