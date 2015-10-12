#pragma once
//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2006 - 2012  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// |    Project: It demonstrates how to use EASendMail to send text/plain email with 
// |             vc++.net.
// |
// |    File: Form1 : implementation file
// |
// |    Author: Ivan Lui ( ivan@emailarchitect.net )
//  ===============================================================================

namespace simplevc
{
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace EASendMail;

	/// <summary> 
	/// Summary for Form1
	///
	/// WARNING: If you change the name of this class, you will need to change the 
	///          'Resource File Name' property for the managed resource compiler tool 
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public __gc class Form1 : public System::Windows::Forms::Form
	{	
	public:
		Form1(void)
		{
			InitializeComponent();
			m_bcancel = false;
			_Init();
			m_arAttachment = new System::Collections::ArrayList();
		}
  
	protected:
		void Dispose(Boolean disposing)
		{
			if (disposing && components)
			{
				components->Dispose();
			}
			__super::Dispose(disposing);
		}

	private:
		System::Windows::Forms::Label *label1;
		System::Windows::Forms::Label *label2;
		System::Windows::Forms::Label *label3;
		System::Windows::Forms::Label *label4;
		System::Windows::Forms::GroupBox *groupBox1;
		System::Windows::Forms::Label *label6;
		System::Windows::Forms::Label *label7;
		System::Windows::Forms::Label *label8;
		System::Windows::Forms::Label *label9;
		System::Windows::Forms::CheckBox *chkSSL;
		System::Windows::Forms::TextBox *textFrom;
		System::Windows::Forms::TextBox *textTo;
		System::Windows::Forms::TextBox *textCc;
		System::Windows::Forms::TextBox *textSubject;
		System::Windows::Forms::TextBox *textPassword;
		System::Windows::Forms::TextBox *textUser;
		System::Windows::Forms::Label *Server;
		System::Windows::Forms::TextBox *textServer;
		System::Windows::Forms::TextBox *textAttachments;
		System::Windows::Forms::Button *btnSend;
		System::Windows::Forms::ProgressBar *pgSending;
		System::Windows::Forms::StatusBar *sbStatus;
		System::Windows::Forms::CheckBox *chkAuth;
		System::Windows::Forms::Button *btnAdd;
		System::Windows::Forms::Button *btnClear;
		System::Windows::Forms::ComboBox *lstCharset;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container * components;

		//struct{
		//	System::String *desc;
		//	System::String *charset;
		
		System::String *m_arCharset[];
		System::String *m_arCharsetDesc[];

		System::Collections::ArrayList *m_arAttachment;
		System::Windows::Forms::RichTextBox *textBody;
		System::Windows::Forms::OpenFileDialog *attachmentDlg;
		System::Windows::Forms::CheckBox *chkHtml;
		System::Windows::Forms::CheckBox *chkSignature;
		System::Windows::Forms::CheckBox *chkEncrypt;
	private: System::Windows::Forms::Button *  btnCancel;
			 bool m_bcancel;
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = new System::Windows::Forms::Label();
			this->label2 = new System::Windows::Forms::Label();
			this->label3 = new System::Windows::Forms::Label();
			this->label4 = new System::Windows::Forms::Label();
			this->textFrom = new System::Windows::Forms::TextBox();
			this->textTo = new System::Windows::Forms::TextBox();
			this->textCc = new System::Windows::Forms::TextBox();
			this->textSubject = new System::Windows::Forms::TextBox();
			this->groupBox1 = new System::Windows::Forms::GroupBox();
			this->chkAuth = new System::Windows::Forms::CheckBox();
			this->chkSSL = new System::Windows::Forms::CheckBox();
			this->textPassword = new System::Windows::Forms::TextBox();
			this->textUser = new System::Windows::Forms::TextBox();
			this->label7 = new System::Windows::Forms::Label();
			this->label6 = new System::Windows::Forms::Label();
			this->Server = new System::Windows::Forms::Label();
			this->textServer = new System::Windows::Forms::TextBox();
			this->label8 = new System::Windows::Forms::Label();
			this->textAttachments = new System::Windows::Forms::TextBox();
			this->btnSend = new System::Windows::Forms::Button();
			this->pgSending = new System::Windows::Forms::ProgressBar();
			this->sbStatus = new System::Windows::Forms::StatusBar();
			this->label9 = new System::Windows::Forms::Label();
			this->lstCharset = new System::Windows::Forms::ComboBox();
			this->btnAdd = new System::Windows::Forms::Button();
			this->btnClear = new System::Windows::Forms::Button();
			this->textBody = new System::Windows::Forms::RichTextBox();
			this->attachmentDlg = new System::Windows::Forms::OpenFileDialog();
			this->chkHtml = new System::Windows::Forms::CheckBox();
			this->chkSignature = new System::Windows::Forms::CheckBox();
			this->chkEncrypt = new System::Windows::Forms::CheckBox();
			this->btnCancel = new System::Windows::Forms::Button();
			this->groupBox1->SuspendLayout();
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(8, 16);
			this->label1->Name = S"label1";
			this->label1->Size = System::Drawing::Size(33, 17);
			this->label1->TabIndex = 0;
			this->label1->Text = S"From";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(8, 48);
			this->label2->Name = S"label2";
			this->label2->Size = System::Drawing::Size(19, 17);
			this->label2->TabIndex = 1;
			this->label2->Text = S"To";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(8, 80);
			this->label3->Name = S"label3";
			this->label3->Size = System::Drawing::Size(20, 17);
			this->label3->TabIndex = 2;
			this->label3->Text = S"Cc";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(8, 112);
			this->label4->Name = S"label4";
			this->label4->Size = System::Drawing::Size(46, 17);
			this->label4->TabIndex = 3;
			this->label4->Text = S"Subject";
			// 
			// textFrom
			// 
			this->textFrom->Location = System::Drawing::Point(64, 16);
			this->textFrom->Name = S"textFrom";
			this->textFrom->Size = System::Drawing::Size(312, 21);
			this->textFrom->TabIndex = 1;
			this->textFrom->Text = S"";
			// 
			// textTo
			// 
			this->textTo->Location = System::Drawing::Point(64, 48);
			this->textTo->Name = S"textTo";
			this->textTo->Size = System::Drawing::Size(312, 21);
			this->textTo->TabIndex = 2;
			this->textTo->Text = S"";
			// 
			// textCc
			// 
			this->textCc->Location = System::Drawing::Point(64, 80);
			this->textCc->Name = S"textCc";
			this->textCc->Size = System::Drawing::Size(312, 21);
			this->textCc->TabIndex = 3;
			this->textCc->Text = S"";
			// 
			// textSubject
			// 
			this->textSubject->Location = System::Drawing::Point(64, 112);
			this->textSubject->Name = S"textSubject";
			this->textSubject->Size = System::Drawing::Size(312, 21);
			this->textSubject->TabIndex = 4;
			this->textSubject->Text = S"Test subject";
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->chkAuth);
			this->groupBox1->Controls->Add(this->chkSSL);
			this->groupBox1->Controls->Add(this->textPassword);
			this->groupBox1->Controls->Add(this->textUser);
			this->groupBox1->Controls->Add(this->label7);
			this->groupBox1->Controls->Add(this->label6);
			this->groupBox1->Controls->Add(this->Server);
			this->groupBox1->Controls->Add(this->textServer);
			this->groupBox1->Location = System::Drawing::Point(400, 8);
			this->groupBox1->Name = S"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(280, 184);
			this->groupBox1->TabIndex = 8;
			this->groupBox1->TabStop = false;
			// 
			// chkAuth
			// 
			this->chkAuth->Location = System::Drawing::Point(8, 48);
			this->chkAuth->Name = S"chkAuth";
			this->chkAuth->Size = System::Drawing::Size(232, 24);
			this->chkAuth->TabIndex = 11;
			this->chkAuth->Text = S"My server requires authentication";
			this->chkAuth->CheckedChanged += new System::EventHandler(this, chkAuth_CheckedChanged);
			// 
			// chkSSL
			// 
			this->chkSSL->Location = System::Drawing::Point(8, 144);
			this->chkSSL->Name = S"chkSSL";
			this->chkSSL->Size = System::Drawing::Size(184, 16);
			this->chkSSL->TabIndex = 14;
			this->chkSSL->Text = S"SSL Connection";
			// 
			// textPassword
			// 
			this->textPassword->Location = System::Drawing::Point(72, 112);
			this->textPassword->Name = S"textPassword";
			this->textPassword->PasswordChar = '*';
			this->textPassword->Size = System::Drawing::Size(192, 21);
			this->textPassword->TabIndex = 13;
			this->textPassword->Text = S"";
			// 
			// textUser
			// 
			this->textUser->Location = System::Drawing::Point(72, 80);
			this->textUser->Name = S"textUser";
			this->textUser->Size = System::Drawing::Size(192, 21);
			this->textUser->TabIndex = 12;
			this->textUser->Text = S"";
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(8, 112);
			this->label7->Name = S"label7";
			this->label7->Size = System::Drawing::Size(59, 17);
			this->label7->TabIndex = 2;
			this->label7->Text = S"Password";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(8, 80);
			this->label6->Name = S"label6";
			this->label6->Size = System::Drawing::Size(31, 17);
			this->label6->TabIndex = 1;
			this->label6->Text = S"User";
			// 
			// Server
			// 
			this->Server->AutoSize = true;
			this->Server->Location = System::Drawing::Point(8, 17);
			this->Server->Name = S"Server";
			this->Server->Size = System::Drawing::Size(41, 17);
			this->Server->TabIndex = 0;
			this->Server->Text = S"Server";
			// 
			// textServer
			// 
			this->textServer->Location = System::Drawing::Point(72, 16);
			this->textServer->Name = S"textServer";
			this->textServer->Size = System::Drawing::Size(192, 21);
			this->textServer->TabIndex = 10;
			this->textServer->Text = S"";
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(8, 232);
			this->label8->Name = S"label8";
			this->label8->Size = System::Drawing::Size(73, 17);
			this->label8->TabIndex = 9;
			this->label8->Text = S"Attachments";
			// 
			// textAttachments
			// 
			this->textAttachments->BackColor = System::Drawing::SystemColors::Info;
			this->textAttachments->ForeColor = System::Drawing::SystemColors::HotTrack;
			this->textAttachments->Location = System::Drawing::Point(96, 232);
			this->textAttachments->Name = S"textAttachments";
			this->textAttachments->ReadOnly = true;
			this->textAttachments->Size = System::Drawing::Size(464, 21);
			this->textAttachments->TabIndex = 6;
			this->textAttachments->Text = S"";
			// 
			// btnSend
			// 
			this->btnSend->Location = System::Drawing::Point(528, 424);
			this->btnSend->Name = S"btnSend";
			this->btnSend->Size = System::Drawing::Size(72, 23);
			this->btnSend->TabIndex = 15;
			this->btnSend->TabStop = false;
			this->btnSend->Text = S"Send";
			this->btnSend->Click += new System::EventHandler(this, btnSend_Click);
			// 
			// pgSending
			// 
			this->pgSending->Location = System::Drawing::Point(8, 432);
			this->pgSending->Name = S"pgSending";
			this->pgSending->Size = System::Drawing::Size(488, 8);
			this->pgSending->TabIndex = 13;
			// 
			// sbStatus
			// 
			this->sbStatus->Location = System::Drawing::Point(0, 451);
			this->sbStatus->Name = S"sbStatus";
			this->sbStatus->RightToLeft = System::Windows::Forms::RightToLeft::No;
			this->sbStatus->Size = System::Drawing::Size(692, 22);
			this->sbStatus->TabIndex = 14;
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(8, 144);
			this->label9->Name = S"label9";
			this->label9->Size = System::Drawing::Size(56, 17);
			this->label9->TabIndex = 15;
			this->label9->Text = S"Encoding";
			// 
			// lstCharset
			// 
			this->lstCharset->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->lstCharset->Location = System::Drawing::Point(64, 144);
			this->lstCharset->Name = S"lstCharset";
			this->lstCharset->Size = System::Drawing::Size(168, 23);
			this->lstCharset->TabIndex = 5;
			// 
			// btnAdd
			// 
			this->btnAdd->Location = System::Drawing::Point(584, 232);
			this->btnAdd->Name = S"btnAdd";
			this->btnAdd->Size = System::Drawing::Size(40, 23);
			this->btnAdd->TabIndex = 7;
			this->btnAdd->Text = S"Add";
			this->btnAdd->Click  += new System::EventHandler(this, btnAdd_Click);
			// 
			// btnClear
			// 
			this->btnClear->Location = System::Drawing::Point(632, 232);
			this->btnClear->Name = S"btnClear";
			this->btnClear->Size = System::Drawing::Size(48, 23);
			this->btnClear->TabIndex = 8;
			this->btnClear->Text = S"Clear";
			this->btnClear->Click  += new System::EventHandler(this, btnClear_Click);
			
