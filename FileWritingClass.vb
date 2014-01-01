Imports System.IO

Public Class FileWritingClass

    Dim myOwnThread As Threading.Thread
    Dim sw As StreamWriter

    Sub New()
        myOwnThread = New Threading.Thread(AddressOf run)
        myOwnThread.Start()
    End Sub

    Sub run()
        While Not gotFirstWaveOfData
            Threading.Thread.Sleep(500)
        End While

        Do

            Try


                If stat.open_graph_state.private_session Then
                    ' Console.WriteLine("Spotify is on a private session")
                    listOfErrors.Add(New Exception("Spotify is on a private session"))
                    Threading.Thread.Sleep(interval * 1000)
                    Continue Do
                End If


                If stat.error IsNot Nothing Then
                    ' Console.WriteLine(String.Format("Spotify returned a error {0} (0x{1})", stat.[error].message, stat.[error].type))
                    listOfErrors.Add(New Exception(String.Format("Spotify returned a error {0} (0x{1})", stat.[error].message, stat.[error].type)))
                    Threading.Thread.Sleep(interval * 1000)
                    Continue Do
                End If

                sw = New StreamWriter(filePath, False)

                If stat.[track].track_type.Trim = "ad" Or stat.[track].track_type.Trim <> "normal" Then
                    sw.WriteLine(adText)
                    'Console.WriteLine(Date.Now & " : " & adText)
                    textForFile = adText
                Else

                    If stat.track Is Nothing Or Not stat.playing Then
                        sw.WriteLine(nosong)
                        'Console.WriteLine(Date.Now & " : " & nosong)
                        textForFile = nosong
                    Else
                        Dim txt As New String(format)



                        txt = txt.Replace("{artist}", stat.track.artist_resource.name)
                        txt = txt.Replace("{song}", stat.track.track_resource.name)
                        txt = txt.Replace("{album}", stat.track.album_resource.name)


                        sw.WriteLine(txt)

                        ' Console.WriteLine(Date.Now & " : " & txt)
                        textForFile = txt
                    End If
                End If
                sw.Close()

                Threading.Thread.Sleep(interval * 1000)

            Catch ex As Exception
                listOfErrors.Add(ex)
                ' Console.WriteLine("Error: " & ex.Message)
                If sw IsNot Nothing Then
                    Try
                        sw.Close()
                    Catch ex2 As Exception
                    End Try
                End If
                Threading.Thread.Sleep(interval * 1000)
            End Try
        Loop
    End Sub

End Class
