object Form1: TForm1
  Left = 205
  Top = 245
  Width = 661
  Height = 435
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
    Top = 41
    Width = 13
    Height = 13
    Caption = 'To'
  end
  object Label4: TLabel
    Left = 6
    Top = 172
    Width = 36
    Height = 13
    Caption = 'Subject'
  end
  object Label9: TLabel
    Left = 6
    Top = 204
    Width = 45
    Height = 13
    Caption = 'Encoding'
  end
  object Label10: TLabel
    Left = 6
    Top = 232
    Width = 59
    Height = 13
    Caption = 'Attachments'
  end
  object Label3: TLabel
    Left = 424
    Top = 200
    Width = 86
    Height = 13
    Caption = 'Maximum Threads'
  end
  object textStatus: TLabel
    Left = 8
    Top = 384
    Width = 47
    Height = 13
    Caption = 'textStatus'
  end
  object textFrom: TEdit
    Left = 71
    Top = 8
    Width = 281
    Height = 21
    TabOrder = 0
  end
  object textSubject: TEdit
    Left = 71
    Top = 172
    Width = 281
    Height = 21
    TabOrder = 1
  end
  object GroupBox1: TGroupBox
    Left = 368
    Top = 0
    Width = 273
    Height = 185
    TabOrder = 2
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
  object lstCharset: TComboBox
    Left = 72
    Top = 199
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
    TabOrder = 3
  end
  object textAttachment: TEdit
    Left = 72
    Top = 228
    Width = 473
    Height = 21
    Color = clYellow
    Enabled = False
    TabOrder = 4
  end
  object btnAdd: TButton
    Left = 551
    Top = 228
    Width = 41
    Height = 20
    Caption = 'Add'
    TabOrder = 5
    OnClick = btnAddClick
  end
  object btnClear: TButton
    Left = 600
    Top = 228
    Width = 41
    Height = 20
    Caption = 'Clear'
    TabOrder = 6
    OnClick = btnClearClick
  end
  object textBody: TMemo
    Left = 8
    Top = 256
    Width = 633
    Height = 113
    Lines.Strings = (
      'textBody')
    ScrollBars = ssVertical
    TabOrder = 7
  end
  object btnSend: TButton
    Left = 512
    Top = 376
    Width = 129
    Height = 25
    Caption = 'Send'
    TabOrder = 8
    OnClick = btnSendClick
  end
  object lstTo: TListView
    Left = 72
    Top = 40
    Width = 281
    Height = 121
    Columns = <
      item
        Caption = 'Email'
        Width = 100
      end
      item
        Caption = 'Status'
        Width = 250
      end>
    GridLines = True
    TabOrder = 9
    ViewStyle = vsReport
  end
  object btnAddRcpt: TButton
    Left = 8
    Top = 64
    Width = 49
    Height = 25
    Caption = 'Add'
    TabOrder = 10
    OnClick = btnAddRcptClick
  end
  object btnClearRcpt: TButton
    Left = 8
    Top = 96
    Width = 49
    Height = 25
    Caption = 'Clear'
    TabOrder = 11
    OnClick = btnClearRcptClick
  end
  object chkTest: TCheckBox
    Left = 288
    Top = 200
    Width = 121
    Height = 17
    Caption = 'Test Email Address'
    TabOrder = 12
  end
  object textThreads: TEdit
    Left = 518
    Top = 196
    Width = 41
    Height = 21
    MaxLength = 3
    TabOrder = 13
    Text = '10'
  end
end
