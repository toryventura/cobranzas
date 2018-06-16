function reset() {
    $("#toggleCSS").attr("href", "../plugins/alertify/alertify.default.css");
    alertify.set({
        labels: {
            ok: "OK",
            cancel: "Cancel"
        },
        delay: 5000,
        buttonReverse: false,
        buttonFocus: "ok"
    });
}

// ==============================
// Standard Dialogs
function alert(mensaje) {
    reset();
    alertify.alert(mensaje);
    return false;
}
//$("#alert").on( 'click', function () {
//	reset();
//	alertify.alert("This is an alert dialog");
//	return false;
//});
function confirm(mensaje) {
    reset();
    alertify.confirm("This is a confirm dialog", function (e) {
        if (e) {
            alertify.success("You've clicked OK");
            
        } else {
            alertify.error("You've clicked Cancel");
        }
    });
    return false;

}
//$("#confirm").on('click', function () {
//    reset();
//    alertify.confirm("This is a confirm dialog", function (e) {
//        if (e) {
//            alertify.success("You've clicked OK");
//        } else {
//            alertify.error("You've clicked Cancel");
//        }
//    });
//    return false;
//});

function prompt(mensaje) {
    reset();
    alertify.prompt("This is a prompt dialog", function (e, str) {
        if (e) {
            alertify.success("You've clicked OK and typed: " + str);
        } else {
            alertify.error("You've clicked Cancel");
        }
    }, "Default Value");
    return false;
}
//$("#prompt").on('click', function () {
//    reset();
//    alertify.prompt("This is a prompt dialog", function (e, str) {
//        if (e) {
//            alertify.success("You've clicked OK and typed: " + str);
//        } else {
//            alertify.error("You've clicked Cancel");
//        }
//    }, "Default Value");
//    return false;
//});

// ==============================
//  Mensajes Ajax
function ajax(mensaje) {
    reset();
    alertify.confirm("Confirm?", function (e) {
        if (e) {
            alertify.alert("Successful AJAX after OK");
        } else {
            alertify.alert("Successful AJAX after Cancel");
        }
    });
}
//$("#ajax").on("click", function () {
//    reset();
//    alertify.confirm("Confirm?", function (e) {
//        if (e) {
//            alertify.alert("Successful AJAX after OK");
//        } else {
//            alertify.alert("Successful AJAX after Cancel");
//        }
//    });
//});

// ==============================
// Standard Dialogs
function notificacion(mensaje) {
    reset();
    alertify.log(mensaje);
    return false;
}
//$("#notification").on('click', function () {
//    reset();
//    alertify.log("Standard log message");
//    return false;
//});
function success(mensaje) {
    reset();
    alertify.success(mensaje);
    return false;
}
//$("#success").on('click', function () {
//    reset();
//    alertify.success("Success log message");
//    return false;
//});
function error(mensaje) {
    reset();
    alertify.error(mensaje);
    return false;
}
//$("#error").on('click', function () {
//    reset();
//    alertify.error("Error log message");
//    return false;
//});

// ==============================
// Custom Properties
function delay(mensaje) {
    reset();
    alertify.set({ delay: 10000 });
    alertify.log(mensaje);
    return false;
}
//$("#delay").on('click', function () {
//    reset();
//    alertify.set({ delay: 10000 });
//    alertify.log("Hiding in 10 seconds");
//    return false;
//});

function forever() {
    reset();
    alertify.log("Will stay until clicked", "", 0);
    return false;
}
//$("#forever").on('click', function () {
//    reset();
//    alertify.log("Will stay until clicked", "", 0);
//    return false;
//});
function labels(mensaje) {
    reset();
    alertify.set({ labels: { ok: "Accept", cancel: "Deny" } });
    alertify.confirm("Confirm dialog with custom button labels", function (e) {
        if (e) {
            alertify.success("You've clicked OK");
        } else {
            alertify.error("You've clicked Cancel");
        }
    });
    return false;
}
//$("#labels").on('click', function () {
//    reset();
//    alertify.set({ labels: { ok: "Accept", cancel: "Deny" } });
//    alertify.confirm("Confirm dialog with custom button labels", function (e) {
//        if (e) {
//            alertify.success("You've clicked OK");
//        } else {
//            alertify.error("You've clicked Cancel");
//        }
//    });
//    return false;
//});

function focus1() {
    reset();
    alertify.set({ buttonFocus: "cancel" });
    alertify.confirm("Confirm dialog with cancel button focused", function (e) {
        if (e) {
            alertify.success("You've clicked OK");
        } else {
            alertify.error("You've clicked Cancel");
        }
    });
    return false;
}
//$("#focus").on('click', function () {
//    reset();
//    alertify.set({ buttonFocus: "cancel" });
//    alertify.confirm("Confirm dialog with cancel button focused", function (e) {
//        if (e) {
//            alertify.success("You've clicked OK");
//        } else {
//            alertify.error("You've clicked Cancel");
//        }
//    });
//    return false;
//});

function order1(mensaje) {
    reset();
    alertify.set({ buttonReverse: true });
    alertify.confirm("Confirm dialog with reversed button order", function (e) {
        if (e) {
            alertify.success("You've clicked OK");
        } else {
            alertify.error("You've clicked Cancel");
        }
    });
    return false;
}
//$("#order").on('click', function () {
//    reset();
//    alertify.set({ buttonReverse: true });
//    alertify.confirm("Confirm dialog with reversed button order", function (e) {
//        if (e) {
//            alertify.success("You've clicked OK");
//        } else {
//            alertify.error("You've clicked Cancel");
//        }
//    });
//    return false;
//});

// ==============================
// Custom Log
function custom(mensaje) {
    reset();
    alertify.custom = alertify.extend("custom");
    alertify.custom(mensaje);
    return false;
}
//$("#custom").on('click', function () {
//    reset();
//    alertify.custom = alertify.extend("custom");
//    alertify.custom("I'm a custom log message");
//    return false;
//});

// ==============================
// Custom Themes
function bootstrap(mensaje) {
    reset();
    $("#toggleCSS").attr("href", "../plugins/alertify/alertify.bootstrap.css");
    alertify.prompt("Prompt dialog with bootstrap theme", function (e) {
        if (e) {
            alertify.success("You've clicked OK");
        } else {
            alertify.error("You've clicked Cancel");
        }
    }, "Default Value");
    return false;
}
//$("#bootstrap").on('click', function () {
//    reset();
//    $("#toggleCSS").attr("href", "../themes/alertify.bootstrap.css");
//    alertify.prompt("Prompt dialog with bootstrap theme", function (e) {
//        if (e) {
//            alertify.success("You've clicked OK");
//        } else {
//            alertify.error("You've clicked Cancel");
//        }
//    }, "Default Value");
//    return false;
//});