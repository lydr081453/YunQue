/// <reference path="jquery.js"/>
var dialogFirst = true;
function dialogByAddMemo(title, content, width, height, cssName) {
    hidselect2();
    if (dialogFirst == true) {
        var temp_float = new String;
        temp_float = "<div id=\"floatBoxBg2\"  style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0);opacity:0;position:absolute;z-index:99999;\"></div>";
        temp_float += "<div id=\"floatBox2\" class=\"floatBox2\" style=\"position:absolute;z-index:999999;overflow:hidden\">";
        temp_float += "<div class=\"title\" ><h4></h4><span onclick=\"hidselect2();\">关闭</span></div>";
        //temp_float += "<div class=\"title\" onclick=\"hidselect();onPageSubmit();\"><h4></h4><span>关闭</span></div>";
        temp_float += "<div  class=\"content\" style=\"position:absolute;z-index:999999;overflow:hidden\"></div>";
        temp_float += "</div>";
        $("body").append(temp_float);
        dialogFirst = false;
    }

    $("#floatBox2 .title span").click(function() {
        $("#floatBoxBg2").animate({ opacity: "0" }, 1, 'linear', function() { $(this).hide(); });
        $("#floatBox2").animate({ top: ($(document).scrollTop() - (height == "auto" ? 300 : parseInt(height))) + "px" }, 1, 'linear', function() { $(this).hide(); });
    });

    $("#floatBox2 .title h4").html(title);
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "url":
            var content_array = content.split("?");
            $("#floatBox2 .content").ajaxStart(function() {
                $(this).html("loading...");
            });
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#floatBox2 .content").html("error...");
                },
                success: function(html) {
                    $("#floatBox2 .content").html(html);
                }
            });
            break;
        case "text":
            $("#floatBox2 .content").html(content);
            break;
        case "id":
            $("#floatBox2 .content").html($("#" + content + "").html());
            break;
        case "iframe":
            $("#floatBox2 .content").html("<iframe src=\"" + content + "\" width=\""+width+"px\" height=\"" + (parseInt(height) - 50) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");

    }

    $("#floatBoxBg2").show();
    $("#floatBoxBg2").animate({ opacity: "0.5" }, "normal");
    $("#floatBox2").attr("class", "floatBox2 " + cssName);
    $("#floatBox2").css({ display: "block", left: (($(document).width()) / 2 - (parseInt(width) / 2)) + "px", top: ($(document).scrollTop() - (height == "auto" ? 300 : parseInt(height))) + "px", width: width, height: height });
    $("#floatBox2").animate({ top: ($(document).scrollTop() + 50) + "px" }, "normal");
}

function hidselect2() {
    var selects = document.getElementsByTagName("select");
    for (var i = 0; i < selects.length; i++) {
        if (selects[i].style.visibility == "") {
            selects[i].style.visibility = "hidden";
        } else {
            selects[i].style.visibility = "";
        }
    }

}

function hiddiv2() {

    $("#floatBoxBg2").animate({ opacity: "0" }, 1, 'linear', function() { $(this).hide(); });
    $("#floatBox2").animate({ top: ($(document).scrollTop() - (height == "auto" ? 300 : parseInt(height))) + "px" }, 1, 'linear', function() { $(this).hide(); });
}