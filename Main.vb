
Imports System.IO

Module Main

    Sub Main()
        Console.Title = "Spotifile"
        Console.WriteLine("Reading config ...")
        setup()
        Console.WriteLine("Connecting to Spotify ...")


        sp = New SpotifyAPI(SpotifyAPIExt.GetOAuth, "127.0.0.1")


        Dim rasp As Responses.CFID = sp.CFID

        If rasp.[error] IsNot Nothing Then
            Console.WriteLine(String.Format("Spotify returned a error {0} (0x{1})", rasp.[error].message, rasp.[error].type))
            Console.ReadKey()
        Else


            Dim fw As New FileWritingClass
            Dim dis As New displayClass
            Dim comm As New commandsClass
            Dim volume As New volumeMaster

            Do
                Try
                    stat = sp.Status
                    gotFirstWaveOfData = True
                    Threading.Thread.Sleep(500)
                Catch ex As Exception
                End Try
            Loop

        End If



    End Sub

    Public Sub setup()
        Try
            If Not File.Exists("config.txt") Then
                Console.WriteLine("Y NO config.txt?")
                Console.ReadKey()
                End
            End If


            Dim sr As New StreamReader("config.txt")

            Dim s As String = sr.ReadToEnd

            For Each line In s.Split(vbNewLine)
                Dim l As String = line.Trim
                If l = "" Then Continue For
                If l(0) = "#" Then
                    Continue For
                End If

                Dim data() As String = l.Split({":"c}, 2)

                If data(0).Trim.ToLower = "interval" Then
                    interval = data(1).Trim
                End If

                If data(0).Trim.ToLower = "file" Then
                    filePath = data(1).Trim
                End If

                If data(0).Trim.ToLower = "format" Then
                    format = data(1).Trim
                End If

                If data(0).Trim.ToLower = "nosong" Then
                    nosong = data(1).Trim
                End If
                If data(0).Trim.ToLower = "ads" Then
                    adText = data(1).Trim
                End If
                If data(0).Trim.ToLower = "advolume" Then
                    adVolume = Integer.Parse(data(1).Trim)
                End If
                If data(0).Trim.ToLower = "musicvolume" Then
                    musicVolume = Integer.Parse(data(1).Trim)
                End If
                If data(0).Trim.ToLower = "auto-volume" Then
                    bypassVolumeControl = If(data(1).Trim.ToLower.Contains("no"), True, False)
                End If




            Next


        Catch ex As Exception
            Console.WriteLine("Config error")
            Console.ReadKey()
            End
        End Try
    End Sub


    Public Sub kill()
        End
    End Sub
End Module
