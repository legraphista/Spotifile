Imports System.Net
Public Module SpotifyAPIExt

    

    Public Function GetOAuth() As String
        Dim client As New WebClient
        client.Headers.Add("User-Agent: SpotifyAPI")


        Dim raw As String = client.DownloadString("https://embed.spotify.com/openplay/?uri=spotify:track:6uQ192yNyZ4W8yoaL0Sb9p")
        raw = raw.Replace(" ", "")
        Dim lines As String() = raw.Split(New String() {vbLf}, StringSplitOptions.None)
        For Each line As String In lines
            If line.StartsWith("tokenData") Then
                Dim l As String() = line.Split(New String() {"'"}, StringSplitOptions.None)
                Return l(1)
            End If
        Next

        Throw New Exception("Could not find OAuth token")
    End Function


End Module
