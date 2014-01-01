Imports JariZ

Module SharedData

    Public gotFirstWaveOfData As Boolean = False

    Public sp As SpotifyAPI
    Public stat As JariZ.Responses.Status

    Public interval As Double = 5
    Public filePath As String = "D:\file.txt"
    Public format As String = "{artist} - {song}"
    Public nosong As String = "No Song !"
    Public adText As String = "Ads!"


    Public listOfErrors As New List(Of Exception)

    Public volumeLevel As Integer = 0.5
    Public textForFile As String


    Public Function Cap(value As Double, max As Double, min As Double) As Double
        If value < min Then value = min
        If value > max Then value = max
        Return value
    End Function


    Public Function getLineFromEx(ex As Exception) As String
        Dim st = New StackTrace(ex, True)
        If st.FrameCount = 0 Then Return ""

        Dim frame = st.GetFrame(st.FrameCount - 1)
        Dim fltxt As String = ""
        Try
            Dim fl() As String = frame.GetFileName.Split("\")
            fltxt = fl(fl.Length - 1)
            fl = fltxt.Split("/")
            fltxt = fl(fl.Length - 1)
        Catch ex2 As Exception
        End Try



        Return frame.GetFileColumnNumber & "," & frame.GetFileLineNumber & ":" & fltxt

    End Function
End Module
