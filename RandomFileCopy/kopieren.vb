Public Class kopieren
    'Public Function GetDiskSpaceFree(ByVal Drive As String) As Double

    '    Dim FSO As Object
    '    Dim Drv As Object
    '    Dim SpaceFree As Double

    '    SpaceFree = -1
    '    Drive = optionen.ziel.ToUpper.Substring(0, 1)

    '    FSO = CreateObject("Scripting.FileSystemObject")
    '    For Each Drv In FSO.Drives
    '        If Drv.DriveLetter = Drive Then
    '            SpaceFree = Drv.AvailableSpace
    '            Exit For
    '        End If
    '    Next

    '    GetDiskSpaceFree = SpaceFree
    'End Function
    'Public Function setvonnach(ByVal von As String, ByVal nach As String)
    '    Label5.Text = von
    '    Label4.Text = nach
    '    Label5.Update()
    '    Label4.Update()
    '    Return True
    'End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Dim i As Integer
        Dim k As Integer
        Dim strOldFile As String
        Dim strNewFile As String
        Dim subziel As String = ""
        Dim dateipraefix As String = ""
        '        Dim status As Boolean
        Dim dateigroesse As Integer
        Dim SpaceFree As Double
        Dim anzahlkopierte As Integer
        Dim configformate As String = ""
        Dim commando As String

        '        Timer1.Stop()
        Label3.Enabled = True
        Label6.Enabled = True


        If optionen.CheckBox1.Checked = True Then
            For i = 1 To 6
                If Dir(ziel & "\CD0" & i, vbDirectory) = "" Then MkDir(ziel & "\CD0" & i)
            Next
        End If
        i = 1
        k = 0
        anzahlkopierte = 0
        For j = 1 To gefundenedateien.Count - 1
            If optionen.CheckBox1.Checked = True Then
                If anzahlkopierte > 6 * 99 Then
                    MsgBox("Maximum von 6 * 99 Dateien erreicht")
                    Exit For
                End If
            End If
            If optionen.CheckBox2.Checked = True Then
                dateipraefix = Format(j, "0000") & "_"
            End If

            strOldFile = gefundenedateien(j)
            dateigroesse = My.Computer.FileSystem.GetFileInfo(strOldFile).Length

            SpaceFree = GetDiskSpaceFree(ziel)
            If SpaceFree < dateigroesse Then
                If optionen.CheckBox4.Checked Then
                    Continue For
                End If
                MsgBox("Kein Ausreichender freier Speicherplatz auf " & ziel & " vorhanden. Es sind nur noch " & SpaceFree / 1000000 & " MB verfürbar, die nächste Datei benötigt aber " & dateigroesse / 1000000 & " MB. Es wurden insgesamt " & j - 1 & " Dateien übertragen.")
                Exit Sub
            End If

            If optionen.CheckBox3.Checked = True Then
                If SpaceFree < dateigroesse + (optionen.TextBox1.Text * 1000000) Then
                    If optionen.CheckBox4.Checked Then
                        Continue For
                    End If
                    MsgBox("Kein Ausreichender freier Speicherplatz auf " & ziel & " vorhanden. Die nächste Datei würde das Limit von " & optionen.TextBox1.Text & " MB freien Speichplatz überschreiten. Es wurden insgesamt " & j - 1 & " Dateien übertragen.")
                    Exit Sub
                End If
            End If

            If optionen.CheckBox1.Checked = True Then
                subziel = "\CD0" & i & "\"
            End If

            strNewFile = ziel & subziel & dateipraefix & IO.Path.GetFileName(strOldFile)

            strNewFile = Replace(strNewFile, "\\", "\")
            'suchen.ListBox1.SetSelected(j - 1, True)

            If k > 100 Then k = 0
            ProgressBar1.Value = k
            k = k + 1

            If strOldFile.Substring(strOldFile.Length - 3) = "ogg" Then
                If optionen.CheckBox5.Checked Then
                    strNewFile = strNewFile & ".mp3"
                    Label5.Text = strOldFile
                    Label4.Text = strNewFile
                    Label5.Update()
                    Label4.Update()
                    commando = """" & optionen.TextBox2.Text & """ """ & strOldFile & """ --sout=#transcode{acodec=mp3,ab=" & optionen.ComboBox1.SelectedItem & ",channels=2,samplerate=44100}:standard{access=file,mux=raw,dst=" & strNewFile & "} vlc://quit"
                    'MsgBox(commando)
                    Shell(commando, AppWinStyle.Hide, True)
                Else
                    Label5.Text = strOldFile
                    Label4.Text = strNewFile
                    Label5.Update()
                    Label4.Update()
                    FileCopy(strOldFile, strNewFile)
                End If
            Else
                Label5.Text = strOldFile
                Label4.Text = strNewFile
                Label5.Update()
                Label4.Update()
                FileCopy(strOldFile, strNewFile)
            End If
            anzahlkopierte = anzahlkopierte + 1
            i = i + 1
            If i > 6 Then i = 1

        Next
        ProgressBar1.Value = 100

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

End Class