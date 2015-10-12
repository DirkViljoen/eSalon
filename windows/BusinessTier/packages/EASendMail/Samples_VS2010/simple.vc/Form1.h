#pragma once


namespace simplevc {

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
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			m_bcancel = false;
			m_eventtick = 0;
			_Init();
			m_arAttachment = gcnew System::Collections::ArrayList();
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}

	protected: 


	private:
		System::Windows::Forms::Label ^label1;
		System::Windows::Forms::Label ^label2;
		System::Windows::Forms::Label ^label3;
		System::Windows::Forms::Label ^label4;
		System::Windows::Forms::GroupBox ^groupBox1;
		System::Windows::Forms::Label ^label6;
		System::Windows::Forms::Label ^label7;
		System::Windows::Forms::Label ^label8;
		System::Windows::Forms::Label ^label9;
		System::Windows::Forms::CheckBox ^chkSSL;
		System::Windows::Forms::TextBox ^textFrom;
		System::Windows::Forms::TextBox ^textTo;
		System::Windows::Forms::TextBox ^textCc;
		System::Windows::Forms::TextBox ^textSubject;
		System::Windows::Forms::TextBox ^textPassword;
		System::Windows::Forms::TextBox ^textUser;
		System::Windows::Forms::Label ^Server;
		System::Windows::Forms::TextBox ^textServer;
		System::Windows::Forms::TextBox ^textAttachments;
		System::Windows::Forms::Button ^btnSend;
		System::Windows::Forms::ProgressBar ^pgSending;
		System::Windows::Forms::StatusBar ^sbStatus;
		System::Windows::Forms::CheckBox ^chkAuth;
		System::Windows::Forms::Button ^btnAdd;
		System::Windows::Forms::Button ^btnClear;
		System::Windows::Forms::Button ^btnCancel;
		System::Windows::Forms::ComboBox ^lstCharset;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

		array<String^>^ m_arCharset;
		array<String^>^ m_arCharsetDesc;


//		System::String^ m_arCharset[];
//		System::String^ m_arCharsetDesc[];

		System::Collections::ArrayList^ m_arAttachment;
		System::Windows::Forms::RichTextBox ^textBody;
		System::Windows::Forms::OpenFileDialog ^attachmentDlg;
		System::Windows::Forms::CheckBox ^chkHtml;
		System::Windows::Forms::CheckBox ^chkSignature;
		System::Windows::Forms::CheckBox ^chkEncrypt;
	private: System::Windows::Forms::ComboBox^  lstProtocol;
			 bool m_bcancel;
			 _int64 m_eventtick;
