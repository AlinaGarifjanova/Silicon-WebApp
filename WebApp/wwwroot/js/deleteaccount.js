document.addEventListener("DOMContentLoaded", function () {
    validateDeleteAccountForm()
})

function validateDeleteAccountForm() {
    const form = document.querySelector("form[action='/Account/DeleteAccount']")

    if (form) {
        form.addEventListener("submit", function (event) {
            event.preventDefault()

            const isDeleted = document.querySelector("input[name='DeleteAccount.IsDeleted']").checked

            if (!isDeleted) {
                alert("Please confirm that you want to delete your account by checking the checkbox!")
                return
            }

            if (!confirm("Are you sure you want to delete your account?")) {
                return false

            }

            form.submit()

        })
    }
   
}
