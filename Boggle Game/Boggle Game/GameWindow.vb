'Project: Boggle
'By:      Allen Retzler and Taylor Scafe
'Date:    10-12-17

'Background Image By Jolene Faber https://www.flickr.com/photos/jovanlaar/2137872208/in/photolist-4fV9VE-6dEdXJ-7vEVv7-qt6uTL-7MYx2d-7bfhL1-7ZYXGq-5RhZ97-4MitNY-nZB3fK-5hgoH6-vwp1Us-aY4g3-4T8hoV-7bfimU-8qiVbt-7sF14G-8qvbQo-9UMgNA-4QioXL-aCwXC-8TuGnf-bgKm6M-pqSmr-aQfrr2-FdFrw-fF9JC-Lj3bW-66Pr88-erbtWm-jEQEc-7zYNyp-5FYch3-7cPczA-7a6zqc-4zQYqx-UQ3ctU-6XhsiQ-TdCi-8hfGKv-8cEacT-68vb2y-b2uReR-94ZJ2v-8hV7T1-cMRco-pWGe9V-qDMJSB-cE4a7u-axDX6i
'Splash Screen Image By Rich Brooks https://www.flickr.com/photos/therichbrooks/4039402557/in/photolist-Hfsnb-8sRUhS-jM3h1-5MV5rb-7wid4Y-3idFWu-ai62v-4SK7dS-2nsWsm-5MtwKv-wqFHV-aqpBv1-Ap2bS-79WZDF-4grY5S-oB74G-66Lfb7-9ARFN-64ysgN-6H1kKA-nTKPqR-2jL2dp-9gh7oJ-7Gqfdb-QGYVZ-7a1Rty-7WPbx3-tAwBF-jC885-ibZuX-8HfcuR-5Mtv6p-5MturK-5MxLLA-dJm4C5-5E9Sxx-9AdHWK-8Xu1Ma-4paK8z-7ikCdo-bexBiK-5Mtanx-cvjrZ-bhF7vF-9jpVvi-5E9SR4-4paHzk-5ET5b9-5MttVi-5Mtxft
Imports System.ComponentModel
Imports VB = Microsoft.VisualBasic


Imports Regex = System.Text.RegularExpressions.Regex

