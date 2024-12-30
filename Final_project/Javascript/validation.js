// Toggle password visibility
const togglePassword = document.getElementById('togglePassword');
const passwordInput = document.getElementById('password');
const toggleConfirmPassword = document.getElementById('toggleConfirmPassword');
const confirmPasswordInput = document.getElementById('confirmPassword');
const eyeIconPassword = document.getElementById('eyeIconPassword');
const eyeIconConfirmPassword = document.getElementById('eyeIconConfirmPassword');

// Toggle the password visibility when the icon/button is clicked
togglePassword.addEventListener('click', function () {
    const type = passwordInput.type === 'password' ? 'text' : 'password';
    passwordInput.type = type;

    // Toggle the eye icon
    eyeIconPassword.classList.toggle('bi-eye-slash');
    eyeIconPassword.classList.toggle('bi-eye');
});

// Toggle the confirm password visibility when the icon/button is clicked
toggleConfirmPassword.addEventListener('click', function () {
    const type = confirmPasswordInput.type === 'password' ? 'text' : 'password';
    confirmPasswordInput.type = type;

    // Toggle the eye icon for confirm password field
    eyeIconConfirmPassword.classList.toggle('bi-eye-slash');
    eyeIconConfirmPassword.classList.toggle('bi-eye');
});


// Function to validate username
function validateUsername() {
    const username = document.getElementById('username');
    if (username.value.length < 6 || username.value.length > 20) {
        username.classList.add('is-invalid');
    } else {
        username.classList.remove('is-invalid');
        username.classList.add('is-valid');
    }
}

// Function to validate password
function validatePassword() {
    const password = passwordInput;
    const passwordContainer = password.closest('.mb-3'); // Get the parent container of the input
    const invalidFeedback = passwordContainer.querySelector('.invalid-feedback');

    if (password.value.length < 6 || password.value.length > 15) {
        password.classList.add('is-invalid');
        password.classList.remove('is-valid');
        invalidFeedback.style.display = 'block'; // Show the invalid-feedback
    } else {
        password.classList.remove('is-invalid');
        password.classList.add('is-valid');
        invalidFeedback.style.display = 'none'; // Hide the invalid-feedback
    }
}


// Function to validate confirm password
function validateConfirmPassword() {
    const password = passwordInput;
    const confirmPassword = confirmPasswordInput;
    const passwordContainer = confirmPassword.closest('.mb-3');
    const invalidFeedback = passwordContainer.querySelector('.invalid-feedback');

    if (confirmPassword.value !== password.value || confirmPassword.value.length < 6 || confirmPassword.value.length > 15) {
        confirmPassword.classList.add('is-invalid');
        password.classList.remove('is-valid');
        invalidFeedback.style.display = 'block'; // Show the invalid-feedback
    } else {
        confirmPassword.classList.remove('is-invalid');
        confirmPassword.classList.add('is-valid');
        invalidFeedback.style.display = 'none'; // Hide the invalid-feedback
    }
}

// Event listeners for the input fields to validate on typing or blur
document.getElementById('username').addEventListener('blur', validateUsername);

document.getElementById('password').addEventListener('blur', validatePassword);

document.getElementById('confirmPassword').addEventListener('blur', validateConfirmPassword);

