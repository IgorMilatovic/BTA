
var countrySelect = document.querySelector("#Country");
var country2Select = document.querySelector("#Country2");
var countryHidden = document.querySelector("#ChoosenCountry");
var citySelect = document.querySelector("#CitySelect");
var city2Select = document.querySelector("#CitySelect2");
var buttonCategories = document.querySelector("#CategoriesButton");
var buttonFeedbacks = document.querySelector("#FeedbacksButton");
var contentWrapper = document.querySelector("#ContentWrapper");
var moduleId = document.querySelector("#ModuleId").value;

var cityImg = document.querySelector(".city-img");




//City select filling
function fillCities(countryValue, city) {
    countryHidden.value = countryValue;

    var xhr;

    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    if (xhr == null) {
        console.log("Your browser does not support XMLHTTP!");
    }
    xhr.open("GET", `/Modules/PopulateCities?value=${countryHidden.value}`);
    xhr.send(null);

    var content;

    (xhr.onreadystatechange = function () {
        if (xhr.status === 200 && xhr.readyState === 4) {
            let xhrt = xhr.responseText;
            conParser(xhrt);
        }
    })();

    function conParser(xhrt) {
        content = JSON.parse(xhrt);
        fillCitySelect(content, city);
    };
}

function fillCitySelect(content, citySlct) {
     citySlct.innerHTML = '<option selected="selected">Choose city</option>';
    var s;
    for (var i = 0; i < content.length; i++) {
        s += '<option>' + content[i].CityName + '</option>';
    }
    citySlct.innerHTML += s;
}

countrySelect.addEventListener("change", function () {
    fillCities(countrySelect.value, citySelect);
})
//City select filling ENDS

window.addEventListener("load", function () {

    $(".page-delay").css("opacity", "0").fadeOut(10).removeClass("invisible");
    setTimeout(function () {
        $(".page-delay").css("opacity", "1").fadeIn(1000);
    }, 300);

    $(".city-img").attr("src", "/Assets/Images/Cities/Default.jpg");
    $("#ContentWrapper").load(`/POIs/RecentPOIs?id=${moduleId}`);
})

citySelect.addEventListener("change", function () {

    $(".city-img").fadeOut(300)
    setTimeout(function () {
        $(".city-img").attr("src", `/Assets/Images/Cities/${citySelect.value}.jpg`).fadeIn(300);
        buttonFeedbacks.style.opacity = "1";
        buttonCategories.style.opacity = "1";
    }, 300);
})

function loadCategories() {
    if (citySelect.value != "Choose city") {
        $("#ContentWrapper").load(`/Categories/CategoriesList?cityName=${citySelect.value}&id=${moduleId}`, function () { }).hide().fadeIn(750);
    }
}

var loadFeedbacks = () => {
    if (citySelect.value != "Choose city") {
        $("#ContentWrapper").load(`/Comments/CityCommentsList?cityName=${citySelect.value}`, function() {}).hide().fadeIn(750);
    }
}

buttonCategories.addEventListener("click", loadCategories);
buttonFeedbacks.addEventListener("click", loadFeedbacks);

citySelect.addEventListener("change", function () {
    loadCategories();
    if (moduleId == "30") {
        setTimeout(function () {
            $(".categoriesRow").addClass("d-none");
        }, 50);
    }
})



//Transport page
if (moduleId == "30") {

    document.querySelectorAll(".fadeinButton").forEach(function (item) {
        item.classList.add("d-none");
    })

    document.querySelectorAll(".transportEl").forEach(function (item) {
        item.classList.remove("d-none");
    })

    country2Select.addEventListener("change", function () {
        fillCities(country2Select.value, city2Select);
    })

    city2Select.addEventListener("change", function () {
        if (city2Select.value != "Choose city") {
            $("#ContentWrapper2").load(`/Lines/LinesList?source=${citySelect.value}&destination=${city2Select.value}`, function () { }).hide().fadeIn(750);
        }
    })
}




    



    
