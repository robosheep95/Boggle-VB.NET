''' <summary>
''' Player Class that holdes a name, score and list of used words
''' </summary>
Public Class Player
    Private strName As String
    Private intScore As Integer
    Private strWordList As List(Of String)

    ''' <summary>
    ''' Constucter sets name and instantiates list of words
    ''' </summary>
    ''' <param name="inputName"></param>
    Public Sub New(ByVal inputName As String)
        SetName(inputName)
        strWordList = New List(Of String)
    End Sub

    ''' <summary>
    ''' Getter for name 
    ''' </summary>
    ''' <returns>player name as string</returns>
    ReadOnly Property GetName() As String
        Get
            Return strName
        End Get
    End Property

    ''' <summary>
    ''' Getter for score
    ''' </summary>
    ''' <returns>score as integer</returns>
    ReadOnly Property GetScore() As String
        Get
            Return intScore
        End Get
    End Property

    ''' <summary>
    ''' Getter for word list
    ''' </summary>
    ''' <returns>WordList as a List(Of String)</returns>
    ReadOnly Property GetWordList() As List(Of String)
        Get
            Return strWordList
        End Get
    End Property

    ''' <summary>
    ''' Setter for name of player
    ''' </summary>
    ''' <param name="inputName"></param>
    Sub SetName(ByVal inputName As String)
        strName = inputName
    End Sub

    ''' <summary>
    ''' Setter for score
    ''' </summary>
    ''' <param name="inputScore"></param>
    Sub SetScore(ByVal inputScore As Integer)
        intScore = inputScore
    End Sub

    ''' <summary>
    ''' Adds input to current score
    ''' </summary>
    ''' <param name="inputScore"></param>
    Sub AddScore(ByVal inputScore As Integer)
        intScore += inputScore
    End Sub

    ''' <summary>
    ''' Adds word to WordList
    ''' </summary>
    ''' <param name="inputWord"></param>
    Sub AddWord(ByVal inputWord As String)
        strWordList.Add(inputWord)
    End Sub

    ''' <summary>
    ''' Adds a "*" to the word passed in
    ''' </summary>
    ''' <param name="word"></param>
    Public Sub MarkDuplicate(ByVal word As String)
        strWordList(strWordList.IndexOf(word)) += "*"
    End Sub

    ''' <summary>
    ''' Clears the players word list
    ''' </summary>
    Sub ClearWordList()
        strWordList.Clear()
    End Sub
End Class
