﻿namespace Vision_API
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
            lblpg = new Label();
            label1 = new Label();
            listBox1 = new ListBox();
            label2 = new Label();
            lstPeople = new ListBox();
            cmdAddFace = new Button();
            txtFace = new TextBox();
            cmdAddPerson = new Button();
            lblPersonGroup = new Label();
            pictureBox1 = new PictureBox();
            cmdAnalyze = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblpg
            // 
            lblpg.AutoSize = true;
            lblpg.Location = new Point(29, 25);
            lblpg.Name = "lblpg";
            lblpg.Size = new Size(82, 15);
            lblpg.TabIndex = 0;
            lblpg.Text = "Person Group:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(407, 51);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 2;
            label1.Text = "Images";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(407, 69);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(331, 274);
            listBox1.TabIndex = 3;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 149);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 4;
            label2.Text = "People";
            // 
            // lstPeople
            // 
            lstPeople.FormattingEnabled = true;
            lstPeople.ItemHeight = 15;
            lstPeople.Location = new Point(29, 167);
            lstPeople.Name = "lstPeople";
            lstPeople.Size = new Size(131, 154);
            lstPeople.TabIndex = 5;
            lstPeople.SelectedIndexChanged += lstPeople_SelectedIndexChanged;
            // 
            // cmdAddFace
            // 
            cmdAddFace.Enabled = false;
            cmdAddFace.Location = new Point(14, 365);
            cmdAddFace.Name = "cmdAddFace";
            cmdAddFace.Size = new Size(137, 26);
            cmdAddFace.TabIndex = 7;
            cmdAddFace.Text = "Add Face";
            cmdAddFace.UseVisualStyleBackColor = true;
            cmdAddFace.Click += cmdAddFace_Click;
            // 
            // txtFace
            // 
            txtFace.Location = new Point(29, 78);
            txtFace.Name = "txtFace";
            txtFace.Size = new Size(100, 23);
            txtFace.TabIndex = 8;
            txtFace.TextChanged += txtFace_TextChanged;
            // 
            // cmdAddPerson
            // 
            cmdAddPerson.Enabled = false;
            cmdAddPerson.Location = new Point(24, 108);
            cmdAddPerson.Name = "cmdAddPerson";
            cmdAddPerson.Size = new Size(105, 21);
            cmdAddPerson.TabIndex = 9;
            cmdAddPerson.Text = "Add Person";
            cmdAddPerson.UseVisualStyleBackColor = true;
            cmdAddPerson.Click += cmdAddPerson_Click;
            // 
            // lblPersonGroup
            // 
            lblPersonGroup.AutoSize = true;
            lblPersonGroup.Location = new Point(122, 25);
            lblPersonGroup.Name = "lblPersonGroup";
            lblPersonGroup.Size = new Size(38, 15);
            lblPersonGroup.TabIndex = 10;
            lblPersonGroup.Text = "label3";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(407, 365);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(244, 189);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // cmdAnalyze
            // 
            cmdAnalyze.Location = new Point(666, 362);
            cmdAnalyze.Name = "cmdAnalyze";
            cmdAnalyze.Size = new Size(74, 29);
            cmdAnalyze.TabIndex = 12;
            cmdAnalyze.Text = "Analyze";
            cmdAnalyze.UseVisualStyleBackColor = true;
            cmdAnalyze.Click += cmdAnalyze_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(862, 558);
            Controls.Add(cmdAnalyze);
            Controls.Add(pictureBox1);
            Controls.Add(lblPersonGroup);
            Controls.Add(cmdAddPerson);
            Controls.Add(txtFace);
            Controls.Add(cmdAddFace);
            Controls.Add(lstPeople);
            Controls.Add(label2);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Controls.Add(lblpg);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblpg;
        private Label label1;
        private ListBox listBox1;
        private Label label2;
        private ListBox lstPeople;
        private Button cmdAddFace;
        private TextBox txtFace;
        private Button cmdAddPerson;
        private Label lblPersonGroup;
        private PictureBox pictureBox1;
        private Button cmdAnalyze;
    }
}
