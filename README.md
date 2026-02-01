# 🚗 DVLD Desktop Application  
### Three-Layer Architecture (Presentation • Business Logic • Data Access)

---

## 📘 Overview  

**DVLD Desktop Application** is a C# desktop management system for handling **driver and vehicle licensing services**, following a **three-layer architecture** for better organization, scalability, and maintainability.

It provides modules to manage applications, drivers, users, and license services — similar to a real Driver & Vehicle Licensing Department (DVLD) workflow.

---

## 🏗️ Architecture Overview  

This project follows the **3-tier architecture**:

1. **Presentation Layer (UI)**  
   - Built with WinForms.  
   - Provides menus and interfaces for managing people, drivers, users, and license services.  

2. **Business Logic Layer (BLL)**  
   - Contains validation, business rules, and the core logic that connects the UI and data layers.  

3. **Data Access Layer (DAL)**  
   - Handles all communication with the SQL Server database.  
   - Manages CRUD (Create, Read, Update, Delete) operations securely and efficiently.  

---


## ✨ Features  

### 🧍 People Management  
- Add, edit, and view personal information.  
- Manage drivers and their associated data.  

### 🚘 Driving License Services  
- **New Driving License**  
  - Local or International.  
- **Renew Driving License**  
- **Replacement** for lost or damaged licenses.  
- **Release Detained License**  
- **Retake Driving Test**  


---

### 📋 Manage Applications  
- View and handle:  
  - Local Driving License Applications  
  - International License Applications  

---

### ⚙️ Manage Types  
- Manage **Application Types** and **Test Types** easily through the settings panel.  

### 👥 User & Account Management  
- Manage user accounts, credentials, and roles.

---

## 🗃️ Database  

- The system uses **SQL Server** as the backend database.  
- Connection details can be configured in the `App.config` file.  
- Includes tables for drivers, users, licenses, applications, and test types.
