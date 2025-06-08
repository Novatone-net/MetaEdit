Imports System.Xml
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Collections.Concurrent
Imports System.ComponentModel

Public Class LibraryData
	Dim MetaFiles() As String
	Dim FileInGrid As String
	Private Sub RenameFolders_Click(sender As Object, e As EventArgs) Handles RenameFolders.Click
		RenameFolders.Enabled = False
		Application.DoEvents()
		' Path to the XML file
		'Dim xmlFilePath As String = "E:\0 Ref\books"
		Dim folderPath As String = Source.Text '"E:\0 Ref\books"
		Dim searchPattern As String = "*_meta.xml"

		Dim files As String() = Directory.GetFiles(folderPath, searchPattern, SearchOption.AllDirectories)
		For Each File As String In files
			Dim Title As String = ""
			Dim DateVal As String = ""
			Dim Year As String = ""
			Dim Subject As String = ""
			Dim Language As String = ""
			Dim Description As String = ""
			Dim Publisher As String = ""
			Dim Author As String = ""
			Dim ModTitle As String = ""
			Dim ShortTitle As String = ""
			Dim EngTitle As String = ""

			' Load the XML document
			Dim xmlDoc As New XmlDocument()
			Try
				xmlDoc.Load(File)
			Catch ex As Exception
				Continue For
			End Try

			' Load specific nodes we are intrested in
			Dim metadataNode As XmlNode = xmlDoc.SelectSingleNode("/metadata")
			If metadataNode IsNot Nothing Then
				Console.WriteLine("Metadata Information:")

				' Loop through child nodes
				For Each ChildNode As XmlNode In metadataNode.ChildNodes
					Dim NodeName As String = ChildNode.Name.Trim("""").ToLower
					Select Case NodeName
						Case Is = "title"
							Title = ChildNode.InnerText
						Case Is = "titleabbrev"
							ShortTitle = ChildNode.InnerText
						Case Is = "engtitle"
							EngTitle = ChildNode.InnerText
						Case Is = "modtitle"
							ModTitle = ChildNode.InnerText
						Case Is = "date"
							DateVal = ChildNode.InnerText
						Case Is = "year"
							Year = ChildNode.InnerText
						Case Is = "subject"
							If Subject = "" Then
								Subject = " " & ChildNode.InnerText
							Else
								Subject &= " " & ChildNode.InnerText
							End If
						Case Is = "description"
							Description = ChildNode.InnerText
						Case Is = "publisher"
							Publisher = ChildNode.InnerText
						Case Is = "language"
							Language = ChildNode.InnerText
						Case Is = "creator"
							Author = ChildNode.InnerText
					End Select
				Next
			End If

			Dim FolderTitle As String = ""
			Dim WorkingTitle As String = Title
			If ModTitle <> "" Then
				WorkingTitle = ModTitle
			ElseIf ShortTitle <> "" Then
				WorkingTitle = ShortTitle
			ElseIf EngTitle <> "" Then
				WorkingTitle = EngTitle
			End If
			If WorkingTitle <> "" Then
				If WorkingTitle.Length > 200 Then ' long WorkingTitle, try to shorten it
					Dim SplitLoc As Integer = WorkingTitle.IndexOf(",")
					If SplitLoc > 180 Then ' reasonable WorkingTitle at the first ,
						FolderTitle = WorkingTitle.Substring(0, SplitLoc)
					Else ' try a :
						SplitLoc = WorkingTitle.IndexOf(":")
						If SplitLoc > 180 Then
							FolderTitle = WorkingTitle.Substring(0, SplitLoc)
						Else ' no luck, maybe a .
							SplitLoc = WorkingTitle.IndexOf(".")
							If SplitLoc > 180 Then
								FolderTitle = WorkingTitle.Substring(0, SplitLoc)
							Else
								' Give up and trim long name
								FolderTitle = WorkingTitle.Remove(195) & "..."
							End If
						End If
					End If
				Else
					FolderTitle = WorkingTitle ' short enough, use it all
				End If
			End If
			If FolderTitle.Length > 195 Then
				FolderTitle = WorkingTitle.Remove(195) & "..."
			End If
			If Year <> "" Then
				FolderTitle = FolderTitle.Replace(" " & Year, "").Trim
				FolderTitle &= " (" & Year & ")"

			ElseIf DateVal <> "" Then
				FolderTitle = FolderTitle.Replace(" " & DateVal, "").Trim
				FolderTitle &= " (" & DateVal & ")"
			End If
			FolderTitle = CleanFileName(FolderTitle)

			Dim Folders() As String = File.Split("\")
			Dim ContainingFolder As String = ""
			If Folders.Length > 1 Then
				ContainingFolder = Folders(Folders.Length - 2)
			End If

			Dim Dupe As Integer
			Dim Braclok As Integer = ContainingFolder.LastIndexOf("[")
			Dim ContBase As String
			If Braclok > 0 Then
				ContBase = ContainingFolder.Remove(ContainingFolder.LastIndexOf("[")).Trim
			Else
				ContBase = ContainingFolder
			End If

			If ContainingFolder <> FolderTitle And ContBase <> FolderTitle Then
				Dim ParentStart As Integer = Path.GetDirectoryName(File).LastIndexOf(ContainingFolder)
				Dim ParentFolder As String = Path.GetDirectoryName(File).Remove(ParentStart)
				Dim TestName As String = ParentFolder & FolderTitle
				Dupe = 0
				Do While Directory.Exists(TestName)
					Dupe += 1
					TestName = ParentFolder & FolderTitle & " [" & Dupe.ToString & "]"
				Loop
				If Dupe > 0 Then
					FolderTitle &= " [" & Dupe.ToString & "]"
				End If
				Dim OldPath As String = Path.GetDirectoryName(File)
				Dim NewPath As String = ParentFolder & FolderTitle
				Try
					FileSystem.Rename(OldPath, NewPath)
				Catch ex As Exception
					MsgBox("Unable to rename folder " & OldPath &
					". The folder may be locked try closing files in that folder. Skipping.",
					MsgBoxStyle.OkOnly)
				End Try
			End If
		Next
		RenameFolders.Enabled = True
		Application.DoEvents()
		LoadSources.PerformClick()
	End Sub
	Function DecodeAndStripHtml(input As String) As String
		' Decode HTML entities
		Dim Decoded As String = WebUtility.HtmlDecode(input)
		' Remove HTML tags
		Dim Stripped As String = Regex.Replace(decoded, "<.*?>", String.Empty)
		Return Stripped
	End Function

	Public Function CleanFileName(FileName As String) As String
		Dim InvalidChars As Char() = Path.GetInvalidFileNameChars()
		Dim CleanedName As New String(FileName.Where(Function(c) Not InvalidChars.Contains(c)).ToArray())
		Return CleanedName
	End Function

	Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		LoadSources.Enabled = False
		ViewBy.SelectedIndex = 0
		Me.Show()
		Dim Pattern As String = ViewBy.SelectedItem
		' TODO: Load saved settings like folder, view-by, window size, position and split etc.

		' Start scan for metadata files asynchronously
		Dim loadTask As Task = Task.Run(Sub() GetMetadataFileList(Pattern))
		' Do Additional initialization here

		' Wait for the file scan to complete to update the listbox
		Await Task.WhenAll(loadTask)
		MetaDataFilesList.DataSource = MetaFiles
		LoadSources.Enabled = True
	End Sub
	Private Sub GetMetadataFileList(SearchPattern)
		Dim xmlFilePath As String = Source.Text
		Dim folderPath As String = Source.Text
		MetaFiles = Directory.GetFiles(folderPath, SearchPattern, SearchOption.AllDirectories)
	End Sub

	Private Async Sub LoadSources_Click(sender As Object, e As EventArgs) Handles LoadSources.Click
		Dim Pattern As String = ViewBy.SelectedItem

		Dim loadTask As Task = Task.Run(Sub() GetMetadataFileList(Pattern))
		' Wait for the file scan to complete to update the listbox
		Await Task.WhenAll(loadTask)
		MetaDataFilesList.DataSource = MetaFiles
	End Sub

	Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
		KeyValGrid.Update()
	End Sub

	Private Sub EditXML_Click(sender As Object, e As EventArgs) Handles EditXML.Click

		' Path to the XML file
		Dim xmlFilePath As String = Source.Text
		Dim folderPath As String = Source.Text
		Dim searchPattern As String = "*_meta.xml"

		Dim Identifier As String = ""
		Dim DBKey As String = ""
		Dim Publish As String = ""
		Dim DocType As String = ""
		Dim Title As String = ""
		Dim EngTitle As String = ""
		Dim ShortTitle As String = ""
		Dim ModTitle As String = ""
		Dim Author As String = ""
		Dim Illustrator As String = ""
		Dim Editor As String = ""
		Dim AuthDeath As String = ""
		Dim IllustDeath As String = ""
		Dim Publisher As String = ""
		Dim City As String = ""
		Dim DateVal As String = ""
		Dim Year As String = ""
		Dim Edition As String = ""
		Dim Language As String = ""
		Dim Subject As String = ""
		Dim Coverage As String = ""
		Dim Description As String = ""
		Dim Abstract As String = ""
		Dim Notes As String = ""
		Dim OnlineUrl As String = ""
		Dim OurUrl As String = ""
		Dim OurToc As String = ""
		Dim OurIndex As String = ""
		Dim PLocation As String = ""

		Dim Filename As String = FileInGrid
		Dim xmlDoc As New XmlDocument()
		' Rename containing folder to shorten the path to less than 260 characters
		If Filename.Length > 255 Then
			Dim Overage As Integer = Filename.Length - 255
			Dim ContainingFolder As String = Path.GetDirectoryName(Filename)
			Dim ParentLoc As Integer = ContainingFolder.LastIndexOf("\")

			Dim File As String = Path.GetFileName(Filename)
			Dim NewFolder As String = ContainingFolder.Substring(0, ContainingFolder.Length - Overage)
			Dim NewPath As String = NewFolder & "\" & File
			If ContainingFolder.Length - Overage > ParentLoc Then
				Try
					FileSystem.Rename(ContainingFolder, NewFolder)
					Filename = NewFolder & "\" & File
				Catch ex As Exception
					' let it fall through to manual renameing
				End Try
			End If
		End If
		Console.WriteLine(Filename.Length)
		Try
			xmlDoc.Load(Filename)
		Catch ex As Exception
			Console.WriteLine(Filename.Length)
			If Filename.Length > 260 Then
				Dim ProblemPath As String = Path.GetDirectoryName(Filename)
				Process.Start("explorer.exe", folderPath)
				MsgBox("The path is too long for: " & vbCrLf & Filename & vbCrLf &
					"Rename the Folder to a shorter name and click ok to refresh the list, and try again.",
					MsgBoxStyle.OkOnly)
				LoadSources.PerformClick()
				MetaDataFilesList.DataSource = MetaFiles
				Exit Sub
			End If
		End Try

		' Read nodes for our standard ones 
		Dim metadataNode As XmlNode = xmlDoc.SelectSingleNode("/metadata")
		If metadataNode IsNot Nothing Then
			'    ' Loop through child nodes
			For Each ChildNode As XmlNode In metadataNode.ChildNodes
				Dim NodeName As String = ChildNode.Name.Trim("""").ToLower
				Select Case NodeName
					Case Is = "identifier"
						Identifier = ChildNode.InnerText
					Case Is = "dbkey"
						DBKey = ChildNode.InnerText
					Case Is = "public"
						Publish = ChildNode.InnerText

					Case Is = "doctype"
						DocType = ChildNode.InnerText
					Case Is = "title"
						Title = ChildNode.InnerText
						Console.WriteLine("Full Title:" & ChildNode.InnerText)
					Case Is = "engtitle"
						EngTitle = ChildNode.InnerText
					Case Is = "titleabbrev"
						ShortTitle = ChildNode.InnerText
					Case Is = "modtitle"
						ModTitle = ChildNode.InnerText
					Case Is = "creator"
						' use the first found creator
						' TODO: support multiple creator nodes - multiple nodes now supported in edit gridview on main window
						If Author = "" Then
							Author = ChildNode.InnerText
						End If
					Case Is = "creatorillust"
						Illustrator = ChildNode.InnerText
					Case Is = "creatordate"
						AuthDeath = ChildNode.InnerText
					Case Is = "creatorillustdate"
						IllustDeath = ChildNode.InnerText
					Case Is = "editor"
						Editor = ChildNode.InnerText
					Case Is = "publisher"
						Publisher = ChildNode.InnerText
						Console.WriteLine("Publisher:" & ChildNode.InnerText)
					Case Is = "city"
						City = ChildNode.InnerText
					Case Is = "date"
						DateVal = ChildNode.InnerText
						Console.WriteLine("Date:" & ChildNode.InnerText)
					Case Is = "year"
						Year = ChildNode.InnerText
						Console.WriteLine("Year:" & ChildNode.InnerText)
					Case Is = "edition"
						Edition = ChildNode.InnerText
					Case Is = "language"
						Language = ChildNode.InnerText
					Case Is = "subject"
						If Subject = "" Then
							Subject = ChildNode.InnerText
						Else
							Subject &= vbCrLf & ChildNode.InnerText
						End If
					Case Is = "coverage"
						Coverage = ChildNode.InnerText
					Case Is = "description"
						Description = DecodeAndStripHtml(ChildNode.InnerText)
						Console.WriteLine("Description:" & DecodeAndStripHtml(ChildNode.InnerText))
					Case Is = "abstract"
						Abstract = ChildNode.InnerText
					Case Is = "notes"
						Notes = ChildNode.InnerText
					Case Is = "urlref"
						OnlineUrl = ChildNode.InnerText
					Case Is = "urlprop"
						OurUrl = ChildNode.InnerText
					Case Is = "urltoc"
						OurToc = ChildNode.InnerText
					Case Is = "urlindex"
						OurIndex = ChildNode.InnerText
					Case Is = "location"
						PLocation = ChildNode.InnerText

				End Select
			Next
			EditWindow.File.Text = Filename
			EditWindow.Identifier.Text = Identifier
			EditWindow.DBKey.Text = DBKey
			EditWindow.DocType.Text = DocType
			If Publish = "True" Then
				EditWindow.Publish.Checked = True
			Else
				EditWindow.Publish.Checked = False
			End If
			EditWindow.Title.Text = Title
			EditWindow.EnglishTitle.Text = EngTitle
			EditWindow.TitleAbbrev.Text = ShortTitle
			EditWindow.ModTitle.Text = ModTitle
			EditWindow.DocType.Text = DocType
			EditWindow.Author.Text = Author
			EditWindow.ADeath.Text = AuthDeath
			EditWindow.Illustrator.Text = Illustrator
			EditWindow.IDeath.Text = IllustDeath
			EditWindow.Editor.Text = Editor
			EditWindow.Publisher.Text = Publisher
			EditWindow.City.Text = City
			EditWindow.Language.Text = Language
			EditWindow.Subjects.Text = Subject
			EditWindow.Coverage.Text = Coverage
			EditWindow.Description.Text = Description
			EditWindow.Abstract.Text = Abstract
			EditWindow.Notes.Text = Notes
			EditWindow.OnlineRef.Text = OnlineUrl
			EditWindow.OurRef.Text = OurUrl

			If DateVal <> "" Then
				EditWindow.OrPub.Text = DateVal
			ElseIf Year <> "" Then
				EditWindow.OrPub.Text = Year
			End If
			EditWindow.Show()
			EditWindow.BringToFront()
			EditWindow.Edition.Text = Edition
			EditWindow.PLocation.Text = PLocation
			EditWindow.TocLink.Text = OurToc
			EditWindow.IndexLink.Text = OurIndex
		Else
			Console.WriteLine("No metadata node found in the XML file.")
			' clear the form in case it is being reused
			Identifier = Path.GetFileName(Filename.Replace(searchPattern.Replace("*", ""), ""))
			EditWindow.Identifier.Text = Identifier
			EditWindow.DBKey.Text = DBKey
			EditWindow.Publish.Checked = False
			EditWindow.DocType.Text = DocType
			EditWindow.Title.Text = Title
			EditWindow.EnglishTitle.Text = EngTitle
			EditWindow.TitleAbbrev.Text = ShortTitle
			EditWindow.ModTitle.Text = ModTitle
			EditWindow.DocType.Text = DocType
			EditWindow.Author.Text = Author
			EditWindow.ADeath.Text = AuthDeath
			EditWindow.Illustrator.Text = Illustrator
			EditWindow.IDeath.Text = IllustDeath
			EditWindow.Editor.Text = Editor
			EditWindow.Publisher.Text = Publisher
			EditWindow.City.Text = City
			EditWindow.Language.Text = Language
			EditWindow.Subjects.Text = Subject
			EditWindow.Coverage.Text = Coverage
			EditWindow.Description.Text = Description
			EditWindow.Abstract.Text = Abstract
			EditWindow.Notes.Text = Notes
			EditWindow.OnlineRef.Text = OnlineUrl
			EditWindow.OurRef.Text = OurUrl
			EditWindow.OrPub.Text = Year
			EditWindow.File.Text = Filename
			EditWindow.PLocation.Text = PLocation
			EditWindow.TocLink.Text = OurToc
			EditWindow.IndexLink.Text = OurIndex

			EditWindow.Show()
			EditWindow.BringToFront()
		End If
	End Sub
	Private Function FindMetaFor(FilePath) As String

		Dim DocFolder As String = Path.GetDirectoryName(FilePath)
		Dim DocName As String = Path.GetFileNameWithoutExtension(FilePath).ToLower
		Dim MetaName As String = DocFolder & "\" & DocName & "_meta.xml"
		Try
			If System.IO.File.Exists(MetaName) Then
				Return MetaName
			End If
		Catch ex As Exception

		End Try
		Dim Candidates() As String = Directory.GetFiles(DocFolder, "*_meta.xml", SearchOption.TopDirectoryOnly)
		If Candidates.Count = 0 Then
			Return ""
		ElseIf Candidates.Count = 1 Then
			Return Candidates(0)
		Else
			Dim MaxScore As Double = 0
			Dim MaxIndex As Integer = 0
			For i = 0 To Candidates.Count - 1
				Dim CandidateName As String = Candidates(i).Replace("_meta.xml", "").ToLower
				Dim Score As Double = ComputeLevenshteinSimilarity(DocName, CandidateName)
				If Score > MaxScore Then
					MaxScore = Score
					MaxIndex = i
				End If
			Next
			Return Candidates(MaxIndex)
		End If
		Return ""
	End Function

	Function ComputeLevenshteinSimilarity(str1 As String, str2 As String) As Double
		Dim len1 As Integer = str1.Length
		Dim len2 As Integer = str2.Length
		Dim matrix(len1, len2) As Integer

		If len1 = 0 Then Return If(len2 = 0, 1.0, 0.0)
		If len2 = 0 Then Return If(len1 = 0, 1.0, 0.0)

		For i As Integer = 0 To len1
			matrix(i, 0) = i
		Next

		For j As Integer = 0 To len2
			matrix(0, j) = j
		Next

		For i As Integer = 1 To len1
			For j As Integer = 1 To len2
				Dim cost As Integer = If(str1(i - 1) = str2(j - 1), 0, 1)
				matrix(i, j) = Math.Min(matrix(i - 1, j) + 1,
								   Math.Min(matrix(i, j - 1) + 1,
											matrix(i - 1, j - 1) + cost))
			Next
		Next

		Dim maxLen As Integer = Math.Max(len1, len2)
		Dim similarity As Double = (maxLen - matrix(len1, len2)) / maxLen
		Return similarity
	End Function
	Private Sub MetaDataFilesList_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles MetaDataFilesList.SelectedIndexChanged
		Dim PathTooLong As Boolean = False
		Dim LoadedTemplate As Boolean = False
		Dim FileOK As Boolean = False

		Try
			' Path to the XML file
			Dim xmlFilePath As String = Source.Text
			Dim folderPath As String = Source.Text
			Dim searchPattern As String = "*_meta.xml"

			Dim Filename As String = MetaDataFilesList.SelectedItem.ToString
			Dim FilePath As String = Path.GetDirectoryName(Filename)
			Dim Basename As String = Path.GetFileNameWithoutExtension(Filename)
			If ViewBy.SelectedItem <> "*_meta.xml" Then
				Filename = FindMetaFor(Filename)
			End If

			Dim xmlDoc As New XmlDocument()
			' Rename containing folder to shorten the path to less than 260 characters
			If Filename.Length > 255 Then
				Dim Overage As Integer = Filename.Length - 255
				Dim ContainingFolder As String = Path.GetDirectoryName(Filename)
				Dim ParentLoc As Integer = ContainingFolder.LastIndexOf("\")

				Dim File As String = Path.GetFileName(Filename)
				Dim NewFolder As String = ContainingFolder.Substring(0, ContainingFolder.Length - Overage)
				Dim NewPath As String = NewFolder & "\" & File
				If ContainingFolder.Length - Overage > ParentLoc Then
					Try
						FileSystem.Rename(ContainingFolder, NewFolder)
						Filename = NewFolder & "\" & File

					Catch ex As Exception
						' let it fall through to manual renaming
					End Try

				End If
			End If
			If Filename = "" Then
				FileOK = False
				Dim Template As String = Path.GetDirectoryName(Application.ExecutablePath) & "\template_meta.xml"
				Try
					' Verify the template exists and is readable
					xmlDoc.Load(Template)
					LoadedTemplate = True
					Dim IdNode As XmlNode = xmlDoc.SelectSingleNode("/metadata/identifier")
					If IdNode IsNot Nothing Then
						IdNode.InnerText = Basename
					Else
						' add
						IdNode = xmlDoc.SelectSingleNode("/metadata")
						Dim NewChild As XmlElement = xmlDoc.CreateElement("identifier")
						NewChild.InnerText = Path.GetFileNameWithoutExtension(Filename)
						IdNode.AppendChild(NewChild)
					End If
					FileInGrid = FilePath & "\" & Basename & "_meta.xml"
				Catch ex As Exception
					If Filename.Length > 260 Then
						PathTooLong = True
					End If
				End Try
			Else
				Try
					xmlDoc.Load(Filename)
					FileOK = True
					FileInGrid = Filename

				Catch ex As Exception
					FileOK = False
					If Filename.Length > 260 Then
						PathTooLong = True
					End If
				End Try
			End If
			Dim CurMetaDS As New MetadataDS.MetaDataTable
			Dim metadataNode As XmlNode = xmlDoc.SelectSingleNode("/metadata")
			If metadataNode IsNot Nothing Then
				' Loop through child nodes
				For Each ChildNode As XmlNode In metadataNode.ChildNodes
					Dim NodeName As String = ChildNode.Name.Trim("""").ToLower
					Dim NewRow As MetadataDS.MetaRow
					NewRow = CurMetaDS.NewRow
					NewRow.KEY = ChildNode.Name
					NewRow.Val = ChildNode.InnerText
					CurMetaDS.Rows.Add(NewRow)
				Next
				KeyValGrid.DataSource = CurMetaDS
				'KeyValGrid.Sort(KeyValGrid.Columns(1), ListSortDirection.Ascending)
				KeyValGrid.Columns(0).Visible = False
				KeyValGrid.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
				KeyValGrid.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
			Else
				FileOK = False
			End If
		Catch ex As Exception
			FileInGrid = ""
			KeyValGrid.DataSource = Nothing
		End Try
		Dim Problems As String = ""
		If FileOK = False Then
			Problems = "Could not read a metadata file. "
		End If
		If LoadedTemplate = True Then
			Problems &= "Loaded a template file. "
		End If
		If PathTooLong = True Then
			Problems &= "Path is too long. "
		End If
		If Problems <> "" Then
			MsgBox(Problems, MsgBoxStyle.OkOnly)
		End If
	End Sub

	Private Sub MetaDataFilesList_DoubleClick(sender As Object, e As EventArgs) Handles MetaDataFilesList.DoubleClick
		EditXML.PerformClick()
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles PickSource.Click
		ChFolder.SelectedPath = Source.Text
		If ChFolder.ShowDialog() = Windows.Forms.DialogResult.OK Then
			'get a list of all the supported files
			Source.Text = ChFolder.SelectedPath
			LoadSources.PerformClick()
		End If
	End Sub

	Private Async Sub MarkEl_Click(sender As Object, e As EventArgs) Handles MarkEl.Click
		MarkEl.Enabled = False
		Dim Failed As New List(Of String)
		Await Task.Run(Sub()
						   Parallel.ForEach(MetaDataFilesList.Items.Cast(Of String)(),
					 Sub(Filename)
						 Dim xmlDoc As New XmlDocument()
						 Dim Changed As Boolean = False

						 Try
							 xmlDoc.Load(Filename)
						 Catch ex As Exception
							 Dim root As XmlElement = xmlDoc.CreateElement("metadata")
							 xmlDoc.AppendChild(root)
							 Changed = True
							 Console.WriteLine(xmlDoc.OuterXml)
						 End Try

						 Dim metadatanode As XmlNode = xmlDoc.SelectSingleNode("/metadata/doctype")
						 If metadatanode IsNot Nothing Then
							 ' Process the doctype
							 Dim DocType As String = metadatanode.InnerText
							 If DocType = "" Then
								 metadatanode.InnerText = "EL"
								 Changed = True
							 ElseIf Not DocType.Contains("EL") Then
								 metadatanode.InnerText = "EL " & DocType
								 Changed = True
							 End If
						 Else
							 metadatanode = xmlDoc.SelectSingleNode("/metadata")
							 Dim NewChild As XmlElement = xmlDoc.CreateElement("doctype")
							 NewChild.InnerText = "EL"
							 metadatanode.AppendChild(NewChild)
							 Changed = True
						 End If

						 If Changed Then
							 Try
								 xmlDoc.Save(Filename)
							 Catch ex As Exception
								 SyncLock Failed
									 Failed.Add(Filename)
								 End SyncLock
							 End Try
						 End If
					 End Sub)
					   End Sub)

		If Failed.Count > 0 Then
			MetaDataFilesList.DataSource = Failed
			MsgBox("Unable to save some files the problem files are in the XML Files list.", MsgBoxStyle.OkOnly)
		End If
		MarkEl.Enabled = True
	End Sub

	Private Sub SaveGrid_Click(sender As Object, e As EventArgs) Handles SaveGrid.Click
		' This can be expected to take very little time, enable / disable the button
		' to give some indication that something happened.
		SaveGrid.Enabled = False
		Cursor = Cursors.WaitCursor
		Application.DoEvents()
		If FileInGrid <> "" Then
			Dim Filename As String = FileInGrid
			Dim xmlDoc As New XmlDocument()
			Dim xmlDeclaration As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
			xmlDoc.AppendChild(xmlDeclaration)
			Dim root As XmlElement = xmlDoc.CreateElement("metadata")
			xmlDoc.AppendChild(root)
			Dim metadatanode As XmlNode
			metadatanode = xmlDoc.SelectSingleNode("/metadata")
			For Each Row As DataGridViewRow In KeyValGrid.Rows
				If Row.Cells(1).Value IsNot Nothing Then
					Dim NewChild As XmlElement = xmlDoc.CreateElement(Row.Cells(1).Value)
					NewChild.InnerText = Row.Cells(2).Value
					metadatanode.AppendChild(NewChild)
				End If
			Next
			Try
				xmlDoc.Save(Filename)
			Catch ex As Exception
				MsgBox("Unable to save: " & vbCrLf & Filename & vbCrLf, MsgBoxStyle.OkOnly)
				MarkEl.Enabled = True
				Exit Sub
			End Try
		End If
		SaveGrid.Enabled = True
		Cursor = Cursors.Default
	End Sub
	Private Async Function LoadPDFFileListAsync(folderPath As String, searchPattern As String) As Task(Of String())
		Return Await Task.Run(Function() Directory.GetFiles(folderPath, searchPattern, SearchOption.AllDirectories))
	End Function


	' Brute force metadata file verification (ie. slow) could have been improved but
	' abandoned in favor of creating metadata files automatically as needed and allowing the file list to show other file
	' types instead of just meta.xml files
	Private Async Sub CreateMissingMeta_Click(sender As Object, e As EventArgs) Handles CreateMissingMeta.Click
		CreateMissingMeta.Enabled = False
		Dim Template As String = Path.GetDirectoryName(Application.ExecutablePath) & "\template_meta.xml"
		Dim xmlTemplate As New XmlDocument()
		Try
			' Verify the template exists and is readable
			xmlTemplate.Load(Template)
		Catch ex As Exception
			MsgBox("Template not found: " & vbCrLf & Template, MsgBoxStyle.OkOnly)
			Exit Sub
		End Try

		Dim FolderPath As String = Source.Text
		Dim SearchPattern As String = "*.pdf"
		Dim FileList As String() = Await LoadPDFFileListAsync(FolderPath, SearchPattern)

		Dim Failed As New ConcurrentBag(Of String)

		Await Task.Run(Sub()
						   Parallel.ForEach(FileList, Sub(Filename)
														  MissingMetaProcessFile(Filename, Template, Failed)
													  End Sub)
					   End Sub)

		CreateMissingMeta.Enabled = True

	End Sub
	' Brute force metadata file verification (ie. slow) could have been improved but
	' abandoned in favor of creating metadata files automatically as needed and allowing the file list to show other file
	' types instead of just meta.xml files

	Private Sub MissingMetaProcessFile(Filename As String, Template As String, Failed As ConcurrentBag(Of String))
		Dim CreateFromTemplate As Boolean = False
		Dim xmlDoc As New XmlDocument()
		Dim xmlTemplate As New XmlDocument()
		If System.IO.File.Exists(Filename) Then
			Try
				xmlDoc.Load(Filename)
			Catch ex As Exception
				CreateFromTemplate = True
			End Try
			Dim metadatanode As XmlNode = xmlDoc.SelectSingleNode("/metadata")
			If metadatanode Is Nothing Then
				CreateFromTemplate = True
			End If
		Else
			CreateFromTemplate = True
		End If

		Dim DocFolder As String = Path.GetDirectoryName(Filename)
		Dim DocName As String = Path.GetFileNameWithoutExtension(Filename)

		If CreateFromTemplate = True Then
			' check if the metadata file exists with an unusual name
			Dim Candidates() As String = Directory.GetFiles(DocFolder, "*_meta.xml", SearchOption.TopDirectoryOnly)
			Dim PDFs() As String = Directory.GetFiles(DocFolder, "*.pdf", SearchOption.TopDirectoryOnly)

			If PDFs.Count = 1 And Candidates.Count = 1 Then
				CreateFromTemplate = False
			ElseIf PDFs.Count >= Candidates.Count Then
				If Candidates.Count > 0 Then
					For Each Candidate As String In Candidates
						Try
							xmlDoc.Load(Candidate)
						Catch ex As Exception
							Continue For
						End Try
						Dim metadatanode As XmlNode = xmlDoc.SelectSingleNode("/metadata/identifier")
						If metadatanode IsNot Nothing Then
							If metadatanode.InnerText.ToLower = DocName.ToLower Then
								CreateFromTemplate = False
								Exit For
							ElseIf Candidates.Count = 1 Then
								If DocName.ToLower.ToLower.Contains(metadatanode.InnerText) Then
									CreateFromTemplate = False
								End If
							End If
						End If

					Next
				End If

			End If

		End If
		If CreateFromTemplate = True Then
			Try
				xmlTemplate.Load(Template)
			Catch ex As Exception
				Failed.Add(Filename)
				Exit Sub
			End Try
			Dim metadatanode As XmlNode = xmlTemplate.SelectSingleNode("/metadata/identifier")
			If metadatanode IsNot Nothing Then
				metadatanode.InnerText = Path.GetFileNameWithoutExtension(Filename)
			Else
				metadatanode = xmlTemplate.SelectSingleNode("/metadata")
				Dim NewChild As XmlElement = xmlTemplate.CreateElement("identifier")
				NewChild.InnerText = Path.GetFileNameWithoutExtension(Filename)
				metadatanode.AppendChild(NewChild)

			End If
			Dim MetaDataFile As String = DocFolder & "\" & DocName & "_meta.xml"
			Try
				xmlTemplate.Save(MetaDataFile)
			Catch ex As Exception
				Failed.Add(MetaDataFile)
			End Try
		End If


	End Sub

	Private Sub ViewBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ViewBy.SelectedIndexChanged
		LoadSources.PerformClick()
	End Sub

	Private Sub LibraryData_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		' TODO: Save settings like folder, view-by, window size, position and split etc.
	End Sub
End Class
