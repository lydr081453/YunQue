<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutEdit.ascx.cs" Inherits="AdministrativeWeb.UserControls.Matter.OutEdit" %>

     <link rel="stylesheet" href="/public/css/jquery-ui-new.css">
     <script src="/public/js/jquery1.12.js"></script>
     <script src="/public/js/jquery-ui-new.js"></script>
    <script src="/public/js/jquery.ui.datepicker.cn.js"></script>
    <script language="javascript" type="text/javascript" src="/public/js/dialog.js"></script>


<script language="javascript" type="text/javascript">

    $(function () {
        $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
        $("#ctl00_ContentPlaceHolder1_matOut_txtOutPickerFrom1").datepicker();
        $("#ctl00_ContentPlaceHolder1_matOut_txtOutPickerTo1").datepicker();

    });

    // 提交校验
    function SubmitCheckOut() {
        var From1 = document.getElementById("<%=txtOutPickerFrom1.ClientID %>");
        var hourFrom = document.getElementById("<%=ddlHourFrom.ClientID %>");
        var minuteFrom = document.getElementById("<%=ddlMinuteFrom.ClientID %>");

        var to1= document.getElementById("<%=txtOutPickerTo1.ClientID %>");
        var hourTo = document.getElementById("<%=ddlHourTo.ClientID %>");
        var minuteTo = document.getElementById("<%=ddlMinuteTo.ClientID %>");

        var dateTo1 = to1.value.replace(/\-/g, "/");
        var dateFrom1 = From1.value.replace(/\-/g, "/");

        var leaveTo1 = new Date(dateTo1 + " " + hourTo.options[hourTo.selectedIndex].value + ":" + minuteTo.options[minuteTo.selectedIndex].value + ":00");
        var leaveFrom1 = new Date(dateFrom1 + " " + hourFrom.options[hourFrom.selectedIndex].value + ":" + minuteFrom.options[minuteFrom.selectedIndex].value + ":00");

        if (leaveTo1 <= leaveFrom1) {
            alert("开始时间必须小于结束时间。");
            return false;
        }
        
        var leaveCause = document.getElementById("<%=txtOutCause.ClientID %>");
        if (leaveCause.value == null || leaveCause.value == "") {
            alert("请填写外出事由。");
            return false;
        }
        showLoading();
        return true;
    }
</script>

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
                        <strong>外出申请单 </strong> &nbsp;&nbsp;&nbsp;&nbsp;<font color="red" style="font-size:12px">从9月25开始外出单的申请同样需要提交，并且审批通过后方可提交月度考勤</font>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                <tr>
                    <td class="td">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    姓名：
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:HiddenField ID="hidOutUserid" runat="server" />
                                    <asp:TextBox ID="txtOutUserName" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    员工编号：
                                    <asp:TextBox ID="txtOutUserCode" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    业务团队：
                                    <asp:TextBox ID="txtOutTeam" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    组别：
                                    <asp:TextBox ID="txtOutGroup" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    申请时间：
                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="labOutAppTime" runat="server" Height="20px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="20" style="background-image: url(../../images/headerbg_left.jpg); background-repeat: no-repeat;
                                    background-position: center left;">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td width="70" bgcolor="#F2F2F2">
                                    外出原因：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    &nbsp;&nbsp;<asp:TextBox TextMode="MultiLine" ID="txtOutCause" Width="500px" Height="50px"
                                        runat="server" />
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="70">
                                    外出时间：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <asp:TextBox ID="txtOutPickerFrom1" onkeyDown="return false; " runat="server" Width="100px" /><asp:DropDownList runat="server" ID="ddlHourFrom"></asp:DropDownList>:<asp:DropDownList runat="server" ID="ddlMinuteFrom"></asp:DropDownList>
                                                </span>
                                            </td>
                                            <td>
                                                至
                                            </td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <asp:TextBox ID="txtOutPickerTo1" onkeyDown="return false; " runat="server" Width="100px" /><asp:DropDownList runat="server" ID="ddlHourTo"></asp:DropDownList>:<asp:DropDownList runat="server" ID="ddlMinuteTo"></asp:DropDownList>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnOutSubmit" Text=" 提交本次事由 "  runat="server" OnClientClick="return SubmitCheckOut();" OnClick="btnOutSubmit_Click" />&nbsp;
                                    <asp:Button ID="btnOutBack" Text=" 返回 " runat="server" OnClientClick="return SubmitCheck();" OnClick="btnOutBack_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
