Public Class GameLogic
    Private strDefaultDice As String = "AAAFRS AAEEEE AAFIRS ADENNN AEEEEM AEEGMU AEGMNN AFIRSY BJKQXZ CCNSTW CEIILT CEILPT CEIPST DHHNOT DHHLOR DHLNOR DDLNOR EIIITT EMOTTT ENSSSU FIPRSY GORRVW HIPRRY NOOTUW OOOTTU"
    Private gameBoard As Board
    ''' <summary>
    ''' Creates the game with # of players
    ''' </summary>
    ''' <param name="intNumOfPlayers"></param>
    Public Sub New(intNumOfPlayers As Integer)
        'TO DO: Make Game Constructor
        gameBoard = New Board(strDefaultDice)
        'MsgBox(gameBoard.GetBoard())
    End Sub
    'TO DO: Make Timer System
End Class