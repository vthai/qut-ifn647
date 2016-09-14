namespace TestGUI
{
    partial class GUIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NextLabel = new System.Windows.Forms.Label();
            this.NextButton = new System.Windows.Forms.Button();
            this.TextEnter = new System.Windows.Forms.TextBox();
            this.TextShowEnter = new System.Windows.Forms.Label();
            this.TextShowChange = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ClickLable2 = new System.Windows.Forms.Label();
            this.ClickLable3 = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.Savebutton = new System.Windows.Forms.Button();
            this.ClickLable1 = new System.Windows.Forms.Label();
            this.BrowseLable = new System.Windows.Forms.Label();
            this.saveDirectory = new System.Windows.Forms.Label();
            this.TextButton = new System.Windows.Forms.Button();
            this.MyfolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // NextLabel
            // 
            this.NextLabel.AutoSize = true;
            this.NextLabel.Location = new System.Drawing.Point(98, 37);
            this.NextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NextLabel.Name = "NextLabel";
            this.NextLabel.Size = new System.Drawing.Size(57, 20);
            this.NextLabel.TabIndex = 0;
            this.NextLabel.Text = "Label1";
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(467, 30);
            this.NextButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(112, 35);
            this.NextButton.TabIndex = 1;
            this.NextButton.Text = "Get next line";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // TextEnter
            // 
            this.TextEnter.Location = new System.Drawing.Point(102, 76);
            this.TextEnter.Name = "TextEnter";
            this.TextEnter.Size = new System.Drawing.Size(331, 26);
            this.TextEnter.TabIndex = 2;
            this.TextEnter.TextChanged += new System.EventHandler(this.TextEnter_TextChanged);
            // 
            // TextShowEnter
            // 
            this.TextShowEnter.AutoSize = true;
            this.TextShowEnter.Location = new System.Drawing.Point(102, 126);
            this.TextShowEnter.Name = "TextShowEnter";
            this.TextShowEnter.Size = new System.Drawing.Size(142, 20);
            this.TextShowEnter.TabIndex = 3;
            this.TextShowEnter.Text = "Show text on enter";
            // 
            // TextShowChange
            // 
            this.TextShowChange.AutoSize = true;
            this.TextShowChange.Location = new System.Drawing.Point(102, 163);
            this.TextShowChange.Name = "TextShowChange";
            this.TextShowChange.Size = new System.Drawing.Size(159, 20);
            this.TextShowChange.TabIndex = 4;
            this.TextShowChange.Text = "show text on change ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // ClickLable2
            // 
            this.ClickLable2.AutoSize = true;
            this.ClickLable2.Location = new System.Drawing.Point(224, 239);
            this.ClickLable2.Name = "ClickLable2";
            this.ClickLable2.Size = new System.Drawing.Size(104, 20);
            this.ClickLable2.TabIndex = 5;
            this.ClickLable2.Text = "Click me for 2";
            this.ClickLable2.Click += new System.EventHandler(this.ClickLable2_Click);
            // 
            // ClickLable3
            // 
            this.ClickLable3.AutoSize = true;
            this.ClickLable3.Location = new System.Drawing.Point(372, 239);
            this.ClickLable3.Name = "ClickLable3";
            this.ClickLable3.Size = new System.Drawing.Size(104, 20);
            this.ClickLable3.TabIndex = 5;
            this.ClickLable3.Text = "Click me for 3";
            this.ClickLable3.Click += new System.EventHandler(this.ClickLable3_Click);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(102, 326);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(79, 35);
            this.BrowseButton.TabIndex = 6;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // Savebutton
            // 
            this.Savebutton.Location = new System.Drawing.Point(106, 386);
            this.Savebutton.Name = "Savebutton";
            this.Savebutton.Size = new System.Drawing.Size(75, 34);
            this.Savebutton.TabIndex = 6;
            this.Savebutton.Text = "save";
            this.Savebutton.UseVisualStyleBackColor = true;
            this.Savebutton.Click += new System.EventHandler(this.Savebutton_Click);
            // 
            // ClickLable1
            // 
            this.ClickLable1.AutoSize = true;
            this.ClickLable1.Location = new System.Drawing.Point(102, 239);
            this.ClickLable1.Name = "ClickLable1";
            this.ClickLable1.Size = new System.Drawing.Size(108, 20);
            this.ClickLable1.TabIndex = 5;
            this.ClickLable1.Text = "Click me for 1 ";
            this.ClickLable1.Click += new System.EventHandler(this.ClickLable1_Click);
            // 
            // BrowseLable
            // 
            this.BrowseLable.AutoSize = true;
            this.BrowseLable.Location = new System.Drawing.Point(205, 326);
            this.BrowseLable.Name = "BrowseLable";
            this.BrowseLable.Size = new System.Drawing.Size(129, 20);
            this.BrowseLable.TabIndex = 5;
            this.BrowseLable.Text = "Browse Directory";
            // 
            // saveDirectory
            // 
            this.saveDirectory.AutoSize = true;
            this.saveDirectory.Location = new System.Drawing.Point(205, 386);
            this.saveDirectory.Name = "saveDirectory";
            this.saveDirectory.Size = new System.Drawing.Size(112, 20);
            this.saveDirectory.TabIndex = 5;
            this.saveDirectory.Text = "Save Directory";
            this.saveDirectory.Click += new System.EventHandler(this.label8_Click);
            // 
            // TextButton
            // 
            this.TextButton.Location = new System.Drawing.Point(467, 76);
            this.TextButton.Name = "TextButton";
            this.TextButton.Size = new System.Drawing.Size(112, 33);
            this.TextButton.TabIndex = 6;
            this.TextButton.Text = "Enter";
            this.TextButton.UseVisualStyleBackColor = true;
            this.TextButton.Click += new System.EventHandler(this.TextButton_Click);
            // 
            // GUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 533);
            this.Controls.Add(this.Savebutton);
            this.Controls.Add(this.TextButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.ClickLable3);
            this.Controls.Add(this.saveDirectory);
            this.Controls.Add(this.BrowseLable);
            this.Controls.Add(this.ClickLable1);
            this.Controls.Add(this.ClickLable2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextShowChange);
            this.Controls.Add(this.TextShowEnter);
            this.Controls.Add(this.TextEnter);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.NextLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GUIForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NextLabel;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.TextBox TextEnter;
        private System.Windows.Forms.Label TextShowEnter;
        private System.Windows.Forms.Label TextShowChange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ClickLable2;
        private System.Windows.Forms.Label ClickLable3;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button Savebutton;
        private System.Windows.Forms.Label ClickLable1;
        private System.Windows.Forms.Label BrowseLable;
        private System.Windows.Forms.Label saveDirectory;
        private System.Windows.Forms.Button TextButton;
        private System.Windows.Forms.FolderBrowserDialog MyfolderBrowserDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

