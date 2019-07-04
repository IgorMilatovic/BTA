var categoryName = document.querySelectorAll(".category");

for (var i = 0; i < categoryName.length; i++) {
    var catId = document.querySelector("#hidden" + (i + 1)).value;
    var cityId = document.querySelector("#hiddenCityityId").value;
    $(".recentCatPois" + (i + 1)).load(`/POIs/RecentPoisByCategory?catId=${catId}&cityId=${cityId}`)
}

