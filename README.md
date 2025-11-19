# 🌐 HomeDecor Store

## 📌 Overview
The **E-Commerce Management System** is designed to streamline the management of products and orders for online retail businesses. This platform facilitates efficient cart operations, order tracking, and inventory management, making it an ideal solution for small to medium-sized e-commerce enterprises. By automating essential processes, the system enhances operational efficiency and improves user experience for both customers and administrators.

## ✨ Features
- **Product Management**: Enable seamless addition, updating, and removal of products from the inventory.
- **Order Tracking**: Keep track of order statuses from placement through to delivery.
- **Cart Operations**: Manage user sessions and allow products to be added to a shopping cart easily.
- **User Authentication**: Secure user login and registration to provide personalized experiences.
- **Exception Handling**: Comprehensive management of errors and exceptions throughout the application.

## 🛠 Tech Stack
- **Languages**: C#
- **Framework**: .NET
- **ORM**: Entity Framework
- **Configuration**: XML files for managing application settings.

## 🏗 Architecture
The system utilizes a **3-layer architecture** composed of:
- **Data Access Layer (DAL)**: Responsible for all database interactions and data model definitions.
- **Business Logic Layer (BLL)**: Contains core business rules and application logic focused on product and order management.
- **Presentation Layer (PL)**: Handles user interactions and processes requests, delivering a user-friendly interface.

This structured approach facilitates easy maintenance, testing, and scalability, ensuring the system can adapt as it grows.

## 📂 Folder Structure
```plaintext
ECommerceManagementSystem/
├── BL/
│   ├── BL.csproj
│   ├── IProduct.cs
│   ├── IOrder.cs
│   └── ... (other business logic components)
├── DalFacade/
│   ├── DalFacade.csproj
│   ├── DO/
│   └── ... (data access components)
├── DalList/
│   ├── DalList.csproj
│   └── ... (list implementations)
├── xml/
│   ├── product.xml
│   ├── orders.xml
```

### ▶️ Running the Project
To run the project, navigate to the project directory and execute the following commands:
```bash
dotnet restore
dotnet run
```

### 🖼 Suggested Screenshots
- **Admin Panel**
- **Analytics / Charts Page**
- **Upload Page**