Public Class frmMain
    Private numberOfPlayers As UShort = 2
    Private gameLogicManager As GameLogic
    Private nameList = New List(Of String)

    Private timerMinutes As UShort = 3
    Private timerSeconds As UShort = 0
    Private timerMax As UInt32 = timerMinutes * 60 + timerSeconds
    Private timerHalted = False

    Private tmpCurrentPlayer = 1
    Private tmpPlayerXWords As List(Of String) = New List(Of String)

    Private currentObject As Object = startScreen

    ''' <summary>
    ''' Centers an interior Object relative to it's exterior container
    ''' </summary>
    ''' <param name="interior">The object to center</param>
    ''' <param name="exterior">The container to center the object within</param>
    Private Sub Center(interior As Object, exterior As Object)
        Try
            interior.left = CInt((exterior.Width / 2) - (interior.Width / 2))
            interior.top = CInt((exterior.Height / 2) - (interior.Height / 2))
        Catch a As NullReferenceException
            Console.WriteLine("Resize Error")
        End Try
    End Sub

    ''' <summary>
    ''' Halts execution of the current function for a period of time.
    ''' This function uses threading to allow interrupts to still function.
    ''' The timer needs to be halted before close
    ''' This is based off of https://stackoverflow.com/a/36362504
    ''' </summary>
    ''' <param name="seconds">The number of seconds to wait until continuing execution</param>
    Private Sub timer(seconds As Double)
        If timerHalted Then
            Return
        End If
        For index = 0 To 10
            Application.DoEvents()
            System.Threading.Thread.Sleep(seconds * 100)
            If timerHalted Then
                Exit For
            End If
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
    ''' Stops the timer from counting down
    ''' </summary>
    Private Sub timerHalt()
        timerHalted = True
    End Sub

    ''' <summary>
    ''' Allows the timer to function if it were halted
    ''' </summary>
    Private Sub timerReset()
        timerHalted = False
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

        currentObject = startScreen

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

        currentObject = nameScreen

        Center(nameScreen, Me)
        Me.AcceptButton = btnOk
        txtPlayerName.Text = ""
        txtPlayerName.Focus()
        nameList = New List(Of String)
        lblEnterName.Text = "Player 1 enter your name"
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

        currentObject = gameScreen

        Center(gameScreen, Me)
        Dim diceList = New List(Of Label)

        btnRescramble.Visible = False
        btnFinish.Visible = False

        diceList.Add(lblDie1)
        diceList.Add(lblDie2)
        diceList.Add(lblDie3)
        diceList.Add(lblDie4)
        diceList.Add(lblDie5)
        diceList.Add(lblDie6)
        diceList.Add(lblDie7)
        diceList.Add(lblDie8)
        diceList.Add(lblDie9)
        diceList.Add(lblDie10)
        diceList.Add(lblDie11)
        diceList.Add(lblDie12)
        diceList.Add(lblDie13)
        diceList.Add(lblDie14)
        diceList.Add(lblDie15)
        diceList.Add(lblDie16)

        Dim qToQu = Function(s As String)
                        If s.ToUpper() = "Q" Then
                            Return "Qu"
                        Else
                            Return s
                        End If
                    End Function

        Dim toUpper = Function(s As String)
                          If s = "Qu" Then
                              Return "Qu"
                          Else
                              Return s.ToUpper()
                          End If
                      End Function

        prgTimer.Value = 0


        timerMinutes = timerMax \ 60
        timerSeconds = timerMax - (60 * timerMinutes)

        timerUpdate()
        timerReset()


        'Scrambeling The Board
        For t = 0 To 20
            For i = 0 To 15
                diceList(i).Text = toUpper(qToQu(gameLogicManager.GetBoard()(i)))
                If gameLogicManager.GetSpecial()(i) Then
                    diceList(i).ForeColor = Color.Red
                Else
                    diceList(i).ForeColor = Color.Black
                End If
            Next
            gameLogicManager.ScrambleBoard()
            timer(0.06)
        Next

        btnRescramble.Visible = True
        btnFinish.Visible = True

        For i = 0 To 15
            diceList(i).Text = toUpper(qToQu(gameLogicManager.GetBoard()(i)))
            If gameLogicManager.GetSpecial()(i) Then
                diceList(i).ForeColor = Color.Red
            Else
                diceList(i).ForeColor = Color.Black
            End If
        Next


        While Not timerDecrement()
            timerUpdate()
            timer(1)
        End While
        timerUpdate()

        'Figure out a better way to do this
        If gameScreen.Visible = True Then
            gotoEnterWords()
        End If


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

        currentObject = inputScreen

        Center(inputScreen, Me)
        Me.AcceptButton = btnAddWord
        txtPlayerXWord.Focus()
        tmpCurrentPlayer = 1
        lblPlayerX.Text = gameLogicManager.GetPlayers(0).GetName()
    End Sub

    ''' <summary>
    ''' Goes to the screen where scores and words found are displayed
    ''' </summary>
    Private Sub gotoResults()
        Dim players = gameLogicManager.GetPlayers()

        startScreen.Visible = False
        nameScreen.Visible = False
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = True

        currentObject = scoreScreen

        Center(scoreScreen, Me)

        rtbPlayer1Words.Text= ""
        rtbPlayer2Words.Text = ""
        rtbPlayer3Words.Text = ""
        rtbPlayer4Words.Text = ""

        lblP2Name.Hide()
        lblP3Name.Hide()
        lblP4Name.Hide()

        lblP2Score.Hide()
        lblP3Score.Hide()
        lblP4Score.Hide()

        rtbPlayer2Words.Hide()
        rtbPlayer3Words.Hide()
        rtbPlayer4Words.Hide()

        gameLogicManager.ScorePlayers()


        lblP1Name.Text = players(0).GetName()
        lblP1Score.Text = players(0).GetScore() & " Points"
        For Each i In players(0).GetWordList()
            rtbPlayer1Words.Text += i + vbNewLine
        Next


        If numberOfPlayers >= 2 Then
            lblP2Name.Show()
            lblP2Score.Show()
            rtbPlayer2Words.Show()

            lblP2Name.Text = players(1).GetName()
            lblP2Score.Text = players(1).GetScore() & " Points"
            For Each i In players(1).GetWordList()
                rtbPlayer2Words.Text += i + vbNewLine
            Next
        End If

        If numberOfPlayers >= 3 Then
            lblP3Name.Show()
            lblP3Score.Show()
            rtbPlayer3Words.Show()

            lblP3Name.Text = players(2).GetName()
            lblP3Score.Text = players(2).GetScore() & " Points"
            For Each i In players(2).GetWordList()
                rtbPlayer3Words.Text += i + vbNewLine
            Next
        End If

        If numberOfPlayers >= 4 Then
            lblP4Name.Show()
            lblP4Score.Show()
            rtbPlayer4Words.Show()

            lblP4Name.Text = players(3).GetName()
            lblP4Score.Text = players(3).GetScore() & " Points"
            For Each i In players(3).GetWordList()
                rtbPlayer4Words.Text += i + vbNewLine
            Next
        End If




    End Sub



    ''' <summary>
    ''' function that runs when the main form is loaded
    ''' </summary>
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gameLogicManager = New GameLogic()
        Me.Width = 800
        Me.Height = 600
        Me.MinimumSize = New Drawing.Size(800, 600)
        gotoMainMenu()

    End Sub

    ''' Button Handlers

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
            MsgBox("Your name must be at least 1 character long", vbExclamation + vbOKOnly, "Invalid Name")
        ElseIf nameList.Contains(txtPlayerName.Text) Then
            MsgBox("Your Name must not be the same as a previous player", vbExclamation + vbOKOnly, "Invalid Name")
        Else
            nameList.Add(txtPlayerName.Text)
            txtPlayerName.Text = ""
            lblEnterName.Text = "Player " + CStr(nameList.Count() + 1) + " enter your name"
        End If
        If nameList.Count() = numberOfPlayers Then
            gameLogicManager.CreatePlayers(nameList)
            gotoGame()
        End If
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        timerHalt()
        gotoMainMenu()
    End Sub

    Private Sub btnAddWord_Click(sender As Object, e As EventArgs) Handles btnAddWord.Click
        Dim word = txtPlayerXWord.Text.ToLower

        'Dim isOnBoard As Boolean = 
        'Dim isOnBoard = True

        Dim alpha As Regex = New Regex("^[a-z]*$")


        If alpha.IsMatch(word) Then
            If word.Length >= 3 And word.Length < 13 Then
                If gameLogicManager.IsRealWord(word) Then
                    If gameLogicManager.SimpleIsOnBoard(word) Then
                        If Not tmpPlayerXWords.Contains(word) Then
                            tmpPlayerXWords.Add(txtPlayerXWord.Text.ToLower())
                            rtbPlayerXWords.Text += word + vbNewLine
                            gameLogicManager.GetPlayers()(tmpCurrentPlayer - 1).AddWord(word)
                            txtPlayerXWord.Text = ""
                            Return
                        Else
                            MsgBox("You already entered this word.", vbExclamation + vbOKOnly, "Word Already Entered")
                        End If
                    Else
                        MsgBox("The word you entered is not on the board.", vbExclamation + vbOKOnly, "Invalid Word")
                    End If
                Else
                    MsgBox("The word you entered is not in the dictionary.", vbExclamation + vbOKOnly, "Invalid Word")
                End If
            Else
                MsgBox("All words must be between 3 and 13 characters long", vbExclamation + vbOKOnly, "Invalid Word")
            End If
        Else
            MsgBox("Words can only contain alphabetical characters", vbExclamation + vbOKOnly, "Invalid Word")
        End If
        txtPlayerXWord.SelectAll()
    End Sub

    ''' <summary>
    ''' Clean up
    ''' </summary>
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        timerHalt()
    End Sub

    Private Sub btnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click
        txtPlayerXWord.Text = ""
        tmpPlayerXWords.Clear()
        rtbPlayerXWords.Clear()
        'gameLogicManager.
        ' Set the score for a particular player
        If tmpCurrentPlayer < numberOfPlayers Then
            tmpCurrentPlayer += 1
            lblPlayerX.Text = gameLogicManager.GetPlayers(tmpCurrentPlayer - 1).GetName()
        Else
            gotoResults()
        End If
    End Sub

    Private Sub txtPlayerXWord_TextChanged(sender As Object, e As EventArgs) Handles txtPlayerXWord.TextChanged
        Dim alpha As Regex = New Regex("^[a-zA-Z]{0,13}$")

        If alpha.IsMatch(txtPlayerXWord.Text) Then
            txtPlayerXWord.ForeColor = Color.Black
        Else
            txtPlayerXWord.ForeColor = Color.Red
        End If
    End Sub

    Private Sub btnNewGame_Click(sender As Object, e As EventArgs) Handles btnNewGame.Click
        nameList.clear()
        gotoMainMenu()
    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        timerHalt()
        gotoEnterWords()
    End Sub

    Private Sub btnRescramble_Click(sender As Object, e As EventArgs) Handles btnRescramble.Click
        gotoGame()
    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        gotoGame()
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Center(currentObject, Me)
    End Sub
End Class
