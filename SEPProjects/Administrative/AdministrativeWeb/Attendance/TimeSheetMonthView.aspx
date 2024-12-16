<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="TimeSheetMonthView.aspx.cs" Inherits="AdministrativeWeb.Attendance.TimeSheetMonthView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="/new_images/ts_03.jpg">
  <tr>
    <td width="1"><img src="/new_images/ts_01.jpg" width="10" height="42" /></td>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="35"><img src="/new_images/ts_04.jpg" width="27" height="42" /></td>
        <td width="165" style=" color:#ffead8;">Timesheet</td>
        <td style=" color:#ffead8; font-family:'arial black';"><strong><asp:Literal ID="litUserName" runat="server" />&nbsp; &nbsp;<asp:Literal ID="litDate" runat="server" /></strong></td>
        <td>&nbsp;</td>
        <td style="color: #ffead8; font-family: Arial, Helvetica, sans-serif; font-size: 12px;">
                           <strong>年假:<asp:Literal ID="litYear" runat="server" />天
                        </td>
        <td width="1"><asp:HyperLink ID="hylPre" runat="server"><img src="/new_images/ts_07.jpg" width="35" height="26" /></asp:HyperLink></td>
        <td class="title_btn-notSelected"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="26" align="center"><asp:HyperLink ID="hylWeek" runat="server">周</asp:HyperLink></td>
          </tr>
        </table></td>
        <td class="title_btn-Selected">          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td height="26" align="center">月</td>
            </tr>
            </table></td>
        <td width="1"><asp:HyperLink ID="hylNext" runat="server"><img src="/new_images/ts_10.jpg" width="36" height="26" /></asp:HyperLink></td>
      </tr>
    </table></td>
    <td width="1" align="right"><img src="/new_images/ts_02.jpg" width="10" height="42" /></td>
  </tr>
</table>
<%
    var monthList = GetMonthList();
    
     %>
