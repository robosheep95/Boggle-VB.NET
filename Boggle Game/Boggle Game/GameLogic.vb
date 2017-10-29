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
    Public Sub New(ByVal strPlayerList As String())

        gameBoard = New Board(strDefaultDice)
        playerList = New List(Of Player)
        For Each playerName In strPlayerList
            playerList.Add(New Player(playerName))
        Next
        wordList = New WordList
        TestStuff() ' Temp test class at EOF

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
            Return wordList.ListOfWordLists.ElementAt(input.Length - 3).Contains(input)
        Else
            Return False
        End If
    End Function

    Private Function IsOnBoard(word As String) As Tuple(Of Boolean, Integer)
        Dim listOfPaths As List(Of Tuple(Of Boolean, Integer)) = New List(Of Tuple(Of Boolean, Integer))
        Dim listOfUsed As List(Of Boolean) = New List(Of Boolean)
        For i As Integer = 0 To 16
            listOfUsed.Add(False)
        Next

        For i As Integer = 0 To 16
            If gameBoard.GetLetter(i) = word(0) Then
                listOfPaths.Add(RabbitHole(word.Substring(1), i, 0, listOfUsed))
            End If
        Next
        If listOfPaths.Count = 0 Then
            Return New Tuple(Of Boolean, Integer)(False, 0)
        Else
            Dim specialCount = 0
            For Each element In listOfPaths
                If element.Item2 > specialCount Then
                    specialCount = element.Item2
                End If
            Next
            Return New Tuple(Of Boolean, Integer)(False, specialCount)
        End If

    End Function

    ''' <summary>
    ''' Recusive Pathfinder
    ''' </summary>
    ''' <param name="word"></param>
    ''' <param name="index"></param>
    ''' <param name="intspecialCount"></param>
    ''' <param name="listOfUsed"></param>
    ''' <returns></returns>
    Private Function RabbitHole(ByVal word As String, ByVal index As Integer, ByVal intspecialCount As Integer, ByRef listOfUsed As List(Of Boolean)) As Tuple(Of Boolean, Integer)
        listOfUsed(index) = True
        If gameBoard.GetSpecials(index) Then
            intspecialCount += 1
        End If
        Dim letterDict As Dictionary(Of Integer, Char) = GetNeighbors(index)

        If word = "" Then
            Return New Tuple(Of Boolean, Integer)(True, intspecialCount)

        ElseIf Not letterDict.ContainsValue(word(0)) Then
            Return New Tuple(Of Boolean, Integer)(False, 0)
        Else

        End If
        For Each entry In letterDict
            If entry.Value = word(0) And Not listOfUsed.ElementAt(entry.Key) Then
                Return RabbitHole(word.Substring(1), entry.Key, intspecialCount, listOfUsed)
            End If
        Next
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
        If index > intConstant And (index Mod intConstant <> (intConstant - 1)) Then
            letterList.Add(index - intConstant + 1, gameBoard.GetLetter(index - intConstant + 1))
        End If
        'North West
        If index > intConstant And (index Mod intConstant) Then
            letterList.Add(index - intConstant - 1, gameBoard.GetLetter(index - intConstant - 1))
        End If
        'South East
        If index < ((intConstant ^ 2) - intConstant) And (index Mod intConstant <> (intConstant - 1)) Then
            letterList.Add(index + intConstant + 1, gameBoard.GetLetter(index + intConstant + 1))
        End If
        'South West
        If index < ((intConstant ^ 2) - intConstant) And (index Mod intConstant) Then
            letterList.Add(index + intConstant - 1, gameBoard.GetLetter(index + intConstant - 1))
        End If

        Return letterList
    End Function


    'Note I have not tested any of these Lambda Functions
    '   Dim get1DIndex = Function(x As Integer())
    '                            Return x(0) + (4 * x(1))
    '                        End Function
    '       Dim get2DIndex = Function(x As Integer)
    '                            Return New Integer() {x - (x \ 4), x \ 4}
    '                        End Function
    '
    '       Dim getAdjacent = Function(x As Integer)
    '                             Dim loc = get2DIndex(x)
    '                             Dim adj = New List(Of Integer)
    '                             If loc(1) - 1 > 0 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0), loc(1) - 1}))
    '                             End If
    '
    '                             If loc(0) - 1 > 0 And loc(1) + 1 < 4 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0) - 1, loc(1) + 1}))
    '                             End If
    '
    '                             If loc(1) + 1 < 4 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0), loc(1) + 1}))
    '                             End If
    '
    '                             If loc(0) + 1 < 4 And loc(1) + 1 < 4 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0) + 1, loc(1) + 1}))
    '                             End If
    '
    '                             If loc(0) + 1 < 4 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0) + 1, loc(1) + 1}))
    '                             End If
    '
    '                             If loc(0) - 1 > 0 And loc(1) + 1 < 4 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0) - 1, loc(1) + 1}))
    '                             End If
    '
    '                             If loc(0) - 1 > 0 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0), loc(1) - 1}))
    '                             End If
    '
    '                             If loc(0) - 1 > 0 And loc(1) - 1 > 0 Then
    '                                 adj.Add(get1DIndex(New Integer() {loc(0) - 1, loc(1) - 1}))
    '                             End If
    '
    '                             Return adj
    '
    '                         End Function
    '
    '
    '       Dim value As Integer = -1
    '
    '       For i = 0 To 15
    '           '--------->BEGIN HERE<-------
    '           'loop through each index and check if the starting letter matches
    '           'then procede to adjacent letters
    '       Next
    '   End Function

    ''' <summary>
    ''' Looks through and commands players to mark duplicates
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
        Console.WriteLine(wordList.ListOfWordLists.ElementAt(0).Contains("aah"))
        Console.WriteLine(IsRealWord("hello"))
    End Sub

End Class