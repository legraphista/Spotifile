Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net
Imports Newtonsoft.Json

Public Class SpotifyAPI
    Private _oauth As String
    Private _host As String
    Private _cfid As String
    ''' <summary>
    ''' Initializes a new SpotifyAPI object which can be used to recieve
    ''' </summary>
    ''' <param name="OAuth">Use <seealso cref="SpotifyAPI.GetOAuth()"/> to get this, Or specify your own</param>
    ''' <param name="Host">Most of the time 127.0.0.1, or for lulz use something like my-awesome-program.spotilocal.com</param>
    Public Sub New(OAuth As String, Host As String)
        _oauth = OAuth
        _host = Host

        'emulate the embed code [NEEDED]
        wc.Headers.Add("Origin", "https://embed.spotify.com")
        wc.Headers.Add("Referer", "https://embed.spotify.com/openplay/?uri=spotify:track:6uQ192yNyZ4W8yoaL0Sb9p")
        wc.Headers.Add("User-Agent: SpotifyAPI")
    End Sub
    ''' <summary>
    ''' Fixes special characters, excoding to UTF-8
    ''' </summary>
    ''' <param name="txt">Text in ANSI encoding from Spotify</param>
    ''' <returns>Text in UTF-8</returns>
    ''' <remarks></remarks>
    Public Shared Function FixTheFormat(txt As String) As String
        Dim utf8Encoding As New System.Text.UTF8Encoding(True)
        Dim encodedString() As Byte

        encodedString = System.Text.Encoding.Default.GetBytes(txt)

        Return utf8Encoding.GetString(encodedString)
    End Function

    ''' <summary>
    ''' Get a link to the 640x640 cover art image of a spotify album
    ''' </summary>
    ''' <param name="uri">The Spotify album URI</param>
    ''' <returns></returns>
    Public Function getArt(uri As String) As String
        Try
            Dim raw As String = New WebClient().DownloadString("http://open.spotify.com/album/" & uri.Split(New String() {":"}, StringSplitOptions.None)(2))
            raw = raw.Replace(vbTab, "")


            Dim lines As String() = raw.Split(New String() {vbLf}, StringSplitOptions.None)
            For Each line As String In lines
                If line.StartsWith("<meta property=""og:image""") Then
                    Dim l As String() = line.Split(New String() {"/"}, StringSplitOptions.None)
                    Return "http://o.scdn.co/640/" & l(4).Replace("""", "").Replace(">", "")
                End If
            Next
        Catch
            Return ""
        End Try
        Return ""
    End Function


    ''' <summary>
    ''' Gets the current Unix Timestamp
    ''' Mostly for internal use
    ''' </summary>
    Public ReadOnly Property TimeStamp() As Integer
        Get
            Return Convert.ToInt32((DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds)
        End Get
    End Property

    Private wc As New WebClient()

    ''' <summary>
    ''' Gets the 'CFID', a unique identifier for the current session.
    ''' Note: It's required to get the CFID before making any other calls
    ''' </summary>
    Public ReadOnly Property CFID() As Responses.CFID
        Get
            Dim a As String = recv("simplecsrf/token.json")
            Dim d As List(Of Responses.CFID) = DirectCast(JsonConvert.DeserializeObject(a, GetType(List(Of Responses.CFID))), List(Of Responses.CFID))
            _cfid = d(0).token
            Return d(0)
        End Get
    End Property

    Private _uri As String = ""
    ''' <summary>
    ''' Used by SpotifyAPI.Play to play Spotify URI's
    ''' Change this URI and then call SpotifyAPI.Play
    ''' </summary>
    Public Property URI() As String
        Get
            Return _uri
        End Get
        Set(value As String)
            _uri = value
        End Set
    End Property

    ''' <summary>
    ''' Plays a certain URI and returns the status afterwards
    ''' Change SpotifyAPI.URI into the needed uri!
    ''' </summary>
    Public ReadOnly Property Play() As Responses.Status
        Get
            Dim a As String = recv("remote/play.json?uri=" & URI, True, True, -1)
            Dim d As List(Of Responses.Status) = DirectCast(JsonConvert.DeserializeObject(a, GetType(List(Of Responses.Status))), List(Of Responses.Status))
            Return d(0)
        End Get
    End Property

    ''' <summary>
    ''' Resume Spotify playback and return the status afterwards 
    ''' </summary>
    Public ReadOnly Property [Resume]() As Responses.Status
        Get
            Dim a As String = recv("remote/pause.json?pause=false", True, True, -1)
            Dim d As List(Of Responses.Status) = DirectCast(JsonConvert.DeserializeObject(a, GetType(List(Of Responses.Status))), List(Of Responses.Status))
            Return d(0)
        End Get
    End Property

    ''' <summary>
    ''' Pause Spotify playback and return the status afterwards
    ''' </summary>
    Public ReadOnly Property Pause() As Responses.Status
        Get
            Dim a As String = recv("remote/pause.json?pause=true", True, True, -1)
            Dim d As List(Of Responses.Status) = DirectCast(JsonConvert.DeserializeObject(a, GetType(List(Of Responses.Status))), List(Of Responses.Status))
            Return d(0)
        End Get
    End Property

    ''' <summary>
    ''' Returns the current track info.
    ''' Change <seealso cref="Wait"/> into the amount of waiting time before it will return
    ''' When the current track info changes it will return before elapsing the amount of seconds in <seealso cref="Wait"/>
    ''' (look at the project site for more information if you do not understand this)
    ''' </summary>
    Public ReadOnly Property Status() As Responses.Status
        Get
            Dim a As String = recv("remote/status.json", True, True, _wait)
            Dim d As List(Of Responses.Status) = DirectCast(JsonConvert.DeserializeObject(a, GetType(List(Of Responses.Status))), List(Of Responses.Status))
            Return d(0)
        End Get
    End Property

    Private _wait As Integer = -1
    ''' <summary>
    ''' Please see <seealso cref="Status"/> for more information
    ''' </summary>
    Public Property Wait() As Integer
        Get
            Return _wait
        End Get
        Set(value As Integer)
            _wait = value
        End Set
    End Property

    ''' <summary>
    ''' Recieves a OAuth key from the Spotify site
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetOAuth() As String
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



    Private Function recv(request As String) As String
        Return recv(request, False, False, -1)
    End Function

    Private Function recv(request As String, oauth As Boolean, cfid As Boolean) As String
        Return recv(request, oauth, cfid, -1)
    End Function

    Private Function recv(request As String, oauth As Boolean, cfid As Boolean, wait As Integer) As String
        Dim parameters As String = "?&ref=&cors=&_=" & TimeStamp
        If request.Contains("?") Then
            parameters = parameters.Substring(1)
        End If

        If oauth Then
            parameters += "&oauth=" & _oauth
        End If
        If cfid Then
            parameters += "&csrf=" & _cfid
        End If

        If wait <> -1 Then
            parameters += "&returnafter=" & wait
            parameters += "&returnon=login%2Clogout%2Cplay%2Cpause%2Cerror%2Cap"
        End If

        Dim a As String = "http://" & _host & ":4380/" & request & parameters
        Dim derp As String = ""
        Try
            derp = wc.DownloadString(a)
            derp = "[ " & derp & " ]"
        Catch z As Exception
            'perhaps spotifywebhelper isn't started (happens sometimes)
            If Process.GetProcessesByName("SpotifyWebHelper").Length < 1 Then
                Try
                    System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Spotify\Data\SpotifyWebHelper.exe")
                Catch dd As Exception
                    Throw New Exception("Could not launch SpotifyWebHelper. Your installation of Spotify might be corrupt or you might not have Spotify installed", dd)
                End Try

                Return recv(request, oauth, cfid)
            Else
                'spotifywebhelper is running but we still can't connect, wtf?!
                Throw New Exception("Unable to connect to SpotifyWebHelper", z)
            End If
        End Try
        Return derp
    End Function

    ''' <summary>
    ''' Recieves client version information.
    ''' Doesn't require a OAuth/CFID
    ''' </summary>
    Public ReadOnly Property ClientVersion() As Responses.ClientVersion
        Get
            Dim a As String = recv("service/version.json?service=remote")
            Dim d As List(Of Responses.ClientVersion) = DirectCast(JsonConvert.DeserializeObject(a, GetType(List(Of Responses.ClientVersion))), List(Of Responses.ClientVersion))
            Return d(0)
        End Get
    End Property
End Class
