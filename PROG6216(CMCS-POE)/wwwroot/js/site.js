function calculateTotalClaim() {
    const hoursWorked = parseFloat(document.getElementById("HoursWorked").value) || 0;
    const hourlyRate = parseFloat(document.getElementById("HourlyRate").value) || 0;
    const totalClaimAmount = hoursWorked * hourlyRate;

    // Set the readonly display field for user view
    document.getElementById("TotalClaimAmountDisplay").value = totalClaimAmount.toFixed(2);
    // Set the hidden field for form submission
    document.getElementById("TotalClaimAmount").value = totalClaimAmount.toFixed(2);
}


// Function to validate file uploads
function validateFiles() {
    const fileInput = document.getElementById("Document");
    const files = fileInput.files;
    const maxFileSize = 2 * 1024 * 1024; // 2 MB
    let fileNames = new Set();

    for (let i = 0; i < files.length; i++) {
        const file = files[i];

        // Check file type
        if (!file.name.endsWith(".docx") && !file.name.endsWith(".pdf")) {
            alert("Only .docx and .pdf files are allowed.");
            fileInput.value = ""; // Clear the input
            return;
        }

        // Check file size
        if (file.size > maxFileSize) {
            alert("Each file must be less than 2MB.");
            fileInput.value = ""; // Clear the input
            return;
        }

        // Check for duplicate file names
        if (fileNames.has(file.name)) {
            alert("You cannot upload multiple files with the same name.");
            fileInput.value = ""; // Clear the input
            return;
        }

        fileNames.add(file.name);
    }
}