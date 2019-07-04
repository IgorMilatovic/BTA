let comments = document.querySelector(".comment-section");
//let id = document.URL.split('/')[document.URL.split('/').length - 1];
//let table = document.URL.split('/')[document.URL.split('/').length - 3];
let id = document.querySelector("#PoiId").value;
let tableNameValue = document.querySelector("#TableName").value;
let newCommentButton = document.querySelector(".add-comment-button"),
    newCommentBox = document.querySelector(".add-comment");

//console.log(table);

window.onload = () => {
    
    $(".comment-section").load(`/Comments/Index?itemId=${id}&table=${tableNameValue}`);
    $(".add-comment").load(`/Comments/Create/itemId=${id}`);

    //document.querySelector("#tableInsert").value = table;
    //document.querySelector("#idInsert").value = id;

    newCommentButton.onclick = () => {
        newCommentBox.style.display == "none" ? newCommentBox.style.display = "block" : newCommentBox.style.display = "none";
        ///////////
        var parentId = document.querySelector("#parentId");
        parentId.value = id;
        var tableName = document.querySelector("#tableName");
        tableName.value = tableNameValue;
    }
}