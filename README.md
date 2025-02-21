# Product Management Console App  

## 📌 Overview  
This console application reads product data from a CSV file, processes it, and sends it to an API for storage. It also handles database migrations and ensures the data is inserted correctly.

---

## 🛠️ Setup Instructions

### **1️⃣ Add the `test.csv` File**  
Create a `test.csv` file in the **root directory** of the project with the following format:

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

### **2️⃣ Set the File Path in Command Line Arguments**  
1. In **Visual Studio**, go to **Project → Properties**.  
2. Select **Debug**.  
3. In the **Application Arguments** field, enter the path to your `test.csv` file, like this:
   ```
   "C:\path\to\your\test.csv"
   ```
4. Save the settings.

---

### **3️⃣ Run Database Migration**  
Before running the application, set up the database schema using **Entity Framework Core**.

1. Open **Package Manager Console** in Visual Studio.
2. Run the following command to create the migration:
   ```powershell
   Add-Migration InitialCreate
   ```
3. Apply the migration to the database:
   ```powershell
   Update-Database
   ```
4. Your database schema is now created and ready for use.

---

### **4️⃣ Run the Application**  
Once everything is set up, **run the console application** using:
```sh
dotnet run "C:\path\to\your\test.csv"
```
or simply start the project in **Visual Studio**.

If successful, you will see:
```
💪 Your data has been inserted successfully!
```

---

## 🎯 **You're Now Ready to Use the Application! 🚀**  

