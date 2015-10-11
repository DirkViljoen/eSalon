object Form1: TForm1
  Left = 192
  Top = 107
  Width = 661
  Height = 453
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnResize = FormResize
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 6
    Top = 10
    Width = 23
    Height = 13
    Caption = 'From'
  end
  object Label2: TLabel
    Left = 6
    Top = 54
    Width = 13
    Height = 13
    Caption = 'To'
  end
  object Label3: TLabel
    Left = 6
    Top = 77
    Width = 13
    Height = 13
    Caption = 'Cc'
  end
  object Label4: TLabel
    Left = 6
    Top = 99
    Width = 36
    Height = 13
    Caption = 'Subject'
  end
  object Label5: TLabel
    Left = 72
    Top = 32
    Width = 260
    Height = 13
    Caption = 'Please separate multiple email addresses with comma(,)'
  end
  object Label9: TLabel
    Left = 6
    Top = 160
    Width = 45
    Height = 13
    Caption = 'Encoding'
  end
  object Label10: TLabel
    Left = 6
    Top = 188
    Width = 59
    Height = 13
    Caption = 'Attachments'
  end
  object textFrom: TEdit
    Left = 71
    Top = 8
    Width = 281
    Height = 21
    TabOrder = 0
  end
  object textTo: TEdit
    Left = 71
    Top = 50
    Width = 281
    Height = 21
    TabOrder = 1
  end
  object textCc: TEdit
    Left = 71
    Top = 74
    Width = 281
    Height = 21
    TabOrder = 2
  end
  object textSubject: TEdit
    Left = 71
    Top = 100
    Width = 281
    Height = 21
    TabOrder = 3
  end
  object GroupBox1: TGroupBox
    Left = 368
    Top = 0
    Width = 273
    Height = 177
    TabOrder = 4
    object Label6: TLabel
      Left = 9
      Top = 17
      Width = 31
      Height = 13
      Caption = 'Server'
    end
    object Label7: TLabel
      Left = 9
      Top = 64
      Width = 22
      Height = 13
      Caption = 'User'
    end
    object Label8: TLabel
      Left = 9
      Top = 89
      Width = 46
      Height = 13
      Caption = 'Password'
    end
    object textServer: TEdit
      Left = 72
      Top = 14
      Width = 193
      Height = 21
      TabOrder = 0
    end
    object chkAuth: TCheckBox
      Left = 9
      Top = 40
      Width = 249
      Height = 17
      Caption = 'My server requires user authentication'
      TabOrder = 1
      OnClick = chkAuthClick
    end
    object textUser: TEdit
      Left = 72
      Top = 59
      Width = 193
      Height = 21
      Color = cl3DLight
      Enabled = False
      TabOrder = 2
    end
    object textPassword: TEdit
      Left = 72
      Top = 86
      Width = 193
      Height = 21
      Color = cl3DLight
      Enabled = False
      PasswordChar = '*'
      TabOrder = 3
    end
    object chkSSL: TCheckBox
      Left = 9
      Top = 112
      Width = 248
      Height = 25
      Caption = 'My server requires secure connection (SSL)'
      TabOrder = 4
    end
    object lstProtocol: TComboBox
      Left = 8
      Top = 144
      Width = 257
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 5
    end
  end
  object chkSign: TCheckBox
    Left = 6
    Top = 129
    Width = 105
    Height = 17
    Caption = 'Digital Signature'
    TabOrder = 5
  end
  object chkEncrypt: TCheckBox
    Left = 142
    Top = 129
    Width = 123
    Height = 17
    Caption = 'Encryption'
    TabOrder = 6
  end
  object lstCharset: TComboBox
    Left = 72
    Top = 155
    Width = 201
    Height = 21
    Style = csDropDownList
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ItemHeight = 13
    ParentFont = False
    TabOrder = 7
  end
  object textAttachment: TEdit
    Left = 72
    Top = 184
    Width = 473
    Height = 21
    Color = clYellow
    Enabled = False
    TabOrder = 8
  end
  object btnAdd: TButton
    Left = 551
    Top = 184
    Width = 41
    Height = 21
    Caption = 'Add'
    TabOrder = 9
    OnClick = btnAddClick
  end
  object btnClear: TButton
    Left = 600
    Top = 184
    Width = 41
    Height = 21
    Caption = 'Clear'
    TabOrder = 10
    OnClick = btnClearClick
  end
  object textBody: TMemo
    Left = 8
    Top = 216
    Width = 633
    Height = 153
    Lines.Strings = (
      'textBody')
    ScrollBars = ssVertical
    TabOrder = 11
  end
  object btnSend: TButton
    Left = 512
    Top = 376
    Width = 129
    Height = 25
    Caption = 'Send'
    TabOrder = 12
    OnClick = btnSendClick
  end
end
