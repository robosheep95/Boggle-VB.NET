'Project: Boggle
'By:      Allen Retzler and Taylor Scafe
'Date:    10-12-17

Imports VB = Microsoft.VisualBasic ' Used for creating a timer https://stackoverflow.com/a/36362504

Public Class frmMain
    Private numberOfPlayers As UShort = 2
    Private gameLogicManager As GameLogic
    Private nameList = New List(Of String)

    Private timerMinutes As UShort = 3
    Private timerSeconds As UShort = 10
    Private timerMax As UInt32 = timerMinutes * 60 + timerSeconds

    'Private Enum timerType
    '    Initial
    '    InGame
    'End Enum
    'Private timerMode As timerType = timerType.Initial

    ''' <summary>
    ''' Centers an interior Object relative to it's exterior container
    ''' </summary>
    ''' <param name="interior">The object to center</param>
    ''' <param name="exterior">The container to center the object within</param>
    Private Sub Center(interior As Object, exterior As Object)
        interior.left = CInt((exterior.Width / 2) - (interior.Width / 2))
        interior.top = CInt((exterior.Height / 2) - (interior.Height / 2))
    End Sub

    ''' <summary>
    ''' Halts execution of the current function for a period of time.
    ''' This function uses threading to allow other events to interrupt
    ''' the timer.
    ''' </summary>
    ''' <param name="seconds">The number of seconds to wait until continuing execution</param>
    Private Sub timer(seconds As Integer)
        For index = 0 To 10
            Application.DoEvents()
            System.Threading.Thread.Sleep(seconds * 100)
        Next
    End Sub

    ''' <summary>
    ''' Decrements the amount of time left
    ''' </summary>
    ''' <returns>Returns True when the timer has elapsed (0min 0sec remaining)</returns>
    Private Function timerDecrement() As Boolean
        If timerMinutes > 0 Or timerSeconds > 0 Then
            If timerSeconds > 0 Then
                timerSeconds -= 1
            Else
                timerMinutes -= 1
                timerSeconds = 59
            End If
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Updates the timer display
    ''' </summary>
    Private Sub timerUpdate()
        prgTimer.Maximum = timerMax
        prgTimer.Value = 60 * timerMinutes + timerSeconds
        Dim ts As String
        If timerSeconds < 10 Then
            ts = "0" + CStr(timerSeconds)
        Else
            ts = CStr(timerSeconds)
        End If
        lblTimerText.Text = CStr(timerMinutes) + ":" + ts
    End Sub


    ''' <summary>
    ''' Goes to the Main Menu Screen
    ''' </summary>
    Private Sub gotoMainMenu()
        startScreen.Visible = True
        nameScreen.Visible = False
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = False
        Center(startScreen, Me)
        Me.AcceptButton = btnStartGame
        btnStartGame.Focus()
    End Sub

    ''' <summary>
    ''' Goes to the screen where the players enter their names (just before the actual game)
    ''' </summary>
    Private Sub gotoEnterNames()
        startScreen.Visible = False
        nameScreen.Visible = True
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = False
        Center(nameScreen, Me)
        Me.AcceptButton = btnOk
        txtPlayerName.Text = ""
        txtPlayerName.Focus()
        nameList = New List(Of String)
        lblEnterName.Text = "Player " + CStr(nameList.Count() + 1) + " enter your name"
    End Sub


    ''' <summary>
    ''' Goes to the actual game and starts it
    ''' </summary>
    Private Sub gotoGame()
        startScreen.Visible = False
        nameScreen.Visible = False
        gameScreen.Visible = True
        inputScreen.Visible = False
        scoreScreen.Visible = False
        Center(gameScreen, Me)

        prgTimer.Value = 0
        gameLogicManager = New GameLogic(nameList.toArray())

        timerMinutes = timerMax \ 60
        timerSeconds = timerMax - (60 * timerMinutes)

        timerUpdate()

        lblDie1.Text = "3"
        lblDie2.Text = "3"
        lblDie3.Text = "3"
        lblDie4.Text = "3"
        lblDie5.Text = "3"
        lblDie6.Text = "3"
        lblDie7.Text = "3"
        lblDie8.Text = "3"
        lblDie9.Text = "3"
        lblDie10.Text = "3"
        lblDie11.Text = "3"
        lblDie12.Text = "3"
        lblDie13.Text = "3"
        lblDie14.Text = "3"
        lblDie15.Text = "3"
        lblDie16.Text = "3"
        timer(1)

        lblDie1.Text = "2"
        lblDie2.Text = "2"
        lblDie3.Text = "2"
        lblDie4.Text = "2"
        lblDie5.Text = "2"
        lblDie6.Text = "2"
        lblDie7.Text = "2"
        lblDie8.Text = "2"
        lblDie9.Text = "2"
        lblDie10.Text = "2"
        lblDie11.Text = "2"
        lblDie12.Text = "2"
        lblDie13.Text = "2"
        lblDie14.Text = "2"
        lblDie15.Text = "2"
        lblDie16.Text = "2"
        timer(1)

        lblDie1.Text = "1"
        lblDie2.Text = "1"
        lblDie3.Text = "1"
        lblDie4.Text = "1"
        lblDie5.Text = "1"
        lblDie6.Text = "1"
        lblDie7.Text = "1"
        lblDie8.Text = "1"
        lblDie9.Text = "1"
        lblDie10.Text = "1"
        lblDie11.Text = "1"
        lblDie12.Text = "1"
        lblDie13.Text = "1"
        lblDie14.Text = "1"
        lblDie15.Text = "1"
        lblDie16.Text = "1"
        timer(1)
        Dim v = gameLogicManager.GetBoard()
        lblDie1.Text = v(0)
        lblDie2.Text = v(1)
        lblDie3.Text = v(2)
        lblDie4.Text = v(3)
        lblDie5.Text = v(4)
        lblDie6.Text = v(5)
        lblDie7.Text = v(6)
        lblDie8.Text = v(7)
        lblDie9.Text = v(8)
        lblDie10.Text = v(9)
        lblDie11.Text = v(10)
        lblDie12.Text = v(11)
        lblDie13.Text = v(12)
        lblDie14.Text = v(13)
        lblDie15.Text = v(14)
        lblDie16.Text = v(15)

        While Not timerDecrement()
            timerUpdate()
            timer(1)
        End While
        timerUpdate()

    End Sub

    ''' <summary>
    ''' Goes to the screen where players enter the words they found (just after the actual game)
    ''' </summary>
    Private Sub gotoEnterWords()
        startScreen.Visible = False
        nameScreen.Visible = False
        gameScreen.Visible = False
        inputScreen.Visible = True
        scoreScreen.Visible = False
        Center(inputScreen, Me)
        Me.AcceptButton = btnAddWord
        txtPlayerX.Focus()
    End Sub

    ''' <summary>
    ''' Goes to the screen where scores and words found are displayed
    ''' </summary>
    Private Sub gotoResults()
        startScreen.Visible = False
        nameScreen.Visible = False
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = True
        Center(scoreScreen, Me)
    End Sub



    ''' <summary>
    ''' function that runs when the main form is loaded
    ''' </summary>
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 500
        Me.Height = 500
        Me.MinimumSize = New Drawing.Size(500, 500)
        Me.MaximumSize = New Drawing.Size(500, 500)
        gotoMainMenu()

    End Sub

    Private Sub LblAllenRetzler_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAllenRetzler.LinkClicked
        Process.Start("https://github.com/allenretz")
    End Sub

    Private Sub LblTaylorScafe_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTaylorScafe.LinkClicked
        Process.Start("https://github.com/robosheep95")
    End Sub

    Private Sub btnStartGame_Click(sender As Object, e As EventArgs) Handles btnStartGame.Click
        If radio1Player.Checked() Then
            numberOfPlayers = 1
        ElseIf Radio2Players.Checked() Then
            numberOfPlayers = 2
        ElseIf radio3Players.Checked() Then
            numberOfPlayers = 3
        Else
            numberOfPlayers = 4
        End If
        gotoEnterNames()
    End Sub

    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        If MsgBox("Are You Sure You Want to Quit?", vbQuestion + vbYesNo, "Quit") = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gotoMainMenu()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If txtPlayerName.Text = "" Then
            MsgBox("Your name must be at least 1 character long", vbExclamation + vbOK, "Invalid Name")
        ElseIf nameList.Contains(txtPlayerName.Text) Then
            MsgBox("Your Name must not be the same as a previous player", vbExclamation + vbOK, "Invalid Name")
        Else
            nameList.Add(txtPlayerName.Text)
            txtPlayerName.Text = ""
            lblEnterName.Text = "Player " + CStr(nameList.Count() + 1) + " enter your name"
        End If
        If nameList.Count() = numberOfPlayers Then
            gotoGame()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        gotoMainMenu()
    End Sub
End Class
