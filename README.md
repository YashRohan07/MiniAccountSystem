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
