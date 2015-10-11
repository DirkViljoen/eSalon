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
	, m_subject(_T(""))
	, m_bInitBody( FALSE )
	, m_bError( FALSE )
	, m_bCancel( FALSE )
	, m_bIdle( TRUE )
	, m_lastErrDescription(_T(""))
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
	DDX_Control(pDX, IDC_EXPLORER1, webMail);
	DDX_Control(pDX, IDC_COMBO2, m_lstFont);
	DDX_Control(pDX, IDC_COMBO3, m_lstSize);
	DDX_Control(pDX, IDC_COMBO4, m_lstProtocol);
}

BEGIN_MESSAGE_MAP(CsimplevcNativeDlg, CDialog)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_CHECK1, &CsimplevcNativeDlg::OnBnClickedCheck1)
	ON_BN_CLICKED(IDC_BUTTON2, &CsimplevcNativeDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON1, &CsimplevcNativeDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDOK, &CsimplevcNativeDlg::OnBnClickedOk)
	ON_CBN_SELCHANGE(IDC_COMBO2, &CsimplevcNativeDlg::OnCbnSelchangeCombo2)
	ON_CBN_SELCHANGE(IDC_COMBO3, &CsimplevcNativeDlg::OnCbnSelchangeCombo3)
	ON_BN_CLICKED(IDC_BUTTON3, &CsimplevcNativeDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON4, &CsimplevcNativeDlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON5, &CsimplevcNativeDlg::OnBnClickedButton5)
	ON_BN_CLICKED(IDC_BUTTON7, &CsimplevcNativeDlg::OnBnClickedButton7)
	ON_BN_CLICKED(IDC_BUTTON6, &CsimplevcNativeDlg::OnBnClickedButton6)
	ON_BN_CLICKED(IDCANCELSEND, &CsimplevcNativeDlg::OnBnClickedCancelsend)
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
	m_subject = _T("Test sample");
	InitCharset();
	
	m_oSmtp.CreateInstance( __uuidof(EASendMailObjLib::Mail));
	if( m_oSmtp == NULL )
	{
		MessageBox( _T("Please make sure you copied EASendMailObj.dll to your exe folder or installed EASendMail ActiveX Object!"), _T("Error"), MB_OK );
		CDialog::OnCancel();
		return FALSE;
	}
    //The license code for EASendMail ActiveX Object,
    //for evaluation usage, please use "TryIt" as the license code.
	m_oSmtp->LicenseCode = _T("TryIt");

	DispEventAdvise(m_oSmtp.GetInterfacePtr()); //attach event handler

	TCHAR* arFont[] = {
		_T("Choose Font Style ..."),
		_T("Allegro BT"),
		_T("Arial"),
		_T("Arial Baltic"),
		_T("Arial Black"),
		_T("Arial CE"),
		_T("Arial CYR"),
		_T("Arial Greek"),
		_T("Arial Narrow"),
		_T("Arial TUR"),
		_T("AvantGarde Bk BT"),
		_T("BankGothic Md BT"),
		_T("Basemic"),
		_T("Basemic Symbol"),
		_T("Basemic Times"),
		_T("Batang"),
		_T("BatangChe"),
		_T("Benguiat Bk BT"),
		_T("BernhardFashion BT"),
		_T("BernhardMod BT"),
		_T("Book Antiqua"),
		_T("Bookman Old Style"),
		_T("Bremen Bd BT"),
		_T("Century Gothic"),
		_T("Charlesworth"),
		_T("Comic Sans MS"),
		_T("CommonBullets"),
		_T("CopprplGoth Bd BT"),
		_T("Courier"),
		_T("Courier New"),
		_T("Courier New Baltic"),
		_T("Courier New CE"),
		_T("Courier New CYR"),
		_T("Courier New Greek"),
		_T("Courier New TUR"),
		_T("Dauphin"),
		_T("Dotum"),
		_T("DotumChe"),
		_T("Dungeon"),
		_T("English111 Vivace BT"),
		_T("Estrangelo Edessa"),
		_T("Fixedsys"),
		_T("Franklin Gothic Medium"),
		_T("Futura Lt BT"),
		_T("Futura Md BT"),
		_T("Futura XBlk BT"),
		_T("FuturaBlack BT"),
		_T("Garamond"),
		_T("Gautami"),
		_T("Georgia"),
		_T("GoudyHandtooled BT"),
		_T("GoudyOlSt BT"),
		_T("Gulim"),
		_T("GulimChe"),
		_T("Gungsuh"),
		_T("GungsuhChe"),
		_T("Haettenschweiler"),
		_T("Humanst521 BT"),
		_T("Impact"),
		_T("Kabel Bk BT"),
		_T("Kabel Ult BT"),
		_T("Kingsoft Phonetic Plain"),
		_T("Latha"),
		_T("Lithograph"),
		_T("LithographLight"),
		_T("Lucida Console"),
		_T("Lucida Sans Unicode"),
		_T("Mangal"),
		_T("Marlett"),
		_T("Microsoft Sans Serif"),
		_T("MingLiU"),
		_T("Modern"),
		_T("Monotype Corsiva"),
		_T("MS Gothic"),
		_T("MS Mincho"),
		_T("MS Outlook"),
		_T("MS PGothic"),
		_T("MS PMincho"),
		_T("MS Sans Serif"),
		_T("MS Serif"),
		_T("MS UI Gothic"),
		_T("MT Extra"),
		_T("MV Boli"),
		_T("Myriad Condensed Web"),
		_T("Myriad Web"),
		_T("OzHandicraft BT"),
		_T("Palatino Linotype"),
		_T("PMingLiU"),
		_T("PosterBodoni BT"),
		_T("Raavi"),
		_T("Roman"),
		_T("Script"),
		_T("Serifa BT"),
		_T("Serifa Th BT"),
		_T("Shruti"),
		_T("Small Fonts"),
		_T("Souvenir Lt BT"),
		_T("Staccato222 BT"),
		_T("Swiss911 XCm BT"),
		_T("Sylfaen"),
		_T("Symbol"),
		_T("System"),
		_T("Tahoma"),
		_T("Terminal"),
		_T("Times New Roman"),
		_T("Times New Roman Baltic"),
		_T("Times New Roman CE"),
		_T("Times New Roman CYR"),
		_T("Times New Roman Greek"),
		_T("Times New Roman TUR"),
		_T("Trebuchet MS"),
		_T("Tunga"),
		_T("TypoUpright BT"),
		_T("Verdana"),
		_T("VisualUI"),
		_T("Webdings"),
		_T("Wingdings"),
		_T("Wingdings 2"),
		_T("Wingdings 3"),
		_T("WP Arabic Sihafa"),
		_T("WP ArabicScript Sihafa"),
		_T("WP BoxDrawing"),
		_T("WP CyrillicA"),
		_T("WP CyrillicB"),
		_T("WP Greek Century"),
		_T("WP Greek Courier"),
		_T("WP Greek Helve"),
		_T("WP Hebrew David"),
		_T("WP IconicSymbolsA"),
		_T("WP IconicSymbolsB"),
		_T("WP Japanese"),
		_T("WP MathA"),
		_T("WP MathB"),
		_T("WP MathExtendedA"),
		_T("WP MathExtendedB"),
		_T("WP MultinationalA Courier"),
		_T("WP MultinationalA Helve"),
		_T("WP MultinationalA Roman"),
		_T("WP MultinationalB Courier"),
		_T("WP MultinationalB Helve"),
		_T("WP MultinationalB Roman"),
		_T("WP Phonetic"),
		_T("WP TypographicSymbols"),
		_T("WST_Czec"),
		_T("WST_Engl"),
		_T("WST_Fren"),
		_T("WST_Germ"),
		_T("WST_Ital"),
		_T("WST_Span"),
		_T("WST_Swed"),
		_T("ZapfEllipt BT"),
		_T("Zurich Ex BT"),
		NULL };

	INT Index = 0;
	while( arFont[Index] != NULL )
	{
		m_lstFont.AddString( arFont[Index] );
		Index++;
	}
	m_lstFont.SetCurSel( 0 );
	
	
	m_lstSize.AddString( _T("Font Size ... "));
	for( int i = 1; i <= 7; i++ )
	{
		CString size;
		size.Format( _T("%d"), i );
		m_lstSize.AddString( size );
	}
	m_lstSize.SetCurSel( 0 );

	webMail.Navigate( _T("about:blank"), NULL, NULL, NULL, NULL );
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();
	spDoc->put_designMode( L"on" );
	spDoc.Release();


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

	m_oSmtp->Reset();
	m_oSmtp->Asynchronous = 1;
	m_oSmtp->Charset = m_charset[m_lstCharset.GetCurSel()];
	//m_oSmtp->LogFileName = _T("d:\\smtp.txt");  //enable smtp log

	
	m_oSmtp->ServerAddr = (LPCTSTR)m_server;
	m_oSmtp->Protocol = m_lstProtocol.GetCurSel();

	if( m_server.GetLength() > 0 )
	{
		if( m_bAuth )
		{
			m_oSmtp->UserName = (LPCTSTR)m_user;
			m_oSmtp->Password = (LPCTSTR)m_password;
		}

		if( m_bSSL )
		{
			m_oSmtp->SSL_init();
			//If SSL port is 465 or other port rather than 25 or 587 port, please use
			//oSmtp->ServerPort = 465;
			//oSmtp->SSL_starttls = 0;
		}
		else
		{
			m_oSmtp->SSL_uninit();
		}
	}

	CString name = _T("");
	CString addr = _T("");
	
	_ParseEmailAddr( m_from, name, addr );
	
	m_oSmtp->From = (LPCTSTR)name;
	m_oSmtp->FromAddr = (LPCTSTR)addr;

	//add digital signature
	m_oSmtp->SignerCert->Unload();
	if( m_bSign )
	{
		if( m_oSmtp->SignerCert->FindSubject((LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER , _T("my")) == VARIANT_FALSE )
		{
			CString error = _T("Error with signer certificate; " ); 
			error.Append( m_oSmtp->SignerCert->GetLastError());
			MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR );
			return;
		}

		if( m_oSmtp->SignerCert->HasPrivateKey == VARIANT_FALSE )
		{
			CString error = _T("Signer certificate does not have a private key, it can not be used to sign email." ); 
			MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR );
			return;			
		}
	}

	m_oSmtp->AddRecipientEx((LPCTSTR)m_to, 0 ); // 0, Normal recipient, 1, cc, 2, bcc
	m_oSmtp->AddRecipientEx((LPCTSTR)m_cc, 0 );

	CString rcpts = m_to;
	rcpts.Append( _T(","));
	rcpts.Append( m_cc );
	rcpts = rcpts.Trim( _T(","));

	CStringArray arRcpt;
	_SplitEx( rcpts, arRcpt );

	
	m_oSmtp->RecipientsCerts->Clear();
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
				oCert.CreateInstance( __uuidof(EASendMailObjLib::Certificate));
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

				m_oSmtp->RecipientsCerts->Add( oCert );
			}
		}
	}

	count = m_arAtt.GetSize();
	for( int i = 0; i < count; i++ )
	{
		if( m_oSmtp->AddAttachment((LPCTSTR)m_arAtt[i] ) != 0 )
		{
			CString error = _T("Error with attachment; " ); 
			error.Append( m_oSmtp->GetLastErrDescription());
			MessageBox( error, _T("Error"), MB_OK | MB_ICONERROR );
			return;	
		}
	}

	m_oSmtp->Subject = (LPCTSTR)m_subject;


	
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();

	CComPtr<IHTMLElement> spElement;
	spDoc->get_body(&spElement );

	BSTR bstr = NULL;
	spElement->get_innerHTML( &bstr );
	
	CString body = bstr;
	::SysFreeString( bstr );
	spElement.Release();
	spDoc.Release();

	body.Replace( _T("[$from]"), m_from );
	body.Replace( _T("[$to]"), rcpts );
	body.Replace( _T("[$subject]"), m_subject );
	
	TCHAR szPath[MAX_PATH+1];
	memset( szPath, 0, sizeof(szPath));
	::GetModuleFileName( NULL, szPath, MAX_PATH );

	LPCTSTR pszBuf = _tcsrchr( szPath, _T('\\'));
	if( pszBuf != NULL )
	{
		szPath[pszBuf-szPath] = _T('\0');
	}

	//imports html with embedded pictures
	m_oSmtp->ImportHtml((LPCTSTR)body, szPath );
	//m_oSmtp->BodyText = (LPCTSTR)body;
	
	pWnd = GetDlgItem( IDOK );
	pWnd->EnableWindow( FALSE );
	pWnd = GetDlgItem( IDCANCELSEND );
	pWnd->EnableWindow( TRUE );
	
	if( rcpts.Find( _T(",")) > 0  && m_server.GetLength() == 0 )
	{
		//To send email without specified smtp server, we have to send the emails one by one
		// to multiple recipients. That is because every recipient has different smtp server.
		DirectSend( m_oSmtp, rcpts );
		pWnd = GetDlgItem( IDOK );
		pWnd->EnableWindow( TRUE );
		pWnd = GetDlgItem( IDCANCELSEND );
		pWnd->EnableWindow( FALSE );
		return;
	}
    
	SetStatus( _T("Connecting  ..."));
    m_bIdle = FALSE;
    m_bCancel = FALSE;
    m_bError = FALSE;

	CProgressCtrl *pBar = (CProgressCtrl*)GetDlgItem( IDC_PROGRESS1 );
	pBar->SetPos( 0 );

	m_oSmtp->SendMail();

	while( !m_bIdle )
	{
		MSG msg;
		if( PeekMessage(&msg, NULL,  0, 0, PM_REMOVE)) 
		{ 
			::TranslateMessage( &msg );
			::DispatchMessage( &msg );
		} 
	}

	if( m_bCancel )
	{
        SetStatus( _T("Operation is cancelled by user."));
		MessageBox(  _T("Operation is cancelled by user."), _T("Error"), MB_OK | MB_ICONERROR );
	}
	else if( m_bError )
	{
        SetStatus( m_lastErrDescription );
		MessageBox( m_lastErrDescription, _T("Error"), MB_OK | MB_ICONERROR );
	}
    else
	{
		SetStatus( _T("Message was delivered successfully"));
		MessageBox( _T("Message was delivered successfully"), _T("OK"), MB_OK );
	}

	pWnd = GetDlgItem( IDOK );
	pWnd->EnableWindow( TRUE );
	pWnd = GetDlgItem( IDCANCELSEND );
	pWnd->EnableWindow( FALSE );

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

		CString s;
		s.Format( _T("Connecting server for %s ..."), (LPCTSTR)addr );
		SetStatus( s );
		m_bIdle = FALSE;
		m_bCancel = FALSE;
		m_bError = FALSE;
		CProgressCtrl *pBar = (CProgressCtrl*)GetDlgItem( IDC_PROGRESS1 );
		pBar->SetPos( 0 );

		oSmtp->SendMail();

		//wait the asynchronous call finish.
		while( !m_bIdle )
		{
			MSG msg;
			if( PeekMessage(&msg, NULL,  0, 0, PM_REMOVE)) 
			{ 
				::TranslateMessage( &msg );
				::DispatchMessage( &msg );
			} 
		}

		s = _T("");
		if( m_bCancel )
		{
			s = _T("Operation is canceled by user.");
		}
		else if( m_bError )
		{
			s.Format( _T("Failed to delivery to %s : %s"), (LPCTSTR)addr, (LPCTSTR)m_lastErrDescription );
		}
		else
		{
			s.Format( _T("Message was delivered to %s successfully"), (LPCTSTR)addr );
		}

		MessageBox( s, _T("Information"), MB_OK | MB_ICONINFORMATION );

		if( m_bCancel )
			break;
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

