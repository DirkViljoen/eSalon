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
  Dialogs, EASendMailObjLib_TLB, StdCtrls, ComCtrls, unit2;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Label4: TLabel;
    textFrom: TEdit;
    textSubject: TEdit;
    GroupBox1: TGroupBox;
    Label6: TLabel;
    textServer: TEdit;
    chkAuth: TCheckBox;
    Label7: TLabel;
    Label8: TLabel;
    textUser: TEdit;
    textPassword: TEdit;
    chkSSL: TCheckBox;
    Label9: TLabel;
    lstCharset: TComboBox;
    Label10: TLabel;
    textAttachment: TEdit;
    btnAdd: TButton;
    btnClear: TButton;
    textBody: TMemo;
    btnSend: TButton;
    lstTo: TListView;
    btnAddRcpt: TButton;
    btnClearRcpt: TButton;
    chkTest: TCheckBox;
    Label3: TLabel;
    textThreads: TEdit;
    textStatus: TLabel;
    lstProtocol: TComboBox;
    procedure FormCreate(Sender: TObject);
    procedure InitCharset();
    procedure btnSendClick(Sender: TObject);
    procedure chkAuthClick(Sender: TObject);
    function ChAnsiToWide(const StrA: AnsiString): WideString;
    procedure btnAddClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnAddRcptClick(Sender: TObject);
    procedure btnClearRcptClick(Sender: TObject);
    procedure Sending();
    // FastSender event handler
    procedure OnSent(ASender: TObject; lRet: Integer; const ErrDesc: WideString;
                                                  nKey: Integer; const tParam: WideString;
                                                  const senderAddr: WideString;
                                                  const Recipients: WideString);
    procedure OnConnected(ASender: TObject; nKey: Integer; const tParam: WideString);
    procedure OnAuthenticated(ASender: TObject; nKey: Integer; const tParam: WideString);
    procedure OnSending(ASender: TObject; lSent: Integer; lTotal: Integer;
    nKey: Integer; const tParam: WideString);
    procedure ChangeStatus(index: integer; status: AnsiString );
    procedure WaitAllTaskFinished();
    procedure SubmitTask( index: integer);
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
  Max_QueuedCount = 100;

var
  Form1: TForm1;
  m_arAttachments : TStringList;
  m_arCharset: array[0..27,0..1] of WideString;
  m_oFastSender: TFastSender;
  g_lSent: integer;
  g_lSend: integer;
  g_failure: integer;
  g_bSending: Boolean;
  g_bStopped: Boolean;
  g_lTotal: integer;
implementation

{$R *.dfm}

// FastSender event handler
procedure TForm1.OnSent(ASender: TObject; lRet: Integer; const ErrDesc: WideString;
                                                  nKey: Integer; const tParam: WideString;
                                                  const senderAddr: WideString;
                                                  const Recipients: WideString);
var
  desc: AnsiString;
begin
  if lRet = 0 then
    if chkTest.Checked then
      desc := 'Test is ok!'
    else
      desc := 'Sent successfully!'
  else
  begin
    desc := ErrDesc;
    g_failure := g_failure + 1;
  end;

  g_lSent := g_lSent + 1;
  ChangeStatus(nKey, desc);
end;

procedure TForm1.OnConnected(ASender: TObject; nKey: Integer; const tParam: WideString);
begin;
  ChangeStatus( nKey, 'Connected');
end;

procedure TForm1.OnAuthenticated(ASender: TObject; nKey: Integer; const tParam: WideString);
begin;
  ChangeStatus( nKey, 'Authorized');
end;

procedure TForm1.OnSending(ASender: TObject; lSent: Integer; lTotal: Integer;
nKey: Integer; const tParam: WideString);
begin
   ChangeStatus( nKey, Format('Sending data %d/%d ...', [lSent, lTotal]));
end;

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
  textSubject.Text := 'delphi mass email test';
  textBody.Text := 'This sample demonstrates how to send mass email + multiple threads.'
  + #13#10 + #13#10
  + 'From: [$from]' + #13#10
  + 'To: [$to]' + #13#10
  + 'Subject: [$subject]' + #13#10 + #13#10
  + 'If no sever address was specified, the email will be delivered to the recipient''s server directly,'
  + 'However, if you don''t have a static IP address, '
  + 'many anti-spam filters will mark it as a junk-email.'
  + #13#10;

  m_arAttachments := TStringList.Create();
  InitCharset();

  if m_oFastSender = nil then
  begin
    m_oFastSender := TFastSender.Create(Application);
    m_oFastSender.OnSent := OnSent;
    m_oFastSender.OnConnected := OnConnected;
    m_oFastSender.OnAuthenticated := OnAuthenticated;
    m_oFastSender.OnSending := OnSending;
  end;

  g_lSent := 0;
  g_lSend := 0;
  g_failure := 0;
  g_bSending := false;
  g_bStopped := true;
  g_lTotal :=0;
  textStatus.Caption := 'Ready';
end;

procedure TForm1.btnSendClick(Sender: TObject);
begin
  if btnSend.Caption = 'Cancel' then
  begin
    m_oFastSender.ClearQueuedMails();
    btnSend.Enabled := false;
    g_bSending := false;
    exit;
  end;

  btnSend.Caption := 'Cancel';
  btnAddRcpt.Enabled := false;
  btnClearRcpt.Enabled := False;
  btnAdd.Enabled := false;
  btnClear.Enabled := false;
  Sending();

