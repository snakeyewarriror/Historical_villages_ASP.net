# Historical Villages of Portugal - ASP.NET Web Application

## Overview
This project is a web application developed in **ASP.NET (.NET Framework)** that provides a platform for sharing information about historical villages in Portugal. The platform allows users to explore, rate, and comment on different locations while providing authenticated users with the ability to contribute their own content.

## Features

### **User Roles:**
1. **Anonymous Users:**
   - Can browse published locations
   - Can register to create an account

2. **Authenticated Users:**
   - Can publish information about locations
   - Can comment and rate existing locations
   - Can edit personal details (name, email, etc.)
   - Can modify or delete locations they have added

### **Location Information:**
- Each location includes:
  - Name
  - Description
  - Address (if applicable)
  - Locality, District, and Municipality
  - Image gallery (displayed using Bootstrap Carousel)
  - Comments and user ratings
  - Map with latitude/longitude support
  - Current weather of each location

- Homepage displays the latest published locations
- Top-rated locations section featuring the highest-rated places

### **Database & Authentication:**
- **SQL Server database** managed through SQL Server Management Studio (SSMS)
- **User authentication** managed via ASP.NET Membership API
- **Session-based user tracking**

### **User Dashboard (Personal Area):**
- Displays a list of locations submitted by the logged-in user
- Provides options to **create**, **edit**, and **delete** locations
- Ability to edit user´s information

## Technologies Used
- **ASP.NET Web Forms (.NET Framework)**
- **SQL Server Database** (locais.sql script for database creation)
- **Bootstrap** for responsive design
- **jQuery** for dynamic interactions
- **ASP.NET Membership API** for user authentication and management


## Project Structure
```
├── Final_project/               # Main project directory
│   ├── utilizador/              # Forms for user-created locations
│   ├── imagens/                 # Image storage folder
│   ├── Global.asax              # Application startup logic
│   ├── Web.config               # Configuration settings
│   ├── login.aspx               # User authentication page
│   ├── criar_conta.aspx         # User registration page
│   ├── areaPessoal.aspx         # User dashboard
│   ├── criarLocal.aspx          # Create a new location
│   ├── editarLocal.aspx         # Edit an existing location
│   ├── paginaInicial.aspx       # Homepage displaying locations
│   ├── verLocal.aspx            # Detailed location view
│   ├── Scripts/                 # jQuery & Bootstrap scripts
├── Historical_villages_ASP_NET_solution.sln # Visual Studio Solution file
```


## License
This project is licensed under the MIT License.

---
**Developed as part of a Web Server Programming course**

