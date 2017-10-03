Public Class Board
    Private DiceList(15) As Dice
    Public Sub New(ByVal diceString As String)
        'TO DO: Initalize Board and Create Dice Objects

    End Sub

    Public Sub ScrambleBoard()
        'TO DO: Create Dice Position Randomizer
        'TO DO: Call ScrambleDice() on each Dice
    End Sub

    Public Function GetBoard() As Dice()
        Return DiceList
    End Function

End Class
