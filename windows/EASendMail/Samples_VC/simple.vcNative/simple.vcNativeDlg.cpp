// simple.vcNativeDlg.cpp : implementation file
//

#include "stdafx.h"
#include "simple.vcNative.h"
#include "simple.vcNativeDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

TCHAR* CsimplevcNativeDlg::m_charset[] = {
	_T("windows-1256"),
	_T("iso-8859-4"),
	_T("windows-1257"),
	_T("iso-8859-2"),
	_T("windows-1250"),
	_T("GB18030"),
	_T("gb2312"),
	_T("hz-gb-2312"),
	_T("big5"),
	_T("iso-8859-5"),
	_T("koi8-r"),
	_T("koi8-u"),
	_T("windows-1251"),
	_T("iso-8859-7"),
	_T("windows-1253"),
	_T("windows-1255"),
	_T("iso-2022-jp"),
	_T("ks_c_5601-1987"),
	_T("euc-kr"),
	_T("iso-8859-15"),
	_T("windows-874"),
	_T("iso-8859-9"),
	_T("windows-1254"),
	_T("utf-7"),
	_T("utf-8"),
	_T("windows-1258"),
	_T("iso-8859-1"),
	_T("Windows-1252"),
	NULL
};

TCHAR* CsimplevcNativeDlg::m_charsetName[] = {
	_T("Arabic(Windows)"),
	_T("Baltic(ISO)"),
	_T("Baltic(Windows)"),
	_T("Central Euporean(ISO)"),
	_T("Central Euporean(Windows)"),
	_T("Chinese Simplified(GB18030)"),
	_T("Chinese Simplified(GB2312)"),
	_T("Chinese Simplified(HZ)"),
	_T("Chinese Traditional(Big5)"),
	_T("Cyrillic(ISO)"),    
	_T("Cyrillic(KOI8-R)"),      
	_T("Cyrillic(KOI8-U)"),
	_T("Cyrillic(Windows)"),
	_T("Greek(ISO)"),
	_T("Greek(Windows)"),  
	_T("Hebrew(Windows)"),  
	_T("Japanese(JIS)"),        
	_T("Korean"),                     
	_T("Korean(EUC)"),
	_T("Latin 9(ISO)"),    
	_T("Thai(Windows)"),    
	_T("Turkish(ISO)"),
	_T("Turkish(Windows)"),   
	_T("Unicode(UTF-7)"),            
	_T("Unicode(UTF-8)"),
	_T("Vietnames(Windows)"),
	_T("Western European(ISO)"),    
	_T("Western European(Windows)"),
	NULL
};
// CsimplevcNativeDlg dialog

CsimplevcNativeDlg::CsimplevcNativeDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CsimplevcNativeDlg::IDD, pParent)
	, m_from(_T(""))
	, m_to(_T(""))
	, m_cc(_T(""))
	, m_bSign(FALSE)
	, m_bEncrypt(FALSE)
	, m_server(_T(""))
	, m_bAuth(FALSE)
	, m_user(_T(""))
	, m_password(_T(""))
	, m_bSSL(FALSE)
	, m_attachments(_T(""))
	, m_body(_T(""))
	, m_subject(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CsimplevcNativeDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT1, m_from);
	DDV_MaxChars(pDX, m_from, 255);
	DDX_Text(pDX, IDC_EDIT2, m_to);
	DDX_Text(pDX, IDC_EDIT3, m_cc);
	DDX_Text(pDX, IDC_EDIT4, m_subject);
	DDX_Check(pDX, IDC_CHECK3, m_bSign);
	DDX_Check(pDX, IDC_CHECK4, m_bEncrypt);
	DDX_Text(pDX, IDC_EDIT5, m_server);
	DDV_MaxChars(pDX, m_server, 255);
	DDX_Check(pDX, IDC_CHECK1, m_bAuth);
	DDX_Text(pDX, IDC_EDIT6, m_user);
	DDV_MaxChars(pDX, m_user, 255);
	DDX_Text(pDX, IDC_EDIT7, m_password);
	DDV_MaxChars(pDX, m_password, 255);
	DDX_Check(pDX, IDC_CHECK2, m_bSSL);
	DDX_Control(pDX, IDC_COMBO1, m_lstCharset);
	DDX_Text(pDX, IDC_EDIT8, m_attachments);
	DDX_Text(pDX, IDC_EDIT9, m_body);
	DDX_Control(pDX, IDC_COMBO2, m_lstProtocol);
}

