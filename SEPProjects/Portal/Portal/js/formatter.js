var twitterList = new Array();
function display() {

}

function parseAt() {

}

function parseVideo() {

}

function parse8Box() {

}

//V1 method
String.prototype.format = function()
{
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,                
        function(m,i){
            return args[i];
        });
}

 

//V2 static
String.format = function() {
    if( arguments.length == 0 )
        return null;

    var str = arguments[0]; 
    for(var i=1;i<arguments.length;i++) {
        var re = new RegExp('\\{' + (i-1) + '\\}','gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

//{0}userid,  {1}username, {2}usericon，{3}content,
//{4}threadid, {5}userid,  {6}username, {7}threadid,
//{8}createtime,{9}消息发布和当前时间的一个差值, {10}表示通过网页还是通过MSN
//{10}threadid,{11}threadid

//<a class='darkLink' title='{8}' href='/Thread.aspx?id={7}'>
var Template_TwitterMessage = "";
var Template_TwitterMessage2 = "<div class='odd' id='status_{12}'>\r\n";
Template_TwitterMessage2 += "<div class='head'><a href='/User.aspx?userid={0}' rel='contact'><img icon='152319' class='buddy_icon' width='48' height='48' title='{1}' src='{2}' /></a></div>\r\n\t";
Template_TwitterMessage2 += "<div class='cont'>\r\n\t";
Template_TwitterMessage2 += "<div class='bg'></div>\r\n\t";
Template_TwitterMessage2 += "{3}\r\n\t";
Template_TwitterMessage2 += "<span class='meta'>\r\n\t";
Template_TwitterMessage2 += "<span class='floatright'>\r\n\t";
Template_TwitterMessage2 += "<span class='reply'>\r\n\t";
Template_TwitterMessage2 += "<a href='/Reply.aspx?id={4}&replay' title='2008-12-09 11:03:33'>回复</a>\r\n\t";
Template_TwitterMessage2 += "</span>\r\n\t";
Template_TwitterMessage2 += "<span id='status_actions_13567518'>\r\n\t";
Template_TwitterMessage2 += "<a href='javascript:void(0);' onclick='ChangeFavourites({12}); return false;' title='收藏它'>\r\n\t";
Template_TwitterMessage2 += "<img id='status_star_{12}' border='0' src='/images/icon_star_empty.gif' />\r\n\t";
Template_TwitterMessage2 += "<span id='status_star_text_{12}'></span>\r\n\t"
Template_TwitterMessage2 += "</a>\r\n\t";
Template_TwitterMessage2 += "<input type='hidden' id='hid' value='{11}'/>";
Template_TwitterMessage2 += "</span>\r\n\t";
Template_TwitterMessage2 += "</span>\r\n\t";
Template_TwitterMessage2 += "<a class='normLink' href='User.aspx?userid={5}'>{6}</a>";
Template_TwitterMessage2 += "<a class='darkLink' title='{8}{7}' href='#'>{9}</a>&nbsp;通过&nbsp; {10}&nbsp;&nbsp; </span>";
Template_TwitterMessage2 += "</div>";
Template_TwitterMessage2 += "</div>";

var Template_TwitterMessage1 = "<div class='odd' id='status_{12}'>\r\n";
Template_TwitterMessage1 += "<div class='head'><a href='/User.aspx?userid={0}' rel='contact'><img icon='152319' class='buddy_icon' width='48' height='48' title='{1}' src='{2}' /></a></div>\r\n\t";
Template_TwitterMessage1 += "<div class='cont'>\r\n\t";
Template_TwitterMessage1 += "<div class='bg'></div>\r\n\t";
Template_TwitterMessage1 += "{3}\r\n\t";
Template_TwitterMessage1 += "<span class='meta'>\r\n\t";
Template_TwitterMessage1 += "<span class='floatright'>\r\n\t";
Template_TwitterMessage1 += "<span class='reply'>\r\n\t";
Template_TwitterMessage1 += "<a href='/Reply.aspx?id={4}&replay' title='2008-12-09 11:03:33'>回复</a>\r\n\t";
Template_TwitterMessage1 += "</span>\r\n\t";
Template_TwitterMessage1 += "<span id='status_actions_13567518'>\r\n\t";
Template_TwitterMessage1 += "<a href='javascript:void(0);' onclick='ChangeFavourites({12}); return false;' title='收藏它'>\r\n\t";
Template_TwitterMessage1 += "<img id='status_star_{12}' border='0' src='/images/icon_star_empty.gif' />\r\n\t";
Template_TwitterMessage1 += "<span id='status_star_text_{12}'></span>\r\n\t"
Template_TwitterMessage1 += "</a>\r\n\t";
Template_TwitterMessage1 += "<a href='javascript:void(0);' onclick='delmessage({11});' title='删除'>";
Template_TwitterMessage1 += "<img border='0' src='/images/icon_trash.gif'/></a>";
Template_TwitterMessage1 += "</span>\r\n\t";
Template_TwitterMessage1 += "</span>\r\n\t";
Template_TwitterMessage1 += "<a class='normLink' href='User.aspx?userid={5}'>{6}</a>";
Template_TwitterMessage1 += "<a class='darkLink' title='{8}{7}' href='#'>{9}</a>&nbsp;通过&nbsp; {10}&nbsp;&nbsp; </span>";
Template_TwitterMessage1 += "</div>";
Template_TwitterMessage1 += "</div>";

function checkinfo(result, currUserid, listphoto, isadmin) {
//    // flag表示使用那套模板，1表示有删除的，2表示没有删除的
//    if (flag == 1) {
//        Template_TwitterMessage = Template_TwitterMessage1;
//    }
//    else {
//        Template_TwitterMessage = Template_TwitterMessage2;
//    }
    var messageInfos = "";
    for (var i = 0; i < result.length; i++) {
        // 首先判断用户是否是管理员，然后再判断该条信息是否但前用户发的
        if (isadmin == "True") {
            Template_TwitterMessage = Template_TwitterMessage1;
        } else if (result[i].fromuser == currUserid) {
            Template_TwitterMessage = Template_TwitterMessage1;
        } else {
            Template_TwitterMessage = Template_TwitterMessage2;
        }
        
        var tempmessageInfo = "";
        
        //****** 计算显示的时间 开始 *******//
        // 消息发布时间
        var createtimes = result[i].createtime;
        var time = new Date(createtimes);

        // 保存发布时间
        var sendtime = time.toLocaleString();
        // 保存与当前时间的差值
        var disctime = result[i].createtimestr;      
        
        //****** 计算显示的时间 结束 *******//

        var touserinfo = result[i].messagesource + " ";
        var tousernameinfo = "";
        if (result[i].touser != "" && result[i].touser != 0) {
            touserinfo += "<a href='User.aspx?userid=" + result[i].touser + "' rel='contact' class='darkLink'>给" + result[i].tousername + "的回复</a>";
            tousernameinfo = "@<a href='User.aspx?userid=" + result[i].touser + "' rel='contact' class='normLink'>" + result[i].tousername + "&nbsp;</a>";
        }
        var message = new Array(result[i].fromuser, result[i].fromusername, listphoto[i], tousernameinfo + result[i].message, result[i].id, sendtime, disctime, touserinfo);
        
        if (message.length > 0) {
            var url = message[3];
            if (url.indexOf("http://www.8box.cn/") != -1) {
                tempmessageInfo = boxContent(message);
            }
            else if (url.indexOf("http://f.yupoo.com/") != -1) {
                tempmessageInfo = yupooContent(message);
            }
            else if (url.indexOf("http://www.youtube.com/") != -1) {
                tempmessageInfo = youtubeContent(message);
            }
            else if (url.indexOf("http://www.tudou.com/") != -1) {
                tempmessageInfo = tudouContent(message);
            }
            else if (url.indexOf("http://player.youku.com/") != -1) {
                tempmessageInfo = youkuContent(message);
            }
            else {
                tempmessageInfo = String.format(Template_TwitterMessage, message[0], message[1], message[2], message[3], message[4], message[0], message[1], message[4], message[5], message[6], message[7], message[4], message[4]);
            }
        }
        messageInfos += tempmessageInfo + "<div class='line'></div>";
    }
    return messageInfos;
}

// 8box内容生成
function boxContent(messagearr){
    if (messagearr.length > 0) {
        var messageinfo = messagearr[3];
        var tempstr = "";
        var contentstr = "";
        var urlinfo = "";
        
        if (messageinfo.indexOf("http://") != -1) {
            tempstr = messageinfo.substring(0, messageinfo.indexOf("http://"));
            contentstr += tempstr;

            urlinfo = GetUrl(messageinfo);
            urlinfo = urlinfo.toString();
            if (urlinfo.indexOf("<br>") != -1) {
                urlinfo = urlinfo.substring(0, urlinfo.indexOf("<br>"));
            }
            tempstr = messageinfo.substring(messageinfo.indexOf(urlinfo) + urlinfo.length, messageinfo.length);

            // 替换URL 将http://www.8box.cn/share/s/276423 和http://www.8box.cn/ad/song/154?ref=wgt_s 格式的替换成http://www.8box.cn/feed/000000_s_276423_/mini.swf 格式的
            if (urlinfo.indexOf("/s/") != -1) {
                var tempurlinfo = urlinfo.substring(urlinfo.indexOf("/s/") + 3, urlinfo.length);
                urlinfo = "http://www.8box.cn/feed/000000_s_" + tempurlinfo + "_/mini.swf";
            } else if (urlinfo.indexOf("/song/") != -1) {
                var tempurlinfo = urlinfo.substring(urlinfo.indexOf("/song/") + 6, urlinfo.indexOf("?ref"));
                urlinfo = "http://www.8box.cn/feed/000000_s_" + tempurlinfo + "_/mini.swf";
            }
            
            contentstr += "<a class='extlink' rel='nofollow' title='指向其它网站的链接' href='" + urlinfo + "' target='_blank'>http://www.8box.cn/...</a><br />";
            contentstr += tempstr;
            contentstr += "<embed src='" + urlinfo + "' type='application/x-shockwave-flash' wmode='opaque' width='160' height='32'></embed>";
        }
        else {
            contentstr += messagearr[3];
        }
        var str = String.format(Template_TwitterMessage, messagearr[0], messagearr[1], messagearr[2], contentstr, messagearr[4], messagearr[0], messagearr[1], messagearr[4], messagearr[5], messagearr[6], messagearr[7], messagearr[4], messagearr[4]);
        return str;
    }
}

// yupoo内容生成
function yupooContent(messagearr) {
    if (messagearr.length > 0) {
        var messageinfo = messagearr[3];
        var tempstr = "";
        var contentstr = "";
        var urlinfo = "";
        if (messageinfo.indexOf("http://") != -1) {
            tempstr = messageinfo.substring(0, messageinfo.indexOf("http://"));
            contentstr += tempstr;

            urlinfo = GetUrl(messageinfo);
            urlinfo = urlinfo.toString();
            if (urlinfo.indexOf("<br>") != -1) {
                urlinfo = urlinfo.substring(0, urlinfo.indexOf("<br>"));
            }
            tempstr = messageinfo.substring(messageinfo.indexOf(urlinfo) + urlinfo.length, messageinfo.length);

            contentstr += "<a class='extlink' rel='nofollow' title='指向其它网站的链接' href='" + urlinfo + "' target='_blank'>http://f.yupoo.com/...</a><br />";
            contentstr += "<embed width='300' height='215'  menu='false' quality='high'";
            contentstr += "src='" + urlinfo + "'";
            contentstr += "type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' ></embed>";
            contentstr += tempstr;
        }
        else {
            contentstr = messagearr[3];
        }
        var str = String.format(Template_TwitterMessage, messagearr[0], messagearr[1], messagearr[2], contentstr, messagearr[4], messagearr[0], messagearr[1], messagearr[4], messagearr[5], messagearr[6], messagearr[7], messagearr[4], messagearr[4]);
        return str;
    }
}

// youtube内容生成
function youtubeContent(messagearr) {
    if (messagearr.length > 0) {
        var messageinfo = messagearr[3];
        var tempstr = "";
        var contentstr = "";
        var urlinfo = "";
        if (messageinfo.indexOf("http://") != -1) {
            tempstr = messageinfo.substring(0, messageinfo.indexOf("http://"));
            contentstr += tempstr;

            urlinfo = GetUrl(messageinfo);
            urlinfo = urlinfo.toString();
            if (urlinfo.indexOf("<br>") != -1) {
                urlinfo = urlinfo.substring(0, urlinfo.indexOf("<br>"));
            }
            tempstr = messageinfo.substring(messageinfo.indexOf(urlinfo) + urlinfo.length, messageinfo.length);
            
            var urlinfov = "";
            var urlinfoview = "";
            // 替换URL 将http://www.youtube.com/watch?v=mOX3OmUhQoo 格式的替换成http://www.youtube.com/v/mOX3OmUhQoo&hl=zh_CN&fs=1 格式的，通用反过来也需要处理
            if (urlinfo.indexOf("v=") != -1) {
                var tempurlinfo = urlinfo.substring(urlinfo.indexOf("v=") + 2, urlinfo.length);
                urlinfov = "http://www.youtube.com/v/" + tempurlinfo + "&hl=zh_CN&fs=1";
                urlinfoview = urlinfo;
            }

            contentstr += "<a class='extlink' rel='nofollow' title='指向其它网站的链接' href='" + urlinfoview + "' target='_blank'>http://www.youtube.com/...</a><br />";
            contentstr += tempstr;
            contentstr += "<div style='border: 1px solid #999999; background-color: #000000; padding: 4px; width: 300px;margin: 5px 0 0px 0;'>\r\n\t";
            contentstr += "<embed src='" + urlinfov + "' quality='high' width='300' height='251'";
            contentstr += " align='middle' allowscriptaccess='sameDomain' wmode='opaque' type='application/x-shockwave-flash'></embed></div>\r\n\t";
        }
        else {
            contentstr += messagearr[3];
        }

        var str = String.format(Template_TwitterMessage, messagearr[0], messagearr[1], messagearr[2], contentstr, messagearr[4], messagearr[0], messagearr[1], messagearr[4], messagearr[5], messagearr[6], messagearr[7], messagearr[4], messagearr[4]);
        return str;
    }
}

// tudou内容生成
function tudouContent(messagearr) {
    if (messagearr.length > 0) {
        var messageinfo = messagearr[3];
        var tempstr = "";
        var contentstr = "";
        var urlinfo = "";
        if (messageinfo.indexOf("http://") != -1) {
            tempstr = messageinfo.substring(0, messageinfo.indexOf("http://"));
            contentstr += tempstr;

            urlinfo = GetUrl(messageinfo);
            urlinfo = urlinfo.toString();
            if (urlinfo.indexOf("<br>") != -1) {
                urlinfo = urlinfo.substring(0, urlinfo.indexOf("<br>"));
            }
            tempstr = messageinfo.substring(messageinfo.indexOf(urlinfo) + urlinfo.length, messageinfo.length);

            var urlinfov = "";
            var urlinfoview = "";
            // 替换URL 将http://www.tudou.com/v/5txl_rXDUcU 格式的替换成http://www.tudou.com/programs/view/5txl_rXDUcU/ 格式的，通用反过来也需要处理
            if (urlinfo.indexOf("/v/") != -1) {
                var tempurlinfo = urlinfo.substring(urlinfo.indexOf("/v/") + 3, urlinfo.length);
                urlinfoview = "http://www.tudou.com/programs/view/" + tempurlinfo + "/";
                urlinfov = urlinfo;
            } else if (urlinfo.indexOf("/view/") != -1) {
                var tempurlinfo = urlinfo.substring(urlinfo.indexOf("/view/") + 6, urlinfo.length - 1);
                urlinfoview = urlinfo;
                urlinfov = "http://www.tudou.com/v/" + tempurlinfo;
            }

            contentstr += "<a class='extlink' rel='nofollow' title='指向其它网站的链接' href='" + urlinfoview + "' target='_blank'>http://www.tudou.com/...</a><br />";
            contentstr += tempstr;
            
            contentstr += "<div style='border: 1px solid #999999; background-color: #000000; padding: 4px; width: 300px;margin: 5px 0 0px 0;'>\r\n\t";
            contentstr += "<object width='300' height='225'><param name='movie' value='" + urlinfov + "'></param><param name='allowScriptAccess' value='always'></param><param name='wmode' value='transparent'></param><embed src='" + urlinfov + "' type='application/x-shockwave-flash' width='300' height='225' allowFullScreen='true' wmode='transparent' allowScriptAccess='always'></embed></object>";
            contentstr += "</div>\r\n\t";
        } 
        else {
            contentstr += messagearr[3];
        }

        var str = String.format(Template_TwitterMessage, messagearr[0], messagearr[1], messagearr[2], contentstr, messagearr[4], messagearr[0], messagearr[1], messagearr[4], messagearr[5], messagearr[6], messagearr[7], messagearr[4], messagearr[4]);
        return str;
    }
}

// youku内容生成
function youkuContent(messagearr) {
    if (messagearr.length > 0) {
        var messageinfo = messagearr[3];
        var tempstr = "";
        var contentstr = "";
        var urlinfo = "";
        if (messageinfo.indexOf("http://") != -1) {
            tempstr = messageinfo.substring(0, messageinfo.indexOf("http://"));
            contentstr += tempstr;

            urlinfo = GetUrl(messageinfo);
            urlinfo = urlinfo.toString();
            if (urlinfo.indexOf("<br>") != -1) {
                urlinfo = urlinfo.substring(0, urlinfo.indexOf("<br>"));
            }
            
            tempstr = messageinfo.substring(messageinfo.indexOf(urlinfo) + urlinfo.length, messageinfo.length);
            contentstr += "<a class='extlink' rel='nofollow' title='指向其它网站的链接' href='" + urlinfo + "' target='_blank'>http://player.youku.com/...</a><br />";
            contentstr += tempstr;
            contentstr += "<div style='border: 1px solid #999999; background-color: #000000; padding: 4px; width: 300px;margin: 5px 0 0px 0;'>\r\n\t";
            contentstr += "<embed src='" + urlinfo + "' quality='high' width='300' height='248'";
            contentstr += " align='middle' allowscriptaccess='sameDomain' wmode='opaque' type='application/x-shockwave-flash'></embed></div>\r\n\t";
        }
        else {
            contentstr += messagearr[3];
        }

        var str = String.format(Template_TwitterMessage, messagearr[0], messagearr[1], messagearr[2], contentstr, messagearr[4], messagearr[0], messagearr[1], messagearr[4], messagearr[5], messagearr[6], messagearr[7], messagearr[4], messagearr[4]);
        return str;
    }
}

function GetUrl(str) {
    if (str != null) {
        var reg = new RegExp("[a-zA-z]+://[^\S][^'][^\"]*");
        return str.match(reg);
    }
    return "";
}

function CreatePagesLink(pagenum) {
    var context = "";
    for (var i = 1; i <= pagenum; i++) {
        context += "<a href='#' onclick='getInfos(" + i + ");'>" + i + "</a>";
    }
    return context;
}

// 添加收藏和取消收藏
function ChangeFavourites(messid) {
    var favimgobj = document.getElementById("status_star_" + messid);
    // 添加收藏
    if (favimgobj.src.indexOf("/images/icon_star_empty.gif") != -1) {
        favimgobj.src = "/images/icon_star_full.gif";
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "API/FavouritesMessageAction.aspx?messid=" + messid + "&flag=1",
            data: "",
            beforeSend: function() { },
            complete: function() { },
            success: function(result) {
                if (result != null) {
                    GetFavMessage(result);
                }
            }
        });
    }
    else {    // 取消收藏
        favimgobj.src = "/images/icon_star_empty.gif";
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "API/FavouritesMessageAction.aspx?messid=" + messid + "&flag=2",
            data: "",
            beforeSend: function() { },
            complete: function() { },
            success: function(result) {
                if (result != null) {
                    GetFavMessage(result);
                }
            }
        });
    }
}

function CheckFavourites(date) {
    if (date != null) {
        for (var i = 0; i < date.length; i++) {
            var favimgobj = document.getElementById("status_star_" + date[i].messageid);
            if (favimgobj != null) {
                favimgobj.src = "/images/icon_star_full.gif";
            }
        }
    }
}
var datafav;

function GetFavMessage(userid) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "API/TwitterFavouritesMessages.aspx?userid=" + userid,
        data: "",
        beforeSend: function() { },
        complete: function() { },
        success: function(result) {
            if (result != null) {
                datafav = result;
                if (datafav != null) {
                    CheckFavourites(datafav);
                }
            }
        }
    });
}