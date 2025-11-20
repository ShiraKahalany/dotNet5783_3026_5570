# 🪞 HomeDecor Store

## 📌 Overview  
HomeDecor is an efficient Management System that empowers online retailers to manage products, orders, and customer interactions seamlessly. Ideal for small to medium-sized businesses, it simplifies cart operations, order tracking, and inventory management, significantly enhancing operational efficiency and user satisfaction.

## ✨ Features  
- **Product Management**: Effortlessly add, update, or remove products from your inventory.  
- **Order Tracking**: Monitor order statuses from placement to delivery in real-time.  
- **Cart Operations**: Simplified management of user sessions for smooth shopping experiences.  
- **User Authentication**: Secure login and registration for personalized customer experiences.  
- **Exception Handling**: Robust management of errors throughout the application.  

## 🛠 Tech Stack  
- **Languages**: C#  
- **Framework**: .NET  
- **ORM**: Entity Framework  
- **Configuration**: XML files    

## 🏗 Architecture  
HomeDecoremploys a **3-layer architecture**:  
- **Data Access Layer (DAL)**: Manages all database interactions and data models.  
- **Business Logic Layer (BLL)**: Enforces core business rules around product and order management.  
- **Presentation Layer (PL)**: Engages users and processes requests via a user-friendly interface.  

## 📂 Folder Structure  
```plaintext
BloomCart/
├── BL/                   # Business Logic
├── DalFacade/           # Data Access Interface
├── DalList/             # List Implementations
├── xml/                 # XML Configuration Files
└── PL/                  # Presentation Layer
```

## ▶️ How to Run  
```bash
dotnet restore
dotnet run
```
