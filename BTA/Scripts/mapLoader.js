function getLocation() {
   if (navigator.geolocation) {

       return navigator.geolocation.getCurrentPosition(showPosition);
   } else {
       console.log("Geolocation is not supported by this browser.");
   }
}

function showPosition(position) {
   let loc = [position.coords.latitude, position.coords.longitude];
   geoLoc(loc[0], loc[1])
   return loc;
}


function geoLoc(lat, lng) {
   var uluru = { lat, lng };
   var center = new google.maps.LatLng(lat, lng);
   $(document).ready(function () {
       map = new google.maps.Map(document.querySelector(".map"), {
           zoom: 13,
           center: center,
           mapTypeId: google.maps.MapTypeId.ROADMAP
       });
       marker = new google.maps.Marker({ position: uluru, map: map });
   });
}

getLocation();

if (document.querySelector(".map")) {
   
    
   geoLoc(-34.397, 150.644)

   document.querySelector("#CitySelect").addEventListener("change", function () {

       setTimeout(function () {
           let lat = Number(document.querySelector("#LatHidden").value);
           let lng = Number(document.querySelector("#LonHidden").value);
           geoLoc(lat, lng)
       }, 333)
   })
}

