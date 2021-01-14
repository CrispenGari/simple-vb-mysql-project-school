
Imports MySql.Data.MySqlClient
Public Class Form3
    Dim _title As String = "UNIVESITY OF FORT HARE CLINIC"
    Dim conn As New MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=STUDENTSPATIENTS")

    Dim stringFormat As String = "{0, -15}{1, 20}{2, 20}{3, 2}{4, -90}"
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = _title
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.CenterToScreen()
        Dim icon As Icon = Icon.ExtractAssociatedIcon("C:\Users\crisp\source\repos\WindowsApp1\WindowsApp1\static\icon.ico")

        Me.Icon = icon
        PictureBox1.Image = Image.FromFile("C:\Users\crisp\source\repos\WindowsApp1\WindowsApp1\static\icon.ico")
        ListBox1.Font = New Font("Courier New", 10)
        ListBox1.Items.Add(String.Format(stringFormat, "STUDENT NUMBER", "STUDENT NAME", "STUDENT SURNAME", "", "STUDENT SICKNESS DESCRIPTION"))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim response As DialogResult = MessageBox.Show("Are you sure you want to clear the add patient fields?", "Clearing The Text Fields", MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox14.Text = ""
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim response As DialogResult = MessageBox.Show("Are you sure you want to clear the update patient fields?", "Clearing The Text Fields", MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            TextBox13.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox15.Text = ""
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim response As DialogResult = MessageBox.Show("Are you sure you want to clear the delete patient fields?", "Clearing The Text Fields", MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim response As DialogResult = MessageBox.Show("Are you sure you want to clear the search patient fields?", "Clearing The Text Fields", MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox12.Text = ""
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim id As Integer = Val(TextBox10.Text)
        Dim name As String = TextBox11.Text
        Dim surname = TextBox12.Text
        Dim found As Boolean = Not True

        ListBox1.Items.Clear()
        ListBox1.Items.Add(String.Format(stringFormat, "STUDENT NUMBER", "STUDENT NAME", "STUDENT SURNAME", "", "STUDENT SICKNESS DESCRIPTION"))

        Try
            conn.Open()
            Dim stm As String
            If name IsNot "" And surname IsNot "" Then
                stm = "SELECT * FROM STUDENTS_PATIENTS WHERE STUDENT_NUMBER LIKE " & id & " AND STUDENT_NAME = '" &
                    name & "' AND STUDENT_SURNAME LIKE '" & surname & "' LIMIT 1;"
            Else
                stm = "SELECT * FROM STUDENTS_PATIENTS WHERE STUDENT_NUMBER LIKE " & id & " LIMIT 1;"
            End If
            Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                found = Not found
                ListBox1.Items.Add(String.Format(stringFormat, reader.GetString(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(3)))
            End While
            If Not found Then
                MessageBox.Show("CAN NOT FIND THE PATIENT WITH THE ID: " & id, _title)
            End If

        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        '' Delete button
        Dim id As Integer = Val(TextBox7.Text)
        Dim name As String = TextBox8.Text
        Dim surname = TextBox9.Text
        ListBox1.Items.Clear()
        ListBox1.Items.Add(String.Format(stringFormat, "STUDENT NUMBER", "STUDENT NAME", "STUDENT SURNAME", "", "STUDENT SICKNESS DESCRIPTION"))

        Dim response As DialogResult = MessageBox.Show("Are you sure you want to DELETE the student with ID: " & id, _title, MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            Try
                conn.Open()
                Dim stm As String
                If name IsNot "" And surname IsNot "" Then
                    stm = "DELETE FROM STUDENTS_PATIENTS WHERE STUDENT_NUMBER LIKE " & id & " AND STUDENT_NAME = '" &
                        name & "' AND STUDENT_SURNAME LIKE '" & surname & "' LIMIT 1;"
                Else
                    stm = "DELETE FROM STUDENTS_PATIENTS WHERE STUDENT_NUMBER LIKE " & id & " LIMIT 1;"
                End If
                Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show("THE STUDENT WAS REMOVED SUCCESSFULLY", _title)

                '' CLEAR THE TEXT BOXES AND SHOW THE SUDENTS AVAILABLE
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                cmd = New MySqlCommand("SELECT * FROM STUDENTS_PATIENTS", conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    ListBox1.Items.Add(String.Format(stringFormat, reader.GetString(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(3)))
                End While

            Catch ex As MySqlException
                Console.WriteLine("Error: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim id As Integer = Val(TextBox1.Text)
        Dim name As String = TextBox2.Text
        Dim surname As String = TextBox3.Text
        Dim description As String = TextBox14.Text

        If id <> 0 And name IsNot "" And surname IsNot "" And description IsNot "" Then
            Try
                conn.Open()
                Dim stm As String = "INSERT INTO STUDENTS_PATIENTS(STUDENT_NUMBER, STUDENT_NAME, STUDENT_SURNAME,
SICKNESS_DESCRIPTION) VALUES(" & id & ",'" & name & "', '" & surname & "','" & description & "');"
                Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
                cmd.ExecuteNonQuery()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox14.Text = ""
                MessageBox.Show("THE PATIENT HAS BEEN ADDED!!", _title)
            Catch ex As MySqlException
                Console.WriteLine("Error: " & ex.Message)
            Finally
                ListBox1.Items.Clear()
                ListBox1.Items.Add(String.Format(stringFormat, "STUDENT NUMBER", "STUDENT NAME", "STUDENT SURNAME", "", "STUDENT SICKNESS DESCRIPTION"))
                Dim cmd = New MySqlCommand("SELECT * FROM STUDENTS_PATIENTS", conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    ListBox1.Items.Add(String.Format(stringFormat, reader.GetString(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(3)))
                End While
                conn.Close()
            End Try
        Else
            MessageBox.Show("FILL ALL THE FIELDS WITH DATA OR THE STUDENT NUMBER OF THAT PATIENT ALREADY EXIST IN THE DATABASE!!", _title)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim id As Integer = Val(TextBox13.Text)
        Dim index As Integer = 0
        Dim found As Boolean = Not True
        If id <> 0 Then
            Try
                conn.Open()
                Dim stm As String = "SELECT * FROM STUDENTS_PATIENTS WHERE STUDENT_NUMBER LIKE " & id & " LIMIT 1;"
                Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    found = Not found
                    TextBox4.Text = reader.GetString(0)
                    TextBox5.Text = reader.GetString(1)
                    TextBox6.Text = reader.GetString(2)
                    TextBox15.Text = reader.GetString(3)
                End While
                If Not found Then
                    MessageBox.Show("CAN NOT FIND THE PATIENT WITH THE ID: " & id, _title)
                End If

            Catch ex As MySqlException
                Console.WriteLine("Error: " & ex.Message)
            Finally
                conn.Close()
            End Try
        Else
            MessageBox.Show("THE ID OF THE PATIENT TO BE UPDATED IS REQUIRED AND CAN NOT BE NULL!!", _title)

        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim response As DialogResult = MessageBox.Show("Are you sure you want to sign out of this App?", _title, MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            Me.Hide()
            Form2.Show()
        End If

    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ListBox1.Items.Clear()
        ListBox1.Items.Add(String.Format(stringFormat, "STUDENT NUMBER", "STUDENT NAME", "STUDENT SURNAME", "", "STUDENT SICKNESS DESCRIPTION"))
        Try
            conn.Open()
            Dim cmd = New MySqlCommand("SELECT * FROM STUDENTS_PATIENTS", conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                ListBox1.Items.Add(String.Format(stringFormat, reader.GetString(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(3)))
            End While
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '' UPDATE BUTTON
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim id As Integer = Val(TextBox4.Text)
        Dim id_query As Integer = Val(TextBox13.Text)
        Dim name As String = TextBox5.Text
        Dim surname As String = TextBox6.Text
        Dim description As String = TextBox15.Text
        Dim response As DialogResult = MessageBox.Show("Are you sure you want to UPDATE the student with ID: " & id, _title, MessageBoxButtons.YesNoCancel)
        If response = 6 Then
            Try
                conn.Open()
                If id <> 0 And name IsNot "" And surname IsNot "" And description IsNot "" Then
                    Dim stm As String = "UPDATE STUDENTS_PATIENTS SET STUDENT_NUMBER = " & id & ", STUDENT_NAME='" & name & "', STUDENT_SURNAME='" & surname & "',SICKNESS_DESCRIPTION = '" & description & "' WHERE STUDENT_NUMBER LIKE " & id_query & ";"
                    Dim cmd = New MySqlCommand(stm, conn)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("THE DETAILS OF " & name & " " & surname & " HAS BEEN UPDATED!!", _title)
                    TextBox13.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""
                    TextBox6.Text = ""
                    TextBox15.Text = ""
                Else
                    MessageBox.Show("ALL THE FIELDS MUST BE FILLED WITH VALUES TO UPDATE A STUDENT.", _title)
                End If

            Catch ex As MySqlException
                Console.WriteLine("Error: " & ex.Message)
            Finally
                ListBox1.Items.Clear()
                ListBox1.Items.Add(String.Format(stringFormat, "STUDENT NUMBER", "STUDENT NAME", "STUDENT SURNAME", "", "STUDENT SICKNESS DESCRIPTION"))
                Dim cmd = New MySqlCommand("SELECT * FROM STUDENTS_PATIENTS", conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    ListBox1.Items.Add(String.Format(stringFormat, reader.GetString(0), reader.GetString(1), reader.GetString(2), "", reader.GetString(3)))
                End While
                conn.Close()
            End Try
        End If

    End Sub
End Class