BEGIN_EVENTSINK_MAP(CsimplevcNativeDlg, CDialog)
	ON_EVENT(CsimplevcNativeDlg, IDC_EXPLORER1, 252, CsimplevcNativeDlg::NavigateComplete2Explorer1, VTS_DISPATCH VTS_PVARIANT)
END_EVENTSINK_MAP()

void CsimplevcNativeDlg::NavigateComplete2Explorer1(LPDISPATCH pDisp, VARIANT* URL)
{
	if( m_bInitBody )
		return;

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();

	CComPtr<IHTMLElement> spElement;
	spDoc->put_charset( L"utf-8");
	spDoc->get_body(&spElement );
	spDoc.Release();
	if( spElement == NULL )
	{
		spDoc.Release();
		return;
	}

	m_bInitBody = TRUE;

	CString  s = _T("");
	s.Append( _T("<div>This sample demonstrates how to send html email.</div><div>&nbsp;</div>"));
	s.Append( _T("<div>From: [$from]</div>"));
	s.Append( _T("<div>To: [$to]</div>"));
	s.Append( _T("<div>Subject: [$subject]</div><div>&nbsp;</div>"));
	s.Append( _T("<div>If no sever address was specified, the email will be delivered to the recipient's server directly,"));
	s.Append( _T("However, if you don't have a static IP address, "));
	s.Append( _T("many anti-spam filters will mark it as a junk-email.</div><div>&nbsp;</div>" ));
	s.Append( _T("<div>If \"Digitial Signature\" was checked, please make sure you have the certificate for the sender address installed on " ));
	s.Append( _T("Local User Certificate Store.</div><div>&nbsp;</div>" ));
	s.Append( _T("<div>If \"Encrypt\" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store.</div>" ));

	BSTR bstr = s.AllocSysString();
	spElement->put_innerHTML( bstr );
	::SysFreeString( bstr );

	spElement.Release();
	spDoc.Release();
}

