Imports System.Configuration
Imports System.Data.OleDb

Public Class Form1
    Inherits System.Windows.Forms.Form

    Private connectionString As String
    Private empleadoSeleccionado As Integer = 0

    Public Sub New()
        InitializeComponent()
        ConfigurarConexion()
        CargarEmpleados()
        ConfigurarFormulario()
    End Sub

    Private Sub ConfigurarConexion()
        Dim connSettings = ConfigurationManager.ConnectionStrings("SistemaPermisosConnectionString")
        If connSettings IsNot Nothing Then
            connectionString = connSettings.ConnectionString
        Else
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Permiso1\Sistemapermisos.accdb;"
        End If
    End Sub

    Private Sub ConfigurarFormulario()
        ' Configurar DateTimePickers
        dtpDiaActual.Value = Date.Today
        dtpDiaPermiso.Value = Date.Today

        ' Configurar NumericUpDown
        numHoras.Minimum = 1
        numHoras.Maximum = 24
        numHoras.Value = 1

        ' Configurar RadioButtons (mutuamente excluyentes)
        rbConSueldo.Checked = True
        rbSinSueldo.Checked = False

        ' Configurar TextBox de solo lectura
        txtRUT.ReadOnly = True
        txtFuncion.ReadOnly = True

        ' Configurar DataGridView
        ConfigurarDataGridView()
    End Sub

    Private Sub ConfigurarDataGridView()
        dgvPermisos.Columns.Clear()
        dgvPermisos.Columns.Add("ID", "ID")
        dgvPermisos.Columns.Add("Nombre", "Nombre")
        dgvPermisos.Columns.Add("RUT", "RUT")
        dgvPermisos.Columns.Add("Funcion", "Función")
        dgvPermisos.Columns.Add("Dia_Permiso", "Fecha Permiso")
        dgvPermisos.Columns.Add("Horas", "Horas")
        dgvPermisos.Columns.Add("Tipo", "Tipo")
        dgvPermisos.Columns.Add("Fecha_Registro", "Fecha Registro")

        dgvPermisos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub CargarEmpleados()
        Try
            Using conn As New OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT ID_Empleado, Nombre, RUT, Funcion FROM Empleados WHERE Estado = 'Activo' ORDER BY Nombre"

                Using cmd As New OleDbCommand(sql, conn)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
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
            Using conn As New OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT RUT, Funcion FROM Empleados WHERE ID_Empleado = ?"

                Using cmd As New OleDbCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", idEmpleado)

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
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
        ' Validar empleado seleccionado
        If empleadoSeleccionado = 0 Then
            MessageBox.Show("Por favor seleccione un empleado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validar que un RadioButton esté seleccionado
        If Not rbConSueldo.Checked And Not rbSinSueldo.Checked Then
            MessageBox.Show("Por favor seleccione el tipo de permiso (Con o Sin Sueldo).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validar horas
        If numHoras.Value <= 0 Then
            MessageBox.Show("Las horas deben ser mayores a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "INSERT INTO Permisos (ID_Empleado, Nombre, RUT, Funcion, Dia_Actual, Dia_Permiso, Horas_Permiso, Con_Sueldo, Sin_Sueldo, Fecha_Registro) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

                Using cmd As New OleDbCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID_Empleado", empleadoSeleccionado)
                    cmd.Parameters.AddWithValue("@Nombre", cmbEmpleados.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@RUT", txtRUT.Text)
                    cmd.Parameters.AddWithValue("@Funcion", txtFuncion.Text)
                    cmd.Parameters.AddWithValue("@Dia_Actual", dtpDiaActual.Value)
                    cmd.Parameters.AddWithValue("@Dia_Permiso", dtpDiaPermiso.Value)
                    cmd.Parameters.AddWithValue("@Horas_Permiso", CInt(numHoras.Value))
                    cmd.Parameters.AddWithValue("@Con_Sueldo", rbConSueldo.Checked)
                    cmd.Parameters.AddWithValue("@Sin_Sueldo", rbSinSueldo.Checked)
                    cmd.Parameters.AddWithValue("@Fecha_Registro", Date.Now)

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
            Using conn As New OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT ID_Permiso, Nombre, RUT, Funcion, Dia_Permiso, Horas_Permiso, Con_Sueldo, Sin_Sueldo, Fecha_Registro FROM Permisos ORDER BY Fecha_Registro DESC"

                Using cmd As New OleDbCommand(sql, conn)
                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        dgvPermisos.Rows.Clear()

                        While reader.Read()
                            Dim tipo As String = ""
                            If CBool(reader("Con_Sueldo")) Then
                                tipo = "Con Sueldo"
                            Else
                                tipo = "Sin Sueldo"
                            End If

                            dgvPermisos.Rows.Add(
                                reader("ID_Permiso").ToString(),
                                reader("Nombre").ToString(),
                                reader("RUT").ToString(),
                                reader("Funcion").ToString(),
                                CDate(reader("Dia_Permiso")).ToString("dd/MM/yyyy"),
                                reader("Horas_Permiso").ToString(),
                                tipo,
                                CDate(reader("Fecha_Registro")).ToString("dd/MM/yyyy HH:mm")
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

            ' Ajustar fechas según tipo de reporte
            If rbSemanal.Checked Then
                ' Reporte semanal (últimos 7 días)
                fechaDesde = Date.Today.AddDays(-7)
                fechaHasta = Date.Today
                dtpDesde.Value = fechaDesde
                dtpHasta.Value = fechaHasta
            ElseIf rbMensual.Checked Then
                ' Reporte mensual (mes actual)
                fechaDesde = New Date(Date.Today.Year, Date.Today.Month, 1)
                fechaHasta = Date.Today
                dtpDesde.Value = fechaDesde
                dtpHasta.Value = fechaHasta
            End If

            Using conn As New OleDbConnection(connectionString)
                conn.Open()
                Dim sql As String = "SELECT Nombre, RUT, Funcion, Dia_Permiso, Horas_Permiso, Con_Sueldo, Sin_Sueldo, Fecha_Registro " &
                                   "FROM Permisos WHERE Fecha_Registro BETWEEN ? AND ? ORDER BY Nombre, Fecha_Registro"

                Using cmd As New OleDbCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Desde", fechaDesde)
                    cmd.Parameters.AddWithValue("@Hasta", fechaHasta)

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        ' Crear nuevo formulario para el reporte
                        Dim frmReporte As New Form()
                        frmReporte.Text = "Reporte de Permisos (" & fechaDesde.ToString("dd/MM/yyyy") & " - " & fechaHasta.ToString("dd/MM/yyyy") & ")"
                        frmReporte.Size = New Size(900, 600)

                        Dim dgvReporte As New DataGridView()
                        dgvReporte.Dock = DockStyle.Fill
                        dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                        ' Agregar columnas
                        dgvReporte.Columns.Add("Nombre", "Nombre")
                        dgvReporte.Columns.Add("RUT", "RUT")
                        dgvReporte.Columns.Add("Funcion", "Función")
                        dgvReporte.Columns.Add("Dia_Permiso", "Fecha Permiso")
                        dgvReporte.Columns.Add("Horas", "Horas")
                        dgvReporte.Columns.Add("Tipo", "Tipo")
                        dgvReporte.Columns.Add("Fecha_Registro", "Fecha Registro")

                        ' Llenar datos
                        While reader.Read()
                            Dim tipo As String = ""
                            If CBool(reader("Con_Sueldo")) Then
                                tipo = "Con Sueldo"
                            Else
                                tipo = "Sin Sueldo"
                            End If

                            dgvReporte.Rows.Add(
                                reader("Nombre").ToString(),
                                reader("RUT").ToString(),
                                reader("Funcion").ToString(),
                                CDate(reader("Dia_Permiso")).ToString("dd/MM/yyyy"),
                                reader("Horas_Permiso").ToString(),
                                tipo,
                                CDate(reader("Fecha_Registro")).ToString("dd/MM/yyyy HH:mm")
                            )
                        End While

                        ' Agregar botón de exportar
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
        Try
            Dim excel As New Microsoft.Office.Interop.Excel.Application()
            excel.Visible = True
            Dim workbook As Microsoft.Office.Interop.Excel.Workbook = excel.Workbooks.Add()
            Dim worksheet As Microsoft.Office.Interop.Excel.Worksheet = workbook.Sheets(1)

            ' Encabezados
            For i As Integer = 0 To dgv.Columns.Count - 1
                worksheet.Cells(1, i + 1).Value = dgv.Columns(i).HeaderText
            Next

            ' Datos
            For i As Integer = 0 To dgv.Rows.Count - 1
                For j As Integer = 0 To dgv.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1).Value = dgv.Rows(i).Cells(j).Value
                Next
            Next

            ' Formato
            worksheet.Rows(1).Font.Bold = True
            worksheet.Columns.AutoFit()

            MessageBox.Show("Reporte exportado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error al exportar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        CargarPermisos()
    End Sub
End Class