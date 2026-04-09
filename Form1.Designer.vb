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
        Me.dtpDiaActual = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpDiaPermiso = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.numHoras = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
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
        CType(Me.numHoras, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPermisos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SistemapermisosDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SistemapermisosDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(44, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre"
        '
        'cmbEmpleados
        '
        Me.cmbEmpleados.FormattingEnabled = True
        Me.cmbEmpleados.Location = New System.Drawing.Point(126, 24)
        Me.cmbEmpleados.Name = "cmbEmpleados"
        Me.cmbEmpleados.Size = New System.Drawing.Size(233, 21)
        Me.cmbEmpleados.TabIndex = 1
        '
        'txtRUT
        '
        Me.txtRUT.Location = New System.Drawing.Point(126, 75)
        Me.txtRUT.Name = "txtRUT"
        Me.txtRUT.ReadOnly = True
        Me.txtRUT.Size = New System.Drawing.Size(121, 20)
        Me.txtRUT.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Rut"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(361, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Funcion"
        '
        'txtFuncion
        '
        Me.txtFuncion.Location = New System.Drawing.Point(456, 79)
        Me.txtFuncion.Name = "txtFuncion"
        Me.txtFuncion.ReadOnly = True
        Me.txtFuncion.Size = New System.Drawing.Size(100, 20)
        Me.txtFuncion.TabIndex = 5
        '
        'dtpDiaActual
        '
        Me.dtpDiaActual.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDiaActual.Location = New System.Drawing.Point(126, 135)
        Me.dtpDiaActual.Name = "dtpDiaActual"
        Me.dtpDiaActual.Size = New System.Drawing.Size(121, 20)
        Me.dtpDiaActual.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(47, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Dia Actual"
        '
        'dtpDiaPermiso
        '
        Me.dtpDiaPermiso.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDiaPermiso.Location = New System.Drawing.Point(364, 134)
        Me.dtpDiaPermiso.Name = "dtpDiaPermiso"
        Me.dtpDiaPermiso.Size = New System.Drawing.Size(100, 20)
        Me.dtpDiaPermiso.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(288, 141)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Dia Permiso"
        '
        'numHoras
        '
        Me.numHoras.Location = New System.Drawing.Point(601, 134)
        Me.numHoras.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.numHoras.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numHoras.Name = "numHoras"
        Me.numHoras.Size = New System.Drawing.Size(69, 20)
        Me.numHoras.TabIndex = 10
        Me.numHoras.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(504, 141)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Numero Horas"
        '
        'rbConSueldo
        '
        Me.rbConSueldo.AutoSize = True
        Me.rbConSueldo.Location = New System.Drawing.Point(156, 192)
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
        Me.rbSinSueldo.Location = New System.Drawing.Point(456, 192)
        Me.rbSinSueldo.Name = "rbSinSueldo"
        Me.rbSinSueldo.Size = New System.Drawing.Size(76, 17)
        Me.rbSinSueldo.TabIndex = 13
        Me.rbSinSueldo.TabStop = True
        Me.rbSinSueldo.Text = "Sin Sueldo"
        Me.rbSinSueldo.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(126, 254)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 14
        Me.btnGuardar.Text = "💾 Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnReporte
        '
        Me.btnReporte.Location = New System.Drawing.Point(343, 253)
        Me.btnReporte.Name = "btnReporte"
        Me.btnReporte.Size = New System.Drawing.Size(75, 23)
        Me.btnReporte.TabIndex = 15
        Me.btnReporte.Text = "📊 Reportes"
        Me.btnReporte.UseVisualStyleBackColor = True
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Location = New System.Drawing.Point(538, 253)
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
        Me.dgvPermisos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8})
        Me.dgvPermisos.DataSource = Me.SistemapermisosDataSetBindingSource
        Me.dgvPermisos.Location = New System.Drawing.Point(12, 299)
        Me.dgvPermisos.Name = "dgvPermisos"
        Me.dgvPermisos.Size = New System.Drawing.Size(776, 150)
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
        Me.dtpDesde.Location = New System.Drawing.Point(194, 486)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(107, 20)
        Me.dtpDesde.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(88, 486)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Fecha inicio reporte"
        '
        'dtpHasta
        '
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(470, 486)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(95, 20)
        Me.dtpHasta.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(377, 492)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Fecha fin reporte"
        '
        'rbSemanal
        '
        Me.rbSemanal.AutoSize = True
        Me.rbSemanal.Location = New System.Drawing.Point(196, 550)
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
        Me.rbMensual.Location = New System.Drawing.Point(364, 550)
        Me.rbMensual.Name = "rbMensual"
        Me.rbMensual.Size = New System.Drawing.Size(105, 17)
        Me.rbMensual.TabIndex = 23
        Me.rbMensual.TabStop = True
        Me.rbMensual.Text = "Reporte mensual"
        Me.rbMensual.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 650)
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
        Me.Controls.Add(Me.rbSinSueldo)
        Me.Controls.Add(Me.rbConSueldo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.numHoras)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtpDiaPermiso)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpDiaActual)
        Me.Controls.Add(Me.txtFuncion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRUT)
        Me.Controls.Add(Me.cmbEmpleados)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.numHoras, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPermisos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SistemapermisosDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SistemapermisosDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cmbEmpleados As ComboBox
    Friend WithEvents txtRUT As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtFuncion As TextBox
    Friend WithEvents dtpDiaActual As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpDiaPermiso As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents numHoras As NumericUpDown
    Friend WithEvents Label6 As Label
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
End Class
