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

Viewer (Dhoni)	Can view existing data only. Cannot add, edit or delete.

Accountant (Rohan) Can create and manage vouchers, manage chart of accounts, but cannot manage users and roles.



## Home Page with Login and Register button ##

![Screenshot (685)](https://github.com/user-attachments/assets/83662727-94e5-4ff6-98e8-2f98ea32b301)

## Register a new user ##

![Screenshot (686)](https://github.com/user-attachments/assets/f99c6355-bca0-47fb-b974-7a06aec287a9)

## User Profile ##

![Screenshot (687)](https://github.com/user-attachments/assets/9b0124cd-20c4-48a8-be95-1cd87e4d52a4)

## Data Tables in SQL Server ##

![Screenshot (688)](https://github.com/user-attachments/assets/594baff7-4b5d-4478-b04b-902593fd6860)


## Now Login as Admin ##

![Screenshot (689)](https://github.com/user-attachments/assets/f8b7ec45-aa44-49d6-98b2-d0c8f26d0b52)

## Admin can view chart of accounts ##

![Screenshot (690)](https://github.com/user-attachments/assets/9a15d149-3b97-45ea-9520-8fc5251383f1)

## Admin can Activate or Deactivate any account ##

![Screenshot (691)](https://github.com/user-attachments/assets/6d9c442e-5d88-48dd-9da1-edb89e50be33)

## Admin can Delete any account ## 

![Screenshot (694)](https://github.com/user-attachments/assets/12f2c9b2-31aa-46e7-8748-6d2762f7ce23)

## Admin can create accounts with assign Parent/Child ## 

![Screenshot (695)](https://github.com/user-attachments/assets/0aa67713-8368-4982-9452-5b09bc6151c0)

![Screenshot (696)](https://github.com/user-attachments/assets/d1342329-fe64-471f-8f56-df3efe20fe86)


## Admin is restricted from create/insert any vouchers data, ensuring role separation ## 

![Screenshot (700)](https://github.com/user-attachments/assets/4aad9c8e-c0a4-4981-9fa2-553cb20890d9)


## Now login as Accountant ##

![Screenshot (701)](https://github.com/user-attachments/assets/5360588d-1dcc-4af4-8f75-2e75b677275b)

## Accountants can record financial transactions by creating vouchers ##

![Screenshot (703)](https://github.com/user-attachments/assets/3b4c3d91-956f-4c1b-820e-7c2a19960c0e)

![Screenshot (704)](https://github.com/user-attachments/assets/4fed96ee-e429-4d54-bc9b-f575a9a22e0e)

## Accountant Cannot Manage Users ##

![Screenshot (706)](https://github.com/user-attachments/assets/e28c2c7f-9169-4400-bc27-c001a925c373)

## Only Admin can see user details ##

![Screenshot (708)](https://github.com/user-attachments/assets/2e6807f0-a929-4226-aad9-b96a954df543)

## Admins can generate and download vouchers as a CSV file ##

![Screenshot (711)](https://github.com/user-attachments/assets/4dbb8491-ab28-4ff7-a6cc-dd52fb871eb5)

![Screenshot (714)](https://github.com/user-attachments/assets/ced11498-5f63-482f-a3d7-3daac3ec59f3)

![Screenshot (715)](https://github.com/user-attachments/assets/7a3bf5e7-f9dc-4012-a56d-251a43ed9d4f)

![Screenshot (718)](https://github.com/user-attachments/assets/86f84981-d4f6-4d1e-b729-2ef1c81fcf07)

![Screenshot (719)](https://github.com/user-attachments/assets/bbaa45c0-5890-48ec-9167-ebab4b18045a)


## Other Data Tables ##

![Screenshot (721)](https://github.com/user-attachments/assets/6e6baa33-3717-4a14-a7cd-ed5c9f21d47f)

![Screenshot (720)](https://github.com/user-attachments/assets/5983079e-cf99-47f7-9b55-5317c3db701b)

![Screenshot (724)](https://github.com/user-attachments/assets/e0e67b81-43f9-4665-9a43-b28ea706bbd1)

![Screenshot (723)](https://github.com/user-attachments/assets/bb64ce92-9b58-4b40-953b-d8df6417d2e9)

![Screenshot (722)](https://github.com/user-attachments/assets/05e9838b-865f-4212-b622-7a0c9cb229e7)










