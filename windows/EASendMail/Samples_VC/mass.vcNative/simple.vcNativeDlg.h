// simple.vcNativeDlg.h : header file
//

#pragma once
#include <atlbase.h>
#include <atlcom.h>
#include "afxwin.h"
#include "afxcmn.h"
#include "explorer1.h"

static _ATL_FUNC_INFO OnSent = {CC_STDCALL, VT_EMPTY, 6, {VT_I4, VT_BSTR, VT_I4, VT_BSTR, VT_BSTR, VT_BSTR}};
static _ATL_FUNC_INFO OnConnected = {CC_STDCALL, VT_EMPTY, 2, {VT_I4, VT_BSTR}};
static _ATL_FUNC_INFO OnAuthenticated = {CC_STDCALL, VT_EMPTY, 2, {VT_I4, VT_BSTR}};
static _ATL_FUNC_INFO OnSending = {CC_STDCALL, VT_EMPTY, 4, {VT_I4, VT_I4, VT_I4, VT_BSTR}};


// CsimplevcNativeDlg dialog
class CsimplevcNativeDlg : public CDialog,
	public IDispEventSimpleImpl<IDD_SIMPLEVCNATIVE_DIALOG, 
	CsimplevcNativeDlg, &__uuidof(_IFastSenderEvents)> 
{
// Construction
public:
	CsimplevcNativeDlg(CWnd* pParent = NULL);	// standard constructor

BEGIN_SINK_MAP(CsimplevcNativeDlg)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IFastSenderEvents), 1, OnSentHandler, &OnSent)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IFastSenderEvents), 2, OnConnectedHandler, &OnConnected)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IFastSenderEvents), 3, OnAuthenticatedHandler, &OnAuthenticated)
	SINK_ENTRY_INFO(IDD_SIMPLEVCNATIVE_DIALOG, __uuidof(_IFastSenderEvents), 4, OnSendingHandler, &OnSending)
END_SINK_MAP()
// Dialog Data
	enum { IDD = IDD_SIMPLEVCNATIVE_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

protected:
	HRESULT __stdcall OnSentHandler( long lRet,
        BSTR ErrDesc,
        long nKey,
        BSTR tParam,
        BSTR senderAddr,
        BSTR Recipients );
	HRESULT __stdcall OnConnectedHandler(long nKey, BSTR tParam);
	HRESULT __stdcall OnAuthenticatedHandler(long nKey, BSTR tParam);
	HRESULT __stdcall OnSendingHandler(  long nSent, long nTotalSize, long nKey, BSTR tParam );


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
	void	SetStatusEx( int nIndex, LPCTSTR lpszSrc );
public:
	CString m_from;
	CString m_subject;
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

	BOOL m_bCancel;
	afx_msg void OnBnClickedCancelsend();
	//IMailPtr	m_oSmtp;
	IFastSenderPtr m_oFastSender;
	CComboBox m_lstProtocol;
	CListCtrl m_listTo;

	afx_msg void OnBnClickedButton8();
	afx_msg void OnBnClickedButton9();

	void DoEvents();
	void WaitAllTaskFinished();
	void SubmitTask( INT nIndex );

	INT m_nSubmitted;
	INT	m_nCompleted;
	INT m_nSuccess;
	INT m_nFailed;
	INT m_nTotal;
	int m_nworkThreads;
};
