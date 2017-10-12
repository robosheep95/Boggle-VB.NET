Public Class Dice
    Private topLetter As Char
    Private letterList As List(Of Char)
    Private specialLetter As Char

    Public Sub New(ByVal input As String)
        'TO DO: Make Constructor
        specialLetter = CheckForSpecial(input)
        letterList = New List(Of Char)
        For Each letter As Char In input
            letterList.Add(letter)
        Next
        Randomize()
        ScrambleDice() 'Temp
    End Sub

    Private Function CheckForSpecial(ByVal input As String) As Char
        Select Case input
            Case "AEEGMU" Or "AEGMNN"
                Return "G"
            Case "BJKQXZ"
                Return "B"
            Case "DHHLOR"
                Return "L"
            Case "EMOTTT"
                Return "E"
            Case "DHHNOT"
                Return "O"
        End Select
        Return ""
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
