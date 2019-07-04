let comments = document.querySelector(".comment-section");
let id;
let tableNameValue = document.querySelector("#TableName").value;
let newCommentButton = document.querySelector(".add-comment-button"),
    newCommentBox = document.querySelector(".add-comment");
if (document.querySelector("#PoiId")) { id = document.querySelector("#PoiId").value };
if (document.querySelector("#CityId")) { id = document.querySelector("#CityId").value };

window.onload = () => {
    
    $(".comment-section").load(`/Comments/Index?itemId=${id}&table=${tableNameValue}`);
    $(".add-comment").load(`/Comments/Create/itemId=${id}`);

    newCommentButton.onclick = () => {
        newCommentBox.style.display == "none" ? newCommentBox.style.display = "block" : newCommentBox.style.display = "none";

        var parentId = document.querySelector("#parentId");
        parentId.value = id;
        var tableName = document.querySelector("#tableName");
        tableName.value = tableNameValue;
    }
}