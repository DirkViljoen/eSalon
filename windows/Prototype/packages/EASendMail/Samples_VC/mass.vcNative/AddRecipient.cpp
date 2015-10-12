// AddRecipient.cpp : implementation file
//

#include "stdafx.h"
#include "simple.vcNative.h"
#include "AddRecipient.h"


// CAddRecipient dialog

IMPLEMENT_DYNAMIC(CAddRecipient, CDialog)

CAddRecipient::CAddRecipient(CWnd* pParent /*=NULL*/)
	: CDialog(CAddRecipient::IDD, pParent)
	, m_name(_T(""))
	, m_address(_T(""))
{

}

CAddRecipient::~CAddRecipient()
{
}

void CAddRecipient::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT1, m_name);
	DDV_MaxChars(pDX, m_name, 128);
	DDX_Text(pDX, IDC_EDIT2, m_address);
	DDV_MaxChars(pDX, m_address, 256);
}


BEGIN_MESSAGE_MAP(CAddRecipient, CDialog)
	ON_BN_CLICKED(IDOK, &CAddRecipient::OnBnClickedOk)
END_MESSAGE_MAP()


// CAddRecipient message handlers

void CAddRecipient::OnBnClickedOk()
{
	if(!UpdateData( TRUE ))
		return;

	m_address.Trim();
	if( m_address.GetLength() == 0 )
	{
		MessageBox( _T("Please input recipient email address!"));
		return;
	}
	OnOK();
}
