Public Class Dice
    Private topLetter As Char

    Public Sub New(ByVal input As String)
        'TO DO: Make Constructor
    End Sub
    Public Sub ScrambleDice()
        'TO DO: Create Scrambler Function
    End Sub
    Public Function GetTopLetter() As Char
        If (Char.IsLetter(topLetter)) Then
            Return topLetter
        Else
            Console.Error.Write("Top Letter not assigned")
            Return ""
        End If
    End Function

End Class
