function DesbloquearPantalla() {
    $.unblockUI();
}
function Redireccionar(url) {
    window.location = url;
}
function BloquearPantalla() {
    $.blockUI(
    {
        message: '<i class="fa fa-spinner fa-spin" style="font-size:40px;color:White; "></i>',
        css: { border: 'none', 'border-radius': '30px', padding: "0.2em", background: "none" }
    });

}
function MostrarMensaje(msj) {
    //$("#Lbl_MensajeError").html(msj);
    DesbloquearPantalla();
}
function MostrarMensaje1(msj) {
   // $("#Lbl_MensajeError1").html(msj);
    DesbloquearPantalla();
}