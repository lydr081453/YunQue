/// <reference path="jquery.js"/>
var dialogFirst = true;
function dialog(title, content, width, height, cssName) {

    hidselect();

    if (dialogFirst == true) {
        var temp_float = new String;
        temp_float = "<div id=\"floatBoxBg\"  style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0);opacity:0;position:absolute;z-index:99999;\"></div>";
        temp_float += "<div id=\"floatBox\" class=\"floatBox\" style=\"position:absolute;z-index:999999;overflow:hidden\">";
        temp_float += "<div class=\"title\" ><h4></h4><span onclick=\"hidselect();onPageSubmit();\">\u5173\u95ED</span></div>";
        //temp_float += "<div class=\"title\" onclick=\"hidselect();onPageSubmit();\"><h4></h4><span>关闭</span></div>";
        temp_float += "<div  class=\"content\" style=\"position:absolute;z-index:999999;overflow:hidden\"></div>";
        temp_float += "</div>";
        $("body").append(temp_float);
        dialogFirst = false;
    }

    $("#floatBox .title span").click(function() {
        $("#floatBoxBg").animate({ opacity: "0" }, 1, 'linear', function() { $(this).hide(); });
        $("#floatBox").animate({ top: ($(document).scrollTop() - (height == "auto" ? 300 : parseInt(height))) + "px" }, 1, 'linear', function() { $(this).hide(); });
    });

    $("#floatBox .title h4").html(title);
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "url":
            var content_array = content.split("?");
            $("#floatBox .content").ajaxStart(function() {
                $(this).html("loading...");
            });
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#floatBox .content").html("error...");
                },
                success: function(html) {
                    $("#floatBox .content").html(html);
                }
            });
            break;
        case "text":
            $("#floatBox .content").html(content);
            break;
        case "id":
            $("#floatBox .content").html($("#" + content + "").html());
            break;
        case "iframe":
            $("#floatBox .content").html("<iframe src=\"" + content + "\" width=\"100%\" height=\"" + (parseInt(height) - 30) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");

    }

    $("#floatBoxBg").show();
    $("#floatBoxBg").animate({ opacity: "0.5" }, "normal");
    $("#floatBox").attr("class", "floatBox " + cssName);
    $("#floatBox").css({ display: "block", left: (($(document).width()) / 2 - (parseInt(width) / 2)) + "px", top: ($(document).scrollTop() - (height == "auto" ? 300 : parseInt(height))) + "px", width: width, height: height });
    $("#floatBox").animate({ top: ($(document).scrollTop() + 50) + "px" }, "normal");
}

function hidselect() {
    var selects = document.getElementsByTagName("select");
    for (var i = 0; i < selects.length; i++) {
        if (selects[i].style.visibility == "") {
            selects[i].style.visibility = "hidden";
        } else {
            selects[i].style.visibility = "";
        }
    }

}

function hiddiv() {

    $("#floatBoxBg").animate({ opacity: "0" }, 1, 'linear', function() { $(this).hide(); });
    $("#floatBox").animate({ top: ($(document).scrollTop() - (height == "auto" ? 300 : parseInt(height))) + "px" }, 1, 'linear', function() { $(this).hide(); });
}