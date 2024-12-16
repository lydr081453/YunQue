<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="TiemSheetViewList.aspx.cs" Inherits="AdministrativeWeb.Attendance.TiemSheetViewList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%
        var week = GetWeekInfoList();
        var types = GetTimeSheetTypeList(); 
    %>
    
    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="/new_images/ts_03.jpg">
      <tr>
        <td width="1"><img src="/new_images/ts_01.jpg" width="10" height="42" /></td>
        <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="35"><img src="/new_images/ts_04.jpg" width="27" height="42" /></td>
            <td width="125" style=" color:#ffead8;">Timesheet</td>
            <td width="220" style=" color:#ffead8; font-family:'arial black';" colspan="2">
            <strong><asp:Literal ID="litUserName" runat="server" />&nbsp; &nbsp;
                                <asp:Literal ID="litDate" runat="server" /></strong></td>
            
            <td width="100"></td>
            <td width="1"><asp:HyperLink ID="hylPre" runat="server"><img src="/new_images/ts_07.jpg" width="35" height="26" /></asp:HyperLink></td>
            <td class="title_btn-Selected"><a href="#"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="26" align="center">周</td>
              </tr>
            </table></a></td>
            <td class="title_btn-notSelected"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td height="26" align="center"><asp:HyperLink ID="hylMonth" runat="server">月</asp:HyperLink></td>
                </tr>
                </table></td>
            <td width="1"><asp:HyperLink ID="hylNext" runat="server"><img src="/new_images/ts_10.jpg" width="36" height="26" /></asp:HyperLink></td>
          </tr>
        </table></td>
        <td width="1" align="right"><img src="/new_images/ts_02.jpg" width="10" height="42" /></td>
      </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-image:url(images/ts_20.jpg); border-bottom:1px dotted #959595;font-size:12px;border-left:1px solid #c6c6c6;border-right:1px solid #c6c6c6;">
      <tr>
        <td width="160" height="43">&nbsp;</td>
        <td width="120" height="43" style="padding-left:20px;"><strong>全部分类<%--<img src="images/icon-cor.jpg" width="7" height="6" hspace="3" />--%></strong></td>
        <td width="50" style="padding-left:10px;"><strong>时间</strong></td>
        <td width="110" style="padding-left:10px;"><strong>项目号</strong></td>
        <td><strong>描述</strong></td>
      </tr>
    </table>
    
    <%
        foreach (var day in week.DayInfos)
        {
            bool isToday = false;
            if (day.Year == DateTime.Now.Year && day.Month == DateTime.Now.Month && day.Day == DateTime.Now.Day)
            {
                isToday = true;
            }
    %>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px solid #c6c6c6;
        border-left: 1px solid #c6c6c6; border-right: 1px solid #c6c6c6;">
        <tr>
            <td rowspan="2" valign="top" <% if(isToday){ %> class="month-list-left-today" <%} else{ %>
                class="month-list-left" <%} %> id="month-list-left">
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>                <span class="date">
                    <%=day.Month %>.<%=day.Day %></span><br />
                <span class="week">
                    <%=day.WeekCn %></span><br /></td>
                        <td align="right" valign="top" style="padding-right:5px;">
                            <%if (day.TimeSheetCommitInfo != null)
                              {%>
                                <%= ESP.Administrative.Common.Status.TimeSheetCommitStatusNames[(int)day.TimeSheetCommitInfo.Status] %>
                              <%    
                              } %>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="5" cellspacing="0" class="hours">
                      <tr>
                        <td width="48" align="center">
                        <%if (day.TimeSheetCommitInfo != null && day.TimeSheetCommitInfo.CurrentGoWorkTime != null)
                          { %>
                            上:<%=day.TimeSheetCommitInfo.CurrentGoWorkTime.Value.ToString("HH:mm") %>
                        <%} %>
                        <br />
                        <%if( day.TimeSheetCommitInfo != null && day.TimeSheetCommitInfo.CurrentOffWorkTime != null){ %>
                            下:<%=day.TimeSheetCommitInfo.CurrentOffWorkTime.Value.ToString("HH:mm")%>
                        <%} %>
                        </td>
                        <td width="48" align="center" class="right"><strong><%=day.WorkHours%></strong><br />
                        小时</td>
                      </tr>
                      <tr>
                        <td colspan="2" align="center" class="bottom">填报小时 <strong><%=day.TotalHours %></strong></td>
                      </tr>
                </table>
                <br />
            </td>
            <td valign="top" style="border-left: 1px dotted #c6c6c6;">

                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <%foreach (var timesheet in day.TimeSheets)
                      { %>
                    <tr>
                        <td class="week_list">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="110" valign="top" style="padding-left: 20px;">
                                        <%if (string.IsNullOrEmpty(timesheet.CategoryName))
                                          { %>
                                            <span style="color:Red">打卡记录异常</span>
                                        <%}else{ %>
                                            <%=timesheet.CategoryName%>
                                        <%} %>
                                    </td>
                                    <td width="50" valign="top" class="time">
                                        <%=timesheet.Hours%>
                                    </td>
                                    <td width="120" valign="top" class="item">
                                        <span title="<%=timesheet.ProjectName%>"><%=timesheet.ProjectCode%></span>
                                    </td>
                                    <td valign="top" class="description">
                                        <%=timesheet.WorkItem%>
                                    </td>
                                    <td width="70" align="center" class="item">
                                        <%if (timesheet.TypeId != (int)ESP.Administrative.Common.TimeSheetType.TimeSheet)
                                          { %>
                                            <span style="color:Red"><%=ESP.Administrative.Common.Status.TimeSheetTypeName()[timesheet.TypeId]%></span>
                                        <%}
                                          else
                                          { %>
                                            <%=ESP.Administrative.Common.Status.TimeSheetTypeName()[timesheet.TypeId]%>
                                        <%} %>
                                    </td>
                                    <td width="70" align="center" class="item">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%} %>
                </table>
            </td>
        </tr>
    </table>
    <%} %>
</asp:Content>
