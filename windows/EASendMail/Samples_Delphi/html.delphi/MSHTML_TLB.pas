unit MSHTML_TLB;

// ************************************************************************ //
// WARNING                                                                    
// -------                                                                    
// The types declared in this file were generated from data read from a
// Type Library. If this type library is explicitly or indirectly (via        
// another type library referring to this type library) re-imported, or the   
// 'Refresh' command of the Type Library Editor activated while editing the   
// Type Library, the contents of this file will be regenerated and all        
// manual modifications will be lost.                                         
// ************************************************************************ //

// Original MSHTML_tlb is too large, i make a small MSHTML_TLB;

{$TYPEDADDRESS OFF} // Unit must be compiled without type-checked pointers. 
{$WARN SYMBOL_PLATFORM OFF}
{$WRITEABLECONST ON}
{$VARPROPSETTER ON}
interface

uses Windows, ActiveX, Classes, Graphics, OleCtrls, OleServer, StdVCL, Variants;

const
  MSHTMLMajorVersion = 4;
  MSHTMLMinorVersion = 0;

  IID_IHTMLElement: TGUID = '{3050F1FF-98B5-11CF-BB82-00AA00BDCE0B}';
  IID_IHTMLElement2: TGUID = '{3050F434-98B5-11CF-BB82-00AA00BDCE0B}';
  DIID_DispHTMLHtmlElement: TGUID = '{3050F560-98B5-11CF-BB82-00AA00BDCE0B}';
  CLASS_HTMLHtmlElement: TGUID = '{3050F491-98B5-11CF-BB82-00AA00BDCE0B}';
  IID_IHTMLElementDefaults: TGUID = '{3050F6C9-98B5-11CF-BB82-00AA00BDCE0B}';

  IID_IHTMLDocument: TGUID = '{626FC520-A41E-11CF-A731-00A0C9082637}';
  IID_IHTMLDocument2: TGUID = '{332C4425-26CB-11D0-B483-00C04FD90119}';
  DIID_DispHTMLDocument: TGUID = '{3050F55F-98B5-11CF-BB82-00AA00BDCE0B}';
  CLASS_HTMLDocument: TGUID = '{25336920-03F9-11CF-8FD0-00AA00686F13}';

type
  IHTMLDocument = interface;
  IHTMLDocumentDisp = dispinterface;
  IHTMLElement = interface;
  IHTMLDocument2 = interface;
  IHTMLDocument2Disp = dispinterface;
  DispHTMLHtmlElement = dispinterface;
  DispHTMLDocument = dispinterface;

  HTMLHtmlElement = DispHTMLHtmlElement;
  HTMLDocument = DispHTMLDocument;

// *********************************************************************//
// Interface: IHTMLDocument
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {626FC520-A41E-11CF-A731-00A0C9082637}
// *********************************************************************//
  IHTMLDocument = interface(IDispatch)
    ['{626FC520-A41E-11CF-A731-00A0C9082637}']
    function Get_Script: IDispatch; safecall;
    property Script: IDispatch read Get_Script;
  end;

// *********************************************************************//
// DispIntf:  IHTMLDocumentDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {626FC520-A41E-11CF-A731-00A0C9082637}
// *********************************************************************//
  IHTMLDocumentDisp = dispinterface
    ['{626FC520-A41E-11CF-A731-00A0C9082637}']
    property Script: IDispatch readonly dispid 1001;
  end;

// *********************************************************************//
// DispIntf:  DispHTMLDocument
// Flags:     (4112) Hidden Dispatchable
// GUID:      {3050F55F-98B5-11CF-BB82-00AA00BDCE0B}
// *********************************************************************//
  DispHTMLDocument = dispinterface
    ['{3050F55F-98B5-11CF-BB82-00AA00BDCE0B}']
    property Script: IDispatch readonly dispid 1001;
    property body: IHTMLElement readonly dispid 1004;
    property designMode: WideString dispid 1014;
    function execCommand(const cmdID: WideString; showUI: WordBool; value: OleVariant): WordBool; dispid 1065;
  end;
