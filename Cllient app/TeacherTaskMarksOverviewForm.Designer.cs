
namespace Cllient_app
{
    partial class TeacherTaskMarksOverviewForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.marksGridView = new System.Windows.Forms.DataGridView();
            this.removeMarkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.marksGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Group:";
            // 
            // groupComboBox
            // 
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.IntegralHeight = false;
            this.groupComboBox.Location = new System.Drawing.Point(57, 6);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(121, 21);
            this.groupComboBox.TabIndex = 7;
            this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
            // 
            // marksGridView
            // 
            this.marksGridView.AllowUserToAddRows = false;
            this.marksGridView.AllowUserToDeleteRows = false;
            this.marksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.marksGridView.Location = new System.Drawing.Point(15, 33);
            this.marksGridView.Name = "marksGridView";
            this.marksGridView.Size = new System.Drawing.Size(773, 405);
            this.marksGridView.TabIndex = 9;
            this.marksGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.marksGridView_CellEndEdit);
            this.marksGridView.SelectionChanged += new System.EventHandler(this.marksGridView_SelectionChanged);
            // 
            // removeMarkButton
            // 
            this.removeMarkButton.Location = new System.Drawing.Point(704, 4);
            this.removeMarkButton.Name = "removeMarkButton";
            this.removeMarkButton.Size = new System.Drawing.Size(84, 23);
            this.removeMarkButton.TabIndex = 10;
            this.removeMarkButton.Text = "Remove mark";
            this.removeMarkButton.UseVisualStyleBackColor = true;
            this.removeMarkButton.Click += new System.EventHandler(this.removeMarkButton_Click);
            // 
            // TeacherTaskMarksOverviewForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.removeMarkButton);
            this.Controls.Add(this.marksGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupComboBox);
            this.Name = "TeacherTaskMarksOverviewForm";
            this.Text = "Marks";
            ((System.ComponentModel.ISupportInitialize)(this.marksGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.DataGridView marksGridView;
        private System.Windows.Forms.Button removeMarkButton;
    }
}