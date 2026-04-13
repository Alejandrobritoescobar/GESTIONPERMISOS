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
    Private WithEvents timerActualizacion As New System.Windows.Forms.Timer()

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
        Console.WriteLine($"[BD] Conexión OK - {ObtenerDiagnosticoArquitectura()}")
    End Sub

    Private Sub ConfigurarFormulario()
        dtpFechaDesde.Value = Date.Today

        ' ── Rango de fechas del permiso ──
        dtpFechaDesde.Value = Date.Today
        dtpFechaHasta.Value = Date.Today

        ' ── Duración de jornada ──
        ' Se asume que en el formulario existen tres RadioButtons:
        '   rbJornadaCompleta, rbMediaJornadaAM, rbMediaJornadadPM
        rbJornadaCompleta.Checked = True

        ' ── Tipo de pago (Con/Sin sueldo) ──
        rbConSueldo.Checked = True
        rbSinSueldo.Checked = False

        ' ── Campos de solo lectura ──
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
        dgvPermisos.Columns.Add("FechaDesde", "Fecha Desde")
        dgvPermisos.Columns.Add("FechaHasta", "Fecha Hasta")
        dgvPermisos.Columns.Add("Duracion", "Duración")
        dgvPermisos.Columns.Add("Tipo", "Tipo Permiso")
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
        CargarPermisos()
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

    ' ──────────────────────────────────────────
    ' Devuelve la etiqueta de duración seleccionada
    ' ──────────────────────────────────────────
    Private Function ObtenerDuracionSeleccionada() As String
        If rbJornadaCompleta.Checked Then Return "Jornada Completa"
        If rbMediaJornadaAM.Checked Then Return "Media Jornada AM"
        If rbMediaJornadaPM.Checked Then Return "Media Jornada PM"
        Return ""
    End Function

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        ' ── Validaciones ──
        If empleadoSeleccionado = 0 Then
            MessageBox.Show("Por favor seleccione un empleado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not rbConSueldo.Checked AndAlso Not rbSinSueldo.Checked Then
            MessageBox.Show("Por favor seleccione el tipo de permiso (Con o Sin Sueldo).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not rbJornadaCompleta.Checked AndAlso Not rbMediaJornadaAM.Checked AndAlso Not rbMediaJornadaPM.Checked Then
            MessageBox.Show("Por favor seleccione la duración del permiso (Jornada Completa, Media Jornada AM o Media Jornada PM).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If dtpFechaHasta.Value.Date < dtpFechaDesde.Value.Date Then
            MessageBox.Show("La fecha 'Hasta' no puede ser anterior a la fecha 'Desde'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim duracion As String = ObtenerDuracionSeleccionada()

        Try
            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()

                ' ── INSERT actualizado: FechaDesde + FechaHasta + Duracion en lugar de DiaPermiso + HorasPermiso ──
                Dim sql As String =
                    "INSERT INTO Permisos " &
                    "(ID_Empleado, Nombre, RUT, Funcion, DiaActual, FechaDesde, FechaHasta, Duracion, ConSueldo, SinSueldo, FechaRegistro) " &
                    "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    cmd.Parameters.Add("@ID_Empleado", OleDb.OleDbType.Integer).Value = empleadoSeleccionado
                    cmd.Parameters.Add("@Nombre", OleDb.OleDbType.VarWChar).Value = cmbEmpleados.SelectedItem.ToString()
                    cmd.Parameters.Add("@RUT", OleDb.OleDbType.VarWChar).Value = txtRUT.Text
                    cmd.Parameters.Add("@Funcion", OleDb.OleDbType.VarWChar).Value = txtFuncion.Text
                    cmd.Parameters.Add("@DiaActual", OleDb.OleDbType.Date).Value = dtpFechaDesde.Value
                    cmd.Parameters.Add("@FechaDesde", OleDb.OleDbType.Date).Value = dtpFechaDesde.Value.Date
                    cmd.Parameters.Add("@FechaHasta", OleDb.OleDbType.Date).Value = dtpFechaHasta.Value.Date
                    cmd.Parameters.Add("@Duracion", OleDb.OleDbType.VarWChar).Value = duracion
                    cmd.Parameters.Add("@ConSueldo", OleDb.OleDbType.Boolean).Value = rbConSueldo.Checked
                    cmd.Parameters.Add("@SinSueldo", OleDb.OleDbType.Boolean).Value = rbSinSueldo.Checked
                    cmd.Parameters.Add("@FechaRegistro", OleDb.OleDbType.Date).Value = Date.Now
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
            If empleadoSeleccionado = 0 Then
                dgvPermisos.Rows.Clear()
                Return
            End If

            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String =
                    "SELECT ID_Permiso, Nombre, RUT, Funcion, FechaDesde, FechaHasta, Duracion, ConSueldo, SinSueldo, FechaRegistro " &
                    "FROM Permisos WHERE ID_Empleado = ? ORDER BY FechaRegistro DESC"

                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    cmd.Parameters.Add("@ID_Empleado", OleDb.OleDbType.Integer).Value = empleadoSeleccionado
                    Using reader As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        dgvPermisos.Rows.Clear()
                        While reader.Read()
                            Dim tipoPago As String = If(CBool(reader("ConSueldo")), "Con Sueldo", "Sin Sueldo")
                            dgvPermisos.Rows.Add(
                                reader("ID_Permiso").ToString(),
                                reader("Nombre").ToString(),
                                reader("RUT").ToString(),
                                reader("Funcion").ToString(),
                                CDate(reader("FechaDesde")).ToString("dd/MM/yyyy"),
                                CDate(reader("FechaHasta")).ToString("dd/MM/yyyy"),
                                reader("Duracion").ToString(),
                                tipoPago,
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
            Dim fechaDesde As Date = dtpDesde.Value.Date
            Dim fechaHasta As Date = dtpHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

            Using conn As New OleDb.OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String =
                    "SELECT Nombre, RUT, Funcion, FechaDesde, FechaHasta, Duracion, ConSueldo, SinSueldo, FechaRegistro " &
                    "FROM Permisos WHERE FechaRegistro BETWEEN ? AND ? ORDER BY Nombre, FechaRegistro"

                Using cmd As New OleDb.OleDbCommand(sql, conn)
                    cmd.Parameters.Add("@Desde", OleDb.OleDbType.DBTimeStamp).Value = fechaDesde
                    cmd.Parameters.Add("@Hasta", OleDb.OleDbType.DBTimeStamp).Value = fechaHasta

                    Using reader As OleDb.OleDbDataReader = cmd.ExecuteReader()
                        Dim frmReporte As New Form()
                        frmReporte.Text = "Reporte de Permisos (" & fechaDesde.ToString("dd/MM/yyyy") & " - " & dtpHasta.Value.Date.ToString("dd/MM/yyyy") & ")"
                        frmReporte.Size = New Size(1000, 600)

                        Dim dgvReporte As New DataGridView()
                        dgvReporte.Dock = DockStyle.Fill
                        dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                        dgvReporte.Columns.Add("Nombre", "Nombre")
                        dgvReporte.Columns.Add("RUT", "RUT")
                        dgvReporte.Columns.Add("Funcion", "Función")
                        dgvReporte.Columns.Add("FechaDesde", "Fecha Desde")
                        dgvReporte.Columns.Add("FechaHasta", "Fecha Hasta")
                        dgvReporte.Columns.Add("Duracion", "Duración")
                        dgvReporte.Columns.Add("Tipo", "Tipo Permiso")
                        dgvReporte.Columns.Add("FechaRegistro", "Fecha Registro")

                        While reader.Read()
                            Dim tipoPago As String = If(CBool(reader("ConSueldo")), "Con Sueldo", "Sin Sueldo")
                            dgvReporte.Rows.Add(
                                reader("Nombre").ToString(),
                                reader("RUT").ToString(),
                                reader("Funcion").ToString(),
                                CDate(reader("FechaDesde")).ToString("dd/MM/yyyy"),
                                CDate(reader("FechaHasta")).ToString("dd/MM/yyyy"),
                                reader("Duracion").ToString(),
                                tipoPago,
                                CDate(reader("FechaRegistro")).ToString("dd/MM/yyyy HH:mm")
                            )
                        End While

                        Dim pnlBotones As New Panel()
                        pnlBotones.Dock = DockStyle.Bottom
                        pnlBotones.Height = 50

                        Dim btnExportar As New Button()
                        btnExportar.Text = "Exportar a Excel"
                        btnExportar.Dock = DockStyle.Right
                        btnExportar.Width = 150
                        AddHandler btnExportar.Click, Sub(s, ev) ExportarAExcel(dgvReporte)

                        Dim btnImprimir As New Button()
                        btnImprimir.Text = "Imprimir Permisos"
                        btnImprimir.Dock = DockStyle.Right
                        btnImprimir.Width = 150
                        Dim reporteTitulo = frmReporte.Text
                        AddHandler btnImprimir.Click, Sub(s, ev) GenerarHTMLeImprimir(dgvReporte, reporteTitulo)

                        pnlBotones.Controls.Add(btnExportar)
                        pnlBotones.Controls.Add(btnImprimir)
                        frmReporte.Controls.Add(dgvReporte)
                        frmReporte.Controls.Add(pnlBotones)
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
        dtpFechaDesde.Value = Date.Today
        dtpFechaDesde.Value = Date.Today
        dtpFechaHasta.Value = Date.Today
        rbJornadaCompleta.Checked = True
        rbConSueldo.Checked = True
        rbSinSueldo.Checked = False
        cmbEmpleados.Focus()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        LimpiarFormulario()
    End Sub

    Private Sub GenerarHTMLeImprimir(dgv As DataGridView, titulo As String)
        Try
            Dim html As New StringBuilder()
            html.AppendLine("<html><head><meta charset='utf-8'><title>Reporte de Permisos</title><style>body { font-family: Arial, sans-serif; } table { border-collapse: collapse; width: 100%; margin-top: 20px;} th, td { border: 1px solid #ddd; padding: 8px; text-align: left; } th { background-color: #f2f2f2; color: black; }</style></head><body onload='window.print()'>")
            html.AppendLine($"<h3>{titulo}</h3>")
            html.AppendLine("<table><tr>")
            For Each col As DataGridViewColumn In dgv.Columns
                html.AppendLine($"<th>{col.HeaderText}</th>")
            Next
            html.AppendLine("</tr>")
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    html.AppendLine("<tr>")
                    For Each cell As DataGridViewCell In row.Cells
                        Dim val As String = If(cell.Value IsNot Nothing, cell.Value.ToString(), "")
                        html.AppendLine($"<td>{val}</td>")
                    Next
                    html.AppendLine("</tr>")
                End If
            Next
            html.AppendLine("</table></body></html>")

            Dim tempPath = IO.Path.Combine(IO.Path.GetTempPath(), "Reporte_Permisos_" & Date.Now.ToString("yyyyMMdd_HHmmss") & ".html")
            IO.File.WriteAllText(tempPath, html.ToString(), System.Text.Encoding.UTF8)
            Process.Start(New ProcessStartInfo(tempPath) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Error al generar vista de impresión: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub rbSemanal_CheckedChanged(sender As Object, e As EventArgs) Handles rbSemanal.CheckedChanged
        If rbSemanal.Checked Then
            dtpDesde.Value = Date.Today.AddDays(-7)
            dtpHasta.Value = Date.Today
        End If
    End Sub

    Private Sub rbMensual_CheckedChanged(sender As Object, e As EventArgs) Handles rbMensual.CheckedChanged
        If rbMensual.Checked Then
            dtpDesde.Value = Date.Today.AddDays(-30)
            dtpHasta.Value = Date.Today
        End If
    End Sub

    Private Sub dtpFechas_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged, dtpHasta.ValueChanged
        If dtpDesde.Focused OrElse dtpHasta.Focused Then
            rbSemanal.Checked = False
            rbMensual.Checked = False
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If DEBUG Then
        Me.Text &= $" [{ObtenerDiagnosticoArquitectura()}]"
#End If
        CargarPermisos()
        timerActualizacion.Interval = 5000
        timerActualizacion.Start()
    End Sub

    Private Sub timerActualizacion_Tick(sender As Object, e As EventArgs) Handles timerActualizacion.Tick
        If Application.OpenForms.Count <= 1 Then
            Dim indexScroll As Integer = -1
            If dgvPermisos.Rows.Count > 0 Then indexScroll = dgvPermisos.FirstDisplayedScrollingRowIndex
            CargarPermisos()
            If indexScroll >= 0 AndAlso indexScroll < dgvPermisos.Rows.Count Then
                dgvPermisos.FirstDisplayedScrollingRowIndex = indexScroll
            End If
        End If
    End Sub

    Private Sub txtFuncion_TextChanged(sender As Object, e As EventArgs) Handles txtFuncion.TextChanged
    End Sub

    Private Sub dgvPermisos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPermisos.CellContentClick

    End Sub
End Class