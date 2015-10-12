// simple.vcNativeDlg.h : header file
//

#pragma once
#include <atlbase.h>
#include <atlcom.h>
#include "afxwin.h"
#include "afxcmn.h"
#include "explorer1.h"

static _ATL_FUNC_INFO OnClosed = {CC_STDCALL, VT_EMPTY, 0};
static _ATL_FUNC_INFO OnSending = {CC_STDCALL, VT_EMPTY, 2, {VT_I4, VT_I4}};
static _ATL_FUNC_INFO OnError = {CC_STDCALL, VT_EMPTY, 2, {VT_I4, VT_BSTR}};
static _ATL_FUNC_INFO OnConnected = {CC_STDCALL, VT_EMPTY, 0};
static _ATL_FUNC_INFO OnAuthenticated = {CC_STDCALL, VT_EMPTY, 0};

// CsimplevcNativeDlg dialog
class CsimplevcNativeDlg : public CDialog,
	public IDispEventSimpleImpl<IDD_SIMPLEVCNATIVE_DIALOG, 
	CsimplevcNativeDlg, &__uuidof(_IMailEvents)> 
{
// Construction
public:
	CsimplevcNativeDlg(CWnd* pParent = NULL);	// standard constructor

BEGIN_SINK_MAP(CsimplevcNativeDlg)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IMailEvents), 1, OnClosedHandler, &OnClosed)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IMailEvents), 2, OnSendingHandler, &OnSending)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IMailEvents), 3, OnErrorHandler, &OnError)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IMailEvents), 4, OnConnectedHandler, &OnConnected)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IMailEvents), 5, OnAuthenticatedHandler, &OnAuthenticated)
END_SINK_MAP()
// Dialog Data
	enum { IDD = IDD_SIMPLEVCNATIVE_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

protected:
	HRESULT __stdcall OnClosedHandler();
	HRESULT __stdcall OnSendingHandler( long nSent, long nTotalSize );
	HRESULT __stdcall OnErrorHandler( long nErrorCode, BSTR ErrorMessage );
	HRESULT __stdcall OnConnectedHandler();
	HRESULT __stdcall OnAuthenticatedHandler();

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

	void	SetStatus( LPCTSTR lpszSrc );
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
	CStringArray m_arAtt;
	static TCHAR* m_charset[];
	static TCHAR* m_charsetName[];
	afx_msg void OnBnClickedCheck1();
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedOk();
	CExplorer1 webMail;
	BOOL	m_bInitBody;
	DECLARE_EVENTSINK_MAP()
	void NavigateComplete2Explorer1(LPDISPATCH pDisp, VARIANT* URL);
	CComboBox m_lstFont;
	CComboBox m_lstSize;
	afx_msg void OnCbnSelchangeCombo2();
	afx_msg void OnCbnSelchangeCombo3();
	afx_msg void OnBnClickedButton3();
	afx_msg void OnBnClickedButton4();
	afx_msg void OnBnClickedButton5();
	afx_msg void OnBnClickedButton7();
	afx_msg void OnBnClickedButton6();

	BOOL m_bError;
	BOOL m_bCancel;
	BOOL m_bIdle;
	CString m_lastErrDescription;
	afx_msg void OnBnClickedCancelsend();
	IMailPtr	m_oSmtp;
	CComboBox m_lstProtocol;
};
