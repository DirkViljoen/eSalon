object Form2: TForm2
  Left = 320
  Top = 266
  Width = 299
  Height = 157
  Caption = 'Form2'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 16
    Top = 20
    Width = 28
    Height = 13
    Caption = 'Name'
  end
  object Label2: TLabel
    Left = 18
    Top = 52
    Width = 25
    Height = 13
    Caption = 'Email'
  end
  object textName: TEdit
    Left = 72
    Top = 16
    Width = 185
    Height = 21
    TabOrder = 0
  end
  object textEmail: TEdit
    Left = 72
    Top = 48
    Width = 185
    Height = 21
    TabOrder = 1
  end
  object btnOK: TButton
    Left = 104
    Top = 80
    Width = 73
    Height = 25
    Caption = 'OK'
    TabOrder = 2
    OnClick = btnOKClick
  end
  object btnCancel: TButton
    Left = 184
    Top = 80
    Width = 73
    Height = 25
    Caption = 'Cancel'
    TabOrder = 3
    OnClick = btnCancelClick
  end
end
