Imports System.Runtime.InteropServices

Module Program
    Public Const COUNT_UP_KEY = ConsoleKey.PageUp
    Public Const COUNT_DOWN_KEY = ConsoleKey.PageDown
    Public Const RESET_KEY = ConsoleKey.Delete

    Sub Main(args As String())
        Console.SetWindowSize(17, 4)
        Console.CursorVisible = False

        Dim defaultValue(9) As Integer
        Dim consoleOutput As Boolean

        If args.Length > 0 Then
            Boolean.TryParse(args(0), consoleOutput)
        End If

        Dim counter(9) As Integer
        For i = 0 To 9
            If args.Length > i + 1 Then
                Integer.TryParse(args(i + 1), defaultValue(i))
            End If
            counter(i) = defaultValue(i)
            If consoleOutput AndAlso defaultValue(i) <> 0 Then
                SetCursorPosition(i)
                Console.Write($"{counter(i),4}")
            End If
        Next

        Dim cKey() = {ConsoleKey.NumPad0,
                      ConsoleKey.NumPad1,
                      ConsoleKey.NumPad2,
                      ConsoleKey.NumPad3,
                      ConsoleKey.NumPad4,
                      ConsoleKey.NumPad5,
                      ConsoleKey.NumPad6,
                      ConsoleKey.NumPad7,
                      ConsoleKey.NumPad8,
                      ConsoleKey.NumPad9}

        'Check keypresses forever
        Do
            'Count up
            If GetAsyncKeyState(COUNT_UP_KEY) Then
                For i = 0 To 9
                    If GetAsyncKeyState(cKey(i)) Then
                        counter(i) += 1
                        IO.File.WriteAllText($"counter{i}.txt", counter(i))
                        If Not consoleOutput Then
                            SetCursorPosition(i)
                            Console.Write($"{counter(i),4}")
                        End If

                        ' Wait to only trigger once per keypress
                        While GetAsyncKeyState(COUNT_UP_KEY) AndAlso GetAsyncKeyState(cKey(i))
                        End While

                        Exit For
                    End If
                Next
            End If

            'Count down
            If GetAsyncKeyState(COUNT_DOWN_KEY) Then
                For i = 0 To 9
                    If GetAsyncKeyState(cKey(i)) Then
                        counter(i) -= 1
                        IO.File.WriteAllText($"counter{i}.txt", counter(i))
                        If Not consoleOutput Then
                            SetCursorPosition(i)
                            Console.Write($"{counter(i),4}")
                        End If

                        ' Wait to only trigger once per keypress
                        While GetAsyncKeyState(COUNT_DOWN_KEY) AndAlso GetAsyncKeyState(cKey(i))
                        End While

                        Exit For
                    End If
                Next
            End If

            'Reset
            If GetAsyncKeyState(RESET_KEY) Then
                For i = 0 To 9
                    If GetAsyncKeyState(cKey(i)) Then
                        counter(i) = defaultValue(i)
                        IO.File.WriteAllText($"counter{i}.txt", counter(i))
                        If Not consoleOutput Then
                            SetCursorPosition(i)
                            Console.Write($"{counter(i),4}")
                        End If

                        ' Wait to only trigger once per keypress
                        While GetAsyncKeyState(RESET_KEY) AndAlso GetAsyncKeyState(cKey(i))
                        End While

                        Exit For
                    End If
                Next
            End If

            'Don't make the cpu too sad
            Threading.Thread.Sleep(15)
        Loop

    End Sub

    Private Sub SetCursorPosition(NumKey As Integer)
        Select Case NumKey
            Case 0
                Console.CursorTop = 3
                Console.CursorLeft = 0
            Case 1
                Console.CursorTop = 2
                Console.CursorLeft = 0
            Case 2
                Console.CursorTop = 2
                Console.CursorLeft = 6
            Case 3
                Console.CursorTop = 2
                Console.CursorLeft = 12
            Case 4
                Console.CursorTop = 1
                Console.CursorLeft = 0
            Case 5
                Console.CursorTop = 1
                Console.CursorLeft = 6
            Case 6
                Console.CursorTop = 1
                Console.CursorLeft = 12
            Case 7
                Console.CursorTop = 0
                Console.CursorLeft = 0
            Case 8
                Console.CursorTop = 0
                Console.CursorLeft = 6
            Case 9
                Console.CursorTop = 0
                Console.CursorLeft = 12
        End Select
        Console.Write("    ")
        Console.CursorLeft = Console.CursorLeft - 4
    End Sub

    <DllImport("user32.dll")>
    Public Function GetAsyncKeyState(ByVal vKey As ConsoleKey) As Short
    End Function

End Module