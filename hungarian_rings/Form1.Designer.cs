using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace hungarian_rings
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
            this.Randomize = new System.Windows.Forms.Button();
            this.Move_number = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.IDA_star_button = new System.Windows.Forms.Button();
            this.Range_Max = new System.Windows.Forms.NumericUpDown();
            this.Range_Min = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Move_number)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Range_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Range_Min)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Randomize
            // 
            this.Randomize.Location = new System.Drawing.Point(51, 432);
            this.Randomize.Name = "Randomize";
            this.Randomize.Size = new System.Drawing.Size(119, 41);
            this.Randomize.TabIndex = 0;
            this.Randomize.Text = "Randomize";
            this.Randomize.UseVisualStyleBackColor = true;
            this.Randomize.Click += new System.EventHandler(this.Randomize_balls);
            // 
            // Move_number
            // 
            this.Move_number.Location = new System.Drawing.Point(194, 453);
            this.Move_number.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Move_number.Name = "Move_number";
            this.Move_number.Size = new System.Drawing.Size(70, 20);
            this.Move_number.TabIndex = 1;
            this.Move_number.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(194, 432);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(70, 13);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Moves to take";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(150, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Left Clockwise";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.B1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(330, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "Right Clockwise";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.B2);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(150, 380);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 30);
            this.button3.TabIndex = 5;
            this.button3.Text = "Left Counter-clockwise";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.B3);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(330, 380);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 30);
            this.button4.TabIndex = 6;
            this.button4.Text = "Right Counter-clockwise";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.B4);
            // 
            // IDA_star_button
            // 
            this.IDA_star_button.Location = new System.Drawing.Point(330, 432);
            this.IDA_star_button.Name = "IDA_star_button";
            this.IDA_star_button.Size = new System.Drawing.Size(93, 41);
            this.IDA_star_button.TabIndex = 7;
            this.IDA_star_button.Text = "Solve with IDA*";
            this.IDA_star_button.UseVisualStyleBackColor = true;
            this.IDA_star_button.Click += new System.EventHandler(this.run_IDA_star);
            // 
            // Range_Max
            // 
            this.Range_Max.Location = new System.Drawing.Point(429, 434);
            this.Range_Max.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Range_Max.Name = "Range_Max";
            this.Range_Max.Size = new System.Drawing.Size(57, 20);
            this.Range_Max.TabIndex = 8;
            this.Range_Max.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Range_Min
            // 
            this.Range_Min.Location = new System.Drawing.Point(429, 455);
            this.Range_Min.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.Range_Min.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Range_Min.Name = "Range_Min";
            this.Range_Min.Size = new System.Drawing.Size(57, 20);
            this.Range_Min.TabIndex = 9;
            this.Range_Min.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(492, 436);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(147, 13);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "Max Random Steps";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(492, 455);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(147, 13);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "Min Random Steps";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(644, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "Reset Puzzle";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 502);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Range_Min);
            this.Controls.Add(this.Range_Max);
            this.Controls.Add(this.IDA_star_button);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Move_number);
            this.Controls.Add(this.Randomize);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Hungarian Rings";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Move_number)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Range_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Range_Min)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Randomize;
        private System.Windows.Forms.NumericUpDown Move_number;
        private System.Windows.Forms.TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button IDA_star_button;
        private NumericUpDown Range_Max;
        private NumericUpDown Range_Min;
        private TextBox textBox2;
        private TextBox textBox3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;


    }
}

