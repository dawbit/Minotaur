namespace Minotaur
{
    partial class Minimap2D
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
            this.startButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.lunch3DButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(272, 96);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(70, 70);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.Start_Click);
            // 
            // endButton
            // 
            this.endButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endButton.Location = new System.Drawing.Point(348, 96);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(70, 70);
            this.endButton.TabIndex = 1;
            this.endButton.Text = "End";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.End_Click);
            // 
            // lunch3DButton
            // 
            this.lunch3DButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lunch3DButton.Location = new System.Drawing.Point(663, 68);
            this.lunch3DButton.Name = "lunch3DButton";
            this.lunch3DButton.Size = new System.Drawing.Size(70, 70);
            this.lunch3DButton.TabIndex = 3;
            this.lunch3DButton.Text = "3D";
            this.lunch3DButton.UseVisualStyleBackColor = true;
            this.lunch3DButton.Click += new System.EventHandler(this.Lunch3DButton_Click);
            // 
            // Minimap2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lunch3DButton);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.startButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Minimap2D";
            this.Text = "Minimap2D";
            this.Click += new System.EventHandler(this.Minimap2D_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintGrid);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.Button lunch3DButton;
    }
}