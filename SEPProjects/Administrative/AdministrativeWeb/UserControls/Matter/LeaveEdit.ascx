<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveEdit.ascx.cs" Inherits="AdministrativeWeb.UserControls.Matter.LeaveEdit" %>


     <link rel="stylesheet" href="/public/css/jquery-ui-new.css">
     <script src="/public/js/jquery1.12.js"></script>
     <script src="/public/js/jquery-ui-new.js"></script>
    <script src="/public/js/jquery.ui.datepicker.cn.js"></script>
    <script language="javascript" type="text/javascript" src="/public/js/dialog.js"></script>



<asp:HiddenField ID="hidLeaveID" runat="server" />
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
    <tr>
        <td>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="17">
                        <strong>
                            <img src="../../images/090407_12-35.jpg" width="17" height="20" hspace="5" vspace="5" /></strong>
                    </td>
                    <td align="left">
                        <strong>休假申请单 </strong>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                <tr>
                    <td class="td">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td>姓名：
                                    <asp:HiddenField ID="hidLeaveUserid" runat="server" />
                                    <asp:TextBox ID="txtLeaveUserName" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    员工编号：
                                    <asp:TextBox ID="txtLeaveUserCode" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    业务团队：
                                    <asp:TextBox ID="txtLeaveTeam" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    组别：
                                    <asp:TextBox ID="txtLeaveGroup" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="20" style="background-image: url(../../images/headerbg_left.jpg); background-repeat: no-repeat; background-position: center left;"></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="10" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkSick" value=""
                                        runat="server" />
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <strong>病假</strong><br />
                                    以小时为单位,病假天数连续满2天须提交病假条，全年享受累计15个工作日的50%带薪病假
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkThing" value=""
                                        runat="server" />
                                </td>
                                <td>
                                    <strong>事假</strong><br />
                                    以小时为单位，年假未休完者不得休事假，每月最多休2天，全年不得超过10天
                                </td>
                            </tr>
                            <tr>
                                <td width="20" bgcolor="#F2F2F2">
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkAnnual" value=""
                                        runat="server" />
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <strong>年假</strong><br />
                                    以小时为单位，按《员工手册》规定说明计算，逾期作废
                                </td>
                            </tr>
                            <tr>
                                <td width="20" bgcolor="#F2F2F2">
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkAnnualLast" value=""
                                        runat="server" />
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <strong>补休去年年假</strong><br />
                                    去年年假延长有效期内，以小时为单位，按《员工手册》规定说明计算，逾期作废
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="radio" name="chkLeave" id="chkMarriage" onclick="onClickValue();" value=""
                                        runat="server" />
                                </td>
                                <td>
                                    <strong>婚假</strong><br />
                                    员工转正后，可享受婚假，休假前须提交结婚证复印件，并于结婚证签署1年内休完
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkMaternity" value=""
                                        runat="server" />
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <strong>产假</strong><br />
                                    女职工生育享受产假，其中产前可以休假15天；难产的，增加产假15天；生育多胞胎的，每多生育1个婴儿，增加产假15天. 须提交医院出具的诊断证明书及婴儿出生证明。
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkPeiChanJia" value=""
                                        runat="server" />
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <strong>陪产假</strong><br />
                                    男职工享受15天陪产假，须提交医院出具的诊断证明书及婴儿出生证明。
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="radio" name="chkLeave" id="chkBereavement" onclick="onClickValue();"
                                        value="" runat="server" />
                                </td>
                                <td>
                                    <strong>丧假</strong><br />
                                    父母，配偶，子女3天；（外）祖父母，兄弟姐妹1天。须提交亲属死亡证明
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="radio" name="chkLeave" id="chkPrenatalCheck" onclick="onClickValue();"
                                        value="" runat="server" />
                                </td>
                                <td>
                                    <strong>产检</strong><br />
                                    产前检查，以半天为单位。
                                </td>
                            </tr>

                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="20" style="background-image: url(../../images/headerbg_left.jpg); background-repeat: no-repeat; background-position: center left;"></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td width="70" bgcolor="#F2F2F2">请假原因：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:TextBox TextMode="MultiLine" ID="txtLeaveCause" Width="500px" Height="50px" runat="server" />
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="70">休假时间：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <%--年假、病事假 开始时间--%>
                                                <span id="dvLeaveStar" runat="server">
                                                         <asp:TextBox ID="txtLeavePickerFrom2" onkeyDown="return false; " runat="server" Width="100px" /><asp:DropDownList runat="server" ID="ddlHourFrom"></asp:DropDownList>:<asp:DropDownList runat="server" ID="ddlMinuteFrom"></asp:DropDownList>
                                                </span>
                                                  <%--婚假、产假、丧假 开始时间--%>
                                                <span id="divTime3" runat="server">
                                                    <asp:TextBox ID="txtLeavePickerFrom3" onkeyDown="return false; " runat="server" />
                                                </span>
                                            </td>
                                            <td>至
                                            </td>
                                            <td>
                                                <%--年假、病事假 结束时间--%>
                                                <span id="dvLeaveEnd" runat="server">
                                                    <asp:TextBox ID="txtLeavePickerTo2" onkeyDown="return false; " runat="server" Width="100px" /><asp:DropDownList runat="server" ID="ddlHourTo"></asp:DropDownList>:<asp:DropDownList runat="server" ID="ddlMinuteTo"></asp:DropDownList>
                                                </span>
                                                <%--婚假、产假、丧假 结束时间--%>
                                                <span id="divTimeEnd3" runat="server">
                                                    <asp:TextBox ID="txtLeavePickerTo3" onkeyDown="return false; " runat="server" />
                                                </span>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnLeaveSubmit" Text=" 提交本次事由 " runat="server"
                                        OnClientClick="return SubmitCheckLeave();" OnClick="btnLeaveSubmit_Click" />&nbsp;
                                    <asp:Button ID="btnLeaveBack" Text=" 返回 " runat="server"
                                        OnClick="btnLeaveBack_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">
    $(function () {
        $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
        $("#ctl00_ContentPlaceHolder1_matLeave_txtLeavePickerFrom2").datepicker();
        $("#ctl00_ContentPlaceHolder1_matLeave_txtLeavePickerFrom3").datepicker();
        $("#ctl00_ContentPlaceHolder1_matLeave_txtLeavePickerTo2").datepicker();
        $("#ctl00_ContentPlaceHolder1_matLeave_txtLeavePickerTo3").datepicker();
        
    });
    function onClickValue() {
        var sick = document.getElementById("<%=chkSick.ClientID %>");
        var thing = document.getElementById("<%=chkThing.ClientID %>");
        var annual = document.getElementById("<%=chkAnnual.ClientID %>");
        var annualLast = document.getElementById("<%=chkAnnualLast.ClientID %>");
        var marriage = document.getElementById("<%=chkMarriage.ClientID %>");
        var maternity = document.getElementById("<%=chkMaternity.ClientID %>");
        var bereavement = document.getElementById("<%=chkBereavement.ClientID %>");
        var prenatacheck = document.getElementById("<%=chkPrenatalCheck.ClientID %>");
        var peiChanJia = document.getElementById("<%=chkPeiChanJia.ClientID %>");


        var LeaveStar = document.getElementById("<%=dvLeaveStar.ClientID %>");
        
        var LeaveEnd = document.getElementById("<%=dvLeaveEnd.ClientID %>");
        var DivTime3 = document.getElementById("<%=divTime3.ClientID %>");
        var DivTimeEnd3 = document.getElementById("<%=divTimeEnd3.ClientID %>");

        if (prenatacheck.checked || annual.checked || annualLast.checked || sick.checked || thing.checked) {
            LeaveStar.style.display = "block";
            LeaveEnd.style.display = "block";
            
           
            DivTime3.style.display = "none";
            DivTimeEnd3.style.display = "none";
        }
        else if (marriage.checked || maternity.checked || bereavement.checked || peiChanJia.checked) {
            LeaveStar.style.display = "none";
            LeaveEnd.style.display = "none";
            
            
            DivTime3.style.display = "block";
            DivTimeEnd3.style.display = "block";
        }
    }
    window.onload = onClickValue;
    // 提交校验
    function SubmitCheckLeave() {
        var sick = document.getElementById("<%=chkSick.ClientID %>");
        var thing = document.getElementById("<%=chkThing.ClientID %>");
        var annual = document.getElementById("<%=chkAnnual.ClientID %>");
        var annualLast = document.getElementById("<%=chkAnnualLast.ClientID %>");
        var marriage = document.getElementById("<%=chkMarriage.ClientID %>");
        var maternity = document.getElementById("<%=chkMaternity.ClientID %>");
        var bereavement = document.getElementById("<%=chkBereavement.ClientID %>");
        var prenatacheck = document.getElementById("<%=chkPrenatalCheck.ClientID %>");
        var peiChanJia = document.getElementById("<%=chkPeiChanJia.ClientID %>");

        if (prenatacheck.checked || sick.checked || thing.checked || annual.checked || annualLast.checked) {
            var From2 = document.getElementById("<%=txtLeavePickerFrom2.ClientID %>");
            var hourFrom = document.getElementById("<%=ddlHourFrom.ClientID %>");
            var minuteFrom = document.getElementById("<%=ddlMinuteFrom.ClientID %>");

            var to2 = document.getElementById("<%=txtLeavePickerTo2.ClientID %>");
            var hourTo = document.getElementById("<%=ddlHourTo.ClientID %>");
            var minuteTo = document.getElementById("<%=ddlMinuteTo.ClientID %>");

            var dateTo2 = to2.value.replace(/\-/g, "/");
            var dateFrom2 = From2.value.replace(/\-/g, "/");

            var leaveTo1 = new Date(dateTo2 + " " + hourTo.options[hourTo.selectedIndex].value + ":" + minuteTo.options[minuteTo.selectedIndex].value + ":00");
            var leaveFrom1 = new Date(dateFrom2 + " " + hourFrom.options[hourFrom.selectedIndex].value + ":" + minuteFrom.options[minuteFrom.selectedIndex].value + ":00");
           
            if (leaveTo1 <= leaveFrom1) {
                alert("开始时间必须小于结束时间。");
                return false;
            }

            if (((leaveTo1 - leaveFrom1) % 3600000) != 0) {
                alert("病事年假最小单位是小时，请确认。");
                return false;
            }
        }

        if (marriage.checked || maternity.checked || bereavement.checked || peiChanJia.checked) {
            var leaveTo1 = LeavePickerTo3.SelectedDates.DateArray[0];
            var leaveFrom1 = LeavePickerFrom3.SelectedDates.DateArray[0];
            if (leaveTo1 < leaveFrom1) {
                alert("开始时间不能大于结束时间。");
                return false;
            }
        }

        var leaveCause = document.getElementById("<%=txtLeaveCause.ClientID %>");
        if (leaveCause.value == null || leaveCause.value == "") {
            alert("请填写请假事由。");
            return false;
        }
        showLoading();
        return true;
    }
</script>
