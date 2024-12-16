<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OutAudit.ascx.cs" Inherits="AdministrativeWeb.UserControls.MatterAudit.OutAudit" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<link href="../../css/style.css" type="text/css" rel="stylesheet" />
<script language="javascript" type="text/javascript">
    // 提交校验
    function SubmitCheckOut() {
        var leaveTo1 = OutPickerTo1.SelectedDates.DateArray[0];
        var leaveFrom1 = OutPickerFrom1.SelectedDates.DateArray[0];
        if (leaveTo1 <= leaveFrom1) {
            alert("开始时间必须小于结束时间。");
            return false;
        }
        
        var leaveCause = document.getElementById("<%=txtOutCause.ClientID %>");
        if (leaveCause.value == null || leaveCause.value == "") {
            alert("请填写外出事由。");
            return false;
        }
        return true;
    }
</script>

<asp:HiddenField ID="hidOutID" runat="server" />
<asp:HiddenField ID="hidOutApproveId" runat="server" />
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
                        <strong>外出申请单 </strong>
                        <span style="color: #B50000; font-size: 9pt;">
                            <b>
                                <asp:Label runat="server" ID="labAfterApprove"></asp:Label>
                            </b>
                        </span>
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
                                    &nbsp;&nbsp;<asp:TextBox TextMode="MultiLine" ID="txtOutCause" Width="500px" Height="50px" ReadOnly="true"
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
                                                    <ComponentArt:Calendar ID="OutPickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker" Enabled="false">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                至
                                            </td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="OutPickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker" Enabled="false">
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
                                    审批意见：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox TextMode="MultiLine" ID="txtOutApproveRemark" runat="server" Width="500px"
                                                    Height="40px" BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
                                                <span style="color: #B50000;">*</span>
                                            </td>
                                        </tr>
                                    </table>
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
                                    <asp:ImageButton ImageUrl="~/images/apppass.jpg" ID="btnPass" runat="server" OnClientClick="return SubmitCheckOff();" 
                                        OnClick="btnPass_Click" />&nbsp;
                                    <asp:ImageButton ImageUrl="~/images/appOverrule.jpg" ID="btnOverrule" runat="server" OnClientClick="return SubmitCheckOff();"
                                        OnClick="btnOverrule_Click" />&nbsp;
                                    <asp:ImageButton ID="btnOffBack" runat="server" ImageUrl="../../images/t2_03-22.jpg"
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