end;

procedure TForm1.Sending();
var
 i, nCount: integer;
begin
  nCount := lstTo.Items.Count;
  m_oFastSender.MaxThreads := StrToInt(textThreads.Text);

  g_lSend := 0;
  g_lSent := 0;
  g_failure := 0;
  g_bSending := true;
  g_bStopped := false;

  for i := 0 to nCount - 1 do
    lstTo.Items[i].SubItems[0] := 'Ready';

  while (g_bSending) and (g_lSend < nCount) do
  begin
    if m_oFastSender.GetQueuedCount() < Max_QueuedCount then
    begin
      ChangeStatus(g_lSend,'Queued ...');
      SubmitTask(g_lSend);
      g_lSend := g_lSend + 1;
    end;
    Application.ProcessMessages();
  end;

  WaitAllTaskFinished();
  btnAdd.Enabled := true;
  btnClear.Enabled := true;
  btnAddRcpt.Enabled := true;
  btnClearRcpt.Enabled := true;

  btnSend.Enabled := true;
  btnSend.Caption := 'Send';
  g_bStopped := true;
end;


procedure TForm1.ChangeStatus(index: integer; status: AnsiString );
var
  nSent, nError: integer;
  item: TListItem;
begin
  item := lstTo.Items[index];
  item.SubItems[0]:= status;

  nSent := g_lSent - g_failure;
  nError := g_failure;
  textStatus.Caption := Format( 'Total %d emails %d success, %d failed.',
  [lstTo.Items.Count, nSent, nError]);
end;

procedure TForm1.WaitAllTaskFinished();
begin
    while m_oFastSender.GetQueuedCount() > 0 do
        Application.ProcessMessages();

    while not (m_oFastSender.GetIdleThreads() = m_oFastSender.GetCurrentThreads()) do
      Application.ProcessMessages();
end;

procedure TForm1.SubmitTask( index: integer);
var
  oSmtp: TMail;
  item: TListItem;
  i: integer;
  bodytext: AnsiString;
begin
  item := lstTo.Items[index];
  oSmtp := TMail.Create(Application);

  // The license code for EASendMail ActiveX Object,
  // for evaluation usage, please use "TryIt" as the license code.
  oSmtp.LicenseCode := 'TryIt';

  //oSmtp.LogFileName = 'c:\smtp.txt'; //enable smtp log

  oSmtp.Charset := m_arCharset[lstCharset.ItemIndex, 1];
  oSmtp.FromAddr := ChAnsiToWide(trim(textFrom.Text));

  // Add recipient's
  oSmtp.AddRecipientEx(ChAnsiToWide(item.Caption), 0 );

  // Set subject
  oSmtp.Subject := ChAnsiToWide(textSubject.Text);

  // Using HTML FORMAT to send mail
  // oSmtp.BodyFormat := 1;

  // Set body
  bodytext := textBody.Text;
  bodytext := StringReplace( bodytext, '[$from]', textFrom.Text, [rfIgnoreCase]);
  bodytext := StringReplace( bodytext, '[$to]', item.Caption, [rfIgnoreCase] );
  bodytext := StringReplace( bodytext, '[$subject]', textSubject.Text, [rfIgnoreCase]);

  oSmtp.BodyText := ChAnsiToWide(bodytext);

  // Add attachments
  for i:= 0 to m_arAttachments.Count - 1 do
  begin
      oSmtp.AddAttachment(ChAnsiToWide(m_arAttachments[i]));
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

  if chkTest.Checked then
  begin
    // only test if the server accept the recipient address, but do not send the email to the mailbox.
    m_oFastSender.Test(oSmtp.DefaultInterface, index, 'any value');
  end
  else
    m_oFastSender.Send(oSmtp.DefaultInterface, index, 'any value');
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

procedure TForm1.btnAddRcptClick(Sender: TObject);
var
  item: TListItem;
begin
  Form2.textName.Text  :='';
  Form2.textEmail.Text := '';

  if Form2.ShowModal() = mrOK then
  begin
    // for i := 0 to 9 do
    // begin
    item := lstTo.Items.Add();
    item.Caption := Form2.textName.Text + ' <' + Form2.textEmail.Text + '>' ;
    item.SubItems.Add('ready');
    // end;
  end
end;

procedure TForm1.btnClearRcptClick(Sender: TObject);
begin
  lstTo.Items.Clear();
end;

procedure TForm1.FormResize(Sender: TObject);
begin
  if Form1.Width < 661 then
    Form1.Width := 661;

  if Form1.Height < 435 then
    Form1.Height := 435;

  textBody.Width := Form1.Width - 30;
  textBody.Height := Form1.Height - 340;
  btnSend.Top := textBody.Top +  textBody.Height + 5;
  btnSend.Left := Form1.Width - 20 - btnSend.Width;
  textStatus.Top := btnSend.Top;
  GroupBox1.Left := Form1.Width - GroupBox1.Width - 20;
  textFrom.Width := Form1.Width - GroupBox1.Width - 110;
  textSubject.Width := textFrom.Width;
  lstTo.Width := textFrom.Width;

end;

end.
