﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OTLateEdit.ascx.cs"
    Inherits="AdministrativeWeb.UserControls.MatterAudit.OTLateEdit" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<link href="../../css/style.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
    // 提交校验
    function SubmitCheckOT() {
        var Remark = document.getElementById("<%=txtOffTuneApproveRemark.ClientID %>");
        if (Remark.value == null || Remark.value == "") {
            alert("请填写审批备注。");
            return false;
        }
        return true;
    }
</script>
<asp:HiddenField ID="hidLeaveApproveId" runat="server" />
<asp:HiddenField ID="hidOffMatterId" runat="server" />
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
                        <strong>调休申请单 </strong>
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
                                    申请时间：
                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="labOffAppTime" runat="server" Height="20px"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                <!-- 剩余调休小时数 -->
                                    <span style="color: #B50000;">
                                    <asp:Label ID="labRemainingHours" runat="server" Height="20px"></asp:Label>
                                    </span>
                                </td>
                            </tr>--%>
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
                                    调休事由：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox TextMode="MultiLine" ID="txtOTCause" Width="500px" Height="50px"
                                                    runat="server" ReadOnly="true" />
                                                <span style="color: #B50000;">*</span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="70">
                                    调休时间：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="OffPickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker" Enabled="false">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                至
                                            </td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="OffPickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
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
                                                <asp:TextBox TextMode="MultiLine" ID="txtOffTuneApproveRemark" runat="server" Width="500px"
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
                                    <asp:ImageButton ImageUrl="~/images/apppass.jpg" ID="btnPass" runat="server" OnClientClick="return SubmitCheckOT();" 
                                        OnClick="btnPass_Click" />&nbsp;
                                    <asp:ImageButton ImageUrl="~/images/appOverrule.jpg" ID="btnOverrule" runat="server" OnClientClick="return SubmitCheckOT();"
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
