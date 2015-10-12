namespace pocket.mobile.cs
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textFrom = new System.Windows.Forms.TextBox();
            this.textTo = new System.Windows.Forms.TextBox();
            this.textCc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textSubject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAuth = new System.Windows.Forms.CheckBox();
            this.textServer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textUser = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textAttachments = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.textBody = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pgSending = new System.Windows.Forms.ProgressBar();
            this.sbStatus = new System.Windows.Forms.StatusBar();
            this.attachmentDlg = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 23);
            this.label2.Text = "To:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 22);
            this.label3.Text = "Cc:";
            // 
            // textFrom
            // 
            this.textFrom.Location = new System.Drawing.Point(79, 3);
            this.textFrom.Name = "textFrom";
            this.textFrom.Size = new System.Drawing.Size(148, 21);
            this.textFrom.TabIndex = 3;
            // 
            // textTo
            // 
            this.textTo.Location = new System.Drawing.Point(79, 32);
            this.textTo.Name = "textTo";
            this.textTo.Size = new System.Drawing.Size(148, 21);
            this.textTo.TabIndex = 4;
            // 
            // textCc
            // 
            this.textCc.Location = new System.Drawing.Point(79, 61);
            this.textCc.Name = "textCc";
            this.textCc.Size = new System.Drawing.Size(148, 21);
            this.textCc.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 30);
            this.label4.Text = "Subject:";
            // 
            // textSubject
            // 
            this.textSubject.Location = new System.Drawing.Point(79, 90);
            this.textSubject.Name = "textSubject";
            this.textSubject.Size = new System.Drawing.Size(148, 21);
            this.textSubject.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 19);
            this.label5.Text = "SMTP Server:";
            // 
            // chkAuth
            // 
            this.chkAuth.Location = new System.Drawing.Point(3, 291);
            this.chkAuth.Name = "chkAuth";
            this.chkAuth.Size = new System.Drawing.Size(166, 23);
            this.chkAuth.TabIndex = 9;
            this.chkAuth.Text = "Require Authentication";
            this.chkAuth.CheckStateChanged += new System.EventHandler(this.chkAuth_CheckStateChanged);
            // 
            // textServer
            // 
            this.textServer.Location = new System.Drawing.Point(99, 267);
            this.textServer.Name = "textServer";
            this.textServer.Size = new System.Drawing.Size(128, 21);
            this.textServer.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 18);
            this.label6.Text = "Username:";
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(83, 314);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(133, 21);
            this.textUser.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 342);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 17);
            this.label7.Text = "Password:";
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(83, 340);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(133, 21);
            this.textPassword.TabIndex = 14;
            // 
            // chkSSL
            // 
            this.chkSSL.Location = new System.Drawing.Point(7, 367);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(198, 29);
            this.chkSSL.TabIndex = 15;
            this.chkSSL.Text = "SSL Connection";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(5, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 23);
            this.label8.Text = "Attachment:";
            // 
            // textAttachments
            // 
            this.textAttachments.Location = new System.Drawing.Point(79, 119);
            this.textAttachments.Name = "textAttachments";
            this.textAttachments.Size = new System.Drawing.Size(148, 21);
            this.textAttachments.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(79, 148);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 24);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(156, 148);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(71, 24);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // textBody
            // 
            this.textBody.Location = new System.Drawing.Point(5, 178);
            this.textBody.Multiline = true;
            this.textBody.Name = "textBody";
            this.textBody.Size = new System.Drawing.Size(222, 83);
            this.textBody.TabIndex = 20;
            this.textBody.Text = "This is a test email";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(12, 401);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(71, 22);
            this.btnSend.TabIndex = 21;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(89, 401);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 22);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pgSending
            // 
            this.pgSending.Location = new System.Drawing.Point(10, 430);
            this.pgSending.Name = "pgSending";
            this.pgSending.Size = new System.Drawing.Size(215, 10);
            // 
            // sbStatus
            // 
            this.sbStatus.Location = new System.Drawing.Point(0, 443);
            this.sbStatus.Name = "sbStatus";
            this.sbStatus.Size = new System.Drawing.Size(234, 22);
            this.sbStatus.Text = "status";
            // 
            // attachmentDlg
            // 
            this.attachmentDlg.FileName = "attachmentDlg";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(234, 465);
            this.Controls.Add(this.chkSSL);
            this.Controls.Add(this.sbStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pgSending);
            this.Controls.Add(this.textServer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.chkAuth);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textBody);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textAttachments);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textSubject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textCc);
            this.Controls.Add(this.textTo);
            this.Controls.Add(this.textFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textFrom;
        private System.Windows.Forms.TextBox textTo;
        private System.Windows.Forms.TextBox textCc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAuth;
        private System.Windows.Forms.TextBox textServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textUser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textAttachments;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox textBody;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar pgSending;
        private System.Windows.Forms.OpenFileDialog attachmentDlg;
        private System.Windows.Forms.StatusBar sbStatus;

    }
}

