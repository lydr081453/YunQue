<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="HRTimeSheetAudit.aspx.cs" Inherits="AdministrativeWeb.Attendance.HRTimeSheetAudit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript">
    function chkSubmit(type) {
        var returnValue = true;
        if ($("[name=commitIds]:checked").length == 0 && $("[name=serialNos]:checked").length == 0) {
            alert('请选择审核的TimeSheet！');
            return false;
        }
        if (type == "0") {
            returnValue = confirm('您确定通过选中的TimeSheet吗？')
        } else {
            returnValue = confirm('您确定驳回选中的TimeSheet吗？')
        }
        return returnValue;
    }


    function updateChecked(obj) {
        var now = new Date();
        var number = now.getSeconds();
        var checked = $(obj).attr("checked");
        var id = $(obj).val();
        $.ajax(
            { url: "TimeSheetAudit.aspx?tid=" + id + "&updateChecked=" + checked + "&random=" + number,
                success: function(data) {
                }
            });
    }
</script>
<%
        var week = GetWeekInfoList();
        var types = GetTimeSheetTypeList(); 
    %>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="/new_images/ts_03.jpg">
        <tr>
            <td width="1">
                <img src="/new_images/ts_01.jpg" width="10" height="42" />
            </td>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="35">
                            <img src="/new_images/ts_04.jpg" width="27" height="42" />
                        </td>
                        <td width="165" style="color: #ffead8;">
                            Timesheet
                        </td>
                        <td style="color: #ffead8; font-family: 'arial black';">
                            <strong><asp:Literal ID="litUserName" runat="server" />&nbsp; &nbsp;
                                <asp:Literal ID="litDate" runat="server" /></strong>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="1" align="right">
                <img src="/new_images/ts_02.jpg" width="10" height="42" />
            </td>
        </tr>
    </table>
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-image:url(images/ts_20.jpg); border-bottom:1px dotted #959595;font-size:12px;border-left:1px solid #c6c6c6;border-right:1px solid #c6c6c6;">
      <tr>
        <td width="30px"></td>
        <td width="160" height="43">&nbsp;</td>
        <td width="120" height="43" style="padding-left:20px;"><strong>全部分类<%--<img src="images/icon-cor.jpg" width="7" height="6" hspace="3" />--%></strong></td>
        <td width="50" style="padding-left:10px;"><strong>时间</strong></td>
        <td width="110" style="padding-left:10px;"><strong>项目号</strong></td>
        <td><strong>描述</strong></td>
        <td width="10px;">&nbsp;</td>
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
            <td align="center" width="30px"><input type="checkbox" id="commitIds" name="commitIds" checked="checked" value='<%= day.TimeSheetCommitInfo.Id %>' /></td>
            <td rowspan="2" valign="top" <% if(isToday){ %> class="month-list-left-today" <%} else{ %>
                class="month-list-left" <%} %> id="Td1">
                <span class="date">
                    <%=day.Month %>.<%=day.Day %></span><br />
                <span class="week">
                    <%=day.WeekCn %></span><br />
                <table border="0" cellpadding="5" cellspacing="0" class="hours">
                      <tr>
                        <td width="48" align="center">
                        <%if(day.TimeSheetCommitInfo.CurrentGoWorkTime != null){ %>
                            上:<%=day.TimeSheetCommitInfo.CurrentGoWorkTime.Value.ToString("HH:mm") %>
                        <%} %>
                        <br />
                        <%if(day.TimeSheetCommitInfo.CurrentOffWorkTime != null){ %>
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
  <%--                                  <td width="70" align="center" class="item">
                                        <%if (timesheet.TypeId == (int)ESP.Administrative.Common.TimeSheetType.Late || timesheet.TypeId == (int)ESP.Administrative.Common.TimeSheetType.LeaveEarly || timesheet.TypeId == (int)ESP.Administrative.Common.TimeSheetType.StayAway)
                                          { %>
                                            <input type="checkbox" value='<%= timesheet.Id %>' onclick="updateChecked(this);"  <%= timesheet.IsChecked == true ? "checked='checked'" : ""   %> /> <span style="color:Red"><%=ESP.Administrative.Common.Status.TimeSheetTypeName()[timesheet.TypeId]%></span>
                                        <%}
                                          else
                                          { %>
                                            <%=ESP.Administrative.Common.Status.TimeSheetTypeName()[timesheet.TypeId]%>
                                        <%} %>
                                    </td>--%>
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
    <asp:Panel ID="palLong" runat="server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="/new_images/ts_03.jpg">
        <tr>
            <td width="1">
                <img src="/new_images/ts_01.jpg" width="10" height="42" />
            </td>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="35">
                            <img src="/new_images/ts_04.jpg" width="27" height="42" />
                        </td>
                        <td width="165" style="color: #ffead8;">
                            长假
                        </td>
                        <td style="color: #ffead8; font-family: 'arial black';">
                            <strong><asp:Literal ID="Literal1" runat="server" />&nbsp; &nbsp;
                                <asp:Literal ID="Literal2" runat="server" /></strong>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="1" align="right">
                <img src="/new_images/ts_02.jpg" width="10" height="42" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" Width="100%" CellPadding="5" CellSpacing="0">
            <Columns>
                <asp:TemplateField HeaderText="选择" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <input type="checkbox" id="serialNos" name="serialNos" checked="checked" value='<%# Eval("serialNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="长假类型" DataField="CategoryName" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="起始时间" DataField="BeginDate" DataFormatString="{0: yyyy-MM-dd}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="结束时间" DataField="EndDate" DataFormatString="{0: yyyy-MM-dd}" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="描述" DataField="workitem" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br />
        <table width="100%">
        <tr>
            <td width="8%">审核描述：</td>
            <td><asp:TextBox ID="txtDesc" runat="server" Width="70%" /></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnOk" runat="server" Text="审核通过" class="button-orange" OnClick="btnOk_Click" OnClientClick="return chkSubmit('0');" />&nbsp;
                <asp:Button ID="btnNo" runat="server" Text="审批驳回" class="button-orange" OnClick="btnNo_Click" OnClientClick="return chkSubmit('1');" />&nbsp;
                <%--<input type="button" value="返回" onclick="javascript:history.back(1);" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
