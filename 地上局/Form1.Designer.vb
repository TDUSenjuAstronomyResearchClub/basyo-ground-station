<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim ChartArea1 As DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend1 As DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series1 As DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea2 As DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend2 As DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series2 As DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea3 As DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend3 As DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series3 As DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea4 As DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend4 As DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series4 As DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        PictureBox1 = New PictureBox()
        Label1 = New Label()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        PictureBox2 = New PictureBox()
        PictureBox3 = New PictureBox()
        TextBox3 = New TextBox()
        Chart1 = New DataVisualization.Charting.Chart()
        Chart2 = New DataVisualization.Charting.Chart()
        Chart3 = New DataVisualization.Charting.Chart()
        Chart4 = New DataVisualization.Charting.Chart()
        Button1 = New Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        CType(Chart1, ComponentModel.ISupportInitialize).BeginInit()
        CType(Chart2, ComponentModel.ISupportInitialize).BeginInit()
        CType(Chart3, ComponentModel.ISupportInitialize).BeginInit()
        CType(Chart4, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(12, 25)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(256, 159)
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 7)
        Label1.Name = "Label1"
        Label1.Size = New Size(32, 15)
        Label1.TabIndex = 1
        Label1.Text = "Time"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(274, 345)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(386, 23)
        TextBox1.TabIndex = 3
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(12, 407)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(254, 23)
        TextBox2.TabIndex = 4
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Location = New Point(274, 25)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(199, 241)
        PictureBox2.TabIndex = 6
        PictureBox2.TabStop = False
        ' 
        ' PictureBox3
        ' 
        PictureBox3.Location = New Point(479, 25)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(181, 241)
        PictureBox3.TabIndex = 6
        PictureBox3.TabStop = False
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(10, 190)
        TextBox3.Multiline = True
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(256, 211)
        TextBox3.TabIndex = 8
        TextBox3.TextAlign = HorizontalAlignment.Center
        ' 
        ' Chart1
        ' 
        ChartArea1.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Chart1.Legends.Add(Legend1)
        Chart1.Location = New Point(996, 190)
        Chart1.Margin = New Padding(3, 2, 3, 2)
        Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Chart1.Series.Add(Series1)
        Chart1.Size = New Size(325, 157)
        Chart1.TabIndex = 9
        Chart1.Text = "Chart1"
        ' 
        ' Chart2
        ' 
        ChartArea2.Name = "ChartArea1"
        Chart2.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Chart2.Legends.Add(Legend2)
        Chart2.Location = New Point(665, 190)
        Chart2.Margin = New Padding(3, 2, 3, 2)
        Chart2.Name = "Chart2"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Chart2.Series.Add(Series2)
        Chart2.Size = New Size(325, 157)
        Chart2.TabIndex = 10
        Chart2.Text = "Chart2"
        ' 
        ' Chart3
        ' 
        ChartArea3.Name = "ChartArea1"
        Chart3.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Chart3.Legends.Add(Legend3)
        Chart3.Location = New Point(665, 351)
        Chart3.Margin = New Padding(3, 2, 3, 2)
        Chart3.Name = "Chart3"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Chart3.Series.Add(Series3)
        Chart3.Size = New Size(325, 157)
        Chart3.TabIndex = 11
        Chart3.Text = "Chart3"
        ' 
        ' Chart4
        ' 
        ChartArea4.Name = "ChartArea1"
        Chart4.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Chart4.Legends.Add(Legend4)
        Chart4.Location = New Point(996, 351)
        Chart4.Margin = New Padding(3, 2, 3, 2)
        Chart4.Name = "Chart4"
        Series4.ChartArea = "ChartArea1"
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        Chart4.Series.Add(Series4)
        Chart4.Size = New Size(325, 157)
        Chart4.TabIndex = 12
        Chart4.Text = "Chart4"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(1198, 7)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 13
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1330, 550)
        Controls.Add(Button1)
        Controls.Add(Chart4)
        Controls.Add(Chart3)
        Controls.Add(Chart2)
        Controls.Add(Chart1)
        Controls.Add(TextBox3)
        Controls.Add(PictureBox3)
        Controls.Add(PictureBox2)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        Controls.Add(PictureBox1)
        Name = "Form1"
        Text = "UI"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        CType(Chart1, ComponentModel.ISupportInitialize).EndInit()
        CType(Chart2, ComponentModel.ISupportInitialize).EndInit()
        CType(Chart3, ComponentModel.ISupportInitialize).EndInit()
        CType(Chart4, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
    Friend WithEvents Chart3 As DataVisualization.Charting.Chart
    Friend WithEvents Chart4 As DataVisualization.Charting.Chart
    Friend WithEvents Button1 As Button
End Class
