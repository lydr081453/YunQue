<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeaveView.ascx.cs" Inherits="AdministrativeWeb.UserControls.MatterView.LeaveView" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<link href="../../css/style.css" type="text/css" rel="stylesheet" />

<script src="../../js/DatePickerChange.js" type="text/javascript"></script>
<script type="text/javascript">
    window.onload = function onClickValue() {
        var sick = document.getElementById("<%=chkSick.ClientID %>");
        var thing = document.getElementById("<%=chkThing.ClientID %>");
        var annual = document.getElementById("<%=chkAnnual.ClientID %>");
        var annualLast = document.getElementById("<%=chkAnnualLast.ClientID %>");
        var marriage = document.getElementById("<%=chkMarriage.ClientID %>");
        var maternity = document.getElementById("<%=chkMaternity.ClientID %>");
        var bereavement = document.getElementById("<%=chkBereavement.ClientID %>");
        var peiChanJia = document.getElementById("<%=chkPeiChanJia.ClientID %>");

        var AnnualLeave = document.getElementById("<%=dvAnnualLeave.ClientID %>");
        var LeaveStar = document.getElementById("<%=dvLeaveStar.ClientID %>");
        var Days = document.getElementById("<%=dvDays.ClientID %>");
        var LeaveEnd = document.getElementById("<%=dvLeaveEnd.ClientID %>");
        if (sick.checked || thing.checked) {
            LeaveStar.style.display = "block";
            LeaveEnd.style.display = "block";
            AnnualLeave.style.display = "none";
            Days.style.display = "none";
        }
        if (marriage.checked || maternity.checked || bereavement.checked || annual.checked || annualLast.checked || peiChanJia.checked) {
            LeaveStar.style.display = "none";
            LeaveEnd.style.display = "none";
            AnnualLeave.style.display = "block";
            Days.style.display = "block";
        }
    }

    // 提交校验
    function SubmitCheckLeave() {
        var leaveCause = document.getElementById("<%=txtLeaveCause.ClientID %>");
        if (leaveCause.value == null || leaveCause.value == "") {
            alert("请填写请假事由。");
            return false;
        }
        return true;
    }
</script>

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
                                <td>
                                    姓名：
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
                                <td height="20" style="background-image: url(../../images/headerbg_left.jpg); background-repeat: no-repeat;
                                    background-position: center left;">
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="10" cellspacing="0" class="nav_wthotBgimg">
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkSick" value=""
                                        runat="server" disabled="disabled"/>
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
                                    以半日为单位，按《员工手册》规定说明计算，逾期作废
                                </td>
                            </tr>
                             <tr>
                                <td width="20" bgcolor="#F2F2F2">
                                    <input type="radio" name="chkLeave" onclick="onClickValue();" id="chkAnnualLast" value=""
                                        runat="server" />
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <strong>补休去年年假</strong><br />
                                    去年年假延长有效期内，以半日为单位，按《员工手册》规定说明计算，逾期作废
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
                                    男职工享受10天陪产假，须提交医院出具的诊断证明书及婴儿出生证明。
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
                                        value="" runat="server" disabled="disabled"/>
                                </td>
                                <td>
                                    <strong>产检</strong><br />
                                    产前检查，以半天为单位。
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
                                    请假原因：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:TextBox TextMode="MultiLine" ID="txtLeaveCause" Width="500px" Height="50px" runat="server" />
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="70">
                                    休假时间：
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="LeavePickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
                                                </span><span id="dvLeaveStar" runat="server">
                                                    <ComponentArt:Calendar ID="LeavePickerFrom2" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                至
                                            </td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="LeavePickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker">
                                                    </ComponentArt:Calendar>
                                                </span><span id="dvLeaveEnd" runat="server">
                                                    <ComponentArt:Calendar ID="LeavePickerTo2" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
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
                            <tr><td colspan="2">&nbsp;</td></tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnLeaveBack" Text="<img src='../../images/t2_03-22.jpg'/>" runat="server"
                                        Width="56" Height="24" OnClick="btnLeaveBack_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
