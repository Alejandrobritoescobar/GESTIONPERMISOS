Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms
Imports System.Configuration
Imports System.Runtime.InteropServices
Imports System.Text
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form1
    Inherits System.Windows.Forms.Form

    Private connectionString As String
    Private empleadoSeleccionado As Integer = 0

    ' ==========================================
    ' 🔍 FUNCIONES DE DETECCIÓN DE ARQUITECTURA (INTEGRADAS)
    ' ==========================================

    Private Function VerificarYPrepararConexion(ByRef connStr As String) As Boolean
        Try
            If ProveedorACEDisponible() Then Return True
            If TryAlternativeProvider(connStr) Then Return True
            MostrarErrorInstalacion()
            Return False
        Catch ex As Exception
            Console.WriteLine($"[Detección] Error: {ex.Message}")
            Return False
        End Try
    End Function

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

    Private Function ObtenerDiagnosticoArquitectura() As String
        Return $"SO:{If(Environment.Is64BitOperatingSystem, "64", "32")} | " &
               $"Proc:{If(IntPtr.Size = 8, "64", "32")} | " &
               $"ACE:{If(ProveedorACEDisponible(), "OK", "NO")}"
    End Function
    ' ==========================================
    ' ✅ FIN DE FUNCIONES DE DETECCIÓN
    ' ==========================================

    Public Sub New()
        InitializeComponent()
        AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath)

        Try
            ' Leer cadena de conexión maestra del archivo App.config
            connectionString = ConfigurationManager.ConnectionStrings("SistemaPermisosConnectionString").ConnectionString

            ConfigurarConexion()
            CargarEmpleados()
            ConfigurarFormulario()
        Catch ex As OleDbException
            MostrarErrorInstalacion()
            DeshabilitarFuncionalidadBD()
        Catch ex As Exception
            MessageBox.Show("Fallo al conectarse a la Base de Datos. Revise el archivo de configuración (.config)." & vbCrLf & vbCrLf & "Detalle: " & ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DeshabilitarFuncionalidadBD()
        End Try
    End Sub

    Private Sub DeshabilitarFuncionalidadBD()
        btnGuardar.Enabled = False
        btnReporte.Enabled = False
        cmbEmpleados.Enabled = False
        MessageBox.Show("La aplicación funcionará en modo limitado hasta que se instale el motor de base de datos.",
                   "Modo limitado", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ConfigurarConexion()
        Using conn As New OleDbConnection(connectionString)
            conn.Open()
            conn.Close()
        End Using

        ' ✅ Diagnóstico opcional en consola (solo desarrollo)
        Console.WriteLine($"[BD] Conexión OK - {ObtenerDiagnosticoArquitectura()}")
    End Sub

    Private Sub ConfigurarFormulario()
        dtpDiaActual.Value = Date.Today
        dtpDiaPermiso.Value = Date.Today
        numHoras.Minimum = 1
        numHoras.Maximum = 24
        numHoras.Value = 1
        rbConSueldo.Checked = True
        rbSinSueldo.Checked = False
        txtRUT.ReadOnly = True
        txtFuncion.ReadOnly = True
        ConfigurarDataGridView()
    End Sub

    Private Sub ConfigurarDataGridView()
        dgvPermisos.DataSource = Nothing
        dgvPermisos.Columns.Clear()
        dgvPermisos.Columns.Add("ID", "ID")
        dgvPermisos.Columns.Add("Nombre", "Nombre")
        dgvPermisos.Columns.Add("RUT", "RUT")
        dgvPermisos.Columns.Add("Funcion", "Función")
        dgvPermisos.Columns.Add("DiaPermiso", "Fecha Permiso")
        dgvPermisos.Columns.Add("Horas", "Horas")
        dgvPermisos.Columns.Add("Tipo", "Tipo")
        dgvPermisos.Columns.Add("FechaRegistro", "Fecha Registro")
        dgvPermisos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub CargarEmpleados()
        Try
            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT ID_Empleado, Nombre, RUT, Funcion FROM Empleados WHERE Estado = True ORDER BY Nombre"

                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    Using reader As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        cmbEmpleados.Items.Clear()
                        cmbEmpleados.Items.Add("-- Seleccione Empleado --")

                        While reader.Read()
                            Dim item As New ListItem(reader("Nombre").ToString(), reader("ID_Empleado").ToString())
                            cmbEmpleados.Items.Add(item)
                        End While

                        cmbEmpleados.SelectedIndex = 0
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar empleados: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Class ListItem
        Public Property Display As String
        Public Property Value As String
        Public Sub New(display As String, value As String)
            Me.Display = display
            Me.Value = value
        End Sub
        Public Overrides Function ToString() As String
            Return Display
        End Function
    End Class

    Private Sub cmbEmpleados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmpleados.SelectedIndexChanged
        If cmbEmpleados.SelectedIndex > 0 Then
            Dim item As ListItem = CType(cmbEmpleados.SelectedItem, ListItem)
            empleadoSeleccionado = CInt(item.Value)
            CargarDatosEmpleado(empleadoSeleccionado)
        Else
            LimpiarDatosEmpleado()
        End If
    End Sub

    Private Sub CargarDatosEmpleado(idEmpleado As Integer)
        Try
            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT RUT, Funcion FROM Empleados WHERE ID_Empleado = ?"
                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", idEmpleado)
                    Using reader As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtRUT.Text = reader("RUT").ToString()
                            txtFuncion.Text = reader("Funcion").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LimpiarDatosEmpleado()
        txtRUT.Text = ""
        txtFuncion.Text = ""
        empleadoSeleccionado = 0
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If empleadoSeleccionado = 0 Then
            MessageBox.Show("Por favor seleccione un empleado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not rbConSueldo.Checked And Not rbSinSueldo.Checked Then
            MessageBox.Show("Por favor seleccione el tipo de permiso (Con o Sin Sueldo).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If numHoras.Value <= 0 Then
            MessageBox.Show("Las horas deben ser mayores a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "INSERT INTO Permisos (ID_Empleado, Nombre, RUT, Funcion, DiaActual, DiaPermiso, HorasPermiso, ConSueldo, SinSueldo, FechaRegistro) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID_Empleado", empleadoSeleccionado)
                    cmd.Parameters.AddWithValue("@Nombre", cmbEmpleados.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@RUT", txtRUT.Text)
                    cmd.Parameters.AddWithValue("@Funcion", txtFuncion.Text)
                    cmd.Parameters.AddWithValue("@DiaActual", dtpDiaActual.Value)
                    cmd.Parameters.AddWithValue("@DiaPermiso", dtpDiaPermiso.Value)
                    cmd.Parameters.AddWithValue("@HorasPermiso", CInt(numHoras.Value))
                    cmd.Parameters.AddWithValue("@ConSueldo", rbConSueldo.Checked)
                    cmd.Parameters.AddWithValue("@SinSueldo", rbSinSueldo.Checked)
                    cmd.Parameters.AddWithValue("@FechaRegistro", Date.Now)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("Permiso guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarFormulario()
            CargarPermisos()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarPermisos()
        Try
            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT ID_Permiso, Nombre, RUT, Funcion, DiaPermiso, HorasPermiso, ConSueldo, SinSueldo, FechaRegistro FROM Permisos ORDER BY FechaRegistro DESC"
                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    Using reader As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        dgvPermisos.Rows.Clear()
                        While reader.Read()
                            Dim tipo As String = ""
                            If CBool(reader("ConSueldo")) Then
                                tipo = "Con Sueldo"
                            Else
                                tipo = "Sin Sueldo"
                            End If
                            dgvPermisos.Rows.Add(
                                reader("ID_Permiso").ToString(),
                                reader("Nombre").ToString(),
                                reader("RUT").ToString(),
                                reader("Funcion").ToString(),
                                CDate(reader("DiaPermiso")).ToString("dd/MM/yyyy"),
                                reader("HorasPermiso").ToString(),
                                tipo,
                                CDate(reader("FechaRegistro")).ToString("dd/MM/yyyy HH:mm")
                            )
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar permisos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        GenerarReporte()
    End Sub

    Private Sub GenerarReporte()
        Try
            Dim fechaDesde As Date = dtpDesde.Value
            Dim fechaHasta As Date = dtpHasta.Value

            If rbSemanal.Checked Then
                fechaDesde = Date.Today.AddDays(-7)
                fechaHasta = Date.Today
                dtpDesde.Value = fechaDesde
                dtpHasta.Value = fechaHasta
            ElseIf rbMensual.Checked Then
                fechaDesde = New Date(Date.Today.Year, Date.Today.Month, 1)
                fechaHasta = Date.Today
                dtpDesde.Value = fechaDesde
                dtpHasta.Value = fechaHasta
            End If

            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT Nombre, RUT, Funcion, DiaPermiso, HorasPermiso, ConSueldo, SinSueldo, FechaRegistro " &
                                   "FROM Permisos WHERE FechaRegistro BETWEEN ? AND ? ORDER BY Nombre, FechaRegistro"
                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Desde", fechaDesde)
                    cmd.Parameters.AddWithValue("@Hasta", fechaHasta)
                    Using reader As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        Dim frmReporte As New Form()
                        frmReporte.Text = "Reporte de Permisos (" & fechaDesde.ToString("dd/MM/yyyy") & " - " & fechaHasta.ToString("dd/MM/yyyy") & ")"
                        frmReporte.Size = New Size(900, 600)
                        Dim dgvReporte As New DataGridView()
                        dgvReporte.Dock = DockStyle.Fill
                        dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                        dgvReporte.Columns.Add("Nombre", "Nombre")
                        dgvReporte.Columns.Add("RUT", "RUT")
                        dgvReporte.Columns.Add("Funcion", "Función")
                        dgvReporte.Columns.Add("DiaPermiso", "Fecha Permiso")
                        dgvReporte.Columns.Add("Horas", "Horas")
                        dgvReporte.Columns.Add("Tipo", "Tipo")
                        dgvReporte.Columns.Add("FechaRegistro", "Fecha Registro")

                        While reader.Read()
                            Dim tipo As String = ""
                            If CBool(reader("ConSueldo")) Then
                                tipo = "Con Sueldo"
                            Else
                                tipo = "Sin Sueldo"
                            End If
                            dgvReporte.Rows.Add(
                                reader("Nombre").ToString(),
                                reader("RUT").ToString(),
                                reader("Funcion").ToString(),
                                CDate(reader("DiaPermiso")).ToString("dd/MM/yyyy"),
                                reader("HorasPermiso").ToString(),
                                tipo,
                                CDate(reader("FechaRegistro")).ToString("dd/MM/yyyy HH:mm")
                            )
                        End While

                        Dim btnExportar As New Button()
                        btnExportar.Text = "Exportar a Excel"
                        btnExportar.Dock = DockStyle.Bottom
                        btnExportar.Height = 40
                        AddHandler btnExportar.Click, Sub(s, ev) ExportarAExcel(dgvReporte)
                        frmReporte.Controls.Add(dgvReporte)
                        frmReporte.Controls.Add(btnExportar)
                        frmReporte.ShowDialog()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al generar reporte: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportarAExcel(dgv As DataGridView)
        Dim excelApp As Excel.Application = Nothing
        Dim workbook As Excel.Workbook = Nothing
        Dim worksheet As Excel.Worksheet = Nothing
        Try
            If Not ExcelInstalado() Then
                MessageBox.Show("Excel no está instalado. Exportando a CSV.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ExportarACSV(dgv)
                Return
            End If
            excelApp = New Excel.Application()
            excelApp.Visible = True
            workbook = excelApp.Workbooks.Add()
            worksheet = CType(workbook.Sheets(1), Excel.Worksheet)
            For i As Integer = 0 To dgv.Columns.Count - 1
                worksheet.Cells(1, i + 1).Value = dgv.Columns(i).HeaderText
                worksheet.Cells(1, i + 1).Font.Bold = True
            Next
            For i As Integer = 0 To dgv.Rows.Count - 1
                For j As Integer = 0 To dgv.Columns.Count - 1
                    If dgv.Rows(i).Cells(j).Value IsNot Nothing Then
                        worksheet.Cells(i + 2, j + 1).Value = dgv.Rows(i).Cells(j).Value.ToString()
                    End If
                Next
            Next
            worksheet.Rows(1).Font.Bold = True
            worksheet.Columns.AutoFit()
            MessageBox.Show("Reporte exportado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error al exportar: " & ex.Message & vbCrLf & "Intentando CSV...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ExportarACSV(dgv)
        Finally
            If worksheet IsNot Nothing Then Marshal.ReleaseComObject(worksheet) : worksheet = Nothing
            If workbook IsNot Nothing Then workbook.Close(False) : Marshal.ReleaseComObject(workbook) : workbook = Nothing
            If excelApp IsNot Nothing Then excelApp.Quit() : Marshal.ReleaseComObject(excelApp) : excelApp = Nothing
            GC.Collect() : GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Function ExcelInstalado() As Boolean
        Try
            Dim excelApp As Object = CreateObject("Excel.Application")
            If excelApp IsNot Nothing Then
                excelApp.Quit()
                Marshal.ReleaseComObject(excelApp)
                Return True
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    Private Sub ExportarACSV(dgv As DataGridView)
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Archivo CSV|*.csv"
            saveDialog.Title = "Guardar Reporte"
            saveDialog.FileName = "Reporte_Permisos_" & Date.Today.ToString("yyyyMMdd")
            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim writer As New StreamWriter(saveDialog.FileName, False, Encoding.UTF8)
                For i As Integer = 0 To dgv.Columns.Count - 1
                    writer.Write(dgv.Columns(i).HeaderText)
                    If i < dgv.Columns.Count - 1 Then writer.Write(";")
                Next
                writer.WriteLine()
                For i As Integer = 0 To dgv.Rows.Count - 1
                    For j As Integer = 0 To dgv.Columns.Count - 1
                        If dgv.Rows(i).Cells(j).Value IsNot Nothing Then
                            writer.Write(dgv.Rows(i).Cells(j).Value.ToString())
                        End If
                        If j < dgv.Columns.Count - 1 Then writer.Write(";")
                    Next
                    writer.WriteLine()
                Next
                writer.Close()
                MessageBox.Show("Reporte exportado a CSV exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al exportar CSV: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LimpiarFormulario()
        cmbEmpleados.SelectedIndex = 0
        txtRUT.Clear()
        txtFuncion.Clear()
        dtpDiaActual.Value = Date.Today
        dtpDiaPermiso.Value = Date.Today
        numHoras.Value = 1
        rbConSueldo.Checked = True
        rbSinSueldo.Checked = False
        cmbEmpleados.Focus()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        LimpiarFormulario()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ✅ Diagnóstico en título solo en modo Debug
#If DEBUG Then
        Me.Text &= $" [{ObtenerDiagnosticoArquitectura()}]"
#End If
        CargarPermisos()
    End Sub

    Private Sub txtFuncion_TextChanged(sender As Object, e As EventArgs) Handles txtFuncion.TextChanged
    End Sub
End Class