Imports System.IO

Module Program
    Dim fileName As String = "StudentRecords.txt"

    Sub Main()
        Dim choice As Integer

        Do
            Console.Clear()
            Console.WriteLine("===== STUDENT RECORDS MANAGEMENT SYSTEM =====")
            Console.WriteLine("1. Add New Student Record")
            Console.WriteLine("2. Display All Records")
            Console.WriteLine("3. Exit")
            Console.Write("Enter your choice (1-3): ")

            If Not Integer.TryParse(Console.ReadLine(), choice) Then
                Console.WriteLine("Invalid choice! Press any key...")
                Console.ReadKey()
                Continue Do
            End If

            Select Case choice
                Case 1
                    AddRecord()
                Case 2
                    DisplayRecords()
                Case 3
                    Console.WriteLine("Exiting... Thank you!")
                    Exit Do
                Case Else
                    Console.WriteLine("Invalid choice! Press any key...")
                    Console.ReadKey()
            End Select

        Loop While True

    End Sub

    ' --- ADD RECORD (Append mode) ---
    Sub AddRecord()
        Console.Clear()
        Console.WriteLine("--- Add New Student ---")

        Console.Write("Enter Registration Number (e.g ST001): ")
        Dim regNo As String = Console.ReadLine()

        Console.Write("Enter Student Name: ")
        Dim name As String = Console.ReadLine()

        Console.Write("Enter Marks: ")
        Dim marks As String = Console.ReadLine()

        ' Validation
        If String.IsNullOrWhiteSpace(regNo) Or String.IsNullOrWhiteSpace(name) Then
            Console.WriteLine("Error: RegNo and Name cannot be empty!")
            Console.ReadKey()
            Return
        End If

        Try
            ' True = Append mode (does NOT delete old records)
            Using writer As New StreamWriter(fileName, True)
                writer.WriteLine($"{regNo}, {name}, {marks}")
            End Using

            Console.WriteLine($"Record saved successfully to {fileName}!")
        Catch ex As UnauthorizedAccessException
            Console.WriteLine("Error: Access denied to file.")
        Catch ex As IOException
            Console.WriteLine("File I/O Error: " & ex.Message)
        Catch ex As Exception
            Console.WriteLine("Unexpected Error: " & ex.Message)
        End Try

        Console.WriteLine("Press any key to continue...")
        Console.ReadKey()
    End Sub

    ' --- DISPLAY RECORDS (Handle missing file) ---
    Sub DisplayRecords()
        Console.Clear()
        Console.WriteLine($"--- All Records from {fileName} ---")

        Try
            If Not File.Exists(fileName) Then
                Throw New FileNotFoundException()
            End If

            Using reader As New StreamReader(fileName)
                Dim count As Integer = 0
                Console.WriteLine("RegNo | Name | Marks")
                Console.WriteLine("---------------------------------")
                While Not reader.EndOfStream
                    Dim line As String = reader.ReadLine()
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Console.WriteLine(line)
                        count += 1
                    End If
                End While
                Console.WriteLine("---------------------------------")
                Console.WriteLine($"Total Records: {count}")
            End Using

        Catch ex As FileNotFoundException
            Console.WriteLine("No records found! File does not exist yet.")
            Console.WriteLine("Please add records first using Option 1.")
        Catch ex As IOException
            Console.WriteLine("Error reading file: " & ex.Message)
        Catch ex As Exception
            Console.WriteLine("Unexpected Error: " & ex.Message)
        End Try

        Console.WriteLine(vbCrLf & "Press any key to continue...")
        Console.ReadKey()
    End Sub

End Module





