namespace Vision_API
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
            button1 = new Button();
            lblStatus = new Label();
            txtAnalysis = new TextBox();
            txtPersonGroup = new TextBox();
            cmdAddPersonGroup = new Button();
            label3 = new Label();
            lstPeronGroups = new ListBox();
            txtImages = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblpg
            // 
            lblpg.AutoSize = true;
            lblpg.Location = new Point(161, 37);
            lblpg.Name = "lblpg";
            lblpg.Size = new Size(74, 15);
            lblpg.TabIndex = 0;
            lblpg.Text = "Image folder";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(488, 52);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 2;
            label1.Text = "Images";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(488, 67);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(167, 259);
            listBox1.TabIndex = 3;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(272, 154);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 4;
            label2.Text = "People";
            // 
            // lstPeople
            // 
            lstPeople.FormattingEnabled = true;
            lstPeople.ItemHeight = 15;
            lstPeople.Location = new Point(272, 172);
            lstPeople.Name = "lstPeople";
            lstPeople.Size = new Size(142, 154);
            lstPeople.TabIndex = 5;
            lstPeople.SelectedIndexChanged += lstPeople_SelectedIndexChanged;
            // 
            // cmdAddFace
            // 
            cmdAddFace.Enabled = false;
            cmdAddFace.Location = new Point(257, 370);
            cmdAddFace.Name = "cmdAddFace";
            cmdAddFace.Size = new Size(137, 26);
            cmdAddFace.TabIndex = 7;
            cmdAddFace.Text = "Add Face";
            cmdAddFace.UseVisualStyleBackColor = true;
            cmdAddFace.Click += cmdAddFace_Click;
            // 
            // txtFace
            // 
            txtFace.Location = new Point(272, 83);
            txtFace.Name = "txtFace";
            txtFace.Size = new Size(100, 23);
            txtFace.TabIndex = 8;
            txtFace.TextChanged += txtFace_TextChanged;
            // 
            // cmdAddPerson
            // 
            cmdAddPerson.Enabled = false;
            cmdAddPerson.Location = new Point(272, 112);
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
            lblPersonGroup.Location = new Point(113, 9);
            lblPersonGroup.Name = "lblPersonGroup";
            lblPersonGroup.Size = new Size(0, 15);
            lblPersonGroup.TabIndex = 10;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(699, 45);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(628, 536);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(488, 332);
            button1.Name = "button1";
            button1.Size = new Size(77, 43);
            button1.TabIndex = 13;
            button1.Text = "Analyze Image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(14, 719);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 15;
            lblStatus.Text = "label3";
            // 
            // txtAnalysis
            // 
            txtAnalysis.Enabled = false;
            txtAnalysis.Location = new Point(327, 471);
            txtAnalysis.Multiline = true;
            txtAnalysis.Name = "txtAnalysis";
            txtAnalysis.Size = new Size(366, 110);
            txtAnalysis.TabIndex = 16;
            // 
            // txtPersonGroup
            // 
            txtPersonGroup.Location = new Point(29, 67);
            txtPersonGroup.Name = "txtPersonGroup";
            txtPersonGroup.Size = new Size(122, 23);
            txtPersonGroup.TabIndex = 17;
            txtPersonGroup.TextChanged += txtPersonGroup_TextChanged;
            // 
            // cmdAddPersonGroup
            // 
            cmdAddPersonGroup.Enabled = false;
            cmdAddPersonGroup.Location = new Point(29, 112);
            cmdAddPersonGroup.Name = "cmdAddPersonGroup";
            cmdAddPersonGroup.Size = new Size(122, 22);
            cmdAddPersonGroup.TabIndex = 18;
            cmdAddPersonGroup.Text = "Add PersonGroup";
            cmdAddPersonGroup.UseVisualStyleBackColor = true;
            cmdAddPersonGroup.Click += cmdAddPersonGroup_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 154);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 19;
            label3.Text = "PersonGroups";
            // 
            // lstPeronGroups
            // 
            lstPeronGroups.FormattingEnabled = true;
            lstPeronGroups.ItemHeight = 15;
            lstPeronGroups.Location = new Point(27, 176);
            lstPeronGroups.Name = "lstPeronGroups";
            lstPeronGroups.Size = new Size(153, 139);
            lstPeronGroups.TabIndex = 20;
            lstPeronGroups.SelectedIndexChanged += lstPeronGroups_SelectedIndexChanged;
            lstPeronGroups.KeyDown += lstPeronGroups_KeyDown;
            lstPeronGroups.KeyPress += lstPeronGroups_KeyPress;
            // 
            // txtImages
            // 
            txtImages.Location = new Point(161, 66);
            txtImages.Name = "txtImages";
            txtImages.Size = new Size(71, 23);
            txtImages.TabIndex = 21;
            txtImages.TextChanged += txtImages_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 33);
            label4.Name = "label4";
            label4.Size = new Size(111, 15);
            label4.TabIndex = 22;
            label4.Text = "PersonGroup Name";
            label4.Click += label4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 743);
            Controls.Add(label4);
            Controls.Add(txtImages);
            Controls.Add(lstPeronGroups);
            Controls.Add(label3);
            Controls.Add(cmdAddPersonGroup);
            Controls.Add(txtPersonGroup);
            Controls.Add(txtAnalysis);
            Controls.Add(lblStatus);
            Controls.Add(button1);
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
        private Button button1;
        private Label lblStatus;
        private TextBox txtAnalysis;
        private TextBox txtPersonGroup;
        private Button cmdAddPersonGroup;
        private Label label3;
        private ListBox lstPeronGroups;
        private TextBox txtImages;
        private Label label4;
    }
}