void CsimplevcNativeDlg::OnCbnSelchangeCombo2()
{
	INT index = m_lstFont.GetCurSel();
	if( index == 0 )
		return;

	CString font = _T("");
	m_lstFont.GetLBText( index, font );

	m_lstFont.SetCurSel(0);

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();
	
	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	vt.vt = VT_BSTR;
	vt.bstrVal = font.AllocSysString();
	spDoc->execCommand( _T("fontname"), VARIANT_FALSE, vt, &b  );
	::VariantClear( &vt );
	spDoc.Release();
	webMail.SetFocus();
}

void CsimplevcNativeDlg::OnCbnSelchangeCombo3()
{
	INT index = m_lstSize.GetCurSel();
	if( index == 0 )
		return;

	CString font = _T("");
	m_lstSize.GetLBText( index, font );

	m_lstSize.SetCurSel(0);

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	vt.vt = VT_BSTR;
	vt.bstrVal = font.AllocSysString();
	spDoc->execCommand( _T("fontsize"), VARIANT_FALSE, vt, &b  );
	::VariantClear( &vt );
	spDoc.Release();
	webMail.SetFocus();

}

void CsimplevcNativeDlg::OnBnClickedButton3()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();
	
	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit( &vt );
	spDoc->execCommand( _T("Bold"), VARIANT_FALSE, vt, &b  );
	::VariantClear( &vt );
	spDoc.Release();
	webMail.SetFocus();
}

