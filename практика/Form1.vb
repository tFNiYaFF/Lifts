Public Class Form1
    Private HeavyLifts As Byte ' количество грузовых лифтов, тип Byte
    Private AllLifts As Byte 'общее кол-во лифтов
    Private Floors As Byte '       количество этажей в здании, по умолчанию 10, тип Byte
    Private Capacity As Single '       нагрузка (среднее количество новых вызовов за такт), по умолчанию 2, тип Byte
    Public WithEvents MyController As Controller
    'Массивы с визуальными элементами - используются в неск.подпрограммах
    Private ArrLabelLifts() As Label 'лейблы, показывающие кол-во человек в лифте
    Private ArrPictLift() As PictureBox 'поверх лейбла - картинка-коробочка.
    Private ColLabelPeople As New Collection 'люди, появл.на этаже (цифра с местом назначения)
    Private ArrLabelFloors() As Label 'Нумерация этажей
    Private ArrPictLines() As PictureBox 'Горизонтальные полосы
    Private ArrPictShaft(,) As PictureBox 'Шахты лифтов
    Private BlnTryForBtn2 As Boolean 'индикатор, нажимали ли на кнопку старт
    Private PictVertical(1) As PictureBox
    Private Seconds As Integer 'Для красивого отображения времени в статистике
    Private Minutes As Integer
    Private Hours As Integer
    Private energySum As Integer
    Private CurrentMode As Boolean  ' Режим отображения: true - отображаем лифты, false - только собираем статистику.
    Private Iteration As Integer 'Количество итераций таймера в случае, если у нас только сбор статистики.
    Private FullTime As Integer
    '*******************************************************************************
    'Замечание: сразу после отправки данных в контроллер переменные AllLifts и Floors уменьшаются
    'на единицу, нумерация этих 2 параметров идет с нуля (для облегчения работы с массивами)
    '*******************************************************************************
    'При загрузке формы:
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Size = New Point(490, 375) '10+230+10+250+10
        Me.AutoScroll = False
        GroupBox1.Size = New Point(230, 305)
        BtnStart.Enabled = False 'Блочим кнопку, т.к. при старте нет объектов
        btnStop.Enabled = False
    End Sub
    '*************************************************************************************
    'При нажатии на кнопку "старт":
    Private Sub BtnStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnStart.Click
        НастройкиToolStripMenuItem.Enabled = False
        ЗагрузитьНастройкиToolStripMenuItem.Enabled = False
        If Form3.mode Then
            CurrentMode = True
        Else
            CurrentMode = False
            FullTime = Form3.time
        End If
        If Not CurrentMode Then 'В случае, если у нас только сбор статистики, то просто создаем контроллер
            energyUsed.Text = 0
            money.Text = 0
            AllLifts = Form2.getMyTower.Lifts
            Floors = Form2.getMyTower.Floors
            Capacity = Form2.Capacity
            MyController = New Controller(Form2.getMyLifts, Capacity, Form2.ratio, Floors, 1)
            Me.Height = 320
            Me.Width = 264
            Panel1.Visible = False
            ProgressBar1.Visible = True
            ProgressBar1.Left = 9
            ProgressBar1.Top = 250
            ProgressBar1.Width = 230
            GroupBox3.Left = 9
            GroupBox3.Top = 28
            BtnStart.Enabled = False
            btnSave.Enabled = False
            ProgressBar1.Visible = True
            ProgressBar1.Maximum = FullTime
            GroupBox3.Visible = False
            GroupBox1.Visible = False
            If Not IsNothing(ArrLabelLifts) Then 'Для первого запуска
                SessiyaVPolitehe()
                For Each i In ArrLabelLifts : i.Visible = False
                Next
                For Each i In ArrPictLift : i.Visible = False
                Next
                For Each i In ArrLabelFloors : i.Visible = False
                Next
                For Each i In ArrPictLines : i.Visible = False
                Next
                For Each i In PictVertical : i.Visible = False
                Next
            End If
            Exit Sub
        End If
        GroupBox1.Visible = True
        ProgressBar1.Visible = False
        If Not BlnTryForBtn2 Then 'Нажали нечетный раз - пошел процесс.
            If BtnStart.Tag = "Продолжить" Then
                MyController.T.Start()
                BtnStart.Tag = "Старт"
                BtnStart.Image = My.Resources.finalpause
                BlnTryForBtn2 = True
            Else
                If Not IsNothing(ArrLabelLifts) Then 'Для первого запуска
                    energyUsed.Text = 0
                    money.Text = 0
                    SessiyaVPolitehe()
                    For Each i In ArrLabelLifts : i.Visible = False
                    Next
                    For Each i In ArrPictLift : i.Visible = False
                    Next
                    For Each i In ArrLabelFloors : i.Visible = False
                    Next
                    For Each i In ArrPictLines : i.Visible = False
                    Next
                    For Each i In PictVertical : i.Visible = False
                    Next
                End If
                AllLifts = Form2.getMyTower.Lifts
                Floors = Form2.getMyTower.Floors
                Capacity = Form2.Capacity
                btnSave.Enabled = True
                Panel1.Visible = True
                If Floors >= 10 Then
                    Panel1.Location = New Point(130 + 40 * (AllLifts + 1), Floors * 23 - 195)
                    GroupBox3.Location = New Point(130 + 40 * (AllLifts + 1), Floors * 23 - 141)
                    Me.Size = New Point(426 + 40 * AllLifts, Floors * 23 + 100)
                Else
                    Panel1.Location = New Point(9, Floors * 23 + 67)
                    GroupBox3.Location = New Point(9, Floors * 23 + 120)
                    Me.Size = New Point(183 + 40 * AllLifts, Floors * 23 + 355)
                End If
                AllLifts -= 1 'Дальше везде пойдут массивы, так удобнее.
                btnStop.Enabled = True  'Делаем доступной кнопку стоп
                НастройкиToolStripMenuItem.Enabled = False ' Блокируем кнопку изменения настроек
                'Записываем всю оставшуюся информацию и передаем контроллеру:

                MyController = New Controller(Form2.getMyLifts, Capacity, Form2.ratio, Floors, 1001 - freq.Value)
                GroupBox1.Size = New Point(150 + 40 * (AllLifts + 1), Floors * 23 + 25)
                GroupBox1.BringToFront()
                GroupBox3.AutoSize = True
                GroupBox3.Visible = True
                BtnStart.Image = My.Resources.finalpause
                'Интерфейс:
                '*******************************************************************************
                LblTime.Text = 0
                LblPeopleCounter.Text = 0
                LblMinTime.Text = 0
                LblMaxTime.Text = 0
                LblAverageTime.Text = 0
                Floors -= 1 'Дальше пойдут массивы, так будет удобнее.

                ReDim ArrLabelLifts(AllLifts)
                ReDim ArrPictLift(AllLifts)
                'Массивы с визуальными элементами - разово рисуются здесь
                ReDim ArrLabelFloors(Floors) 'Нумерация этажей
                ReDim ArrPictLines(Floors \ 3)  'Горизонтальные полосы
                ReDim ArrPictShaft(AllLifts, Floors) 'Шахты лифтов

                For i = 0 To Floors
                    'Расставляем этажи
                    ArrLabelFloors(i) = New Label
                    ArrLabelFloors(i).Font = New Font("Microsoft sans serif", 10)
                    ArrLabelFloors(i).Text = i + 1
                    ArrLabelFloors(i).Location = New Point(10, (Floors - i) * 23 + 10)
                    ArrLabelFloors(i).AutoSize = True
                    GroupBox1.Controls.Add(ArrLabelFloors(i))
                Next

                Dim SizeOfLine As UShort = GroupBox1.Width - 20
                For i = 0 To Floors \ 3
                    'Рисуем горизонтальные линии
                    ArrPictLines(i) = New PictureBox
                    ArrPictLines(i).Image = My.Resources.HorizontalLight_png
                    ArrPictLines(i).Size = New Point(SizeOfLine, 1)
                    ArrPictLines(i).Location = New Point(10, (Floors - i * 3 + 1) * 23 + 6)
                    GroupBox1.Controls.Add(ArrPictLines(i))
                Next

                'Рисуем вертикальные линии

                For i = 0 To 1
                    PictVertical(i) = New PictureBox
                    PictVertical(i).Image = My.Resources.Vertical_png
                    PictVertical(i).Size = New Point(1, Floors * 23 + 20)
                    PictVertical(i).Location = New Point(37 + i * 100, ArrLabelFloors(Floors).Top)
                    GroupBox1.Controls.Add(PictVertical(i))
                    PictVertical(i).BringToFront()
                Next

                For i = 0 To AllLifts
                    'Ставим лейблы-лифты
                    ArrLabelLifts(i) = New Label
                    ArrLabelLifts(i).Text = 0
                    ArrLabelLifts(i).Location = New Point(149 + i * 40, ArrLabelFloors(0).Top)
                    ArrLabelLifts(i).Font = New Font("Microsoft sans serif", 8)
                    ArrLabelLifts(i).AutoSize = True
                    ArrLabelLifts(i).TextAlign = ContentAlignment.TopLeft
                    GroupBox1.Controls.Add(ArrLabelLifts(i))

                    'Кладем картинку
                    ArrPictLift(i) = New PictureBox
                    If AllLifts + 1 - HeavyLifts >= i + 1 Then : ArrPictLift(i).Image = My.Resources.lift2
                    Else : ArrPictLift(i).Image = My.Resources.liftbig
                    End If
                    ''Локейшн переделать! Надо вымерять
                    ''Разобраться с визибл, возможно будут проблемы
                    ArrPictLift(i).Location = New Point(148 + i * 40, ArrLabelFloors(0).Top - 2)
                    ArrPictLift(i).SizeMode = PictureBoxSizeMode.AutoSize
                    GroupBox1.Controls.Add(ArrPictLift(i))
                Next
                '*******************************************************************************

                BlnTryForBtn2 = True 'Флажок. Следующий раз будет четным.
            End If
        Else
            BtnStart.Tag = "Продолжить"
            'Возвращаем форму к действию:
            MyController.T.Stop()
            BtnStart.Image = My.Resources.newStart__2_
            'Очищаем форму, она должна быть пустой во время настройки пользователем
            BlnTryForBtn2 = False 'Флажок. Следующий раз будет нечетным.
        End If
    End Sub

    Private Sub SessiyaVPolitehe() 'Очистка формы от удаленных людей
        For Each i In ColLabelPeople
            i.visible = False
        Next
        ColLabelPeople.Clear()
    End Sub

    Private Function SearchForMin() As UShort
        Dim ColTimes As Collection = MyController.GetTimeStatistics
        Dim UshRes As UShort = 65535
        Dim Bln As Boolean
        For Each i In ColTimes
            If UshRes > i Then
                UshRes = i
                Bln = True
            End If
        Next
        If Bln Then Return UshRes
        Return 0
    End Function

    Private Function SearchForMax() As UShort
        Dim ColTimes As Collection = MyController.GetTimeStatistics
        Dim UshRes As UShort
        For Each i In ColTimes
            If UshRes < i Then UshRes = i
        Next
        Return UshRes
    End Function

    Private Function SearchForAverage() As UShort
        Dim ColTimes As Collection = MyController.GetTimeStatistics
        Dim UshRes As UShort
        Dim bln As Boolean
        For Each i In ColTimes
            UshRes += i
            bln = True
        Next
        If bln Then Return UshRes \ ColTimes.Count
        Return 0
    End Function
    Sub showMeIt()
        GroupBox3.Visible = True
        For Each i In Form2.getMyLifts
            energySum += i.worktime * i.power
        Next
        energyUsed.Text = Math.Round(energySum / 3600, 2)
        money.Text = Math.Round(energySum * Form2.getMyTower.Price / 3600, 2)
        Seconds += 1
        If FullTime >= 3600 Then
            Hours = FullTime \ 3600
            FullTime = FullTime Mod 3600
        End If
        If FullTime >= 60 Then
            Minutes = FullTime \ 60
            FullTime = FullTime Mod 60
        End If
        Seconds = FullTime
        LblTime.Text = String.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds)

        LblPeopleCounter.Text = MyController.GetMenNumberStatistics()

        Dim Min As String = SearchForMin()
        Min = String.Format("{0:00}:{1:00}", Min \ 60, Min Mod 60)
        LblMinTime.Text = Min

        Dim Max As String = SearchForMax()
        Max = String.Format("{0:00}:{1:00}", Max \ 60, Max Mod 60)
        LblMaxTime.Text = Max

        Dim Average As String = SearchForAverage()
        Average = String.Format("{0:00}:{1:00}", Average \ 60, Average Mod 60)
        LblAverageTime.Text = Average
        BtnStart.Enabled = True
    End Sub
    Sub Controller_EventHandler(ByVal Men As Collection, ByVal Lifts As Collection) Handles MyController.NewState
        If Not CurrentMode Then 'Если мы собираем статистику, то дальше ифа идти не нужно
            Iteration += 1
            ProgressBar1.Value += 1
            If Iteration <> FullTime Then
                Exit Sub
            End If
            'Когда время истекло, нужно все оформить
            Iteration = 0
            btnStop.Enabled = True
            ProgressBar1.Value = 0
            showMeIt()
            btnStop.PerformClick()
            Exit Sub
        End If
        SessiyaVPolitehe() 'Сперва очищаем форму от старого
        Dim ArrPeople(Floors) As Byte 'Кол-во людей, стоящих на каждом этаже
        energySum = 0
        For Each i In Form2.getMyLifts
            energySum += i.worktime * i.power 'фуфуфу
        Next
        energyUsed.Text = Math.Round(energySum / 3600, 2)
        money.Text = Math.Round(energySum * Form2.getMyTower.Price / 3600, 2)
        Seconds += 1
        If Seconds = 60 Then
            Seconds = 0
            Minutes += 1
            If Minutes = 60 Then
                Minutes = 0
                Hours += 1
            End If
        End If
        LblTime.Text = String.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds)

        LblPeopleCounter.Text = MyController.GetMenNumberStatistics()

        Dim Min As String = SearchForMin()
        Min = String.Format("{0:00}:{1:00}", Min \ 60, Min Mod 60)
        LblMinTime.Text = Min

        Dim Max As String = SearchForMax()
        Max = String.Format("{0:00}:{1:00}", Max \ 60, Max Mod 60)
        LblMaxTime.Text = Max

        Dim Average As String = SearchForAverage()
        Average = String.Format("{0:00}:{1:00}", Average \ 60, Average Mod 60)
        LblAverageTime.Text = Average


        For i = 1 To Men.Count 'Размещаем людей на форме
            ColLabelPeople.Add(New Label)

            ColLabelPeople(i).autosize = True

            If ArrPeople(Men(i).beginning - 1) < 3 Then
                ColLabelPeople(i).text = Men(i).destination
                ColLabelPeople(i).font = New Font("Microsoft sans serif", 10)
                ColLabelPeople(i).location = New Point(45 + 25 * ArrPeople(Men(i).beginning - 1), (Floors - Men(i).beginning + 1) * 23 + 10)
                ArrPeople(Men(i).beginning - 1) += 1
            Else
                ColLabelPeople(i).text = "..."
                ColLabelPeople(i).font = New Font("Microsoft sans serif", 10, FontStyle.Bold)
                ColLabelPeople(i).location = New Point(115, (Floors - Men(i).beginning + 1) * 23 + 10)
            End If

            GroupBox1.Controls.Add(ColLabelPeople(i))
        Next

        For i = 0 To AllLifts 'Ставим лифт
            ArrLabelLifts(i).Location = New Point(149 + i * 40, (Floors - Lifts(i + 1).floor + 1) * 23 + 10)
            ArrLabelLifts(i).Text = Lifts(i + 1).Meninlift.count
            ArrPictLift(i).Location = New Point(148 + i * 40, (Floors - Lifts(i + 1).floor + 1) * 23 + 8)
        Next
    End Sub

    Private Sub ИзменитьToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ИзменитьToolStripMenuItem.Click
        Form2.ShowDialog()
    End Sub

    Private Sub СохранитьНастройкиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles СохранитьНастройкиToolStripMenuItem.Click
        Dim _saveFileDialog As New SaveFileDialog()
        Dim saveStream As IO.StreamWriter
        Dim i As Integer
        Dim myLifts As Collection = Form2.getMyLifts
        _saveFileDialog.Filter = "Config files (*.cfg)|*.cfg"
        _saveFileDialog.Title = "Сохранить настройки"
        If _saveFileDialog.ShowDialog() = DialogResult.OK Then
            saveStream = New IO.StreamWriter(_saveFileDialog.OpenFile)
            saveStream.Write("|" & Form2.getMyTower.Floors)
            saveStream.Write("|" & Form2.getMyTower.Lifts)
            saveStream.Write("|" & Form2.getMyTower.LengthOneFloor)
            saveStream.Write("|" & Form2.getMyTower.Price)
            saveStream.Write("|" & Form2.ratio)
            saveStream.Write("|" & Form2.Capacity)
            For i = 1 To Form2.getMyLifts.Count
                saveStream.Write("|" & myLifts.Item(i).speed)
                saveStream.Write("|" & myLifts.Item(i).power)
                saveStream.Write("|" & myLifts.Item(i).maxweight)
                saveStream.Write("|" & myLifts.Item(i).AboveFloorWhenEmpty)
                saveStream.Write("|" & myLifts.Item(i).StopWhenGoDown)
                saveStream.Write("|" & myLifts.Item(i).BehaviorWhenEmpty)
            Next
            saveStream.Write("|")
            saveStream.Close()
        End If
    End Sub

    Private Sub ЗагрузитьНастройкиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЗагрузитьНастройкиToolStripMenuItem.Click
        Dim _openFileDialog As New OpenFileDialog()
        Dim openStream As IO.StreamReader
        Dim loadSettings As String
        _openFileDialog.Filter = "Config files (*.cfg)|*.cfg"
        _openFileDialog.Title = "Сохранить настройки"
        If _openFileDialog.ShowDialog() = DialogResult.OK Then
            openStream = New IO.StreamReader(_openFileDialog.OpenFile)
            loadSettings = openStream.ReadToEnd()
            openStream.Close()
            If setNewSettings(loadSettings) Then
                MessageBox.Show("Настройки успешно загружены!")
            Else
                MessageBox.Show("Ошибка при загрузке настроек!")
            End If
        End If
    End Sub
    Private Function setNewSettings(ByVal loadSettings As String) As Boolean 'Ставим загруженные настройки
        Dim newTower As Tower
        Dim behavior As Integer
        Dim newLifts As New Collection
        Dim stopWhenDown, afterCall As Boolean
        Dim floors, lifts, maxWeight, n, i As Integer
        Dim length, price, speed, power, ratio, capacity As Single
        Try
            floors = getNumber(loadSettings)
            lifts = getNumber(loadSettings)
            length = getNumber(loadSettings)
            price = getNumber(loadSettings)
            ratio = getNumber(loadSettings)
            capacity = getNumber(loadSettings)
            newTower = New Tower(floors, lifts, length, price)
            Form2.BCapacity.Value = capacity * 100
            If ratio = 0.05 Then
                Form2.RadioButton1.Checked = True
            End If
            If ratio = 0.4 Then
                Form2.RadioButton2.Checked = True
            End If
            If ratio = 0.95 Then
                Form2.RadioButton3.Checked = True
            Else
                Form2.RadioButton4.Checked = True
                Form2.mode.Enabled = True
                Form2.mode.Value = ratio * 100
                Form2.Label0.Enabled = True
                Form2.Label100.Enabled = True
            End If
            n = lifts
            Form2.Capacity = capacity
            Form2.ratio = ratio
            For i = 1 To n   'Записываем инфу с лифтов
                speed = getNumber(loadSettings)
                power = getNumber(loadSettings)
                maxWeight = getNumber(loadSettings)
                If getString(loadSettings) = "True" Then
                    afterCall = True
                Else
                    afterCall = False
                End If
                If getString(loadSettings) = "True" Then
                    stopWhenDown = True
                Else
                    stopWhenDown = False
                End If
                behavior = getNumber(loadSettings)
                newLifts.Add(New Lift(maxWeight, stopWhenDown, afterCall, behavior, 7, power, speed))
            Next
            Form2.getMyLifts = newLifts 'Устанавливаем настрйоки
            Form2.getMyTower = newTower
            BtnStart.Enabled = True 'Разблокируем кнопку
            btnSave.Enabled = True
        Catch
            Return False
        End Try
        Return True
    End Function
    Private Function getString(ByRef loadSettings As String) As String
        Dim result As String
        Dim pos1, pos2 As Integer
        pos1 = InStr(loadSettings, "|")
        pos2 = InStr(pos1 + 1, loadSettings, "|")
        result = Mid(loadSettings, pos1 + 1, pos2 - pos1 - 1)
        loadSettings = Mid(loadSettings, pos2)
        Return result
    End Function
    Private Function getNumber(ByRef loadSettings As String) As Single
        Dim pos1, pos2 As Integer
        Dim result As Single
        pos1 = InStr(loadSettings, "|")
        pos2 = InStr(pos1 + 1, loadSettings, "|")
        result = Mid(loadSettings, pos1 + 1, pos2 - pos1 - 1)
        loadSettings = Mid(loadSettings, pos2)
        Return result
    End Function

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        btnSave.Enabled = True
        ЗагрузитьНастройкиToolStripMenuItem.Enabled = True
        BtnStart.Tag = "Старт"
        BtnStart.Image = My.Resources.newStart__2_
        BlnTryForBtn2 = False 'Для корректной работы кнопки старт
        MyController.T.Stop()
        НастройкиToolStripMenuItem.Enabled = True
        btnStop.Enabled = False
        For Each l In Form2.getMyLifts
            'Ставим лифту стартовые настройки(т.е. начальный этаж=1  кол-во вызовов =0 и т.д.)
            l.floorCalls.clear()
            l.menInLift.clear()
            l.bannedMen.clear()
            l.floor = 1
            l.waitingtime = 0
            l.state = Lift.LState.NotMoving
            l.opendoor = False
            l.worktime = 0
            l.sleepTime_ = 0
        Next
        energySum = 0
        Hours = 0
        Minutes = 0
        Seconds = 0
    End Sub

    Private Sub freq_Scroll(sender As Object, e As EventArgs) Handles freq.Scroll
        If Not IsNothing(MyController) Then
            MyController.T.Interval = 1001 - freq.Value
        End If

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim myContent, fName As String
        fName = Form3.fileName
        myContent = "Full Time;People;Min. Time;Max. Time;AVG Time;Energy Used;Money Used;" & vbNewLine & LblTime.Text & ";" & LblPeopleCounter.Text & ";" & LblMinTime.Text & ";" & LblMaxTime.Text & ";" & LblAverageTime.Text & ";" & energyUsed.Text & ";" & money.Text & vbNewLine & "Floors:;" & Form2.getMyTower.Floors & vbNewLine & "Lifts:;" & Form2.getMyTower.Lifts & vbNewLine & "=============================================================" & vbNewLine
        IO.File.AppendAllText(fName, myContent)
    End Sub

    Private Sub ПрограммыToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ПрограммыToolStripMenuItem.Click
        Form3.ShowDialog()
    End Sub

End Class
