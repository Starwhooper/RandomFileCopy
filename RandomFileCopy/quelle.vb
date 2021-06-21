Public Class quelle

    Private Sub quelle_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = fensterkonfiguration("Quelle festlegen")

        Dim quelle As String = ""
        Dim i As Integer = 0
        Dim format As Boolean = False
        Dim quellverzeichnis As String

        For Each dateiformat In dateiformate
            format = dateiformatestatus(dateiformat.GetHashCode)
            CheckedListBox1.Items.Add(dateiformat, format)
        Next
        CheckedListBox1.Sorted = True
        'End If
        For Each quellverzeichnis In quellverzeichnisse
            ListBox2.Items.Add(quellverzeichnis)
        Next
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then ListBox2.Items.Add(FolderBrowserDialog1.SelectedPath)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ListBox2.Items.Remove(ListBox2.SelectedItem)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ListBox2.Items.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListBox2.Items.Count > 0 Then
            Me.Hide()
            suchen.Show()
        Else
            MsgBox("Keine Verzeichnisse gewählt")
        End If
    End Sub


End Class