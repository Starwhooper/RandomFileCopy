Public Class suchen

    Private Sub suchen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = My.Application.Info.Title & " (" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & ")" & " Dateien gefunden"
        Button1.Enabled = Button2.Enabled = False
    End Sub

    Private Sub Rekursiv(ByVal oDir As IO.DirectoryInfo)
        Dim oSubDir As IO.DirectoryInfo
        Dim oFile As IO.FileInfo
        Dim dateityp As String
        Dim dateiname As String
        Dim i As Integer
        Cursor.Current = Cursors.WaitCursor

        ' zunächst alle Dateien des Ordners aufspüren
        For Each oFile In oDir.GetFiles()
            dateityp = oFile.Name.Substring(oFile.Name.Length - 3, 3)
            For Each itemChecked In quelle.CheckedListBox1.CheckedItems
                If itemChecked = dateityp Then
                    dateiname = oFile.DirectoryName & "\" & oFile.Name
                    If IsNothing(gefundenedateien) Then
                        i = 0
                    Else
                        i = gefundenedateien.Length
                    End If
                    ReDim Preserve gefundenedateien(i)
                    i = gefundenedateien.Length - 1
                    gefundenedateien(i) = dateiname
                    Exit For
                End If
            Next
        Next
        ' Jetzt alle Unterverzeichnis durchlaufen und die Prozedur rekursiv selbst aufrufen
        For Each oSubDir In oDir.GetDirectories()
            Rekursiv(oSubDir)
        Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        Dim verzeichnisse(quelle.ListBox2.Items.Count() - 1) As IO.DirectoryInfo
        Dim verzeichnis As IO.DirectoryInfo
        Dim i As Integer

        Cursor.Current = Cursors.WaitCursor


        For i = 0 To quelle.ListBox2.Items.Count() - 1
            If Not (System.IO.Directory.Exists(quelle.ListBox2.Items.Item(i))) Then
                MsgBox(quelle.ListBox2.Items.Item(i) & " ungültig")
                Cursor.Current = Cursors.Arrow
                Exit Sub
            End If
        Next

        For i = 0 To quelle.ListBox2.Items.Count() - 1
            verzeichnisse(i) = New IO.DirectoryInfo(quelle.ListBox2.Items.Item(i))
        Next

        For Each verzeichnis In verzeichnisse
            Rekursiv(verzeichnis)
        Next

        For Each gefundenedatei In gefundenedateien
            'For i = 0 To gefundenedateien.Count - 1
            ListBox1.Items.Add(gefundenedatei)
        Next

        Label1.Text = "Anzahl: " & gefundenedateien.Count
        Button1.Enabled = Button2.Enabled = True

        Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim ergebnisRandomField() As Object = getRndArrayMix(gefundenedateien)
        Dim i As Integer
        ListBox1.DataSource = ergebnisRandomField
        For i = 0 To ergebnisRandomField.Length - 2
            gefundenedateien(i + 1) = ergebnisRandomField(i)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Hide()
        optionen.Show()
    End Sub
End Class