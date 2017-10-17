Public Class frmMain
    Private numberOfPlayers = 2
    Private gameLogicManager As GameLogic
    Private nameList = New List(Of String)


    Private Sub Center(interior As Object, exterior As Object)
        interior.left = CInt((exterior.Width / 2) - (interior.Width / 2))
        interior.top = CInt((exterior.Height / 2) - (interior.Height / 2))
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 500
        Me.Height = 500
        Me.MinimumSize = New Drawing.Size(500, 500)
        Me.MaximumSize = New Drawing.Size(500, 500)
        gotoMainMenu()

    End Sub

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

    Private Sub gotoEnterNames()
        startScreen.Visible = False
        nameScreen.Visible = True
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = False
        Center(nameScreen, Me)
        Me.AcceptButton = btnOk
        txtPlayerName.Focus()
        lblEnterName.Text = "Player " + CStr(nameList.Count() + 1) + " enter your name"
        nameList = New List(Of String)
    End Sub

    Private Sub gotoGame()
        startScreen.Visible = False
        nameScreen.Visible = False
        gameScreen.Visible = True
        inputScreen.Visible = False
        scoreScreen.Visible = False
        Center(gameScreen, Me)
    End Sub

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

    Private Sub gotoResults()
        startScreen.Visible = False
        nameScreen.Visible = False
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = True
        Center(scoreScreen, Me)
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
        Else
            nameList.Add(txtPlayerName.Text)
            txtPlayerName.Text = ""
            lblEnterName.Text = "Player " + CStr(nameList.Count() + 1) + " enter your name"
        End If
        If nameList.Count() = numberOfPlayers Then
            gotoGame()
        End If
    End Sub
End Class
