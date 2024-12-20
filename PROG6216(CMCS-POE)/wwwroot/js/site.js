﻿function calculateTotalClaim() {
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

setTimeout(function () {
    $('.alert').alert('close');
}, 5000);

function populateModal(claimID, lecturerID, submissionDate, hoursWorked, hourlyRate, totalClaimAmount, addNotes, documentNames, claimStatus) {
    document.getElementById("modalClaimID").textContent = claimID;
    document.getElementById("modalLecturerID").textContent = lecturerID;
    document.getElementById("modalSubmissionDate").textContent = submissionDate;
    document.getElementById("modalHoursWorked").textContent = hoursWorked;
    document.getElementById("modalHourlyRate").textContent = hourlyRate;
    document.getElementById("modalTotalClaimAmount").textContent = totalClaimAmount;
    document.getElementById("modalAddNotes").textContent = addNotes;



    // Clear the document names list
    var documentList = document.getElementById('modalDocumentNames');
    documentList.innerHTML = ''; // Clear existing items
    if (documentNames) {
        var docs = documentNames.split(", ");
        docs.forEach(function (doc) {
            var li = document.createElement('li');
            li.textContent = doc;
            documentList.appendChild(li);
        });
    } else {
        var li = document.createElement('li');
        li.textContent = 'No documents uploaded';
        documentList.appendChild(li);
    }
    document.getElementById("modalClaimStatus").textContent = claimStatus;
    // Set approve/reject button URLs
    document.getElementById('approveButton').setAttribute('href', '/Claim/Approve/' + claimID);
    document.getElementById('rejectButton').setAttribute('href', '/Claim/Reject/' + claimID);
}
