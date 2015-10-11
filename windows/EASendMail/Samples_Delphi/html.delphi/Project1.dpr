program Project1;

uses
  Forms,
  Unit1 in 'Unit1.pas' {Form1},
  EASendMailObjLib_TLB in 'EASendMailObjLib_TLB.pas',
  MSHTML_TLB in 'MSHTML_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
