<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtherEdit.ascx.cs" Inherits="AdministrativeWeb.UserControls.Matter.OtherEdit" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<link href="../../css/style.css" type="text/css" rel="stylesheet" />
<script language="javascript" type="text/javascript">
    // 提交校验
    function SubmitCheckOther() {
        var leaveTo1 = OtherPickerTo1.SelectedDates.DateArray[0];
        var leaveFrom1 = OtherPickerFrom1.SelectedDates.DateArray[0];
        if (leaveTo1 <= leaveFrom1) {
            alert("开始时间必须小于结束时间。");
            return false;
        }
        
        var leaveCause = document.getElementById("<%=txtOtherCause.ClientID %>");
        if (leaveCause.value == null || leaveCause.value == "") {
            alert("请填写原因。");
            return false;
        }
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
                        <strong>其他申请单 </strong>
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
                                    <asp:HiddenField ID="hidOtherUserid" runat="server" />
                                    <asp:TextBox ID="txtOtherUserName" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    员工编号：
                                    <asp:TextBox ID="txtOtherUserCode" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    业务团队：
                                    <asp:TextBox ID="txtOtherTeam" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    组别：
                                    <asp:TextBox ID="txtOtherGroup" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    申请时间：
                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="labOtherAppTime" runat="server" Height="20px"></asp:Label>
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
                                    原因：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    &nbsp;&nbsp;<asp:TextBox TextMode="MultiLine" ID="txtOtherCause" Width="500px" Height="50px"
                                        runat="server" />
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="70">
                                    时间：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="OtherPickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                至
                                            </td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="OtherPickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
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
                                <td colspan="2">
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:Button ID="btnOutSubmit" Text=" 提交本次事由 "  runat="server"
                                         OnClientClick="return SubmitCheckOther();" OnClick="btnOtherSubmit_Click"/>&nbsp;
                                    <asp:Button ID="btnOtherBack" Text=" 返回 " runat="server"
                                         OnClick="btnOtherBack_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
