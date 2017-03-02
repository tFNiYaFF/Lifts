<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Weight = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.FormLiftNum = New System.Windows.Forms.NumericUpDown()
        Me.FormStopOnFloor = New System.Windows.Forms.ComboBox()
        Me.FormWhereToGo = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.FormEmptyBeh = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Power = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Speed = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Apply = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.mode = New System.Windows.Forms.TrackBar()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Price = New System.Windows.Forms.NumericUpDown()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.floorLength = New System.Windows.Forms.NumericUpDown()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.Label0 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.FormLifts = New System.Windows.Forms.NumericUpDown()
        Me.BCapacity = New System.Windows.Forms.TrackBar()
        Me.FormFloors = New System.Windows.Forms.NumericUpDown()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.Weight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FormLiftNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Power, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Speed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.mode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Price, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.floorLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FormLifts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FormFloors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Weight
        '
        Me.Weight.Increment = New Decimal(New Integer() {20, 0, 0, 0})
        Me.Weight.Location = New System.Drawing.Point(163, 170)
        Me.Weight.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.Weight.Minimum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.Weight.Name = "Weight"
        Me.Weight.Size = New System.Drawing.Size(50, 20)
        Me.Weight.TabIndex = 33
        Me.Weight.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label7.Location = New System.Drawing.Point(20, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(137, 17)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Грузоподъёмность:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label14.Location = New System.Drawing.Point(15, 69)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(145, 17)
        Me.Label14.TabIndex = 43
        Me.Label14.Text = "Обслуживать людей:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 20)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "Лифт №"
        '
        'FormLiftNum
        '
        Me.FormLiftNum.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.FormLiftNum.Location = New System.Drawing.Point(76, 10)
        Me.FormLiftNum.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.FormLiftNum.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.FormLiftNum.Name = "FormLiftNum"
        Me.FormLiftNum.Size = New System.Drawing.Size(27, 23)
        Me.FormLiftNum.TabIndex = 40
        Me.FormLiftNum.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'FormStopOnFloor
        '
        Me.FormStopOnFloor.FormattingEnabled = True
        Me.FormStopOnFloor.Items.AddRange(New Object() {"да", "нет"})
        Me.FormStopOnFloor.Location = New System.Drawing.Point(163, 94)
        Me.FormStopOnFloor.Name = "FormStopOnFloor"
        Me.FormStopOnFloor.Size = New System.Drawing.Size(80, 21)
        Me.FormStopOnFloor.TabIndex = 47
        Me.FormStopOnFloor.Text = "да"
        '
        'FormWhereToGo
        '
        Me.FormWhereToGo.FormattingEnabled = True
        Me.FormWhereToGo.Items.AddRange(New Object() {"ближайших", "сверху"})
        Me.FormWhereToGo.Location = New System.Drawing.Point(163, 69)
        Me.FormWhereToGo.Name = "FormWhereToGo"
        Me.FormWhereToGo.Size = New System.Drawing.Size(80, 21)
        Me.FormWhereToGo.TabIndex = 46
        Me.FormWhereToGo.Text = "ближайших"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label13.Location = New System.Drawing.Point(24, 44)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(136, 17)
        Me.Label13.TabIndex = 42
        Me.Label13.Text = "При простое ехать:"
        '
        'FormEmptyBeh
        '
        Me.FormEmptyBeh.FormattingEnabled = True
        Me.FormEmptyBeh.Items.AddRange(New Object() {"Никуда", "1 этаж", "2 этаж", "3 этаж ", "4 этаж", "5 этаж", "6 этаж", "7 этаж", "8 этаж", "9 этаж", "10 этаж"})
        Me.FormEmptyBeh.Location = New System.Drawing.Point(163, 40)
        Me.FormEmptyBeh.Name = "FormEmptyBeh"
        Me.FormEmptyBeh.Size = New System.Drawing.Size(80, 21)
        Me.FormEmptyBeh.TabIndex = 45
        Me.FormEmptyBeh.Text = "Никуда"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label15.Location = New System.Drawing.Point(12, 94)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(148, 17)
        Me.Label15.TabIndex = 44
        Me.Label15.Text = "Попутная остановка:"
        '
        'Power
        '
        Me.Power.DecimalPlaces = 1
        Me.Power.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Power.Location = New System.Drawing.Point(163, 144)
        Me.Power.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.Power.Name = "Power"
        Me.Power.Size = New System.Drawing.Size(40, 20)
        Me.Power.TabIndex = 49
        Me.Power.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label8.Location = New System.Drawing.Point(81, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(79, 17)
        Me.Label8.TabIndex = 48
        Me.Label8.Text = "Мощность:"
        '
        'Speed
        '
        Me.Speed.DecimalPlaces = 1
        Me.Speed.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Speed.Location = New System.Drawing.Point(163, 118)
        Me.Speed.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.Speed.Name = "Speed"
        Me.Speed.Size = New System.Drawing.Size(40, 20)
        Me.Speed.TabIndex = 51
        Me.Speed.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label9.Location = New System.Drawing.Point(87, 118)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(73, 17)
        Me.Label9.TabIndex = 50
        Me.Label9.Text = "Скорость:"
        '
        'Apply
        '
        Me.Apply.Location = New System.Drawing.Point(250, 318)
        Me.Apply.Name = "Apply"
        Me.Apply.Size = New System.Drawing.Size(74, 23)
        Me.Apply.TabIndex = 52
        Me.Apply.Text = "Сохранить"
        Me.Apply.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Weight)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.FormEmptyBeh)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.FormWhereToGo)
        Me.Panel1.Controls.Add(Me.FormStopOnFloor)
        Me.Panel1.Controls.Add(Me.FormLiftNum)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Power)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Speed)
        Me.Panel1.Location = New System.Drawing.Point(285, 9)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(261, 289)
        Me.Panel1.TabIndex = 115
        '
        'mode
        '
        Me.mode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.mode.Enabled = False
        Me.mode.Location = New System.Drawing.Point(143, 196)
        Me.mode.Maximum = 100
        Me.mode.Name = "mode"
        Me.mode.Size = New System.Drawing.Size(100, 45)
        Me.mode.TabIndex = 103
        Me.mode.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label10.Location = New System.Drawing.Point(3, 144)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 20)
        Me.Label10.TabIndex = 106
        Me.Label10.Text = "Режим"
        '
        'Price
        '
        Me.Price.DecimalPlaces = 1
        Me.Price.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Price.Location = New System.Drawing.Point(203, 116)
        Me.Price.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.Price.Name = "Price"
        Me.Price.Size = New System.Drawing.Size(40, 20)
        Me.Price.TabIndex = 38
        Me.Price.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(12, 173)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton1.TabIndex = 107
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Утро"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label5.Location = New System.Drawing.Point(78, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 17)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Цена за 1 квт*ч:"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(95, 173)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(52, 17)
        Me.RadioButton2.TabIndex = 108
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "День"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'floorLength
        '
        Me.floorLength.DecimalPlaces = 1
        Me.floorLength.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.floorLength.Location = New System.Drawing.Point(203, 88)
        Me.floorLength.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.floorLength.Name = "floorLength"
        Me.floorLength.Size = New System.Drawing.Size(40, 20)
        Me.floorLength.TabIndex = 36
        Me.floorLength.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.BackColor = System.Drawing.Color.Transparent
        Me.Label100.Enabled = False
        Me.Label100.Location = New System.Drawing.Point(220, 216)
        Me.Label100.Margin = New System.Windows.Forms.Padding(0)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(36, 13)
        Me.Label100.TabIndex = 105
        Me.Label100.Text = "вечер"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(188, 170)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(55, 17)
        Me.RadioButton3.TabIndex = 109
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "Вечер"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'Label0
        '
        Me.Label0.AutoSize = True
        Me.Label0.BackColor = System.Drawing.Color.Transparent
        Me.Label0.Enabled = False
        Me.Label0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label0.Location = New System.Drawing.Point(133, 216)
        Me.Label0.Margin = New System.Windows.Forms.Padding(0)
        Me.Label0.Name = "Label0"
        Me.Label0.Size = New System.Drawing.Size(29, 13)
        Me.Label0.TabIndex = 104
        Me.Label0.Text = "утро"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(80, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 17)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Длина пролёта:"
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(12, 196)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(101, 17)
        Me.RadioButton4.TabIndex = 110
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "Произвольный"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'FormLifts
        '
        Me.FormLifts.Location = New System.Drawing.Point(203, 65)
        Me.FormLifts.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.FormLifts.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.FormLifts.Name = "FormLifts"
        Me.FormLifts.Size = New System.Drawing.Size(40, 20)
        Me.FormLifts.TabIndex = 31
        Me.FormLifts.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'BCapacity
        '
        Me.BCapacity.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BCapacity.Location = New System.Drawing.Point(143, 239)
        Me.BCapacity.Maximum = 20
        Me.BCapacity.Name = "BCapacity"
        Me.BCapacity.Size = New System.Drawing.Size(100, 45)
        Me.BCapacity.TabIndex = 111
        Me.BCapacity.TickStyle = System.Windows.Forms.TickStyle.None
        Me.BCapacity.Value = 2
        '
        'FormFloors
        '
        Me.FormFloors.Location = New System.Drawing.Point(203, 40)
        Me.FormFloors.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.FormFloors.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.FormFloors.Name = "FormFloors"
        Me.FormFloors.Size = New System.Drawing.Size(40, 20)
        Me.FormFloors.TabIndex = 30
        Me.FormFloors.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label16.Location = New System.Drawing.Point(139, 259)
        Me.Label16.Margin = New System.Windows.Forms.Padding(0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(21, 13)
        Me.Label16.TabIndex = 113
        Me.Label16.Text = "0%"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 221)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 20)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "Нагрузка"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(131, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 17)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Лифтов:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(220, 259)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 114
        Me.Label12.Text = "100%"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(133, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 17)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Этажей:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label2.Location = New System.Drawing.Point(3, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 20)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "В здании"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.FormFloors)
        Me.Panel2.Controls.Add(Me.BCapacity)
        Me.Panel2.Controls.Add(Me.FormLifts)
        Me.Panel2.Controls.Add(Me.RadioButton4)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label0)
        Me.Panel2.Controls.Add(Me.RadioButton3)
        Me.Panel2.Controls.Add(Me.Label100)
        Me.Panel2.Controls.Add(Me.floorLength)
        Me.Panel2.Controls.Add(Me.RadioButton2)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.RadioButton1)
        Me.Panel2.Controls.Add(Me.Price)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.mode)
        Me.Panel2.Location = New System.Drawing.Point(12, 9)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(267, 289)
        Me.Panel2.TabIndex = 116
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 356)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Apply)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form2"
        Me.Text = "Настройки имитации"
        CType(Me.Weight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FormLiftNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Power, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Speed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.mode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Price, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.floorLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FormLifts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FormFloors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Weight As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents FormLiftNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents FormStopOnFloor As System.Windows.Forms.ComboBox
    Friend WithEvents FormWhereToGo As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents FormEmptyBeh As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Power As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Speed As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Apply As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents mode As System.Windows.Forms.TrackBar
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Price As System.Windows.Forms.NumericUpDown
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents floorLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents Label0 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents FormLifts As System.Windows.Forms.NumericUpDown
    Friend WithEvents BCapacity As System.Windows.Forms.TrackBar
    Friend WithEvents FormFloors As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
