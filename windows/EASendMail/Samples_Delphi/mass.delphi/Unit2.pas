unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TForm2 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    textName: TEdit;
    textEmail: TEdit;
    btnOK: TButton;
    btnCancel: TButton;
    procedure btnOKClick(Sender: TObject);
    procedure btnCancelClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form2: TForm2;

implementation

{$R *.dfm}

procedure TForm2.btnOKClick(Sender: TObject);
begin
if Length(textEmail.Text) = 0 then
  Application.MessageBox( 'Please input email address!', '')
else
  ModalResult := mrOK;

end;

procedure TForm2.btnCancelClick(Sender: TObject);
begin
ModalResult := mrCancel;

end;

end.