BEGIN_MESSAGE_MAP(CsimplevcNativeDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_CHECK1, &CsimplevcNativeDlg::OnBnClickedCheck1)
	ON_BN_CLICKED(IDC_BUTTON2, &CsimplevcNativeDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON1, &CsimplevcNativeDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDOK, &CsimplevcNativeDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CsimplevcNativeDlg message handlers

BOOL CsimplevcNativeDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	::CoInitialize( NULL ); //initialize activex environment

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	m_body = _T("This sample demonstrates how to send simple email.\r\n\r\n");
	m_body +=  _T("From: [$from]\r\n");
	m_body += _T("To: [$to]\r\n");
	m_body += _T("Subject: [$subject]\r\n\r\n");
	m_body += _T("If no sever address was specified, the email will be delivered to the recipient's server directly. \r\n" );
	m_body += _T("However, if you don't have a static IP address, many anti-spam filters will mark it as a junk-email.");
	m_subject = _T("Test sample");

	InitCharset();

	UpdateData( FALSE );

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CsimplevcNativeDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CsimplevcNativeDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void	
CsimplevcNativeDlg::InitCharset()
{
	INT n = 0;
	while( m_charsetName[n] != NULL )
	{
		m_lstCharset.AddString( m_charsetName[n] );
		n++;
	}

	m_lstCharset.SetCurSel( 24 );

	m_lstProtocol.AddString(_T("SMTP Protocol - Recommended"));
	m_lstProtocol.AddString(_T("Exchange Web Service - 2007/2010"));
	m_lstProtocol.AddString(_T("Exchange WebDav - 2000/2003"));

	m_lstProtocol.SetCurSel( 0 );

}

void CsimplevcNativeDlg::OnBnClickedCheck1()
{
	CButton* pBtn = (CButton*)GetDlgItem( IDC_CHECK1 );
	BOOL b = (pBtn->GetCheck() == BST_CHECKED);
	CWnd *pWnd = GetDlgItem( IDC_EDIT6 );
	pWnd->EnableWindow( b );
	pWnd = GetDlgItem( IDC_EDIT7 );
	pWnd->EnableWindow( b );
	
}

void CsimplevcNativeDlg::OnBnClickedButton2()
{
	m_attachments = _T("");
	m_arAtt.RemoveAll();
	CWnd *pWnd = GetDlgItem( IDC_EDIT8 );
	pWnd->SetWindowText( _T(""));
}

void CsimplevcNativeDlg::OnBnClickedButton1()
{
	CFileDialog *pFileDlg = new CFileDialog( TRUE );
	pFileDlg->m_ofn.Flags |= OFN_ALLOWMULTISELECT;
	pFileDlg->m_ofn.Flags |= OFN_FILEMUSTEXIST;

	m_attachments;
	if( pFileDlg->DoModal() == IDOK )
	{
		POSITION pos = pFileDlg->GetStartPosition();
		while( pos != NULL )
		{
			CString fileName = pFileDlg->GetNextPathName( pos );
			m_arAtt.Add( fileName );
			INT nIndex = fileName.ReverseFind('\\');
			if( nIndex != -1 )
				fileName = fileName.Mid( nIndex + 1 );

			m_attachments += fileName;
			m_attachments += ";";
		}
	}
 
	CWnd *pWnd = GetDlgItem( IDC_EDIT8 );
	pWnd->SetWindowText( m_attachments );

	delete pFileDlg;	
}

void CsimplevcNativeDlg::OnBnClickedOk()
{
	if(!UpdateData( TRUE ))
		return;
	
	m_from = m_from.Trim();
	m_to = m_to.Trim();
	m_cc = m_cc.Trim();

	CWnd *pWnd = NULL;
	if( m_from.GetLength() == 0 )
	{
		MessageBox(_T("Please input From email address!"), _T("Error"), MB_OK | MB_ICONERROR );
		pWnd = GetDlgItem( IDC_EDIT1 );
		pWnd->SetFocus();
        return;
	}

	if( m_to.GetLength() == 0 &&
		m_cc.GetLength()== 0 )
	{
		MessageBox(_T("Please input To or Cc email addresses, please use comma(,) to separate multiple addresses"), _T("Error"), MB_OK | MB_ICONERROR );
		pWnd = GetDlgItem( IDC_EDIT2 );
		pWnd->SetFocus();
        return;		
	}

	IMailPtr oSmtp = NULL;
	oSmtp.CreateInstance( __uuidof(EASendMailObjLib::Mail));

	if( oSmtp == NULL )
	{
		MessageBox( _T("Please make sure you copied EASendMailObj.dll to your exe folder or installed EASendMail ActiveX Object!"), _T("Error"), MB_OK );
		return;
	}
    //The license code for EASendMail ActiveX Object,
    //for evaluation usage, please use "TryIt" as the license code.
	oSmtp->LicenseCode = _T("TryIt");
	oSmtp->Charset = m_charset[m_lstCharset.GetCurSel()];
	//oSmtp.LogFileName = _T("d:\\smtp.txt");  //enable smtp log
	
	
	oSmtp->ServerAddr = (LPCTSTR)m_server;
	oSmtp->Protocol = m_lstProtocol.GetCurSel();

	if( m_server.GetLength() > 0 )
	{
		if( m_bAuth )
		{
			oSmtp->UserName = (LPCTSTR)m_user;
			oSmtp->Password = (LPCTSTR)m_password;
		}

		if( m_bSSL )
		{
			oSmtp->SSL_init();
			//If SSL port is 465 or other port rather than 25 or 587 port, please use
			//oSmtp->ServerPort = 465;
			//oSmtp->SSL_starttls = 0;
		}
	}

	CString name = _T("");
	CString addr = _T("");
	
	_ParseEmailAddr( m_from, name, addr );
	
	oSmtp->From = (LPCTSTR)name;
	oSmtp->FromAddr = (LPCTSTR)addr;

	//add digital signature
	oSmtp->SignerCert->Unload();
	if( m_bSign )
	{
		if( oSmtp->SignerCert->FindSubject((LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER , _T("my")) == VARIANT_FALSE )
		{
			CString error = _T("Error with signer certificate; " ); 
			error.Append( oSmtp->SignerCert->GetLastError());
			MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR );
			return;
		}

		if( oSmtp->SignerCert->HasPrivateKey == VARIANT_FALSE )
		{
			CString error = _T("Signer certificate does not have a private key, it can not be used to sign email." ); 
			MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR );
			return;			
		}
	}

	oSmtp->AddRecipientEx((LPCTSTR)m_to, 0 ); // 0, Normal recipient, 1, cc, 2, bcc
	oSmtp->AddRecipientEx((LPCTSTR)m_cc, 0 );

	CString rcpts = m_to;
	rcpts.Append( _T(","));
	rcpts.Append( m_cc );
	rcpts = rcpts.Trim( _T(","));

	CStringArray arRcpt;
	_SplitEx( rcpts, arRcpt );

	
	oSmtp->RecipientsCerts->Clear();
	//encrypt email, lookup certificate in current user store by email address.
	int count = arRcpt.GetSize();
	if( m_bEncrypt )
	{
		for( int i = 0; i < count; i++ )
		{
			_ParseEmailAddr( arRcpt[i], name, addr );
			if( addr.GetLength() > 0 )
			{
				ICertificatePtr oCert = NULL;
				oCert.CreateInstance(  __uuidof(EASendMailObjLib::Certificate));
				if( oCert->FindSubject( (LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER, _T("AddressBook")) == VARIANT_FALSE )
				{
					if(oCert->FindSubject( (LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER, _T("my")) == VARIANT_FALSE )
					{
						CString error = _T("Encrypting certificate not found; " ); 
						error.Append( oCert->GetLastError());
						MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR );
						return;
					}
				}

				oSmtp->RecipientsCerts->Add( oCert );
			}
		}
	}

	count = m_arAtt.GetSize();
	for( int i = 0; i < count; i++ )
	{
		if( oSmtp->AddAttachment((LPCTSTR)m_arAtt[i] ) != 0 )
		{
			CString error = _T("Error with attachment; " ); 
			error.Append( oSmtp->GetLastErrDescription());
			MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR );
			return;	
		}
	}

	oSmtp->Subject = (LPCTSTR)m_subject;
	CString body = m_body;
	body.Replace( _T("[$from]"), m_from );
	body.Replace( _T("[$to]"), rcpts );
	body.Replace( _T("[$subject]"), m_subject );
	
	oSmtp->BodyText = (LPCTSTR)body;
	//oSmtp->BodyFormat = 1; //' Using HTML FORMAT to send mail

	pWnd = GetDlgItem( IDOK );
	pWnd->EnableWindow( FALSE );
	pWnd = GetDlgItem( IDCANCEL );
	pWnd->EnableWindow( FALSE );
	
	if( rcpts.Find( _T(",")) > 0  && m_server.GetLength() == 0 )
	{
		//To send email without specified smtp server, we have to send the emails one by one
		// to multiple recipients. That is because every recipient has different smtp server.
		DirectSend( oSmtp, rcpts );
		pWnd = GetDlgItem( IDOK );
		pWnd->EnableWindow( TRUE );
		pWnd = GetDlgItem( IDCANCEL );
		pWnd->EnableWindow( TRUE );
		return;
	}

	if( oSmtp->SendMail() == 0 )
	{
		MessageBox( _T("Message delivered."), _T("OK"), MB_OK );
	}
	else
	{
		CString error = _T("Failed to delivery email; " );
		error.Append( oSmtp->GetLastErrDescription());
		MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR ); 
	}

	pWnd = GetDlgItem( IDOK );
	pWnd->EnableWindow( TRUE );
	pWnd = GetDlgItem( IDCANCEL );
	pWnd->EnableWindow( TRUE);

}

