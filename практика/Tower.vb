Public Class Tower
    Private _Floors As Byte
    Private _Lifts As Byte
    Private _LengthOneFloor As Single
    Private _Price As Single
    Public Function Floors() As Byte
        Return _Floors
    End Function
    Public Function Lifts() As Byte
        Return _Lifts
    End Function
    Public Function LengthOneFloor() As Single
        Return _LengthOneFloor
    End Function
    Public Function Price() As Single
        Return _Price
    End Function
    Public Sub New(ByVal bFloors As Byte, ByVal bLifts As Byte, ByVal sngLength As Single, ByVal sngPrice As Single)
        _Floors = bFloors
        _Lifts = bLifts
        _LengthOneFloor = sngLength
        _Price = sngPrice
    End Sub
End Class
