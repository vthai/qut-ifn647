namespace Week8Challenge
{
    partial class Form1
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
            this.search = new System.Windows.Forms.Label();
            this.suggest = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.suggestionBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // search
            // 
            this.search.AutoSize = true;
            this.search.Location = new System.Drawing.Point(60, 106);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(91, 20);
            this.search.TabIndex = 0;
            this.search.Text = "Search Box";
            // 
            // suggest
            // 
            this.suggest.AutoSize = true;
            this.suggest.Location = new System.Drawing.Point(64, 235);
            this.suggest.Name = "suggest";
            this.suggest.Size = new System.Drawing.Size(121, 20);
            this.suggest.TabIndex = 1;
            this.suggest.Text = "Suggestion Box";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(251, 106);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(169, 26);
            this.searchBox.TabIndex = 2;
            this.searchBox.Click += new System.EventHandler(this.suggestionBox_SelectedIndexChanged);
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // suggestionBox
            // 
            this.suggestionBox.FormattingEnabled = true;
            this.suggestionBox.ItemHeight = 20;
            this.suggestionBox.Location = new System.Drawing.Point(251, 260);
            this.suggestionBox.Name = "suggestionBox";
            this.suggestionBox.Size = new System.Drawing.Size(273, 144);
            this.suggestionBox.TabIndex = 4;
            this.suggestionBox.SelectedIndexChanged += new System.EventHandler(this.suggestionBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 588);
            this.Controls.Add(this.suggestionBox);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.suggest);
            this.Controls.Add(this.search);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label search;
        private System.Windows.Forms.Label suggest;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ListBox suggestionBox;
    }
}

