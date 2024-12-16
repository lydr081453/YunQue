<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newMonthStatAuditList.aspx.cs" Inherits="AdministrativeWeb.Audit.newMonthStatAuditList" MasterPageFile="~/Default.Master" %>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <link href="../css/comboboxStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        function checkedAll() {
            for (var i = 0; i < document.getElementsByName("chkAudit").length; i++) {
                var e = document.getElementsByName("chkAudit")[i];

                e.checked = document.getElementById("chkAll").checked;
            }
        }

        function getType(ID) {
            var hidtype = document.getElementById("<%= hidType.ClientID %>").value;
            var sum = "";
            if (hidtype <= 6) {
                sum = "<a href='MonthStatAuditEdit.aspx?id=" + ID + "&type=" + hidtype + "'><img src=\"../images/edit.gif\" /></a>";
            }
            return sum;           
        }

        function submitMatter(date1, flag) {
            var hid = document.getElementById("<%= hidMatter.ClientID %>");
            hid.value = "";
            var col = document.getElementsByName("chkAudit");
            for (var i = 0; i < col.length; i++) {
                //   var e = document.getElementsByName("ctl00$ContentPlaceHolder1$ctl00_ContentPlaceHolder1_GridNoNeed_0_1_0$chkAudit")[i];
                var e = col[i];

                if (e.checked)
                    hid.value += e.value + ",";
            }
            if (hid.value == "") {
                alert("请选择要审批的事由！");
                return false;
            }
            else {
                hid.value = hid.value.substring(0, hid.value.length - 1);
                if (flag == 1)
                    return confirm("您确定要审批通过，您所选择的考勤记录吗？");
                else
                    return confirm("您确定要审批驳回，您所选择的考勤记录吗？");
            }
        }
    </script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
          <td>
              <table width="100%" border="0" cellpadding="5" cellspacing="0">
                  <tr>
                      <td align="right">
                        时间：
                      </td>
                      <td align="left">
                        &nbsp;<asp:DropDownList ID="drpYear" runat="server" />年
                        <asp:DropDownList ID="drpMonth" runat="server" />月
                      </td>
                      <td align="right">
                        姓名：
                      </td>
                      <td align="left">
                        <asp:TextBox ID="txtUserName" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="202" Height="18"></asp:TextBox>
                      </td>
                      <td align="right">
                        &nbsp;
                      </td>
                  </tr>
                  <tr>
                    <td align="right">
                        部门：
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="upnDepartment" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <ComponentArt:ComboBox ID="cbCompany" runat="Server" Width="100" Height="20" AutoHighlight="false"
                                                AutoComplete="true" AutoFilter="true" DataTextField="CountryName" DataValueField="CountryCode"
                                                ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" CssClass="cmb" HoverCssClass="cmb-hover"
                                                TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbCom_SelectedIndexChanged">
                                            </ComponentArt:ComboBox>
                                        </td>
                                        <td>
                                            <ComponentArt:ComboBox ID="cbDepartment1" runat="Server" Width="100" Height="20"
                                                AutoHighlight="false" AutoComplete="true" AutoFilter="true" DataTextField="CountryName"
                                                DataValueField="CountryCode" ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover"
                                                CssClass="cmb" HoverCssClass="cmb-hover" TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                DropDownHeight="100" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbDepartment1_SelectedIndexChanged">
                                            </ComponentArt:ComboBox>
                                        </td>
                                        <td>
                                            <ComponentArt:ComboBox ID="cbDepartment2" runat="Server" Width="100" Height="20"
                                                AutoHighlight="false" AutoComplete="true" AutoFilter="true" DataTextField="CountryName"
                                                DataValueField="CountryCode" ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover"
                                                CssClass="cmb" HoverCssClass="cmb-hover" TextBoxCssClass="txtcob" DropHoverImageUrl="../../images/combobox/ddn-hover.png"
                                                DropImageUrl="../../images/combobox/ddn.png" DropDownResizingMode="bottom" DropDownWidth="98"
                                                DropDownHeight="200" DropDownCssClass="ddn" DropDownContentCssClass="ddn-con">
                                            </ComponentArt:ComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="right">
                        员工编号：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUserCode" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" Width="202" Height="18"></asp:TextBox>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                  </tr>
                  <tr>
                      <td align="right">
                        &nbsp;
                      </td>
                      <td align="left">
                        &nbsp;
                      </td>
                      <td align="right">
                        &nbsp;
                      </td>
                      <td align="left">
                        &nbsp;
                      </td>
                      <td align="right">
                        <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" ID="btnSearch"
                            runat="server" OnClick="btnSearch_Click" ImageAlign="Middle" />
                      </td>
                  </tr>
              </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:ImageButton ID="btnExport" ImageUrl="../images/export.jpg" ToolTip="导出当月的考勤记录信息"
                            Width="52" Height="29" hspace="10" OnClientClick="return confirm('您确定要导出当月考勤记录？');"
                            OnClick="btnExport_Click" runat="server" />
                       <%-- <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="导出记录" />--%>
                    </td>
                </tr>
              <tr>
                <td class="td">
                    <ComponentArt:Grid ID="Grid1"                                              
                        DataAreaCssClass="GridData" 
                        EnableViewState="true"
                        ShowHeader="true"
                        CssClass="Grid"
                        FooterCssClass="GridFooter" 
                        PagerInfoClientTemplateId="ClientTemplate3"
                        GroupingNotificationTextCssClass="GridHeaderText"
                        GroupingNotificationText="月审核人员列表"
                        HeaderHeight="30"
                        HeaderCssClass="GridHeader"
                        IndentCellWidth="22"
                        PagerStyle="Numbered" 
                        PagerPosition="BottomRight"
                        PagerInfoPosition="BottomLeft"
                        PagerTextCssClass="GridFooterText"
                        PagerButtonWidth="41"
                        PagerButtonHeight="22"
                        PagerButtonHoverEnabled="false"
                        PagePaddingEnabled="false"
                        PageSize="50"
                        SliderHeight="26"
                        SliderWidth="150" 
                        SliderGripWidth="9" 
                        SliderPopupOffsetX="50" 
                        ImagesBaseUrl="../images/gridview2/"
                        PagerImagesFolderUrl="../images/gridview2/pager/" 
                        TreeLineImagesFolderUrl="../images/gridview2/lines/"
		                TreeLineImageWidth="11"
		                TreeLineImageHeight="11"
		                PreExpandOnGroup="true"
		                ClientTarget="Uplevel"
		                AutoPostBackOnSelect="false"
		                EditOnClickSelectedItem="false"
                        Width="100%" Height="500"
                        runat="server">
                        <Levels>
                            <ComponentArt:GridLevel   
                                DataKeyField="ID"                               
                                ShowTableHeading="false"
                                TableHeadingCssClass="GridHeader" 
                                RowCssClass="Row" 
                                ColumnReorderIndicatorImageUrl="reorder.gif"
                                DataCellCssClass="DataCell" 
                                HeadingCellCssClass="HeadingCell" 
                                HeadingCellHoverCssClass="HeadingCellHover"
                                HeadingCellActiveCssClass="HeadingCellActive" 
                                HeadingRowCssClass="HeadingRow"
                                HeadingTextCssClass="HeadingCellText" 
                                SortedDataCellCssClass="SortedDataCell"
                                SortAscendingImageUrl="asc.gif" 
                                SortDescendingImageUrl="desc.gif" 
                                SortImageWidth="9"
                                SortImageHeight="5">
                                <Columns>                                    
                                    <ComponentArt:GridColumn DataField="ID" AllowSorting="True" Visible="false"/>
                                    <ComponentArt:GridColumn HeadingText="选择" AllowSorting="True" DataCellClientTemplateId="checkTemplate" Width="30" Align="Center"/> 
                                    <ComponentArt:GridColumn HeadingText="姓名" AllowSorting="True" DataField="ApplicantName" Align="Center" Width="50" />
                                    <ComponentArt:GridColumn HeadingText="迟到" AllowSorting="True" DataField="LateCount" Align="Center" DataCellServerTemplateId="LateCountTemplate" />
                                    <ComponentArt:GridColumn HeadingText="早退" AllowSorting="True" DataField="LeaveEarlyCount" Align="Center" DataCellServerTemplateId="LeaveEarlyCountTemplate"/>
                                    <ComponentArt:GridColumn HeadingText="旷工" AllowSorting="True" DataField="AbsentDays" DataCellServerTemplateId="AbsentDaysTemplate" Align="Center"/> 
                                    <ComponentArt:GridColumn HeadingText="病假" AllowSorting="True" DataField="SickLeaveHours" Align="Center" DataCellServerTemplateId="SickLeaveHoursTemplate"/>
                                    <ComponentArt:GridColumn HeadingText="事假" AllowSorting="True" DataField="AffairLeaveHours" Align="Center" DataCellServerTemplateId="AffairLeaveHoursTemplate"/>   
                                    <ComponentArt:GridColumn HeadingText="年假" AllowSorting="True" DataField="AnnualLeaveDays" DataCellServerTemplateId="AnnualLeaveDaysTemplate" Align="Center"/>                                   
                                    <ComponentArt:GridColumn HeadingText="产假" AllowSorting="True" DataField="MaternityLeaveHours" DataCellServerTemplateId="MaternityLeaveHoursTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="婚假" AllowSorting="True" DataField="MarriageLeaveHours" DataCellServerTemplateId="MarriageLeaveHoursTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="丧假" AllowSorting="True" DataField="FuneralLeaveHours" DataCellServerTemplateId="FuneralLeaveHoursTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="出差" AllowSorting="True" DataField="EvectionDays" DataCellServerTemplateId="EvectionDaysTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="外出" AllowSorting="True" DataField="EgressHours" Align="Center" DataCellServerTemplateId="EgressHoursTemplate"/>
                                    <ComponentArt:GridColumn HeadingText="调休" AllowSorting="True" DataField="OffTuneHours" DataCellServerTemplateId="OffTuneHoursTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="产检" AllowSorting="True" DataField="PrenatalCheckHours" DataCellServerTemplateId="PrenatalCheckHoursTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="未提交" AllowSorting="True" DataField="monthData" DataCellServerTemplateId="WTBTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="未审批" AllowSorting="True" DataField="monthData" DataCellServerTemplateId="unauditTemplate" Align="Center"/>
                                    <ComponentArt:GridColumn HeadingText="月历" AllowSorting="True" DataField="monthData" DataCellServerTemplateId="MonthTemplate" Align="Center"/>
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels> 
                        <ClientTemplates> 
                            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);background-repeat: repeat-x;">
                                            <span style="color: White; font-weight: bold;">月审核人员列表</span>
                                        </td>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg);background-repeat: repeat-x;"></td>
                                        <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;</td>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg);background-repeat: repeat-x;"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="checkTemplate">
                                <input type="checkbox" id="chkAudit" name="chkAudit" value="## DataItem.GetMember('ID').Value ##" />
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="ClientTemplate3" >
                            <table border="0" cellpadding="0"  cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left"><input type="checkbox" id="chkAll" onclick="checkedAll();"/>全选</td>                                     
                                    </tr>
                                </table>    
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="auditTemplate">
                                ## getType(DataItem.GetMember('ID').Value); ##
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        
                        <ServerTemplates>
                            <ComponentArt:GridServerTemplate ID="LateCountTemplate">
                                <Template>
                                    <%# GetTimeInfo2(Container.DataItem["LateCount"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="LeaveEarlyCountTemplate">
                                <Template>
                                    <%# GetTimeInfo2(Container.DataItem["LeaveEarlyCount"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="SickLeaveHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo2(Container.DataItem["SickLeaveHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="AffairLeaveHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo2(Container.DataItem["AffairLeaveHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="EgressHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo2(Container.DataItem["EgressHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="DeductSumTemplate">
                                <Template>
                                    <%# GetAttendanceType(Container.DataItem["AttendanceSubType"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="AbsentDaysTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["AbsentDays"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="AnnualLeaveDaysTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["AnnualLeaveDays"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="MaternityLeaveHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["MaternityLeaveHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="MarriageLeaveHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["MarriageLeaveHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="FuneralLeaveHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["FuneralLeaveHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="EvectionDaysTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["EvectionDays"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="OffTuneHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["OffTuneHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                                                        <ComponentArt:GridServerTemplate ID="IncentiveHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo(Container.DataItem["IncentiveHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                                                        <ComponentArt:GridServerTemplate ID="PrenatalCheckHoursTemplate">
                                <Template>
                                    <%# GetTimeInfo2(Container.DataItem["PrenatalCheckHours"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="WTBTemplate">
                                <Template>
                                    <%# GetWTBday(Container.DataItem["monthData"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                             <ComponentArt:GridServerTemplate ID="unauditTemplate">
                                <Template>
                                    <%# GetUnauditday(Container.DataItem["monthData"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                            <ComponentArt:GridServerTemplate ID="MonthTemplate">
                                <Template>
                                    <a target="_blank" href="/Attendance/TimeSheetMonthView.aspx?sYM=<%#Container.DataItem["monthData"].ToString().Split('#')[0] %>&userid=<%#Container.DataItem["monthData"].ToString().Split('#')[1] %>">查看</a>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                        </ServerTemplates>
                    </ComponentArt:Grid>
                </td>
              </tr>
                <tr>
                    <td>
                        <br />
                        <asp:ImageButton ID="btnAudit" runat="server" OnClientClick="return submitMatter(DataItem, 1);"
                            OnClick="btnAudit_Click" ImageUrl="~/images/apppass.jpg"/>
                        <input type="hidden" id="hidMatter" value="" runat="server" /><input type="hidden"
                            id="hidType" value="" runat="server" />
                        <asp:ImageButton ID="btnOverrule" runat="server" OnClientClick="return submitMatter(DataItem, 2);" Visible="false"
                            OnClick="btnOverrule_Click" ImageUrl="~/images/appOverrule.jpg" />
                    </td>
                </tr>
            </table>
          </td>
        </tr>
      </table>    
</asp:Content>

