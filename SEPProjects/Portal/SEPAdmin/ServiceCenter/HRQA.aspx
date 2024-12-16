<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRQA.aspx.cs" Inherits="SEPAdmin.ServiceCenter.HRQA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="js/artDialog.js?skin=default"></script>

    <script type="text/javascript" src="js/iframeTools.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="tabContent" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr id="tr1" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-1.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我因公出差需要预定机票、火车票，该如何办理？<br />
                    <br />
                </strong>*</span> 机票申请请依照以下流程申请办理：<br />
                <br />
                1、<strong>机票预定：</strong>由需求员工线上填写并提交申请单，经业务团队审批通过后，由前台联系供应商双方确认价格后， 进行预定。预订成功后供应商会发确认短信给预定的员工，收到短信代表机票预订成功。<br />
                2、<strong>机票预订流程</strong><br />
                <img src="images/hrqa-1.jpg" width="60%" height="60%" />
                <br />
                <span class="font-color-orag">*</span> 预订火车票：公司暂无火车票的合作供应商。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr2" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-2.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我因公出差需要预定酒店，该如何办理？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span> 目前尚无固定合作酒店。员工自行预订，住宿标准请以财务规定为准。<br />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr3" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px; width: 90%;">
                <img src="images/no-3.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>如何正确填写快递单？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span> 请参见前台快递单样本 (Sample) 填写并依照以下流程申请办理：<br />
                <br />
                <img src="images/hrqa-3.jpg" />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr4" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px; width: 90%;">
                <img src="images/no-4.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我要印名片,应该向谁申请？<br />
                    <br />
                </strong>*</span> 请依照以下流程申请办理：<br />
                <br />
                <img src="images/hrqa-4.jpg" />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr5" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px; width: 90%;">
                <img src="images/no-5.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>公司的办公用品怎么申请？<br />
                    <br />
                </strong>*</span> 请依照以下流程申请办理：<br />
                <br />
                <img src="images/hrqa-5.jpg" />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr6" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px; width: 90%;">
                <img src="images/no-6.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我想阅读公司出版的书籍《宣讲》或送给客户
                    ，怎么申请？<br />
                    <br />
                </strong>*</span> 请依照以下流程申请办理：<br />
                <br />
                <img src="images/hrqa-6.jpg" />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr7" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px; width: 90%;">
                <img src="images/no-7.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>需要送给客户礼品，怎么申请？
                </strong></span>
                <br />
                <br />
                <span class="font-color-orag">*</span> 请依照以下流程申请办理：<br />
                <br />
                <img src="images/hrqa-6.jpg" /><br />
                <br />
                <span class="font-color-orag">*</span> 备注：目前礼品有：MP3、U 盘、名片夹、移动车载电视
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr8" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-8.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>填写各种申请单时，项目号如何使用？<br />
                    <br />
                </strong>*</span> 任何人申请OT/出差/申领办公用品及礼品/快递时，必须选择相对应的项目号，无项目号无法申请。 非业务团队应提前与业务人员沟通，及时索取项目号（如特殊情况请联系行政部）
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr9" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-9.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我如果需要用会议室开会，怎么来预订房间？<br />
                    <br />
                </strong>*</span> 可与前台联系预订，建议重要会议或对设备要求较高的会议，请提前1 天预订会议室。
                <br />
                （目前公司会议室均可通用。）
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr10" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-10.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>开会要用投影仪，录音笔，翻页器怎么申请使用？<br />
                    <br />
                </strong>*</span>请至少提前1 天与前台联系预订并到前台登记领用。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr11" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-11.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我要用会议室的多方通话功能，怎么来操作？<br />
                    <br />
                </strong>*</span>请参照会议室桌面上相关的《操作说明书》。如特殊情况需要，可直接联系IT 同事， 请其协助操作。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr12" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-12.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我们团队需要一辆公务车去接送客户及看活动场地，如何申请？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>公司的公务车统一由总裁助理管理，业务组有用车需求可发邮件或电话联系预订。
                <br />
                <span class="font-color-orag">*</span>公务车使用本着以客户优先使用及按照预订的先后顺序的规范预订为原则。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr13" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-13.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>公司是否可以借阅书籍？如何借阅？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>公司为丰富员工的知识领域及业余生活，特设立图书馆供员工借阅图书使用。
                <br />
                <span class="font-color-orag">*</span>借阅与归还的时间为每周四、周五，每人最多可借阅2本，借阅期限为20 天；
                <br />
                <span class="font-color-orag">*</span>借阅与归还请务必到前台登记签字，如有紧急借阅情况，如前台无人办理借阅时需在图书馆紧急登记表
                上登记，才可将书籍拿走，切勿未登记就将书籍带走。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr14" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-14.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>集团对于员工的工作服饰是否有规定？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>是的，周一至周四，员工应穿着大方庄重的职业正装，周五可穿商业休闲装。
                <br />
                <span class="font-color-orag">*</span>除创意、制作部门外，员工原则上不允许穿着太过随意的牛仔衫、超短裤、无袖裙，或其他张扬怪异的服饰。
                <br />
                <span class="font-color-orag">*</span>研究表明，穿着得体的人士能够获得他人更多的尊重，及对其能力的认可。因此，星言云汇员工应在会见客户和工作时间穿着职业正装（单色及深色为合适）。
                <br />
                <img src="images/dress.jpg" /><br />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr15" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-15.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我在哪里可以阅读公司的规章制度，并获取到需要的日常职能工作表格？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>入职当天，每位新员工均会领到公司的《员工手册》，请详细阅读各部分内容。
                <br />
                <span class="font-color-orag">*</span>同时，在公司服务器GM 中的“员工入职”中及系统员工服务中心可以阅读电子版《员工手册》；
                <br />
                <span class="font-color-orag">*</span>公司各类常规表格可在公司服务器的GM中“运营表格”文件下载打印，如有疑问，请随时与行政部联系咨询。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr16" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-1.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>线上如何鉴定上午和下午的上班时间？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>公司上班时间：上午 9：30-12：00；下午 13：30-18：30
                <br />
                <span class="font-color-orag">*</span>如因客户原因需要调整上下班时间，向总监提出申请，批准后请告知本部门人事行政负责人，
                IT 部调 整系统上下班时间方可生效。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr17" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-2.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>平时OT是否需要进行申请？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>是的。OT必须线上申请，在申请时须详细填写TimeSheet，总监批复后方可有效。（实际刷卡记录做参考）
                <br />
                <span class="font-color-orag">*</span>说明：前一天OT至 21：00 以后，第二天可以10:30 之前到岗；前一天OT至
                00：00 以后，第二 天可以13:30 之前到岗；周五OT，周一正常上班；周日OT至 21：00（21：20）以后，周一可10:30之前到岗；OT申请审批完成后，系统将自动默认以上情况。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr18" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-3.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>每天的考勤记录时间的截点？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>员工每日刷卡自动计考勤（首次和末次），每天考勤时间截点为早上6：00。如有OT超过此时间的同事，
                需在6：00之前刷卡。如6：00之后还需工作，请在 6：00 之后再刷卡，表示新一天的上班时间。 未刷卡无记录将按旷工处理。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr19" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-4.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>考勤管理中的“提交审批”该在什么时候提交？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>”提交审批”是提交个人全月考勤时进行的操作，每月提交一次，平时误操作，请联系人事行政部。
                考勤提交日期为下月5日之前，请及时进行日常考勤操作，以避免考勤混乱耽误全月提交审批。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr20" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-5.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>周末OT是否可以调休，调休时间是否可以延长？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>周末OT满4小时可调休半天，满8小时可调休1天。周末调休严格按照集团规定时间执行，自OT
                之日起 1个月内需调休完毕，如错过调休时间，系统将自动清零调休天数，无法延期补休。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr21" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-6.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>做了请假申请，为什么还显示旷工或迟到？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>任何考勤单项事由申请，都有时间段的选择，你的请假时间必须和进出公司的时间准确对应，如时间
                存在偏差，将会显示旷工或迟到、早退。 无论任何情况，只要是长时间离开公司或返回公司，都需打卡。 申请任何单项事由，打卡时间必须和进出公司的刷卡时间准确对应 。<br />
                <span class="font-color-orag">*</span>例：上午外出开会，11：32 刷卡进公司，你的外出申请的时间必须是 9：30-11：32
                。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr22" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-7.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>忘刷卡怎么办？忘带卡怎么办？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>前一天下班刷卡记录和当天的上班刷卡记录，系统将最晚在当天下午显示出来，如确定为忘记打卡或
                未带卡，必须于当天或知晓的第一时间到前台登记，行政部会依据各个入口的监控录像查询并记录。<br />
                <span class="font-color-orag">*</span>提醒：刷卡考勤是公司工作纪律，必须严格执行。行政部将负责监察未带卡、忘记打卡事实。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr23" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-8.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>月底提交考勤需注意些什么？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>月底提交考勤时，如显示有旷工、早退等与实际考勤不符的异常情况，请检查其原因。<br />
                <span class="font-color-orag">*</span>大家提交的考勤为本人确认且认可的考勤，每位同事对自己提交的考勤负责。考勤数据与相对应的
                财务报销挂钩。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr24" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-9.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>请假申请的线上提交有时间限制吗？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>所有考勤申请必须在发生起7个日历日操作完成，逾时再申请，系统不执行操作，考勤按旷工记录。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr25" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-10.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我的出差、休假、病假、以及因上一日OT而晚到的考勤说明，需要让谁审批同意？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>业务团队的负责人审批即可，由行政部统计监管；病假 2 天（含）以上须出示有效医院证明。（行政部每月根据门卡系统个人记录制作独立考勤表，并做抽查核对）
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr26" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-11.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>日常考勤和年假等记录在什么地方可以查到？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>登陆线上系统，各员工自己的考勤记录表上即可查询。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr31" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-1.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>公司内部是有统一的培训计划吗？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>有的，公司设置了完善的培训体系，包括入职培训、业务培训、专业培训等，培训前均有邮件通知，
                请随时关注集团邮件通知。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr32" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-2.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>个人在星言云汇的职业发展怎样规划？公司将提供什么样的帮助？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>职业规划需公司及个人共同努力实现，个人态度、技能、行动很重要。<br />
                <span class="font-color-orag">*</span>星言云汇规划并提供了各类培训、ON JOB 辅导，以及各种形式的沟通及工作表现评估，包括试用中期及转正评估、年中优异员工特别评估、年终个人
                《Year End Review》、特殊评估等。详细信息请仔细阅读《员工手册》中关于员工的晋升发展等具体政策。<br />
                <span class="font-color-orag">*</span>如有疑问，可以随时与本部门业务负责人、本BU 人事主管、或集团人力资源部同事进行深入沟通和了解。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr33" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-3.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>我加盟公司后，档案放在哪里？当月是否即可享受社会保险和公积金？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>员工转正后，档案可调入代理公司（中智）) 管理（仅限北京市户口）。凡正式入职者，当月即可依照
                政府规定享受社保和住房公积金<br />
                <span class="font-color-orag">*</span>员工入职后即可享受：养老、医疗、失业、工伤、生育、住房公积金；<br />
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr34" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-4.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>公司缴纳社保的基数按全额工资还是基本工资？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span> 在职员工以上一年度月平均工资为基数缴纳社保，新入职员工以offer 上确认的工资数额为基数缴纳社保。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr35" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-5.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>请问社保缴纳的基数上限是多少？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span> 公司严格按照国家有关法规执行，员工社保基数的上限为本地社平工资（以国家公布的数据为准）的三倍。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr36" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-6.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>员工如何报销医疗费（北京）？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>1) 每季度末，代理公司中智会派专人到公司收取医疗单据，请注意集团人事通知。<br />
                <span class="font-color-orag">*</span>2) 日常报销单据可随时递交集团人事负责人，集团人事负责人统一定期快递给中智负责人。<br />
                <span class="font-color-orag">*</span>3) 员工也可随时自行将报销单据快递给代理公司中智。<br />
                <span class="font-color-orag">*</span> 地址：汉威大厦西区27 层。联系方式：王军（电话：65613920-8243）
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr37" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-7.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>如何查询公积金？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>1) 如有公积金联名卡，请登录北京市住房公积金管理中心网站（http://www.bjgjj.gov.cn/），凭本人
                身份证号查询。初始密码为个人身份证后六位。<br />
                <span class="font-color-orag">*</span> 2) 没有公积金联名卡，请咨询人事部负责人
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr38" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-8.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>如何支取公积金？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span> 详细操作方法及须知请详见server。支取时间为每月月底，请随时关注集团邮件通知。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr39" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-9.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>如何办理社保卡？何时发放？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>1) 已参加过工作，社保卡由原单位发放。<br />
                <span class="font-color-orag">*</span> 2) 首次参加工作，可在本单位申请，发放时间等社保中心告知，请注意集团人事部通知。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr40" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-10.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>社保卡丢失怎么办？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>社保卡丢失，需参保人自行挂失补办。<br />
                <span class="font-color-orag">*</span> 挂失咨询热线：96102
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr41" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-11.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>本人社保卡未办理成功，需要做二次采集如何办理？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>首先需个人填写《补打二采表申请表》<br />
                <span class="font-color-orag">*</span> 填写完毕后交与集团人事负责人，发放时间等社保中心告知，请注意集团人事部通知。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr42" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-12.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>年假年底没休完怎么办？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span> 个人年假应在有效期内自行妥善安排休完，未休完部分将视为自动放弃。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr43" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-13.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>试用期期间，是否可以申请提前转正？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span> 对于表现特别优异、才华出众的试用者，若整体表现的评估总分可达4.5 分（满分5 分）以上的，可以向
                本部门负责人申请，公司将视表现考虑提前转正评估。若您有提前转正的愿望，请提前与各BU HR 沟通，了解转正评估的相关内容
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr44" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-14.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>薪资涨幅的比率是多少？根据什么标准来涨幅？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>星言云汇具健全的职级/ 薪资框架，但薪资涨幅公司并无硬性规定，能否加薪及加多少，唯一的衡量标准是根
                据个人能力、工作表现、对团队贡献等可达至哪个级别而做的判断。<br />
                <span class="font-color-orag">*</span> 通常升职加薪，公司必须根据员工整体工作表现进行全面考核后而做出决定。
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
        <tr id="tr45" runat="server" style="display: none;">
            <td style="width: 10%;">
                &nbsp;
            </td>
            <td style="padding: 25px 0 0 0; line-height: 24px;">
                <img src="images/no-15.jpg" width="15" height="13" hspace="5" /><span class="font-color-orag"><strong>女员工如何申领生育津贴？</strong></span><br />
                <br />
                <span class="font-color-orag">*</span>女员工社保生育保险赔付办理流程须知:<br />
                女员工产假期间，由公司按月照常先行支付女员工１００％产假工资。<br />
                女员工产假结束返岗当月，应及时协助公司办理社保生育保险津贴的赔付工作――――<br />
                提醒：如果因本人原因未能及时递交下列相关规定的文件，影响了生育津贴的返回公司，本人当月工资有可能被缓发，直至所有个人文件收齐递交集团人事部<br />
                具体流程:<br />
                <img src="images/flow15.jpg" /><br />
                申请津贴所需材料请见链接：<br />
                <a href="http://www.ciicbj.com/store/detail/template2007/serviceIntrodetail.asp?articleId=6624&Columnid=1253&view=&column2id">
                    http://www.ciicbj.com/store/detail/template2007/serviceIntrodetail.asp?articleId=6624＆Columnid=1253＆view=＆column2id</a>
            </td>
            <td style="width: 10%;">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
