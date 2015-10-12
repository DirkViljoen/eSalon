//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2006  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// |    Project: It demonstrates how to use EASendMail to send mass email with 
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

namespace mass.csharp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkSSL;
		private System.Windows.Forms.TextBox textFrom;
		private System.Windows.Forms.TextBox textSubject;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textUser;
		private System.Windows.Forms.Label Server;
		private System.Windows.Forms.TextBox textServer;
		private System.Windows.Forms.TextBox textAttachments;
		private System.Windows.Forms.Button btnSend;
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
		private System.Windows.Forms.RichTextBox textBody;
		private System.Windows.Forms.OpenFileDialog attachmentDlg;
		private System.Windows.Forms.CheckBox chkTestRecipients;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView lstTo;
		private System.Windows.Forms.Button btnAddTo;
		private System.Windows.Forms.Button btnClearTo;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colAddress;
		private System.Windows.Forms.ColumnHeader colStatus;
		private System.Windows.Forms.TextBox textThreads;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSimple;
		private bool m_bcancel = false;
		
		private int m_ntotal = 0;
		private System.Windows.Forms.Label status;
		private int m_nsent = 0;
		private int m_nsuccess = 0;
		private ComboBox lstProtocol;
		private int m_nfailure = 0;

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
			this.label4 = new System.Windows.Forms.Label();
			this.textFrom = new System.Windows.Forms.TextBox();
			this.textSubject = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lstProtocol = new System.Windows.Forms.ComboBox();
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
			this.label9 = new System.Windows.Forms.Label();
			this.lstCharset = new System.Windows.Forms.ComboBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.textBody = new System.Windows.Forms.RichTextBox();
			this.attachmentDlg = new System.Windows.Forms.OpenFileDialog();
			this.chkTestRecipients = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textThreads = new System.Windows.Forms.TextBox();
			this.lstTo = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colAddress = new System.Windows.Forms.ColumnHeader();
			this.colStatus = new System.Windows.Forms.ColumnHeader();
			this.btnAddTo = new System.Windows.Forms.Button();
			this.btnClearTo = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSimple = new System.Windows.Forms.Button();
			this.status = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "From";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "To";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 247);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 15);
			this.label4.TabIndex = 3;
			this.label4.Text = "Subject";
			// 
			// textFrom
			// 
			this.textFrom.Location = new System.Drawing.Point(64, 8);
			this.textFrom.Name = "textFrom";
			this.textFrom.Size = new System.Drawing.Size(328, 21);
			this.textFrom.TabIndex = 1;
			// 
			// textSubject
			// 
			this.textSubject.Location = new System.Drawing.Point(67, 244);
			this.textSubject.Name = "textSubject";
			this.textSubject.Size = new System.Drawing.Size(325, 21);
			this.textSubject.TabIndex = 4;
			this.textSubject.Text = "Test sample";
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
			this.groupBox1.Location = new System.Drawing.Point(411, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(261, 205);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			// 
			// lstProtocol
			// 
			this.lstProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstProtocol.Location = new System.Drawing.Point(13, 169);
			this.lstProtocol.Name = "lstProtocol";
			this.lstProtocol.Size = new System.Drawing.Size(234, 23);
			this.lstProtocol.TabIndex = 15;
			// 
			// chkAuth
			// 

			this.chkAuth.Location = new System.Drawing.Point(11, 48);
			this.chkAuth.Name = "chkAuth";
			this.chkAuth.Size = new System.Drawing.Size(206, 19);
			this.chkAuth.TabIndex = 11;
			this.chkAuth.Text = "My server requires authentication";
			this.chkAuth.CheckedChanged += new System.EventHandler(this.chkAuth_CheckedChanged);
			// 
			// chkSSL
			// 
			this.chkSSL.Location = new System.Drawing.Point(11, 137);
			this.chkSSL.Name = "chkSSL";
			this.chkSSL.Size = new System.Drawing.Size(114, 19);
			this.chkSSL.TabIndex = 14;
			this.chkSSL.Text = "SSL Connection";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(73, 104);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(175, 21);
			this.textPassword.TabIndex = 13;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(73, 76);
			this.textUser.Name = "textUser";
			this.textUser.Size = new System.Drawing.Size(175, 21);
			this.textUser.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 107);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(61, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "Password";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 76);
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
			this.textServer.Location = new System.Drawing.Point(73, 16);
			this.textServer.Name = "textServer";
			this.textServer.Size = new System.Drawing.Size(175, 21);
			this.textServer.TabIndex = 10;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(7, 300);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(74, 15);
			this.label8.TabIndex = 9;
			this.label8.Text = "Attachments";
			// 
			// textAttachments
			// 
			this.textAttachments.BackColor = System.Drawing.SystemColors.Info;
			this.textAttachments.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.textAttachments.Location = new System.Drawing.Point(79, 300);
			this.textAttachments.Name = "textAttachments";
			this.textAttachments.ReadOnly = true;
			this.textAttachments.Size = new System.Drawing.Size(440, 21);
			this.textAttachments.TabIndex = 6;
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(352, 428);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(104, 23);
			this.btnSend.TabIndex = 15;
			this.btnSend.TabStop = false;
			this.btnSend.Text = "Send";
			this.btnSend.Click += new System.EventHandler(this.button1_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 276);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(59, 15);
			this.label9.TabIndex = 15;
			this.label9.Text = "Encoding";
			// 
			// lstCharset
			// 
			this.lstCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstCharset.Location = new System.Drawing.Point(79, 271);
			this.lstCharset.Name = "lstCharset";
			this.lstCharset.Size = new System.Drawing.Size(220, 23);
			this.lstCharset.TabIndex = 5;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(567, 299);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(40, 23);
			this.btnAdd.TabIndex = 7;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(615, 299);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(52, 23);
			this.btnClear.TabIndex = 8;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// textBody
			// 
			this.textBody.Location = new System.Drawing.Point(6, 328);
			this.textBody.Name = "textBody";
			this.textBody.Size = new System.Drawing.Size(666, 94);
			this.textBody.TabIndex = 14;
			this.textBody.Text = "";
			// 
			// chkTestRecipients
			// 

			this.chkTestRecipients.Location = new System.Drawing.Point(311, 274);
			this.chkTestRecipients.Name = "chkTestRecipients";
			this.chkTestRecipients.Size = new System.Drawing.Size(131, 19);
			this.chkTestRecipients.TabIndex = 16;
			this.chkTestRecipients.Text = "Test Email Address";
			this.chkTestRecipients.CheckedChanged += new System.EventHandler(this.chkTestRecipients_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(442, 274);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111, 15);
			this.label3.TabIndex = 17;
			this.label3.Text = "Maximum Threads";
			// 
			// textThreads
			// 
			this.textThreads.Location = new System.Drawing.Point(559, 270);
			this.textThreads.Name = "textThreads";
			this.textThreads.Size = new System.Drawing.Size(48, 21);
			this.textThreads.TabIndex = 18;
			this.textThreads.Text = "10";
			// 
			// lstTo
			// 
			this.lstTo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					this.colName,
																					this.colAddress,
																					this.colStatus});
			this.lstTo.FullRowSelect = true;
			this.lstTo.GridLines = true;
			this.lstTo.Location = new System.Drawing.Point(64, 32);
			this.lstTo.Name = "lstTo";
			this.lstTo.Size = new System.Drawing.Size(328, 206);
			this.lstTo.TabIndex = 19;
			this.lstTo.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			// 
			// colAddress
			// 
			this.colAddress.Text = "Address";
			// 
			// colStatus
			// 
			this.colStatus.Text = "Status";
			this.colStatus.Width = 300;
			// 
			// btnAddTo
			// 
			this.btnAddTo.Location = new System.Drawing.Point(8, 56);
			this.btnAddTo.Name = "btnAddTo";
			this.btnAddTo.Size = new System.Drawing.Size(48, 23);
			this.btnAddTo.TabIndex = 21;
			this.btnAddTo.Text = "Add";
			this.btnAddTo.Click += new System.EventHandler(this.btnAddTo_Click);
			// 
			// btnClearTo
			// 
			this.btnClearTo.Location = new System.Drawing.Point(8, 88);
			this.btnClearTo.Name = "btnClearTo";
			this.btnClearTo.Size = new System.Drawing.Size(48, 23);
			this.btnClearTo.TabIndex = 22;
			this.btnClearTo.Text = "Clear";
			this.btnClearTo.Click += new System.EventHandler(this.btnClearTo_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(576, 428);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 23);
			this.btnCancel.TabIndex = 23;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSimple
			// 
			this.btnSimple.Location = new System.Drawing.Point(464, 428);
			this.btnSimple.Name = "btnSimple";
			this.btnSimple.Size = new System.Drawing.Size(104, 23);
			this.btnSimple.TabIndex = 24;
			this.btnSimple.TabStop = false;
			this.btnSimple.Text = "Simple Send";
			this.btnSimple.Click += new System.EventHandler(this.btnSimple_Click);
			// 
			// status
			// 
			this.status.AutoSize = true;
			this.status.Location = new System.Drawing.Point(8, 432);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(42, 15);
			this.status.TabIndex = 25;
			this.status.Text = "Ready";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(684, 462);
			this.Controls.Add(this.status);
			this.Controls.Add(this.btnSimple);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnClearTo);
			this.Controls.Add(this.btnAddTo);
			this.Controls.Add(this.lstTo);
			this.Controls.Add(this.textThreads);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.chkTestRecipients);
			this.Controls.Add(this.textBody);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lstCharset);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textAttachments);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textSubject);
			this.Controls.Add(this.textFrom);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "Form1";
			this.Text = "Form1";
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
		
		#region Cross Thread Access List Item	
		protected delegate void SetItemTextDelegate( string v, int index );
		protected void _SetItemText( string v, int index )
		{
			lstTo.Items[index].SubItems[2].Text = v;
		}

		//Why we need to change the list item text by this function.
		//Because with BeginSendMail method, all the events are fired on another
		//thread, to change the list item safety, we used this function to 
		//update list item. more detail, please refer to Control.BeginInvoke method
		// in MSDN
		protected void _CrossThreadSetItemText( string v, int index )
		{
			object[] args = new object[2];
			args[0] = v;
			args[1] = index;
			SetItemTextDelegate d = new SetItemTextDelegate( _SetItemText);
			BeginInvoke(d, args);
			
		}
		#endregion

		#region	EASendMail EventHandler
		void OnIdle( object sender, ref bool cancel )
		{
			cancel = m_bcancel;
			if( !cancel )
				Application.DoEvents();//waiting server reponse or connecting server.
			
		}

		void OnConnected(object sender, ref bool cancel )
		{
			SmtpClient oSmtp = (SmtpClient)sender;
			int index = (int)oSmtp.Tag;
			_CrossThreadSetItemText( "Connected", index );
			cancel = m_bcancel;
		}


		void OnSendingDataStream(object sender, int sent, int total, ref bool cancel )
		{
			SmtpClient oSmtp = (SmtpClient)sender;
			int index = (int)oSmtp.Tag;
			
			if( sent != total )
			{
				string v = String.Format( "Sending {0}/{1} ... ", sent, total );
				_CrossThreadSetItemText( v, index );
			}
			else
			{
				_CrossThreadSetItemText( "Disconnecting ...", index );
			}
			cancel = m_bcancel;
		}

		void OnAuthorized( object sender, ref bool cancel )
		{
			SmtpClient oSmtp = (SmtpClient)sender;
			int index = (int)oSmtp.Tag;
			_CrossThreadSetItemText( "Authorized", index );
			cancel = m_bcancel;
		}

		void OnSecuring( object sender, ref bool cancel )
		{
			SmtpClient oSmtp = (SmtpClient)sender;
			int index = (int)oSmtp.Tag;
			_CrossThreadSetItemText( "Securing ...", index );
			cancel = m_bcancel;
		}

		#endregion

		#region "Initialize Encoding List"
		private void _InitCharset()
		{
			int nIndex = 0;
			string defaultEncoding = "utf-8"; //System.Text.Encoding.Default.HeaderName;

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

		private void _InitProtocols()
		{
			lstProtocol.Items.Add("SMTP Protocol - Recommended");
			lstProtocol.Items.Add("Exchange Web Service - 2007/2010");
			lstProtocol.Items.Add("Exchange WebDav - 2000/2003");
			lstProtocol.SelectedIndex = 0;
		}

		private void _Init()
		{
			System.Text.StringBuilder s = new System.Text.StringBuilder();
			s.Append( "Hi [$name], \r\nThis sample demonstrates how to send email to mutilple recipients.\r\n\r\n" );
			s.Append( "From: [$from]\r\n" );
			s.Append( "To: <[$address]>\r\n" );
			s.Append( "Subject: [$subject]\r\n\r\n" );
		
			s.Append( "If no sever address was specified, the email will be delivered to the recipient's server directly, " );
			s.Append( "However, if you don't have a static IP address, ");
			s.Append( "many anti-spam filters will mark it as a junk-email.\r\n\r\n" );
				
			s.Append("If \"Test Email Address\" was checked, then only the recipient address will be tested and no message will be sent.\r\n");

			textBody.Text = s.ToString(); 
			
			_InitCharset();
			_InitProtocols();
			_ChangeAuthStatus();
		}
	
		void _AddInstances( ref SmtpClient[] arSmtp,
			ref SmtpClientAsyncResult[] arResult,
			int index )
		{
			int count = arSmtp.Length;
			for( int i = 0; i < count; i++ )
			{
				SmtpClient oSmtp = arSmtp[i];
				if( oSmtp == null )
				{
					//idle instance found.

					oSmtp = new SmtpClient();
					//store current list item index to object instance
					//and we can retrieve it in EASendMail events.
					oSmtp.Tag = index; 
					
					//For evaluation usage, please use "TryIt" as the license code, otherwise the 
					//"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
					//"trial version expired" exception will be thrown.

					//For licensed uasage, please use your license code instead of "TryIt", then the object
					//will never expire
					SmtpMail oMail = new SmtpMail("TryIt");

					//If you want to specify a reply address
					//oMail.Headers.ReplaceHeader( "Reply-To: <reply@mydomain>" );

					//From is a MailAddress object, in c#, it supports implicit converting from string.
					//The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"
				
					//The example code without implicit converting
					// oMail.From = new MailAddress( "Tester", "test@adminsystem.com" )
					// oMail.From = new MailAddress( "Tester<test@adminsystem.com>" )
					// oMail.From = new MailAddress( "test@adminsystem.com" )
					oMail.From = textFrom.Text;

					string name, address;
					ListViewItem item = lstTo.Items[index];
					name = item.Text;
					address = item.SubItems[1].Text;

					oMail.To.Add( new MailAddress( name, address ));
					
					oMail.Subject = textSubject.Text;
					oMail.Charset = m_arCharset[lstCharset.SelectedIndex,1];

					//replace keywords in body text.
					string body = textBody.Text;
					body = body.Replace( "[$subject]", oMail.Subject );
					body = body.Replace( "[$from]", oMail.From.ToString());
					body = body.Replace( "[$name]", name );
					body = body.Replace( "[$address]", address );

					oMail.TextBody = body;

					int y = m_arAttachment.Count;
					for( int x = 0; x < y; x++ )
					{
						//add attachment
						oMail.AddAttachment( m_arAttachment[x] as string );
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

				
					_CrossThreadSetItemText( "Connecting ...", index );

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
					
					SmtpClientAsyncResult oResult = null;
					if( !chkTestRecipients.Checked )
					{
						oResult = oSmtp.BeginSendMail(
							oServer, oMail,  null, null );
					}
					else
					{
						//Just test the email address without sending email data.
						oResult = oSmtp.BeginTestRecipients(
							null, oMail, null, null );
					}

					//Add the object instance to the array.
					arSmtp[i] = oSmtp;
					arResult[i] = oResult;
					break;
				}
			}
		}

		void _UpdateResult( ref SmtpClient oSmtp,
			ref SmtpClientAsyncResult oResult )
		{
			//Get the item index from Tag property
			int index = (int)oSmtp.Tag;
			try
			{
				if( !chkTestRecipients.Checked )
				{
					oSmtp.EndSendMail( oResult );
					_CrossThreadSetItemText( "Completed", index );
				}
				else
				{
					oSmtp.EndTestRecipients( oResult );
					_CrossThreadSetItemText( "PASS", index );
				
				}
				m_nsuccess++;
			}
			catch( SmtpTerminatedException exp )
			{
				string err = exp.Message;
				_CrossThreadSetItemText( err, index );
				m_nfailure++;
			}
			catch( SmtpServerException exp )
			{
				string err = String.Format( "Exception: Server Respond: {0}", exp.ErrorMessage );
				_CrossThreadSetItemText( err, index );
				m_nfailure++;
			}
			catch( System.Net.Sockets.SocketException exp )
			{
				string err = String.Format( "Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message );
				_CrossThreadSetItemText( err, index );
				m_nfailure++;
			}
			catch( System.ComponentModel.Win32Exception exp )
			{
				string err = String.Format( "Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message );			
				_CrossThreadSetItemText( err, index );
				m_nfailure++;
			}
			catch( System.Exception exp )
			{
				string err = String.Format( "Exception: Common: {0}", exp.Message );			
				_CrossThreadSetItemText(err, index );
				m_nfailure++;
			}

			m_nsent++;
			status.Text = String.Format( "Total {0}, Finished {1}, Succeeded {2}, Failed {3}", 
				m_ntotal, 
				m_nsent,
				m_nsuccess,
				m_nfailure);
		}

		void _WaitAllInstances( ref SmtpClient[] arSmtp,
			ref SmtpClientAsyncResult[] arResult )
		{
			bool bcontinue = false;
			do
			{
				bcontinue = false;
				int count = arSmtp.Length;
				for( int i = 0; i < count; i++ )
				{
					Application.DoEvents();
					SmtpClient oSmtp = arSmtp[i];
					if( oSmtp == null )
						continue;

					// not all object finished.
					bcontinue = true;
					SmtpClientAsyncResult oResult = arResult[i];
					if( oResult.AsyncWaitHandle.WaitOne( 0, false ))
					{
						//this message was finished, get the result by 
						//_UpdateResult
						_UpdateResult( ref oSmtp, ref oResult );
						//Set the object instanct to null.
						arSmtp[i] = null;
						arResult[i] = null;
					}
				}
			
			}while( bcontinue );
		}

		bool _WaitInstances( ref SmtpClient[] arSmtp,
			ref SmtpClientAsyncResult[] arResult )
		{
			bool b = false;
			int count = arSmtp.Length;
			for( int i = 0; i < count; i++ )
			{
				Application.DoEvents();
				SmtpClient oSmtp = arSmtp[i];
				if( oSmtp == null )
					continue;

				SmtpClientAsyncResult oResult = arResult[i];
				if( oResult.AsyncWaitHandle.WaitOne( 0, false ))
				{
					//this message was finished, get the result by 
					//_UpdateResult
					b = true;
					//Set the object instanct to null.
					_UpdateResult( ref oSmtp, ref oResult );
					arSmtp[i] = null;
					arResult[i] = null;
					break;
				}

			}
			return b;
		}
		
		private void button1_Click(object sender, System.EventArgs e)
		{
			if( textFrom.Text.Length == 0 )
			{
				MessageBox.Show( "Please input From, the format can be test@domain.com or Tester<test@domain.com>" );
				return;
			}

			int to_count = lstTo.Items.Count;
			if( to_count == 0 )
			{
				MessageBox.Show( "please add a recipient at least!" );
				return;
			}

			btnSend.Enabled = false;
			btnSimple.Enabled = false;
			btnAdd.Enabled = false;
			btnClear.Enabled = false;
			btnAddTo.Enabled = false;
			btnClearTo.Enabled = false;
			chkTestRecipients.Enabled =false;

			btnCancel.Enabled = true;

			m_bcancel = false;
			int maxInstances = 10;
			try
			{
				maxInstances = Int32.Parse(textThreads.Text);
			}
			catch( Exception ep )
			{
			
			}

			if( maxInstances < 1 )
				maxInstances = 1;

			int curInstances = 0;

			SmtpClient[] arSmtp = new SmtpClient[maxInstances];
			SmtpClientAsyncResult[] arResult = new SmtpClientAsyncResult[maxInstances];

			for( int i = 0; i < maxInstances; i++ )
			{
				arSmtp[i] = null;
			}

			
			int sent = 0;
			for( sent = 0; sent < to_count; sent++ )
			{
				lstTo.Items[sent].SubItems[2].Text = "Ready";
			}

			m_ntotal = to_count;
			m_nsent = 0;
			m_nsuccess = 0;
			m_nfailure = 0;
			status.Text = String.Format( "Total {0}, Finished {1}, Succeeded {2}, Failed {3}",
				m_ntotal, m_nsent, m_nsuccess, m_nfailure );
			
			sent = 0;
			while( sent < to_count && !m_bcancel)
			{
				if( curInstances >= maxInstances )
				{
					if( _WaitInstances( ref arSmtp, ref arResult ))
					{
						curInstances--;
					}
					else
					{
						Application.DoEvents();
						//System.Threading.Thread.Sleep( 10 );
					}
					continue;
				}

				curInstances++;
				_AddInstances( ref arSmtp, ref arResult, sent );
				sent++;
				Application.DoEvents();
				
			}

			//Wait all message sent.
			_WaitAllInstances( ref arSmtp,  ref arResult );

			if( m_bcancel )
			{
				for( ; sent < to_count; sent++ )
				{
					lstTo.Items[sent].SubItems[2].Text = "Operation was cancelled";
				}
			}
			
			btnSend.Enabled = true;
			btnSimple.Enabled = true;
			btnAdd.Enabled = true;
			btnClear.Enabled = true;
			btnAddTo.Enabled = true;
			btnClearTo.Enabled = true;
			chkTestRecipients.Enabled = true;

			btnCancel.Enabled = false;
			
		}

		private void chkAuth_CheckedChanged(object sender, System.EventArgs e)
		{
			_ChangeAuthStatus();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
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

		private void btnAddTo_Click(object sender, System.EventArgs e)
		{
			Form2 dlg = new Form2();
			if( dlg.ShowDialog( this ) == DialogResult.OK )
			{
				string name = dlg.textName.Text;
				if( name.Length == 0 )
				{
					name = "";
				}

				//for( int i = 0; i < 100; i++ )
				//{
				ListViewItem item = lstTo.Items.Add( name );
				item.SubItems.Add( dlg.textAddress.Text );
				item.SubItems.Add( "Ready" );
				//}

			}

			dlg.Dispose();
		}

		private void btnClearTo_Click(object sender, System.EventArgs e)
		{
			lstTo.Items.Clear();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			m_bcancel = true;
			btnCancel.Enabled = false;
		}

		private void chkTestRecipients_CheckedChanged(object sender, System.EventArgs e)
		{
			textServer.Enabled = (!chkTestRecipients.Checked);
			lstProtocol.SelectedIndex = 0;
			lstProtocol.Enabled = (!chkTestRecipients.Checked);
		}

		#region Send Mass E-mail with Simple Code(single thread)
		private void btnSimple_Click(object sender, System.EventArgs e)
		{
			
			if( textFrom.Text.Length == 0 )
			{
				MessageBox.Show( "Please input From, the format can be test@domain.com or Tester<test@domain.com>" );
				return;
			}

			int to_count = lstTo.Items.Count;
			if( to_count == 0 )
			{
				MessageBox.Show( "please add a recipient at least!" );
				return;
			}

			MessageBox.Show( 
				"Simple Send will send email with single thread, the code is vey simple.\r\nIf you don't want the extreme performance, the code is recommended to beginer!" );

			btnSend.Enabled = false;
			btnSimple.Enabled = false;
			btnAdd.Enabled = false;
			btnClear.Enabled = false;
			btnAddTo.Enabled = false;
			btnClearTo.Enabled = false;
			chkTestRecipients.Enabled =false;

			btnCancel.Enabled = true;
			m_bcancel = false;
			

			int sent = 0;
			for( sent = 0; sent < to_count; sent++ )
			{
				lstTo.Items[sent].SubItems[2].Text = "Ready";
			}

			m_ntotal = to_count;
			m_nsent = 0;
			m_nsuccess = 0;
			m_nfailure = 0;
			status.Text = String.Format( "Total {0}, Finished {1}, Succeeded {2}, Failed {3}",
				m_ntotal, m_nsent, m_nsuccess, m_nfailure );
			
			sent = 0;
			while( sent < to_count && !m_bcancel)
			{
				Application.DoEvents();
				int index = sent;
				sent++;
				//For evaluation usage, please use "TryIt" as the license code, otherwise the 
				//"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
				//"trial version expired" exception will be thrown.

				//For licensed uasage, please use your license code instead of "TryIt", then the object
				//will never expire
				SmtpMail oMail = new SmtpMail("TryIt");
				SmtpClient oSmtp = new SmtpClient();
				oSmtp.Tag = index;
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

					string name, address;
					ListViewItem item = lstTo.Items[index];
					name = item.Text;
					address = item.SubItems[1].Text;

					oMail.To.Add( new MailAddress( name, address ));
					
					oMail.Subject = textSubject.Text;
					oMail.Charset = m_arCharset[lstCharset.SelectedIndex,1];

					//replace keywords in body text.
					string body = textBody.Text;
					body = body.Replace( "[$subject]", oMail.Subject );
					body = body.Replace( "[$from]", oMail.From.ToString());
					body = body.Replace( "[$name]", name );
					body = body.Replace( "[$address]", address );

					oMail.TextBody = body;

					int y = m_arAttachment.Count;
					for( int x = 0; x < y; x++ )
					{
						//add attachment
						oMail.AddAttachment( m_arAttachment[x] as string );
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
				

					_CrossThreadSetItemText( "Connecting...", index );
					if(!chkTestRecipients.Checked)
					{
						oSmtp.SendMail( oServer, oMail  );
						_CrossThreadSetItemText( "Completed", index );
					}
					else
					{
						oSmtp.TestRecipients( null, oMail );
						_CrossThreadSetItemText( "PASS", index );
					}
					
					m_nsuccess ++;
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
					_CrossThreadSetItemText( err, index );
					m_nfailure++;
				}
				catch( SmtpServerException exp )
				{
					err = String.Format( "Exception: Server Respond: {0}", exp.ErrorMessage );
					_CrossThreadSetItemText( err, index );
					m_nfailure++;
				}
				catch( System.Net.Sockets.SocketException exp )
				{
					err = String.Format( "Exception: Networking Error: {0} {1}", exp.ErrorCode, exp.Message );
					_CrossThreadSetItemText( err, index );
					m_nfailure++;
				}
				catch( System.ComponentModel.Win32Exception exp )
				{
					err = String.Format( "Exception: System Error: {0} {1}", exp.ErrorCode, exp.Message );			
					_CrossThreadSetItemText( err, index );
					m_nfailure++;
				}
				catch( System.Exception exp )
				{
					err = String.Format( "Exception: Common: {0}", exp.Message );			
					_CrossThreadSetItemText( err, index );
					m_nfailure++;
				}			

				m_nsent++;
				status.Text = String.Format( "Total {0}, Finished {1}, Succeeded {2}, Failed {3}", 
					m_ntotal, 
					m_nsent,
					m_nsuccess,
					m_nfailure);				
			}

			if( m_bcancel )
			{
				for( ; sent < to_count; sent++ )
				{
					lstTo.Items[sent].SubItems[2].Text = "Operation was cancelled";
				}
			}
			
			btnSend.Enabled = true;
			btnSimple.Enabled = true;
			btnAdd.Enabled = true;
			btnClear.Enabled = true;
			btnAddTo.Enabled = true;
			btnClearTo.Enabled = true;
			chkTestRecipients.Enabled = true;

			btnCancel.Enabled = false;		
		}
		#endregion

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

			textBody.Width = this.Width - 35;
			textBody.Height = this.Height - 410;

			btnSend.Top = textBody.Height + textBody.Top + 5;
			status.Top = btnSend.Top;
			btnCancel.Top = btnSend.Top;
			btnSimple.Top = btnSend.Top;
			btnSend.Left = this.Width - 350;
			btnSimple.Left = btnSend.Left + 110;
			btnCancel.Left =  btnSimple.Left + 110;

			groupBox1.Left = this.Width - 290;

			textFrom.Width = this.Width - 380;
			textSubject.Width = textFrom.Width;
			lstTo.Width = textFrom.Width;

			textAttachments.Width = this.Width - 230;
			btnAdd.Left = textAttachments.Width + 100;
			btnClear.Left = textAttachments.Width + 150;
		}
	}
}
