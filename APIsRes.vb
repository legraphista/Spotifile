Imports System.Collections.Generic
Imports System.Linq
Imports System.Text


Public Class Responses
    Public Class ClientVersion
        Public Property [error]() As Internal.error
            Get
                Return m_error
            End Get
            Set(value As Internal.error)
                m_error = value
            End Set
        End Property
        Private m_error As Internal.error
        Public Property version() As Integer
            Get
                Return m_version
            End Get
            Set(value As Integer)
                m_version = value
            End Set
        End Property
        Private m_version As Integer
        Public Property client_version() As String
            Get
                Return m_client_version
            End Get
            Set(value As String)
                m_client_version = value
            End Set
        End Property
        Private m_client_version As String
        Public Property running() As Boolean
            Get
                Return m_running
            End Get
            Set(value As Boolean)
                m_running = value
            End Set
        End Property
        Private m_running As Boolean
    End Class

    Public Class CFID
        Public Property [error]() As Internal.error
            Get
                Return m_error
            End Get
            Set(value As Internal.error)
                m_error = value
            End Set
        End Property
        Private m_error As Internal.error
        Public Property token() As String
            Get
                Return m_token
            End Get
            Set(value As String)
                m_token = value
            End Set
        End Property
        Private m_token As String
    End Class

    Public Class Status
        Public Property [error]() As Internal.error
            Get
                Return m_error
            End Get
            Set(value As Internal.error)
                m_error = value
            End Set
        End Property
        Private m_error As Internal.error
        Public Property version() As Integer
            Get
                Return m_version
            End Get
            Set(value As Integer)
                m_version = value
            End Set
        End Property
        Private m_version As Integer
        Public Property client_version() As String
            Get
                Return m_client_version
            End Get
            Set(value As String)
                m_client_version = value
            End Set
        End Property
        Private m_client_version As String
        Public Property playing() As Boolean
            Get
                Return m_playing
            End Get
            Set(value As Boolean)
                m_playing = value
            End Set
        End Property
        Private m_playing As Boolean
        Public Property shuffle() As Boolean
            Get
                Return m_shuffle
            End Get
            Set(value As Boolean)
                m_shuffle = value
            End Set
        End Property
        Private m_shuffle As Boolean
        Public Property repeat() As Boolean
            Get
                Return m_repeat
            End Get
            Set(value As Boolean)
                m_repeat = value
            End Set
        End Property
        Private m_repeat As Boolean
        Public Property play_enabled() As Boolean
            Get
                Return m_play_enabled
            End Get
            Set(value As Boolean)
                m_play_enabled = value
            End Set
        End Property
        Private m_play_enabled As Boolean
        Public Property prev_enabled() As Boolean
            Get
                Return m_prev_enabled
            End Get
            Set(value As Boolean)
                m_prev_enabled = value
            End Set
        End Property
        Private m_prev_enabled As Boolean
        Public Property track() As Internal.track
            Get
                Return m_track
            End Get
            Set(value As Internal.track)
                m_track = value
            End Set
        End Property
        Private m_track As Internal.track
        Public Property playing_position() As Double
            Get
                Return m_playing_position
            End Get
            Set(value As Double)
                m_playing_position = value
            End Set
        End Property
        Private m_playing_position As Double
        Public Property server_time() As Integer
            Get
                Return m_server_time
            End Get
            Set(value As Integer)
                m_server_time = value
            End Set
        End Property
        Private m_server_time As Integer
        Public Property volume() As Double
            Get
                Return m_volume
            End Get
            Set(value As Double)
                m_volume = value
            End Set
        End Property
        Private m_volume As Double
        Public Property online() As Boolean
            Get
                Return m_online
            End Get
            Set(value As Boolean)
                m_online = value
            End Set
        End Property
        Private m_online As Boolean
        Public Property open_graph_state() As Internal.open_graph_state
            Get
                Return m_open_graph_state
            End Get
            Set(value As Internal.open_graph_state)
                m_open_graph_state = value
            End Set
        End Property
        Private m_open_graph_state As Internal.open_graph_state
        Public Property running() As Boolean
            Get
                Return m_running
            End Get
            Set(value As Boolean)
                m_running = value
            End Set
        End Property
        Private m_running As Boolean
    End Class

    Public Class Internal
#Region "Misc"
        Public Class [error]
            Public Property type() As String
                Get
                    Return m_type
                End Get
                Set(value As String)
                    m_type = value
                End Set
            End Property
            Private m_type As String
            Public Property message() As String
                Get
                    Return m_message
                End Get
                Set(value As String)
                    m_message = value
                End Set
            End Property
            Private m_message As String
        End Class
#End Region

#Region "Status"
        Public Class open_graph_state
            Public Property private_session() As Boolean
                Get
                    Return m_private_session
                End Get
                Set(value As Boolean)
                    m_private_session = value
                End Set
            End Property
            Private m_private_session As Boolean
            Public Property posting_disabled() As Boolean
                Get
                    Return m_posting_disabled
                End Get
                Set(value As Boolean)
                    m_posting_disabled = value
                End Set
            End Property
            Private m_posting_disabled As Boolean
        End Class

        Public Class track
            Public Property track_resource() As resource
                Get
                    Return m_track_resource
                End Get
                Set(value As resource)
                    m_track_resource = value
                End Set
            End Property
            Private m_track_resource As resource
            Public Property artist_resource() As resource
                Get
                    Return m_artist_resource
                End Get
                Set(value As resource)
                    m_artist_resource = value
                End Set
            End Property
            Private m_artist_resource As resource
            Public Property album_resource() As resource
                Get
                    Return m_album_resource
                End Get
                Set(value As resource)
                    m_album_resource = value
                End Set
            End Property
            Private m_album_resource As resource
            Public Property length() As Integer
                Get
                    Return m_length
                End Get
                Set(value As Integer)
                    m_length = value
                End Set
            End Property
            Private m_length As Integer
            Public Property track_type() As String
                Get
                    Return m_track_type
                End Get
                Set(value As String)
                    m_track_type = value
                End Set
            End Property
            Private m_track_type As String
        End Class

        Public Class resource
            Public Property name() As String
                Get
                    Return m_name
                End Get
                Set(value As String)
                    m_name = value
                End Set
            End Property
            Private m_name As String
            Public Property uri() As String
                Get
                    Return m_uri
                End Get
                Set(value As String)
                    m_uri = value
                End Set
            End Property
            Private m_uri As String
            Public Property location() As location
                Get
                    Return m_location
                End Get
                Set(value As location)
                    m_location = value
                End Set
            End Property
            Private m_location As location
        End Class

        Public Class location
            Public Property og() As String
                Get
                    Return m_og
                End Get
                Set(value As String)
                    m_og = value
                End Set
            End Property
            Private m_og As String
        End Class
#End Region
    End Class
End Class
