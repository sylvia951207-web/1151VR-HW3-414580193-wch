# 1151VR-HW3-414580193-wch
# HW3：今天喝什麼？飲料輪盤

- 學號：414580193
- 開發工具：Unity、C#、Xcode
- 執行平台：iPhone

## 作品介紹
以飲料選擇為主題，製作可以透過點擊啟動的輪盤，協助使用者隨機決定要喝的飲料。

## 操作方式
1. 點擊輪盤即可開始旋轉，並播放音效。
2. 輪盤會逐漸減速，最後停止。
3. 固定指針所指的飲料就是本次結果。
4. 停止後可以再次點擊，重新抽選。

## 製作流程
1. 匯入飲料輪盤與指針圖片，調整大小、位置及背景顏色。
2. 在輪盤加入 Circle Collider 2D，設定點擊範圍。
3. 撰寫 WheelSpin 腳本，處理點擊、隨機旋轉角度及加減速。
4. 加入 Audio Source，讓旋轉動畫配合音效長度。
5. 將指針與輪盤分開，讓輪盤旋轉時指針保持固定。
6. 使用 Unity 匯出 iOS 專案，透過 Xcode 安裝至 iPhone 測試。

## 測試結果
已在 iPhone 上完成點擊旋轉、音效播放及重複操作測試。

## 開啟專案
使用 Unity Hub 加入此專案，開啟 DrinkWheel 場景，按下 Play 即可測試。

## 操作影片
https://github.com/user-attachments/assets/21b1c1c1-9843-4938-a23f-af065afc8387

## iPhone手機版操作影片
https://github.com/user-attachments/assets/a916ace8-4026-4ef0-ac18-2bfc296b98f5