			// 
			// textBody
			// 
			this->textBody->Location = System::Drawing::Point(8, 264);
			this->textBody->Name = S"textBody";
			this->textBody->Size = System::Drawing::Size(672, 152);
			this->textBody->TabIndex = 14;
			this->textBody->Text = S"";
			// 
			// chkHtml
			// 
			this->chkHtml->Location = System::Drawing::Point(16, 198);
			this->chkHtml->Name = S"chkHtml";
			this->chkHtml->Size = System::Drawing::Size(80, 24);
			this->chkHtml->TabIndex = 16;
			this->chkHtml->Text = S"Html Body";
			// 
			// chkSignature
			// 
			this->chkSignature->Location = System::Drawing::Point(128, 198);
			this->chkSignature->Name = S"chkSignature";
			this->chkSignature->Size = System::Drawing::Size(120, 24);
			this->chkSignature->TabIndex = 17;
			this->chkSignature->Text = S"Digitial Signature";
			// 
			// chkEncrypt
			// 
			this->chkEncrypt->Location = System::Drawing::Point(280, 200);
			this->chkEncrypt->Name = S"chkEncrypt";
			this->chkEncrypt->Size = System::Drawing::Size(88, 20);
			this->chkEncrypt->TabIndex = 18;
			this->chkEncrypt->Text = S"Encrypt";
			// 
			// btnCancel
			// 
			this->btnCancel->Enabled = false;
			this->btnCancel->Location = System::Drawing::Point(608, 424);
			this->btnCancel->Name = S"btnCancel";
			this->btnCancel->Size = System::Drawing::Size(72, 23);
			this->btnCancel->TabIndex = 19;
			this->btnCancel->TabStop = false;
			this->btnCancel->Text = S"Cancel";
			this->btnCancel->Click += new System::EventHandler(this, btnCancel_Click);
			// 
			// Form1
			// 
			this->AutoScaleBaseSize = System::Drawing::Size(6, 14);
			this->ClientSize = System::Drawing::Size(692, 473);
			this->Controls->Add(this->btnCancel);
			this->Controls->Add(this->chkEncrypt);
			this->Controls->Add(this->chkSignature);
			this->Controls->Add(this->chkHtml);
			this->Controls->Add(this->textBody);
			this->Controls->Add(this->btnClear);
			this->Controls->Add(this->btnAdd);
			this->Controls->Add(this->lstCharset);
			this->Controls->Add(this->label9);
			this->Controls->Add(this->textAttachments);
			this->Controls->Add(this->label8);
			this->Controls->Add(this->textSubject);
			this->Controls->Add(this->textCc);
			this->Controls->Add(this->textTo);
			this->Controls->Add(this->textFrom);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->sbStatus);
			this->Controls->Add(this->pgSending);
			this->Controls->Add(this->btnSend);
			this->Controls->Add(this->groupBox1);
			this->Font = new System::Drawing::Font(S"Microsoft Sans Serif", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, (System::Byte)0);
			this->Name = S"Form1";
			this->Text = S"Form1";
			this->Resize += new System::EventHandler(this, Form1_Resize);
			this->groupBox1->ResumeLayout(false);
			this->ResumeLayout(false);

		}	




		void _Init()
		{
			System::Text::StringBuilder *s = new System::Text::StringBuilder();
			s->Append( "This sample demonstrates how to send simple email.\r\n\r\n" );
			s->Append( "From: [$from]\r\n" );
			s->Append( "To: [$to]\r\n" );
			s->Append( "Subject: [$subject]\r\n\r\n" );
			s->Append( "If no sever address was specified, the email will be delivered to the recipient's server directly," );
			s->Append( "However, if you don't have a static IP address, ");
			s->Append( "many anti-spam filters will mark it as a junk-email.\r\n\r\n" );
			s->Append( "If \"Digitial Signature\" was checked, please make sure you have the certificate for the sender address installed on " );
			s->Append( "Local User Certificate Store.\r\n\r\n" );
			s->Append( "If \"Encrypt\" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store.\r\n" );
						
			textBody->Text = s->ToString();
			_InitCharset();
			 _ChangeAuthStatus();
		}