// *********************************************************************//
// Interface: IHTMLDocument2
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {332C4425-26CB-11D0-B483-00C04FD90119}
// *********************************************************************//
  IHTMLDocument2 = interface(IHTMLDocument)
    ['{332C4425-26CB-11D0-B483-00C04FD90119}']

    function Get_body: IHTMLElement; safecall;
    procedure Set_designMode(const p: WideString); safecall;
    function Get_designMode: WideString; safecall;
    function execCommand(const cmdID: WideString; showUI: WordBool; value: OleVariant): WordBool; safecall;
    property body: IHTMLElement read Get_body;
    property designMode: WideString read Get_designMode write Set_designMode;
  end;

// *********************************************************************//
// DispIntf:  IHTMLDocument2Disp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {332C4425-26CB-11D0-B483-00C04FD90119}
// *********************************************************************//
  IHTMLDocument2Disp = dispinterface
    ['{332C4425-26CB-11D0-B483-00C04FD90119}']
    property body: IHTMLElement readonly dispid 1004;
    property designMode: WideString dispid 1014;
    function execCommand(const cmdID: WideString; showUI: WordBool; value: OleVariant): WordBool; dispid 1065;
  end;
 // *********************************************************************//
// Interface: IHTMLElement
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {3050F1FF-98B5-11CF-BB82-00AA00BDCE0B}
// *********************************************************************//
  IHTMLElement = interface(IDispatch)
    ['{3050F1FF-98B5-11CF-BB82-00AA00BDCE0B}']
    procedure Set_innerHTML(const p: WideString); safecall;
    function Get_innerHTML: WideString; safecall;
    property innerHTML: WideString read Get_innerHTML write Set_innerHTML;
  end;

// *********************************************************************//
// DispIntf:  IHTMLElementDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {3050F1FF-98B5-11CF-BB82-00AA00BDCE0B}
// *********************************************************************//
  IHTMLElementDisp = dispinterface
    ['{3050F1FF-98B5-11CF-BB82-00AA00BDCE0B}']
    property innerHTML: WideString dispid -2147417086;
  end;
// *********************************************************************//
// DispIntf:  DispHTMLHtmlElement
// Flags:     (4112) Hidden Dispatchable
// GUID:      {3050F560-98B5-11CF-BB82-00AA00BDCE0B}
// *********************************************************************//
  DispHTMLHtmlElement = dispinterface
    ['{3050F560-98B5-11CF-BB82-00AA00BDCE0B}']
    property innerHTML: WideString dispid -2147417086;
  end;

  // *********************************************************************//
// The Class CoHTMLDocument provides a Create and CreateRemote method to          
// create instances of the default interface DispHTMLDocument exposed by              
// the CoClass HTMLDocument. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoHTMLDocument = class
    class function Create: DispHTMLDocument;
    class function CreateRemote(const MachineName: string): DispHTMLDocument;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : THTMLDocument
// Help String      : 
// Default Interface: DispHTMLDocument
// Def. Intf. DISP? : Yes
// Event   Interface: HTMLDocumentEvents
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  THTMLDocumentProperties= class;
{$ENDIF}
  THTMLDocument = class(TOleServer)
  private
    FIntf:        DispHTMLDocument;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       THTMLDocumentProperties;
    function      GetServerProperties: THTMLDocumentProperties;
{$ENDIF}
    function      GetDefaultInterface: DispHTMLDocument;
  protected
    procedure InitServerData; override;
    procedure InvokeEvent(DispID: TDispID; var Params: TVariantArray); override;
    function Get_Script: IDispatch;
    function Get_body: IHTMLElement;
    procedure Set_designMode(const Param1: WideString);
    function Get_designMode: WideString;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: DispHTMLDocument);
    procedure Disconnect; override;

    function execCommand(const cmdID: WideString; showUI: WordBool): WordBool; overload;
    function execCommand(const cmdID: WideString; showUI: WordBool; value: OleVariant): WordBool; overload;
    property DefaultInterface: DispHTMLDocument read GetDefaultInterface;
    property Script: IDispatch read Get_Script;
    property body: IHTMLElement read Get_body;
    property designMode: WideString read Get_designMode write Set_designMode;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: THTMLDocumentProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : THTMLDocument
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 THTMLDocumentProperties = class(TPersistent)
  private
    FServer:    THTMLDocument;
    function    GetDefaultInterface: DispHTMLDocument;
    constructor Create(AServer: THTMLDocument);
  protected
    function Get_Script: IDispatch;
    function Get_body: IHTMLElement;
    procedure Set_designMode(const Param1: WideString);
    function Get_designMode: WideString;
  public
    property DefaultInterface: DispHTMLDocument read GetDefaultInterface;
  published
  end;
{$ENDIF}

