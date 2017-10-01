namespace KartonKrieger
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
            this.ActiveCharacter = new System.Windows.Forms.Label();
            this.EndRound = new System.Windows.Forms.Button();
            this.GoNorth = new System.Windows.Forms.Button();
            this.GoWest = new System.Windows.Forms.Button();
            this.GoSouth = new System.Windows.Forms.Button();
            this.GoEast = new System.Windows.Forms.Button();
            this.ActionPoints = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Attack1 = new System.Windows.Forms.Button();
            this.Attack2 = new System.Windows.Forms.Button();
            this.Attack3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ActiveCharacter
            // 
            this.ActiveCharacter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActiveCharacter.Location = new System.Drawing.Point(13, 13);
            this.ActiveCharacter.Name = "ActiveCharacter";
            this.ActiveCharacter.Size = new System.Drawing.Size(100, 23);
            this.ActiveCharacter.TabIndex = 0;
            this.ActiveCharacter.Text = "???";
            // 
            // EndRound
            // 
            this.EndRound.Location = new System.Drawing.Point(13, 530);
            this.EndRound.Name = "EndRound";
            this.EndRound.Size = new System.Drawing.Size(100, 23);
            this.EndRound.TabIndex = 1;
            this.EndRound.Text = "Runde beenden";
            this.EndRound.UseVisualStyleBackColor = true;
            this.EndRound.Click += new System.EventHandler(this.EndRound_Click);
            // 
            // GoNorth
            // 
            this.GoNorth.Location = new System.Drawing.Point(47, 351);
            this.GoNorth.Name = "GoNorth";
            this.GoNorth.Size = new System.Drawing.Size(25, 25);
            this.GoNorth.TabIndex = 2;
            this.GoNorth.Text = "↑";
            this.GoNorth.UseVisualStyleBackColor = true;
            this.GoNorth.Click += new System.EventHandler(this.GoNorth_Click);
            // 
            // GoWest
            // 
            this.GoWest.Location = new System.Drawing.Point(16, 382);
            this.GoWest.Name = "GoWest";
            this.GoWest.Size = new System.Drawing.Size(25, 25);
            this.GoWest.TabIndex = 3;
            this.GoWest.Text = "←";
            this.GoWest.UseVisualStyleBackColor = true;
            this.GoWest.Click += new System.EventHandler(this.GoWest_Click);
            // 
            // GoSouth
            // 
            this.GoSouth.Location = new System.Drawing.Point(47, 413);
            this.GoSouth.Name = "GoSouth";
            this.GoSouth.Size = new System.Drawing.Size(25, 25);
            this.GoSouth.TabIndex = 4;
            this.GoSouth.Text = "↓";
            this.GoSouth.UseVisualStyleBackColor = true;
            this.GoSouth.Click += new System.EventHandler(this.GoSouth_Click);
            // 
            // GoEast
            // 
            this.GoEast.Location = new System.Drawing.Point(77, 382);
            this.GoEast.Name = "GoEast";
            this.GoEast.Size = new System.Drawing.Size(25, 25);
            this.GoEast.TabIndex = 5;
            this.GoEast.Text = "→";
            this.GoEast.UseVisualStyleBackColor = true;
            this.GoEast.Click += new System.EventHandler(this.GoEast_Click);
            // 
            // ActionPoints
            // 
            this.ActionPoints.Location = new System.Drawing.Point(12, 59);
            this.ActionPoints.Name = "ActionPoints";
            this.ActionPoints.Size = new System.Drawing.Size(100, 23);
            this.ActionPoints.TabIndex = 6;
            this.ActionPoints.Text = "???";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Aktionspunkte";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Attack1
            // 
            this.Attack1.Location = new System.Drawing.Point(13, 125);
            this.Attack1.Name = "Attack1";
            this.Attack1.Size = new System.Drawing.Size(90, 48);
            this.Attack1.TabIndex = 8;
            this.Attack1.Text = "button1";
            this.Attack1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Attack1.UseVisualStyleBackColor = true;
            // 
            // Attack2
            // 
            this.Attack2.Location = new System.Drawing.Point(13, 179);
            this.Attack2.Name = "Attack2";
            this.Attack2.Size = new System.Drawing.Size(90, 48);
            this.Attack2.TabIndex = 9;
            this.Attack2.Text = "button1";
            this.Attack2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Attack2.UseVisualStyleBackColor = true;
            // 
            // Attack3
            // 
            this.Attack3.Location = new System.Drawing.Point(12, 233);
            this.Attack3.Name = "Attack3";
            this.Attack3.Size = new System.Drawing.Size(90, 48);
            this.Attack3.TabIndex = 10;
            this.Attack3.Text = "button1";
            this.Attack3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Attack3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 565);
            this.Controls.Add(this.Attack3);
            this.Controls.Add(this.Attack2);
            this.Controls.Add(this.Attack1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ActionPoints);
            this.Controls.Add(this.GoEast);
            this.Controls.Add(this.GoSouth);
            this.Controls.Add(this.GoWest);
            this.Controls.Add(this.GoNorth);
            this.Controls.Add(this.EndRound);
            this.Controls.Add(this.ActiveCharacter);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ActiveCharacter;
        private System.Windows.Forms.Button EndRound;
        private System.Windows.Forms.Button GoNorth;
        private System.Windows.Forms.Button GoWest;
        private System.Windows.Forms.Button GoSouth;
        private System.Windows.Forms.Button GoEast;
        private System.Windows.Forms.Label ActionPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Attack1;
        private System.Windows.Forms.Button Attack2;
        private System.Windows.Forms.Button Attack3;
    }
}

