## **Website User Guide**

### **Table of Contents:**
1. **Home Page**
2. **Submitting a Claim**
3. **Viewing Claims (Lecturer Claims)**
4. **Viewing Claim Details (Bootstrap Modal)**
5. **Approving or Rejecting Claims**
6. **Error Messages and Validation**

===========================================================================

### 1. **Home Page**

**Purpose:**  
The Home page provides an overview of the website. From here, you can navigate to various sections of the site using the navigation bar.

**Features:**
- Navigation bar with links to submit new claims and view claims.
- Quick access to submit claims via the “Submit a Claim” button.

===========================================================================

### 2. **Submitting a Claim**

**How to Submit a Claim:**
1. **Navigate to the 'Submit a Claim' page:**
   - Use the navigation bar to select the "Submit a Claim" option.
2. **Fill out the claim form:**
   - **Lecturer ID**: Enter your lecturer ID.
   - **Submission Date**: Choose the date for claim submission (Note: the date cannot be in the future).
   - **Hours Worked**: Enter the number of hours you worked (positive values only).
   - **Hourly Rate**: Enter your hourly rate (positive values only).
   - **Total Claim Amount**: Automatically calculated as Hours Worked * Hourly Rate.
   - **Additional Notes**: Optional field for any extra information related to the claim.
   - **Supporting Documents**: Upload supporting documents. Only `.docx`, `.pdf`, and `.xlsx` files are allowed, and the file size must be less than 2MB.
3. **Submit the form** by clicking the "Submit" button.

**Validation:**  
The system will display error messages if:
- Hours worked or hourly rate are negative.
- The submission date is in the future.
- Unsupported file types are uploaded or the file size exceeds 2MB.

**Confirmation:**  
Once the claim is submitted successfully, you will see a success message.

===========================================================================

### 3. **Viewing Claims (Lecturer Claims)**

**Purpose:**  
The **Lecturer Claims** page allows lecturers to view all the claims they have submitted.

**How to Access:**
1. Navigate to the **Lecturer Claims** page using the navigation bar.
2. A table displaying all your claims will be shown. The table includes the following details:
   - **Claim ID**
   - **Lecturer ID**
   - **Submission Date**
   - **Hours Worked**
   - **Hourly Rate**
   - **Total Claim Amount**
   - **Claim Status (Pending, Approved, or Rejected)**
   - **Additional Notes**
   - **Document Names** (Names of the supporting files submitted)

**Note:**  
Lecturers can **view** their claims, but cannot approve or reject claims. Only administrators can modify claim statuses.

===========================================================================

### 4. **Viewing Claim Details (Bootstrap Modal)**

**Purpose:**  
The system provides a way to view the details of a claim in more depth using a Bootstrap modal pop-up.

**How to Use:**
1. On the **Lecturer Claims** page, each claim has a "View Details" button.
2. Clicking this button will open a modal pop-up with detailed information about the claim.

===========================================================================

### 5. **Approving or Rejecting Claims**

**Purpose:**  
Administrators have the ability to approve or reject claims from the **Lecturer Claims** page.

**How to Access:**
- In the future, this functionality will be added to allow only authorized users (administrators) to approve or reject claims.

**Current State:**  
This feature is restricted for now, and lecturers can only **view** their claims without modifying the status.

===========================================================================
### 6. **Error Messages and Validation**

The system includes various validation checks to ensure the data entered is correct:

- **Negative Values**: Hours worked and hourly rate cannot be negative. An error message will display if negative values are entered.
- **Invalid Date**: The submission date cannot be in the future.
- **File Type**: Only `.docx`, `.pdf`, and `.xlsx` files are allowed for supporting documents.
- **File Size**: Files larger than 2MB are not allowed. If a file exceeds the size limit, you will see an error message.
- **Form Validation**: All fields must be completed correctly. If any required fields are left empty or incorrectly filled, an error message will be shown.

===========================================================================

### **Conclusion:**

This guide provides a comprehensive overview of how to navigate the website, submit claims, view your claims, and understand validation rules. If you encounter any issues, please ensure that all inputs comply with the validation requirements, and you will receive helpful feedback messages in case of errors.



# CMCS - Contract Monthly Claim System

## Overview

This project is a Contract Monthly Claim System (CMCS) that allows users to submit, approve, and track claims. The system has different user roles including Admin/Coordinator, HR, and Lecturer. The project uses **ASP.NET Core**, **Entity Framework Core**, and **SQL Server** for database management.

## Prerequisites

Before setting up the database, ensure you have the following installed:

1. **SQL Server** (Express or Developer edition)
2. **Visual Studio 2022** (or any compatible IDE)
3. **.NET SDK 8.0** (for building and running the project)
4. **SQL Server Management Studio (SSMS)** (for managing the SQL Server instance)

## Database Setup Instructions

### 1. **Clone the Project**