// *********************************************************************//
// The Class CoHTMLHtmlElement provides a Create and CreateRemote method to          
// create instances of the default interface DispHTMLHtmlElement exposed by              
// the CoClass HTMLHtmlElement. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoHTMLHtmlElement = class
    class function Create: DispHTMLHtmlElement;
    class function CreateRemote(const MachineName: string): DispHTMLHtmlElement;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : THTMLHtmlElement
// Help String      : 
// Default Interface: DispHTMLHtmlElement
// Def. Intf. DISP? : Yes
// Event   Interface: HTMLElementEvents
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  THTMLHtmlElementProperties= class;
{$ENDIF}
  THTMLHtmlElement = class(TOleServer)
  private
    FIntf:        DispHTMLHtmlElement;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       THTMLHtmlElementProperties;
    function      GetServerProperties: THTMLHtmlElementProperties;
{$ENDIF}
    function      GetDefaultInterface: DispHTMLHtmlElement;
  protected
    procedure InitServerData; override;
    procedure InvokeEvent(DispID: TDispID; var Params: TVariantArray); override;
    procedure Set_innerHTML(const Param1: WideString);
    function Get_innerHTML: WideString;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: DispHTMLHtmlElement);
    procedure Disconnect; override;
    property DefaultInterface: DispHTMLHtmlElement read GetDefaultInterface;
    property innerHTML: WideString read Get_innerHTML write Set_innerHTML;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: THTMLHtmlElementProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : THTMLHtmlElement
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 THTMLHtmlElementProperties = class(TPersistent)
  private
    FServer:    THTMLHtmlElement;
    function    GetDefaultInterface: DispHTMLHtmlElement;
    constructor Create(AServer: THTMLHtmlElement);
  protected
    procedure Set_innerHTML(const Param1: WideString);
    function Get_innerHTML: WideString;
  public
    property DefaultInterface: DispHTMLHtmlElement read GetDefaultInterface;
  published
    property innerHTML: WideString read Get_innerHTML write Set_innerHTML;
  end;
{$ENDIF}

procedure Register;

resourcestring
  dtlServerPage = 'ActiveX';

  dtlOcxPage = 'ActiveX';

implementation

uses ComObj;

class function CoHTMLDocument.Create: DispHTMLDocument;
begin
  Result := CreateComObject(CLASS_HTMLDocument) as DispHTMLDocument;
end;

class function CoHTMLDocument.CreateRemote(const MachineName: string): DispHTMLDocument;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_HTMLDocument) as DispHTMLDocument;
end;

procedure THTMLDocument.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{25336920-03F9-11CF-8FD0-00AA00686F13}';
    IntfIID:   '{3050F55F-98B5-11CF-BB82-00AA00BDCE0B}';
    EventIID:  '{3050F260-98B5-11CF-BB82-00AA00BDCE0B}';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure THTMLDocument.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    ConnectEvents(punk);
    Fintf:= punk as DispHTMLDocument;
  end;
end;

procedure THTMLDocument.ConnectTo(svrIntf: DispHTMLDocument);
begin
  Disconnect;
  FIntf := svrIntf;
  ConnectEvents(FIntf);
end;

procedure THTMLDocument.DisConnect;
begin
  if Fintf <> nil then
  begin
    DisconnectEvents(FIntf);
    FIntf := nil;
  end;
end;

function THTMLDocument.GetDefaultInterface: DispHTMLDocument;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor THTMLDocument.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := THTMLDocumentProperties.Create(Self);
{$ENDIF}
end;

destructor THTMLDocument.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function THTMLDocument.GetServerProperties: THTMLDocumentProperties;
begin
  Result := FProps;
end;
{$ENDIF}

procedure THTMLDocument.InvokeEvent(DispID: TDispID; var Params: TVariantArray);
begin
  case DispID of
    -1: Exit;  // DISPID_UNKNOWN

  end; {case DispID}
end;

function THTMLDocument.Get_Script: IDispatch;
begin
    Result := DefaultInterface.Script;
end;

function THTMLDocument.Get_body: IHTMLElement;
begin
    Result := DefaultInterface.body;
end;

