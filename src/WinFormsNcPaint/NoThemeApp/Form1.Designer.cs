namespace NoThemeApp
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node14");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node15");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node16");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node5", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node17");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node18");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Root", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode9,
            treeNode12,
            treeNode15,
            treeNode18});
            this.btnDisableEnableTheme = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDisableEnableTheme
            // 
            this.btnDisableEnableTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisableEnableTheme.Location = new System.Drawing.Point(90, 297);
            this.btnDisableEnableTheme.Name = "btnDisableEnableTheme";
            this.btnDisableEnableTheme.Size = new System.Drawing.Size(214, 26);
            this.btnDisableEnableTheme.TabIndex = 12;
            this.btnDisableEnableTheme.Text = "Disable Theme";
            this.btnDisableEnableTheme.UseVisualStyleBackColor = true;
            this.btnDisableEnableTheme.Click += new System.EventHandler(this.btnDisableEnableTheme_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox4);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(203, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 115);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Checkboxes";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Location = new System.Drawing.Point(6, 88);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(80, 17);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "checkBox1";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(6, 65);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(83, 17);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "checkBox 3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 42);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(83, 17);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "checkBox 2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(83, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox 1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 115);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Radiobuttons";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Enabled = false;
            this.radioButton4.Location = new System.Drawing.Point(6, 88);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(88, 17);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.Text = "radioButton 4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Location = new System.Drawing.Point(6, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(88, 17);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "radioButton 3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(88, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "radioButton 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 147);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node7";
            treeNode1.Text = "Node7";
            treeNode2.Name = "Node8";
            treeNode2.Text = "Node8";
            treeNode3.Name = "Node1";
            treeNode3.Text = "Node1";
            treeNode4.Name = "Node9";
            treeNode4.Text = "Node9";
            treeNode5.Name = "Node10";
            treeNode5.Text = "Node10";
            treeNode6.Name = "Node2";
            treeNode6.Text = "Node2";
            treeNode7.Name = "Node11";
            treeNode7.Text = "Node11";
            treeNode8.Name = "Node12";
            treeNode8.Text = "Node12";
            treeNode9.Name = "Node3";
            treeNode9.Text = "Node3";
            treeNode10.Name = "Node13";
            treeNode10.Text = "Node13";
            treeNode11.Name = "Node14";
            treeNode11.Text = "Node14";
            treeNode12.Name = "Node4";
            treeNode12.Text = "Node4";
            treeNode13.Name = "Node15";
            treeNode13.Text = "Node15";
            treeNode14.Name = "Node16";
            treeNode14.Text = "Node16";
            treeNode15.Name = "Node5";
            treeNode15.Text = "Node5";
            treeNode16.Name = "Node17";
            treeNode16.Text = "Node17";
            treeNode17.Name = "Node18";
            treeNode17.Text = "Node18";
            treeNode18.Name = "Node6";
            treeNode18.Text = "Node6";
            treeNode19.Name = "Node0";
            treeNode19.Text = "Root";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19});
            this.treeView1.Size = new System.Drawing.Size(359, 131);
            this.treeView1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.btnDisableEnableTheme);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.Text = "No Theme App";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDisableEnableTheme;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TreeView treeView1;
    }
}