#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->textFrom = (gcnew System::Windows::Forms::TextBox());
			this->textTo = (gcnew System::Windows::Forms::TextBox());
			this->textCc = (gcnew System::Windows::Forms::TextBox());
			this->textSubject = (gcnew System::Windows::Forms::TextBox());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->lstProtocol = (gcnew System::Windows::Forms::ComboBox());
			this->chkAuth = (gcnew System::Windows::Forms::CheckBox());
			this->chkSSL = (gcnew System::Windows::Forms::CheckBox());
			this->textPassword = (gcnew System::Windows::Forms::TextBox());
			this->textUser = (gcnew System::Windows::Forms::TextBox());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->Server = (gcnew System::Windows::Forms::Label());
			this->textServer = (gcnew System::Windows::Forms::TextBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->textAttachments = (gcnew System::Windows::Forms::TextBox());
			this->btnSend = (gcnew System::Windows::Forms::Button());
			this->pgSending = (gcnew System::Windows::Forms::ProgressBar());
			this->sbStatus = (gcnew System::Windows::Forms::StatusBar());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->lstCharset = (gcnew System::Windows::Forms::ComboBox());
			this->btnAdd = (gcnew System::Windows::Forms::Button());
			this->btnClear = (gcnew System::Windows::Forms::Button());
			this->textBody = (gcnew System::Windows::Forms::RichTextBox());
			this->attachmentDlg = (gcnew System::Windows::Forms::OpenFileDialog());
			this->chkHtml = (gcnew System::Windows::Forms::CheckBox());
			this->chkSignature = (gcnew System::Windows::Forms::CheckBox());
			this->chkEncrypt = (gcnew System::Windows::Forms::CheckBox());
			this->btnCancel = (gcnew System::Windows::Forms::Button());
			this->groupBox1->SuspendLayout();
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(8, 20);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(36, 15);
			this->label1->TabIndex = 0;
			this->label1->Text = L"From";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(8, 49);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(21, 15);
			this->label2->TabIndex = 1;
			this->label2->Text = L"To";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(8, 76);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(21, 15);
			this->label3->TabIndex = 2;
			this->label3->Text = L"Cc";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(8, 102);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(48, 15);
			this->label4->TabIndex = 3;
			this->label4->Text = L"Subject";
			// 
			// textFrom
			// 
			this->textFrom->Location = System::Drawing::Point(64, 20);
			this->textFrom->Name = L"textFrom";
			this->textFrom->Size = System::Drawing::Size(314, 21);
			this->textFrom->TabIndex = 1;
			// 
			// textTo
			// 
			this->textTo->Location = System::Drawing::Point(64, 47);
			this->textTo->Name = L"textTo";
			this->textTo->Size = System::Drawing::Size(314, 21);
			this->textTo->TabIndex = 2;
			// 
			// textCc
			// 
			this->textCc->Location = System::Drawing::Point(64, 74);
			this->textCc->Name = L"textCc";
			this->textCc->Size = System::Drawing::Size(314, 21);
			this->textCc->TabIndex = 3;
			// 
			// textSubject
			// 
			this->textSubject->Location = System::Drawing::Point(64, 101);
			this->textSubject->Name = L"textSubject";
			this->textSubject->Size = System::Drawing::Size(314, 21);
			this->textSubject->TabIndex = 4;
			this->textSubject->Text = L"Test subject";
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->lstProtocol);
			this->groupBox1->Controls->Add(this->chkAuth);
			this->groupBox1->Controls->Add(this->chkSSL);
			this->groupBox1->Controls->Add(this->textPassword);
			this->groupBox1->Controls->Add(this->textUser);
			this->groupBox1->Controls->Add(this->label7);
			this->groupBox1->Controls->Add(this->label6);
			this->groupBox1->Controls->Add(this->Server);
			this->groupBox1->Controls->Add(this->textServer);
			this->groupBox1->Location = System::Drawing::Point(407, 8);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(265, 172);
			this->groupBox1->TabIndex = 8;
			this->groupBox1->TabStop = false;
			// 
			// lstProtocol
			// 
			this->lstProtocol->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->lstProtocol->FormattingEnabled = true;
			this->lstProtocol->Location = System::Drawing::Point(11, 140);
			this->lstProtocol->Name = L"lstProtocol";
			this->lstProtocol->Size = System::Drawing::Size(241, 23);
			this->lstProtocol->TabIndex = 15;
			// 
			// chkAuth
			// 
			this->chkAuth->AutoSize = true;
			this->chkAuth->Location = System::Drawing::Point(8, 41);
			this->chkAuth->Name = L"chkAuth";
			this->chkAuth->Size = System::Drawing::Size(206, 19);
			this->chkAuth->TabIndex = 11;
			this->chkAuth->Text = L"My server requires authentication";
			this->chkAuth->CheckedChanged += gcnew System::EventHandler(this, &Form1::chkAuth_CheckedChanged);
			// 
			// chkSSL
			// 
			this->chkSSL->AutoSize = true;
			this->chkSSL->Location = System::Drawing::Point(11, 119);
			this->chkSSL->Name = L"chkSSL";
			this->chkSSL->Size = System::Drawing::Size(114, 19);
			this->chkSSL->TabIndex = 14;
			this->chkSSL->Text = L"SSL Connection";
			// 
			// textPassword
			// 
			this->textPassword->Location = System::Drawing::Point(64, 92);
			this->textPassword->Name = L"textPassword";
			this->textPassword->PasswordChar = '*';
			this->textPassword->Size = System::Drawing::Size(189, 21);
			this->textPassword->TabIndex = 13;
			// 
			// textUser
			// 
			this->textUser->Location = System::Drawing::Point(64, 67);
			this->textUser->Name = L"textUser";
			this->textUser->Size = System::Drawing::Size(189, 21);
			this->textUser->TabIndex = 12;
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(4, 94);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(61, 15);
			this->label7->TabIndex = 2;
			this->label7->Text = L"Password";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(5, 69);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(33, 15);
			this->label6->TabIndex = 1;
			this->label6->Text = L"User";
			// 
			// Server
			// 
			this->Server->AutoSize = true;
			this->Server->Location = System::Drawing::Point(5, 17);
			this->Server->Name = L"Server";
			this->Server->Size = System::Drawing::Size(42, 15);
			this->Server->TabIndex = 0;
			this->Server->Text = L"Server";
			// 
			// textServer
			// 
			this->textServer->Location = System::Drawing::Point(64, 16);
			this->textServer->Name = L"textServer";
			this->textServer->Size = System::Drawing::Size(189, 21);
			this->textServer->TabIndex = 10;
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(11, 197);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(68, 15);
			this->label8->TabIndex = 9;
			this->label8->Text = L"Attachment";
			// 
			// textAttachments
			// 
			this->textAttachments->BackColor = System::Drawing::SystemColors::Info;
			this->textAttachments->ForeColor = System::Drawing::SystemColors::HotTrack;
			this->textAttachments->Location = System::Drawing::Point(85, 194);
			this->textAttachments->Name = L"textAttachments";
			this->textAttachments->ReadOnly = true;
			this->textAttachments->Size = System::Drawing::Size(481, 21);
			this->textAttachments->TabIndex = 6;
			// 
			// btnSend
			// 
			this->btnSend->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->btnSend->Location = System::Drawing::Point(522, 407);
			this->btnSend->Name = L"btnSend";
			this->btnSend->Size = System::Drawing::Size(72, 23);
			this->btnSend->TabIndex = 15;
			this->btnSend->TabStop = false;
			this->btnSend->Text = L"Send";
			this->btnSend->Click += gcnew System::EventHandler(this, &Form1::btnSend_Click);
			// 
			// pgSending
			// 
			this->pgSending->Location = System::Drawing::Point(8, 415);
			this->pgSending->Name = L"pgSending";
			this->pgSending->Size = System::Drawing::Size(480, 8);
			this->pgSending->TabIndex = 13;
			// 
			// sbStatus
			// 
			this->sbStatus->Location = System::Drawing::Point(0, 440);
			this->sbStatus->Name = L"sbStatus";
			this->sbStatus->RightToLeft = System::Windows::Forms::RightToLeft::No;
			this->sbStatus->Size = System::Drawing::Size(684, 22);
			this->sbStatus->TabIndex = 14;
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(8, 132);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(59, 15);
			this->label9->TabIndex = 15;
			this->label9->Text = L"Encoding";
			// 
			// lstCharset
			// 
			this->lstCharset->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->lstCharset->Location = System::Drawing::Point(85, 129);
			this->lstCharset->Name = L"lstCharset";
			this->lstCharset->Size = System::Drawing::Size(194, 23);
			this->lstCharset->TabIndex = 5;
			// 
			// btnAdd
			// 
			this->btnAdd->Location = System::Drawing::Point(572, 195);
			this->btnAdd->Name = L"btnAdd";
			this->btnAdd->Size = System::Drawing::Size(40, 23);
			this->btnAdd->TabIndex = 7;
			this->btnAdd->Text = L"Add";
			this->btnAdd->Click += gcnew System::EventHandler(this, &Form1::btnAdd_Click);
			// 
			// btnClear
			// 
			this->btnClear->Location = System::Drawing::Point(618, 194);
			this->btnClear->Name = L"btnClear";
			this->btnClear->Size = System::Drawing::Size(54, 23);
			this->btnClear->TabIndex = 8;
			this->btnClear->Text = L"Clear";
			this->btnClear->Click += gcnew System::EventHandler(this, &Form1::btnClear_Click);
			// 
			// textBody
			// 
			this->textBody->Location = System::Drawing::Point(8, 224);
			this->textBody->Name = L"textBody";
			this->textBody->Size = System::Drawing::Size(664, 177);
			this->textBody->TabIndex = 14;
			this->textBody->Text = L"";
			// 
			// chkHtml
			// 
			this->chkHtml->AutoSize = true;
			this->chkHtml->Location = System::Drawing::Point(11, 163);
			this->chkHtml->Name = L"chkHtml";
			this->chkHtml->Size = System::Drawing::Size(90, 19);
			this->chkHtml->TabIndex = 16;
			this->chkHtml->Text = L"HTML Body";
			// 
			// chkSignature
			// 
			this->chkSignature->AutoSize = true;
			this->chkSignature->Location = System::Drawing::Point(110, 163);
			this->chkSignature->Name = L"chkSignature";
			this->chkSignature->Size = System::Drawing::Size(120, 19);
			this->chkSignature->TabIndex = 17;
			this->chkSignature->Text = L"Digitial Signature";
			// 
			// chkEncrypt
			// 
			this->chkEncrypt->AutoSize = true;
			this->chkEncrypt->Location = System::Drawing::Point(248, 163);
			this->chkEncrypt->Name = L"chkEncrypt";
			this->chkEncrypt->Size = System::Drawing::Size(66, 19);
			this->chkEncrypt->TabIndex = 18;
			this->chkEncrypt->Text = L"Encrypt";
			// 
			// btnCancel
			// 
			this->btnCancel->Enabled = false;
			this->btnCancel->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->btnCancel->Location = System::Drawing::Point(600, 407);
			this->btnCancel->Name = L"btnCancel";
			this->btnCancel->Size = System::Drawing::Size(72, 23);
			this->btnCancel->TabIndex = 19;
			this->btnCancel->TabStop = false;
			this->btnCancel->Text = L"Cancel";
			this->btnCancel->Click += gcnew System::EventHandler(this, &Form1::btnCancel_Click);
			// 
			// Form1
			// 
			this->AutoScaleBaseSize = System::Drawing::Size(6, 14);
			this->ClientSize = System::Drawing::Size(684, 462);
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
			this->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 9, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->Name = L"Form1";
			this->Text = L"Form1";
			this->Resize += gcnew System::EventHandler(this, &Form1::Form1_Resize);
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void btnSend_Click(System::Object^  sender, System::EventArgs^  e) {
			 
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
			SmtpMail ^oMail = gcnew SmtpMail("TryIt");
			SmtpClient ^oSmtp = gcnew SmtpClient();
			
			//To generate a log file for SMTP transaction, please use
			//oSmtp->LogFileName = "c:\\smtp.log";

			System::String ^err = "";

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
				oMail->From = gcnew EASendMail::MailAddress(textFrom->Text);
				
				//To, Cc and Bcc is a AddressCollection object, in C#, it supports implicit converting from string.
				// multiple address are separated with (,;)
				//The syntax is like this: "test@adminsystem.com, test1@adminsystem.com"

				//The example code without implicit converting
				// oMail->To = new AddressCollection( S"test1@adminsystem.com, test2@adminsystem.com" );
				// oMail->To = new AddressCollection( S"Tester1<test@adminsystem.com>, Tester2<test2@adminsystem.com>");
				oMail->To =  gcnew EASendMail::AddressCollection(textTo->Text);
				//You can add more recipient by Add method
				// oMail->To->Add( new MailAddress( S"tester", S"test@adminsystem.com"));

				oMail->Cc = gcnew EASendMail::AddressCollection(textCc->Text);

				oMail->Subject = textSubject->Text;
				oMail->Charset = m_arCharset[lstCharset->SelectedIndex];

				//digital signature and encryption
				if(!_Signencrypt( oMail ))
				{
					btnSend->Enabled = true;
					btnCancel->Enabled = false;
					return;
				}

				System::String  ^body = textBody->Text;
				body = body->Replace( "[$from]", oMail->From->ToString());
				body = body->Replace( "[$to]", oMail->To->ToString());
				body = body->Replace( "[$subject]", oMail->Subject );
				
				if( chkHtml->Checked )
					oMail->HtmlBody = body;
				else
					oMail->TextBody = body;

				int count = m_arAttachment->Count;
				for( int i = 0; i < count; i++ )
				{
					oMail->AddAttachment( dynamic_cast<System::String^>(m_arAttachment[i]));
				}

				SmtpServer ^oServer = gcnew SmtpServer( textServer->Text );
				oServer->Protocol = (ServerProtocol)lstProtocol->SelectedIndex;

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
					System::Globalization::CultureInfo ^cur = gcnew System::Globalization::CultureInfo("en-US");
					System::DateTime now = System::DateTime::Now;
					System::String ^gmtdate = now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", cur);
					gmtdate->Remove( gmtdate->Length - 3, 1 );
					System::String ^recvheader = String::Format( "from {0} ([127.0.0.1]) by {0} ([127.0.0.1]) with SMTPSVC;\r\n\t {1}",
						oServer->HeloDomain,
						gmtdate );

					oMail->Headers->Insert( 0, gcnew HeaderItem( "Received", recvheader ));
				}

				//Catching the following events is not necessary, 
				//just make the application more user friendly.
				//If you use the object in asp.net/windows service or non-gui application, 
				//You need not to catch the following events.
				//To learn more detail, please refer to the code in EASendMail EventHandler region
				oSmtp->OnIdle += gcnew SmtpClient::OnIdleEventHandler( this, &Form1::OnIdle );
				oSmtp->OnAuthorized += gcnew SmtpClient::OnAuthorizedEventHandler(this, &Form1::OnAuthorized );
				oSmtp->OnConnected += gcnew SmtpClient::OnConnectedEventHandler(this, &Form1::OnConnected );
				oSmtp->OnSecuring += gcnew SmtpClient::OnSecuringEventHandler(this, &Form1::OnSecuring );
				oSmtp->OnSendingDataStream += gcnew SmtpClient::OnSendingDataStreamEventHandler( this, &Form1::OnSendingDataStream );
				
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
				
					MessageBox::Show( String::Format( "The message was sent to {0} successfully!", 
						oSmtp->CurrentSmtpServer->Server ));

					sbStatus->Text = "Completed";
				}
				//If you want to reuse the mail object, please reset the Date and Message-ID, otherwise
				//the Date and Message-ID will not change.
				//oMail->Date = System::DateTime::Now;
				//oMail->ResetMessageID();
				//oMail->To = "another@example.com";
				//oSmtp->SendMail( oServer, oMail );
			}
			catch( EASendMail::SmtpTerminatedException ^exp )
			{
				err = exp->Message;
			}
			catch( EASendMail::SmtpServerException ^exp )
			{
				err = String::Format( "Exception: Server Respond: {0}", exp->ErrorMessage );
			}
			catch( System::Net::Sockets::SocketException ^exp )
			{
				err = String::Format( "Exception: Networking Error: {0} {1}", exp->ErrorCode.ToString("d"), exp->Message );
			}
			catch( System::ComponentModel::Win32Exception ^exp )
			{
				err = String::Format( "Exception: System Error: {0} {1}", exp->ErrorCode.ToString("d"), exp->Message );			
			}
			catch( System::Exception ^exp )
			{
				err = String::Format( "Exception: Common: {0}", exp->Message );			
			}
			
			if( err->Length > 0  )
			{
				MessageBox::Show( err );
				sbStatus->Text = err;
			}
			
			btnSend->Enabled = true;
			btnCancel->Enabled = false;
			 }

