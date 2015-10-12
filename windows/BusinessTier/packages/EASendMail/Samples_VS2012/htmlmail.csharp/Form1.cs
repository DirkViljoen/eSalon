//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2006  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// |    Project: It demonstrates how to use EASendMail to send html email with 
// |             c#.
// |
// |    File: Form1 : implementation file
// |
// |    Author: Ivan Lui ( ivan@emailarchitect.net )
//  ===============================================================================
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using EASendMail;

namespace htmlmail.csharp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkSSL;
		private System.Windows.Forms.TextBox textFrom;
		private System.Windows.Forms.TextBox textTo;
		private System.Windows.Forms.TextBox textCc;
		private System.Windows.Forms.TextBox textSubject;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textUser;
		private System.Windows.Forms.Label Server;
		private System.Windows.Forms.TextBox textServer;
		private System.Windows.Forms.TextBox textAttachments;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.ProgressBar pgSending;
		private System.Windows.Forms.StatusBar sbStatus;
		private System.Windows.Forms.CheckBox chkAuth;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.ComboBox lstCharset;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string[,] m_arCharset = new string[28, 2];
		private ArrayList m_arAttachment = new ArrayList();
		private System.Windows.Forms.OpenFileDialog attachmentDlg;
		private System.Windows.Forms.CheckBox chkSignature;
        private System.Windows.Forms.CheckBox chkEncrypt;
		mshtml.IHTMLDocument2 m_htmlDoc = null;
		private System.Windows.Forms.ColorDialog colorDlg;
		private System.Windows.Forms.ComboBox lstFont;
		private System.Windows.Forms.ComboBox lstSize;
		private System.Windows.Forms.Button btnB;
		private System.Windows.Forms.Button btnI;
		private System.Windows.Forms.Button btnU;
		private System.Windows.Forms.Button btnC;
		private System.Windows.Forms.Button btnP;
		private System.Windows.Forms.Button btnCancel;
        private WebBrowser htmlEditor;
        private ComboBox lstProtocol;
		private bool m_bcancel = false;
        private long m_eventtick = 0;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_Init();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textFrom = new System.Windows.Forms.TextBox();
            this.textTo = new System.Windows.Forms.TextBox();
            this.textCc = new System.Windows.Forms.TextBox();
            this.textSubject = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAuth = new System.Windows.Forms.CheckBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textUser = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Server = new System.Windows.Forms.Label();
            this.textServer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textAttachments = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pgSending = new System.Windows.Forms.ProgressBar();
            this.sbStatus = new System.Windows.Forms.StatusBar();
            this.label9 = new System.Windows.Forms.Label();
            this.lstCharset = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.attachmentDlg = new System.Windows.Forms.OpenFileDialog();
            this.chkSignature = new System.Windows.Forms.CheckBox();
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.lstFont = new System.Windows.Forms.ComboBox();
            this.lstSize = new System.Windows.Forms.ComboBox();
            this.btnB = new System.Windows.Forms.Button();
            this.btnI = new System.Windows.Forms.Button();
            this.btnU = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnP = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.htmlEditor = new System.Windows.Forms.WebBrowser();
            this.lstProtocol = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subject";
            // 
            // textFrom
            // 
            this.textFrom.Location = new System.Drawing.Point(64, 19);
            this.textFrom.Name = "textFrom";
            this.textFrom.Size = new System.Drawing.Size(295, 21);
            this.textFrom.TabIndex = 1;
            // 
            // textTo
            // 
            this.textTo.Location = new System.Drawing.Point(64, 45);
            this.textTo.Name = "textTo";
            this.textTo.Size = new System.Drawing.Size(295, 21);
            this.textTo.TabIndex = 2;
            // 
            // textCc
            // 
            this.textCc.Location = new System.Drawing.Point(64, 72);
            this.textCc.Name = "textCc";
            this.textCc.Size = new System.Drawing.Size(295, 21);
            this.textCc.TabIndex = 3;
            // 
            // textSubject
            // 
            this.textSubject.Location = new System.Drawing.Point(64, 100);
            this.textSubject.Name = "textSubject";
            this.textSubject.Size = new System.Drawing.Size(295, 21);
            this.textSubject.TabIndex = 4;
            this.textSubject.Text = "Test HTML subject";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstProtocol);
            this.groupBox1.Controls.Add(this.chkAuth);
            this.groupBox1.Controls.Add(this.chkSSL);
            this.groupBox1.Controls.Add(this.textPassword);
            this.groupBox1.Controls.Add(this.textUser);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Server);
            this.groupBox1.Controls.Add(this.textServer);
            this.groupBox1.Location = new System.Drawing.Point(392, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 176);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // chkAuth
            // 
            this.chkAuth.AutoSize = true;
            this.chkAuth.Location = new System.Drawing.Point(8, 43);
            this.chkAuth.Name = "chkAuth";
            this.chkAuth.Size = new System.Drawing.Size(206, 19);
            this.chkAuth.TabIndex = 11;
            this.chkAuth.Text = "My server requires authentication";
            this.chkAuth.CheckedChanged += new System.EventHandler(this.chkAuth_CheckedChanged);
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(11, 122);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(114, 19);
            this.chkSSL.TabIndex = 14;
            this.chkSSL.Text = "SSL Connection";
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(75, 96);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(181, 21);
            this.textPassword.TabIndex = 13;
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(75, 70);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(181, 21);
            this.textUser.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "User";
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Location = new System.Drawing.Point(8, 17);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(42, 15);
            this.Server.TabIndex = 0;
            this.Server.Text = "Server";
            // 
            // textServer
            // 
            this.textServer.Location = new System.Drawing.Point(75, 18);
            this.textServer.Name = "textServer";
            this.textServer.Size = new System.Drawing.Size(181, 21);
            this.textServer.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "Attachments";
            // 
            // textAttachments
            // 
            this.textAttachments.BackColor = System.Drawing.SystemColors.Info;
            this.textAttachments.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textAttachments.Location = new System.Drawing.Point(86, 194);
            this.textAttachments.Name = "textAttachments";
            this.textAttachments.ReadOnly = true;
            this.textAttachments.Size = new System.Drawing.Size(482, 21);
            this.textAttachments.TabIndex = 6;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(496, 407);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 23);
            this.btnSend.TabIndex = 15;
            this.btnSend.TabStop = false;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // pgSending
            // 
            this.pgSending.Location = new System.Drawing.Point(8, 414);
            this.pgSending.Name = "pgSending";
            this.pgSending.Size = new System.Drawing.Size(469, 8);
            this.pgSending.TabIndex = 13;
            // 
            // sbStatus
            // 
            this.sbStatus.Location = new System.Drawing.Point(0, 440);
            this.sbStatus.Name = "sbStatus";
            this.sbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sbStatus.Size = new System.Drawing.Size(684, 22);
            this.sbStatus.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "Encoding";
            // 
            // lstCharset
            // 
            this.lstCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstCharset.Location = new System.Drawing.Point(75, 130);
            this.lstCharset.Name = "lstCharset";
            this.lstCharset.Size = new System.Drawing.Size(196, 23);
            this.lstCharset.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(574, 190);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(620, 190);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(46, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkSignature
            // 
            this.chkSignature.AutoSize = true;
            this.chkSignature.Location = new System.Drawing.Point(12, 164);
            this.chkSignature.Name = "chkSignature";
            this.chkSignature.Size = new System.Drawing.Size(120, 19);
            this.chkSignature.TabIndex = 17;
            this.chkSignature.Text = "Digitial Signature";
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.Location = new System.Drawing.Point(152, 164);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(66, 19);
            this.chkEncrypt.TabIndex = 18;
            this.chkEncrypt.Text = "Encrypt";
            // 
            // lstFont
            // 
            this.lstFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstFont.Location = new System.Drawing.Point(8, 222);
            this.lstFont.Name = "lstFont";
            this.lstFont.Size = new System.Drawing.Size(136, 23);
            this.lstFont.TabIndex = 20;
            this.lstFont.SelectedIndexChanged += new System.EventHandler(this.lstFont_SelectedIndexChanged);
            // 
            // lstSize
            // 
            this.lstSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstSize.Location = new System.Drawing.Point(152, 222);
            this.lstSize.Name = "lstSize";
            this.lstSize.Size = new System.Drawing.Size(80, 23);
            this.lstSize.TabIndex = 21;
            this.lstSize.SelectedIndexChanged += new System.EventHandler(this.lstSize_SelectedIndexChanged);
            // 
            // btnB
            // 
            this.btnB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnB.Location = new System.Drawing.Point(240, 220);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(24, 23);
            this.btnB.TabIndex = 22;
            this.btnB.Text = "B";
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // btnI
            // 
            this.btnI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnI.Location = new System.Drawing.Point(264, 220);
            this.btnI.Name = "btnI";
            this.btnI.Size = new System.Drawing.Size(24, 23);
            this.btnI.TabIndex = 23;
            this.btnI.Text = "I";
            this.btnI.Click += new System.EventHandler(this.btnI_Click);
            // 
            // btnU
            // 
            this.btnU.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnU.Location = new System.Drawing.Point(288, 220);
            this.btnU.Name = "btnU";
            this.btnU.Size = new System.Drawing.Size(24, 23);
            this.btnU.TabIndex = 24;
            this.btnU.Text = "U";
            this.btnU.Click += new System.EventHandler(this.btnU_Click);
            // 
            // btnC
            // 
            this.btnC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC.ForeColor = System.Drawing.Color.Red;
            this.btnC.Location = new System.Drawing.Point(312, 220);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(24, 23);
            this.btnC.TabIndex = 25;
            this.btnC.Text = "C";
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // btnP
            // 
            this.btnP.Location = new System.Drawing.Point(352, 220);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(88, 23);
            this.btnP.TabIndex = 26;
            this.btnP.Text = "Insert Picture";
            this.btnP.Click += new System.EventHandler(this.btnP_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(592, 407);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // htmlEditor
            // 
            this.htmlEditor.Location = new System.Drawing.Point(8, 252);
            this.htmlEditor.MinimumSize = new System.Drawing.Size(20, 20);
            this.htmlEditor.Name = "htmlEditor";
            this.htmlEditor.Size = new System.Drawing.Size(664, 149);
            this.htmlEditor.TabIndex = 28;
            this.htmlEditor.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.htmlEditor_Navigated);
            // 
            // lstProtocol
            // 
            this.lstProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstProtocol.FormattingEnabled = true;
            this.lstProtocol.Location = new System.Drawing.Point(11, 146);
            this.lstProtocol.Name = "lstProtocol";
            this.lstProtocol.Size = new System.Drawing.Size(250, 23);
            this.lstProtocol.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.htmlEditor);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnP);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.btnU);
            this.Controls.Add(this.btnI);
            this.Controls.Add(this.btnB);
            this.Controls.Add(this.lstSize);
            this.Controls.Add(this.lstFont);
            this.Controls.Add(this.chkEncrypt);
            this.Controls.Add(this.chkSignature);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstCharset);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textAttachments);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textSubject);
            this.Controls.Add(this.textCc);
            this.Controls.Add(this.textTo);
            this.Controls.Add(this.textFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sbStatus);
            this.Controls.Add(this.pgSending);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		#region	EASendMail EventHandler
		void OnIdle( object sender, ref bool cancel )
		{
			cancel = m_bcancel;
			if( !cancel )
				Application.DoEvents();//waiting server reponse or connecting server.
			
		}

		void OnConnected(object sender, ref bool cancel )
		{
			_SetStatus("Connected");
			cancel = m_bcancel;
		}


		void OnSendingDataStream(object sender, int sent, int total, ref bool cancel )
		{
            if (pgSending.Value == 0)
            {
                _SetStatus("Sending ...");
            }
            _SetProgress(sent, total);
			cancel = m_bcancel;
			if( sent == total )
				_SetStatus("Disconnecting ..."); 
		}

		void OnAuthorized( object sender, ref bool cancel )
		{
			_SetStatus("Authorized");
			cancel = m_bcancel;
		}

		void OnSecuring( object sender, ref bool cancel )
		{
			_SetStatus( "Securing ...");
			cancel = m_bcancel;
		}

		#endregion

		#region Initialize the Encoding List
		private void _InitCharset()
		{
			int nIndex = 0;
            string defaultEncoding = "utf-8";// System.Text.Encoding.Default.HeaderName;

			m_arCharset[nIndex, 0] = "Arabic(Windows)";
			m_arCharset[nIndex, 1] = "windows-1256";
			nIndex++;

			m_arCharset[nIndex, 0] = "Baltic(ISO)";
			m_arCharset[nIndex, 1] = "iso-8859-4";
			nIndex++;

			m_arCharset[nIndex, 0] = "Baltic(Windows)";
			m_arCharset[nIndex, 1] = "windows-1257";
			nIndex++;

			m_arCharset[nIndex, 0] = "Central Euporean(ISO)";
			m_arCharset[nIndex, 1] = "iso-8859-2";
			nIndex++;

			m_arCharset[nIndex, 0] = "Central Euporean(Windows)";
			m_arCharset[nIndex, 1] = "windows-1250";
			nIndex++;

			m_arCharset[nIndex, 0] = "Chinese Simplified(GB18030)";
			m_arCharset[nIndex, 1] = "GB18030";
			nIndex++;

			m_arCharset[nIndex, 0] = "Chinese Simplified(GB2312)";
			m_arCharset[nIndex, 1] = "gb2312";
			nIndex++;

			m_arCharset[nIndex, 0] = "Chinese Simplified(HZ)";
			m_arCharset[nIndex, 1] = "hz-gb-2312";
			nIndex++;

			m_arCharset[nIndex, 0] = "Chinese Traditional(Big5)";
			m_arCharset[nIndex, 1] = "big5";
			nIndex++;

			m_arCharset[nIndex, 0] = "Cyrillic(ISO)";
			m_arCharset[nIndex, 1] = "iso-8859-5";
			nIndex++;

			m_arCharset[nIndex, 0] = "Cyrillic(KOI8-R)";
			m_arCharset[nIndex, 1] = "koi8-r";
			nIndex++;

			m_arCharset[nIndex, 0] = "Cyrillic(KOI8-U)";
			m_arCharset[nIndex, 1] = "koi8-u";
			nIndex++;

			m_arCharset[nIndex, 0] = "Cyrillic(Windows)";
			m_arCharset[nIndex, 1] = "windows-1251";
			nIndex++;

			m_arCharset[nIndex, 0] = "Greek(ISO)";
			m_arCharset[nIndex, 1] = "iso-8859-7";
			nIndex++;

			m_arCharset[nIndex, 0] = "Greek(Windows)";
			m_arCharset[nIndex, 1] = "windows-1253";
			nIndex++;

			m_arCharset[nIndex, 0] = "Hebrew(Windows)";
			m_arCharset[nIndex, 1] = "windows-1255";
			nIndex++;

			m_arCharset[nIndex, 0] = "Japanese(JIS)";
			m_arCharset[nIndex, 1] = "iso-2022-jp";
			nIndex++;

			m_arCharset[nIndex, 0] = "Korean";
			m_arCharset[nIndex, 1] = "ks_c_5601-1987";
			nIndex++;

			m_arCharset[nIndex, 0] = "Korean(EUC)";
			m_arCharset[nIndex, 1] = "euc-kr";
			nIndex++;

			m_arCharset[nIndex, 0] = "Latin 9(ISO)";
			m_arCharset[nIndex, 1] = "iso-8859-15";
			nIndex++;

			m_arCharset[nIndex, 0] = "Thai(Windows)";
			m_arCharset[nIndex, 1] = "windows-874";
			nIndex++;

			m_arCharset[nIndex, 0] = "Turkish(ISO)";
			m_arCharset[nIndex, 1] = "iso-8859-9";
			nIndex++;

			m_arCharset[nIndex, 0] = "Turkish(Windows)";
			m_arCharset[nIndex, 1] = "windows-1254";
			nIndex++;

			m_arCharset[nIndex, 0] = "Unicode(UTF-7)";
			m_arCharset[nIndex, 1] = "utf-7";
			nIndex++;

			m_arCharset[nIndex, 0] = "Unicode(UTF-8)";
			m_arCharset[nIndex, 1] = "utf-8";
			nIndex++;

			m_arCharset[nIndex, 0] = "Vietnames(Windows)";
			m_arCharset[nIndex, 1] = "windows-1258";
			nIndex++;

			m_arCharset[nIndex, 0] = "Western European(ISO)";
			m_arCharset[nIndex, 1] = "iso-8859-1";
			nIndex++;

			m_arCharset[nIndex, 0] = "Western European(Windows)";
			m_arCharset[nIndex, 1] = "Windows-1252";
			nIndex++;

			int selectIndex = 25; //utf-8
			for( int i = 0; i < nIndex; i++ )
			{
				lstCharset.Items.Add(m_arCharset[i, 0]);
				if( String.Compare(
					m_arCharset[i,1], defaultEncoding, true ) == 0 )
				{
					selectIndex = i;
				}
			}
			
			lstCharset.SelectedIndex = selectIndex;		
		}
		#endregion

		private void _ChangeAuthStatus()
		{
			textUser.Enabled = chkAuth.Checked ;
			textPassword.Enabled = chkAuth.Checked ;
		}
		private void _Init()
		{	
			_InitCharset();
            _InitProtocols();
			_ChangeAuthStatus();
		}

        private void _InitProtocols()
        {
            lstProtocol.Items.Add("SMTP Protocol - Recommended");
            lstProtocol.Items.Add("Exchange Web Service - 2007/2010");
            lstProtocol.Items.Add("Exchange WebDav - 2000/2003");
            lstProtocol.SelectedIndex = 0;
        }

