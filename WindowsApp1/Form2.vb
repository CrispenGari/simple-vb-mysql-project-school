
Imports MySql.Data.MySqlClient
Public Class Form2
    Dim conn As New MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=STUDENTSPATIENTS")
    Dim _title As String = "UNIVESITY OF FORT HARE CLINIC"
    Dim background_path As String = "C:\Users\crisp\source\repos\WindowsApp1\WindowsApp1\static\cover.jpg"
    Dim username, password As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = _title
        Label1.BackColor = Color.Transparent
        Label2.BackColor = Label1.BackColor
        Label5.BackColor = Label1.BackColor
        Label6.BackColor = Label1.BackColor
        CheckBox1.BackColor = Label5.BackColor
        Label3.BackColor = Label5.BackColor
        Me.Text = _title
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.CenterToScreen()
        Dim icon As Icon = Icon.ExtractAssociatedIcon("C:\Users\crisp\source\repos\WindowsApp1\WindowsApp1\static\icon.ico")

        Me.Icon = icon
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If TextBox2.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
            CheckBox1.Text = "Hide Password"
        Else
            TextBox2.UseSystemPasswordChar = Not False
            CheckBox1.Text = "Show Password"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '' DATABASE PART
        username = TextBox1.Text
        password = TextBox2.Text
        ''Dim cmd As New MySqlCommand()
        Try
            conn.Open()
            Dim stm As String = "SELECT id, username, password FROM admin WHERE username LIKE '" & username & "' AND password LIKE '" & password & "' LIMIT 1;"

            Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Dim userFound As Boolean = False
            While reader.Read()
                userFound = Not False
            End While
            If userFound Then
                '' Go to the next Page
                Me.Hide()
                Form3.Show()
                Label3.Text = ""
                TextBox2.Text = ""
                TextBox1.Text = ""

            Else
                TextBox2.Text = ""
                Label3.Text = "Invalid username or password"
            End If
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim response As DialogResult = MessageBox.Show("Are you sure you want to clear the password and username field(s)?", "Clearing The Text Fields", MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            TextBox1.Text = ""
            TextBox2.Text = TextBox1.Text
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        Form1.Show()
    End Sub
End Class