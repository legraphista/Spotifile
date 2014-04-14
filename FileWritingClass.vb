Imports System.IO

Public Class FileWritingClass

    Dim myOwnThread As Threading.Thread

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
                    listOfErrors.Add(New Exception("Spotify is on a private session"))
                    GoTo ContinueLoop
                End If


                If stat.error IsNot Nothing Then
                    listOfErrors.Add(New Exception(String.Format("Spotify returned a error {0} (0x{1})", stat.[error].message, stat.[error].type)))
                    GoTo ContinueLoop
                End If


                If stat.[track].track_type.Trim = "ad" Or stat.[track].track_type.Trim <> "normal" Then

                    If Not textForFile.Equals(adText) Then
                        If Not WriteToFile(adText) Then GoTo ContinueLoop
                        textForFile = adText
                    End If

                Else

                    If stat.track Is Nothing Or Not stat.playing Then

                        If Not (textForFile.Equals(nosong)) Then
                            If Not WriteToFile(nosong) Then GoTo ContinueLoop
                            textForFile = nosong
                        End If

                    Else
                        Dim txt As New String(format)



                        txt = txt.Replace("{artist}", FixTheFormat(stat.track.artist_resource.name))
                        txt = txt.Replace("{song}", FixTheFormat(stat.track.track_resource.name))
                        txt = txt.Replace("{album}", FixTheFormat(stat.track.album_resource.name))

                        If Not txt.Equals(textForFile) Then
                            If Not WriteToFile(txt) Then GoTo ContinueLoop
                            textForFile = txt
                        End If

                    End If
                End If

ContinueLoop:

                Threading.Thread.Sleep(interval * 1000)

            Catch ex As Exception
                listOfErrors.Add(ex)
                Threading.Thread.Sleep(interval * 1000)
            End Try
        Loop
    End Sub

    Private Function WriteToFile(text As String) As Boolean
        Try

            Dim sw = New StreamWriter(filePath, False)
            sw.Write(text)
            sw.Close()
            Return True

        Catch ex As Exception

            listOfErrors.Add(ex)

        End Try

        Return False
    End Function

End Class
