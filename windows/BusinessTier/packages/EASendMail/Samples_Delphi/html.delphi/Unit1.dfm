object Form1: TForm1
  Left = 200
  Top = 183
  Width = 656
  Height = 452
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
  object textStatus: TLabel
    Left = 8
    Top = 384
    Width = 31
    Height = 13
    Caption = 'Ready'
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
      Width = 249
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
  object btnSend: TButton
    Left = 376
    Top = 376
    Width = 129
    Height = 25
    Caption = 'Send'
    TabOrder = 11
    OnClick = btnSendClick
  end
  object htmlEditor: TWebBrowser
    Left = 8
    Top = 240
    Width = 633
    Height = 129
    TabOrder = 12
    OnNavigateComplete2 = htmlEditorNavigateComplete2
    ControlData = {
      4C0000006C410000550D00000000000000000000000000000000000000000000
      000000004C000000000000000000000001000000E0D057007335CF11AE690800
      2B2E126208000000000000004C0000000114020000000000C000000000000046
      8000000000000000000000000000000000000000000000000000000000000000
      00000000000000000100000000000000000000000000000000000000}
  end
  object lstFont: TComboBox
    Left = 7
    Top = 214
    Width = 153
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 13
    OnChange = lstFontChange
  end
  object lstSize: TComboBox
    Left = 168
    Top = 213
    Width = 113
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 14
    OnChange = lstSizeChange
  end
  object btnB: TButton
    Left = 299
    Top = 211
    Width = 33
    Height = 25
    Caption = 'B'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 15
    OnClick = btnBClick
  end
  object btnI: TButton
    Left = 331
    Top = 211
    Width = 33
    Height = 25
    Caption = 'I'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold, fsItalic]
    ParentFont = False
    TabOrder = 16
    OnClick = btnIClick
  end
  object btnU: TButton
    Left = 364
    Top = 211
    Width = 33
    Height = 25
    Caption = 'U'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold, fsUnderline]
    ParentFont = False
    TabOrder = 17
    OnClick = btnUClick
  end
  object btnC: TButton
    Left = 396
    Top = 211
    Width = 33
    Height = 25
    Caption = 'C'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 18
    OnClick = btnCClick
  end
  object btnInsert: TButton
    Left = 441
    Top = 210
    Width = 129
    Height = 25
    Caption = 'Insert Image'
    TabOrder = 19
    OnClick = btnInsertClick
  end
  object btnCancel: TButton
    Left = 512
    Top = 376
    Width = 129
    Height = 25
    Caption = 'Cancel'
    Enabled = False
    TabOrder = 20
    OnClick = btnCancelClick
  end
  object ColorDialog1: TColorDialog
    Left = 312
    Top = 136
  end
end
