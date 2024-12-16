<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Portal.WebSite.Default1"
    MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
       
 
    <script src="js/jquery.js" type="text/javascript"></script>

    <script src="js/jquery.history_remote.pack.js" type="text/javascript"></script>
    <script src="js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/dialog1.js"></script>

    <script language="javascript" type="text/javascript">

        function AutoLink() {
            window.open("AllJob.aspx", "所有职位", null, null);
        }

        function FormatDateTime(dt) {
            function toFixedLengthString(val, len) {
                var n = Math.pow(10, len);
                var s = new String();
                s += (val + n);
                s = s.substr(s.length - len, len);
                return s;
            }

            var ret = new String();
            ret += dt.getFullYear();
            ret += '-';
            ret += toFixedLengthString(dt.getMonth() + 1, 2);
            ret += '-';
            ret += toFixedLengthString(dt.getDate(), 2);
            ret += ' ';
            ret += toFixedLengthString(dt.getHours(), 2);
            ret += ':';
            ret += toFixedLengthString(dt.getMinutes(), 2);
            ret += ':';
            ret += toFixedLengthString(dt.getSeconds(), 2);
            return ret;
        }
    </script>

    <script language="javascript" type="text/javascript">
        function Nothing() {
            str = "<span class=\"title\">暂无待办事宜</span> <em></em>";
            str += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#ffcfa7\">";
            str += "    <tr class=\"nav1\">";
            str += "    </tr>";
            str += "</table>";
            var s = document.getElementById("taskItemSpan");
            s.innerHTML = str;
            $("#refreshdiv").text("");
        }
        var isloading = false;
        $(document).ready(function() {
            AjaxGetTaskItem();
        });

        function AjaxGetTaskItem() {
            $("#refreshdiv").text("读取");
            if (isloading)
                return;
            isloading = true;
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "API/TaskItemMessages.aspx",
                data: "",
                beforeSend: function() { },
                complete: function() {
                    isloading = false;
                    window.setTimeout(AjaxGetTaskItem, 10000);
                },
                success: function(result) {
                    if (result != null) {
                        if (typeof (result) == "string") {
                            alert(result);
                            return;
                        }
                        var str = "";
                        var num = 0;
                        for (each in result) {
                            var key = each;
                            var value = result[each];
                            if (value != null && value.length > 0) {
                                str += "<strong>00" + (++num) + "</strong> | <span class=\"title\">" + key + "</span> <em></em>";
                                str += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#ffcfa7\">";
                                str += "    <tr class=\"nav1\">";
                                str += "        <td height=\"25\" align=\"center\" bgcolor=\"#FFFFFF\" width='10%'>单据编号</td>";
                                str += "        <td align=\"center\" bgcolor=\"#FFFFFF\" width='15%'>申请人</td>";
                                str += "        <td align=\"center\" bgcolor=\"#FFFFFF\" width='20%'>申请日期</td>";
                                str += "        <td align=\"center\" bgcolor=\"#FFFFFF\" width='15%'>待审批人</td>";
                                str += "        <td align=\"center\" bgcolor=\"#FFFFFF\" width='20%'>描述</td>";
                                str += "        <td align=\"center\" bgcolor=\"#FFFFFF\" width='10%'>操作</td>";
                                str += "        <td align=\"center\" bgcolor=\"#FFFFFF\" width='10%'>审批流程</td>";
                                str += "    </tr>";
                                for (var i = 0; i < value.length; i++) {
                                    str += FormatTaskItem(value[i]);
                                }
                                str += "</table>";
                            }
                        }
                        if (num == 0) {
                            Nothing();
                        }
                        else {
                            var s = document.getElementById("taskItemSpan");
                            s.innerHTML = str;
                            $("#refreshdiv").text("");
                        }
                    }
                    else {
                        Nothing();
                    }
                },
                error: function(result) {
                    Nothing();
                }
            });
        }

        function FormatTaskItem(taskItemInfo) {
            // {0} 单据编号(FormNumber), {1} 申请人(ApplicantName), {2} 申请日期(AppliedTime), {3} 待审批人(ApproverName), {4} 描述(Description)
            // {5} 单据页面的Url(Url), {6} 显示所有审核人的页面的Url(ApproversUrl),{7} 申请人ID(ApplicantID),{8}待审批人ID(ApproverID)
            var Template = "    <tr class=\"nav2\">";
            Template += "        <td height=\"25\" align=\"center\" bgcolor=\"#FFFFFF\">{0}</td>";
            Template += "        <td align=\"center\" bgcolor=\"#FFFFFF\">";
            Template += "            <span onclick=\"ShowMsg({7})\" onmouseover=\"this.style.cursor='pointer',this.style.color='#f97b02'\" ";
            Template += "            onmouseout=\"this.style.cursor='auto',this.style.color='#666666'\" >{1}</span></td>";
            Template += "        <td align=\"center\" bgcolor=\"#FFFFFF\" style='font-family:Verdana'>{2}</td>";
            Template += "        <td align=\"center\" bgcolor=\"#FFFFFF\">";
            Template += "            <span onclick=\"ShowMsg({8})\" onmouseover=\"this.style.cursor='pointer',this.style.color='#f97b02'\" ";
            Template += "            onmouseout=\"this.style.cursor='auto',this.style.color='#666666'\" >{3}</span>";
            Template += "        <td align=\"center\" bgcolor=\"#FFFFFF\" style='font-family:Verdana'>{4}</td>";
            Template += "        <td align=\"center\" bgcolor=\"#FFFFFF\">";
            Template += "            <a href=\"{5}\" target=\"TaskItemOp\">";
            Template += "            <img src=\"images/ico (1).gif\" width=\"14\" height=\"14\" /></a>";
            Template += "        </td>";
            Template += "        <td align=\"center\" bgcolor=\"#FFFFFF\">";
            Template += "            <a href=\"{6}\" target=\"TaskItemApprovalFlow\">";
            Template += "            <img src=\"images/ico (1).gif\" width=\"14\" height=\"14\" /></a>";
            Template += "        </td>";
            Template += "    </tr>";

            
            return String.format(Template, taskItemInfo.FormNumber, taskItemInfo.ApplicantName, FormatDateTime(taskItemInfo.AppliedTime),
                                taskItemInfo.ApproverName, taskItemInfo.Description, taskItemInfo.Url, taskItemInfo.ApproversUrl,
                                taskItemInfo.ApplicantID, taskItemInfo.ApproverID);
            
        }

        function ShowMsg(ApplicantID) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "API/TaskItemMessages.aspx?ApplicantID=" + ApplicantID,
                data: "",
                beforeSend: function() { },
                complete: function() { },
                success: function(result) {
                    if (result != null) {
                        var script = "<table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
                        script += "  <tr>";
                        script += "    <td width=\"4\"><img src=\"images/t_03.gif\" width=\"4\" height=\"30\" /></td>";
                        script += "    <td background=\"images/t_04.gif\" class=\"swindow_title\">人员基本信息</td>";
                        script += "    <td width=\"30\" background=\"images/t_04.gif\"><a href=\"javascript:$.unblockUI();\"><img src=\"images/t_09.gif\" width=\"22\" height=\"21\" /></a></td>";
                        script += "    <td width=\"4\" align=\"right\"><img src=\"images/t_06.gif\" width=\"4\" height=\"30\" /></td>";
                        script += "  </tr>";
                        script += "</table>";
                        script += "<table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" background=\"images/t_12.gif\" style=\"background-repeat:no-repeat; \">";
                        script += "  <tr>";
                        script += "    <td height=\"300\" valign=\"top\" class=\"swindow_matter\">";
                        result = script + result;
                        script = "   </td>";
                        script += "  </tr>";
                        script += "</table>";
                        result = result + script;

                        jQuery.blockUI({
                            message: result, hideHeader: true, centerX: true,
                            css: {
                                padding: '10px',
                                margin: 0,
                                width: '60%',
                                top: '20%',
                                left: '20%',
                                textAlign: 'center',
                                color: '#000',
                                border: 'none',
                                backgroundColor: 'transparent',
                                overflow: "auto"
                            }
                        });
                    }
                }
            });
        }

        function ShowAssetMsg(userId) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "API/TaskItemMessages.aspx?assetuserid=" + userId,
                data: "",
                beforeSend: function () { },
                complete: function () { },
                success: function (result) {
                    if (result != null) {
                        var script = "<table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
                        script += "  <tr>";
                        script += "    <td width=\"4\"><img src=\"images/t_03.gif\" width=\"4\" height=\"30\" /></td>";
                        script += "    <td background=\"images/t_04.gif\" class=\"swindow_title\">固定资产领用</td>";
                        script += "    <td width=\"30\" background=\"images/t_04.gif\"><a href=\"javascript:$.unblockUI();\"><img src=\"images/t_09.gif\" width=\"22\" height=\"21\" /></a></td>";
                        script += "    <td width=\"4\" align=\"right\"><img src=\"images/t_06.gif\" width=\"4\" height=\"30\" /></td>";
                        script += "  </tr>";
                        script += "</table>";
                        script += "<table width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" background=\"images/t_12.gif\" style=\"background-repeat:no-repeat; \">";
                        script += "  <tr>";
                        script += "    <td align=\"left\">";
                        result = script + result;
                        script = "   </td>";
                        script += "  </tr>";
                        script += "</table>";
                        result = result + script;

                        jQuery.blockUI({
                            message: result, hideHeader: true, centerX: true,
                            css: {
                                padding: '10px',
                                margin: 0,
                                width: '60%',
                                top: '20%',
                                left: '20%',
                                textAlign: 'center',
                                color: '#000',
                                border: 'none',
                                backgroundColor: 'transparent',
                                overflow: "auto"
                            }
                        });
                    }
                }
            });
        }

        function AboutTaskItems() {
            jQuery.blockUI({ message: $("#aboutTaskItems"), hideHeader: true, centerX: true,
                css: {
                    padding: '10px',
                    margin: 0,
                    width: '60%',
                    top: '20%',
                    left: '20%',
                    textAlign: 'center',
                    color: '#000',
                    border: 'none',
                    backgroundColor: 'transparent',
                    overflow: "auto"
                }
            });
        }
        function AboutTaskItems2() {
            jQuery.blockUI({ message: $("#aboutTaskItems2"), hideHeader: true, centerX: true,
                css: {
                    padding: '10px',
                    margin: 0,
                    width: '60%',
                    top: '20%',
                    left: '20%',
                    textAlign: 'center',
                    color: '#000',
                    border: 'none',
                    backgroundColor: 'transparent',
                    overflow: "auto"
                }
            });
        }
    </script>


    <div id="aboutTaskItems" style="display: none">
        <table width="600" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="4">
                    <input type="hidden" runat="server" id="hidVote" />
                    <input type="hidden" runat="server" id="hidUrl" />
                    <img src="images/t_03.gif" width="4" height="30" />
                </td>
                <td background="images/t_04.gif" class="swindow_title">
                    关于待办事宜
                </td>
                <td width="30" background="images/t_04.gif">
                    <a href="javascript:$.unblockUI();">
                        <img src="images/t_09.gif" width="22" height="21" /></a>
                </td>
                <td width="4" align="right">
                    <img src="images/t_06.gif" width="4" height="30" />
                </td>
            </tr>
        </table>
        <table width="600" border="0" cellpadding="0" cellspacing="0" background="images/t_12.gif"
            style="background-repeat: no-repeat;">
            <tr>
                <td height="300" align="left" valign="top" class="swindow_matter">
                    <p>
                        调整后的代办事宜主要减少了系统资源占用情况，并优化了代理功能。目前尚在测试阶段，请大家协助测试，谢谢！
                    </p>
                    <p>
                        待办事宜功能帮助您快速定位要处理的事务，通过该功能你可以一目了然的查看所有未处理的工作项，并可以直接打开以便快速处理。 在待办事宜的工作项列表中:<br />
                        单击任何一条记录的操作图标可以在新的浏览器窗口中打开该工作项并进行处理。<br />
                        单击任何一条记录的审批流程图标可以查看该工作项的进度信息。<br />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <div id="aboutTaskItems2" style="display: none">
        <table width="600" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td width="4">
                    <img src="images/t_03.gif" width="4" height="30" />
                </td>
                <td background="images/t_04.gif" class="swindow_title">
                    关于新考勤系统
                </td>
                <td width="30" background="images/t_04.gif">
                    <a href="javascript:$.unblockUI();">
                        <img src="images/t_09.gif" width="22" height="21" /></a>
                </td>
                <td width="4" align="right">
                    <img src="images/t_06.gif" width="4" height="30" />
                </td>
            </tr>
        </table>
        <table width="600" border="0" cellpadding="0" cellspacing="0" background="images/t_12.gif"
            style="background-repeat: no-repeat;">
            <tr>
                <td height="300" align="left" valign="top" class="swindow_matter">
                    <p>
                        新的考勤系统以一个日历的形式清晰直观的展现每日的考勤信息，目前尚在测试阶段，请大家协助测试，谢谢！
                    </p>
                    <p>
                        <a href="helpfile/星言云汇考勤系统使用手册.doc">《考勤系统使用手册》</a>
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 10px 0 10px 0;">
        <tr>
            <td valign="top" class="nav">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="1" height="59" background="/images/n1_03.jpg" style="background-repeat: repeat-x;
                                        background-position: bottom left;">
                                        <img src="/images/m1_03.jpg" width="63" height="59" hspace="20" />
                                    </td>
                                    <td width="1" background="/images/n1_03.jpg" style="background-repeat: repeat-x;
                                        background-position: bottom left;">
                                        <img src="/images/m1_05.jpg" width="2" height="59" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td background="/images/n1_03.jpg" style="background-repeat: repeat-x; background-position: bottom left;
                            font-size: 14px; color: #000000;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <strong><span style="color: Red; font-size: 14px">
                                            <asp:Label runat="server" ID="lblNotify"></asp:Label></span></strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="5px">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/menu_bg.jpg">
                    <tr>
                        <td width="5">
                            <img src="images/a1_113.gif" width="5" height="32" />
                        </td>
                        <td class="top">
                            看看今天有什么事情要处理<div id="refreshdiv" />
                        </td>
                        <td width="75" valign="bottom">
                            <a>
                                <img src="images/renlibtn_05.jpg" width="75" height="27" border="0" /></a>
                        </td>
                        <td width="3">
                        </td>
                        <td width="75" valign="bottom">
                            <a href="HumanMap.aspx">
                                <img src="images/renlibtn_09.jpg" width="75" height="27" border="0" /></a>
                        </td>
                        <td width="3">
                        </td>
                        <td width="75" valign="bottom">
                            <a href="AttendanceSelect.aspx">
                                <img src="images/attendance2.jpg" width="75" height="27" border="0" /></a>
                        </td>
                         <td width="6">
                        </td>
                        <td width="5">
                            <img src="images/a1_19.gif" width="5" height="32" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="navnav">
                            <span id="taskItemSpan"></span>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="300" valign="top" class="navright">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="about">
                    <tr>
                        <td height="190" valign="top">
                            <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="title" colspan="2">
                                        <img src="images/a1_30.gif" width="17" height="15" />
                                        我的信息
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1" bgcolor="#565656" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 10px 0;" colspan="2">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="94">
                                                    <table width="90" border="0" cellpadding="1" cellspacing="1" bgcolor="#999999">
                                                        <tr>
                                                            <td bgcolor="#FFFFFF">
                                                                <img id="imgUserIcon" runat="server" width="90" height="90" hspace="0" vspace="0" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="174" style="padding-left: 10px;">
                                                    <asp:Label ID="labUserInfo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">
                                        <img src="images/a1_asset.gif" width="17" height="15" />
                                        资产领用
                                    </td>
                                     <td class="title">
                                          <a href='http://xf.shunyagroup.com/Return/SalaryView.aspx' target="_blank" style="text-decoration:none; color:black;"><img src="/images/salary.gif"/>
                                        薪资查询 </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1" bgcolor="#565656" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 10px 0;" colspan="2">
                                        <asp:Label ID="lblAsset" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="notice" style="margin: 5px 0 5px 0;">
                    <tr>
                        <td valign="top">
                            <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="title">
                                        <img src="images/a1_48.gif" width="19" height="16" />
                                        公告
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1">
                                        <img src="images/a1_51.gif" width="269" height="2" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="财务部重要提醒2024年12月25日前需协助完成事宜.docx">财务部重要提醒 - 2024年12月25日前需协助完成事宜</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                  <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="费用申请报销与借款管理条例.docx">费用申请、报销与借款管理条例</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="费用申请报销细则.docx">费用申请报销细则</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 10px;">
                                    &nbsp;<br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                  
                </table>
                
 <%--               <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/bele.jpg"
                    class="portal-box" style="font-size: 12px; color: #FFF;">
                    <tr>
                        <td height="40" style="font-size: 14px; font-weight: bold; padding-left: 40px;">
                            热点招聘信息
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 20px;">
                            <marquee id="tdAlle" runat="server" behavior="scroll" direction="up" height="90"
                                scrollamount="2" scrolldelay="100" width="100%" onmouseout="this.start()" onmouseover="this.stop()"></marquee>
                        </td>
                    </tr>
                    <tr>
                        <td height="40" valign="top" style="line-height: 24px; padding-left: 20px;">
                            <input type="button" name="button" id="Submit1dd" value="所有职位..." onclick="AutoLink()"
                                style="background-image: url(../images/btnbg.jpg); height: 28px; width: 102px;
                                color: #ed6801; font-weight: bold; border: none;">
                        </td>
                    </tr>
                </table>--%>
            </td>
        </tr>
    </table>

</asp:Content>

