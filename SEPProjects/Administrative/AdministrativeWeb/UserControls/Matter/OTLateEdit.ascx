<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OTLateEdit.ascx.cs"
    Inherits="AdministrativeWeb.UserControls.Matter.OTLateEdit" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<link href="../../css/style.css" type="text/css" rel="stylesheet" />

<script language="javascript" type="text/javascript">
    // 提交校验
    function SubmitCheckOT() {
        var leaveTo1 = OffPickerTo1.SelectedDates.DateArray[0];
        var leaveFrom1 = OffPickerFrom1.SelectedDates.DateArray[0];
        var date1, date2, time1, time2;
        date1 = new Date(leaveTo1.getYear(), (leaveTo1.getMonth() + 1), leaveTo1.getDate());
        date2 = new Date(leaveFrom1.getYear(), (leaveFrom1.getMonth() + 1), leaveFrom1.getDate());
        //date1 = (leaveTo1.getMonth() + 1) + "/" + leaveTo1.getDate() + "/" + leaveTo1.getYear();
        //date2 = (leaveFrom1.getMonth() + 1) + "/" + leaveFrom1.getDate() + "/" + leaveFrom1.getYear();
        time1 = (leaveTo1.getHours() * 60 * 60 * 1000) + (leaveTo1.getMinutes() * 60 * 1000) + (leaveTo1.getSeconds() * 1000) + leaveTo1.getMilliseconds();
        time2 = (leaveFrom1.getHours() * 60 * 60 * 1000) + (leaveFrom1.getMinutes() * 60 * 1000) + (leaveFrom1.getSeconds() * 1000) + leaveFrom1.getMilliseconds();
        var timevalue = 12 * 60 * 60 * 1000;
        if (date1 < date2) {
            alert("开始时间不能大于结束时间。");
            return false;
        }
        else if (date1 == date2 && time2 >= timevalue && time1 < timevalue) {
            alert("开始时间不能大于结束时间。");
            return false;
        }

        var offTuneCause = document.getElementById("<%=txtOTCause.ClientID %>");
        if (offTuneCause.value == null || offTuneCause.value == "") {
            alert("请填写晚到申请事由。");
            return false;
        }
        //        if (leaveTo1 < leaveFrom1) {
        //            alert("开始时间不能大于结束时间。");
        //            return false;
        //        }
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
                        <strong>晚到申请单 </strong>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                <tr>
                    <td class="td">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 姓名： &nbsp;&nbsp;&nbsp;
                                    <asp:HiddenField ID="hidOffUserid" runat="server" />
                                    <asp:TextBox ID="txtOffUserName" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    员工编号：
                                    <asp:TextBox ID="txtOffUserCode" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    业务团队：
                                    <asp:TextBox ID="txtOffTeam" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    组别：
                                    <asp:TextBox ID="txtOffGroup" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    申请时间： &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="labOffAppTime" runat="server" Height="20px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <!-- 剩余调休小时数 -->
                                    <span style="color: #B50000;">
                                        <asp:Label ID="labRemainingHours" runat="server" Height="20px"></asp:Label>
                                    </span>
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
                                    晚到申请事由：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:TextBox TextMode="MultiLine" ID="txtOTCause" Width="500px" Height="50px"
                                        runat="server" />
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="70">
                                    晚到申请时间：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="OffPickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>

                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                至
                                            </td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="OffPickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
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
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    注：使用键盘上下方向键可以切换上、下午。
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnOffSubmit" Text=" 提交本次事由 "  runat="server"
                                        OnClick="btnOffSubmit_Click" OnClientClick="return SubmitCheckOT();" />&nbsp;
                                    <asp:Button ID="btnOffBack" Text=" 返回 " runat="server"
                                        OnClick="btnOffBack_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
