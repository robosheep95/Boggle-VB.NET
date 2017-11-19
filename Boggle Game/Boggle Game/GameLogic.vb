''' <summary>
''' Primary Logic Class. Used as an interface between the game window and the stored data.
''' </summary>
Public Class GameLogic

    Private strDefaultDice As String = "AAAFRS AAEEEE AAFIRS ADENNN AEEEEM AEEGMU AEGMNN AFIRSY BJKQXZ CCNSTW CEIILT CEILPT CEIPST DHHNOT DHHLOR DHLNOR DDLNOR EIIITT EMOTTT ENSSSU FIPRSY GORRVW HIPRRY NOOTUW OOOTTU"
    Private scoreArray As Integer() = {0, 0, 0, 1, 1, 2, 3, 5, 11}
    Private gameBoard As Board
    Private playerList As List(Of Player)
    Private wordValue As Dictionary(Of String, Integer)
    Private wordList As WordList

    ''' <summary>
    ''' Creates the game with an array of player names
    ''' </summary>
    Public Sub New()
        gameBoard = New Board(strDefaultDice)
        wordList = New WordList
        gameBoard.ScrambleBoard()
    End Sub

    ''' <summary>
    ''' Creates a list of players with the players names passed in
    ''' </summary>
    ''' <param name="players"></param>
    Public Sub CreatePlayers(players As List(Of String))
        playerList = New List(Of Player)
        For Each playerName In players
            playerList.Add(New Player(playerName))
        Next
    End Sub

    ''' <summary>
    ''' Returns Letters of Dice on the Board
    ''' </summary>
    ''' <returns>Letters as Character Array</returns>
    Public Function GetBoard() As Char()
        Return gameBoard.GetBoard()
    End Function

    ''' <summary>
    ''' Returns Boolean Array indicating special dice
    ''' </summary>
    ''' <returns>Boolean Array</returns>
    Public Function GetSpecial() As Boolean()
        Return gameBoard.GetSpecials().ToArray()
    End Function

    ''' <summary>
    ''' Returns Score of all players in order
    ''' </summary>
    ''' <returns>Score as Integer Array</returns>
    Public Function GetScores() As Integer()
        Dim tempScore = New List(Of Integer)
        For Each player In playerList
            tempScore.Add(player.GetScore)
        Next
        Return tempScore.ToArray()
    End Function

    ''' <summary>
    ''' Scrambles the board
    ''' </summary>
    Public Sub ScrambleBoard()
        gameBoard.ScrambleBoard()
    End Sub

    ''' <summary>
    ''' Gets the list of player(s)
    ''' </summary>
    ''' <returns>Returns a List(Of Players)</returns>
    Public Function GetPlayers() As List(Of Player)
        Return playerList
    End Function

    ''' <summary>
    ''' Checks Dictionary if input is a real word
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns>True or False</returns>
    Public Function IsRealWord(ByVal input As String) As Boolean
        If (input.Length >= 3) Then
            Return wordList.ListOfWordLists.ElementAt(input.Length - 3).Contains(input)
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns a Boolean if the word is on the board
    ''' </summary>
    ''' <param name="word"></param>
    ''' <returns>Boolean</returns>
    Public Function SimpleIsOnBoard(word As String) As Boolean
        Return IsOnBoard(word).Item1
    End Function

    ''' <summary>
    ''' Checks if a word is on the board
    ''' </summary>
    ''' <param name="word"></param>
    ''' <returns></returns>
    Private Function IsOnBoard(word As String) As Tuple(Of Boolean, Integer)
        Dim listOfPaths As List(Of Tuple(Of Boolean, Integer)) = New List(Of Tuple(Of Boolean, Integer))
        Dim listOfUsed As List(Of Boolean) = New List(Of Boolean)
        For i As Integer = 0 To 16
            listOfUsed.Add(False)
        Next

        For i As Integer = 0 To 16
            If gameBoard.GetLetter(i) = word(0) AndAlso word(0) <> "q" Then
                listOfPaths.Add(RabbitHole(word.Substring(1), i, 0, listOfUsed))
            ElseIf gameBoard.GetLetter(i) = word(0) AndAlso word(0) = "q" AndAlso Len(word) > 1 AndAlso word(1) = "u" Then
                listOfPaths.Add(RabbitHole(word.Substring(2), i, 0, listOfUsed))
            End If
        Next
        If listOfPaths.Count = 0 Then
            Return New Tuple(Of Boolean, Integer)(False, 0)
        Else
            Dim specialCount As Integer = -1
            Dim hasPath As Boolean
            For Each element In listOfPaths
                If element.Item2 > specialCount AndAlso element.Item1 Then
                    hasPath = True
                    specialCount = element.Item2
                End If
            Next
            Return New Tuple(Of Boolean, Integer)(hasPath, specialCount)
        End If

    End Function

    ''' <summary>
    ''' Recusive Pathfinder for IsOnBoard
    ''' </summary>
    ''' <param name="word"></param>
    ''' <param name="index"></param>
    ''' <param name="intSpecialCount"></param>
    ''' <param name="listOfUsed"></param>
    ''' <returns>Tuple of Boolean and Integer</returns>
    Private Function RabbitHole(ByVal word As String, ByVal index As Integer, ByVal intSpecialCount As Integer, ByVal listOfUsed As List(Of Boolean)) As Tuple(Of Boolean, Integer)
        listOfUsed(index) = True
        If gameBoard.GetSpecials(index) Then
            intSpecialCount += 1
        End If
        Dim letterDict As Dictionary(Of Integer, Char) = GetNeighbors(index)

        If word = "" Then
            Return New Tuple(Of Boolean, Integer)(True, intSpecialCount)

        ElseIf Not letterDict.ContainsValue(word(0)) Then
            Return New Tuple(Of Boolean, Integer)(False, 0)
        Else

            For Each entry In letterDict
                If entry.Value = word(0) AndAlso (Not listOfUsed.ElementAt(entry.Key)) AndAlso entry.Value <> "q" Then
                    Return RabbitHole(word.Substring(1), entry.Key, intSpecialCount, listOfUsed)
                ElseIf entry.Value = word(0) AndAlso Not listOfUsed.ElementAt(entry.Key) AndAlso entry.Value = "q" AndAlso Len(word) > 1 Then
                    If word(1) = "u" Then
                        Return RabbitHole(word.Substring(2), entry.Key, intSpecialCount, listOfUsed)
                    End If
                End If
            Next

        End If
        Return New Tuple(Of Boolean, Integer)(False, 0)
    End Function

    ''' <summary>
    ''' Takes an index and returns all the neighbors with the index and character value
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns>Dictionary of Indexes and Character Values</returns>
    Private Function GetNeighbors(ByVal index As Integer) As Dictionary(Of Integer, Char)
        Dim intConstant As Integer = 4
        Dim letterList As New Dictionary(Of Integer, Char)

        'North
        If index > intConstant Then
            letterList.Add(index - intConstant, gameBoard.GetLetter(index - intConstant))
        End If

        'South
        If index < ((intConstant ^ 2) - intConstant) Then
            letterList.Add(index + intConstant, gameBoard.GetLetter(index + intConstant))
        End If

        'East
        If (index Mod intConstant <> (intConstant - 1)) Then
            letterList.Add(index + 1, gameBoard.GetLetter(index + 1))
        End If

        'West
        If (index Mod intConstant) <> 0 Then
            letterList.Add(index - 1, gameBoard.GetLetter(index - 1))
        End If

        'North East
        If index > intConstant AndAlso (index Mod intConstant <> (intConstant - 1)) Then
            letterList.Add(index - intConstant + 1, gameBoard.GetLetter(index - intConstant + 1))
        End If

        'North West
        If index > intConstant And (index Mod intConstant) Then
            letterList.Add(index - intConstant - 1, gameBoard.GetLetter(index - intConstant - 1))
        End If

        'South East
        If index < ((intConstant ^ 2) - intConstant) AndAlso (index Mod intConstant <> (intConstant - 1)) Then
            letterList.Add(index + intConstant + 1, gameBoard.GetLetter(index + intConstant + 1))
        End If

        'South West
        If index < ((intConstant ^ 2) - intConstant) And (index Mod intConstant) Then
            letterList.Add(index + intConstant - 1, gameBoard.GetLetter(index + intConstant - 1))
        End If

        Return letterList
    End Function

    ''' <summary>
    ''' Checks to see if multple players have the same word, If they do the player is commanded to mark that word
    ''' </summary>
    Private Sub MarkDuplicates()
        If playerList.Count <> 0 Then
            Dim wordList As List(Of String) = New List(Of String)
            For Each player In playerList
                wordList.AddRange(player.GetWordList)
            Next

            wordList = wordList.GroupBy(Function(m) m) _
                 .Where(Function(g) g.Count() > 1) _
                 .Select(Function(g) g.Key).ToList

            For Each word In wordList
                For Each player In playerList
                    player.MarkDuplicate(word)
                Next
            Next
        End If
    End Sub

    ''' <summary>
    ''' Scores the Players and Saves score to the player object
    ''' </summary>
    Public Sub ScorePlayers()
        MarkDuplicates()
        For Each player In playerList
            For Each word In player.GetWordList
                If Not word.Contains("*") Or word.Contains("-") Then
                    Dim wordLength As Integer = word.Length
                    If wordLength > 8 Then
                        wordLength = 8
                    End If
                    Dim tmpData As Tuple(Of Boolean, Integer) = IsOnBoard(word)
                    If tmpData.Item1 AndAlso tmpData.Item2 = 0 Then
                        player.AddScore(scoreArray(wordLength))
                    ElseIf tmpData.Item1 AndAlso tmpData.Item2 <> 0 Then
                        player.AddScore(scoreArray(wordLength) * (2 * tmpData.Item2))
                    End If
                End If
            Next
        Next
    End Sub

End Class