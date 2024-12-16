<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Dingding._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="https://g.alicdn.com/dingding/dingtalk-pc-api/2.7.0/index.js"></script>
    <script type="text/javascript">
        DingTalkPC.config({
            agentId: '<%=dic["agentId"]%>', // 必填，微应用ID
            corpId: '<%=dic["corpId"]%>',//必填，企业ID
            timeStamp: '<%=dic["timeStamp"]%>', // 必填，生成签名的时间戳
            nonceStr: '<%=dic["nonceStr"]%>', // 必填，生成签名的随机串
            signature: '<%=dic["signature"]%>', // 必填，签名
            jsApiList: ['runtime.info',
                'biz.contact.choose',
                'device.notification.confirm',
                'device.notification.alert',
                'device.notification.prompt',
                'biz.ding.post',
                'runtime.permission.requestAuthCode',
                'device.geolocation.get',
                'biz.ding.post',
                'biz.contact.complexChoose']
        });
        DingTalkPC.ready(function () {
            //获取免登授权码 -- 注销获取免登服务，可以测试jsapi的一些方法
            DingTalkPC.runtime.permission.requestAuthCode({
                corpId: '<%=dic["corpId"]%>',
                onSuccess: function (result) {
                    $.getJSON("getuserinfo.aspx?code=" + result["code"], function (data) {
                        if (data.Code == 0) {
                            $("#img").append("<img src=\"" + data.Data.avatar + "\" height=\"100\" width=\"100\">")
                            $("#isAdmin").text(data.Data.isAdmin);
                            $("#name").text(data.Data.name);
                            $("#mobile").text(data.Data.mobile);
                            $("#workPlace").text(data.Data.workPlace);
                            $("#position").text(data.Data.position);
                            $("#jobnumber").text(data.Data.jobnumber);
                            $("#hiredDate").text(data.Data.hiredDate);
                        }
                    })
                },
                onFail: function (err) {
                }
            });; k
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="img"></div>
            是否为企业的管理员：<span id="isAdmin"></span><br /><br />
            姓名：<span id="name"></span><br /><br />
            手机号：<span id="mobile"></span><br /><br />
            办公地点<span id="workPlace"></span><br /><br />
            职位信息<span id="position"></span><br /><br />
            员工工号<span id="jobnumber"></span><br /><br />
            入职时间<span id="hiredDate"></span><br /><br />
        </div>
    </form>
</body>
</html>