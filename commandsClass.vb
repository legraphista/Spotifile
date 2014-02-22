Public Class commandsClass
    Dim myOwnThread As Threading.Thread

    Sub New()
        myOwnThread = New Threading.Thread(AddressOf run)
        myOwnThread.Start()
    End Sub
    Sub run()
        Do
            Dim c As ConsoleKeyInfo = Console.ReadKey

            If c.KeyChar = "q"c Or c.KeyChar = "Q"c Then
                kill()
            End If
            If c.KeyChar = "r"c Or c.KeyChar = "R"c Then
                setup()
            End If
            If c.KeyChar = "v"c Or c.KeyChar = "V"c Then
                bypassVolumeControl = Not bypassVolumeControl
            End If
        Loop
    End Sub
End Class
