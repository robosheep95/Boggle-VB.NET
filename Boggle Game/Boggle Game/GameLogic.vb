Public Class GameLogic
    Private LetterList3, LetterList4, LetterList5, LetterList6, LetterList7, LetterList8, LetterList9, LetterList10, LetterList11, LetterList12 As List(Of String)
    Private ListOfLetterList As List(Of List(Of String))
    Private strDefaultDice As String = "AAAFRS AAEEEE AAFIRS ADENNN AEEEEM AEEGMU AEGMNN AFIRSY BJKQXZ CCNSTW CEIILT CEILPT CEIPST DHHNOT DHHLOR DHLNOR DDLNOR EIIITT EMOTTT ENSSSU FIPRSY GORRVW HIPRRY NOOTUW OOOTTU"
    Private scoreArray As Integer() = {0, 0, 0, 1, 1, 2, 3, 5, 11}
    Private gameBoard As Board
    Private playerList As List(Of Player)

    ''' <summary>
    ''' Creates the game with an array of player names
    ''' </summary>
    Public Sub New(ByVal strPlayerList As String())

        gameBoard = New Board(strDefaultDice)
        playerList = New List(Of Player)
        For Each playerName In strPlayerList
            playerList.Add(New Player(playerName))
        Next
        ListOfLetterList = New List(Of List(Of String))
        ListOfLetterList.AddRange(collection:={LetterList3, LetterList4, LetterList5, LetterList6, LetterList7, LetterList8, LetterList9, LetterList10, LetterList11, LetterList12})

        TestStuff()

    End Sub

    ''' <summary>
    ''' Returns Letters of Dice on the Board
    ''' </summary>
    ''' <returns>Letters as Character Array</returns>
    ReadOnly Property GetBoard() As Char()
        Get
            Return gameBoard.GetBoard()
        End Get
    End Property

    ''' <summary>
    ''' Returns Boolean Array indicating special dice
    ''' </summary>
    ''' <returns>Boolean Array</returns>
    ReadOnly Property GetSpecial() As Boolean()
        Get
            Return gameBoard.GetSpecials().ToArray()
        End Get
    End Property

    ''' <summary>
    ''' Returns Score of all players in order
    ''' </summary>
    ''' <returns>Score as Integer Array</returns>
    ReadOnly Property GetScores() As Integer()
        Get
            Dim tempScore = New List(Of Integer)
            For Each player In playerList
                tempScore.Add(player.GetScore)
            Next
            Return tempScore.ToArray
        End Get
    End Property

    ''' <summary>
    ''' Checks Dictionary if input is a real word
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns>True or False</returns>
    Public Function IsRealWord(ByVal input As String) As Boolean
        If (input.Length >= 3) Then

            If (ListOfLetterList.ElementAt(input.Length - 3).Count = 0) Then
                Dim Filename = Application.StartupPath() = +input.Length.ToString + "-letter-words.txt"
                ListOfLetterList.ElementAt(input.Length - 3).AddRange(Split(Filename))
            End If
            Return ListOfLetterList.ElementAt(input.Length - 3).Contains(input)
        End If
        Return False
    End Function

    ''' <summary>
    ''' Checks if the word is on the board
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns>Boolean and Integer equal to number of specials</returns>
    Private Function IsOnBoard(ByVal input As String) As Tuple(Of Boolean, Integer)
        'TO DO: Create IsOnBoard Function
        Return New Tuple(Of Boolean, Integer)(True, 1)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="input"></param>
    ''' <param name="playerIndex"></param>
    ''' <returns></returns>
    Private Sub MarkDuplicates()
        If playerList.Count <> 0 Then
            Dim wordLists As List(Of List(Of String)) = New List(Of List(Of String))
            For Each player In playerList
                wordLists.Add(player.GetWordList)
            Next
            'Dim duplacates = wordLists(0).Intersect(wordLists.gr)
        End If

    End Sub

    ''' <summary>
    ''' Scores the Players and Saves score to the player object
    ''' </summary>
    Public Sub ScorePlayers()
        MarkDuplicates()
        For Each player In playerList

            For Each word In player.GetWordList


                If Not word.Contains("*") Then
                    Dim wordLength As Integer = word.Length
                    If wordLength > 8 Then
                        wordLength = 8
                    End If
                    Dim tmpData As Tuple(Of Boolean, Integer) = IsOnBoard(word)
                    If tmpData.Item1 And tmpData.Item2 = 0 Then
                        player.AddScore(scoreArray(wordLength))
                    ElseIf tmpData.Item1 And tmpData.Item2 <> 0 Then
                        player.AddScore(scoreArray(wordLength) * (2 * tmpData.Item2))
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub TestStuff()
        playerList(0).AddWord("Hello")
        playerList(0).AddWord("World")
        playerList(1).AddWord("Hello")
        playerList(1).AddWord("Billy")
        playerList(1).AddWord("Idol")
        ScorePlayers()
        Console.WriteLine(playerList(0).GetWordList(0))
        Console.WriteLine(playerList(0).GetWordList(1))
        Console.WriteLine(playerList(1).GetWordList(0))
        Console.WriteLine(playerList(1).GetWordList(1))
        Console.WriteLine(playerList(1).GetWordList(2))
        Console.WriteLine(playerList(0).GetScore)
        Console.WriteLine(playerList(1).GetScore)
    End Sub

End Class