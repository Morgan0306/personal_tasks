# 任務管理系統(未完成)

## 摘要
任務管理系統是一個專為個人及團隊設計的任務管理系統。它能夠讓使用者輕鬆管理任務、追蹤進度，並實現任務委派及即時通知功能。系統將會使用 ASP.NET Core 8 MVC 框架結合 SQL Server、SignalR 和 Hangfire 等技術打造。

## 預計功能
- **使用者認證：** 帳號登入 & 身份驗證
- **任務管理：** 創建、編輯、刪除任務，支持任務分類與搜索篩選
- **任務委派：** 部門主管/管理者專屬的帳號管理功能（新增、編輯、刪除其他帳戶）
- **日曆視圖：** 按日期查看任務排程
- **即時通知：** 利用 SignalR 推送任務提醒與進度報告
- **背景任務：** 使用 Hangfire 進行數據備份與提醒機制

## 使用工具
- **IDE:** Visual Studio
- **語言:** C#
- **框架:** ASP.NET Core 8 MVC
- **資料庫:** SQL Server
- **版本控制:** Git
- **RTC即時通訊:** SignalR(待更新)
- **背景工作:** Hangfire(待更新)
- **前端:** Razor + Bootstrap

## 登入畫面
<img src="https://cdn.discordapp.com/attachments/984816932010201129/1350022849687060561/image.png?ex=67d53a41&is=67d3e8c1&hm=144d1bb161461b6c25862395802f3c2c31f38325c9e3bee5022075ea8b8bfcdd&" alt="登入示意圖" width="600"/>

## 任務列表(待更新)
<img src="https://media.discordapp.net/attachments/984816932010201129/1350022850052096060/image.png?ex=67d53a41&is=67d3e8c1&hm=890cc379b00df95f9128384f6a68c08a0f1c6eb0ba34186e90d11347391cafb5&=&format=webp&quality=lossless&width=830&height=622" alt="任務列表示意圖" width="600"/>

## 帳號管理功能(權限控制)
<img src="https://cdn.discordapp.com/attachments/984816932010201129/1351441611904651317/image.png?ex=67da6394&is=67d91214&hm=0705e4be7353f591151d23b33088c73a0a87dba2857b38833e99f03797e74d98&" alt="帳號管理示意圖" width="600"/>

## 用戶搜尋
<img src="https://media.discordapp.net/attachments/984816932010201129/1351441072903159848/image.png?ex=67da6314&is=67d91194&hm=77389b71a7ab5ece19f53d4b99f7df57250efe79d09112c06dcdc2184f308e08&=&format=webp&quality=lossless&width=1095&height=752" alt="用戶搜尋示意圖" width="600"/>

## 帳號註冊畫面
<img src="https://media.discordapp.net/attachments/984816932010201129/1351441073184051222/image.png?ex=67da6314&is=67d91194&hm=cb9ea364782787fce7b963b358516005699906b3431c4d05d148db9ef46bd5af&=&format=webp&quality=lossless&width=1054&height=752" alt="帳號註冊示意圖" width="600"/>

## 帳號編輯
<img src="https://media.discordapp.net/attachments/984816932010201129/1351441073498751027/image.png?ex=67da6314&is=67d91194&hm=f18bfe7d5dc1206b3d7611167a079ba7f0ea75693438a4d2d78b3dfc374344f3&=&format=webp&quality=lossless&width=1054&height=752" alt="帳號編輯示意圖" width="600"/>

## 帳號刪除
<img src="https://media.discordapp.net/attachments/984816932010201129/1351441073859330078/image.png?ex=67da6314&is=67d91194&hm=f44c45c10d33d5f93cd64f6db3dae4ae85f9cf91eb6410e5778f3a65805760b1&=&format=webp&quality=lossless&width=1054&height=752" alt="帳號刪除示意圖" width="600"/>
