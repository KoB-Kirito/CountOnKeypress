Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports Newtonsoft.Json

Module Program

    Sub Main(args As String())
        Console.SetWindowSize(17, 4)
        Console.CursorVisible = False

        'Parse json
        If Not File.Exists("config.json") Then
            Dim tCfg = New Config With {
                .ShowConsoleOutput = True,
                .CountUpKey = ConsoleKey.PageUp,
                .CountDownKey = ConsoleKey.PageDown,
                .ResetKey = ConsoleKey.Delete,
                .CounterKeys = {96, 97, 98, 99, 100, 101, 102, 103, 104, 105},
                .CounterDefaultValue = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            }
            Dim tJson = JsonConvert.SerializeObject(tCfg, Formatting.Indented)
            File.WriteAllText("config.json", tJson, New UTF8Encoding(False))
        End If
        Dim json = File.ReadAllText("config.json", New UTF8Encoding(False))
        Dim cfg = JsonConvert.DeserializeObject(Of Config)(json)

        'Parse settings
        Dim COUNT_UP_KEY = cfg.CountUpKey
        Dim COUNT_UP_KEY_2 = cfg.CountUpKey2
        Dim COUNT_DOWN_KEY = cfg.CountDownKey
        Dim COUNT_DOWN_KEY_2 = cfg.CountDownKey2
        Dim RESET_KEY = cfg.ResetKey
        Dim RESET_KEY_2 = cfg.ResetKey2

        Dim defaultValue(9) As Integer
        Dim consoleOutput As Boolean

        If args.Length > 0 Then
            Boolean.TryParse(args(0), consoleOutput)
        Else
            consoleOutput = cfg.ShowConsoleOutput
        End If

        Dim counter(9) As Integer
        For i = 0 To 9
            If args.Length > i + 1 Then
                Integer.TryParse(args(i + 1), defaultValue(i))
            Else
                defaultValue(i) = cfg.CounterDefaultValue(i)
            End If
            counter(i) = defaultValue(i)
            If consoleOutput AndAlso defaultValue(i) <> 0 Then
                SetCursorPosition(i)
                Console.Write($"{counter(i),4}")
            End If
        Next

        Dim cState(9) As Short
        'Check keypresses forever
        Do

            'Fixes bug of states not resetting
            For i = 0 To 9
                cState(i) = GetAsyncKeyState(cfg.CounterKeys(i))
            Next

            'Count up
            If GetAsyncKeyState(COUNT_UP_KEY) AndAlso (COUNT_UP_KEY_2 = 0 OrElse GetAsyncKeyState(COUNT_UP_KEY_2)) Then
                For i = 0 To 9
                    If cState(i) Then
                        counter(i) += 1
                        IO.File.WriteAllText($"counter{i}.txt", counter(i))
                        If consoleOutput Then
                            SetCursorPosition(i)
                            Console.Write($"{counter(i),4}")
                        End If

                        ' Wait to only trigger once per keypress
                        While GetAsyncKeyState(COUNT_UP_KEY) AndAlso GetAsyncKeyState(cfg.CounterKeys(i))
                            Threading.Thread.Sleep(15)
                        End While

                        Exit For
                    End If
                Next
            End If

            'Count down
            If GetAsyncKeyState(COUNT_DOWN_KEY) AndAlso (COUNT_DOWN_KEY_2 = 0 OrElse GetAsyncKeyState(COUNT_DOWN_KEY_2)) Then
                For i = 0 To 9
                    If cState(i) Then
                        counter(i) -= 1
                        IO.File.WriteAllText($"counter{i}.txt", counter(i))
                        If consoleOutput Then
                            SetCursorPosition(i)
                            Console.Write($"{counter(i),4}")
                        End If

                        ' Wait to only trigger once per keypress
                        While GetAsyncKeyState(COUNT_DOWN_KEY) AndAlso GetAsyncKeyState(cfg.CounterKeys(i))
                            Threading.Thread.Sleep(15)
                        End While

                        Exit For
                    End If
                Next
            End If

            'Reset
            If GetAsyncKeyState(RESET_KEY) AndAlso (RESET_KEY_2 = 0 OrElse GetAsyncKeyState(RESET_KEY_2)) Then
                For i = 0 To 9
                    If cState(i) Then
                        counter(i) = defaultValue(i)
                        IO.File.WriteAllText($"counter{i}.txt", counter(i))
                        If consoleOutput Then
                            SetCursorPosition(i)
                            Console.Write($"{counter(i),4}")
                        End If

                        ' Wait to only trigger once per keypress
                        While GetAsyncKeyState(RESET_KEY) AndAlso GetAsyncKeyState(cfg.CounterKeys(i))
                            Threading.Thread.Sleep(15)
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
    Public Function GetAsyncKeyState(ByVal vKey As Integer) As Short
    End Function

End Module

Public Structure Config
    Public ShowConsoleOutput As Boolean
    Public CountUpKey As Integer
    Public CountUpKey2 As Integer
    Public CountDownKey As Integer
    Public CountDownKey2 As Integer
    Public ResetKey As Integer
    Public ResetKey2 As Integer
    Public CounterKeys As Integer()
    Public CounterDefaultValue As Integer()
End Structure