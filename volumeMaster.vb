Imports CoreAudioApi

Public Class volumeMaster
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
            If bypassVolumeControl Then
                Try
                    SharedData.currentMasterVolume = getVolume()
                Catch ex As Exception
                End Try
                Threading.Thread.Sleep(500)
                Continue Do
            End If
            Try
                If stat.[track].track_type.Trim = "ad" Or stat.[track].track_type.Trim <> "normal" Then
                    setVolume(adVolume)
                Else
                    setVolume(musicVolume)
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
