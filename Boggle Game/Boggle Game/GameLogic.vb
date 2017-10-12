Public Class GameLogic
    Private strDefaultDice As String = "AAAFRS AAEEEE AAFIRS ADENNN AEEEEM AEEGMU AEGMNN AFIRSY BJKQXZ CCNSTW CEIILT CEILPT CEIPST DHHNOT DHHLOR DHLNOR DDLNOR EIIITT EMOTTT ENSSSU FIPRSY GORRVW HIPRRY NOOTUW OOOTTU"
    Private gameBoard As Board
    Private playerList As List(Of Player)
    ''' <summary>
    ''' Creates the game with # of players
    ''' </summary>
    ''' <param name="intNumOfPlayers"></param>
    Public Sub New(strPlayerList As String())
        'TO DO: Make Game Constructor
        gameBoard = New Board(strDefaultDice)
        playerList = New List(Of Player)
        For Each playerName In strPlayerList
            playerList.Add(New Player(playerName))
        Next
        'Temp Game Setup
        MsgBox(gameBoard.GetBoard())
        MsgBox(playerList.ElementAt(0).GetName())
    End Sub
    'TO DO: Make Timer System
End Class