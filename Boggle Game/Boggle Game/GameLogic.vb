Public Class GameLogic
    Private LetterList3, LetterList4, LetterList5, LetterList6, LetterList7, LetterList8, LetterList9, LetterList10, LetterList11, LetterList12 As List(Of String)
    Private ListOfLetterList As List(Of List(Of String))
    Private strDefaultDice As String = "AAAFRS AAEEEE AAFIRS ADENNN AEEEEM AEEGMU AEGMNN AFIRSY BJKQXZ CCNSTW CEIILT CEILPT CEIPST DHHNOT DHHLOR DHLNOR DDLNOR EIIITT EMOTTT ENSSSU FIPRSY GORRVW HIPRRY NOOTUW OOOTTU"
    Private gameBoard As Board
    Private playerList As List(Of Player)
    ''' <summary>
    ''' Creates the game with # of players
    ''' </summary>
    Public Sub New(strPlayerList As String())

        gameBoard = New Board(strDefaultDice)
        playerList = New List(Of Player)
        For Each playerName In strPlayerList
            playerList.Add(New Player(playerName))
        Next
        ListOfLetterList = New List(Of List(Of String))
        ListOfLetterList.AddRange(collection:={LetterList3, LetterList4, LetterList5, LetterList6, LetterList7, LetterList8, LetterList9, LetterList10, LetterList11, LetterList12})
        'Temp Game Setup

        'Debug Info Should be written to the Console or you should have your own branch
        Console.WriteLine("Taylor - " + gameBoard.GetBoard())
        Console.WriteLine("Taylor - " + playerList(0).GetName())
        'MsgBox(gameBoard.GetBoard())
        'MsgBox(playerList.ElementAt(0).GetName())
    End Sub
    'TO DO: Make Timer System
    ReadOnly Property GetBoard() As Char()
        Get
            Return gameBoard.GetBoard()
        End Get
    End Property

    Function IsRealWord(input As String) As Boolean
        If (input.Length >= 3) Then

            If (ListOfLetterList.ElementAt(input.Length - 3).Count = 0) Then
                Dim Filename = Application.StartupPath() = +input.Length.ToString + "-letter-words.txt"
                ListOfLetterList.ElementAt(input.Length - 3).AddRange(Split(Filename))
            End If
            Return ListOfLetterList.ElementAt(input.Length - 3).Contains(input)
        End If
        Return False
    End Function


End Class