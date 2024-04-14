document.addEventListener("DOMContentLoaded", function () {
    validateAccountForm()
   
})



function validateAccountForm(){

    const form = document.getElementById('passwordForm')

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        const currentPassword = document.getElementById('currentPassword').value
        const newPassword = document.getElementById('newPassword').value
        const confirmPassword = document.getElementById('confirmPassword').value

        if (currentPassword === '' || newPassword === '' || confirmPassword === '') {
            alert('All fields are required')
            return
        }

        if (newPassword !== confirmPassword) {
            alert('New password and confirm password do not match')
            return
        }

        alert('Password validation successful!')
        form.reset()
    })
}

