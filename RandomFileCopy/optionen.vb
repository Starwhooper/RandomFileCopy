Public Class optionen

    Private Sub optionen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = My.Application.Info.Title & " (" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & ")" & " Kopieroptionen"

        CheckBox1.Checked = fLoad("optionen", "cd0xformat", False, configfile)
        CheckBox2.Checked = fLoad("optionen", "vorziffern", False, configfile)
        ziel = fLoad("ziel", "ziel", "", configfile)
        TextBox2.Text = fLoad("optionen", "vlcpath", "", configfile)
        Label2.Text = "Ziel: " & ziel
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Shell("explorer /e," & ziel & "", vbNormalFocus)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If FolderBrowserDialog2.ShowDialog() = DialogResult.OK Then
            ziel = FolderBrowserDialog2.SelectedPath
            If Not ziel.Substring(ziel.Length - 1, 1) = "\" Then ziel = ziel & "\"
        End If
        Label2.Text = "Ziel: " & ziel
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim status As Boolean
        Dim configformate As String = ""

        If Not (Dir(configfile) = "") Then Kill(configfile)
        For i = 0 To quelle.ListBox2.Items.Count() - 1
            fSave("quelle", i, quelle.ListBox2.Items.Item(i), configfile)
        Next
        fSave("ziel", "ziel", ziel, configfile)

        fSave("optionen", "vlcpath", TextBox2.Text, configfile)

        For i = 0 To quelle.CheckedListBox1.Items.Count() - 1
            If quelle.CheckedListBox1.GetItemCheckState(i) = 1 Then
                status = True
            Else
                status = False
            End If
            fSave("format", quelle.CheckedListBox1.Items.Item(i), status, configfile)
            configformate = configformate & quelle.CheckedListBox1.Items.Item(i) & ","
        Next
        configformate = configformate.Substring(0, configformate.Length - 1)

        fSave("optionen", "cd0xformat", CheckBox1.Checked, configfile)
        fSave("optionen", "vorziffern", CheckBox2.Checked, configfile)
        fSave("format", "formate", configformate, configfile)

        Me.Hide()
        kopieren.Show()

    End Sub
End Class