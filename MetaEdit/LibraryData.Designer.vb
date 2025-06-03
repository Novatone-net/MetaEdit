<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LibraryData
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.RenameFolders = New System.Windows.Forms.Button()
		Me.LoadSources = New System.Windows.Forms.Button()
		Me.EditXML = New System.Windows.Forms.Button()
		Me.Source = New System.Windows.Forms.TextBox()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.MetaDataFilesList = New System.Windows.Forms.ListBox()
		Me.KeyValGrid = New System.Windows.Forms.DataGridView()
		Me.PickSource = New System.Windows.Forms.Button()
		Me.ChFolder = New System.Windows.Forms.FolderBrowserDialog()
		Me.MarkEl = New System.Windows.Forms.Button()
		Me.MetadataDS = New MetaEdit.MetadataDS()
		Me.MetadataDSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
		Me.SaveGrid = New System.Windows.Forms.Button()
		Me.CreateMissingMeta = New System.Windows.Forms.Button()
		Me.ViewBy = New System.Windows.Forms.ComboBox()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		CType(Me.KeyValGrid, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.MetadataDS, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.MetadataDSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'RenameFolders
		'
		Me.RenameFolders.Location = New System.Drawing.Point(602, 16)
		Me.RenameFolders.Name = "RenameFolders"
		Me.RenameFolders.Size = New System.Drawing.Size(269, 23)
		Me.RenameFolders.TabIndex = 0
		Me.RenameFolders.Text = "Rename Folders based on metadata files"
		Me.RenameFolders.UseVisualStyleBackColor = True
		'
		'LoadSources
		'
		Me.LoadSources.Location = New System.Drawing.Point(12, 17)
		Me.LoadSources.Name = "LoadSources"
		Me.LoadSources.Size = New System.Drawing.Size(130, 23)
		Me.LoadSources.TabIndex = 5
		Me.LoadSources.Text = "Load Metadat sources"
		Me.LoadSources.UseVisualStyleBackColor = True
		'
		'EditXML
		'
		Me.EditXML.Location = New System.Drawing.Point(521, 16)
		Me.EditXML.Name = "EditXML"
		Me.EditXML.Size = New System.Drawing.Size(75, 23)
		Me.EditXML.TabIndex = 6
		Me.EditXML.Text = "Edit XML"
		Me.EditXML.UseVisualStyleBackColor = True
		'
		'Source
		'
		Me.Source.Location = New System.Drawing.Point(148, 19)
		Me.Source.Name = "Source"
		Me.Source.Size = New System.Drawing.Size(211, 20)
		Me.Source.TabIndex = 7
		Me.Source.Text = "\\dev-64\e$\0 Ref\heraldry"
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(11, 46)
		Me.SplitContainer1.Name = "SplitContainer1"
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.MetaDataFilesList)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.KeyValGrid)
		Me.SplitContainer1.Size = New System.Drawing.Size(1304, 674)
		Me.SplitContainer1.SplitterDistance = 637
		Me.SplitContainer1.TabIndex = 8
		'
		'MetaDataFilesList
		'
		Me.MetaDataFilesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MetaDataFilesList.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MetaDataFilesList.FormattingEnabled = True
		Me.MetaDataFilesList.ItemHeight = 20
		Me.MetaDataFilesList.Location = New System.Drawing.Point(3, 3)
		Me.MetaDataFilesList.Name = "MetaDataFilesList"
		Me.MetaDataFilesList.ScrollAlwaysVisible = True
		Me.MetaDataFilesList.Size = New System.Drawing.Size(631, 664)
		Me.MetaDataFilesList.TabIndex = 5
		'
		'KeyValGrid
		'
		Me.KeyValGrid.AllowUserToOrderColumns = True
		DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.KeyValGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
		Me.KeyValGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.KeyValGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.KeyValGrid.DefaultCellStyle = DataGridViewCellStyle6
		Me.KeyValGrid.Location = New System.Drawing.Point(3, 1)
		Me.KeyValGrid.Name = "KeyValGrid"
		Me.KeyValGrid.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.KeyValGrid.Size = New System.Drawing.Size(660, 666)
		Me.KeyValGrid.TabIndex = 2
		'
		'PickSource
		'
		Me.PickSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.PickSource.Location = New System.Drawing.Point(366, 17)
		Me.PickSource.Name = "PickSource"
		Me.PickSource.Size = New System.Drawing.Size(32, 23)
		Me.PickSource.TabIndex = 9
		Me.PickSource.Text = "📂"
		Me.PickSource.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.PickSource.UseVisualStyleBackColor = True
		'
		'MarkEl
		'
		Me.MarkEl.Location = New System.Drawing.Point(877, 16)
		Me.MarkEl.Name = "MarkEl"
		Me.MarkEl.Size = New System.Drawing.Size(123, 23)
		Me.MarkEl.TabIndex = 10
		Me.MarkEl.Text = "Mark all XML as EL"
		Me.MarkEl.UseVisualStyleBackColor = True
		'
		'MetadataDS
		'
		Me.MetadataDS.DataSetName = "MetadataDS"
		Me.MetadataDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
		'
		'MetadataDSBindingSource
		'
		Me.MetadataDSBindingSource.DataSource = Me.MetadataDS
		Me.MetadataDSBindingSource.Position = 0
		'
		'SaveGrid
		'
		Me.SaveGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SaveGrid.Location = New System.Drawing.Point(1240, 16)
		Me.SaveGrid.Name = "SaveGrid"
		Me.SaveGrid.Size = New System.Drawing.Size(75, 23)
		Me.SaveGrid.TabIndex = 11
		Me.SaveGrid.Text = "Save Edits"
		Me.SaveGrid.UseVisualStyleBackColor = True
		'
		'CreateMissingMeta
		'
		Me.CreateMissingMeta.Location = New System.Drawing.Point(1006, 16)
		Me.CreateMissingMeta.Name = "CreateMissingMeta"
		Me.CreateMissingMeta.Size = New System.Drawing.Size(204, 23)
		Me.CreateMissingMeta.TabIndex = 12
		Me.CreateMissingMeta.Text = "Create Missing Metadat files for PDFs"
		Me.CreateMissingMeta.UseVisualStyleBackColor = True
		Me.CreateMissingMeta.Visible = False
		'
		'ViewBy
		'
		Me.ViewBy.FormattingEnabled = True
		Me.ViewBy.Items.AddRange(New Object() {"*_meta.xml", "*.pdf"})
		Me.ViewBy.Location = New System.Drawing.Point(404, 18)
		Me.ViewBy.Name = "ViewBy"
		Me.ViewBy.Size = New System.Drawing.Size(95, 21)
		Me.ViewBy.TabIndex = 13
		'
		'LibraryData
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1327, 732)
		Me.Controls.Add(Me.ViewBy)
		Me.Controls.Add(Me.CreateMissingMeta)
		Me.Controls.Add(Me.SaveGrid)
		Me.Controls.Add(Me.MarkEl)
		Me.Controls.Add(Me.PickSource)
		Me.Controls.Add(Me.SplitContainer1)
		Me.Controls.Add(Me.Source)
		Me.Controls.Add(Me.EditXML)
		Me.Controls.Add(Me.LoadSources)
		Me.Controls.Add(Me.RenameFolders)
		Me.Name = "LibraryData"
		Me.Text = "MetaEdit"
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		CType(Me.KeyValGrid, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.MetadataDS, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.MetadataDSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents RenameFolders As Button
	Friend WithEvents LoadSources As Button
	Friend WithEvents EditXML As Button
	Friend WithEvents Source As TextBox
	Friend WithEvents SplitContainer1 As SplitContainer
	Friend WithEvents MetaDataFilesList As ListBox
	Friend WithEvents KeyValGrid As DataGridView
	Friend WithEvents PickSource As Button
	Friend WithEvents ChFolder As FolderBrowserDialog
	Friend WithEvents MarkEl As Button
	Friend WithEvents MetadataDSBindingSource As BindingSource
	Friend WithEvents MetadataDS As MetadataDS
	Friend WithEvents SaveGrid As Button
	Friend WithEvents CreateMissingMeta As Button
	Friend WithEvents ViewBy As ComboBox
End Class
