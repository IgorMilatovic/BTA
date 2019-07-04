var categoryMark = document.querySelector("#categoryMark");
var category = document.querySelector("#CategoryId");
var transpPoiForm = document.querySelector(".transportPoi");
var classicPoiForm = document.querySelector(".classicPoi");
var transportArr = ["100", "190", "200", "210", "220", "230", "240"];

category.addEventListener("change", function () {
    
    if (transportArr.includes(category.value)) {
        transpPoiForm.classList.remove("d-none");
        categoryMark.value = "transportPoi";
    }
    else {
        classicPoiForm.classList.remove("d-none");
        categoryMark.value = "classicPoi";
        if (!transpPoiForm.classList.contains("d-none")) {
            transpPoiForm.classList.add("d-none");
        }
    }
})

document.querySelector("#sbmtBtn").addEventListener("click", function (e) {
    //e.preventDefault();
    document.querySelector("#weburl").value = document.querySelector("#Website").value;
    //console.log(document.querySelector("#weburl").value);
})
