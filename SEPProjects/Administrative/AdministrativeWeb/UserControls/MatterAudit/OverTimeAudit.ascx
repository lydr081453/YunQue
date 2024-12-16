<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OverTimeAudit.ascx.cs"
    Inherits="AdministrativeWeb.UserControls.MatterAudit.OverTimeAudit" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>


<script language="javascript" type="text/javascript">
    function openOverTimeProject() {
        var win = window.open('OverTimeSelectProjectList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setOverTimeProjectInfo(projectId, projectCode, projectDesc, deptList) {
        document.getElementById("<%=hidOverTimeProjectId.ClientID %>").value = projectId;
        document.getElementById("<%=txtOverTimeProjectNo.ClientID %>").value = projectCode;
    }
    
    // 提交校验
    function SubmitCheckOverTime() {
        var projectNo = document.getElementById("<%=txtOverTimeApproveRemark.ClientID %>");
        if (projectNo.value == null || projectNo.value == "") {
            alert("请填写审批备注。");
            return false;
        }
        return true;
    }
</script>
<link href="../../css/a.css" rel="stylesheet" type="text/css" />
<link href="../../css/calendar.css" type="text/css" rel="stylesheet" />
<script src="../../js/DatePicker.js" type="text/javascript"></script> 

<asp:HiddenField ID="singlOverTimeId" runat="server" />
<asp:HiddenField ID="hidLeaveApproveId" runat="server" />
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
                        <strong>OT申请单 </strong>
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
                                <td width="10%">
                                    姓名：
                                </td>
                                <td>
                                    <asp:HiddenField ID="hidUserid" runat="server" />
                                    <asp:TextBox ID="txtUserName" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    员工编号：
                                    <asp:TextBox ID="txtUserCode" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    业务团队：
                                    <asp:TextBox ID="txtTeam" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    组别：
                                    <asp:TextBox ID="txtGroup" runat="server" Width="100px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    申请时间：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:Label ID="labAppTime" runat="server" Height="20px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    OT项目号：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <asp:HiddenField ID="hidOverTimeProjectId" runat="server" />
                                    <asp:TextBox ID="txtOverTimeProjectNo" runat="server" Width="400px" Height="14px" BorderColor="#CCCCCC"
                                        BorderWidth="1px" BorderStyle="Solid" onkeyDown="return false;"></asp:TextBox>
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    工作描述：
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" ID="txtDes" runat="server" Width="500px" Height="40px"
                                        BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="Solid" ReadOnly="true"></asp:TextBox>
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#F2F2F2">
                                    OT时长：
                                </td>
                                <td bgcolor="#F2F2F2">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span id="dvAnnualLeave" runat="server">
                                                    <ComponentArt:Calendar ID="PickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker" Enabled="false">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>至</td>
                                            <td>
                                                <span id="dvDays" runat="server">
                                                    <ComponentArt:Calendar ID="PickerTo1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd, HH:mm"
                                                        ControlType="Picker" PickerCssClass="picker" Enabled="false">
                                                    </ComponentArt:Calendar>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:GridView ID="grdMatterDetails" Width="80%" runat="server" 
                                        AutoGenerateColumns="false" onrowdatabound="grdMatterDetails_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="详细时间" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTimeStart" runat="server"></asp:Label>&nbsp;至&nbsp;
                                                    <asp:Label ID="lblTimeEnd" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="工作内容" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                <ItemTemplate>
                                                <asp:Label TextMode="MultiLine" ID="txtMatterDetails" runat="server" BorderColor="#CCCCCC"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    审批意见：
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" ID="txtOverTimeApproveRemark" runat="server" Width="500px"
                                        Height="40px" BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
                                    <span style="color: #B50000;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 10px 0 5px 5px; color: #B50000;">
                                    注：周末及节假日OT以半日起算，调休也以半日为调休原则。
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="5" cellpadding="0">
                            <tr>
                                <td width="60">
                                   <asp:ImageButton ID="btnPass" runat="server" ImageUrl="~/images/apppass.jpg" OnClientClick="return SubmitCheckOverTime();" 
                                        OnClick="btnPass_Click" />
                                </td>
                                <td width="60">
                                   <asp:ImageButton ID="btnOverrule" runat="server" ImageUrl="~/images/appOverrule.jpg" OnClientClick="return SubmitCheckOverTime();"
                                        OnClick="btnOverrule_Click" />
                                </td>
                                <td width="60">
                                    <asp:ImageButton ID="Button1" runat="server" ImageUrl="../../images/t2_03-22.jpg"
                                        OnClick="btnBack_Click" CausesValidation="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<ComponentArt:Calendar runat="server" ID="CalendarFrom" AllowMultipleSelection="false"
    AllowWeekSelection="false" AllowMonthSelection="false" ControlType="Calendar"
    PopUp="Custom" PopUpExpandControlId="calendar_from_button" CalendarTitleCssClass="title"
    DayHeaderCssClass="dayheader" DayCssClass="day" DayHoverCssClass="dayhover" OtherMonthDayCssClass="othermonthday"
    SelectedDayCssClass="selectedday" CalendarCssClass="calendar" NextPrevCssClass="nextprev"
    MonthCssClass="month" SwapSlide="Linear" SwapDuration="300" DayNameFormat="Short"
    ImagesBaseUrl="../../images/calendar" PrevImageUrl="cal_prevMonth.gif" NextImageUrl="cal_nextMonth.gif">
    <ClientEvents>
        <SelectionChanged EventHandler="CalendarFrom2_OnChange" />
    </ClientEvents>
</ComponentArt:Calendar>
