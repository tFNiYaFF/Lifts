Public Class Lift
	Public Floor As Byte = 1								'этаж, на котором лифт находится в этот момент
	Public OpenDoor As Boolean								'дверь лифта открыта
    Public WaitingTime As Short                             'текущее количество тактов ожидания
	'                   равно -1, если лифт движется на стоянку после периода бездействия
    Private _MenInLift As Collection                        'коллекция людей, находящихся в лифте
    '                   отсортирована по возрастанию Destination
    Private _workTime As Integer
    Private _Speed As Single
    Private sleepTime As Decimal
    Private _Power As Single
    Private _MaxWeight As UShort                            'грузоподъемность лифта (в кг)
    Private _StopWhenGoDown As Boolean                      'параметр: останавливаться при спуске лифта вниз
	Private _AboveFloorWhenEmpty As Boolean					'при вызове лифт поднимается на самый
	'                   высокий этаж (True)/на самый низкий этаж (False)
    Private _BehaviorWhenEmpty As Integer            'параметр: определяет поведение лифта во время бездействия
    Private _MaxWaitingTime As Integer                          'максимальное количество тактов ожидания (для параметра BehaviorWhenEmpty)
	Private _FloorCalls As Collection						'номера этажей, куда должен приехать лифт
	'                   отсортирована по возрастанию этажа
	Public BannedMen As Collection							'коллекция людей, которых лифт не смог взять по весу
	'очищается, когда лифт выпустил достаточное количество людей
    Private _High As Single

	Public Enum LState										 'состояние лифта
		MoveUp			'движется вверх
		MoveDown		'движется вниз
		NotMoving		'стоит
	End Enum

	Public State As LState									 'текущее состояние лифта

	'**************************************************************************************************
	'количество людей в лифте
	'**************************************************************************************************
	Public ReadOnly Property MenInLift As Collection
		Get
			Return _MenInLift
		End Get
    End Property
    Public WriteOnly Property sleepTime_ As Decimal
        Set(value As Decimal)
            sleepTime = value
        End Set
    End Property

	'**************************************************************************************************
	'максимальная грузоподъемность
	'**************************************************************************************************
	Public ReadOnly Property MaxWeight As UShort
		Get
			Return _MaxWeight
		End Get
    End Property
    Public Property High As Single
        Get
            Return _High
        End Get
        Set(ByVal value As Single)
            _High += value
        End Set
    End Property
    Public Function Power() As Single
        Return _Power
    End Function
    Public Function Speed() As Single
        Return _Speed
    End Function

	'**************************************************************************************************
	'останавливаться при спуске вниз
	'**************************************************************************************************
	Public ReadOnly Property StopWhenGoDown As Boolean
		Get
			Return _StopWhenGoDown
		End Get
    End Property
    Public Property workTime As Integer
        Get
            Return _workTime
        End Get
        Set(ByVal value As Integer)
            _workTime = value
        End Set
    End Property

	'**************************************************************************************************
	'подниматься на самый высокий этаж
	'**************************************************************************************************
	Public ReadOnly Property AboveFloorWhenEmpty As Boolean
		Get
			Return _AboveFloorWhenEmpty
		End Get
	End Property

	'**************************************************************************************************
	'поведение при бездействии
	'**************************************************************************************************
    Public ReadOnly Property BehaviorWhenEmpty As Integer
        Get
            Return _BehaviorWhenEmpty
        End Get
    End Property

	'**************************************************************************************************
	'максимальный период сна до отбытия на стоянку
	'**************************************************************************************************
    Public ReadOnly Property MaxWaitingTime As Integer
        Get
            Return _MaxWaitingTime
        End Get
    End Property

	'**************************************************************************************************
	'коллекция вызовов с этажей (отсортирована по возрастанию этажа)
	'**************************************************************************************************
	Public ReadOnly Property FloorCalls As Collection
		Get
			Return _FloorCalls
		End Get
	End Property

	'**************************************************************************************************
	'количество этажей, куда должен приехать лифт
	'**************************************************************************************************
	Public ReadOnly Property FloorCallsNum As Byte
		Get
			Return _FloorCalls.Count()
		End Get
	End Property

	'**************************************************************************************************
	'Суммарный вес всех людей в лифте, тип UShort
	'**************************************************************************************************
	Public ReadOnly Property Weight As UShort
		Get
			Weight = 0
			For Each i As Man In _MenInLift
				Weight += i.Weight
			Next
			Return Weight
		End Get
	End Property

	'**************************************************************************************************
	'Количество людей в лифте
	'**************************************************************************************************
	Public ReadOnly Property MenInLiftNum
		Get
			Return _MenInLift.Count()
		End Get
	End Property

	'**************************************************************************************************
	'Заупскает человека в лифт, если тот не перегружает лифт, добавляет в коллекцию людей в лифте
	'сохраняя ее отсортированность по возрастанию этажа
	'**************************************************************************************************
	'Вход:  человек, тип Man
	'**************************************************************************************************
	'Выход: признак успешности операции
	'**************************************************************************************************
	Public Function LetGoIn(ByVal M As Man) As Boolean
		If (Weight + M.Weight) > _MaxWeight Then
			BannedMen.Add(M)
			Return False
		End If

		For i = 1 To _MenInLift.Count()
			If _MenInLift(i).Destination > M.Destination Then
				_MenInLift.Add(M, Before:=i)
				Return True
			End If
		Next

		_MenInLift.Add(M)
		Return True
	End Function

	'**************************************************************************************************
	'Выпускает людей из лифта, очищает BannedMen по возможности
	'**************************************************************************************************
	'Выход: True - хотя бы кто-то вышел из лифта, False - иначе
	'**************************************************************************************************
	Public Function LetGoOut() As Boolean
		Dim Res As Boolean					'показывает, вышел ли хотя бы один человек из лифта
		For i = _MenInLift.Count() To 1 Step -1
			If _MenInLift(i).Destination = Floor Then
				_MenInLift.Remove(i)
				Res = True
			End If
		Next
		If Res And (Weight < MaxWeight / 2) Then BannedMen.Clear()
		Return Res
	End Function

	'**************************************************************************************************
	'Добавляет новый вызов, который должен быть обработан данным лифтом, сохраняя сортировку по
	'возрастанию начального этажа; ключом служит номер этажа; глушит одинаковые вызовы
	'**************************************************************************************************
	'Вход:  номер этажа, тип Byte
	'**************************************************************************************************
	Public Sub AddNewFloorCall(ByVal Floor As Byte)
		'очень тормозит, если с try-catch
		For i = 1 To _FloorCalls.Count()
			If _FloorCalls(i) > Floor Then
				_FloorCalls.Add(Floor, Before:=i, Key:=Floor)
				Exit Sub
			ElseIf _FloorCalls(i) = Floor Then
				Exit Sub
			End If
		Next
		_FloorCalls.Add(Floor, Key:=Floor)
	End Sub

	'**************************************************************************************************
	'Удаляет вызов из списка вызовов, которые должны быть обработаны данным лифтом,
	'если вызов там находится
	'**************************************************************************************************
	'Вход:  номер этажа, тип Byte
	'**************************************************************************************************
	Public Sub RemoveFloorCall(ByVal Floor As Byte)
		If _FloorCalls.Contains(Floor) Then _FloorCalls.Remove(Key:=Floor)
	End Sub

	'**************************************************************************************************
	'Показывает, что лифт должен сменить направление. Это может быть связано с людьми, севшими в лифт,
	'либо со списками новых вызовов лифта. Не обрабатывает лифт, когда тот едет на стоянку (возвращает
	'False).
	'**************************************************************************************************
	'Выход: True - необходимо сменить направление, False - иначе
	'**************************************************************************************************
	Public Function IsItTimeToTurn() As Boolean
		Dim Dir As Short

		If WaitingTime = -1 Then Return False
		'лифт движется на стоянку, поэтому не обрабатывается данной функцией

		If MenInLiftNum = 0 And FloorCallsNum = 0 Then Return False

		For Each M As Man In _MenInLift
			Dir = CShort(M.Beginning) - CShort(M.Destination)
			'положительно в случае, если человек изначально хотел вниз, 
			'отрицательно - иначе
			If (Dir > 0 And State = LState.MoveDown) Or (Dir < 0 And State = LState.MoveUp) Then Return False
		Next

		'в лифте люди, но всем им нужно другое направление
		If MenInLiftNum > 0 Then Return True

		'людей в лифте нет, необходимо обработать поступившие вызовы
		If AboveFloorWhenEmpty Then
			Dir = CShort(Floor) - CShort(FloorCalls(FloorCalls.Count()))
		Else
			If FloorCalls(1) = 1 And FloorCallsNum > 1 Then
				Dir = CShort(Floor) - CShort(FloorCalls(2))
			Else
				Dir = CShort(Floor) - CShort(FloorCalls(1))
			End If
		End If

		'Dir - положительно, если лифт выше человека, отрицательно - ниже
		If (Dir > 0 And State = LState.MoveUp) Or (Dir < 0 And State = LState.MoveDown) Then Return True
		Return False
	End Function


	'**************************************************************************************************
	'Создает новый экземпляр класса Lift; состояние лифта - пустой.
	'**************************************************************************************************
	'Вход:  грузоподъемность лифта в кг, тип UShort
	'       параметр: показывает, разрешены ли остановки во время спуска лифта, тип Boolean
	'       параметр: показывает, поднимается ли лифт при вызове на самый верхний этаж или на самый нижний
	'           тип Boolean (True - верхний, False - нижний)
	'       параметр: поведение лифта при бездействии, элемент EmptyBehavior
	'       параметр: количество тактов бездействия, после которого поведение лифта определяется  
	'           параметром BehavorWhenEmpty, тип Ushort
	'**************************************************************************************************
    Public Sub New(ByVal MaxW As UShort, ByVal StopWGD As Boolean, ByVal AboveFWE As Boolean, ByVal BehaviorWE As integer, ByVal TimeW As Integer, ByVal sngPower As Single, ByVal sngSpeed As Single)
        _MenInLift = New Collection
        _FloorCalls = New Collection
        BannedMen = New Collection
        State = LState.NotMoving
        _MaxWeight = MaxW
        _StopWhenGoDown = StopWGD
        _AboveFloorWhenEmpty = AboveFWE
        _BehaviorWhenEmpty = BehaviorWE
        _MaxWaitingTime = TimeW
        _Power = sngPower
        _Speed = sngSpeed
        sleepTime = 0
    End Sub

	'**************************************************************************************************
	'Показывает, в какое состояние необходимо перевести лифт, чтобы привести его к месту стоянки
	'Если поведение спящего лифта не предусматривает стоянку, возвращает LState.NotMoving
	'**************************************************************************************************
	'Вход:  количество этажей в здании, тип Byte
	'**************************************************************************************************
	'Выход: состояние лифта, LState
	'**************************************************************************************************
	Public Function HowGoToParking(ByVal NumOfFloors As Byte) As LState
        If BehaviorWhenEmpty = -1 Then
            Return LState.NotMoving
        Else
            Dim Dir As Short = BehaviorWhenEmpty
            If Dir - Floor = 0 Then
                Return LState.NotMoving
            End If
            If Dir - Floor < 0 Then
                Return LState.MoveDown
            Else
                Return LState.MoveUp
            End If
            End If
            Return True
    End Function

    '**************************************************************************************************
    'Двигает лифт в направлении, которое указано в свойствах
    '**************************************************************************************************
    Public Sub MoveLift()
        If Me.State = LState.NotMoving Then Exit Sub
        sleepTime += 1
        If sleepTime = Math.Round(Form2.getMyTower.LengthOneFloor / _Speed) Then 'К сожалению получится обработать лишь целые соотношения
            If State = LState.MoveUp Then
                Floor += 1
                sleepTime = 0
            ElseIf State = LState.MoveDown Then
                Floor -= 1
                sleepTime = 0
            End If
        End If
        Me._workTime += 1 'Количесвто тактов, отработанных лифтом
    End Sub

	'**************************************************************************************************
	'Разворачивает лифт
	'**************************************************************************************************
	Public Sub TurnLift()
		If State = Lift.LState.MoveUp Then
			State = Lift.LState.MoveDown
		ElseIf State = Lift.LState.MoveDown Then
			State = Lift.LState.MoveUp
		End If
	End Sub
End Class