void CsimplevcNativeDlg::OnBnClickedButton4()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();
	
	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit( &vt );
	spDoc->execCommand( _T("Italic"), VARIANT_FALSE, vt, &b  );
	::VariantClear( &vt );
	spDoc.Release();
	webMail.SetFocus();
}

void CsimplevcNativeDlg::OnBnClickedButton5()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();
	
	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit( &vt );
	spDoc->execCommand( _T("Underline"), VARIANT_FALSE, vt, &b  );
	::VariantClear( &vt );
	spDoc.Release();
	webMail.SetFocus();
}

void CsimplevcNativeDlg::OnBnClickedButton7()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();
	
	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit( &vt );
	spDoc->execCommand( _T("InsertImage"), VARIANT_TRUE, vt, &b  );
	::VariantClear( &vt );
	spDoc.Release();
	webMail.SetFocus();
}

void CsimplevcNativeDlg::OnBnClickedButton6()
{
	CColorDialog dlg;
	CString s = _T("");
	if( dlg.DoModal() == IDOK )
	{
		COLORREF color = dlg.GetColor();
		s.Format( _T("#%02x%02x%02x"), GetRValue(color), GetGValue(color), GetBValue(color));
	}
	else
	{
		return;
	}

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)webMail.get_Document();
	
	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit( &vt );
	vt.vt = VT_BSTR;
	vt.bstrVal = s.AllocSysString();
	spDoc->execCommand( _T("ForeColor"), VARIANT_TRUE, vt, &b  );
	::VariantClear( &vt );
	spDoc.Release();
	webMail.SetFocus();
}
/////////////////////////////////////////////////////////////////////////////
// OnClosedHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall 
	CsimplevcNativeDlg::OnClosedHandler()
{
	if( !m_bError )
		SetStatus( _T("Completed"));

	m_bIdle = TRUE;
	return S_OK;
}
	
