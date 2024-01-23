document.addEventListener("DOMContentLoaded", function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;

            document.cookie = `latitude=${latitude}; path=/`;
            document.cookie = `longitude=${longitude}; path=/`;

        }, function (error) {
            if (error.code === error.PERMISSION_DENIED) {
                
                alert("To use our website, please enable location services in your browser settings.");
                window.location.href = "/";
            } else {
                console.error("Error getting geolocation:", error.message);
            }
        });
    } else {
        console.error("Geolocation is not supported by this browser.");
    }
});