procedure THTMLDocument.Set_designMode(const Param1: WideString);
  { Warning: The property designMode has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.designMode := Param1;
end;

function THTMLDocument.Get_designMode: WideString;
begin
    Result := DefaultInterface.designMode;
end;


function THTMLDocument.execCommand(const cmdID: WideString; showUI: WordBool): WordBool;
begin
  Result := DefaultInterface.execCommand(cmdID, showUI, EmptyParam);
end;

function THTMLDocument.execCommand(const cmdID: WideString; showUI: WordBool; value: OleVariant): WordBool;
begin
  Result := DefaultInterface.execCommand(cmdID, showUI, value);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor THTMLDocumentProperties.Create(AServer: THTMLDocument);
begin
  inherited Create;
  FServer := AServer;
end;

function THTMLDocumentProperties.GetDefaultInterface: DispHTMLDocument;
begin
  Result := FServer.DefaultInterface;
end;

function THTMLDocumentProperties.Get_Script: IDispatch;
begin
    Result := DefaultInterface.Script;
end;

function THTMLDocumentProperties.Get_all: IHTMLElementCollection;
begin
    Result := DefaultInterface.all;
end;

function THTMLDocumentProperties.Get_body: IHTMLElement;
begin
    Result := DefaultInterface.body;
end;

procedure THTMLDocumentProperties.Set_designMode(const Param1: WideString);
  { Warning: The property designMode has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.designMode := Param1;
end;

function THTMLDocumentProperties.Get_designMode: WideString;
begin
    Result := DefaultInterface.designMode;
end;
{$ENDIF}

class function CoHTMLHtmlElement.Create: DispHTMLHtmlElement;
begin
  Result := CreateComObject(CLASS_HTMLHtmlElement) as DispHTMLHtmlElement;
end;

class function CoHTMLHtmlElement.CreateRemote(const MachineName: string): DispHTMLHtmlElement;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_HTMLHtmlElement) as DispHTMLHtmlElement;
end;

procedure THTMLHtmlElement.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{3050F491-98B5-11CF-BB82-00AA00BDCE0B}';
    IntfIID:   '{3050F560-98B5-11CF-BB82-00AA00BDCE0B}';
    EventIID:  '{3050F33C-98B5-11CF-BB82-00AA00BDCE0B}';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure THTMLHtmlElement.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    ConnectEvents(punk);
    Fintf:= punk as DispHTMLHtmlElement;
  end;
end;

procedure THTMLHtmlElement.ConnectTo(svrIntf: DispHTMLHtmlElement);
begin
  Disconnect;
  FIntf := svrIntf;
  ConnectEvents(FIntf);
end;

procedure THTMLHtmlElement.DisConnect;
begin
  if Fintf <> nil then
  begin
    DisconnectEvents(FIntf);
    FIntf := nil;
  end;
end;

function THTMLHtmlElement.GetDefaultInterface: DispHTMLHtmlElement;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor THTMLHtmlElement.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := THTMLHtmlElementProperties.Create(Self);
{$ENDIF}
end;

destructor THTMLHtmlElement.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function THTMLHtmlElement.GetServerProperties: THTMLHtmlElementProperties;
begin
  Result := FProps;
end;
{$ENDIF}

procedure THTMLHtmlElement.InvokeEvent(DispID: TDispID; var Params: TVariantArray);
begin
  case DispID of
    -1: Exit;  // DISPID_UNKNOWN
  end; {case DispID}
end;

procedure THTMLHtmlElement.Set_innerHTML(const Param1: WideString);
  { Warning: The property innerHTML has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.innerHTML := Param1;
end;

function THTMLHtmlElement.Get_innerHTML: WideString;
begin
    Result := DefaultInterface.innerHTML;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor THTMLHtmlElementProperties.Create(AServer: THTMLHtmlElement);
begin
  inherited Create;
  FServer := AServer;
end;

function THTMLHtmlElementProperties.GetDefaultInterface: DispHTMLHtmlElement;
begin
  Result := FServer.DefaultInterface;
end;

procedure THTMLHtmlElementProperties.Set_innerHTML(const Param1: WideString);
  { Warning: The property innerHTML has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.innerHTML := Param1;
end;

function THTMLHtmlElementProperties.Get_innerHTML: WideString;
begin
    Result := DefaultInterface.innerHTML;
end;
{$ENDIF}

procedure Register;
begin
  RegisterComponents(dtlServerPage, [THTMLDocument]);
end;

end.
