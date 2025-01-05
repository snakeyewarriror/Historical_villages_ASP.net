document.addEventListener('DOMContentLoaded', function () {

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
});
