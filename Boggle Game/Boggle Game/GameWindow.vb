Public Class frmMain
    Private numberOfPlayers = 2
    Private gameLogicManager As GameLogic

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 550
        Me.Height = 464
        Me.MinimumSize = New Drawing.Size(550, 464)
        Me.MaximumSize = New Drawing.Size(550, 464)
        gotoStartScreen()


        'Temp Game Setup
        Dim strPlayerList = New String(1) {"Allen", "Taylor"}
        gameLogicManager = New GameLogic(strPlayerList)

    End Sub

    Private Sub gotoStartScreen()
        startScreen.Visible = True
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = False
        startScreen.Left = 265 - (startScreen.Width / 2)
        Me.AcceptButton = btnStartGame
        btnStartGame.Focus()
    End Sub


    Private Sub gotoGameScreen()
        startScreen.Visible = False
        gameScreen.Visible = True
        inputScreen.Visible = False
        scoreScreen.Visible = False
        gameScreen.Left = 265 - (gameScreen.Width / 2)
    End Sub

    Private Sub gotoInputScreen()
        startScreen.Visible = False
        gameScreen.Visible = False
        inputScreen.Visible = True
        scoreScreen.Visible = False
        inputScreen.Left = 265 - (inputScreen.Width / 2)
        Me.AcceptButton = btnAddWord
        txtPlayerX.Focus()
    End Sub

    Private Sub gotoScoreScreen()
        startScreen.Visible = False
        gameScreen.Visible = False
        inputScreen.Visible = False
        scoreScreen.Visible = True
        scoreScreen.Left = 265 - (scoreScreen.Width / 2)
    End Sub



    Private Sub LblAllenRetzler_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAllenRetzler.LinkClicked
        Process.Start("https://github.com/allenretz")
    End Sub

    Private Sub LblTaylorScafe_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTaylorScafe.LinkClicked
        Process.Start("https://github.com/robosheep95")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        gotoStartScreen()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        gotoGameScreen()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        gotoScoreScreen()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        gotoInputScreen()
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
        gotoGameScreen()
    End Sub
End Class