void CsimplevcNativeDlg::DirectSend( IMailPtr &oSmtp, LPCTSTR lpszRcpts )
{
	CStringArray arRcpt;
	_SplitEx( lpszRcpts, arRcpt );
	int count = arRcpt.GetSize();
	for( int i = 0; i < count; i++ )
	{
		CString addr = arRcpt[i];
		oSmtp->ClearRecipient();
		oSmtp->AddRecipientEx((LPCTSTR)addr, 0 );

		if( oSmtp->SendMail() == 0 )
		{
			CString s = _T("Message delivered to: ");
			s.Append( addr );
			s.Append( _T(" successfully!"));
			MessageBox( s, _T("OK"), MB_OK );
		}
		else
		{
			CString s = _T("Failed to delivery to: ");
			s.Append( addr );
			s.Append( _T(": "));
			s.Append( oSmtp->GetLastErrDescription());
			MessageBox( s, _T("Error"), MB_OK | MB_ICONERROR ); 
		}
	}

}


void	
CsimplevcNativeDlg::_ParseEmailAddr( LPCTSTR lpszSrc, CString &name, CString &addr )
{
	name = _T("");
	addr = _T("");

	if( lpszSrc == NULL )
		return;

	LPCTSTR pszBuf = _tcschr( lpszSrc, _T('<'));
	if( pszBuf == NULL )
	{
		addr = lpszSrc;
	}
	else
	{
		name.Append( lpszSrc, pszBuf - lpszSrc );
		addr.Append( pszBuf );
	}

	name = name.Trim( _T(" \"<>"));
	addr = addr.Trim( _T(" \"<>"));
}

// split multiple addresses to an string array 
void
CsimplevcNativeDlg::_SplitEx( LPCTSTR lpszSrc, CStringArray &ar )
{
	ar.RemoveAll();

	if( lpszSrc == NULL )
		return;

	LPCTSTR	pszStart	= lpszSrc;
	LPCTSTR	pszBuf		= pszStart;
	BOOL	bQuoted		= FALSE;
 
	while((pszBuf = ::_tcspbrk( pszBuf, _T("\",;"))) != NULL )
	{
		if( *pszBuf == _T('\"'))
		{
			bQuoted = !bQuoted;
			pszBuf++;	
			continue;
		}

		if( bQuoted )
		{
			pszBuf++;
			continue;
		}

		CString s;
		s.Append( pszStart, pszBuf - pszStart );
	
		s = s.Trim(_T(" ;,\r\n\t"));
		if( s.GetLength() > 0 )
		{
			ar.Add( s );
		}

		pszBuf++;
		pszStart = pszBuf;
	}

	CString s1;
	s1.Append( pszStart );
	s1= s1.Trim(_T(" ;,\r\n\t"));
	if( s1.GetLength() > 0 )
	{
		ar.Add( s1 );
	}
}
