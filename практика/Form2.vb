Public Class Form2
    Private myTower As Tower
    Private _ratio As Single
    Private myLifts As New Collection
    Private flag As Boolean = False    'Флаг для определения, что мы действительно сами меняем форму
    Private _capacity As Single
    Public Property Capacity As Single
        Get
            Return _capacity
        End Get
        Set(value As Single)
            _capacity = value
        End Set
    End Property
    Public Property ratio As Single
        Get
            Return _ratio
        End Get
        Set(value As Single)
            _ratio = value
        End Set
    End Property
    Public Property getMyTower As Tower
        Get
            Return myTower
        End Get
        Set(value As Tower)
            myTower = value
        End Set
    End Property
    Public Property getMyLifts As Collection
        Get
            Return myLifts
        End Get
        Set(value As Collection)
            myLifts = value
        End Set
    End Property
    Private Sub FormLifts_ValueChanged(sender As Object, e As EventArgs) Handles FormLifts.ValueChanged
        If flag Then
            FormLiftNum.Maximum = FormLifts.Value
            If FormLifts.Value > myLifts.Count Then 'Если мы увлеичили кол-во лифтов
                myLifts.Add(New Lift(300, True, False, -1, 7, 5, 1)) ' Добавляем еще один стандартный лифт
            ElseIf FormLifts.Value < myLifts.Count Then
                myLifts.Remove(myLifts.Count) ' Иначе удаляем последний лифт
            End If
        End If
    End Sub

    Private Sub Apply_Click(sender As Object, e As EventArgs) Handles Apply.Click
        Dim whereToGo, stopOnFloor As Boolean
        Dim behavior As String
        Form1.СохранитьНастройкиToolStripMenuItem.Enabled = True
        behavior = doBehavior()
        whereToGo = afterCall()
        stopOnFloor = stopOnThisFloor()
        myTower = New Tower(FormFloors.Value, FormLifts.Value, floorLength.Value, Price.Value)
        myLifts.Add(New Lift(Weight.Value, stopOnFloor, whereToGo, behavior, 7, Power.Value, Speed.Value), , , FormLiftNum.Value)
        myLifts.Remove(Convert.ToInt32(FormLiftNum.Value)) 'Для сохранения настроек последнего лифта
        flag = False
        Form1.BtnStart.Enabled = True 'Делаем доступной кнопку запуска
        Me.Hide()
    End Sub
    'Что делать при простое
    Private Function doBehavior() As String
        Dim behavior As String
        Select Case FormEmptyBeh.SelectedItem
            Case Nothing
                behavior = -1
            Case "Никуда"
                behavior = -1
            Case Else
                behavior = Mid(FormEmptyBeh.SelectedItem, 1, InStr(FormEmptyBeh.SelectedItem, "этаж") - 1)
        End Select
        Return behavior
    End Function
    'Куда ехать в первую очередь
    Private Function afterCall() As Boolean
        Dim whereTogo As Boolean
        If FormWhereToGo.SelectedItem = "сверху" Then
            whereTogo = True
        Else
            'Подразумевается следующее, но других вариантов все равно нет
            'If FormWhereToGo.SelectedItem = "ближайших" Then
            whereTogo = False
        End If
        Return whereTogo
    End Function
    'Остановка на этаже
    Private Function stopOnThisFloor() As Boolean
        Dim stopFloor As Boolean
        If FormStopOnFloor.SelectedItem = "да" Then
            stopFloor = True
        Else
            'Подразумевается следующее, но других вариантов все равно нет
            'If FormStopOnFloor.SelectedItem = "нет" Then
            stopFloor = False
        End If
        Return stopFloor
    End Function
    Private Function ReverseDoBehavior(ByVal indexOfLift As Byte) As String
        Dim behavior As String = ""
        Select Case myLifts.Item(indexOfLift).BehaviorWhenEmpty
            Case -1
                behavior = "Никуда"
            Case Else
                behavior = myLifts.Item(indexOfLift).BehaviorWhenEmpty & "этаж"
        End Select
        Return behavior
    End Function
    'Куда ехать в первую очередь
    Private Function ReverseAfterCall(ByVal indexOfLift As Byte) As String
        Dim whereTogo As String
        If myLifts.Item(indexOfLift).AboveFloorWhenEmpty = True Then
            whereTogo = "сверху"
        Else
            'Подразумевается следующее, но других вариантов все равно нет
            'If FormWhereToGo.SelectedItem = "ближайших" Then
            whereTogo = "ближайших"
        End If
        Return whereTogo
    End Function
    'Остановка на этаже
    Private Function ReverseStopOnThisFloor(ByVal indexOfLift As Byte) As String
        Dim stopFloor As String
        If myLifts.Item(indexOfLift).StopWhenGoDown = True Then
            stopFloor = "да"
        Else
            'Подразумевается следующее, но других вариантов все равно нет
            'If FormStopOnFloor.SelectedItem =  Then
            stopFloor = "нет"
        End If
        Return stopFloor
    End Function
    Private Sub FormLiftNum_ValueChanged(sender As Object, e As EventArgs) Handles FormLiftNum.ValueChanged
        If flag Then
            Dim whereToGo, stopOnFloor As Boolean
            Dim behavior As String
            Static previousLift As Byte = 1 'Какой лифт меняли до этого, для сохранения настроек(изначально 1)
            behavior = doBehavior()
            whereToGo = afterCall()
            stopOnFloor = stopOnThisFloor()
            myLifts.Add(New Lift(Weight.Value, stopOnFloor, whereToGo, behavior, 7, Power.Value, Speed.Value), , , previousLift)
            myLifts.Remove(previousLift)
            forPeople(FormLiftNum.Value)
            previousLift = FormLiftNum.Value
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _capacity = CSByte(BCapacity.Value) / 100
        floorLength.Minimum = Speed.Value
        Speed.Maximum = floorLength.Value
        flag = True
        Dim i As Integer
        FormLiftNum.Value = 1
        If myLifts.Count = 0 Then
            FormLiftNum.Maximum = 2
            For i = 0 To 1
                myLifts.Add(New Lift(300, True, False, -1, 7, 5, 1)) ' Если впервые открываем настройки, то сразу же создаем 2 стандартных лифта
            Next
        Else
            forPeople(1)
        End If
        If Not IsNothing(myTower) Then      'Если дом уже был создан, то ставим его настройки
            FormFloors.Value = myTower.Floors
            FormLifts.Value = myTower.Lifts
            floorLength.Value = myTower.LengthOneFloor
            Price.Value = myTower.Price
            FormLiftNum.Maximum = FormLifts.Value
        End If
    End Sub
    Private Sub forPeople(ByVal indexOfLift As Byte) 'Процедура делает корректное отображение текущих параметров лифта
        Dim whereToGo, stopOnFloor, behavior As String
        whereToGo = ReverseAfterCall(indexOfLift)
        stopOnFloor = ReverseStopOnThisFloor(indexOfLift)
        behavior = ReverseDoBehavior(indexOfLift)
        FormEmptyBeh.SelectedItem = behavior
        FormWhereToGo.SelectedItem = whereToGo
        FormStopOnFloor.SelectedItem = stopOnFloor
        Speed.Value = myLifts.Item(indexOfLift).speed
        Power.Value = myLifts.Item(indexOfLift).power
        Weight.Value = myLifts.Item(indexOfLift).maxWeight
    End Sub

    Private Sub Speed_ValueChanged(sender As Object, e As EventArgs) Handles Speed.ValueChanged
        floorLength.Minimum = Speed.Value
    End Sub

    Private Sub floorLength_ValueChanged(sender As Object, e As EventArgs) Handles floorLength.ValueChanged
        Speed.Maximum = floorLength.Value
    End Sub

    Private Sub BCapacity_Scroll(sender As Object, e As EventArgs) Handles mode.Scroll
        _ratio = mode.Value / 100
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged

        If RadioButton4.Checked Then
            _ratio = mode.Value / 100
            Label100.Enabled = True
            Label0.Enabled = True
            mode.Enabled = True
        Else
            Label100.Enabled = False
            Label0.Enabled = False
            mode.Enabled = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            _ratio = 0.05
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            _ratio = 0.4
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then
            _ratio = 0.95
        End If
    End Sub

    Private Sub BCapacity_Scroll_1(sender As Object, e As EventArgs) Handles BCapacity.Scroll
        _capacity = BCapacity.Value / 100
    End Sub

    Private Sub FormFloors_ValueChanged(sender As Object, e As EventArgs) Handles FormFloors.ValueChanged
        Dim i As Integer
        FormEmptyBeh.Items.Clear()
        FormEmptyBeh.Items.Add("Никуда")
        For i = 1 To FormFloors.Value
            FormEmptyBeh.Items.Add(i & "этаж")
        Next
    End Sub
End Class