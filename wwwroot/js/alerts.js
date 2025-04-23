window.setTimeout(function () {
    var banner = document.getElementById('successBanner');
    if (banner) {
        var bsAlert = bootstrap.Alert.getOrCreateInstance(banner);
        bsAlert.close();
    }
}, 3000);