<table width="100%" border="0" cellpadding="10" cellspacing="1" bgcolor="#c6c6c6">
  <tr>
    <td class="month-title">星期日</td>
    <td class="month-title">星期一</td>
    <td class="month-title">星期二</td>
    <td class="month-title">星期三</td>
    <td class="month-title">星期四</td>
    <td class="month-title">星期五</td>
    <td class="month-title">星期六</td>
  </tr>
  
  <%
      for (int i = 0; i < 6; i++)
      {%>
          <tr>
          <%for (int j = 0; j < 7; j++)
          {%>
                
    <td class="<%= monthList[ (i*7)+j].ClassName%>"><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="40" valign="top" width="50%"><%= monthList[ (i*7)+j].Day %></td>
          <td style="font-size:10px;" valign="top" align="right">
            <% if (monthList[(i * 7) + j].TimeSheetCommitModel != null)
                { %>
                    <%= ESP.Administrative.Common.Status.TimeSheetCommitStatusNames[(int)monthList[(i * 7) + j].TimeSheetCommitModel.Status]%>
                <% }
                else {%>
                &nbsp;
               <% } %>
          </td>
        </tr>
                            <tr>
                                                <td style="height:20px; font-size:10px;" colspan="2">
                        <% if (monthList[(i * 7) + j].TimeSheetCommitModel != null)
                           { %>
                            <%if (monthList[(i * 7) + j].TimeSheetCommitModel.CurrentGoWorkTime != null)
                              { %>
                            上:<%=monthList[(i * 7) + j].TimeSheetCommitModel.CurrentGoWorkTime.Value.ToString("HH:mm")%>
                            <%} %>
                            <br />
                            <%if (monthList[(i * 7) + j].TimeSheetCommitModel.CurrentOffWorkTime != null)
                              { %>
                            下:<%=monthList[(i * 7) + j].TimeSheetCommitModel.CurrentOffWorkTime.Value.ToString("HH:mm")%>
                            <%} %>
                            <% }
                           else {%>
                            &nbsp;
                           <% } %>
                        </td>
                    </tr>
        <tr>
                                <td style="width:80px">
                            <% if (monthList[(i * 7) + j].ChiDao > 0)
                               { %>
                            <img src="../new_images/chidao.jpg" alt="" title="迟到" />
                            <%} %>
                            <% if (monthList[(i * 7) + j].ZaoTui > 0)
                               { %>
                            <img src="../new_images/zaotui.jpg" alt="" title="早退" />
                            <%} %>
                            <% if (monthList[(i * 7) + j].KuangGong > 0)
                               { %>
                            <img src="../new_images/kuanggong.jpg" alt="" title="旷工" />
                            <%} %>
                            <% if (monthList[(i * 7) + j].QingJia > 0)
                               { %>
                            <img src="../new_images/qingjia.jpg" alt="" title="请假" />
                            <%} %>
                            <% if (monthList[(i * 7) + j].ChuChai > 0)
                               { %>
                            <img src="../new_images/chucha.jpg" alt="" title="出差" />
                            <%} %>
                            <% if (monthList[(i * 7) + j].WaiChu > 0)
                               { %>
                            <img src="../new_images/waichu.jpg" alt="" title="外出" />
                            <%} %>
                            <% if (monthList[(i * 7) + j].JiaBan > 0)
                               { %>
                            <img src="../new_images/jiaban.jpg" alt="" title="OT" />
                            <%} %>
                            <% if (monthList[(i * 7) + j].TiaoXiu > 0)
                               { %>
                            <img src="../new_images/nianxiujia.jpg" alt="" title="调休" />
                            <%} %>
                            
                        </td>
          <td style="cursor:pointer;" height="40" align="right" valign="bottom"><table border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td align="center" class="number"><a href='TiemSheetViewList.aspx?userid=<%=monthList[ (i*7)+j].UserId %>&bdate=<%= monthList[ (i*7)+j].Year + "-" + monthList[ (i*7)+j].Month + "-" + monthList[ (i*7)+j].Day %>'><%= monthList[(i * 7) + j].TimeSheetHours%></a></td>
            </tr>
          </table>
          </td>
        </tr>
      </table></td>
 
          <%}%>
           </tr>
      <%}
       %>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0" background="/new_images/ts_20.jpg">
  <tr>
    <td width="1"><img src="/new_images/ts_19.jpg" width="10" height="46" /></td>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <%--<td><table border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td class="button-grey"><img src="/new_images/icon-month.jpg" width="13" height="13" / style="margin:0 5px -3px 0;" /><a href="#"><strong>上传本周工时</strong></a></td>
            </tr>
          </table></td>--%>
          <td>&nbsp;</td>
          <td align="right"><%--<img src="/new_images/btn-s1-2.jpg" width="40" height="26" /><a href="#"><img src="/new_images/btn-s2-1.jpg" width="40" height="26" /></a>--%></td>
        </tr>
      </table></td>
    <td width="1" align="right"><img src="/new_images/ts_21.jpg" width="10" height="46" /></td>
  </tr>
</table>
    <br />
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#c1c1c1" style="font-size:12px;">
                                    <tr>
                                        <td colspan="14">考勤统计(单位：小时)</td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="center" bgcolor="#ececec">迟到</td>
                                        <td align="center" bgcolor="#ececec">早退</td>
                                        <td align="center" bgcolor="#ececec">旷工</td>
                                        <td align="center" bgcolor="#ececec">工作日OT</td>
<%--                                        <td align="center" bgcolor="#ececec">节假日OT</td>--%>
                                        <td align="center" bgcolor="#ececec">出差</td>
                                        <td align="center" bgcolor="#ececec">外出</td>
                                        <td align="center" bgcolor="#ececec">病假</td>
                                        <td align="center" bgcolor="#ececec">事假</td>
                                        <td align="center" bgcolor="#ececec">年假</td>
                                        <td align="center" bgcolor="#ececec">婚假</td>
                                        <td align="center" bgcolor="#ececec">丧假</td>
                                        <td align="center" bgcolor="#ececec">产假</td>
                                        <td align="center" bgcolor="#ececec">产检</td>
                                        
                                    </tr>
                                    <tr>
                                        <td height="25" align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labLate" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labLeaveEarly" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labAbsent" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labOverTime" runat="server"></asp:Label>
                                        </td>
<%--                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labHolidayOverTime" runat="server"></asp:Label>
                                        </td>--%>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labEvection" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labEgress" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labSickLeave" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labAffiairLeave" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labAnnualLeave" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labMarriageLeave" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labFuneralLeave" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labMaternityLeave" runat="server"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#ffffff">
                                            <asp:Label ID="labPrenatalCheck" runat="server"></asp:Label>
                                        </td>
                                       
                                    </tr>
                                </table>
</asp:Content>
