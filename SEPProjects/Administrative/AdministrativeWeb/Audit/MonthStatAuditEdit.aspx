<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthStatAuditEdit.aspx.cs"
    Inherits="AdministrativeWeb.Audit.MonthStatAuditEdit" MasterPageFile="~/Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/tabStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function vali() {
            var deductsum = "";
            var patrn = /^(\d{1,3},?)+(\.\d{1,2})?$/;
            if (deductsum.length == "") {
                return confirm("确认进行修改吗？");
            }
            if (!patrn.test(deductsum)) {
                alert("请填写正确的数值！");
                return false;
            }
            else {
                return confirm("确认进行修改吗？");
            }
        }
        function sub() {
            var Remark = document.getElementById("<%=txtAppRemark.ClientID %>");
            if (Remark.value == null || Remark.value == "") {
                alert("请填写审批备注。");
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
                            <strong>考勤详细信息</strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr>
                        <td class="td">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td bgcolor="#F2F2F2" colspan="2">
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin: 0px 0px 5px 15px;">
                                            <tr>
                                                <td colspan="8">
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td valign="top" height="14px">
                                                    姓名：
                                                </td>
                                                <td valign="top">
                                                    <asp:HiddenField ID="hidID" runat="server" />
                                                    <asp:HiddenField ID="hidLeaveUserid" runat="server" />
                                                    <asp:Label ID="txtLeaveUserName" runat="server"></asp:Label>
                                                    ，
                                                </td>
                                                <td valign="top">
                                                    员工编号：
                                                </td>
                                                <td valign="top">
                                                    <asp:Label ID="txtLeaveUserCode" runat="server"></asp:Label>，
                                                </td>
                                                <td valign="top">
                                                    业务团队：
                                                </td>
                                                <td valign="top">
                                                    <asp:Label ID="txtLeaveTeam" runat="server"></asp:Label>，
                                                </td>
                                                <td valign="top">
                                                    组别：
                                                </td>
                                                <td valign="top">
                                                    <asp:Label ID="txtLeaveGroup" runat="server"></asp:Label>
                                                </td>
                                                <td valign="top">
                                                    <span style="color: Red;">
                                                    <asp:Label ID="lblAttendanceType" runat="server" Font-Bold="true"></asp:Label>
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F2F2" colspan="2" style="padding: 0px 0px 0px 10px;">
                                        <ComponentArt:Calendar ID="cldAttendance" runat="server" AutoPostBackOnSelectionChanged="false"
                                            AutoPostBackOnVisibleDateChanged="true" AllowDaySelection="true" ControlType="Calendar"
                                            DayNameFormat="Full" ClientTarget="Auto" CalendarCssClass="calendar" MonthCssClass="month"
                                            DayHeaderCssClass="dayheader" CalendarTitleCssClass="CalendarTitle" OtherMonthDayCssClass="othermonthday"
                                            SelectedDayCssClass="selectedday" NextPrevCssClass="nextprev" UseServersTodaysDate="true"
                                            ImagesBaseUrl="../images/calendar" PrevImageUrl="prev_wht.gif" NextImageUrl="next_wht.gif"
                                            MonthPadding="0" MonthSpacing="0" PopUp="None" ReactOnSameSelection="false" RenderSearchEngineStamp="false">
                                            <Templates>
                                                <ComponentArt:CalendarDayCustomTemplate ID="DefaultTemplate" runat="server">
                                                    <Template>
                                                        <div style="float: right; font-size: larger">
                                                            <%# DataBindGetDay(Container.DataItem) %>&nbsp;</div>
                                                        <div>
                                                            <%# DataBindGetClockIn(Container.DataItem) %>&nbsp;</div>
                                                        <div>
                                                            <%# DataBindGetClockOut(Container.DataItem) %>&nbsp;</div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div style="position: relative; top: 20px">
                                                            <%# DataBindGetIconsHtml(Container.DataItem) %></div>
                                                    </Template>
                                                </ComponentArt:CalendarDayCustomTemplate>
                                            </Templates>
                                        </ComponentArt:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F2F2" colspan="2">
                                        <br />
                                        <table id="Table1" width="674" border="0" cellpadding="0" cellspacing="0" bgcolor="#e4e4e4" style="margin: 0px 0px 0px 10px;"
                                            runat="server">
                                            <tr>
                                                <td width="4" style="background-image: url(../images/kaoqin_37.jpg); background-repeat: repeat-x;">
                                                    <img src="../images/kaoqin_36.jpg" width="4" height="27" />
                                                </td>
                                                <td style="background-image: url(../images/kaoqin_37.jpg); background-repeat: repeat-x;">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="20%" height="27" align="left" style="padding-left: 10px;">
                                                                <strong>考勤统计</strong>（天：D，小时：H）
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="4" style="background-image: url(../images/kaoqin_37.jpg); background-repeat: repeat-x;"
                                                    align="right">
                                                    <img src="../images/kaoqin_42.jpg" width="4" height="27" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="padding-bottom: 4px;">
                                                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1">
                                                        <tr>
                                                            <td height="25" align="center" bgcolor="#ececec">
                                                                迟到
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                早退
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                旷工D
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                出差D
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                外出H
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                病假H
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                事假H
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                年假D
                                                            </td>
                                                             <td align="center" bgcolor="#ececec">
                                                                年假(补)D
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                婚假D
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                丧假D
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                产假D
                                                            </td>
                                                            <td align="center" bgcolor="#ececec">
                                                                产检H
                                                            </td>
                                                          
                                                        </tr>
                                                        <tr>
                                                            <td height="25" align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtLate" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57))) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtLeaveEarly" runat="server" Width="85%" OnKeyPress="if((event.keyCode>=48) && (event.keyCode <=57)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtAbsent" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtEvection" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtEgress" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtSickLeave" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtAffiairLeave" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtAnnualLeave" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                              <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtAnnualLast" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtMarriageLeave" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtFuneralLeave" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtMaternityLeave" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                            <td align="center" bgcolor="#ffffff">
                                                                <asp:TextBox ID="txtPrenatalCheck" runat="server" Width="85%" OnKeyPress="if(((event.keyCode>=48) && (event.keyCode <=57)) || event.keyCode==46) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                                            </td>
                                                           
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2F2F2" colspan="2" valign="top" style="padding: 0px 0px 0px 10px;">
                                        <asp:Label ID="labApproveDesc" runat="server"></asp:Label>
                                        <br />
                                        审批意见：
                                        <asp:TextBox TextMode="MultiLine" ID="txtAppRemark" runat="server" Width="500px"
                                            Height="40px" BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
                                        <span style="color: #B50000;">*</span>
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
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="btnSave" Text="<img src='../../images/t2_03-20.jpg'/>" runat="server"
                                             OnClientClick="return vali();" OnClick="btnSave_Click" />&nbsp;
                                        <asp:ImageButton ID="btnAppPass" runat="server" ImageUrl="~/images/apppass.jpg" OnClientClick="return sub();"
                                            OnClick="btnAppPass_Click" />&nbsp;
                                        <asp:ImageButton ID="btnOverrule" runat="server" ImageUrl="~/images/appOverrule.jpg"
                                            OnClientClick="return sub();" OnClick="btnOverrule_Click" />&nbsp;
                                        <asp:LinkButton ID="btnBack" Text="<img src='../../images/t2_03-22.jpg'/>" runat="server"
                                             OnClick="btnBack_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
