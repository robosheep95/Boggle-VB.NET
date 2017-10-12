Public Class Dice
    Private topLetter As Char
    Private letterList As List(Of char)

    Public Sub New(ByVal input As String)
        'TO DO: Make Constructor
        letterList = New List(Of Char)
        For Each letter As Char In input
            letterList.Add(letter)
        Next
        Randomize()
        ScrambleDice() 'Temp
    End Sub
    ''' <summary>
    ''' Randomaly Selects Top Letter
    ''' </summary>
    Public Sub ScrambleDice()

        topLetter = letterList.ElementAt(CInt(Math.Floor((6) * Rnd())))

    End Sub
    ''' <summary>
    ''' Gets top letter of the dice
    ''' </summary>
    ''' <returns>Single Character if avalable; otherwise, returns blank and errors</returns>
    Public Function GetTopLetter() As Char
        If (Char.IsLetter(topLetter)) Then
            Return topLetter
        Else
            Console.Error.Write("Top Letter not assigned")
            Return ""
        End If
    End Function

End Class
