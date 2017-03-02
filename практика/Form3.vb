Public Class Form3
    Private _mode As Boolean = True
    Private _time As Integer
    Public ReadOnly Property fileName As String
        Get
            Return fName.Text
        End Get
    End Property
    Public ReadOnly Property time As Integer
        Get
            Return _time
        End Get
    End Property
    Private Sub saveBtn_Click(sender As Object, e As EventArgs) Handles saveBtn.Click
        If RadioButton1.Checked Then
            _mode = True
        Else
            _time = timeMod.Text
            _mode = False
        End If
        Me.Hide()
    End Sub
    Public ReadOnly Property mode As Boolean
        Get
            Return _mode
        End Get
    End Property

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            timeMod.Enabled = True
            Label4.Enabled = True
        Else
            timeMod.Enabled = False
            Label4.Enabled = False
        End If
    End Sub
End Class