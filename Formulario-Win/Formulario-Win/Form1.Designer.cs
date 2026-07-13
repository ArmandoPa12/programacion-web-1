namespace Formulario_Win
{
    partial class Form1
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
            richTextBox1 = new RichTextBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            btnCalcular = new Button();
            txtbox1 = new TextBox();
            txtbox2 = new TextBox();
            button2 = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            maskedTextBox1 = new MaskedTextBox();
            menuStrip1 = new MenuStrip();
            opcion1ToolStripMenuItem = new ToolStripMenuItem();
            radioButton3 = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(434, 37);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(210, 145);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(6, 22);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(103, 19);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "Contar vocales";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(6, 59);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(108, 19);
            radioButton2.TabIndex = 2;
            radioButton2.TabStop = true;
            radioButton2.Text = "Contar palabras";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(57, 196);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(75, 23);
            btnCalcular.TabIndex = 3;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // txtbox1
            // 
            txtbox1.Location = new Point(6, 22);
            txtbox1.Name = "txtbox1";
            txtbox1.Size = new Size(147, 23);
            txtbox1.TabIndex = 6;
            txtbox1.KeyPress += textBox1_KeyPress;
            // 
            // txtbox2
            // 
            txtbox2.Location = new Point(6, 22);
            txtbox2.Name = "txtbox2";
            txtbox2.Size = new Size(147, 23);
            txtbox2.TabIndex = 7;
            txtbox2.KeyPress += textBox2_KeyPress;
            // 
            // button2
            // 
            button2.Location = new Point(492, 292);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "Ejecutar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtbox1);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(51, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(176, 60);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ingresar un numero";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtbox2);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(51, 114);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(176, 60);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ingresar un numero";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(radioButton1);
            groupBox3.Controls.Add(radioButton2);
            groupBox3.Location = new Point(458, 196);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(157, 90);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Operaciones";
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(309, 25);
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(100, 23);
            maskedTextBox1.TabIndex = 13;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { opcion1ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // opcion1ToolStripMenuItem
            // 
            opcion1ToolStripMenuItem.Name = "opcion1ToolStripMenuItem";
            opcion1ToolStripMenuItem.Size = new Size(62, 20);
            opcion1ToolStripMenuItem.Text = "opcion1";
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(327, 226);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(94, 19);
            radioButton3.TabIndex = 15;
            radioButton3.TabStop = true;
            radioButton3.Text = "radioButton3";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(radioButton3);
            Controls.Add(maskedTextBox1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(btnCalcular);
            Controls.Add(richTextBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Button btnCalcular;
        private TextBox txtbox1;
        private TextBox txtbox2;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private MaskedTextBox maskedTextBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem opcion1ToolStripMenuItem;
        private RadioButton radioButton3;
    }
}
