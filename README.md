# Hospice Tools Coding Exercise

## 📋 Overview
This is a Single Page Application (SPA) developed as part of a technical exercise for **Hospice Tools**. The application manages **Patient** color preferences, allowing users to collect and visualize data and favorite colors with full server-side persistence.

## 🛠️ Tech Stack
* **Back-End:** .NET 8 (C#)
* **Front-End:** Angular 21+
* **Database:** PostgreSQL (Hosted via [NeonDB](https://neon.com/))
* **Package Manager:** pnpm

---

## 🚀 Getting Started

### 1. Prerequisites
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Node.js (v24.13.0](https://nodejs.org/)
* [pnpm](https://pnpm.io/installation)

### 2. Database Configuration
This project uses a hosted PostgreSQL instance on **NeonDB**.
* The connection string is already configured in `appsettings.json` for immediate use.
* All data is persisted to this server-side store and will remain available at NeonDB.

### 3. Running the Back-End (.NET 8)
1. To run using Visual Studio, open the solution file: `HospiceToolsChallenge.sln`. and run "http" launch profile
   
2. To run using the CLI you can navigate to the Back-end directory `/back-end` and run via CLI:
   ```bash
   dotnet watch run --project HospiceToolsChallenge.Api/HospiceToolsChallenge.Api.csproj
   ```

### 4. Running the Front-End (Angular 21)
1. Navigate to the Front-end directory `/front-end` and run via CLI:
2. Install dependencies:
   ```bash
   pnpm install
   ```
3. Start the application:
   ```bash
   pnpm start
   ```
4. Access the SPA in your browser at: `http://localhost:4200`.

### 5. Authentication
Access the application using the default credentials:
* **Username:** `admin`
* **Password:** `admin`

---

## 💡 Key Features
* **Patient Dashboard:** A primary view featuring a table of all prior entries.
* **Preference Statistics:** Dynamic analytics displaying which colors are most popular within specific age groups.
* **Patient CRUD:** User-friendly form and table to create, read, update and delete patients.
---

*This project was developed as part of a coding exercise for Hospice Tools.*
