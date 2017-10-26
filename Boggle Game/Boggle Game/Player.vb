Public Class Player
    Private strName As String
    Private intScore As Integer
    Private strWordList As List(Of String)

    Public Sub New(ByVal inputName As String)
        SetName(inputName)
        strWordList = New List(Of String)
    End Sub
    '---Getters---'
    ReadOnly Property GetName() As String
        Get
            Return strName
        End Get
    End Property

    ReadOnly Property GetScore() As String
        Get
            Return intScore
        End Get
    End Property
    ReadOnly Property GetWordList() As List(Of String)
        Get
            Return strWordList
        End Get
    End Property
    '---Setters---'
    Sub SetName(ByVal inputName As String)
        strName = inputName
    End Sub
    Sub SetScore(ByVal inputScore As Integer)
        intScore = inputScore
    End Sub
    Sub AddScore(ByVal inputScore As Integer)
        intScore += inputScore
    End Sub
    '---List Functions---'
    Sub AddWord(ByVal inputWord As String)
        strWordList.Add(inputWord)
    End Sub
    Sub ClearWordList()
        strWordList.Clear()
    End Sub
End Class
