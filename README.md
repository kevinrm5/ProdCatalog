# Product Management Console App  

## 📌 Overview  
This console application reads product data from a CSV file, processes it, and sends it to an API for storage. It also handles database migrations and ensures the data is inserted correctly. Additionally, a test project is included to run unit tests for the application's functionality.

---

## 🛠️ Setup Instructions

### **1 Add the `TestExampleFile.csv` File**  
Create a `TestExampleFile.csv` file in the **root directory** of the project with the following format:

```csv
ProductName,ProductCode,CategoryName,CategoryCode
Laptop,D001,Electronics,C001
Smartphone,D002,Electronics,C001
Washing Machine,D003,Home Appliances,C002
Refrigerator,D004,Home Appliances,C002
Microwave,D005,Home Appliances,C002
Desk Chair,D006,Furniture,C003
Dining Table,D007,Furniture,C003
T-Shirt,D008,Clothing,C004
Jeans,D009,Clothing,C004
Running Shoes,D010,Sports,C005
Basketball,D011,Sports,C005
Yoga Mat,D012,Sports,C005
Book - Science Fiction,D013,Books,C006
Book - Mystery,D014,Books,C006
Guitar,D015,Musical Instruments,C007
Keyboard Piano,D016,Musical Instruments,C007
Headphones,D017,Electronics,C001
Smartwatch,D018,Electronics,C001
```

---

### **2 Set the File Path in Command Line Arguments**  
1. In **Visual Studio**, go to **Project → Properties**.  
2. Select **Debug**.  
3. In the **Application Arguments** field, enter the path to your `TestExampleFile.csv` file, like this:
   ```
   "C:\path\to\your\TestExampleFile.csv"
   ```
4. Save the settings.

---

### **3 Run Database Migration**  
Before running the application, set up the database schema using **Entity Framework Core**.

1. Open **Package Manager Console** in Visual Studio.
2. Run the following command to create the migration:
   Add-Migration InitialCreate
   
3. Apply the migration to the database:
   Update-Database

4. Your database schema is now created and ready for use.

---

### **4 Run the Application**  
Once everything is set up, **start the project**

If successful, you will see:
```
Your data has been inserted successfully!
```

---

### **5 Run Unit Test Cases**  
A test project (`ProductTestCases`) is included to verify the functionality of the application.

1. **Start the application**.
2. Open the **Package Manager Console** in Visual Studio.
3. Select **ProductTestCases** from the default project dropdown.
4. Navigate to the test project directory:
   cd ProductTestCases

5. Run the test cases:
   dotnet test

If successful, you will see the test results in the console output.


## 🎯 **You're Now Ready to Use and Test the Application! 🚀**  
