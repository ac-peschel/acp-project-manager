namespace acProj
{
    partial class Wnd
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(25, 24, 40);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(137, 180, 250);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(319, 50);
            label1.TabIndex = 0;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.TextChanged += label1_TextChanged;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(319, 584);
            panel1.TabIndex = 1;
            // 
            // Wnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BackColor = Color.FromArgb(30, 30, 46);
            ClientSize = new Size(319, 634);
            Controls.Add(panel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Wnd";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ACProj";
            TopMost = true;
            Deactivate += Wnd_Deactivate;
            KeyDown += Wnd_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel panel1;
    }
}
