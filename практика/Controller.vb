
Public Class Controller
    'важнейшие свойства
    Public WithEvents T As Timer                    'таймер
    Private NewCalls As Collection                  'новые вызовы: коллекция людей, ожидающих лифта (отсортировано по возрастанию этажа)
    Private Load As Single                          'нагрузка: среднее количество новых вызовов за такт работы
    Private NumOfFloors As Byte                     'количество этажей в здании
    Private Lifts As Collection                     'коллекция лифтов
    Private CreationRatio As Single                 'соотношение вероятности появления нового вызова на 
    '       первом этаже к вероятности появления нового вызова на других этажах

    'выбор оптимального лифта
    Private Const IndForParkingLifts As Short = 5       'параметр для лифтов, едущих на стоянку
    Private Const IndForSleepingLifts As Short = 5      'параметр для спящих лифтов
    Private Const IndForNewChoice As Short = 4          'параметр для потенциального нового лифта
    Private Const MaxOptValue As Short = 199            'максимально возможное значение, при котором лифту
    '       передается данный вызов

    'для статистики
    Private WaitingTimes As Collection                  'коллекция количества тактов ожидания
    Private MenNumber As UInteger                       'общее количество людей, воспользовавшихся лифтами

    'для облегчения работы
    Private Const MoveUp As Lift.LState = Lift.LState.MoveUp        'направление движение лифта: вверх
    Private Const MoveDown As Lift.LState = Lift.LState.MoveDown    'направление движение лифта: вниз
    Private Const NotMoving As Lift.LState = Lift.LState.NotMoving  'направление движение лифта: стоит на месте

    '**************************************************************************************************
    'Создает новый экземпляр класса Controller
    '**************************************************************************************************
    'Вход:  коллекция лифтов здании, объекты класса Lift
    '       нагрузка: среднее количество новых вызовов за такт, тип Single; по умолчанию 0,2
    '       соотношение вероятности появления нового вызова на первом этаже к вероятности появления
    '           нового вызова на других этажах, по умолчанию 1, тип Single
    '       количество этажей в здании, по умолчанию 20, тип Byte
    '       интервал одного такта работы контроллера в миллисекундах, по умолчанию 1000, тип UShort
    '**************************************************************************************************
    Public Sub New(ByVal LiftsCol As Collection, Optional ByVal SLoad As Single = 0.2, Optional ByVal SCreationRatio As Single = 1, Optional ByVal NFloors As Byte = 20, Optional ByVal UshTInterval As UShort = 1000)
        NewCalls = New Collection
        WaitingTimes = New Collection
        Lifts = LiftsCol
        Load = SLoad
        CreationRatio = SCreationRatio
        NumOfFloors = NFloors
        T = New Timer
        T.Interval = UshTInterval
        T.Start()
    End Sub

    '***********************************************************************************************
    'Обработчик таймера; по факту эмулирует работу контроллера
    'В конце каждого такта выбрасывает событие NewState
    '***********************************************************************************************
    Sub Timer_EventHandler() Handles T.Tick

        Randomize()
        '1: обработка лифтов (физическое перемещение в случае необходимости)
        ProcessLifts()

        '2: создание новых вызовов
        CreateNewCalls()

        '3: обработка вызовов
        ProcessCalls()
        RaiseEvent NewState(NewCalls, Lifts)
    End Sub

    '**************************************************************************************************
    'Обрабатывает лифты (перемещает их, меняет их направление, усыпляет и пр.)
    '**************************************************************************************************
    Private Sub ProcessLifts()
        For Each L As Lift In Lifts
            If L.State = NotMoving Then ProcessSleepLift(L)
            'если лифт двигался (возможно, его только что отправили на стоянку)
            If Not (L.State = NotMoving) Then ProcessWorkLift(L)
        Next
    End Sub

    '**************************************************************************************************
    'Обрабатывает спящий лифт, в случае необходимости отправляет его на место стоянки
    '(задает направление перемещения, но НЕ выполянет физического перемещения)
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '**************************************************************************************************
    Private Sub ProcessSleepLift(ByRef L As Lift)
        Dim NewSt As Lift.LState

        If Not (L.State = NotMoving) Then Exit Sub

        L.WaitingTime += 1

        'если лифт остановился и выпустил людей:
        If L.WaitingTime = 1 And L.OpenDoor Then L.OpenDoor = False

        'если параметры лифта предусматривают стоянку для сна
        If Not (L.BehaviorWhenEmpty = -1) And L.WaitingTime = CShort(L.MaxWaitingTime) Then
            NewSt = L.HowGoToParking(NumOfFloors)
            If Not (NewSt = NotMoving) Then
                L.WaitingTime = -1
                FromSleepToWork(L, NewSt)
            End If
        End If

    End Sub

    '**************************************************************************************************
    'Обрабатывает работающий лифт: выпускает людей из лифта, запускает их в лифт, двигает лифт на 
    'этаж вниз/вверх, усыпляет его
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '**************************************************************************************************
    Private Sub ProcessWorkLift(ByRef L As Lift)

        If L.State = NotMoving Then Exit Sub

        '1: выпустить людей
        If PeopleGoOut(L) Then
            L.OpenDoor = True
            Exit Sub
        End If

        '2: сменить направление в случае необходимости (используется при незапланированном заходе людей)
        If L.IsItTimeToTurn() Then L.TurnLift()

        '3: запустить людей в лифт
        If PeopleGoIn(L) Then
            L.OpenDoor = True
            Exit Sub
        Else
            L.OpenDoor = False
        End If

        '4: смена физического состояния лифта

        If L.MenInLiftNum = 0 And L.FloorCallsNum = 0 And L.WaitingTime > -1 Then
            FromWorkToSleep(L)
        End If

        If L.IsItTimeToTurn() Then
            L.TurnLift()
            L.MoveLift()
        Else
            If L.WaitingTime = -1 And L.HowGoToParking(NumOfFloors) = NotMoving Then
                FromWorkToSleep(L)
            Else
                L.MoveLift()
            End If
        End If
    End Sub

    '**************************************************************************************************
    'Усыпляет лифт, изменяя соответствующие свойства лифта
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '**************************************************************************************************
    Private Sub FromWorkToSleep(ByRef L As Lift)
        L.WaitingTime = 0
        L.OpenDoor = False
        L.State = NotMoving
    End Sub

    '**************************************************************************************************
    'Изменяет состояние лифта на одно из работающих, НЕ выполняя физического перемещения лифта
    'Возбуждает исключение типа ArgumentException, если состояние, в которое следует перевести
    'лифт, Lift.LState.NotMoving
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '       состояние, в которое следует перевести лифт
    '**************************************************************************************************
    Private Sub FromSleepToWork(ByRef L As Lift, ByVal NewSt As Lift.LState)
        If NewSt = NotMoving Then Throw New ArgumentException
        If L.WaitingTime > 0 Then L.WaitingTime = 0
        L.State = NewSt
    End Sub

    '**************************************************************************************************
    'Выполняет обработку вызовов
    '**************************************************************************************************
    Private Sub ProcessCalls()
        Dim BestChoiceLN As Byte                'номер лифта, являющегося лучшим вариантом для человека
        Dim MustGetNewChoice As Boolean         'выбор для предыдущих вызовов не подходит

        Dim M As Man

        For i = NewCalls.Count() To 1 Step -1
            M = NewCalls(i)
            NewCalls(i).IncreaseWaitingTime()
            MustGetNewChoice = True
            '1: глушитель вызовов с одного и того же этажа
            If i < NewCalls.Count() Then
                If M.Beginning = NewCalls(i + 1).Beginning Then
                    MustGetNewChoice = False
                    M.WaitForLiftN = NewCalls(i + 1).WaitForLiftN
                End If
            End If
            If MustGetNewChoice Then
                '2: выбор лифта
                BestChoiceLN = ChooseBestLift(M)
                '3: устранение последствий, если в прошлом такте был выбран другой лифт в качестве оптимального
                If Not (BestChoiceLN = M.WaitForLiftN) Then
                    'обработка лифта, который перестал быть оптимальным
                    If M.WaitForLiftN > 0 Then ProcessWorseLift(Lifts(M.WaitForLiftN), M.Beginning)
                    'обработка оптимального лифта
                    If BestChoiceLN > 0 Then ProcessBestLift(Lifts(BestChoiceLN), M.Beginning)
                    'обработка человека
                    M.WaitForLiftN = BestChoiceLN
                End If
            End If
        Next
    End Sub

    '**************************************************************************************************
    'Обрабатывает лифт после поступления вызова: разворачивает его в случае необходимости,
    'добавляет этаж в список вызовов
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '       номер этажа, с которого поступил вызов, тип Byte
    '**************************************************************************************************
    Private Sub ProcessBestLift(ByRef L As Lift, ByVal Floor As Byte)
        L.AddNewFloorCall(Floor)

        If L.State = NotMoving Or L.WaitingTime = -1 Then
            If L.Floor > Floor Then
                L.State = MoveDown
            Else
                L.State = MoveUp
            End If
            L.WaitingTime = 0
        End If
    End Sub

    '**************************************************************************************************
    'Обрабатывает лифт после отмены вызова: разворачивает, усыпляет его в случае необходимости,
    'удаляет этаж из списка вызовов
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '       номер этажа, с которого отменили вызов, тип Byte
    '**************************************************************************************************
    Private Sub ProcessWorseLift(ByRef L As Lift, ByVal Floor As Byte)
        Dim NumFC As Byte   'количество вызовов

        L.RemoveFloorCall(Floor)
        NumFC = L.FloorCallsNum

        If L.MenInLiftNum = 0 And NumFC = 0 Then FromWorkToSleep(L)
        'обработка физического состояния будет сделана ProcessLifts в этом такте
    End Sub

    '**************************************************************************************************
    'Запускает людей в лифт
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '**************************************************************************************************
    'Выход:	Признак успешности операции: True - хотя бы один человек зашел, False - иначе
    '**************************************************************************************************
    Private Function PeopleGoIn(ByRef L As Lift) As Boolean
        Dim Res As Boolean                  'результат работы функции
        Dim WasBanned As Boolean            'показывает, что лифт не может взять человека из-за перегрузки
        Dim Dir As Short                    'для определения направления движения человека
        Dim M As Man

        If LiftMustTakePeople(L) Then
            For i = NewCalls.Count() To 1 Step -1
                M = NewCalls(i)
                If L.Floor = M.Beginning Then
                    If L.LetGoIn(M) Then
                        MenNumber += 1
                        WaitingTimes.Add(NewCalls(i).GetWaitingTime())
                        NewCalls.Remove(i)
                    End If
                End If
            Next
            L.RemoveFloorCall(L.Floor)
            Res = True
        ElseIf L.OpenDoor Then
            'в предыдущем такте кто-то вышел из лифта (или зашел в него) на этом этаже, дери лифта открыты
            For i = NewCalls.Count() To 1 Step -1
                M = NewCalls(i)
                If M.Beginning = L.Floor Then
                    WasBanned = False
                    For Each BM As Man In L.BannedMen
                        If M = BM Then
                            WasBanned = True
                            Exit For
                        End If
                    Next
                    If Not WasBanned Then
                        Dir = CShort(M.Destination) - CShort(M.Beginning)
                        If L.MenInLiftNum = 0 Or (L.State = MoveUp And Dir > 0) Or (L.State = MoveDown And Dir < 0) Then
                            If L.LetGoIn(M) Then
                                NewCalls.Remove(i)
                                If M.WaitForLiftN > 0 Then Lifts(M.WaitForLiftN).RemoveFloorCall(M.Beginning)
                                Res = True
                            End If
                        End If
                    End If
                End If
            Next
        End If
        Return Res
    End Function

    '**************************************************************************************************
    'Показывает, что лифт должен остановиться и подобрать людей на данном этаже
    '**************************************************************************************************
    'Вход:  объект типа Lift
    '**************************************************************************************************
    'Выход: True - должен остановиться и подобрать, False - не должен
    '**************************************************************************************************
    Private Function LiftMustTakePeople(ByVal L As Lift)
        If Not L.FloorCalls.Contains(L.Floor) Then Return False
        'не совпадает этаж или лифт не должен останавливаться на этом этаже

        If L.Floor = 1 Then Return True
        'на первом этаже люди всегда заходят

        If L.State = MoveDown And L.MenInLiftNum > 0 And Not L.StopWhenGoDown Then Return False
        'лифт движется на верхних этажах набитым вниз и не должен пускать людей

        If L.State = MoveUp And L.MenInLiftNum > 0 Then Return False
        'лифт движется на верхних этажах еще выше с людьми

        If L.State = MoveUp And L.MenInLiftNum = 0 And (L.AboveFloorWhenEmpty) And (L.FloorCalls(L.FloorCallsNum) > L.Floor) Then Return False
        'лифт двигается пустой за самым верхних человеком, и этот человек не является самым верхним

        If L.State = MoveDown And L.MenInLiftNum = 0 And Not (L.AboveFloorWhenEmpty) And L.FloorCalls(1) < L.Floor And L.FloorCalls(1) > 1 Then Return False
        'лифт двигается вниз за самым нижним человеком (не первый этаж), и этот человек не является самым нижним
        Return True
    End Function

    '**************************************************************************************************
    'Выпускает людей из лифта
    '**************************************************************************************************
    'Вход:  объект типа Lift, по ссылке
    '**************************************************************************************************
    'Выход:	признак успешности операции: True - хотя бы один человек вышел, False - иначе
    '**************************************************************************************************
    Private Function PeopleGoOut(ByRef L As Lift) As Boolean
        If L.LetGoOut() Then Return True
        Return False
    End Function

    '**************************************************************************************************
    'Выбирает наиболее оптимальный лифт для данного человека
    '**************************************************************************************************
    'Вход:  объект типа Man из списка новых вызовов
    '**************************************************************************************************
    'Выход: номер наиболее оптимального лифта в коллекции лифтов; 0 - если таких лифтов не нашлось
    '**************************************************************************************************
    Private Function ChooseBestLift(ByVal M As Man) As Byte
        Dim NumOfBestLift As Byte           'номер оптимального лифта
        Dim OptValue As Short               'значение параметра для данного лифта
        Dim MinOptValue As Short            'минимален для оптимального лифта

        MinOptValue = MaxOptValue + 1

        For i = 1 To Lifts.Count()
            OptValue = ReturnOptValue(M, i)
            If OptValue < MinOptValue Then
                NumOfBestLift = i
                MinOptValue = OptValue
            End If
        Next
        Return NumOfBestLift
    End Function

    '**************************************************************************************************
    'Возвращает значение оптимальности лифта (чем меньше, тем лучше)
    '**************************************************************************************************
    'Вход: объект типа Man, номер лифта в коллекции Lifts, тип Byte
    '**************************************************************************************************
    'Выход: оптимальность, тип Short
    '**************************************************************************************************
    Private Function ReturnOptValue(ByVal M As Man, ByVal LN As Byte) As Short
        Dim OptValue As Short                           'параметр, по которому будет происходит выбор лифта
        '       чем меньше значение, тем лучше
        Dim MILN As Short                               'количество людей в лифте
        Dim LS As Lift.LState                           'состояние лифта
        Dim LF As Short                                 'этаж, на котором находится лифт
        Dim MF As Short                                 'этаж, на котором находится человек
        Dim SWGD As Boolean                             'останавливаться, когда лифт движется вниз
        Dim AF As Boolean                               'подниматься на самый высокий этаж
        Dim WT As Short                                 'время сна лифта
        Dim MW As Short                                 'максимальная грузоподъемность лифта
        Dim W As Short                                  'текущий вес лифта
        Dim CN As Short                                 'количество ждущих вызовов
        Dim FC As Short                                 'индекс вызова, который нижний (не с первого этажа)
        Dim LC As Short                                 'индекс вызова, который верхний (не с первого этажа)
        Dim FD As Short                                 'номер нижнего этажа назначения (не первый этаж)
        Dim LD As Short                                 'номер верхнего этажа назначения (не первый этаж)
        Dim AverageMN As Short                          'среднее количество человек, помещающихся в лифт
        Dim NewChoice As Boolean                        'показывает, что в прошлом такте этот лифт не был лучшим
        Dim A As Short
        Dim L As Lift

        L = Lifts(LN)

        'человек не влез в лифт в прошлых тактах
        For Each BM As Man In L.BannedMen
            If M = BM Then Return MaxOptValue + 1
        Next

        MILN = CShort(L.MenInLiftNum)
        LS = L.State
        SWGD = L.StopWhenGoDown
        AF = L.AboveFloorWhenEmpty
        LF = CShort(L.Floor)
        WT = CShort(L.WaitingTime)
        MW = CShort(L.MaxWeight)
        W = CShort(L.Weight)
        CN = CShort(L.FloorCallsNum)
        MF = CShort(M.Beginning)
        AverageMN = MW / CShort(Man.AverageWeight)

        For A = 1 To CN
            If L.FloorCalls(A) > 1 Then
                FC = CShort(L.FloorCalls(A))
                Exit For
            End If
        Next

        If CN Then
            If L.FloorCalls(CN) > 1 Then LC = CShort(L.FloorCalls(CN))
        End If

        For A = 1 To MILN
            If L.MenInLift(A).Destination > 1 Then
                FD = CShort(L.MenInLift(A).Destination)
                Exit For
            End If
        Next

        If MILN Then
            If L.MenInLift(MILN).Destination > 1 Then LD = CShort(L.MenInLift(MILN).Destination)
        End If

        'чтобы не выбирал каждый раз новые лифты при малом изменении выгоды
        If LN <> M.WaitForLiftN Then
            NewChoice = True
            OptValue += IndForNewChoice
        End If

        'перехватывать лифт, когда он едет на стоянку - плохо
        If WT = -1 Then
            OptValue += IndForParkingLifts
            OptValue += Math.Abs(LF - MF)
            Return OptValue
        End If

        'заставлять работать спящий лифт - плохо
        If WT > 0 Then
            OptValue += IndForSleepingLifts
            OptValue += Math.Abs(LF - MF)
            Return OptValue
        End If

        'человек на первом этаже ждет лифта
        If MF = 1 Then
            If LF = 1 Then
                Return 0
            ElseIf LS = MoveUp And (CN + MILN) > 0 Then
                A = CShort(Math.Max(LC, LD))
                OptValue += MILN + 2 * A - LF - 1
            Else
                OptValue += MILN + LF - 1
            End If
            If NewChoice Then
                OptValue += CN
            Else
                OptValue += CN - 1
            End If
            Return OptValue
        End If

        'чем больше людей в лифте, тем хуже
        'чем больше вызовов у лифта, тем хуже
        'чем больше грузоподъемность лифта - тем лучше
        OptValue += MILN + 2 * CN - AverageMN

        'остались только варианты, когда лифт едет за людьми или с людьми
        If AF And SWGD Then
            'подъезжает к самому верхнему и останавливается при спуске
            If NewChoice And ((CN + 1 > AverageMN) Or (MF > FC And FC > 0 And CN + 2 > AverageMN)) Then Return MaxOptValue + 1
            If LS = MoveUp Then
                A = CShort(Math.Max(LC, LD))
                OptValue += Math.Abs(MF - A) + Math.Abs(A - LF)
            Else
                If MILN > 0 Then
                    OptValue += LF - 1 + Math.Abs(MF - LC)
                Else
                    OptValue += Math.Abs(LF - MF)
                End If
            End If
        ElseIf (Not AF) And SWGD Then
            'подъезжает к самому нижнему и останавливается при спуске
            If LS = MoveUp Then
                If FC > 0 And NewChoice Then Return MaxOptValue + 1
                If MILN > 0 Then
                    OptValue = Math.Abs(LF - LD) + Math.Abs(LD - MF)
                End If
            ElseIf MF > LF And LS = MoveDown Then
                If FC > 0 And NewChoice Then Return MaxOptValue + 1
                If MILN > 0 Then
                    OptValue += LF + MF - 2
                End If
            ElseIf MF <= LF And LS = MoveDown Then
                If FC > 0 And LF < FC And NewChoice Then Return MaxOptValue + 1
                If MILN > 0 Then
                    OptValue += LS - MF
                End If
            End If
        ElseIf AF And (Not SWGD) Then
            'подъезжает к самому верхнему и не останавливается при спуске
            If FC > 0 And FC < MF And NewChoice Then Return MaxOptValue + 1

            For i = CN To 1 Step -1
                If L.FloorCalls(i) > MF Then
                    OptValue += 2 * L.FloorCalls(i) - 2
                Else
                    Exit For
                End If
            Next

            If LS = MoveUp Then
                If MILN > 0 Then
                    OptValue += 2 * LD - LF - 1
                Else
                    OptValue -= LF - 1
                End If
            ElseIf LS = MoveDown Then
                If MILN > 0 Then
                    OptValue += LF - 1
                Else
                    OptValue += LF - LC
                End If
            End If
            OptValue += MF - 1
        Else
            'подъезжает к самому нижнему и не останавливается при спуске
            If MF < LC And NewChoice Then Return MaxOptValue + 1

            For i = 1 To CN
                If L.FloorCalls(i) > MF Then
                    OptValue += 2 * L.FloorCalls(i) - 2
                Else
                    Exit For
                End If
            Next

            If LS = MoveUp Then
                If MILN > 0 Then
                    OptValue += 2 * LD - LF - 1
                Else
                    OptValue -= LF - 1
                End If
            ElseIf LS = MoveDown Then
                If MILN > 0 Then
                    OptValue += LF - 1
                Else
                    OptValue += LF - FC
                End If
            End If
            OptValue += MF - 1
        End If

        Return OptValue
    End Function

    '**************************************************************************************************
    'Добавляет новых людей в коллекцию вызовов NewCalls, сохраняя сортировку по возрастанию этажа
    '**************************************************************************************************
    Private Sub CreateNewCalls()
        Dim x As Byte
        Dim M As Man

        If Rnd() < (Load Mod 1) Then x = 1

        'создание новых вызовов
        For i = 1 To Fix(Rnd() * Fix(Load + 1) + x)
            M = RndMan()
            'добавление так, чтобы оставалась по возрастанию Beginning (используется в глушителе вызовов)
            For j = 1 To NewCalls.Count()
                If NewCalls(j).Beginning > M.Beginning Then
                    NewCalls.Add(M, Before:=j)
                    Exit For
                End If
            Next
            'если это первый вызов, то код выше не сработает, поэтому:
            If NewCalls.Count() = 0 Then NewCalls.Add(M)
        Next
    End Sub

    '**************************************************************************************************
    'Возвращает объект типа Man, пригодный для работы контроллера
    '**************************************************************************************************
    'Выход: объект типа Man
    '**************************************************************************************************
    Private Function RndMan() As Man
        Dim BeginFl, EndFl, Weight As Byte
        Randomize()

        'установка BeginFl и EndFl
        If Fix(Rnd() * 100) < Fix(100 * CreationRatio) Then
            'человек появляется на первом этаже и едет куда-то вверх
            BeginFl = 1
            EndFl = Fix(Rnd() * (NumOfFloors - 1)) + 2
        Else
            'человек появляется на других этажах
            BeginFl = Fix(Rnd() * (NumOfFloors - 1)) + NumOfFloors \ 3 'Чтобы разогнать народ(скапливается внизу) такой вот рандом...
            If BeginFl > NumOfFloors Then
                BeginFl = NumOfFloors
            End If
            If Fix(Rnd() * NumOfFloors) = 0 Then
                'есть некоторая вероятность, что человек поедет не на 1 этаж
                Do
                    EndFl = Fix(Rnd() * (NumOfFloors - 1)) + 2
                Loop While EndFl = BeginFl
            Else
                EndFl = 1
            End If
        End If

        'установка веса человека
        Weight = Man.AverageWeight / 2 + Fix(Rnd() * Man.AverageWeight)

        'периодически появляются люди с колясками и прочими тяжелыми вещами
        If Fix(Rnd() * 20) = 0 Then
            Weight += Fix(Rnd() * Man.AverageWeight)
        End If

        Return New Man(BeginFl, EndFl, Weight)
    End Function

    '**************************************************************************************************
    'Возвращает статистику по времени ожидания
    '**************************************************************************************************
    'Выход:	коллекция тактов ожидания людей, пользовавшихся лифтами
    '**************************************************************************************************
    Public Function GetTimeStatistics() As Collection
        Return WaitingTimes
    End Function

    '**************************************************************************************************
    'Возвращает статистику по количеству людей
    '**************************************************************************************************
    'Выход:	количество людей, воспользовавшихся лифтам, тип UInteger
    '**************************************************************************************************
    Public Function GetMenNumberStatistics() As UInteger
        Return MenNumber
    End Function

    '**************************************************************************************************
    'Оповещает об изменении состояния системы
    '**************************************************************************************************
    'Вход:  коллекция людей, ожидающих лифта
    '       коллекция лифтов в здании
    '**************************************************************************************************
    Event NewState(ByVal Men As Collection, ByVal Lifts As Collection)

End Class