//Initialize the Encoding List
		void _InitCharset()
		{
			int nIndex = 0;
			System::String *defaultEncoding = S"utf-8";//System::Text::Encoding::Default->HeaderName;

			m_arCharsetDesc = new System::String*[28];
			m_arCharset = new System::String*[28];

			m_arCharsetDesc[nIndex] = S"Arabic(Windows)";
			m_arCharset[nIndex] = S"windows-1256";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Baltic(ISO)";
			m_arCharset[nIndex] = S"iso-8859-4";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Baltic(Windows)";
			m_arCharset[nIndex] = S"windows-1257";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Central Euporean(ISO)";
			m_arCharset[nIndex] = S"iso-8859-2";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Central Euporean(Windows)";
			m_arCharset[nIndex] = S"windows-1250";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Chinese Simplified(GB18030)";
			m_arCharset[nIndex] = S"GB18030";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Chinese Simplified(GB2312)";
			m_arCharset[nIndex] = S"gb2312";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Chinese Simplified(HZ)";
			m_arCharset[nIndex] = S"hz-gb-2312";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Chinese Traditional(Big5)";
			m_arCharset[nIndex] = S"big5";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Cyrillic(ISO)";
			m_arCharset[nIndex] = S"iso-8859-5";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Cyrillic(KOI8-R)";
			m_arCharset[nIndex] = S"koi8-r";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Cyrillic(KOI8-U)";
			m_arCharset[nIndex] = S"koi8-u";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Cyrillic(Windows)";
			m_arCharset[nIndex] = S"windows-1251";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Greek(ISO)";
			m_arCharset[nIndex] = S"iso-8859-7";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Greek(Windows)";
			m_arCharset[nIndex] = S"windows-1253";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Hebrew(Windows)";
			m_arCharset[nIndex] = S"windows-1255";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Japanese(JIS)";
			m_arCharset[nIndex] = S"iso-2022-jp";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Korean";
			m_arCharset[nIndex] = S"ks_c_5601-1987";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Korean(EUC)";
			m_arCharset[nIndex] = S"euc-kr";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Latin 9(ISO)";
			m_arCharset[nIndex] = S"iso-8859-15";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Thai(Windows)";
			m_arCharset[nIndex] = S"windows-874";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Turkish(ISO)";
			m_arCharset[nIndex] = S"iso-8859-9";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Turkish(Windows)";
			m_arCharset[nIndex] = S"windows-1254";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Unicode(UTF-7)";
			m_arCharset[nIndex] = S"utf-7";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Unicode(UTF-8)";
			m_arCharset[nIndex] = S"utf-8";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Vietnames(Windows)";
			m_arCharset[nIndex] = S"windows-1258";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Western European(ISO)";
			m_arCharset[nIndex] = S"iso-8859-1";
			nIndex++;

			m_arCharsetDesc[nIndex] = S"Western European(Windows)";
			m_arCharset[nIndex] = S"Windows-1252";
			nIndex++;

			int selectIndex = 25; //utf-8
			for( int i = 0; i < nIndex; i++ )
			{
				lstCharset->Items->Add(m_arCharsetDesc[i]);
				if( String::Compare(
					m_arCharset[i], defaultEncoding, true ) == 0 )
				{
					selectIndex = i;
				}
			}
			
			lstCharset->SelectedIndex = selectIndex;		
		}

		void _ChangeAuthStatus()
		{
			textUser->Enabled = chkAuth->Checked ;
			textPassword->Enabled = chkAuth->Checked ;
		}

		System::Void chkAuth_CheckedChanged(System::Object *  sender, System::EventArgs *  e)
		{
			 _ChangeAuthStatus();
		}

		System::Void btnAdd_Click(System::Object *  sender, System::EventArgs *  e)
		{
			attachmentDlg->Reset(); 
			attachmentDlg->Multiselect = true;
			attachmentDlg->CheckFileExists = true;
			attachmentDlg->CheckPathExists = true;
			if( attachmentDlg->ShowDialog()!= DialogResult::OK )
				return;
	
			System::String  *attachments[] = attachmentDlg->FileNames;
			int nLen = attachments->Length;
			for( int i = 0; i < nLen; i++ )
			{
				m_arAttachment->Add( attachments[i] );
				System::String  *fileName = attachments[i];
				int pos = fileName->LastIndexOf( "\\" );
				if( pos != -1 )
					fileName = fileName->Substring( pos+1 );

				textAttachments->Text = System::String::Concat( textAttachments->Text, fileName );
				textAttachments->Text = System::String::Concat( textAttachments->Text, ";" );
			}
		 }

		System::Void btnClear_Click(System::Object *  sender, System::EventArgs *  e)
		{
			m_arAttachment->Clear();
			textAttachments->Text = S"";
		}

