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
        PictureBox1.Location = New Point(14, 33)
        PictureBox1.Margin = New Padding(3, 4, 3, 4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(293, 212)
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(42, 20)
        Label1.TabIndex = 1
        Label1.Text = "Time"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(313, 460)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(441, 27)
        TextBox1.TabIndex = 3
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(14, 543)
        TextBox2.Margin = New Padding(3, 4, 3, 4)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(290, 27)
        TextBox2.TabIndex = 4
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Location = New Point(313, 33)
        PictureBox2.Margin = New Padding(3, 4, 3, 4)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(227, 321)
        PictureBox2.TabIndex = 6
        PictureBox2.TabStop = False
        ' 
        ' PictureBox3
        ' 
        PictureBox3.Location = New Point(547, 33)
        PictureBox3.Margin = New Padding(3, 4, 3, 4)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(207, 321)
        PictureBox3.TabIndex = 6
        PictureBox3.TabStop = False
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(14, 253)
        TextBox3.Margin = New Padding(3, 4, 3, 4)
        TextBox3.Multiline = True
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(292, 280)
        TextBox3.TabIndex = 8
        TextBox3.TextAlign = HorizontalAlignment.Center
        ' 
        ' Chart1
        ' 
        ChartArea1.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Chart1.Legends.Add(Legend1)
        Chart1.Location = New Point(760, 33)
        Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Chart1.Series.Add(Series1)
        Chart1.Size = New Size(371, 209)
        Chart1.TabIndex = 9
        Chart1.Text = "Chart1"
        ' 
        ' Chart2
        ' 
        ChartArea2.Name = "ChartArea1"
        Chart2.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Chart2.Legends.Add(Legend2)
        Chart2.Location = New Point(760, 253)
        Chart2.Name = "Chart2"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Chart2.Series.Add(Series2)
        Chart2.Size = New Size(371, 209)
        Chart2.TabIndex = 10
        Chart2.Text = "Chart2"
        ' 
        ' Chart3
        ' 
        ChartArea3.Name = "ChartArea1"
        Chart3.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Chart3.Legends.Add(Legend3)
        Chart3.Location = New Point(760, 468)
        Chart3.Name = "Chart3"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Chart3.Series.Add(Series3)
        Chart3.Size = New Size(371, 209)
        Chart3.TabIndex = 11
        Chart3.Text = "Chart3"
        ' 
        ' Chart4
        ' 
        ChartArea4.Name = "ChartArea1"
        Chart4.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Chart4.Legends.Add(Legend4)
        Chart4.Location = New Point(1137, 36)
        Chart4.Name = "Chart4"
        Series4.ChartArea = "ChartArea1"
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        Chart4.Series.Add(Series4)
        Chart4.Size = New Size(371, 209)
        Chart4.TabIndex = 12
        Chart4.Text = "Chart4"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1671, 734)
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
        Margin = New Padding(3, 4, 3, 4)
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
End Class
