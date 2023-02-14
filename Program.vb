Module Module1
    Sub Main()
        ' Set console window size
        Console.WindowWidth = 60
        Console.WindowHeight = 8

        While True ' Allow for multiple shutdowns in the same session
            ' Ask user for desired shutdown time
            Console.WriteLine("Please choose a shutdown time:")
            Console.WriteLine("1. 30 minutes")
            Console.WriteLine("2. 1 hour")
            Console.WriteLine("3. 2 hours")
            Console.WriteLine("4. 4 hours")
            Console.WriteLine("5. Custom time")
            Console.Write("Enter your choice (1-5): ")
            Dim choice As Integer
            Dim validChoice As Boolean = Integer.TryParse(Console.ReadLine(), choice)
            If Not validChoice OrElse choice < 1 OrElse choice > 5 Then
                Console.WriteLine("Invalid input. Please enter a number from 1 to 5.")
                Continue While
            End If

            ' Set shutdown time based on user choice
            Dim shutdownTime As Date
            Select Case choice
                Case 1
                    shutdownTime = Now.AddMinutes(30)
                Case 2
                    shutdownTime = Now.AddHours(1)
                Case 3
                    shutdownTime = Now.AddHours(2)
                Case 4
                    shutdownTime = Now.AddHours(4)
                Case 5
                    Console.Write("Enter the number of minutes (01-1440): ")
                    Dim minutes As Integer
                    Dim validMinutes As Boolean = Integer.TryParse(Console.ReadLine(), minutes)
                    If Not validMinutes OrElse minutes < 1 Or minutes > 1440 Then
                        Console.WriteLine("Invalid input. Please enter a number from 01 to 1440.")
                        Continue While
                    End If
                    shutdownTime = Now.AddMinutes(minutes)
            End Select

            ' Display countdown to shutdown
            Console.WriteLine("Your computer will shut down at " & shutdownTime.ToString("hh:mm:ss tt") & ".")
            Console.WriteLine("To cancel the shutdown, type '-a' and press Enter.")
            While shutdownTime > Now
                Console.Write("Time remaining: " & (shutdownTime - Now).ToString("hh\:mm\:ss") & "  ")
                If Console.KeyAvailable Then
                    Dim key = Console.ReadKey(True)
                    If key.KeyChar = "-" Then
                        Dim buffer = key.KeyChar.ToString()
                        While Console.KeyAvailable
                            buffer += Console.ReadKey(True).KeyChar
                        End While
                        If buffer.ToLower() = "-a" Then
                            Console.WriteLine("Shutdown cancelled.")
                            Exit While
                        End If
                    End If
                    If key.KeyChar = "y" Then
                        Exit While
                    End If
                    If key.KeyChar = "n" Then
                        Exit Sub
                    End If
                    If Not (key.KeyChar = ControlChars.Back) AndAlso Not (Char.IsDigit(key.KeyChar)) Then
                        Console.WriteLine("Invalid input. Only '-a', 'y', and 'n' are allowed.")
                        Continue While
                    End If
                End If
                Threading.Thread.Sleep(1000)
                Console.CursorLeft = 0
            End While
        End While

        Console.ReadLine()
    End Sub
End Module
