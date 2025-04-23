// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.setTimeout(function () {
    var banner = document.getElementById('successBanner');
    if (banner) {
        var bsAlert = bootstrap.Alert.getOrCreateInstance(banner);
        bsAlert.close();
    }
}, 3000);
