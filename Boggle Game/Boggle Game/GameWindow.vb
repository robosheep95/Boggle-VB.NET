'Project: Boggle
'By:      Allen Retzler and Taylor Scafe
'Date:    10-12-17

'References
'Background Image By Jolene Faber https://www.flickr.com/photos/jovanlaar/2137872208/in/photolist-4fV9VE-6dEdXJ-7vEVv7-qt6uTL-7MYx2d-7bfhL1-7ZYXGq-5RhZ97-4MitNY-nZB3fK-5hgoH6-vwp1Us-aY4g3-4T8hoV-7bfimU-8qiVbt-7sF14G-8qvbQo-9UMgNA-4QioXL-aCwXC-8TuGnf-bgKm6M-pqSmr-aQfrr2-FdFrw-fF9JC-Lj3bW-66Pr88-erbtWm-jEQEc-7zYNyp-5FYch3-7cPczA-7a6zqc-4zQYqx-UQ3ctU-6XhsiQ-TdCi-8hfGKv-8cEacT-68vb2y-b2uReR-94ZJ2v-8hV7T1-cMRco-pWGe9V-qDMJSB-cE4a7u-axDX6i
'Splash Screen Image By Rich Brooks https://www.flickr.com/photos/therichbrooks/4039402557/in/photolist-Hfsnb-8sRUhS-jM3h1-5MV5rb-7wid4Y-3idFWu-ai62v-4SK7dS-2nsWsm-5MtwKv-wqFHV-aqpBv1-Ap2bS-79WZDF-4grY5S-oB74G-66Lfb7-9ARFN-64ysgN-6H1kKA-nTKPqR-2jL2dp-9gh7oJ-7Gqfdb-QGYVZ-7a1Rty-7WPbx3-tAwBF-jC885-ibZuX-8HfcuR-5Mtv6p-5MturK-5MxLLA-dJm4C5-5E9Sxx-9AdHWK-8Xu1Ma-4paK8z-7ikCdo-bexBiK-5Mtanx-cvjrZ-bhF7vF-9jpVvi-5E9SR4-4paHzk-5ET5b9-5MttVi-5Mtxft

Imports System.ComponentModel
Imports VB = Microsoft.VisualBasic
Imports System.Text
Imports Regex = System.Text.RegularExpressions.Regex

