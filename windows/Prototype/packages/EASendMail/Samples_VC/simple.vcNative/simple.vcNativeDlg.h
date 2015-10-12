// simple.vcNativeDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CsimplevcNativeDlg dialog
class CsimplevcNativeDlg : public CDialog
{
// Construction
public:
	CsimplevcNativeDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_SIMPLEVCNATIVE_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()

	void	InitCharset();
	static void	_ParseEmailAddr( LPCTSTR lpszSrc, CString &name, CString &addr );
	static void _SplitEx( LPCTSTR lpszSrc, CStringArray &ar );

	void DirectSend( IMailPtr &oSmtp, LPCTSTR lpszRcpts );
public:
	CString m_from;
	CString m_to;
	CString m_cc;
	CString m_subject;
	BOOL m_bSign;
	BOOL m_bEncrypt;
	CString m_server;
	BOOL m_bAuth;
	CString m_user;
	CString m_password;
	BOOL m_bSSL;
	CComboBox m_lstCharset;
	CString m_attachments;
	CString m_body;
	CStringArray m_arAtt;
	static TCHAR* m_charset[];
	static TCHAR* m_charsetName[];
	afx_msg void OnBnClickedCheck1();
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedOk();

	CComboBox m_lstProtocol;
};
