Namespace My

    '    ' Für MyApplication sind folgende Ereignisse verfügbar:
    '    ' 
    '    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgelöst.
    '    ' Shutdown: Wird nach dem Schließen aller Anwendungsformulare ausgelöst. Dieses Ereignis wird nicht ausgelöst, wenn die Anwendung nicht normal beendet wird.
    '    ' UnhandledException: Wird ausgelöst, wenn in der Anwendung eine unbehandelte Ausnahme auftritt.
    '    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgelöst, wenn diese bereits aktiv ist. 
    '    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgelöst.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim quelle As String = ""
            Dim i As Integer = 0
            Dim format As Boolean = False
            Dim anzahl As Integer
            Dim configformate As String
            If Not (Dir(configfile) = "") Then

                Do
                    quelle = fLoad("quelle", i, "", configfile)
                    If quelle = "" Then Exit Do
                    quellverzeichnisse.Add(quelle)
                    i = i + 1
                Loop


                configformate = fLoad("format", "formate", "", configfile)
                anzahl = Len(configformate) - Len(Replace(configformate, ",", ""))
                ReDim dateiformate(anzahl + 1)
                dateiformate = configformate.Split(",")

                For Each dateiformat In dateiformate
                    dateiformatetable.Add(dateiformat.GetHashCode, dateiformat)
                Next

                For Each dateiformat In dateiformate

                    format = fLoad("format", dateiformat, 0, configfile)
                    dateiformatestatus.add(dateiformat.GetHashCode, format)

                    '    '    '        CheckedListBox1.Items.Add(dateiformat, format)
                Next
                ''                CheckedListBox1.Sorted = True
            End If
        End Sub
    End Class


End Namespace