Public Class GameWindow
    'Data Collection Variables
    Private numberOfPlayers As UShort = 2
    Private gameLogicManager As GameLogic
    Private nameList = New List(Of String)

    'Timer Variables
    Private timerMinutes As UShort = 3
    Private timerSeconds As UShort = 0
    Private timerMax As UInt32 = timerMinutes * 60 + timerSeconds
    Private timerHalted = False

    'Player Trackers
    Private tmpCurrentPlayer = 1
    Private tmpPlayerXWords As List(Of String) = New List(Of String)

    'Resize Reference
    Private currentObject As Object = startScreen

    'Game Window Element Collections
    Private pnlScreens As List(Of Panel)
    Private lblGameDice As List(Of Label)
    Private rtbResultPlayerNames As List(Of RichTextBox)
    Private lblResultPlayerScores As List(Of Label)
    Private lblResultPlayerName As List(Of Label)

    ''' <summary>
    ''' Function that runs when the main form is loaded
    ''' </summary>
    Private Sub GameWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlScreens = New List(Of Panel)
        pnlScreens.AddRange({startScreen, nameScreen, gameScreen, inputScreen, scoreScreen})
        lblGameDice = New List(Of Label)
        lblGameDice.AddRange({lblDie1, lblDie2, lblDie3, lblDie4, lblDie5, lblDie6, lblDie7, lblDie8, lblDie9, lblDie10, lblDie11, lblDie12, lblDie13, lblDie14, lblDie15, lblDie16})
        rtbResultPlayerNames = New List(Of RichTextBox)
        rtbResultPlayerNames.AddRange({rtbPlayer1Words, rtbPlayer2Words, rtbPlayer3Words, rtbPlayer4Words})
        lblResultPlayerScores = New List(Of Label)
        lblResultPlayerScores.AddRange({lblP1Score, lblP2Score, lblP3Score, lblP4Score})
        lblResultPlayerName = New List(Of Label)
        lblResultPlayerName.AddRange({lblP1Name, lblP2Name, lblP3Name, lblP4Name})

        gameLogicManager = New GameLogic()
        Width = 800
        Height = 600
        MinimumSize = New Drawing.Size(800, 600)
        GotoMainMenu()
    End Sub

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
    Private Sub Timer(seconds As Double)
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
    Private Function TimerDecrement() As Boolean
        If timerMinutes > 0 OrElse timerSeconds > 0 Then
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
    Private Sub TimerUpdate()
        prgTimer.Maximum = timerMax
        prgTimer.Value = 60 * timerMinutes + timerSeconds
        Dim ts As String
        If timerSeconds < 10 Then
            ts = "0" & CStr(timerSeconds)
        Else
            ts = CStr(timerSeconds)
        End If
        lblTimerText.Text = CStr(timerMinutes) & ":" & ts
    End Sub

    ''' <summary>
    ''' Stops the timer from counting down
    ''' </summary>
    Private Sub TimerHalt()
        timerHalted = True
    End Sub

    ''' <summary>
    ''' Allows the timer to function if it were halted
    ''' </summary>
    Private Sub TimerReset()
        timerHalted = False
    End Sub

    ''' <summary>
    ''' Goes to the Main Menu Screen
    ''' </summary>
    Private Sub GotoMainMenu()
        ChangeScreen(0)
        AcceptButton = btnStartGame
        btnStartGame.Focus()
    End Sub

    ''' <summary>
    ''' Goes to the screen where the players enter their names (just before the actual game)
    ''' </summary>
    Private Sub GotoEnterNames()
        ChangeScreen(1)
        AcceptButton = btnOk
        txtPlayerName.Text = ""
        txtPlayerName.Focus()
        nameList = New List(Of String)
        lblEnterName.Text = "Player 1 enter your name"
    End Sub

    ''' <summary>
    ''' Goes to the actual game and starts it
    ''' </summary>
    Private Sub GotoGame()
        ChangeScreen(2)

        btnRescramble.Visible = False
        btnFinish.Visible = False

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
        TimerUpdate()
        TimerReset()

        'Scrambeling The Board
        For t = 0 To 20
            For i = 0 To 15
                lblGameDice(i).Text = toUpper(qToQu(gameLogicManager.GetBoard()(i)))
                If gameLogicManager.GetSpecial()(i) Then
                    lblGameDice(i).ForeColor = Color.Red
                Else
                    lblGameDice(i).ForeColor = Color.Black
                End If
            Next
            gameLogicManager.ScrambleBoard()
            Timer(0.06)
        Next

        btnRescramble.Visible = True
        btnFinish.Visible = True

        For i = 0 To 15
            lblGameDice(i).Text = toUpper(qToQu(gameLogicManager.GetBoard()(i)))
            If gameLogicManager.GetSpecial()(i) Then
                lblGameDice(i).ForeColor = Color.Red
            Else
                lblGameDice(i).ForeColor = Color.Black
            End If
        Next

        While Not TimerDecrement()
            TimerUpdate()
            Timer(1)
        End While
        TimerUpdate()

        'Figure out a better way to do this
        If gameScreen.Visible = True Then
            GotoEnterWords()
        End If


    End Sub

    ''' <summary>
    ''' Goes to the screen where players enter the words they found (just after the actual game)
    ''' </summary>
    Private Sub GotoEnterWords()
        ChangeScreen(3)
        AcceptButton = btnAddWord
        txtPlayerXWord.Focus()
        tmpCurrentPlayer = 1
        lblPlayerX.Text = gameLogicManager.GetPlayers(0).GetName()
    End Sub

    ''' <summary>
    ''' Goes to the screen where scores and words found are displayed and loads data
    ''' </summary>
    Private Sub GotoResults()
        Dim players = gameLogicManager.GetPlayers()
        ChangeScreen(4)
        gameLogicManager.ScorePlayers()

        For j As Integer = 0 To lblResultPlayerName.Count - 1
            rtbResultPlayerNames(j).Text = ""
            rtbResultPlayerNames(j).Hide()
            lblResultPlayerName(j).Hide()
            lblResultPlayerScores(j).Hide()
        Next

        Dim i As Integer = 0
        For Each player In players

            lblResultPlayerName(i).Text = player.GetName()
            lblResultPlayerName(i).Show()

            lblResultPlayerScores(i).Text = player.GetScore & " Points"
            lblResultPlayerScores(i).Show()

            Dim builder As StringBuilder = New StringBuilder
            For Each word In player.GetWordList()
                builder.Append(word & vbNewLine)
            Next
            rtbResultPlayerNames(i).Text = builder.ToString
            rtbResultPlayerNames(i).Show()

            i += 1
        Next

    End Sub

    ''' <summary>
    ''' Swaps panels to the selected panel
    ''' </summary>
    ''' <param name="index">Index of panel to display</param>
    Private Sub ChangeScreen(ByVal index As Integer)
        For Each panel In pnlScreens
            panel.Visible = False
        Next
        pnlScreens(index).Visible = True
        Center(pnlScreens(index), Me)
        currentObject = CType(pnlScreens(index), Object)
    End Sub


    '================Button Handlers====================

    ''' <summary>
    ''' Opens Allen Retzler's GitHub page
    ''' </summary>
    Private Sub LblAllenRetzler_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAllenRetzler.LinkClicked
        Process.Start("https://github.com/allenretz")
    End Sub

    ''' <summary>
    ''' Opens Taylor Scafe's GitHub page
    ''' </summary>
    Private Sub LblTaylorScafe_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTaylorScafe.LinkClicked
        Process.Start("https://github.com/robosheep95")
    End Sub

    ''' <summary>
    ''' Opens project's GitHub page
    ''' </summary>
    Private Sub LblSource_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblSource.LinkClicked
        Process.Start("https://github.com/robosheep95/Boggle-VB.NET")
    End Sub

    ''' <summary>
    ''' Starts the game with the number of players selected
    ''' </summary>
    Private Sub BtnStartGame_Click(sender As Object, e As EventArgs) Handles btnStartGame.Click
        If radio1Player.Checked() Then
            numberOfPlayers = 1
        ElseIf Radio2Players.Checked() Then
            numberOfPlayers = 2
        ElseIf radio3Players.Checked() Then
            numberOfPlayers = 3
        Else
            numberOfPlayers = 4
        End If
        GotoEnterNames()
    End Sub

    ''' <summary>
    ''' Prompts if player wants to quit and quits if selected.
    ''' </summary>
    Private Sub BtnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        If MsgBox("Are you sure you want to quit?", vbQuestion + vbYesNo, "Quit") = vbYes Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Returns to start screen
    ''' </summary>
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        GotoMainMenu()
    End Sub

    ''' <summary>
    ''' Takes and valadates player name. Starts game if last player has entered a name else it asks for another name
    ''' </summary>
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If txtPlayerName.Text = "" Then
            MsgBox("Your name must be at least 1 character long", vbExclamation + vbOKOnly, "Invalid Name")
        ElseIf nameList.Contains(txtPlayerName.Text) Then
            MsgBox("Your Name must not be the same as a previous player", vbExclamation + vbOKOnly, "Invalid Name")
        Else
            nameList.Add(txtPlayerName.Text)
            txtPlayerName.Text = ""
            lblEnterName.Text = "Player " & CStr(nameList.Count() + 1) & " enter your name"
        End If
        If nameList.Count() = numberOfPlayers Then
            gameLogicManager.CreatePlayers(nameList)
            GotoGame()
        End If
    End Sub

    ''' <summary>
    ''' Returns to start screen
    ''' </summary>
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        TimerHalt()
        GotoMainMenu()
    End Sub

    ''' <summary>
    ''' Checks to see if word entered meets all requirements
    ''' </summary>
    Private Sub BtnAddWord_Click(sender As Object, e As EventArgs) Handles btnAddWord.Click
        Dim word = txtPlayerXWord.Text.ToLower
        Dim alpha As Regex = New Regex("^[a-z]*$")

        If alpha.IsMatch(word) Then
            If word.Length >= 3 AndAlso word.Length < 13 Then
                If gameLogicManager.SimpleIsOnBoard(word) Then
                    If gameLogicManager.IsRealWord(word) Then
                        If Not tmpPlayerXWords.Contains(word) Then
                            tmpPlayerXWords.Add(txtPlayerXWord.Text.ToLower())
                            rtbPlayerXWords.Text += word & vbNewLine
                            gameLogicManager.GetPlayers()(tmpCurrentPlayer - 1).AddWord(word)
                            txtPlayerXWord.Text = ""
                            Return
                        Else
                            MsgBox("You already entered this word.", vbExclamation + vbOKOnly, "Word Already Entered")
                        End If
                    Else
                        MsgBox("The word you entered is not in the dictionary. -1 point", vbExclamation + vbOKOnly, "Invalid Word")
                        gameLogicManager.GetPlayers()(tmpCurrentPlayer - 1).AddScore(-1)
                        tmpPlayerXWords.Add("- " + txtPlayerXWord.Text.ToLower())
                        rtbPlayerXWords.Text += "- " + word & vbNewLine
                        gameLogicManager.GetPlayers()(tmpCurrentPlayer - 1).AddWord("- " + word)
                        txtPlayerXWord.Text = ""
                    End If
                Else
                    MsgBox("The word you entered is not on the board.", vbExclamation + vbOKOnly, "Invalid Word")
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
    Private Sub FrmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        TimerHalt()
    End Sub

    ''' <summary>
    ''' Opens text entry for next player or goes to score page if there are no more players
    ''' </summary>
    Private Sub BtnDone_Click(sender As Object, e As EventArgs) Handles btnDone.Click
        txtPlayerXWord.Text = ""
        tmpPlayerXWords.Clear()
        rtbPlayerXWords.Clear()
        If tmpCurrentPlayer < numberOfPlayers Then
            tmpCurrentPlayer += 1
            lblPlayerX.Text = gameLogicManager.GetPlayers(tmpCurrentPlayer - 1).GetName()
        Else
            GotoResults()
        End If
    End Sub

    ''' <summary>
    ''' Checks to see if word it syntactactly coorect and changes the text color to red if incorrect
    ''' </summary>
    Private Sub TxtPlayerXWord_TextChanged(sender As Object, e As EventArgs) Handles txtPlayerXWord.TextChanged
        Dim alpha As Regex = New Regex("^[a-zA-Z]{0,13}$")

        If alpha.IsMatch(txtPlayerXWord.Text) Then
            txtPlayerXWord.ForeColor = Color.Black
        Else
            txtPlayerXWord.ForeColor = Color.Red
        End If
    End Sub

    ''' <summary>
    ''' Clears user data and moves to the start screen
    ''' </summary>
    Private Sub BtnNewGame_Click(sender As Object, e As EventArgs) Handles btnNewGame.Click
        nameList.clear()
        GotoMainMenu()
    End Sub

    ''' <summary>
    ''' Stops the timer and opens the word submission screen
    ''' </summary>
    Private Sub BtnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        TimerHalt()
        GotoEnterWords()
    End Sub

    ''' <summary>
    ''' Loads a new board
    ''' </summary>
    Private Sub BtnRescramble_Click(sender As Object, e As EventArgs) Handles btnRescramble.Click
        GotoGame()
    End Sub

    ''' <summary>
    ''' Brings up a new board without clearing player data
    ''' </summary>
    Private Sub BtnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        For Each player In gameLogicManager.GetPlayers
            player.ClearWordList()
        Next
        GotoGame()
    End Sub

    ''' <summary>
    ''' Moves the primary panel to the center of the screen
    ''' </summary>
    Private Sub FrmMain_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Center(currentObject, Me)
    End Sub

End Class