//Send E-mail without SMTP server to multiple recipients
		System::Void _DirectSend( EASendMail::SmtpMail ^oMail, EASendMail::SmtpClient ^oSmtp )
		{
			AddressCollection ^recipients = oMail->Recipients->Copy();
			int count = recipients->Count;
			for( int i = 0; i < count; i++ )
			{
				System::String ^err = "";
				MailAddress ^address = dynamic_cast<MailAddress^>(recipients[i]);

				bool terminated = false;
				try
				{
					oMail->To->Clear();
					oMail->Cc->Clear();
					oMail->Bcc->Clear();

					oMail->To->Add( address );
					SmtpServer ^oServer = gcnew SmtpServer( "" );

					sbStatus->Text = String::Format( "Connecting server for {0} ... ", address->Address );
					pgSending->Value = 0;
					oSmtp->SendMail( oServer, oMail );
					MessageBox::Show( String::Format( "The message to <{0}> was sent to {1} successfully!", 
									address->Address,
									oSmtp->CurrentSmtpServer->Server ));

					sbStatus->Text = "Completed";
					
				}
				catch( SmtpTerminatedException ^exp )
				{
					err = exp->Message;
					terminated = true;
				}
				catch( SmtpServerException ^exp )
				{
					err = String::Format( "Exception: Server Respond: {0}", exp->ErrorMessage );
				}
				catch( System::Net::Sockets::SocketException ^exp )
				{
					err = String::Format( "Exception: Networking Error: {0} {1}", exp->ErrorCode.ToString("d"), exp->Message );
				}
				catch( System::ComponentModel::Win32Exception ^exp )
				{
					err = String::Format( "Exception: System Error: {0} {1}", exp->ErrorCode.ToString("d"), exp->Message );			
				}
				catch( System::Exception ^exp )
				{
					err = String::Format( "Exception: Common: {0}", exp->Message );			
				}	


				if( terminated )
					break;
				
				if( err->Length > 0 )
				{ 
					MessageBox::Show( String::Format("The message was unable to delivery to <{0}> due to \r\n{1}",
							address->Address, err ));

					sbStatus->Text = err;
				}
			}		
		}

		bool _Signencrypt( SmtpMail ^oMail )
		{
			if( chkSignature->Checked )
			{
				try
				{
					oMail->From->Certificate->FindSubject( oMail->From->Address,
						Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
						"My" );
				}
				catch( Exception ^exp )
				{
					MessageBox::Show( 
						String::Format( "No sign certificate found for <{0}>:{1}",
						oMail->From->Address,
						exp->Message ));
					btnSend->Text = "Send";
					return false;
				}
			}

			int count = 0;
			if( chkEncrypt->Checked )
			{
				count = oMail->To->Count;
				for( int i = 0; i < count; i++ )
				{
					MailAddress ^oAddress = dynamic_cast<MailAddress^>(oMail->To[i]);
					try
					{
						oAddress->Certificate->FindSubject( oAddress->Address,
							Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
							"AddressBook" );
					}
					catch( Exception ^ep )
					{
						try
						{
							oAddress->Certificate->FindSubject( oAddress->Address,
								Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
								"My" );
						}
						catch( Exception ^exp )
						{
							MessageBox::Show( 
								String::Format(
								"No encryption certificate found for <{0}>:{1}",
								oAddress->Address,
								exp->Message ));
							return false;
						}
					}
				}

				count = oMail->Cc->Count;
				for( int i = 0; i < count; i++ )
				{
					MailAddress ^oAddress = dynamic_cast<MailAddress^>(oMail->Cc[i]);
					
					try
					{
						oAddress->Certificate->FindSubject( oAddress->Address,
							Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
							"AddressBook" );
					}
					catch( Exception ^ep )
					{
						try
						{
							oAddress->Certificate->FindSubject( oAddress->Address,
								Certificate::CertificateStoreLocation::CERT_SYSTEM_STORE_CURRENT_USER,
								"My" );
						}
						catch( Exception ^exp )
						{
							MessageBox::Show( 
								String::Format(
								"No encryption certificate found for <{0}>:{1}",
								oAddress->Address,
								exp->Message ));
							return false;
						}
					}
				}
			}

			return true;
		}

