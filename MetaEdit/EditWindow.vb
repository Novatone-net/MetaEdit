Imports System.ComponentModel
Imports System.Xml
Imports System.IO

Public Class EditWindow
    Private Sub EditWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' Prevent the form from closing
        e.Cancel = True
        ' Hide the form instead
        Me.Hide()
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        ' TODO: This is tedious and error prone to maintain re-factor to simplify and use a field list rather than
        '       hard-coded fields

        Dim Filename As String = File.Text

        ' Load the XML document
        Dim xmlDoc As New XmlDocument()
        Try
            xmlDoc.Load(Filename)

        Catch ex As Exception
            Dim root As XmlElement = xmlDoc.CreateElement("metadata")
            xmlDoc.AppendChild(root)
            Console.WriteLine(xmlDoc.OuterXml)
        End Try

        Dim metadatanode As XmlNode
        metadatanode = xmlDoc.SelectSingleNode("/metadata/identifier")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Identifier.Text
        Else
            If Me.Identifier.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("identifier")
                NewChild.InnerText = Me.Identifier.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/dbkey")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.DBKey.Text
        Else
            If Me.DBKey.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("dbkey")
                NewChild.InnerText = Me.DBKey.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/public")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Publish.Text
        Else
            'add
            metadatanode = xmlDoc.SelectSingleNode("/metadata")
            Dim NewChild As XmlElement = xmlDoc.CreateElement("public")
            NewChild.InnerText = Me.Publish.Checked
            metadatanode.AppendChild(NewChild)
        End If


        metadatanode = xmlDoc.SelectSingleNode("/metadata/title")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Title.Text
        Else
            If Me.Title.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("title")
                NewChild.InnerText = Me.Title.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/titleabbrev")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.TitleAbbrev.Text
        Else
            If Me.TitleAbbrev.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("titleabbrev")
                NewChild.InnerText = Me.TitleAbbrev.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/doctype")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.DocType.Text
        Else
            If Me.DocType.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("doctype")
                NewChild.InnerText = Me.DocType.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/engtitle")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.EnglishTitle.Text
        Else
            If Me.EnglishTitle.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("engtitle")
                NewChild.InnerText = Me.EnglishTitle.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/modtitle")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.ModTitle.Text
        Else
            If Me.ModTitle.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("modtitle")
                NewChild.InnerText = Me.ModTitle.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/creator")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Author.Text
        Else
            If Me.Author.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("creator")
                NewChild.InnerText = Me.Author.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/creatorillust")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Illustrator.Text
        Else
            If Me.Illustrator.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("creatorillust")
                NewChild.InnerText = Me.Illustrator.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/creatordate")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.ADeath.Text
        Else
            If Me.ADeath.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("creatordate")
                NewChild.InnerText = Me.ADeath.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/creatorillustdate")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.IDeath.Text
        Else
            If Me.IDeath.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("creatorillustdate")
                NewChild.InnerText = Me.IDeath.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/editor")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Editor.Text
        Else
            If Me.Editor.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("editor")
                NewChild.InnerText = Me.Editor.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/publisher")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Publisher.Text
        Else
            If Me.Publisher.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("publisher")
                NewChild.InnerText = Me.Publisher.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If


        metadatanode = xmlDoc.SelectSingleNode("/metadata/city")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.City.Text
        Else
            If Me.City.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("city")
                NewChild.InnerText = Me.City.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/date")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.OrPub.Text
        Else
            If Me.OrPub.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("date")
                NewChild.InnerText = Me.OrPub.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/edition")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Edition.Text
        Else
            If Me.Edition.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("edition")
                NewChild.InnerText = Me.Edition.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/language")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Language.Text
        Else
            If Me.Language.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("language")
                NewChild.InnerText = Me.Language.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/coverage")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Coverage.Text
        Else
            If Me.Coverage.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("coverage")
                NewChild.InnerText = Me.Coverage.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/description")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Description.Text
        Else
            If Me.Description.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("description")
                NewChild.InnerText = Me.Description.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/abstract")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Abstract.Text
        Else
            If Me.Abstract.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("abstract")
                NewChild.InnerText = Me.Abstract.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/notes")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Notes.Text
        Else
            If Me.Notes.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("notes")
                NewChild.InnerText = Me.Notes.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/notes")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.Notes.Text
        Else
            If Me.Notes.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("notes")
                NewChild.InnerText = Me.Notes.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/urlref")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.OnlineRef.Text
        Else
            If Me.OnlineRef.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("urlref")
                NewChild.InnerText = Me.OnlineRef.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/urlprop")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.OurRef.Text
        Else
            If Me.OurRef.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("urlprop")
                NewChild.InnerText = Me.OurRef.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/location")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.PLocation.Text
        Else
            If Me.PLocation.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("location")
                NewChild.InnerText = Me.PLocation.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/urlindex")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.IndexLink.Text
        Else
            If Me.IndexLink.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("urlindex")
                NewChild.InnerText = Me.IndexLink.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        metadatanode = xmlDoc.SelectSingleNode("/metadata/urltoc")
        If metadatanode IsNot Nothing Then
            metadatanode.InnerText = Me.TocLink.Text
        Else
            If Me.TocLink.Text <> "" Then
                'add
                metadatanode = xmlDoc.SelectSingleNode("/metadata")
                Dim NewChild As XmlElement = xmlDoc.CreateElement("urltoc")
                NewChild.InnerText = Me.TocLink.Text
                metadatanode.AppendChild(NewChild)
            End If
        End If

        Console.WriteLine(xmlDoc.OuterXml)
        Try
            xmlDoc.Save(Filename)
            Me.Hide()
        Catch ex As Exception
            MsgBox("Unable to save: " & vbCrLf & Filename & vbCrLf, MsgBoxStyle.OkOnly)
            Exit Sub
        End Try
    End Sub

    Private Sub OpenPdf_Click(sender As Object, e As EventArgs) Handles OpenPdf.Click
        Dim FolderPath As String = Path.GetDirectoryName(File.Text)

        Dim Document As String = FolderPath & "\" & Identifier.Text & ".pdf"
        If IO.File.Exists(Document) Then
            System.Diagnostics.Process.Start(Document)
        Else
            'Dim xmlFilePath As String = Source.Text
            'Dim SearchPattern As String = "*.pdf"
            Dim PDFfiles()
            Try
                PDFfiles = Directory.GetFiles(FolderPath, "*.pdf", SearchOption.AllDirectories)
            Catch ex As Exception
                'MsgBox("Unable to find part of the path " & FolderPath, MsgBoxStyle.OkOnly)
                Exit Sub
            End Try
            If PDFfiles.Length > 0 Then
                System.Diagnostics.Process.Start(PDFfiles(0))
            End If
            'Process.Start("explorer.exe", "/select," & filePath)
        End If
    End Sub

    Private Sub ShowFolder_Click(sender As Object, e As EventArgs) Handles ShowFolder.Click
        Dim FolderPath As String = Path.GetDirectoryName(File.Text)

        Dim Document As String = FolderPath & "\" & Identifier.Text & ".pdf"
        If IO.File.Exists(Document) Then
            Process.Start("explorer.exe", FolderPath)
            'System.Diagnostics.Process.Start(Document)
        Else
            'Dim xmlFilePath As String = Source.Text
            'Dim SearchPattern As String = "*.pdf"
            Dim PDFfiles() = Directory.GetFiles(FolderPath, "*.pdf", SearchOption.AllDirectories)
            If PDFfiles.Length > 0 Then
                System.Diagnostics.Process.Start(PDFfiles(0))
                Process.Start("explorer.exe", FolderPath)
            Else
                Process.Start("explorer.exe", FolderPath)
            End If
        End If
    End Sub
End Class