#region Sign and Encrypt E-mail by Digital Certificate
		private bool _Signencrypt( ref SmtpMail oMail )
		{
			if( chkSignature.Checked )
			{
				try
				{
					oMail.From.Certificate.FindSubject( oMail.From.Address,
						Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
						"My" );
				}
				catch( Exception exp )
				{
					MessageBox.Show( "No sign certificate found for <" + oMail.From.Address + ">:" + exp.Message );
					btnSend.Text = "Send";
					return false;
				}
			}

			int count = 0;
			if( chkEncrypt.Checked )
			{
				count = oMail.To.Count;
				for( int i = 0; i < count; i++ )
				{
					MailAddress oAddress = oMail.To[i] as MailAddress;
					try
					{
						oAddress.Certificate.FindSubject( oAddress.Address,
							Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
							"AddressBook" );
					}
					catch( Exception ep )
					{
						try
						{
							oAddress.Certificate.FindSubject( oAddress.Address,
								Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
								"My" );
						}
						catch( Exception exp )
						{
							MessageBox.Show( "No encryption certificate found for <" + oAddress.Address + ">:" + exp.Message );
							return false;
						}
					}
				}

				count = oMail.Cc.Count;
				for( int i = 0; i < count; i++ )
				{
					MailAddress oAddress = oMail.Cc[i] as MailAddress;
					try
					{
						oAddress.Certificate.FindSubject( oAddress.Address,
							Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
							"AddressBook" );
					}
					catch( Exception ep )
					{
						try
						{
							oAddress.Certificate.FindSubject( oAddress.Address,
								Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
								"My" );
						}
						catch( Exception exp )
						{
							MessageBox.Show( "No encryption certificate found for <" + oAddress.Address + ">:" + exp.Message );
							return false;
						}
					}
				}
			}

			return true;
		}

		#endregion