private: System::Void btnCancel_Click(System::Object^  sender, System::EventArgs^  e) {
			 btnCancel->Enabled = false;
			 m_bcancel = true;
		 }

private: System::Void btnAdd_Click(System::Object^  sender, System::EventArgs^  e) {
			attachmentDlg->Reset(); 
			attachmentDlg->Multiselect = true;
			attachmentDlg->CheckFileExists = true;
			attachmentDlg->CheckPathExists = true;
			if( attachmentDlg->ShowDialog()!= Windows::Forms::DialogResult::OK )
				return;
	
			array<System::String^>^ attachments = attachmentDlg->FileNames;
			int nLen = attachments->Length;
			for( int i = 0; i < nLen; i++ )
			{
				m_arAttachment->Add( attachments[i] );
				System::String  ^fileName = attachments[i];
				int pos = fileName->LastIndexOf( "\\" );
				if( pos != -1 )
					fileName = fileName->Substring( pos+1 );

				textAttachments->Text = System::String::Concat( textAttachments->Text, fileName );
				textAttachments->Text = System::String::Concat( textAttachments->Text, ";" );
			}
		 }

private: void _InitProtocols()
		 {
			lstProtocol->Items->Add("SMTP Protocol - Recommended");
            lstProtocol->Items->Add("Exchange Web Service - 2007/2010");
            lstProtocol->Items->Add("Exchange WebDav - 2000/2003");
            lstProtocol->SelectedIndex = 0;
		 }