//Sign and Encrypt E-mail by digital certificate
		bool _Signencrypt( SmtpMail *oMail )
		{
			if( chkSignature->Checked )
			{
				try
				{
					oMail->From->Certificate->FindSubject( oMail->From->Address,
						Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
						S"My" );
				}
				catch( Exception *exp )
				{
					MessageBox::Show( 
						String::Format( S"No sign certificate found for <{0}>:{1}",
						oMail->From->Address,
						exp->Message ));
					btnSend->Text = S"Send";
					return false;
				}
			}

			int count = 0;
			if( chkEncrypt->Checked )
			{
				count = oMail->To->Count;
				for( int i = 0; i < count; i++ )
				{
					MailAddress *oAddress = dynamic_cast<MailAddress*>(oMail->To->Item[i]);
					try
					{
						oAddress->Certificate->FindSubject( oAddress->Address,
							Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
							S"AddressBook" );
					}
					catch( Exception *ep )
					{
						try
						{
							oAddress->Certificate->FindSubject( oAddress->Address,
								Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
								S"My" );
						}
						catch( Exception *exp )
						{
							MessageBox::Show( 
								String::Format(
								S"No encryption certificate found for <{0}>:{1}",
								oAddress->Address,
								exp->Message ));
							return false;
						}
					}
				}

				count = oMail->Cc->Count;
				for( int i = 0; i < count; i++ )
				{
					MailAddress *oAddress = dynamic_cast<MailAddress*>(oMail->Cc->Item[i]);
					
					try
					{
						oAddress->Certificate->FindSubject( oAddress->Address,
							Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
							S"AddressBook" );
					}
					catch( Exception *ep )
					{
						try
						{
							oAddress->Certificate->FindSubject( oAddress->Address,
								Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
								S"My" );
						}
						catch( Exception *exp )
						{
							MessageBox::Show( 
								String::Format(
								S"No encryption certificate found for <{0}>:{1}",
								oAddress->Address,
								exp->Message ));
							return false;
						}
					}
				}
			}

			return true;
		}

