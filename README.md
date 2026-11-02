# ReFrame for Chiyu

ちゆ (plium) 用の ReFrame です。アバターに入っている衣装・髪・小物・ギミックのうち **使わないものにチェックを入れるだけで、アップロード時に自動で取り除きます。** 元のアセットは書き換えません。

## 導入

1. VCC に ReFrame のリポジトリを追加します: <https://reframe.illusive-isc.jp/>
2. `ReFrame for Chiyu` をプロジェクトに追加します。共通部分の ReFrameCore は一緒に入ります。

## 使い方

1. Hierarchy でちゆを右クリック → **ILLUSORY OVERRIDE → ReFrame → このアバターに ReFrame を追加**
2. `ReFrame` オブジェクトの Inspector で、使わない項目にチェックを入れます。チェックした項目は Scene と Hierarchy から消えて見えます。
3. いつもどおりアップロードします。

## 選べる項目

- **服**: カーディガン / ワンピース / ソックス / ローファー / ベレー帽 / 頭のリボン / ヘアピン / レギンス・パンツ / 下着
- **髪**: 耳・尻尾 / ツインテール / 横髪 / 前髪の長さ / 前髪の形
- **表情**: まばたき OFF / 表情ロック / なでなで / 表情差分 / もぐもぐ / 胸の大きさ
- **小物**: 鞄 / 小物を触って操作 / 眼鏡 / 食べ物 / スマホ / ハンディファン / ぬいぐるみ
- **その他**: ジャンプ・ダッシュ / AFK / AFK のぬいぐるみ / エモート

## Quest 簡易対応版

Inspector 上部の **「Quest 簡易対応版を作成」** で Quest 用の設定が増えます。Quest で動かないコンポーネント・揺れ物・マテリアルをここで減らし、ビルドターゲットが Android のときに使われます。詳しくは ReFrameCore の README を見てください。

## ライセンス・連絡先

- [MIT License](LICENSE)
- 作者: illusive_isc — <https://x.com/illusive_isc>
- 配布物: <https://illusive-isc.booth.pm/>
