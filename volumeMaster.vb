Imports CoreAudioApi

Public Class volumeMaster
    Dim myOwnThread As Threading.Thread
    Dim currentVolumeUpdateThread As Threading.Thread

    Sub New()
        SharedData.currentMasterVolume = getVolume()
        myOwnThread = New Threading.Thread(AddressOf run)
        myOwnThread.Start()

        currentVolumeUpdateThread = New Threading.Thread(Sub()
                                                             Do
                                                                 Try
                                                                     Threading.Thread.Sleep(1000 * 30)
                                                                     SharedData.currentMasterVolume = getVolume()
                                                                 Catch ex As Exception

                                                                 End Try
                                                             Loop
                                                         End Sub)
        currentVolumeUpdateThread.Start()
    End Sub



    Sub run()
        While Not gotFirstWaveOfData
            Threading.Thread.Sleep(500)
        End While

        Do
            If bypassVolumeControl Then
                Continue Do
            End If
            Try
                If stat.[track].track_type.Trim = "ad" Or stat.[track].track_type.Trim <> "normal" Then
                    If (currentMasterVolume <> adVolume) Then
                        setVolume(adVolume)
                        SharedData.currentMasterVolume = adVolume
                    End If
                   
                    'spotify will pause so we resume
                    Dim playing As Boolean = stat.playing
                    Do While Not playing
                        stat = sp.Resume
                        playing = stat.playing
                    Loop

                Else
                    If SharedData.currentMasterVolume <> musicVolume Then
                        setVolume(musicVolume)
                        SharedData.currentMasterVolume = musicVolume
                    End If
                   
                End If
                SharedData.currentMasterVolume = getVolume()

                Threading.Thread.Sleep(500)

            Catch ex As Exception
                listOfErrors.Add(ex)

                Threading.Thread.Sleep(500)
            End Try
        Loop
    End Sub




    Public Function setVolume(volume As Integer)
        volume = Cap(volume, 100, 0)

        Try
            Dim masterVolume As SimpleAudioVolume = getSpotifySimpleAudioVolume()
            masterVolume.MasterVolume = volume / 100.0F
            Return True
        Catch ex As Exception
        End Try

        Return False
    End Function

    Public Function getVolume() As Integer
        Try
            Dim masterVolume As SimpleAudioVolume = getSpotifySimpleAudioVolume()
            Return Cap(masterVolume.MasterVolume * 100, 100, 0)
        Catch ex As Exception
        End Try

        Return 0
    End Function

    Public Function getSpotifySimpleAudioVolume() As SimpleAudioVolume

        Dim DevEnum As New MMDeviceEnumerator()
        Dim device As MMDevice = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)

        Dim osInfo As System.OperatingSystem = System.Environment.OSVersion

        Dim found As Boolean = False
        For i As Integer = 0 To device.AudioSessionManager.Sessions.Count - 1
            Dim session As AudioSessionControl = device.AudioSessionManager.Sessions(i)

            If Process.GetProcessById(Convert.ToInt32(session.ProcessID)).ProcessName.ToLower() = "spotify" Then
                found = True

                Return session.SimpleAudioVolume
            End If
        Next
        If Not found Then

            SharedData.listOfErrors.Add(New Exception("Volume controller: Unable to find Spotify! Are you sure you're running win7 + and spotify is enabled?"))

            Return Nothing
        End If
        Return Nothing
    End Function
End Class
