
namespace Cllient_app
{
    partial class StudentsTaskOverviewForm
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
            this.reportsGridView = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.issuesGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.reportsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.issuesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // reportsGridView
            // 
            this.reportsGridView.AllowUserToDeleteRows = false;
            this.reportsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportsGridView.Location = new System.Drawing.Point(12, 29);
            this.reportsGridView.Name = "reportsGridView";
            this.reportsGridView.Size = new System.Drawing.Size(776, 87);
            this.reportsGridView.TabIndex = 0;
            this.reportsGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.reportsGridView_CellBeginEdit);
            this.reportsGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.reportsGridView_CellEndEdit);
            this.reportsGridView.SelectionChanged += new System.EventHandler(this.reportsGridView_SelectionChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Reports";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Issues";
            // 
            // issuesGridView
            // 
            this.issuesGridView.AllowUserToAddRows = false;
            this.issuesGridView.AllowUserToDeleteRows = false;
            this.issuesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.issuesGridView.Location = new System.Drawing.Point(12, 153);
            this.issuesGridView.Name = "issuesGridView";
            this.issuesGridView.ReadOnly = true;
            this.issuesGridView.Size = new System.Drawing.Size(776, 285);
            this.issuesGridView.TabIndex = 3;
            // 
            // StudentsTaskOverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.issuesGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.reportsGridView);
            this.Name = "StudentsTaskOverviewForm";
            this.Text = "Task";
            ((System.ComponentModel.ISupportInitialize)(this.reportsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.issuesGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView reportsGridView;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView issuesGridView;
    }
}