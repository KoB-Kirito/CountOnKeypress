Imports System.Runtime.InteropServices

Module Program
    Sub Main(args As String())
        Dim counter1 As Integer
        Dim counter2 As Integer

        Console.CursorVisible = False

        'Check keypresses forever
        Do
            If GetAsyncKeyState(ConsoleKey.C) AndAlso GetAsyncKeyState(ConsoleKey.NumPad1) Then
                counter1 += 1
                IO.File.WriteAllText("counter1.txt", counter1)

                Console.CursorLeft = 0
                Console.CursorTop = 0
                Console.Write(counter1)

                ' Wait to only trigger once
                While GetAsyncKeyState(ConsoleKey.C) AndAlso GetAsyncKeyState(ConsoleKey.NumPad1)
                End While
            End If

            If GetAsyncKeyState(ConsoleKey.C) AndAlso GetAsyncKeyState(ConsoleKey.NumPad2) Then
                counter2 += 1
                IO.File.WriteAllText("counter2.txt", counter2)

                Console.CursorLeft = 0
                Console.CursorTop = 1
                Console.Write(counter2)

                ' Wait to only trigger once
                While GetAsyncKeyState(ConsoleKey.C) AndAlso GetAsyncKeyState(ConsoleKey.NumPad2)
                End While
            End If
        Loop

    End Sub

    <DllImport("user32.dll")>
    Public Function GetAsyncKeyState(ByVal vKey As ConsoleKey) As Short
    End Function

End Module