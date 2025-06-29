## Mini Account System — Project Overview ##
The Mini Account System is a web-based accounting management tool built using ASP.NET Core Razor Pages with role-based access control (RBAC). 
It is designed for small businesses, accounting teams or organizations that want to manage financial transactions, chart of accounts, vouchers and user access securely and efficiently.


## Technical Details ##
Backend: ASP.NET Core Razor Pages

Authentication: ASP.NET Core Identity for user and role management

Database: SQL Server with stored procedures managing core CRUD operations

Frontend: Razor Pages with vanilla JavaScript for dynamic form manipulation


## User Roles & Access ##
Admin (Yash)	Can manage all users, roles, chart of accounts.

Viewer (Dhoni)	Can view existing data only; cannot add, edit or delete.

Accountant (Rohan) Can create and manage vouchers, manage chart of accounts, but cannot manage users and roles.



## Home Page with Login and Register button ##

![Screenshot (685)](https://github.com/user-attachments/assets/83662727-94e5-4ff6-98e8-2f98ea32b301)

## Register a new user ##

![Screenshot (686)](https://github.com/user-attachments/assets/f99c6355-bca0-47fb-b974-7a06aec287a9)

## User Profile ##

![Screenshot (687)](https://github.com/user-attachments/assets/9b0124cd-20c4-48a8-be95-1cd87e4d52a4)

## Data Table from Microsoft SQL Server Management Studio

![Screenshot (688)](https://github.com/user-attachments/assets/594baff7-4b5d-4478-b04b-902593fd6860)


## Now Login as Admin ##

![Screenshot (689)](https://github.com/user-attachments/assets/f8b7ec45-aa44-49d6-98b2-d0c8f26d0b52)

## Admin can view chart of accounts ##

![Screenshot (690)](https://github.com/user-attachments/assets/9a15d149-3b97-45ea-9520-8fc5251383f1)

## Admin can Activate or Deactivate any account ##

![Screenshot (691)](https://github.com/user-attachments/assets/6d9c442e-5d88-48dd-9da1-edb89e50be33)

## Admin can Delete any account ## 

![Screenshot (694)](https://github.com/user-attachments/assets/12f2c9b2-31aa-46e7-8748-6d2762f7ce23)

## Admin can create accounts with assign parent and child ## 

![Screenshot (695)](https://github.com/user-attachments/assets/0aa67713-8368-4982-9452-5b09bc6151c0)

![Screenshot (696)](https://github.com/user-attachments/assets/d1342329-fe64-471f-8f56-df3efe20fe86)


## Admin can't create/insert any vouchers data ## 

![Screenshot (700)](https://github.com/user-attachments/assets/4aad9c8e-c0a4-4981-9fa2-553cb20890d9)