//Send E-mail without SMTP server to multiple recipients
		System::Void _DirectSend( EASendMail::SmtpMail *oMail, EASendMail::SmtpClient *oSmtp )
		{
			AddressCollection *recipients = oMail->Recipients->Copy();
			int count = recipients->Count;
			for( int i = 0; i < count; i++ )
			{
				System::String *err = S"";
				MailAddress *address = dynamic_cast<MailAddress*>(recipients->Item[i]);

				bool terminated = false;
				try
				{
					oMail->To->Clear();
					oMail->Cc->Clear();
					oMail->Bcc->Clear();

					oMail->To->Add( address );
					SmtpServer *oServer = new SmtpServer( S"" );

					sbStatus->Text = String::Format( S"Connecting server for {0} ... ", address->Address );
					pgSending->Value = 0;
					oSmtp->SendMail( oServer, oMail );
					MessageBox::Show( String::Format( S"The message to <{0}> was sent to {1} successfully!", 
									address->Address,
									oSmtp->CurrentSmtpServer->Server ));

					sbStatus->Text = "Completed";
					
				}
				catch( SmtpTerminatedException *exp )
				{
					err = exp->Message;
					terminated = true;
				}
				catch( SmtpServerException *exp )
				{
					err = String::Format( S"Exception: Server Respond: {0}", exp->ErrorMessage );
				}
				catch( System::Net::Sockets::SocketException *exp )
				{
					err = String::Format( S"Exception: Networking Error: {0} {1}", exp->ErrorCode.ToString(S"d"), exp->Message );
				}
				catch( System::ComponentModel::Win32Exception *exp )
				{
					err = String::Format( S"Exception: System Error: {0} {1}", exp->ErrorCode.ToString(S"d"), exp->Message );			
				}
				catch( System::Exception *exp )
				{
					err = String::Format( S"Exception: Common: {0}", exp->Message );			
				}	


				if( terminated )
					break;
				
				if( err->Length > 0 )
				{ 
					MessageBox::Show( String::Format(S"The message was unable to delivery to <{0}> due to \r\n{1}",
							address->Address, err ));

					sbStatus->Text = err;
				}
			}		
		}

 		System::Void btnSend_Click(System::Object *  sender, System::EventArgs *  e)
		 {
			if( textFrom->Text->Length == 0 )
			{
				MessageBox::Show( "Please input From!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>" );
				return;
			}

			if( textTo->Text->Length == 0 &&
				textCc->Text->Length == 0 )
			{
				MessageBox::Show( "Please input To or Cc!, the format can be test@adminsystem.com or Tester<test@adminsystem.com>, please use , or ; to separate multiple recipients" );
				return;
			}
		
			btnSend->Enabled = false;
			btnCancel->Enabled = true;
			m_bcancel = false;
			
			//For evaluation usage, please use "TryIt" as the license code, otherwise the 
			//"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
			//"trial version expired" exception will be thrown.

			//For licensed uasage, please use your license code instead of "TryIt", then the object
			//will never expire
			SmtpMail *oMail = new SmtpMail(S"TryIt");
			SmtpClient *oSmtp = new SmtpClient();
			
			//To generate a log file for SMTP transaction, please use
			//oSmtp->LogFileName = "c:\\smtp.log";

			System::String *err = S"";

			try
			{
				oMail->Reset();
				//If you want to specify a reply address
				//oMail->Headers->ReplaceHeader( S"Reply-To: <reply@mydomain>" );

				//From is a MailAddress object, in c#, it supports implicit converting from string.
				//The syntax is like this: "test@adminsystem.com" or "Tester<test@adminsystem.com>"
				
				//The example code without implicit converting
				// oMail->From = new MailAddress( S"Tester", S"test@adminsystem.com" )
				// oMail->From = new MailAddress( S"Tester<test@adminsystem.com>" )
				// oMail->From = new MailAddress( S"test@adminsystem.com" )
				oMail->From = new EASendMail::MailAddress(textFrom->Text);
				
				//To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
				// multiple address are separated with (,;)
				//The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

				//The example code without implicit converting
				// oMail->To = new AddressCollection( S"test1@adminsystem.com, test2@adminsystem.com" );
				// oMail->To = new AddressCollection( S"Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");
				oMail->To =  new EASendMail::AddressCollection(textTo->Text);
				//You can add more recipient by Add method
				// oMail->To->Add( new MailAddress( S"tester", S"test@adminsystem.com"));

				oMail->Cc = new EASendMail::AddressCollection(textCc->Text);

				oMail->Subject = textSubject->Text;
				oMail->Charset = m_arCharset[lstCharset->SelectedIndex];

				//digital signature and encryption
				if(!_Signencrypt( oMail ))
				{
					btnSend->Enabled = true;
					btnCancel->Enabled = false;
					return;
				}

				System::String  *body = textBody->Text;
				body = body->Replace( S"[$from]", oMail->From->ToString());
				body = body->Replace( S"[$to]", oMail->To->ToString());
				body = body->Replace( S"[$subject]", oMail->Subject );
				
				if( chkHtml->Checked )
					oMail->HtmlBody = body;
				else
					oMail->TextBody = body;

				int count = m_arAttachment->Count;
				for( int i = 0; i < count; i++ )
				{
					oMail->AddAttachment( dynamic_cast<System::String*>(m_arAttachment->Item[i]));
				}

				SmtpServer *oServer = new SmtpServer( textServer->Text );

				if( oServer->Server->Length != 0 )
				{
					if( chkAuth->Checked )
					{
						oServer->User = textUser->Text;
						oServer->Password = textPassword->Text;
					}

					if( chkSSL->Checked )
						oServer->ConnectType = SmtpConnectType::ConnectSSLAuto;
				}
				else
				{
					//To send email to the recipient directly(simulating the smtp server), 
					//please add a Received header, 
					//otherwise, many anti-spam filter will make it as junk email.
					System::Globalization::CultureInfo *cur = new System::Globalization::CultureInfo(S"en-US");
					System::DateTime now = System::DateTime::Now;
					System::String *gmtdate = now.ToString(S"ddd, dd MMM yyyy HH:mm:ss zzz", cur);
					gmtdate->Remove( gmtdate->Length - 3, 1 );
					System::String *recvheader = String::Format( S"from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;\r\n\t {1}",
						oServer->HeloDomain,
						gmtdate );

					oMail->Headers->Insert( 0, new HeaderItem( S"Received", recvheader ));
				}

				//Catching the following events is not necessary, 
				//just make the application more user friendly.
				//If you use the object in asp.net/windows service or non-gui application, 
				//You need not to catch the following events.
				//To learn more detail, please refer to the code in EASendMail EventHandler region
				oSmtp->OnIdle += new SmtpClient::OnIdleEventHandler( this, &Form1::OnIdle );
				oSmtp->OnAuthorized += new SmtpClient::OnAuthorizedEventHandler(this, &Form1::OnAuthorized );
				oSmtp->OnConnected += new SmtpClient::OnConnectedEventHandler(this, &Form1::OnConnected );
				oSmtp->OnSecuring += new SmtpClient::OnSecuringEventHandler(this, &Form1::OnSecuring );
				oSmtp->OnSendingDataStream += new SmtpClient::OnSendingDataStreamEventHandler( this, &Form1::OnSendingDataStream );
				
				if( oServer->Server->Length == 0 && oMail->Recipients->Count > 1 )
				{
					//To send email without specified smtp server, we have to send the emails one by one 
					// to multiple recipients. That is because every recipient has different smtp server.
					_DirectSend( oMail, oSmtp );
				}
				else
				{
					sbStatus->Text = "Connecting ... ";
					pgSending->Value = 0;

					oSmtp->SendMail( oServer, oMail );
				
					MessageBox::Show( String::Format( S"The message was sent to {0} successfully!", 
						oSmtp->CurrentSmtpServer->Server ));

					sbStatus->Text = S"Completed";
				}
				//If you want to reuse the mail object, please reset the Date and Message-ID, otherwise
				//the Date and Message-ID will not change.
				//oMail->Date = System::DateTime::Now;
				//oMail->ResetMessageID();
				//oMail->To = "another@example.com";
				//oSmtp->SendMail( oServer, oMail );
			}
			catch( EASendMail::SmtpTerminatedException *exp )
			{
				err = exp->Message;
			}
			catch( EASendMail::SmtpServerException *exp )
			{
				err = String::Format( S"Exception: Server Respond: {0}", exp->ErrorMessage );
			}
			catch( System::Net::Sockets::SocketException *exp )
			{
				err = String::Format( S"Exception: Networking Error: {0} {1}", exp->ErrorCode.ToString(S"d"), exp->Message );
			}
			catch( System::ComponentModel::Win32Exception *exp )
			{
				err = String::Format( S"Exception: System Error: {0} {1}", exp->ErrorCode.ToString(S"d"), exp->Message );			
			}
			catch( System::Exception *exp )
			{
				err = String::Format( S"Exception: Common: {0}", exp->Message );			
			}
			
			if( err->Length > 0  )
			{
				MessageBox::Show( err );
				sbStatus->Text = err;
			}
			
			btnSend->Enabled = true;
			btnCancel->Enabled = false;
		 }

