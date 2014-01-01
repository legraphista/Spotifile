Public Class displayClass
    Dim myOwnThread As Threading.Thread
    Dim w As Integer = Console.BufferWidth

    Sub New()
        myOwnThread = New Threading.Thread(AddressOf run)
        myOwnThread.Start()
    End Sub


    Sub drawLine(chars As String, Optional cl As ConsoleColor = ConsoleColor.White, Optional bkcol As ConsoleColor = ConsoleColor.Black)

        Dim txt As String = ""
 

        For i As Integer = 0 To w - 1
            txt &= chars(i Mod chars.Length)
        Next

        Console.BackgroundColor = bkcol
        Console.ForegroundColor = cl
        Console.WriteLine(txt)
        resetcols()
    End Sub

    Sub resetcols()
        Console.BackgroundColor = ConsoleColor.Black
        Console.ForegroundColor = ConsoleColor.White
    End Sub
    Sub newline()
        Console.WriteLine()
    End Sub


    Sub run()
        While Not gotFirstWaveOfData
            Threading.Thread.Sleep(500)
        End While

        Do
            Console.Clear()
            resetcols()
            Console.WriteLine("Date and time : " & Date.Now)
            drawLine("~", ConsoleColor.Yellow)
            resetcols()

            'Dim stat As JariZ.Responses.Status = sp.Status
            Try
                If stat IsNot Nothing AndAlso Not (stat.open_graph_state.private_session Or stat.error IsNot Nothing) Then
                    Console.Write("Song type : ")

                    If stat.track Is Nothing Or Not stat.playing Then
                        Console.WriteLine("Nothing is playing")
                    ElseIf stat.[track].track_type.Trim = "ad" Or stat.[track].track_type.Trim <> "normal" Then
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.WriteLine("Ad")
                    Else
                        Console.ForegroundColor = ConsoleColor.Green
                        Console.WriteLine("Music")
                    End If
                    resetcols()

                    newline()
                    Console.WriteLine("File contents : " & textForFile)
                    newline()

                    resetcols()
                    Console.Write("Volume : ")

                    If stat.track Is Nothing Or Not stat.playing Then
                    ElseIf stat.[track].track_type.Trim = "ad" Or stat.[track].track_type.Trim <> "normal" Then
                        Console.ForegroundColor = ConsoleColor.DarkRed
                    Else
                        Console.ForegroundColor = ConsoleColor.Green
                    End If
                    Console.WriteLine(Math.Round(stat.volume * 100, 2))

                End If
            Catch ex As Exception
                listOfErrors.Add(New Exception("Unknown error! Did Spotify close/crash?"))
            End Try
            

            newline()

            If listOfErrors.Count > 0 Then
                Try
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("Error(s):")
                    For Each ex As Exception In listOfErrors
                        Console.WriteLine(getLineFromEx(ex) & " - " & ex.Message)
                    Next
                Catch ex As Exception

                End Try

                listOfErrors.Clear()
            End If


            resetcols()
            newline()
            newline()
            Console.WriteLine("q - Close the app")
            Console.WriteLine("r - Reload config")



            Threading.Thread.Sleep(interval * 1000)


        Loop
    End Sub
End Class
