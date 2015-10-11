#pragma once


// CAddRecipient dialog

class CAddRecipient : public CDialog
{
	DECLARE_DYNAMIC(CAddRecipient)

public:
	CAddRecipient(CWnd* pParent = NULL);   // standard constructor
	virtual ~CAddRecipient();

// Dialog Data
	enum { IDD = IDD_DIALOG1 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CString m_name;
	CString m_address;
	afx_msg void OnBnClickedOk();
};
