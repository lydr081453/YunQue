<%@ Page Language="c#" Inherits="FrameSite.Include.Page.DefaultWorkSpace" CodeBehind="DefaultWorkSpace.aspx.cs"
    AutoEventWireup="True" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>系统首页</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../css/syshomepage.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 0px auto;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #4a4a49;
        }
        .font-color-orag
        {
            color: #fe9000;
            font-size: 12px;
        }
    </style>
</head>

<script type="text/javascript">
    function Part(index) {
        var tr1 = document.getElementById("tr1");
        var tr2 = document.getElementById("tr2")
        var tr3 = document.getElementById("tr3");
        var tr4 = document.getElementById("tr4");
        var tr5 = document.getElementById("tr5");

        tr1.style.display = "none";
        tr2.style.display = "none";
        tr3.style.display = "none";
        tr4.style.display = "none";

        if (index == 1) {
            tr1.style.display = "block";
            tr5.style.display = "none";
        }
        else if (index == 2) {
            tr2.style.display = "block";
            tr5.style.display = "block";
        }
        else if (index == 3) {
            tr3.style.display = "block";
            tr5.style.display = "block";
        }
        else if (index == 4) {
            tr4.style.display = "block";
            tr5.style.display = "block";
        }
    }
</script>

<body style="margin: 15px 10px 0 10px;">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr id="tr1">
            <td>
                <table border="0" cellspacing="0" cellpadding="0" background="/images/bookbg.jpg"
                    onclick="Part(2);" style="background-repeat: no-repeat; cursor: pointer; width: 103px;
                    height: 126px; margin: 0 20px 10px 20px; float: left;">
                    <tr>
                        <td align="center" class="font-color-orag">
                            （一）<br />
                            考勤管理原则
                        </td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0" background="/images/bookbg.jpg"
                    onclick="Part(3);" style="background-repeat: no-repeat; cursor: pointer; width: 103px;
                    height: 126px; margin: 0 20px 10px 20px; float: left;">
                    <tr>
                        <td align="center" class="font-color-orag">
                            （二）<br />
                            个人考勤填写说明
                        </td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0" background="/images/bookbg.jpg"
                    onclick="Part(4);" style="background-repeat: no-repeat; cursor: pointer; width: 103px;
                    height: 126px; margin: 0 20px 10px 20px; float: left;">
                    <tr>
                        <td align="center" class="font-color-orag">
                            （三）<br />
                            各级考勤审批说明
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr2" style="display: none;">
            <td style="color: #787878; font-size: 12px;">
                1、 严格的考勤制度是企业管理的组成部分，它建立在员工自觉自律和良好的职业道德基础上。<br />
                <br />
                2、 公司全体员工均应按照《员工手册》要求，于上、下班规定时间打卡，并在系统上提交考勤http://xy.shunyagroup.com/<br />
                <br />
                3、 员工不得委托或伪造出勤记录，违规者依据《员工手册》有关规定处理。
                <br />
                <br />
                4、 公司对员工日常出勤要求及工作时间如有调整，由公司统一公告实施。
                <br />
                <br />
                5、 总监级别以下员工应及时提交各自的考勤，将有病假、事假、年假、OT及调休情况的申请和说明，在线上系统提交至团队领导进行审批。<br />
                <br />
                6、 总监级别（含AAD）以上的员工每天上、下班也必须正常打卡。病、事假、年假等需通过系统按时提交申请，当月全月考勤于月底通过线上提交，由团队总经理审批。<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总经理级别的考勤由集团总裁审批。此级别人员不享受OT调休制度。<br />
                <br />
                7、 日常工作时间：
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1) 公司实行每天8小时工作制。每周一至周五上午 09:30—12:30，下午 13:30—18:30。（法定节假日除外）<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2) 由于忘记带了卡、漏打卡、门禁卡故障或暂无门禁卡而无考勤记录者，须及时在前台填写《星言云汇员工考勤异常情况记录表》追认其当天的考勤记录，说明原因。<br />
                <br />
            </td>
        </tr>
        <tr id="tr3" style="display: none;">
            <td style="color: #787878; font-size: 12px;">
                1、查看日常考勤信息、添加事由路径：<br />
                行政系统-->考勤管理； （或首页考勤管理）<br />
                <br />
                2、查看所有的事由信息、删除事由、撤销事由路径：<br />
                行政系统-->查询中心-->事由查询；
                <br />
                <br />
                3、关于请假的填写说明：<br />
                根据实际情况详细地在系统填写线上申请，经团队领导审批后方可休假。 如遇特殊情况无法及时提交线上申请者，需提前以电话、短信或电子邮件方式通知团队领导，正常上班后及时上线补登考勤记录，否则视为旷工处理。<br />
                <br />
                具体假期类别如下：<br />
                病假：以小时为单位，病假天数连续满2天须提交病假条，全年享受累计15个 工作日的50%带薪病假<br />
                事假：以小时为单位，年假未休完者不得休事假，每月最多休2天，全年不得超过10天<br />
                年假：以半日为单位，按《员工手册》规定说明计算，逾期作废。<br />
                婚假：员工转正后，可享受3个日历日的婚假，如为晚婚可享受10个日历日婚假，休假前须提交结婚证复印件，并于结婚证签署1年内休完。<br />
                产假：女职工生育享受98天产假，其中产前可以休假15天；难产的，增加产假15天；生育多胞胎的，每多生育1个婴儿，增加产假15天. 须提交医院出具的诊断证明书及婴儿出生证明。<br />
                丧假：父母，配偶，子女3天；（外）祖父母，兄弟姐妹1天。须提交亲属死亡证明<br />
                产前检查：产前检查，以半天为单位。<br />
                奖励假：奖励假只能1次性休完，有效期为2年，如在离职时未休，视为自动作废。当提交离职申请时，系统将自动清零。<br />
                <br />
                <br />
                4、关于OT的填写说明：<br />
                需在线上系统及时填写OT申请。要准确填写OT项目号、工作描述和OT时长，经团队领导审批后生效，否则不做OT处理。周末/节假日OT可享受调休，其他情况不予调休。<br />
                <br />
                5、关于外出的填写说明：<br />
                需在线上系统及时填写外出申请单，要准确填写外出原因、外出时间。经团队领导审批后生效，否则按旷工处理。<br />
                <br />
                6、关于出差的填写说明：<br />
                需在线上系统及时填写出差申请单，要准确填写OT项目号、内容描述和出差时长。周末出差除了要在系统内填写出差申请之外，还要根据实际情况填写是否有OT及OT的工作内容，这样才可以核算OT以及需调休的时间。<br />
                <br />
                7、关于调休的填写说明：<br />
                需在线上系统及时填写调休申请，要准确填写调休事由，调休时间。调休时间自OT之日起1个月内需调休完毕，如错过调休时间将不做延期处理。年底时，人事部会根据内网系统数据统计个人未能调休的时间，交给各团队领导作为激励的条款参考。<br />
                <br />
            </td>
        </tr>
        <tr id="tr4" style="display: none;">
            <td style="color: #787878; font-size: 12px;">
                1、日常考勤信息审批路径：<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;行政系统-->审批中心<br />
                <br />
                2、单项审批<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工OT、调休、外出、出差的单项申请单，直接由团队领导审批。<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工请假申请单：<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*婚假、产假、丧假、产前检查假，需要HR预审，之后由团队领导审批。<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*病假、事假、年假、福利假，直接由团队领导审批，不需要HR预审。<br />
                <br />
                3、月度审批<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员工当月所有单项审批通过后，在月底提交月度考勤，HR直接审批。<br />
                <br />
            </td>
        </tr>
        <tr id="tr5" style="display: none;">
            <td style="color: #787878; font-size: 12px;" align="center">
                <a onclick="Part(1);" style="cursor: pointer; text-decoration: underline;">返回 </a>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</body>
</html>
