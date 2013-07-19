var startTime;
var endTime;
//点击视频标题时，弹出popup
$(".videoDetail").bind("click", function (e) {
    //e.preventDefault();
    alert("1");
    $('#popup').bPopup({
        appendTo: 'body'
        , follow: [true, false]
        , zIndex: 1000
        , modalClose: false
        , modalColor: '#000'
        , modal: true
        , escClose: false                                                            
    });
});
//获取视频详细内容后，为视频也得button绑定单击事件
function bindEventAfterGetVideoDetail() {
    //点击开始学习，如果存在视频地址，执行以下操作
    //显示视频内容
    //改变第二个button的value值，使其根据value值确定要做的事
    //记录开始时间
    $("#video_btn_start").click(function () {

        $(this).attr("disabled", true);
        var url = $("#video_url").val();
        //alert(url);
        if (url != null && url != "") { //如果存在该视频
            $("#video_btn_end").val("结束学习");
            startTime = new Date().toLocaleString();
            alert("开始时间：" + startTime);
            setupVideoPlayer();
            jwplayer("mediaspace").load("/Uploads/video/" + url);   //加载视频，地址问题
        } else {
            $(".novideo_notice").show();
        }
        $("#resource_detail").show();
        $(".studyNotice").hide();
    });
    $("#video_btn_end").click(function () {
        if ($(this).val() == "结束学习") {
            jwplayer('mediaspace').pause(true);     //暂停
            var isClose = confirm("确定结束当前学习");
            if (!isClose) {
                jwplayer("mediaspace").pause(false);   //继续
                return false;
            } else {
                jwplayer("mediaspace").stop();   //结束，从内存清除
                postRecord();
            }
        }
        $('#popup').bPopup().close();

    });
}
//向后台发送学习记录数据
function postRecord() {
    endTime = new Date().toLocaleString();
    var name = $(".video_title").attr("data-title");
    var id = $("#Id").val();
    //alert(name);
    alert("开始时间是:" + startTime + "   结束时间是：" + endTime);
    $.post("/Record/Add", {
        strStart: startTime,
        strEnd: endTime,
        resourceName: name,
        resourceId: id
    }, function (data, status) {
        alert(data);
    })
}
//加载播放器
function setupVideoPlayer() {
    //alert("1");
    jwplayer("mediaspace").setup({
        autostart: true,
        height: "400",
        width: "560",
        skin: "/Scripts/mediaplayer-5.10/newtubedark.zip",
        modes: [
                    { type: "flash", src: "/Scripts/mediaplayer-5.10/player.swf" },
                    { type: "html5" }
                ]
    });
}


///处理查看文档的js代码
$(".docDetail").bind("click", function (e) {
    //e.preventDefault();
    $('#popup').bPopup({
        appendTo: 'body'
                    , follow: [true, false]
                    , zIndex: 1000
                    , modalClose: false
                    , modalColor: '#000'
                    , modal: true
                    , escClose: false
    });
});
function bindEventAfterGetDocDetail() {
    //点击开始学习，如果存在视频地址，执行以下操作
    //显示视频内容
    //改变第二个button的value值，使其根据value值确定要做的事
    //记录开始时间
    $("#btn_start").click(function () {
        $(this).attr("disabled", true);
        $("#btn_end").val("结束学习");
        startTime = new Date().toLocaleString();
        alert("开始时间：" + startTime);
        $("#resource_detail").show();
        $(".studyNotice").hide();
    });
    $("#btn_end").click(function () {
        if ($(this).val() == "结束学习") {
            postDocRecord();
        }
        $('#popup').bPopup().close();
    });
}
function postDocRecord() {
    endTime = new Date().toLocaleString();
    var name = $(".doc_title").attr("data-title");
    var id = $("#Id").val();
    //alert(id);
    alert("开始时间是:" + startTime + "   结束时间是：" + endTime);
    $.post("/Record/Add", {
        strStart: startTime,
        strEnd: endTime,
        resourceName: name,
        resourceId: id
    }, function (data, status) {
        alert(data);
    })
}