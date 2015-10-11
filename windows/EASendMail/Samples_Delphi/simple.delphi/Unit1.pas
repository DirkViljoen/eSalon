//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2010 ADMINSYSTEM SOFTWARE LIMITED                        |
// |
// |    Project: It demonstrates how to use EASendMailObj to send email with synchronous mode
// |
// |    Author: Ivan Lui ( ivan@emailarchitect.net )
//  ===============================================================================
unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, EASendMailObjLib_TLB, StdCtrls;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    textFrom: TEdit;
    textTo: TEdit;
    textCc: TEdit;
    textSubject: TEdit;
    Label5: TLabel;
    GroupBox1: TGroupBox;
    Label6: TLabel;
    textServer: TEdit;
    chkAuth: TCheckBox;
    Label7: TLabel;
    Label8: TLabel;
    textUser: TEdit;
    textPassword: TEdit;
    chkSSL: TCheckBox;
    chkSign: TCheckBox;
    chkEncrypt: TCheckBox;
    Label9: TLabel;
    lstCharset: TComboBox;
    Label10: TLabel;
    textAttachment: TEdit;
    btnAdd: TButton;
    btnClear: TButton;
    textBody: TMemo;
    btnSend: TButton;
    lstProtocol: TComboBox;
    procedure FormCreate(Sender: TObject);
    procedure InitCharset();
    procedure btnSendClick(Sender: TObject);
    procedure chkAuthClick(Sender: TObject);
    function ChAnsiToWide(const StrA: AnsiString): WideString;
    procedure btnAddClick(Sender: TObject);
    procedure DirectSend(oSmtp: TMail);
    procedure btnClearClick(Sender: TObject);
    procedure FormResize(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
  end;

const
  CRYPT_MACHINE_KEYSET = 32;
  CRYPT_USER_KEYSET = 4096;
  CERT_SYSTEM_STORE_CURRENT_USER = 65536;
  CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072;
    
var
  Form1: TForm1;
  m_arAttachments : TStringList;
  m_arCharset: array[0..27,0..1] of WideString;
implementation

{$R *.dfm}

procedure TForm1.InitCharset();
var
  i, index: integer;
begin
  index := 0;

  m_arCharset[index, 0] := 'Arabic(Windows)';
  m_arCharset[index, 1] := 'windows-1256';
  index := index + 1;

  m_arCharset[index, 0] := 'Baltic(ISO)';
  m_arCharset[index, 1] := 'iso-8859-4';
  index := index + 1;

  m_arCharset[index, 0] := 'Baltic(Windows)';
  m_arCharset[index, 1] := 'windows-1257';
  index := index + 1;

  m_arCharset[index, 0] := 'Central Euporean(ISO)';
  m_arCharset[index, 1] := 'iso-8859-2';
  index := index + 1;

  m_arCharset[index, 0] := 'Central Euporean(Windows)';
  m_arCharset[index, 1] := 'windows-1250';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Simplified(GB18030)';
  m_arCharset[index, 1] := 'GB18030';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Simplified(GB2312)';
  m_arCharset[index, 1] := 'gb2312';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Simplified(HZ)';
  m_arCharset[index, 1] := 'hz-gb-2312';
  index := index + 1;

  m_arCharset[index, 0] := 'Chinese Traditional(Big5)';
  m_arCharset[index, 1] := 'big5';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(ISO)';
  m_arCharset[index, 1] := 'iso-8859-5';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(KOI8-R)';
  m_arCharset[index, 1] := 'koi8-r';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(KOI8-U)';
  m_arCharset[index, 1] := 'koi8-u';
  index := index + 1;

  m_arCharset[index, 0] := 'Cyrillic(Windows)';
  m_arCharset[index, 1] := 'windows-1251';
  index := index + 1;

  m_arCharset[index, 0] := 'Greek(ISO)';
  m_arCharset[index, 1] := 'iso-8859-7';
  index := index + 1;

  m_arCharset[index, 0] := 'Greek(Windows)';
  m_arCharset[index, 1] := 'windows-1253';
  index := index + 1;

  m_arCharset[index, 0] := 'Hebrew(Windows)';
  m_arCharset[index, 1] := 'windows-1255';
  index := index + 1;

  m_arCharset[index, 0] := 'Japanese(JIS)';
  m_arCharset[index, 1] := 'iso-2022-jp';
  index := index + 1;

  m_arCharset[index, 0] := 'Korean';
  m_arCharset[index, 1] := 'ks_c_5601-1987';
  index := index + 1;

  m_arCharset[index, 0] := 'Korean(EUC)';
  m_arCharset[index, 1] := 'euc-kr';
  index := index + 1;

  m_arCharset[index, 0] := 'Latin 9(ISO)';
  m_arCharset[index, 1] := 'iso-8859-15';
  index := index + 1;

  m_arCharset[index, 0] := 'Thai(Windows)';
  m_arCharset[index, 1] := 'windows-874';
  index := index + 1;

  m_arCharset[index, 0] := 'Turkish(ISO)';
  m_arCharset[index, 1] := 'iso-8859-9';
  index := index + 1;

  m_arCharset[index, 0] := 'Turkish(Windows)';
  m_arCharset[index, 1] := 'windows-1254';
  index := index + 1;

  m_arCharset[index, 0] := 'Unicode(UTF-7)';
  m_arCharset[index, 1] := 'utf-7';
  index := index + 1;

  m_arCharset[index, 0] := 'Unicode(UTF-8)';
  m_arCharset[index, 1] := 'utf-8';
  index := index + 1;

  m_arCharset[index, 0] := 'Vietnames(Windows)';
  m_arCharset[index, 1] := 'windows-1258';
  index := index + 1;

  m_arCharset[index, 0] := 'Western European(ISO)';
  m_arCharset[index, 1] := 'iso-8859-1';
  index := index + 1;

  m_arCharset[index, 0] := 'Western European(Windows)';
  m_arCharset[index, 1] := 'windows-1252';


  for i:= 0 to 27 do
  begin
    lstCharset.AddItem(m_arCharset[i,0], nil);
  end;
  // Set default encoding to utf-8, it supports all languages.
  lstCharset.ItemIndex := 24;

  lstProtocol.AddItem('SMTP Protocol - Recommended', nil);
  lstProtocol.AddItem('Exchange Web Service - 2007/2010', nil);
  lstProtocol.AddItem('Exchange WebDav - 2000/2003', nil);
  lstProtocol.ItemIndex := 0;

end;
procedure TForm1.FormCreate(Sender: TObject);
begin
  textSubject.Text := 'delphi email test';
  textBody.Text := 'This sample demonstrates how to send simple email.'
  + #13#10 + #13#10
  + 'If no sever address was specified, the email will be delivered to the recipient''s server directly,'
  + 'However, if you don''t have a static IP address, '
  + 'many anti-spam filters will mark it as a junk-email.'
  + #13#10;

  m_arAttachments := TStringList.Create();
  InitCharset();
end;

procedure TForm1.btnSendClick(Sender: TObject);
var
  oSmtp: TMail;
  i: integer;
  Rcpts: OleVariant;
  RcptBound: integer;
  RcptAddr: WideString;
  oEncryptCert: TCertificate;
begin

  if trim(textFrom.Text) = '' then
  begin
    ShowMessage( 'Plese input From email address!' );
    textFrom.SetFocus();
    exit;
  end;

  if(trim(textTo.Text) = '' ) and
     (trim(textCc.Text) = '' ) then
  begin
    ShowMessage( 'Please input To or Cc email addresses, please use comma(,) to separate multiple addresses!');
    textTo.SetFocus();
    exit;
  end;

  if chkAuth.Checked and ((trim(textUser.Text)='') or
  (trim(textPassword.Text)='')) then
  begin
    ShowMessage( 'Please input User, Password for SMTP authentication!' );
    textUser.SetFocus();
    exit;
  end;

  btnSend.Enabled := false;
  // Create TMail Object
  oSmtp := TMail.Create(Application);
  oSmtp.LicenseCode := 'TryIt';

  oSmtp.Charset := m_arCharset[lstCharset.ItemIndex, 1];
  oSmtp.FromAddr := ChAnsiToWide(trim(textFrom.Text));

  // Add recipient's
  oSmtp.AddRecipientEx(ChAnsiToWide(trim(textTo.Text)), 0 );
  oSmtp.AddRecipientEx(ChAnsiToWide(trim(textCc.Text)), 0 );

  // Set subject
  oSmtp.Subject := ChAnsiToWide(textSubject.Text);

  // Using HTML FORMAT to send mail
  // oSmtp.BodyFormat := 1;

  // Set body
  oSmtp.BodyText := ChAnsiToWide(textBody.Text);

  // Add attachments
  for i:= 0 to m_arAttachments.Count - 1 do
  begin
      oSmtp.AddAttachment(ChAnsiToWide(m_arAttachments[i]));
  end;

  // Add digital signature
  if chkSign.Checked then
  begin
    if not oSmtp.SignerCert.FindSubject( oSmtp.FromAddr,
    CERT_SYSTEM_STORE_CURRENT_USER, 'my' ) then
    begin
      ShowMessage( 'Not cert found for signing: ' + oSmtp.SignerCert.GetLastError());
      btnSend.Enabled := true;
      exit;
    end;

    if not oSmtp.SignerCert.HasCertificate Then
    begin
      ShowMessage( 'Signer certificate has no private key, ' +
      'this certificate can not be used to sign email');
       btnSend.Enabled := true;
       exit;
    end;
  end;

  // get all to, cc, bcc email address to an array
  Rcpts := oSmtp.Recipients;
  RcptBound := VarArrayHighBound( Rcpts, 1 );
  // search encrypting cert for every recipient.
  if chkEncrypt.Checked then
    for i := 0 to RcptBound do
    begin
       RcptAddr := VarArrayGet( Rcpts, i );
       oEncryptCert := TCertificate.Create(Application);
       if not oEncryptCert.FindSubject(RcptAddr,
          CERT_SYSTEM_STORE_CURRENT_USER, 'AddressBook' ) then
          if not oEncryptCert.FindSubject(RcptAddr,
            CERT_SYSTEM_STORE_CURRENT_USER, 'my' ) then
          begin
            ShowMessage( 'Failed to find cert for ' + RcptAddr + ': ' + oEncryptCert.GetLastError());
            btnSend.Enabled := true;
            exit;
          end;

       oSmtp.RecipientsCerts.Add(oEncryptCert.DefaultInterface);
    end;

  oSmtp.ServerAddr := trim(textServer.Text);
  oSmtp.Protocol := lstProtocol.ItemIndex;
  if oSmtp.ServerAddr <> '' then
  begin
    if chkAuth.Checked then
    begin
      oSmtp.UserName := trim(textUser.Text);
      oSmtp.Password := trim(textPassword.Text);
    end;

    if chkSSL.Checked then
    begin
      oSmtp.SSL_init();
      // If SSL port is 465, please add the following codes
      // oSmtp.ServerPort := 465;
      // oSmtp.SSL_starttls := 0;
    end;
  end;

  if (RcptBound > 0) and (oSmtp.ServerAddr = '') then
  begin
    // To send email without specified smtp server, we have to send the emails one by one
    // to multiple recipients. That is because every recipient has different smtp server.
    DirectSend( oSmtp );
    btnSend.Enabled := true;
    exit;
  end;

  if oSmtp.SendMail() = 0 then
    ShowMessage( 'Message delivered' )
  else
    ShowMessage( oSmtp.GetLastErrDescription());

  btnSend.Enabled := true;
end;

procedure TForm1.DirectSend(oSmtp: TMail);
var
  Rcpts: OleVariant;
  i, RcptBound: integer;
  RcptAddr: WideString;
begin
  Rcpts := oSmtp.Recipients;
  RcptBound := VarArrayHighBound( Rcpts, 1 );
  for i := 0 to RcptBound do
  begin
    RcptAddr := VarArrayGet( Rcpts, i );
    oSmtp.ClearRecipient();
    oSmtp.AddRecipientEx( RcptAddr, 0 );
    ShowMessage( 'Start to send email to ' + RcptAddr );
    if oSmtp.SendMail() = 0 then
      ShowMessage( 'Message delivered to ' +  RcptAddr + ' successfully!')
    else
      ShowMessage( 'Failed to deliver to ' + RcptAddr + ': ' + oSmtp.GetLastErrDescription());
  end;
end;

procedure TForm1.chkAuthClick(Sender: TObject);
begin
  textUser.Enabled := chkAuth.Checked;
  textPassword.Enabled := chkAuth.Checked;

  if( chkAuth.Checked ) then
  begin
    textUser.Color := clWindow;
    textPassword.Color := clWindow;
  end
  else
  begin
    textUser.Color := cl3DLight;
    textPassword.Color := cl3DLight;
  end;
end;

// before delphi doesn't support unicode very well in VCL, so
// we have to convert the ansistring to unicode by current default codepage.
function TForm1.ChAnsiToWide(const StrA: AnsiString): WideString;
var
  nLen: integer;
begin
  Result := StrA;
  if Result <> '' then
  begin
    // convert ansi string to widestring (unicode) by current system codepage
    nLen := MultiByteToWideChar(GetACP(), 1, PAnsiChar(@StrA[1]), -1, nil, 0);
    SetLength(Result, nLen - 1);
    if nLen > 1 then
      MultiByteToWideChar(GetACP(), 1, PAnsiChar(@StrA[1]), -1, PWideChar(@Result[1]), nLen - 1);
  end;
end;

procedure TForm1.btnAddClick(Sender: TObject);
var
  pFileDlg : TOpenDialog;
  fileName : string;
  index: integer;
begin
  pFileDlg := TOpenDialog.Create(Form1);
  if pFileDlg.Execute() then
  begin
    fileName := pFileDlg.FileName;
    m_arAttachments.Add( fileName );
    while true do
    begin
       index := Pos(  '\', fileName );
       if index <= 0 then
          break;

       fileName := Copy( fileName, index+1, Length(fileName)- index  );
    end;
    textAttachment.Text := textAttachment.Text + fileName + ';';
  end;
  pFileDlg.Destroy();
end;


procedure TForm1.btnClearClick(Sender: TObject);
begin
  m_arAttachments.Clear();
  textAttachment.Text := '';
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  if Form1.Width < 671 then
    Form1.Width := 671;

  if Form1.Height < 445 then
    Form1.Height := 445;

  textBody.Width := Form1.Width - 30;
  textBody.Height := Form1.Height - 300;
  btnSend.Top := textBody.Top +  textBody.Height + 5;
  btnSend.Left := Form1.Width - 20 - btnSend.Width;
  GroupBox1.Left := Form1.Width - GroupBox1.Width - 20;
  textFrom.Width := Form1.Width - GroupBox1.Width - 110;
  textSubject.Width := textFrom.Width;
  textTo.Width := textFrom.Width;
  textCc.Width := textFrom.Width;

  
end;

end.
