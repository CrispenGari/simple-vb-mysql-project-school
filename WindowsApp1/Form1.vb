Imports MySql.Data.MySqlClient
Public Class Form1
    Dim _title As String = "UNIVESITY OF FORT HARE CLINIC"
    Dim background_path As String = "C:\Users\crisp\source\repos\WindowsApp1\WindowsApp1\static\cover.jpg"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackgroundImage = Image.FromFile(background_path)
        Me.Text = _title
        Label2.BackColor = Color.Transparent
        Label3.BackColor = Color.Transparent
        Me.Text = _title
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.CenterToScreen()
        Dim icon As Icon = Icon.ExtractAssociatedIcon("C:\Users\crisp\source\repos\WindowsApp1\WindowsApp1\static\icon.ico")

        Me.Icon = icon
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form2.Show()
        Form2.Text = _title
        Form2.BackgroundImage = Image.FromFile(background_path)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim response As DialogResult = MessageBox.Show("Are you sure you want to quit this App?", _title, MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            Close()
        End If
    End Sub
End Class
