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
<img src="https://media.discordapp.net/attachments/984816932010201129/1350022850458947648/image.png?ex=67d53a41&is=67d3e8c1&hm=a949a4338c576bc97de7bb0b817e6eb13eefb644d6681c6b41a644a0393bc331&=&format=webp&quality=lossless&width=830&height=622" alt="帳號管理示意圖" width="600"/>

## 用戶搜尋
<img src="https://media.discordapp.net/attachments/984816932010201129/1350022850807070731/image.png?ex=67d53a41&is=67d3e8c1&hm=97430e5923e23cffeb68b5ca2496a6b72719d83712ea0b8ca1931b8a9e7701cf&=&format=webp&quality=lossless&width=830&height=622" alt="用戶搜尋示意圖" width="600"/>

## 帳號註冊畫面
<img src="https://media.discordapp.net/attachments/984816932010201129/1350022851133964319/image.png?ex=67d53a41&is=67d3e8c1&hm=9ed8f0c77ec3ab5d05b8781f19e583d18093a497ca928ca0b78cc0f2354d9dd8&=&format=webp&quality=lossless&width=830&height=622" alt="帳號註冊示意圖" width="600"/>

## 帳號編輯
<img src="https://media.discordapp.net/attachments/984816932010201129/1350022851511582760/image.png?ex=67d53a42&is=67d3e8c2&hm=5b6f3b93fc08d7a8053e90bf59b9c1b6398c3187b61291e752370e0e4245aea3&=&format=webp&quality=lossless&width=830&height=622" alt="帳號編輯示意圖" width="600"/>

## 帳號刪除
<img src="https://media.discordapp.net/attachments/984816932010201129/1350022851956047893/image.png?ex=67d53a42&is=67d3e8c2&hm=f7a6f68bd7c3b243106978d1b23d2adbf8838e3bd5a74fc4340b3eb0fb398ea3&=&format=webp&quality=lossless&width=830&height=622" alt="帳號刪除示意圖" width="600"/>
