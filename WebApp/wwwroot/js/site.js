document.addEventListener("DOMContentLoaded", function() {
    validateForm()
    //validateAccountForm()
    mobilViewOn()
    enableDarkModeSwitch()
    select()
    search()
    handleSidebarImageUpload()
})

function enableDarkModeSwitch() {

    try
    {
        let sw = document.querySelector('#switch-mode')
        sw.addEventListener('change', function () {
            let mode = this.checked ? 'dark' : 'light'

            localStorage.setItem('mode', mode)
            sessionStorage.setItem('mode', mode)


            fetch(`/sitesettings/theme?mode=${mode}`)
                .then(res => {
                    if (res.ok)
                        window.location.reload()
                    else
                     console.log('unable to switch to theme mode')
                })
        })
    }
    catch {
        console.log('unable to switch to theme mode')

    }

}

function mobilViewOn() {
    let btnMobile = document.querySelector('.btn-mobile')
    let nav = document.querySelector('nav')

    btnMobile.addEventListener('click', () => {
        btnMobile.classList.toggle('active')
        btnMobile.classList.toggle('fixed')

        nav.classList.toggle('active')
    })

    window.addEventListener('resize', () => {
        btnMobile.classList.remove('active')
        btnMobile.classList.remove('fixed')

        nav.classList.remove('active')
    })

}


function validateForm(){
    let forms = document.querySelectorAll('form')
    let inputs = forms[0].querySelectorAll('input')

    inputs.forEach(input => {
        if (input.dataset.val === 'true') {
            input.addEventListener('keyup', (e) => {
                switch (e.target.type) {
                    case 'text':
                        textValidation(e, e.target.dataset.valMinlengthMin)
                        break
                    case 'email':
                        emailValidation(e)
                        break
                    case 'password':
                        passwordValidation(e)
                        break

                }
            })
        }
    })



    const handleValidationOutput = (isValid, e, text = "") => {
        let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

        if (isValid) {
            e.target.classList.remove('input-validation-error')
            if (span) {
                span.classList.remove('field-validation-error')
                span.classList.add('field-validation-valid')
                span.innerHTML = ""
            } 

        } else {
            e.target.classList.add('input-validation-error')
            if (span) {
                span.classList.add('field-validation-error')
                span.classList.remove('field-validation-valid')
                span.innerHTML = text
            }

        }
    }

   


    const textValidation = (e, minLength = 2) => {
        if (e.target.value.length > 0)
            handleValidationOutput(e.target.value.length >= minLength, e, e.target.dataset.valMinlength)
        else
            handleValidationOutput(false, e, e.target.dataset.valRequired)

    }


    const emailValidation = (e) => {
        const regEx = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$/

        if (e.target.value.length > 0)
            handleValidationOutput(regEx.test(e.target.value), e, e.target.dataset.valRegex)
        else
            handleValidationOutput(false, e, e.target.dataset.valRequired)

    }

    document.querySelectorAll('input[name="Password"], input[name="ConfirmPassword"]').forEach(input => {
        input.addEventListener('input', () => {
            checkPasswordMatch();
        })
    })

    const passwordValidation = (e) => {
        const regEx = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?!.*\s).{8,}$/;

        let compareTo = document.querySelector(e.target.dataset.valEqualtoOther)

        if (e.target.value.length > 0)
            handleValidationOutput(regEx.test(e.target.value), e, e.target.dataset.valRegex)
        else
            handleValidationOutput(false, e, e.target.dataset.valRequired)

    }

    const checkPasswordMatch = () => {
        let passwordInput = document.querySelector('input[name="Password"]')
        let confirmPasswordInput = document.querySelector('input[name="ConfirmPassword"]')

        if (passwordInput.value !== confirmPasswordInput.value) {
            handleValidationOutput(false, confirmPasswordInput, confirmPasswordInput.dataset.valEqualTo)
        } else {
            handleValidationOutput(true, confirmPasswordInput)
        }
    }

    
}


function select() {
    try {
        let select = document.querySelector('.select')
        let selected = select.querySelector('.selected')
        let selectOptions = select.querySelector('.select-option')

        selected.addEventListener('click', function () {

            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'

        })

        let options = selectOptions.querySelectorAll('.option')

        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')

                updateCoursesbyFilter(category)
            })
        })
    }
    catch {
    }
}

function updateCoursesbyFilter(category) {
    const searchValue = document.getElementById('searchQuery').value
    const url = `/courses/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchValue)}`

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.courses-box').innerHTML = dom.querySelector('.courses-box').innerHTML

            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML: ''
            document.querySelector('.pagination').innerHTML = pagination
        })

}

function search(){
    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            const searchValue = this.value
            const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
            const url = `/courses/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchValue)}`

            fetch(url)
                .then(res => res.text())
                .then(data => {
                    const parser = new DOMParser()
                    const dom = parser.parseFromString(data, 'text/html')
                    document.querySelector('.courses-box').innerHTML = dom.querySelector('.courses-box').innerHTML

                    const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
                    document.querySelector('.pagination').innerHTML = pagination
                })
        })

    } catch { }
}

function handleSidebarImageUpload() {

    try {
        let fileUploader = document.querySelector('#fileUploader')
        if (fileUploader != undefined) {
            fileUploader.addEventListener('change', function () {
                if (this.files.length > 0) {
                    this.form.submit()
                }
            })
        }

    } catch {

    }
}


//function validateAccountForm() {
//    let forms = document.querySelectorAll('form')
//    let inputs = forms[2].querySelectorAll('input')

//    inputs.forEach(input => {
//        if (input.dataset.val === 'true') {
//            input.addEventListener('keyup', (e) => {
//                switch (e.target.name) {
//                    case 'CurrentPassword':
//                        passwordValidation(e)
//                        break
//                    case 'NewPassword':
//                    case 'ConfirmPassword':
//                        passwordValidation(e)
//                        checkPasswordMatch()
//                        break
//                }
//            })
//        }
//    })

//    document.querySelectorAll('input[name = "Password"]').forEach(input => {
//        input.addEventListener('keyup', () => {
//            handleValidationOutput(true, input)
//        })
//    })

//    const handleValidationOutput = (isValid, e, text = "") => {
//        let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)


//        if (isValid) {
//            e.target.classList.remove('input-validation-error')
//            span.classList.remove('field-validation-error')
//            span.classList.add('field-validation-valid')
//            span.innerHTML = ""

//        } else {
//            e.target.classList.add('input-validation-error')
//            span.classList.add('field-validation-error')
//            span.classList.remove('field-validation-valid')
//            span.innerHTML = text
//        }
//    }

//    const passwordValidation = (e) => {
//        console.log(e.target.name)
//        const regEx = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?!.*\s).{8,}$/;

//        let compareTo = document.querySelector(e.target.dataset.valEqualtoOther)

//        if (e.target.value.length > 0)
//            handleValidationOutput(regEx.test(e.target.value), e, e.target.dataset.valRegex)
//        else
//            handleValidationOutput(false, e, e.target.dataset.valRequired)

//    }

//    const checkPasswordMatch = () => {
//        console.log(newPasswordInput)
//        let newPasswordInput = document.querySelector('input[name="NewPassword"]')
//        let confirmPasswordInput = document.querySelector('input[name="ConfirmPassword"]')

//        if (newPasswordInput.value !== confirmPasswordInput.value) {
//            handleValidationOutput(false, confirmPasswordInput, confirmPasswordInput.dataset.valEqualTo)
//        } else {
//            handleValidationOutput(true, confirmPasswordInput)
//        }
//    }

//}