/////////////////////////////////////////////////////////////////////////////
// OnSendingHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall 
	CsimplevcNativeDlg::OnSendingHandler( long nSent, long nTotalSize )
{
	TCHAR	szBuf[256];
	memset( szBuf, 0, sizeof(szBuf));
	wsprintf( szBuf, _T("Sending %d/%d ..."), nSent, nTotalSize );
	SetStatus( szBuf );

	CProgressCtrl *pBar = (CProgressCtrl*)GetDlgItem( IDC_PROGRESS1 );
	if( nSent == 0 )
	{
		pBar->SetRange32( 0, nTotalSize );
	}

	pBar->SetPos( nSent );

	if( nSent == nTotalSize )
		SetStatus( _T("Disconnecting ... " ));
	

	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// OnErrorHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall 
	CsimplevcNativeDlg::OnErrorHandler( long nErrorCode, BSTR ErrorMessage )
{
	SetStatus( ErrorMessage );
	m_lastErrDescription = ErrorMessage;
	m_bError = TRUE;
	m_bIdle = TRUE;
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// OnConnectedHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall 
	CsimplevcNativeDlg::OnConnectedHandler()
{
	SetStatus( _T("Connected"));
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// OnAuthenticatedHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall 
	CsimplevcNativeDlg::OnAuthenticatedHandler()
{
	SetStatus( _T("Authenticated"));
	return S_OK;
}

void	
CsimplevcNativeDlg::SetStatus( LPCTSTR lpszSrc )
{
	CWnd *pWnd = GetDlgItem( IDC_STATIC_STATUS );
	pWnd->SetWindowText( lpszSrc );
}

void CsimplevcNativeDlg::OnBnClickedCancelsend()
{
    m_oSmtp->Terminate();
    m_bCancel = TRUE;
    m_bIdle = TRUE;
    CWnd *pWnd = GetDlgItem( IDCANCELSEND );
	pWnd->EnableWindow( FALSE );
}