## Mini Account System — Project Overview ##
The Mini Account System is a web-based accounting management tool built using ASP.NET Core Razor Pages with role-based access control (RBAC). 
It is designed for small businesses, accounting teams or organizations that want to manage financial transactions, chart of accounts, vouchers and user access securely and efficiently.


## Technical Details ##
Backend: ASP.NET Core Razor Pages

Authentication: ASP.NET Core Identity for user and role management

Database: SQL Server with stored procedures managing core CRUD operations

Frontend: Razor Pages with vanilla JavaScript for dynamic form manipulation


## User Roles & Access ##
Admin (Yash)	Can manage all users, roles, chart of accounts, vouchers.

Viewer (Dhoni)	Can view existing data only; cannot add, edit or delete.

Accountant (Rohan) Can create and manage vouchers, manage chart of accounts, but cannot manage users and roles.



## Screenshots ##

![Screenshot (685)](https://github.com/user-attachments/assets/83662727-94e5-4ff6-98e8-2f98ea32b301)

Home Page with Login and Register button.

![Screenshot (686)](https://github.com/user-attachments/assets/f99c6355-bca0-47fb-b974-7a06aec287a9)

Register a new user.

![Screenshot (687)](https://github.com/user-attachments/assets/9b0124cd-20c4-48a8-be95-1cd87e4d52a4)

User Profile.
