document.addEventListener("DOMContentLoaded", function () {

     //initializeCustomSelect()
     validateContactForm()
   
})


function initializeCustomSelect() {
    try {
        
        let select = document.querySelector('.custom-select')
        let selected = select.querySelector('.custom-selected')
        let selectOptions = select.querySelector('.custom-options')

        selected.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.custom-option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'

                let selectedOptionValue = this.getAttribute('data-value')
                let hiddenInput = document.getElementById('Contact_HiddenSelectInput')
                hiddenInput.value = selectedOptionValue


            })
        })
    }
    catch{ }
}

function validateContactForm() {
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
                }
            })
        }
    })


    const handleValidationOutput = (isValid, e, text = "") => {
        let span = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

        if (isValid) {
            e.target.classList.remove('input-validation-error')
            span.classList.remove('field-validation-error')
            span.classList.add('field-validation-valid')
            span.innerHTML = ""

        } else {
            e.target.classList.add('input-validation-error')
            span.classList.add('field-validation-error')
            span.classList.remove('field-validation-valid')
            span.innerHTML = text
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

    initializeCustomSelect();
}



