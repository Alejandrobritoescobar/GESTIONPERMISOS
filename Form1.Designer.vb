<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbEmpleados = New System.Windows.Forms.ComboBox()
        Me.txtRUT = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFuncion = New System.Windows.Forms.TextBox()
        Me.dtpFechaDesde = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpFechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rbConSueldo = New System.Windows.Forms.RadioButton()
        Me.rbSinSueldo = New System.Windows.Forms.RadioButton()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnReporte = New System.Windows.Forms.Button()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.dgvPermisos = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SistemapermisosDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SistemapermisosDataSet = New SistemaPermisos.SistemapermisosDataSet()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.rbSemanal = New System.Windows.Forms.RadioButton()
        Me.rbMensual = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbMediaJornadaPM = New System.Windows.Forms.RadioButton()
        Me.rbMediaJornadaAM = New System.Windows.Forms.RadioButton()
        Me.rbJornadaCompleta = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvPermisos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SistemapermisosDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SistemapermisosDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(179, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre"
        '
        'cmbEmpleados
        '
        Me.cmbEmpleados.FormattingEnabled = True
        Me.cmbEmpleados.Location = New System.Drawing.Point(236, 89)
        Me.cmbEmpleados.Name = "cmbEmpleados"
        Me.cmbEmpleados.Size = New System.Drawing.Size(233, 21)
        Me.cmbEmpleados.TabIndex = 1
        '
        'txtRUT
        '
        Me.txtRUT.Location = New System.Drawing.Point(524, 89)
        Me.txtRUT.Name = "txtRUT"
        Me.txtRUT.ReadOnly = True
        Me.txtRUT.Size = New System.Drawing.Size(135, 20)
        Me.txtRUT.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(489, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Rut"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(676, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Funcion"
        '
        'txtFuncion
        '
        Me.txtFuncion.Location = New System.Drawing.Point(727, 90)
        Me.txtFuncion.Name = "txtFuncion"
        Me.txtFuncion.ReadOnly = True
        Me.txtFuncion.Size = New System.Drawing.Size(180, 20)
        Me.txtFuncion.TabIndex = 5
        '
        'dtpFechaDesde
        '
        Me.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaDesde.Location = New System.Drawing.Point(254, 140)
        Me.dtpFechaDesde.Name = "dtpFechaDesde"
        Me.dtpFechaDesde.Size = New System.Drawing.Size(97, 20)
        Me.dtpFechaDesde.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(179, 142)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Fecha desde"
        '
        'dtpFechaHasta
        '
        Me.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaHasta.Location = New System.Drawing.Point(254, 175)
        Me.dtpFechaHasta.Name = "dtpFechaHasta"
        Me.dtpFechaHasta.Size = New System.Drawing.Size(97, 20)
        Me.dtpFechaHasta.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(179, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Fecha hasta"
        '
        'rbConSueldo
        '
        Me.rbConSueldo.AutoSize = True
        Me.rbConSueldo.Location = New System.Drawing.Point(26, 23)
        Me.rbConSueldo.Name = "rbConSueldo"
        Me.rbConSueldo.Size = New System.Drawing.Size(80, 17)
        Me.rbConSueldo.TabIndex = 12
        Me.rbConSueldo.TabStop = True
        Me.rbConSueldo.Text = "Con Sueldo"
        Me.rbConSueldo.UseVisualStyleBackColor = True
        '
        'rbSinSueldo
        '
        Me.rbSinSueldo.AutoSize = True
        Me.rbSinSueldo.Location = New System.Drawing.Point(26, 56)
        Me.rbSinSueldo.Name = "rbSinSueldo"
        Me.rbSinSueldo.Size = New System.Drawing.Size(76, 17)
        Me.rbSinSueldo.TabIndex = 13
        Me.rbSinSueldo.TabStop = True
        Me.rbSinSueldo.Text = "Sin Sueldo"
        Me.rbSinSueldo.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(809, 159)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 14
        Me.btnGuardar.Text = "💾 Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnReporte
        '
        Me.btnReporte.Location = New System.Drawing.Point(789, 397)
        Me.btnReporte.Name = "btnReporte"
        Me.btnReporte.Size = New System.Drawing.Size(75, 23)
        Me.btnReporte.TabIndex = 15
        Me.btnReporte.Text = "📊 Reportes"
        Me.btnReporte.UseVisualStyleBackColor = True
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Location = New System.Drawing.Point(689, 159)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(75, 23)
        Me.btnLimpiar.TabIndex = 16
        Me.btnLimpiar.Text = "Limpiar"
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'dgvPermisos
        '
        Me.dgvPermisos.AllowUserToOrderColumns = True
        Me.dgvPermisos.AutoGenerateColumns = False
        Me.dgvPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPermisos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9})
        Me.dgvPermisos.DataSource = Me.SistemapermisosDataSetBindingSource
        Me.dgvPermisos.Location = New System.Drawing.Point(12, 228)
        Me.dgvPermisos.Name = "dgvPermisos"
        Me.dgvPermisos.Size = New System.Drawing.Size(893, 150)
        Me.dgvPermisos.TabIndex = 17
        '
        'Column1
        '
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Column2"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Column3"
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "Column4"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "Column5"
        Me.Column5.Name = "Column5"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Column6"
        Me.Column6.Name = "Column6"
        '
        'Column7
        '
        Me.Column7.HeaderText = "Column7"
        Me.Column7.Name = "Column7"
        '
        'Column8
        '
        Me.Column8.HeaderText = "Column8"
        Me.Column8.Name = "Column8"
        '
        'SistemapermisosDataSetBindingSource
        '
        Me.SistemapermisosDataSetBindingSource.DataSource = Me.SistemapermisosDataSet
        Me.SistemapermisosDataSetBindingSource.Position = 0
        '
        'SistemapermisosDataSet
        '
        Me.SistemapermisosDataSet.DataSetName = "SistemapermisosDataSet"
        Me.SistemapermisosDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dtpDesde
        '
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesde.Location = New System.Drawing.Point(129, 400)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(107, 20)
        Me.dtpDesde.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(21, 407)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Fecha inicio reporte"
        '
        'dtpHasta
        '
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(374, 399)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(95, 20)
        Me.dtpHasta.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(264, 406)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Fecha fin reporte"
        '
        'rbSemanal
        '
        Me.rbSemanal.AutoSize = True
        Me.rbSemanal.Location = New System.Drawing.Point(510, 399)
        Me.rbSemanal.Name = "rbSemanal"
        Me.rbSemanal.Size = New System.Drawing.Size(105, 17)
        Me.rbSemanal.TabIndex = 22
        Me.rbSemanal.TabStop = True
        Me.rbSemanal.Text = "Reporte semanal"
        Me.rbSemanal.UseVisualStyleBackColor = True
        '
        'rbMensual
        '
        Me.rbMensual.AutoSize = True
        Me.rbMensual.Location = New System.Drawing.Point(659, 399)
        Me.rbMensual.Name = "rbMensual"
        Me.rbMensual.Size = New System.Drawing.Size(105, 17)
        Me.rbMensual.TabIndex = 23
        Me.rbMensual.TabStop = True
        Me.rbMensual.Text = "Reporte mensual"
        Me.rbMensual.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Global.SistemaPermisos.My.Resources.Resources.insignia
        Me.PictureBox1.Image = Global.SistemaPermisos.My.Resources.Resources.insignia
        Me.PictureBox1.Location = New System.Drawing.Point(24, 26)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(132, 162)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 24
        Me.PictureBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Myanmar Text", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(353, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(345, 65)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Gestor de Permisos"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbMediaJornadaPM)
        Me.GroupBox1.Controls.Add(Me.rbMediaJornadaAM)
        Me.GroupBox1.Controls.Add(Me.rbJornadaCompleta)
        Me.GroupBox1.Location = New System.Drawing.Point(385, 116)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(128, 95)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        '
        'rbMediaJornadaPM
        '
        Me.rbMediaJornadaPM.AutoSize = True
        Me.rbMediaJornadaPM.Location = New System.Drawing.Point(6, 72)
        Me.rbMediaJornadaPM.Name = "rbMediaJornadaPM"
        Me.rbMediaJornadaPM.Size = New System.Drawing.Size(114, 17)
        Me.rbMediaJornadaPM.TabIndex = 2
        Me.rbMediaJornadaPM.TabStop = True
        Me.rbMediaJornadaPM.Text = "Media Jornada PM"
        Me.rbMediaJornadaPM.UseVisualStyleBackColor = True
        '
        'rbMediaJornadaAM
        '
        Me.rbMediaJornadaAM.AutoSize = True
        Me.rbMediaJornadaAM.Location = New System.Drawing.Point(7, 49)
        Me.rbMediaJornadaAM.Name = "rbMediaJornadaAM"
        Me.rbMediaJornadaAM.Size = New System.Drawing.Size(114, 17)
        Me.rbMediaJornadaAM.TabIndex = 1
        Me.rbMediaJornadaAM.TabStop = True
        Me.rbMediaJornadaAM.Text = "Media Jornada AM"
        Me.rbMediaJornadaAM.UseVisualStyleBackColor = True
        '
        'rbJornadaCompleta
        '
        Me.rbJornadaCompleta.AutoSize = True
        Me.rbJornadaCompleta.Location = New System.Drawing.Point(7, 23)
        Me.rbJornadaCompleta.Name = "rbJornadaCompleta"
        Me.rbJornadaCompleta.Size = New System.Drawing.Size(110, 17)
        Me.rbJornadaCompleta.TabIndex = 0
        Me.rbJornadaCompleta.TabStop = True
        Me.rbJornadaCompleta.Text = "Jornada Completa"
        Me.rbJornadaCompleta.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbConSueldo)
        Me.GroupBox2.Controls.Add(Me.rbSinSueldo)
        Me.GroupBox2.Location = New System.Drawing.Point(524, 116)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(135, 95)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        '
        'Column9
        '
        Me.Column9.HeaderText = "Column9"
        Me.Column9.Name = "Column9"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 439)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.rbMensual)
        Me.Controls.Add(Me.rbSemanal)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.dtpHasta)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.dgvPermisos)
        Me.Controls.Add(Me.btnLimpiar)
        Me.Controls.Add(Me.btnReporte)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtpFechaHasta)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpFechaDesde)
        Me.Controls.Add(Me.txtFuncion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRUT)
        Me.Controls.Add(Me.cmbEmpleados)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.dgvPermisos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SistemapermisosDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SistemapermisosDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cmbEmpleados As ComboBox
    Friend WithEvents txtRUT As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtFuncion As TextBox
    Friend WithEvents dtpFechaDesde As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpFechaHasta As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents rbConSueldo As RadioButton
    Friend WithEvents rbSinSueldo As RadioButton
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnReporte As Button
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents dgvPermisos As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents rbSemanal As RadioButton
    Friend WithEvents rbMensual As RadioButton
    Friend WithEvents SistemapermisosDataSetBindingSource As BindingSource
    Friend WithEvents SistemapermisosDataSet As SistemapermisosDataSet
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label9 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbMediaJornadaAM As RadioButton
    Friend WithEvents rbJornadaCompleta As RadioButton
    Friend WithEvents rbMediaJornadaPM As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
End Class
