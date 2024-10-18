## **Website User Guide**

### **Table of Contents:**
1. **Home Page**
2. **Submitting a Claim**
3. **Viewing Claims (Lecturer Claims)**
4. **Viewing Claim Details (Bootstrap Modal)**
5. **Approving or Rejecting Claims**
6. **Error Messages and Validation**

================================================================================================

### 1. **Home Page**

**Purpose:**  
The Home page provides an overview of the website. From here, you can navigate to various sections of the site using the navigation bar.

**Features:**
- Navigation bar with links to submit new claims and view claims.
- Quick access to submit claims via the “Submit a Claim” button.

================================================================================================

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

================================================================================================

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

================================================================================================

### 4. **Viewing Claim Details (Bootstrap Modal)**

**Purpose:**  
The system provides a way to view the details of a claim in more depth using a Bootstrap modal pop-up.

**How to Use:**
1. On the **Lecturer Claims** page, each claim has a "View Details" button.
2. Clicking this button will open a modal pop-up with detailed information about the claim.

================================================================================================

### 5. **Approving or Rejecting Claims**

**Purpose:**  
Administrators have the ability to approve or reject claims from the **Lecturer Claims** page.

**How to Access:**
- In the future, this functionality will be added to allow only authorized users (administrators) to approve or reject claims.

**Current State:**  
This feature is restricted for now, and lecturers can only **view** their claims without modifying the status.

================================================================================================
### 6. **Error Messages and Validation**

The system includes various validation checks to ensure the data entered is correct:

- **Negative Values**: Hours worked and hourly rate cannot be negative. An error message will display if negative values are entered.
- **Invalid Date**: The submission date cannot be in the future.
- **File Type**: Only `.docx`, `.pdf`, and `.xlsx` files are allowed for supporting documents.
- **File Size**: Files larger than 2MB are not allowed. If a file exceeds the size limit, you will see an error message.
- **Form Validation**: All fields must be completed correctly. If any required fields are left empty or incorrectly filled, an error message will be shown.

================================================================================================

### **Conclusion:**

This guide provides a comprehensive overview of how to navigate the website, submit claims, view your claims, and understand validation rules. If you encounter any issues, please ensure that all inputs comply with the validation requirements, and you will receive helpful feedback messages in case of errors.

https://learn.microsoft.com/en-us/ef/core/
https://youtu.be/gNvuZTg0H1k?si=9c2l-a-wZ1GZed8T
OpenAI.2024.Chat-GPT(Version3.5).[Large language mode]. available at: 
https://chatgpt.com/.
Colour scheme: https://coolors.co/
W3Schools (2022). HTML Tutorial. [online] W3schools.com. Available at:
https://www.w3schools.com/html/default.asp [Accessed 9 Sep. 2024].
w3schools (2020). W3.CSS Home. [online] W3schools.com. Available at:
https://www.w3schools.com/w3css/default.asp [Accessed 9 Sep. 2024]
