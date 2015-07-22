namespace MonsterGulf
{
    partial class monstergulf_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(monstergulf_frm));
            this.execute_btn = new System.Windows.Forms.Button();
            this.open_folder_btn = new System.Windows.Forms.Button();
            this.exit_app_btn = new System.Windows.Forms.Button();
            this.pages_num = new System.Windows.Forms.Label();
            this.pages_num_textBox = new System.Windows.Forms.TextBox();
            this.url = new System.Windows.Forms.Label();
            this.web_url_textBox = new System.Windows.Forms.TextBox();
            this.job_type_comboBox = new System.Windows.Forms.ComboBox();
            this.job_type_label = new System.Windows.Forms.Label();
            this.region_label = new System.Windows.Forms.Label();
            this.region_comboBox = new System.Windows.Forms.ComboBox();
            this.result_textBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // execute_btn
            // 
            this.execute_btn.Location = new System.Drawing.Point(607, 474);
            this.execute_btn.MaximumSize = new System.Drawing.Size(300, 300);
            this.execute_btn.Name = "execute_btn";
            this.execute_btn.Size = new System.Drawing.Size(61, 24);
            this.execute_btn.TabIndex = 0;
            this.execute_btn.Text = "&Execute";
            this.execute_btn.UseVisualStyleBackColor = true;
            this.execute_btn.Click += new System.EventHandler(this.execute_btn_Click);
            // 
            // open_folder_btn
            // 
            this.open_folder_btn.AccessibleDescription = "";
            this.open_folder_btn.Location = new System.Drawing.Point(674, 474);
            this.open_folder_btn.MaximumSize = new System.Drawing.Size(300, 300);
            this.open_folder_btn.Name = "open_folder_btn";
            this.open_folder_btn.Size = new System.Drawing.Size(61, 24);
            this.open_folder_btn.TabIndex = 1;
            this.open_folder_btn.Tag = "";
            this.open_folder_btn.Text = "&Open";
            this.open_folder_btn.UseVisualStyleBackColor = true;
            this.open_folder_btn.Click += new System.EventHandler(this.open_folder_btn_Click);
            // 
            // exit_app_btn
            // 
            this.exit_app_btn.Location = new System.Drawing.Point(741, 474);
            this.exit_app_btn.MaximumSize = new System.Drawing.Size(300, 300);
            this.exit_app_btn.Name = "exit_app_btn";
            this.exit_app_btn.Size = new System.Drawing.Size(61, 24);
            this.exit_app_btn.TabIndex = 2;
            this.exit_app_btn.Text = "&Quit";
            this.exit_app_btn.UseVisualStyleBackColor = true;
            this.exit_app_btn.Click += new System.EventHandler(this.exit_app_btn_Click);
            // 
            // pages_num
            // 
            this.pages_num.AutoSize = true;
            this.pages_num.Location = new System.Drawing.Point(1, 477);
            this.pages_num.Name = "pages_num";
            this.pages_num.Size = new System.Drawing.Size(46, 13);
            this.pages_num.TabIndex = 3;
            this.pages_num.Text = "Page(s):";
            // 
            // pages_num_textBox
            // 
            this.pages_num_textBox.Location = new System.Drawing.Point(45, 474);
            this.pages_num_textBox.Name = "pages_num_textBox";
            this.pages_num_textBox.Size = new System.Drawing.Size(20, 20);
            this.pages_num_textBox.TabIndex = 4;
            this.pages_num_textBox.Text = "0";
            this.pages_num_textBox.TextChanged += new System.EventHandler(this.pages_num_textBox_TextChanged);
            // 
            // url
            // 
            this.url.AutoSize = true;
            this.url.Location = new System.Drawing.Point(1, 453);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(23, 13);
            this.url.TabIndex = 5;
            this.url.Text = "Url:";
            // 
            // web_url_textBox
            // 
            this.web_url_textBox.Location = new System.Drawing.Point(22, 450);
            this.web_url_textBox.Name = "web_url_textBox";
            this.web_url_textBox.Size = new System.Drawing.Size(780, 20);
            this.web_url_textBox.TabIndex = 6;
            this.web_url_textBox.Text = "http://jobsearch.monstergulf.com";
            this.web_url_textBox.TextChanged += new System.EventHandler(this.web_url_textBox_TextChanged);
            // 
            // job_type_comboBox
            // 
            this.job_type_comboBox.FormattingEnabled = true;
            this.job_type_comboBox.Items.AddRange(new object[] {
            "Admin/Secretarial",
            "Customer Service/ BPO/ KPO",
            "Finance & Accounts",
            "HR",
            "IT",
            "Legal",
            "Marketing",
            "Purchase & Supply Chain"});
            this.job_type_comboBox.Location = new System.Drawing.Point(147, 474);
            this.job_type_comboBox.Name = "job_type_comboBox";
            this.job_type_comboBox.Size = new System.Drawing.Size(152, 21);
            this.job_type_comboBox.TabIndex = 8;
            this.job_type_comboBox.Text = "IT";
            this.job_type_comboBox.SelectedIndexChanged += new System.EventHandler(this.job_type_comboBox_SelectedIndexChanged);
            // 
            // job_type_label
            // 
            this.job_type_label.AutoSize = true;
            this.job_type_label.Location = new System.Drawing.Point(90, 478);
            this.job_type_label.Name = "job_type_label";
            this.job_type_label.Size = new System.Drawing.Size(54, 13);
            this.job_type_label.TabIndex = 9;
            this.job_type_label.Text = "Job Type:";
            // 
            // region_label
            // 
            this.region_label.AutoSize = true;
            this.region_label.Location = new System.Drawing.Point(325, 478);
            this.region_label.Name = "region_label";
            this.region_label.Size = new System.Drawing.Size(44, 13);
            this.region_label.TabIndex = 10;
            this.region_label.Text = "Region:";
            // 
            // region_comboBox
            // 
            this.region_comboBox.FormattingEnabled = true;
            this.region_comboBox.Items.AddRange(new object[] {
            "United Arab Emirates",
            "Saudi Arabia",
            "Qatar",
            "Bahrain",
            "Oman",
            "Kuwait",
            "Egypt",
            "Iraq",
            "Jordan",
            "Lebanon"});
            this.region_comboBox.Location = new System.Drawing.Point(375, 474);
            this.region_comboBox.Name = "region_comboBox";
            this.region_comboBox.Size = new System.Drawing.Size(156, 21);
            this.region_comboBox.TabIndex = 11;
            this.region_comboBox.Text = "United Arab Emirates";
            this.region_comboBox.SelectedIndexChanged += new System.EventHandler(this.region_comboBox_SelectedIndexChanged);
            // 
            // result_textBox
            // 
            this.result_textBox.Location = new System.Drawing.Point(2, 3);
            this.result_textBox.Name = "result_textBox";
            this.result_textBox.Size = new System.Drawing.Size(800, 445);
            this.result_textBox.TabIndex = 12;
            this.result_textBox.Text = "Progress:";
            // 
            // monstergulf_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 501);
            this.Controls.Add(this.result_textBox);
            this.Controls.Add(this.region_comboBox);
            this.Controls.Add(this.region_label);
            this.Controls.Add(this.job_type_label);
            this.Controls.Add(this.job_type_comboBox);
            this.Controls.Add(this.web_url_textBox);
            this.Controls.Add(this.url);
            this.Controls.Add(this.pages_num_textBox);
            this.Controls.Add(this.pages_num);
            this.Controls.Add(this.exit_app_btn);
            this.Controls.Add(this.open_folder_btn);
            this.Controls.Add(this.execute_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "monstergulf_frm";
            this.Text = "MonsterGulf -Grabber";
            this.Load += new System.EventHandler(this.monstergulf_frm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button execute_btn;
        private System.Windows.Forms.Button open_folder_btn;
        private System.Windows.Forms.Button exit_app_btn;
        private System.Windows.Forms.Label pages_num;
        private System.Windows.Forms.TextBox pages_num_textBox;
        private System.Windows.Forms.Label url;
        private System.Windows.Forms.TextBox web_url_textBox;
        private System.Windows.Forms.ComboBox job_type_comboBox;
        private System.Windows.Forms.Label job_type_label;
        private System.Windows.Forms.Label region_label;
        private System.Windows.Forms.ComboBox region_comboBox;
        private System.Windows.Forms.RichTextBox result_textBox;
    }
}

