

////function ValidateEmail(mail) {
////    var mail_format = /^w+([.-]?w+)*@w+([.-]?w+)*(.w{2,3})+$/;
////    ///^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
////    if (mail_format.test(mail)) {
////        return (true)
////    }
////    return (false)
////}

function ValidateEmail(mail) {
    console.log(mail);
    
    if(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
        return (true)
    }
    //  alert("You have entered an invalid email address!")
    return (false)
}
