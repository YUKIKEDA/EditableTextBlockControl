# EditableTextBlockControl

## 概要

`EditableTextBlockControl` は、WPF（Windows Presentation Foundation）向けのカスタムコントロールです。通常の `TextBlock` のようにテキストを表示しつつ、ダブルクリックや特定の操作で編集可能な `TextBox` に切り替わる機能を提供します。

## 主な機能
- 通常時は `TextBlock` としてテキストを表示
- 編集時は `TextBox` に切り替わり、テキストの編集が可能
- 編集完了時に元の `TextBlock` に戻る
- バインディング対応

## 使い方
1. プロジェクトに `EditableTextBlockControl` を追加します。
2. XAML でコントロールを配置します。

```xml
<Window ...>
    <Grid>
        <local:EditableTextBlock Text="編集可能なテキスト" />
    </Grid>
</Window>
```

- `Text` プロパティにバインディングも可能です。

## 必要条件
- .NET Framework 4.5 以上 または .NET Core 3.1 以上
- WPF アプリケーション

## インストール
1. このリポジトリをクローンまたはダウンロードします。
2. プロジェクトにソースファイルを追加します。

## ライセンス
このプロジェクトは MIT ライセンスのもとで公開されています。