private: void _InitCharset()
		{
			int nIndex = 0;
			System::String^ defaultEncoding = "UTF-8";

			m_arCharsetDesc = gcnew array<System::String^>(28);;
			m_arCharset = gcnew array<System::String^>(28);

			m_arCharsetDesc[nIndex] = "Arabic(Windows)";
			m_arCharset[nIndex] = "windows-1256";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Baltic(ISO)";
			m_arCharset[nIndex] = "iso-8859-4";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Baltic(Windows)";
			m_arCharset[nIndex] = "windows-1257";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Central Euporean(ISO)";
			m_arCharset[nIndex] = "iso-8859-2";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Central Euporean(Windows)";
			m_arCharset[nIndex] = "windows-1250";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Chinese Simplified(GB18030)";
			m_arCharset[nIndex] = "GB18030";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Chinese Simplified(GB2312)";
			m_arCharset[nIndex] = "gb2312";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Chinese Simplified(HZ)";
			m_arCharset[nIndex] = "hz-gb-2312";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Chinese Traditional(Big5)";
			m_arCharset[nIndex] = "big5";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Cyrillic(ISO)";
			m_arCharset[nIndex] = "iso-8859-5";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Cyrillic(KOI8-R)";
			m_arCharset[nIndex] = "koi8-r";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Cyrillic(KOI8-U)";
			m_arCharset[nIndex] = "koi8-u";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Cyrillic(Windows)";
			m_arCharset[nIndex] = "windows-1251";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Greek(ISO)";
			m_arCharset[nIndex] = "iso-8859-7";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Greek(Windows)";
			m_arCharset[nIndex] = "windows-1253";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Hebrew(Windows)";
			m_arCharset[nIndex] = "windows-1255";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Japanese(JIS)";
			m_arCharset[nIndex] = "iso-2022-jp";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Korean";
			m_arCharset[nIndex] = "ks_c_5601-1987";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Korean(EUC)";
			m_arCharset[nIndex] = "euc-kr";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Latin 9(ISO)";
			m_arCharset[nIndex] = "iso-8859-15";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Thai(Windows)";
			m_arCharset[nIndex] = "windows-874";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Turkish(ISO)";
			m_arCharset[nIndex] = "iso-8859-9";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Turkish(Windows)";
			m_arCharset[nIndex] = "windows-1254";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Unicode(UTF-7)";
			m_arCharset[nIndex] = "utf-7";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Unicode(UTF-8)";
			m_arCharset[nIndex] = "utf-8";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Vietnames(Windows)";
			m_arCharset[nIndex] = "windows-1258";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Western European(ISO)";
			m_arCharset[nIndex] = "iso-8859-1";
			nIndex++;

			m_arCharsetDesc[nIndex] = "Western European(Windows)";
			m_arCharset[nIndex] = "Windows-1252";
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

		void _Init()
		{
			System::Text::StringBuilder ^s = gcnew System::Text::StringBuilder();
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
			_InitProtocols();
			 _ChangeAuthStatus();
		}

		void _ChangeAuthStatus()
		{
			textUser->Enabled = chkAuth->Checked ;
			textPassword->Enabled = chkAuth->Checked ;
		}
private: System::Void btnClear_Click(System::Object^  sender, System::EventArgs^  e) {
			m_arAttachment->Clear();
			textAttachments->Text = "";

		 }

//EASendMail EventHandler
		 System::Void OnIdle( System::Object ^sender, System::Boolean % cancel )
		{
			cancel = m_bcancel;
			if( !m_bcancel )
				Application::DoEvents();//waiting server reponse or connecting server.
			
		}

		System::Void OnConnected(System::Object ^sender, System::Boolean % cancel )
		{
			_SetStatus("Connected");
			cancel = m_bcancel;
		}


		System::Void OnSendingDataStream(System::Object ^sender, int sent, int total, System::Boolean % cancel )
		{
			if( pgSending->Value == 0 )
			{
				_SetStatus("Sending ...");
			}
			_SetProgress( sent, total );
			if( sent == total )
				_SetStatus("Disconnecting ...");

		}

		System::Void OnAuthorized( System::Object ^sender, System::Boolean % cancel )
		{
			_SetStatus("Authorized");
			cancel = m_bcancel;
		}

		System::Void OnSecuring( System::Object ^sender, System::Boolean % cancel )
		{
			_SetStatus("Securing ...");
			cancel = m_bcancel;
		}
private: System::Void Form1_Resize(System::Object^  sender, System::EventArgs^  e) {

			 if (this->Width < 700)
			 {
				 this->Width = 700;
			 }

			 if (this->Height < 500)
			 {
				 this->Height = 500;
			 }

			 textBody->Width = this->Width - 35;
			 textBody->Height = this->Height - 320;

			 pgSending->Top = textBody->Height + textBody->Top + 15;
			 pgSending->Width = this->Width - 240;

			 btnSend->Top = pgSending->Top - 8;
			 btnCancel->Top =btnSend->Top;

			 btnSend->Left = pgSending->Width + 30;
			 btnCancel->Left = pgSending->Width + 125;

			 groupBox1->Left = this->Width - 320;
			 textSubject->Width = this->Width - 400;
			 textFrom->Width =textSubject->Width;
			 textTo->Width = textSubject->Width;
			 textCc->Width = textSubject->Width;

			 textAttachments->Width  = this->Width - 230;
			 btnAdd->Left = textAttachments->Width + 100;
			 btnClear->Left = textAttachments->Width + 150;

		 }
//Cross Thread Access Control
private: delegate System::Void SetStatusDelegate(String^ v);
private: delegate System::Void SetProgressDelegate(int sent, int total);

private: System::Void _SetProgressCallBack(int sent, int total)
        {
            long t = sent;
            t = t * 100;
            t = t / total;
            int x = (int)t;
            pgSending->Value = x;

			_int64 tick = System::DateTime::Now.Ticks;
            // call DoEvents every 0.2 second 
            if (tick - m_eventtick > 2000000)
            {
                // Do not call DoEvents too frequently in a very fast lan + larg email.
                m_eventtick = tick;
                Application::DoEvents();
            }

        }

private: System::Void _SetStatusCallBack(String^ v)
        {
            sbStatus->Text = v;
        }

        //Why we need to change the status text by this function.
        //Because some the events are fired on another
        //thread, to change the control value safety, we used this function to 
        //update control value. more detail, please refer to Control.BeginInvoke method
        // in MSDN
private: System::Void _SetStatus(String^ v)
		 {
			 if( InvokeRequired  )
			 {
				 array<System::Object^>^ args = gcnew array<System::Object^>(1);
				 args[0] = v;

				 SetStatusDelegate^ d = gcnew SetStatusDelegate(this, &Form1::_SetStatusCallBack);
				 BeginInvoke(d, args);
			 }
			 else
			 {
				 _SetStatusCallBack(v);
			 }
		 }
private: System::Void _SetProgress(int sent, int total)
		 {
			 if( InvokeRequired  )
			 {
				 array<System::Object^>^ args = gcnew array<System::Object^>(2);
				 args[0] = sent;
				 args[1] = total;

				 SetProgressDelegate^ d = gcnew SetProgressDelegate(this, &Form1::_SetProgressCallBack);
				 BeginInvoke(d, args);
			 }
			 else
			 {
				 _SetProgressCallBack(sent, total );
			 }
		 }
private: System::Void chkAuth_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
			 _ChangeAuthStatus();
		 }
};
}
