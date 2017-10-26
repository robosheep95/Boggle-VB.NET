Public Class Board

    Private DiceList As List(Of Dice)
    ''' <summary>
    ''' Creates a board of 16 dice using a space seperated sting of dice
    ''' </summary>
    ''' <param name="diceString">Space Seperated List of Dice</param>
    Public Sub New(ByVal diceString As String)
        DiceList = New List(Of Dice)
        For Each sequence In Split(diceString, " ")
            DiceList.Add(New Dice(sequence))
        Next
    End Sub

    ''' <summary>
    ''' Scrambles Board and Dice
    ''' </summary>
    Public Sub ScrambleBoard()
        For i As Integer = 0 To 100
            Dim diceMove As Dice = DiceList.ElementAt(CInt(Math.Floor((16) * Rnd())))
            DiceList.Remove(diceMove)
            DiceList.Add(diceMove)
        Next
        For Each dice In DiceList
            dice.ScrambleDice()
        Next
    End Sub

    ''' <summary>
    ''' Gets board letters
    ''' </summary>
    ''' <returns>A string of characters</returns>
    Public Function GetBoard() As Char()
        Dim strOuput As String = ""
        For Each dice In DiceList
            strOuput = strOuput + dice.GetTopLetter()
        Next
        Return strOuput
    End Function

    ''' <summary>
    ''' Gets Boolean List of Specials
    ''' </summary>
    ''' <returns>Boolean Array</returns>
    Public Function GetSpecials() As List(Of Boolean)
        Dim bolOutput As List(Of Boolean) = New List(Of Boolean)
        For Each dice In DiceList
            bolOutput.Add(dice.IsSpecial)
        Next
        Return bolOutput
    End Function

End Class
