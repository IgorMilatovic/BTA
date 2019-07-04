var xhr;

if (window.XMLHttpRequest) {
    xhr = new XMLHttpRequest();
}
if (xhr == null) {
    console.log("Your browser does not support XMLHTTP!");
}
if (!xhr.open("GET", "../Modules/NavBar")) xhr.open("GET", "../../Modules/NavBar");
//if (xhr.status === 404) { }
xhr.send(null);

var items = [];
let menu;

(xhr.onreadystatechange = function () {
    if (xhr.status === 200 && xhr.readyState === 4) {
        let xhrt = xhr.responseText;
        conParser(xhrt);
    }
})();
function conParser(xhrt) {
    items = JSON.parse(xhrt);
    //console.log(items);
    loadMenu(items);
};

let navBar = document.querySelector(".navbar-nav");

function loadMenu(items) {
    for (let i = 0; i < items.Result.length; i++) {
        
        navBar.innerHTML += `<li class="nav-item">
            <a class="nav-link" href="../../Modules/Details/${items.Result[i].ModuleId}">${items.Result[i].ModuleName}<span class="sr-only">(current)</span></a>
        </li>`;
        
    }

    let currentModule = document.querySelector(".moduleName");

    var navLinks = document.querySelectorAll(".nav-link");
   
    for (var i = 0; i < navLinks.length; i++) {
        if (currentModule) {
            if (navLinks[i].innerHTML.split("<")[0] == currentModule.innerHTML) {
                navLinks[i].style.color = "orange";
            }
            else {
                navLinks[i].style.color = "white";
            }
        }
    }
}


    