Clone the repository to your local machine:
```bash
git clone https://github.com/TakuMurwira/PROG6216-CMCS-POE-.git
```

### 2. **Configure Database Connection String**

1. Open the project in **Visual Studio**.
2. In the **appsettings.json** file, update the connection string to point to your local SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ClaimsDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```
   - If you're using a different instance of SQL Server or a different computer, adjust the `Server` part accordingly.
   
   Example for a local SQL Server instance:
   ```json
   "DefaultConnection": "Server=YourServerName;Database=ClaimsDb;Trusted_Connection=True;"
   ```

### 3. **Create the Database**

#### Option 1: **Using Entity Framework Migrations**

1. Open the **Package Manager Console** in Visual Studio:
   - `Tools` > `NuGet Package Manager` > `Package Manager Console`.
   
2. Run the following command to apply the migrations and create the database:
   ```bash
   Update-Database
   ```

   This will automatically create the database (`ClaimsDb`) in your SQL Server instance.

#### Option 2: **Manual Database Setup (without EF Migrations)**

If you are unable to use EF migrations or prefer a manual setup:

1. Open **SQL Server Management Studio (SSMS)** and connect to your SQL Server instance.
2. Execute the `CREATE DATABASE` script to create the database:
   ```sql
   CREATE DATABASE ClaimsDb;
   GO
   ```

3. After the database is created, you can manually create tables using the SQL script generated by EF Core, or use the following approach:
   - Copy the SQL schema (tables, relationships, etc.) from the existing database on the original machine.
   - Run the scripts to set up the database structure.

### 4. **Seed the Database (Optional)**

You may want to seed some initial data, such as roles (Admin, HR, Lecturer). You can use the `SeedRolesAndAdminAsync` method in the `Program.cs` file to do this.

1. In **Program.cs**, ensure the seeding code is included:
   ```csharp
   public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
   {
       var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
       var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

       // Create roles
       string[] roles = { "Admin", "HR", "Lecturer" };
       foreach (var role in roles)
       {
           if (!await roleManager.RoleExistsAsync(role))
           {
               await roleManager.CreateAsync(new IdentityRole(role));
           }
       }

       // Seed Admin user
       var adminEmail = "admin@email.com";
       var adminPassword = "Admin123!";
       var adminUser = await userManager.FindByEmailAsync(adminEmail);
       if (adminUser == null)
       {
           var newAdmin = new IdentityUser
           {
               UserName = "admin",
               Email = adminEmail,
               EmailConfirmed = true
           };
           var result = await userManager.CreateAsync(newAdmin, adminPassword);
           if (result.Succeeded)
           {
               await userManager.AddToRoleAsync(newAdmin, "Admin");
           }
       }
   }
   ```

2. Ensure the application runs the seeding code on first start-up (as shown in the `Program.cs`).

---

### 5. **Running the Application**

Once the database is set up, follow these steps to run the application:

1. Build the project in Visual Studio:
   - `Build` > `Build Solution` or press `Ctrl + Shift + B`.

2. Start the application:
   - Press `Ctrl + F5` to run the application without debugging, or `F5` to run with debugging.

3. Navigate to `http://localhost:5000` (or the configured URL) in your browser.

### 6. **Verifying the Database Connection**

After running the application, you should be able to log in with the seeded Admin account (or create new users). You can also verify the database connection by checking if data is being saved to the `ClaimsDb` database.

### 7. **Managing Database Changes**

If you need to make changes to the database schema (e.g., adding new tables or columns), you can use **Entity Framework Migrations** to generate migration scripts:

1. Run the following command in **Package Manager Console**:
   ```bash
   Add-Migration YourMigrationName
   ```

2. Apply the migration to update the database:
   ```bash
   Update-Database
   ```

---

## Troubleshooting

- **Database Connection Issues:** Ensure the connection string is correctly configured. Test the connection in SSMS first to verify access to the SQL Server instance.
- **Missing Roles or Admin User:** If roles or the admin user are missing, you can re-run the `SeedRolesAndAdminAsync` method to create them again.
- **EF Core Migrations Not Working:** Ensure the database context is correctly configured and all migration files are applied.

---

## License

This project is licensed under the MIT License.



### **REFERENCES:**
https://learn.microsoft.com/en-us/ef/core/
https://youtu.be/gNvuZTg0H1k?si=9c2l-a-wZ1GZed8T
OpenAI.2024.Chat-GPT(Version3.5).[Large language mode]. available at: https://chatgpt.com/.
Colour scheme: https://coolors.co/
W3Schools (2022). HTML Tutorial. [online] W3schools.com. Available at:https://www.w3schools.com/html/default.asp [Accessed 9 Sep. 2024].
w3schools (2020). W3.CSS Home. [online] W3schools.com. Available at:https://www.w3schools.com/w3css/default.asp [Accessed 9 Sep. 2024]
