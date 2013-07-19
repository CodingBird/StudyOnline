//调整左侧导航条的高度
var mainHeight = $("#main").css("height");
mainHeight = mainHeight.replace("px","");
//alert(mainHeight);
var newHeight = parseInt(mainHeight) + 20;
$("#nav").css("height",newHeight+"px");

$("#pre-ellipsis").hide();
var totalPages = parseInt($("#pagination-last").html());
var currentPage = $("#pagination-ul-container>li.active>a").html();
//alert(totalPages);
//alert(currentPage);

if (totalPages > 10) {   //页面量大于10时，隐藏之后的a标签,显示省略号
    $("#next-ellipsis").show();
    $("#pagination-ul-container>a:gt(10):not(#pagination-last):not(#next-ellipsis)").hide(); //初始时，大于10的页标签不显示
} else {
    $("#next-ellipsis").hide();
    //alert("12");
}

controlPagingDisplay(currentPage, totalPages);
$("#ajax-paging").show();   //显示翻页

//$(document).ready(function () {
//    $.enablePaging();
//});
//$("li>a:first").addClass("pageIndex_Selected");

$("#pagination-ul-container>li").click(function () {
    $(this).addClass("active");
    //$(this).parent().css("border", "1px solid yellow");
    //$(this).parent().siblings().css("border", "1px solid blue");
    //$(this).parent().siblings().each(function () {
    //    $(this).children().removeClass("pageIndex_Selected");
    //});
    $(this).siblings().removeClass("active");

    var currentPage = parseInt($(this).children().html());
    var totalPages = parseInt($("#pagination-last").html());
    //alert(totalPages);
    //alert(currentPage);
    controlPagingDisplay(currentPage, totalPages);
});
function controlPagingDisplay(currentPage, totalPages) {
    //alert("1");
    if (currentPage > 7) {     //控制前省略号
        $("#pre-ellipsis").show();
    } else {
        //alert("1");
        $("#pre-ellipsis").hide();
    }
    if ((totalPages - currentPage) <= 6) {  //控制后省略号
        $("#next-ellipsis").hide();
    } else {
        $("#next-ellipsis").show();
    }
    $("#pagination-ul-container>li>a:not(#pagination-last):not(#next-ellipsis):not(#pagination-first):not(#pre-ellipsis)").each(function () {
        var index = parseInt($(this).html());
        //alert(index);
        if (Math.abs(currentPage - index) <= 5) {
            $(this).parent().show();
        } else {
            $(this).parent().hide();
        }
    });
}