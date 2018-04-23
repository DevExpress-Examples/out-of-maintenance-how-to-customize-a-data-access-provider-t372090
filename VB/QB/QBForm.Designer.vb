Namespace QB
    Partial Public Class QBForm
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.buttonsRunQb = New DevExpress.XtraEditors.SimpleButton()
            Me.SuspendLayout()
            ' 
            ' buttonsRunQb
            ' 
            Me.buttonsRunQb.Location = New System.Drawing.Point(12, 12)
            Me.buttonsRunQb.Name = "buttonsRunQb"
            Me.buttonsRunQb.Size = New System.Drawing.Size(166, 34)
            Me.buttonsRunQb.TabIndex = 0
            Me.buttonsRunQb.Text = "Run QB"
            ' 
            ' QBForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(190, 58)
            Me.Controls.Add(Me.buttonsRunQb)
            Me.Name = "QBForm"
            Me.Text = "QbForm"
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private WithEvents buttonsRunQb As DevExpress.XtraEditors.SimpleButton
    End Class
End Namespace

