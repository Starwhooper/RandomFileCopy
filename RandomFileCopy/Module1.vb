Module Module1
    Private Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Int32, ByVal lpFileName As String) As Int32
    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Int32

    Public configfile As String = Environment.CurrentDirectory & "\config.ini"
    Public ziel As String = ""
    Public gefundenedateien() As String
    Public programmname As String = My.Application.Info.Title
    Public programmversion As String = "2.2.1.0" 'My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
    Public programmcopyright As String = My.Application.Info.Copyright
    Public quellverzeichnisse As New ArrayList
    Public dateiformate() As String
    Public dateiformatestatus As New Hashtable
    Public dateiformatetable As New Hashtable

    Function fensterkonfiguration(ByVal name As String) As String
        Dim titel As String
        titel = programmname & " (" & programmversion & ") - " & name
        Return (titel)
    End Function

    Function fLoad(ByVal strSection As String, ByVal strKey As String, ByVal strDefault As String, ByVal strFile As String) As String
        Dim strTemp As String = Space(1024), lLength As Integer
        lLength = GetPrivateProfileString(strSection, strKey, strDefault, strTemp, strTemp.Length, strFile)
        Return (strTemp.Substring(0, lLength))
    End Function
    Function fSave(ByVal strSection As String, ByVal strKey As String, ByVal strValue As String, ByVal strFile As String) As Boolean
        Return (Not (WritePrivateProfileString(strSection, strKey, strValue, strFile) = 0))
    End Function
    Function GetDiskSpaceFree(ByVal Drive As String) As Double

        Dim FSO As Object
        Dim Drv As Object
        Dim SpaceFree As Double

        SpaceFree = -1
        Drive = ziel.ToUpper.Substring(0, 1)

        FSO = CreateObject("Scripting.FileSystemObject")
        For Each Drv In FSO.Drives
            If Drv.DriveLetter = Drive Then
                SpaceFree = Drv.AvailableSpace
                Exit For
            End If
        Next

        GetDiskSpaceFree = SpaceFree
    End Function

    ' Erzeugen eines zufälligen Mix der Arrayelemente
    ' <param name="theArray">das zu verarbeitende Array (Source-Feld)</param>
    Function getRndArrayMix(ByVal theArray As Array) As Array
        ' Feld mit zufällig verteilten Indizes erstellen
        Dim rndNum() As Integer = GetRndIndizes(theArray.Length)
        Dim temp(rndNum.Length - 1) As Object

        ' Temporäres Feld mit den zufällig verteilten Werten  des Source-Felds erstellen
        For i As Integer = 0 To theArray.Length - 1
            temp.SetValue(theArray(i), rndNum(i))
        Next

        Return temp ' Rückgabe des gemischten Felds
    End Function

    ' Erzeugen eines Index-Array mit Zufallszahlen ohne Duplikate
    ' <param name="theArrayLen">Länge des zu mischenden Arrays</param>
    Function GetRndIndizes(ByVal theArrayLen As Integer) As Integer()
        Dim rndIndizes(theArrayLen - 1) As Integer
        Dim i, idRnd As Integer

        ' Initialisieren des Feldes der zufällig verteilten Indizes
        For i = 0 To theArrayLen - 1
            rndIndizes(i) = -1
        Next

        Randomize()
        ' Einspeichern von Indizes in zufälliger Reihenfolge
        For i = 0 To theArrayLen - 1
            Do
                ' Zufallszahl im Bereich (0...größter Index des Source-Feldes) erzeugen
                idRnd = (theArrayLen - 1) * Rnd()
                ' Ist Zufallszahl schon im Feld der Indizes?
                If Array.IndexOf(rndIndizes, idRnd) < 0 Then
                    rndIndizes(i) = idRnd
                    Exit Do
                End If
            Loop
        Next
        Return rndIndizes
    End Function
End Module

