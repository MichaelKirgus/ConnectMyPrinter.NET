<Serializable> Public Class PrinterQueueInfo
    Public Name As String
    Public ShareName As String
    Public Server As String
    Public DefaultPrinter As Boolean = False
    Public DriverName As String
    Public DriverVersion As String
    Public State As String
    Public Description As String
    Public Location As String

    Public Driver As PrinterDriverInfo
End Class
