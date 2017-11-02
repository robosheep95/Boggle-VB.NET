Public Class Dice
    Private topLetter As Char
    Private letterList As List(Of Char)
    Private specialLetter As Char

    Public Sub New(ByVal input As String)
        specialLetter = CheckForSpecial(input)
        Console.WriteLine(specialLetter)
        letterList = New List(Of Char)
        For Each letter As Char In input
            letterList.Add(Char.ToLower(letter))
        Next
        Randomize()
        ScrambleDice()
    End Sub

    Private Function CheckForSpecial(ByVal input As String) As Char
        Select Case input
            Case "AEEGMU"
                Return "g"
            Case "AEGMNN"
                Return "g"
            Case "BJKQXZ"
                Return "b"
            Case "DHHLOR"
                Return "l"
            Case "EMOTTT"
                Return "e"
            Case "DHHNOT"
                Return "o"
        End Select
        Return " "
    End Function


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

    ReadOnly Property IsSpecial() As Boolean
        Get
            Return (specialLetter = topLetter)
        End Get
    End Property
End Class
