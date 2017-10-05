Public Class frmMain


    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 550
        Me.Height = 464
        Me.MinimumSize = New Drawing.Size(550, 464)
        Me.MaximumSize = New Drawing.Size(550, 464)
        gotoStartScreen()


    End Sub

    Private Sub gotoStartScreen()
        StartScreen.Visible = True
        GameScreen.Visible = False
        ScoreScreen.Visible = False
        StartScreen.Left = 265 - (StartScreen.Width / 2)
    End Sub

    Private Sub gotoGameScreen()
        StartScreen.Visible = False
        GameScreen.Visible = True
        ScoreScreen.Visible = False
        GameScreen.Left = 265 - (GameScreen.Width / 2)
    End Sub

    Private Sub gotoScoreScreen()
        StartScreen.Visible = False
        GameScreen.Visible = False
        ScoreScreen.Visible = True
        ScoreScreen.Left = 265 - (ScoreScreen.Width / 2)
    End Sub

    Private Sub LblAllenRetzler_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblAllenRetzler.LinkClicked
        Process.Start("https://github.com/allenretz")
    End Sub

    Private Sub LblTaylorScafe_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblTaylorScafe.LinkClicked
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
End Class