#region Send E-mail without SMTP server to multiple recipients
		private void _DirectSend( ref SmtpMail oMail, ref SmtpClient oSmtp )
		{
			AddressCollection recipients = oMail.Recipients.Copy();
			int count = recipients.Count;
			for( int i = 0; i < count; i++ )
			{
				string err = "";
				MailAddress address = recipients[i] as MailAddress;

				bool terminated = false;
				try
				{
					oMail.To.Clear();
					oMail.Cc.Clear();
					oMail.Bcc.Clear();

					oMail.To.Add( address );
					SmtpServer oServer = new SmtpServer( "" );

					sbStatus.Text = String.Format( "Connecting server for {0} ... ", address.Address );
					pgSending.Value = 0;
					oSmtp.SendMail( oServer, oMail );
					MessageBox.Show( String.Format( "The message to <{0}> was sent to {1} successfully!", 
						address.Address,
						oSmtp.CurrentSmtpServer.Server ));

					sbStatus.Text = "Completed";
					
				}
				catch( SmtpTerminatedException exp )
				{
					err = exp.Message;
					terminated = true;
				}
				catch( SmtpServerException exp )
				{
					err = String.Format( "Exception: Server Respond: {0}", exp.ErrorMessage );
				}
				catch( System.Net.Sockets.SocketException exp )
				{
					err = String.Format( "Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message );
				}
				catch( System.ComponentModel.Win32Exception exp )
				{
					err = String.Format( "Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message );			
				}
				catch( System.Exception exp )
				{
					err = String.Format( "Exception: Common: {0}", exp.Message );			
				}	


				if( terminated )
					break;
				
				if( err.Length > 0 )
				{ 
					MessageBox.Show( String.Format("The message was unable to delivery to <{0}> due to \r\n{1}",
						address.Address, err ));

					sbStatus.Text = err;
				}
			}
		}

		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{

			if( textFrom.Text.Length == 0 )
			{
				MessageBox.Show( "Please input From!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>" );
				return;
			}

			if( textTo.Text.Length == 0 &&
				textCc.Text.Length == 0 )
			{
				MessageBox.Show( "Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients" );
				return;
			}

			btnSend.Enabled = false;
			btnCancel.Enabled = true;
			m_bcancel = false;
			
			//For evaluation usage, please use "TryIt" as the license code, otherwise the 
			//"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
			//"trial version expired" exception will be thrown.

			//For licensed usage, please use your license code instead of "TryIt", then the object
			//will never expire
			SmtpMail oMail = new SmtpMail("TryIt");
			SmtpClient oSmtp = new SmtpClient();
			//To generate a log file for SMTP transaction, please use
			//oSmtp.LogFileName = "c:\\smtp.log";

			string err = "";

			try
			{
				oMail.Reset();
				//If you want to specify a reply address
				//oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" );

				//From is a MailAddress object, in c#, it supports implicit converting from string.
				//The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"
				
				//The example code without implicit converting
				// oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
				// oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
				// oMail.From = new MailAddress( "test@adminsystem.com" )
				oMail.From = textFrom.Text;


				//To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
				// multiple address are separated with (,;)
				//The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

				//The example code without implicit converting
				// oMail.To = new AddressCollection( "test1@adminsystem.com, test2@adminsystem.com" );
				// oMail.To = new AddressCollection( "Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");
				oMail.To = textTo.Text;
				//You can add more recipient by Add method
				// oMail.To.Add( new MailAddress( "tester", "test@adminsystem.com"));

				oMail.Cc = textCc.Text;
				oMail.Subject = textSubject.Text;
				oMail.Charset = m_arCharset[lstCharset.SelectedIndex,1];

				//digital signature and encryption
				if(!_Signencrypt( ref oMail ))
				{
					btnSend.Enabled = true;
					btnCancel.Enabled = false;
					return;
				}


				string basepath = Application.ExecutablePath;
				int pos = basepath.LastIndexOfAny( "/\\".ToCharArray() );
				if( pos != -1 )
					basepath = basepath.Substring( 0, pos );
			
				string body = m_htmlDoc.body.innerHTML;
				body = body.Replace( "[$from]", _EncodeAddress(oMail.From.ToString()));
				body = body.Replace( "[$to]", _EncodeAddress(oMail.To.ToString()));
				body = body.Replace( "[$subject]", _EncodeAddress(oMail.Subject) );
				string htmlheader = String.Format( "<html><title>{0}</title><meta HTTP-EQUIV=\"Content-Type\" Content=\"text-html; charset={1}\"><META content=\"MSHTML 6.00.2900.2769\" name=GENERATOR><body>",
					oMail.Subject, oMail.Charset );
								
				body = body.Insert( 0, htmlheader );
				body += "</body></html>";

				oMail.ImportHtml( body, 
					Application.ExecutablePath,
					ImportHtmlBodyOptions.ImportLocalPictures );

				int count = m_arAttachment.Count;
				for( int i = 0; i < count; i++ )
				{
					oMail.AddAttachment( m_arAttachment[i] as string );
				}

				SmtpServer oServer = new SmtpServer( textServer.Text );
                oServer.Protocol = (ServerProtocol)lstProtocol.SelectedIndex;

				if( oServer.Server.Length != 0 )
				{
					if( chkAuth.Checked )
					{
						oServer.User = textUser.Text;
						oServer.Password = textPassword.Text;
					}

					if( chkSSL.Checked )
						oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
				}
				else
				{
					//To send email to the recipient directly(simulating the smtp server), 
					//please add a Received header, 
					//otherwise, many anti-spam filter will make it as junk email.
					System.Globalization.CultureInfo cur = new System.Globalization.CultureInfo("en-US");
					string gmtdate = System.DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur);
					gmtdate.Remove( gmtdate.Length - 3, 1 );
					string recvheader = String.Format( "from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;\r\n\t {1}",
						oServer.HeloDomain,
						gmtdate );

					oMail.Headers.Insert( 0, new HeaderItem( "Received", recvheader ));
				}

				//Catching the following events is not necessary, 
				//just make the application more user friendly.
				//If you use the object in asp.net/windows service or non-gui application, 
				//You need not to catch the following events.
				//To learn more detail, please refer to the code in EASendMail EventHandler region
				oSmtp.OnIdle += new SmtpClient.OnIdleEventHandler( OnIdle );
				oSmtp.OnAuthorized += new SmtpClient.OnAuthorizedEventHandler( OnAuthorized );
				oSmtp.OnConnected += new SmtpClient.OnConnectedEventHandler( OnConnected );
				oSmtp.OnSecuring += new SmtpClient.OnSecuringEventHandler( OnSecuring );
				oSmtp.OnSendingDataStream += new SmtpClient.OnSendingDataStreamEventHandler( OnSendingDataStream );
				
				if( oServer.Server.Length == 0 && oMail.Recipients.Count > 1 )
				{
					//To send email without specified smtp server, we have to send the emails one by one 
					// to multiple recipients. That is because every recipient has different smtp server.
					_DirectSend( ref oMail, ref oSmtp );
				}
				else
				{
					sbStatus.Text = "Connecting ... ";
					pgSending.Value = 0;

					oSmtp.SendMail( oServer, oMail  );

					MessageBox.Show( String.Format( "The message was sent to {0} successfully!", 
						oSmtp.CurrentSmtpServer.Server ));

					sbStatus.Text = "Completed";				
				}
				//If you want to reuse the mail object, please reset the Date and Message-ID, otherwise
				//the Date and Message-ID will not change.
				//oMail.Date = System.DateTime.Now;
				//oMail.ResetMessageID();
				//oMail.To = "another@example.com";
				//oSmtp.SendMail( oServer, oMail );
		
			}
			catch( SmtpTerminatedException exp )
			{
				err = exp.Message;
			}
			catch( SmtpServerException exp )
			{
				err = String.Format( "Exception: Server Respond: {0}", exp.ErrorMessage );
			}
			catch( System.Net.Sockets.SocketException exp )
			{
				err = String.Format( "Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message );
			}
			catch( System.ComponentModel.Win32Exception exp )
			{
				err = String.Format( "Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message );			
			}
			catch( System.Exception exp )
			{
				err = String.Format( "Exception: Common: {0}", exp.Message );			
			}
			
			if( err.Length > 0  )
			{
				MessageBox.Show( err );
				sbStatus.Text = err;
			}
			
			btnSend.Enabled = true;
			btnCancel.Enabled = false;
		}

		private void chkAuth_CheckedChanged(object sender, System.EventArgs e)
		{
			_ChangeAuthStatus();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			htmlEditor.Focus();
			attachmentDlg.Reset(); 
			attachmentDlg.Multiselect = true;
			attachmentDlg.CheckFileExists = true;
			attachmentDlg.CheckPathExists = true;
			if( attachmentDlg.ShowDialog()!= DialogResult.OK )
				return;
	
			string [] attachments = attachmentDlg.FileNames;
			int nLen = attachments.Length;
			for( int i = 0; i < nLen; i++ )
			{
				m_arAttachment.Add( attachments[i] );
				string fileName = attachments[i];
				int pos = fileName.LastIndexOf( "\\" );
				if( pos != -1 )
					fileName = fileName.Substring( pos+1 );

				textAttachments.Text += fileName;
				textAttachments.Text += ";";
			}
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			m_arAttachment.Clear();
			textAttachments.Text = "";
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			htmlEditor.Navigate( "about:blank");

			m_htmlDoc = htmlEditor.Document.DomDocument as mshtml.IHTMLDocument2;
			m_htmlDoc.designMode = "on";
			InitFonts();
		}

		private void btnB_Click(object sender, System.EventArgs e)
		{
			m_htmlDoc.execCommand( "Bold", false, null );
			htmlEditor.Focus();
		}

		private void btnI_Click(object sender, System.EventArgs e)
		{
			m_htmlDoc.execCommand( "Italic", false, null );
			htmlEditor.Focus();		
		}

		private void btnU_Click(object sender, System.EventArgs e)
		{
			m_htmlDoc.execCommand( "Underline", false, null );
			htmlEditor.Focus();
	
		}

		private void btnC_Click(object sender, System.EventArgs e)
		{
			if( colorDlg.ShowDialog() == DialogResult.OK )
			{
				string v = string.Format( "#{0:x2}{1:x2}{2:x2}", colorDlg.Color.R,
					colorDlg.Color.G,
					colorDlg.Color.B );
				m_htmlDoc.execCommand( "ForeColor", false, v );
			}
			htmlEditor.Focus();
		}

		#region Initialize fonts list
		private void InitFonts()
		{
			string[] arFonts = new string[] { "Choose Font Style ...",
												"Allegro BT",
												"Arial",
												"Arial Baltic",
												"Arial Black",
												"Arial CE",
												"Arial CYR",
												"Arial Greek",
												"Arial Narrow",
												"Arial TUR",
												"AvantGarde Bk BT",
												"BankGothic Md BT",
												"Basemic",
												"Basemic Symbol",
												"Basemic Times",
												"Batang",
												"BatangChe",
												"Benguiat Bk BT",
												"BernhardFashion BT",
												"BernhardMod BT",
												"Book Antiqua",
												"Bookman Old Style",
												"Bremen Bd BT",
												"Century Gothic",
												"Charlesworth",
												"Comic Sans MS",
												"CommonBullets",
												"CopprplGoth Bd BT",
												"Courier",
												"Courier New",
												"Courier New Baltic",
												"Courier New CE",
												"Courier New CYR",
												"Courier New Greek",
												"Courier New TUR",
												"Dauphin",
												"Dotum",
												"DotumChe",
												"Dungeon",
												"English111 Vivace BT",
												"Estrangelo Edessa",
												"Fixedsys",
												"Franklin Gothic Medium",
												"Futura Lt BT",
												"Futura Md BT",
												"Futura XBlk BT",
												"FuturaBlack BT",
												"Garamond",
												"Gautami",
												"Georgia",
												"GoudyHandtooled BT",
												"GoudyOlSt BT",
												"Gulim",
												"GulimChe",
												"Gungsuh",
												"GungsuhChe",
												"Haettenschweiler",
												"Humanst521 BT",
												"Impact",
												"Kabel Bk BT",
												"Kabel Ult BT",
												"Kingsoft Phonetic Plain",
												"Latha",
												"Lithograph",
												"LithographLight",
												"Lucida Console",
												"Lucida Sans Unicode",
												"Mangal",
												"Marlett",
												"Microsoft Sans Serif",
												"MingLiU",
												"Modern",
												"Monotype Corsiva",
												"MS Gothic",
												"MS Mincho",
												"MS Outlook",
												"MS PGothic",
												"MS PMincho",
												"MS Sans Serif",
												"MS Serif",
												"MS UI Gothic",
												"MT Extra",
												"MV Boli",
												"Myriad Condensed Web",
												"Myriad Web",
												"OzHandicraft BT",
												"Palatino Linotype",
												"PMingLiU",
												"PosterBodoni BT",
												"Raavi",
												"Roman",
												"Script",
												"Serifa BT",
												"Serifa Th BT",
												"Shruti",
												"Small Fonts",
												"Souvenir Lt BT",
												"Staccato222 BT",
												"Swiss911 XCm BT",
												"Sylfaen",
												"Symbol",
												"System",
												"Tahoma",
												"Terminal",
												"Times New Roman",
												"Times New Roman Baltic",
												"Times New Roman CE",
												"Times New Roman CYR",
												"Times New Roman Greek",
												"Times New Roman TUR",
												"Trebuchet MS",
												"Tunga",
												"TypoUpright BT",
												"Verdana",
												"VisualUI",
												"Webdings",
												"Wingdings",
												"Wingdings 2",
												"Wingdings 3",
												"WP Arabic Sihafa",
												"WP ArabicScript Sihafa",
												"WP BoxDrawing",
												"WP CyrillicA",
												"WP CyrillicB",
												"WP Greek Century",
												"WP Greek Courier",
												"WP Greek Helve",
												"WP Hebrew David",
												"WP IconicSymbolsA",
												"WP IconicSymbolsB",
												"WP Japanese",
												"WP MathA",
												"WP MathB",
												"WP MathExtendedA",
												"WP MathExtendedB",
												"WP MultinationalA Courier",
												"WP MultinationalA Helve",
												"WP MultinationalA Roman",
												"WP MultinationalB Courier",
												"WP MultinationalB Helve",
												"WP MultinationalB Roman",
												"WP Phonetic",
												"WP TypographicSymbols",
												"WST_Czec",
												"WST_Engl",
												"WST_Fren",
												"WST_Germ",
												"WST_Ital",
												"WST_Span",
												"WST_Swed",
												"ZapfEllipt BT",
												"Zurich Ex BT" };

			int nCount = arFonts.Length;
			for( int i = 0; i < nCount; i++ )
			{
				lstFont.Items.Add( arFonts[i]);
			}

			lstFont.SelectedIndex = 0;

			lstSize.Items.Add( "Font Size ... " );
			for( int i = 1; i < 7; i++ )
			{
				lstSize.Items.Add( i );
			}
			lstSize.SelectedIndex = 0;

		}
		#endregion-

		private void lstFont_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string font = lstFont.Text;
			lstFont.SelectedIndex = 0;
			m_htmlDoc.execCommand( "fontname", false, font );
			htmlEditor.Focus();
		}

		private void lstSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string size = lstSize.Text;
			lstSize.SelectedIndex = 0;
			m_htmlDoc.execCommand( "fontsize", false, size);
			htmlEditor.Focus();		
		}

		private void btnP_Click(object sender, System.EventArgs e)
		{
            m_htmlDoc.execCommand("InsertImage", true, null);
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			btnCancel.Enabled = false;
			m_bcancel = true;
		}
		private string _EncodeAddress( string v )
		{
			v = v.Replace( ">", "&gt;" );
			v = v.Replace( "<", "&lt;" );
			return v;
		}

        private void htmlEditor_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("<div>This sample demonstrates how to send html email.</div><div>&nbsp;</div>");
            s.Append("<div>From: [$from]</div>");
            s.Append("<div>To: [$to]</div>");
            s.Append("<div>Subject: [$subject]</div><div>&nbsp;</div>");
            s.Append("<div>If no sever address was specified, the email will be delivered to the recipient's server directly,");
            s.Append("However, if you don't have a static IP address, ");
            s.Append("many anti-spam filters will mark it as a junk-email.</div><div>&nbsp;</div>");
            s.Append("<div>If \"Digitial Signature\" was checked, please make sure you have the certificate for the sender address installed on ");
            s.Append("Local User Certificate Store.</div><div>&nbsp;</div>");
            s.Append("<div>If \"Encrypt\" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store.</div>");

            m_htmlDoc.body.innerHTML = s.ToString();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.Width < 700)
            {
                this.Width = 700;
            }

            if (this.Height < 500)
            {
                this.Height = 500;
            }

            htmlEditor.Width = this.Width - 35;
            htmlEditor.Height = this.Height - 350;

            pgSending.Top = htmlEditor.Height + htmlEditor.Top + 15;
            pgSending.Width = this.Width - 240;
            btnSend.Top = pgSending.Top - 8;
            btnCancel.Top = btnSend.Top;
            btnSend.Left = pgSending.Width + 30;
            btnCancel.Left = pgSending.Width + 125;

            groupBox1.Left = this.Width - 310;

            textFrom.Width = this.Width - 400;
            textTo.Width = textFrom.Width;
            textCc.Width = textFrom.Width;
            textSubject.Width = textFrom.Width;

            textAttachments.Width = this.Width - 230;
            btnAdd.Left = textAttachments.Width + 100;
            btnClear.Left = textAttachments.Width + 150;

        }

        #region Cross Thread Access Control
        protected delegate void SetStatusDelegate(string v);
        protected delegate void SetProgressDelegate(int sent, int total);

        protected void _SetProgressCallBack(int sent, int total)
        {
            long t = sent;
            t = t * 100;
            t = t / total;
            int x = (int)t;
            pgSending.Value = x;

            long tick = System.DateTime.Now.Ticks;
            // call DoEvents every 0.2 second 
            if (tick - m_eventtick > 2000000)
            {
                // Do not call DoEvents too frequently in a very fast lan + larg email.
                m_eventtick = tick;
                Application.DoEvents();
            }
        }

        protected void _SetStatusCallBack(string v)
        {
            sbStatus.Text = v;
        }

        //Why we need to change the status text by this function.
        //Because some the events are fired on another
        //thread, to change the control value safety, we used this function to 
        //update control value. more detail, please refer to Control.BeginInvoke method
        // in MSDN
        protected void _SetStatus(string v)
        {
            if (InvokeRequired)
            {
                object[] args = new object[1];
                args[0] = v;

                SetStatusDelegate d = new SetStatusDelegate(_SetStatusCallBack);
                BeginInvoke(d, args);
            }
            else
            {
                _SetStatusCallBack(v);
            }
        }

        protected void _SetProgress(int sent, int total)
        {
            if (InvokeRequired)
            {
                object[] args = new object[2];
                args[0] = sent;
                args[1] = total;

                SetProgressDelegate d = new SetProgressDelegate(_SetProgressCallBack);
                BeginInvoke(d, args);
            }
            else
            {
                _SetProgressCallBack(sent, total);
            }
        }
        #endregion


	}
}