//EASendMail EventHandler
		 void OnIdle( System::Object *sender, bool __gc *cancel )
		{
			*cancel = m_bcancel;
			if( !m_bcancel )
				Application::DoEvents();//waiting server reponse or connecting server.
			
		}

		void OnConnected(System::Object *sender, bool __gc *cancel )
		{
			sbStatus->Text = S"Connected";
			*cancel = m_bcancel;
		}


		void OnSendingDataStream(System::Object *sender, int sent, int total, bool __gc *cancel )
		{
			sbStatus->Text = "Sending ...";
			Int64 t = sent;
			t = t * 100;
			t = t/total;
			int x = (int)t;
			pgSending->Value = x;
			*cancel = m_bcancel;
			if( sent == total )
				sbStatus->Text = "Disconnecting ...";

			Application::DoEvents(); 
		}

		void OnAuthorized( System::Object *sender, bool __gc *cancel )
		{
			sbStatus->Text = "Authorized";
			*cancel = m_bcancel;
		}

		void OnSecuring( System::Object *sender, bool __gc *cancel )
		{
			sbStatus->Text = "Securing ...";
			*cancel = m_bcancel;
		}

		System::Void btnCancel_Click(System::Object *  sender, System::EventArgs *  e)
		 {
			 btnCancel->Enabled = false;
			 m_bcancel = true;
		 }



private: System::Void Form1_Resize(System::Object *  sender, System::EventArgs *  e)
		 {
			 if (this->Width < 700)
			 {
				 this->Width = 700;
			 }

			 if (this->Height < 500)
			 {
				 this->Height = 500;
			 }

			 textBody->Width = this->Width - 35;
			 textBody->Height = this->Height - 350;

			 pgSending->Top = textBody->Height + textBody->Top + 15;
			 pgSending->Width = this->Width - 240;

			 btnSend->Top = pgSending->Top - 8;
			 btnCancel->Top =btnSend->Top;

			 btnSend->Left = pgSending->Width + 30;
			 btnCancel->Left = pgSending->Width + 125;

			 groupBox1->Left = this->Width - 310;
			 textSubject->Width = this->Width - 400;
			 textFrom->Width =textSubject->Width;
			 textTo->Width = textSubject->Width;
			 textCc->Width = textSubject->Width;

			 textAttachments->Width  = this->Width - 230;
			 btnAdd->Left = textAttachments->Width + 100;
			 btnClear->Left = textAttachments->Width + 150;
		 }

};
}


