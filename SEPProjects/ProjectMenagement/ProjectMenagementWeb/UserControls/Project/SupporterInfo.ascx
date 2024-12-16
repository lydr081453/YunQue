<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_SupporterInfo"
    CodeBehind="SupporterInfo.ascx.cs" %>

<script type="text/javascript" src="/public/js/DatePicker.js"></script>

<script type="text/javascript">
    function setDate(obj) {
        popUpCalendar(obj, obj, 'yyyy-mm-dd');
    }
    function SupportCostDetailClick() {
        window.__doPostBack = __doPostBack;
        var win = window.open('/Dialogs/CostSupportDetailDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.SupportID %>=<%=Request[ESP.Finance.Utility.RequestName.SupportID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function CreateDynamic() {
        var date2 = document.getElementById("<% =txtBeginDate.ClientID %>").value;
        var date1 = document.getElementById("<% =txtEndDate.ClientID %>").value;
        var ProDate1 = document.getElementById("<%=hidBeginDate.ClientID %>").value;
        var ProDate2 = document.getElementById("<%=hidEndDate.ClientID %>").value;
        if (date1 < date2) {
            return -1;
        }
        if (date2 < ProDate1) {
            return -2;
        }
        if (date1 > ProDate2) {
            return -2;
        }
        var year = parseFloat(date1.substr(0, 4)) - parseFloat(date2.substr(0, 4));
        var month = parseFloat(date1.substr(5, 2)) - parseFloat(date2.substr(5, 2)) + parseFloat("1");
        var differ = 0;

        differ = year * 12 + month;
        return differ;


    }
    function selectPercent() {
        var monthAmount = CreateDynamic();
        var date2 = document.getElementById("<% =txtBeginDate.ClientID %>").value;
        var year = date2.substr(0, 4);
        var month = date2.substr(5, 2);
        if (monthAmount == "undefined" || monthAmount == "" || monthAmount == -1) {
            alert("请输入正确的项目起始日期和结束日期.");
            return false;
        }
        if (monthAmount == "undefined" || monthAmount == "" || monthAmount == -2) {
            alert("支持方的日期不得早于或超出项目日期.");
            return false;
        }
        var win = window.open('/Dialogs/PercentForSupporterDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.SupportID %>=<%=Request[ESP.Finance.Utility.RequestName.SupportID] %>&<% =ESP.Finance.Utility.RequestName.Percent %>=' + monthAmount + '&<% =ESP.Finance.Utility.RequestName.BeginYear %>=' + year + '&<% =ESP.Finance.Utility.RequestName.BeginMonth %>=' + month, null, 'height=600px,width=800px,scrollbars=yes,top=400px,left=400px');
        win.resizeTo(screen.availWidth * 0.4, screen.availHeight * 0.4);
    }

    function selectMember() {
        var win = window.open('/Dialogs/EmployeeList.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.SupportID %>=<%=Request[ESP.Finance.Utility.RequestName.SupportID] %>&<% =ESP.Finance.Utility.RequestName.SearchType %>=SupporterMember&<% =ESP.Finance.Utility.RequestName.DeptID %>=<%=GetGroupID %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function ApplicantClick() {
        var dept = document.getElementById("<% =hidGroupID.ClientID %>").value;
        var win = window.open('/Dialogs/EmployeeList.aspx?<% =ESP.Finance.Utility.RequestName.SearchType %>=SupporterApplicant&<% =ESP.Finance.Utility.RequestName.DeptID %>=' + dept, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
</script>

<script language="javascript">
    function setDate(obj) {
        popUpCalendar(obj, obj, 'yyyy-mm-dd');
    }
</script>

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            支持方申请
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            所支持之项目号:
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="lblPrjCode" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            主项目号组别:
        </td>
        <td class="oddrow-l" style="width: 35%">
        <input type="hidden" runat="server" id="hidGroupID"/>
            <asp:Label ID="lblGroup" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            由客户支付之税金:
        </td>
        <td class="oddrow-l">
            <asp:TextBox runat="server" ID="txtBilledTax" Text="0.00"></asp:TextBox>
        </td>
        <td class="oddrow">
            项目经理:
        </td>
        <td class="oddrow-l">
        <input type="hidden"  runat="server" id="hidLeaderID"/>
         <input type="hidden"  runat="server" id="hidOldLeaderID"/>
            <asp:Label ID="lblLeaderEmployeeName" runat="server" CssClass="userLabel"></asp:Label><input type="button"
                id="btnApplicant" onclick="return ApplicantClick();" class="widebuttons" value="变更" />
        </td>
    </tr>
    <tr>
     <td class="oddrow" style="width: 15%">
            支持方组别:
        </td>
         <td class="oddrow-l">
            <asp:Label ID="lblSupportGroup" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            服务费收入:
        </td>
        <td class="oddrow-l">
            <asp:TextBox runat="server" ID="txtIncomeFee" Text="0.00"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="oddrow">
            业务总额:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblBudgetAllocation" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            服务类型:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblServiceType" runat="server"></asp:Label>
        </td>
    </tr>
     <tr>
        <td class="oddrow">
            不含增值税金额:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTotalNoVAT" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            附加税:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTaxFee" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            业务描述:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="lblServiceDescription" Width="80%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            支持方组成员&nbsp;&nbsp;<%--<img align="absbottom" src="/images/a3_07.jpg" onclick="return selectMember();"
                style="cursor: hand" alt="添加项目成员" />--%><asp:Button ID="btnAddMember" runat="server"
                    OnClientClick="return selectMember();" Text="添加支持方组成员" CssClass="widebuttons">
            </asp:Button>
            <asp:LinkButton runat='server' ID="btnMember" OnClick="btnMember_Click" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="4">
            <input type="hidden" id="hidMembers" runat="server" />
            <asp:GridView ID="gvMember" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportMemberId"
                OnRowDataBound="gvMember_RowDataBound" OnRowCommand="gvMember_RowCommand" Width="100%">
                <Columns>
                    <asp:BoundField DataField="SupportMemberId" HeaderText="SupportMemberId" ItemStyle-HorizontalAlign="Center"
                        Visible="false" />
                    <asp:BoundField DataField="MemberUserID" HeaderText="系统ID" ItemStyle-HorizontalAlign="Center"
                        Visible="false" />
                    <asp:TemplateField HeaderText="成员姓名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("MemberEmployeeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MemberCode" HeaderText="成员编号" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="15%" />
                    <asp:BoundField DataField="MemberUserName" HeaderText="成员帐号" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="10%" />
                    <asp:BoundField DataField="GroupID" HeaderText="组ID" ItemStyle-HorizontalAlign="Center"
                        Visible="false" />
                    <asp:BoundField DataField="GroupName" HeaderText="业务组" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="10%" />
                    <asp:BoundField DataField="MemberEmail" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="15%" />
                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderText="电话">
                        <ItemTemplate>
                            <asp:Label ID="lblMemberPhone" runat="server" Text='<%# Eval("MemberPhone")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("SupportMemberId") %>'
                                CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                OnClientClick="return confirm('你确定删除吗？');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Visible="false" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            预计完成百分比
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            项目起始日期:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblBeginDate" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            项目结束日期:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            支持方起始日期:
        </td>
        <td class="oddrow-l">
            <input type="hidden" runat="server" id="hidBeginDate" /><input type="hidden" runat="server"
                id="hidEndDate" />
            <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                onclick="setDate(this);" />&nbsp;<font color="red">*</font>
        </td>
        <td class="oddrow">
            支持方预计结束日期:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                onclick="setDate(this);" />&nbsp;<font color="red">*</font>
        </td>
    </tr>
    <%--成本明细--%>
    <tr>
        <td class="heading" colspan="4">
            成本明细&nbsp;&nbsp;<%--<img id="imgCostDetail" align="baseline" src="/images/a3_07.jpg" alt="添加成本明细" onclick="return SupportCostDetailClick();" style="cursor: hand" />--%>
            <asp:Button ID="btnAddCost" runat="server" OnClientClick="return SupportCostDetailClick();"
                Text="添加成本明细" CssClass="widebuttons" />&nbsp;<font color="red">*</font>
            <asp:LinkButton runat='server' ID="btnCost" OnClick="btnCost_Click" />
        </td>
    </tr>
     <tr id="tr1" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">
                        序号
                    </th>
                    <th scope="col">
                        成本描述
                    </th>
                    <th scope="col">
                        成本金额
                    </th>
                        <%-- <th scope="col">
                        已使用金额
                    </th>--%>
                </tr>
                <tr class="td" align="left">
                    <td colspan="3" align="center">
                        <span>暂时没有相应的成本记录</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr2" runat="server" visible="true">
        <td class="oddrow" colspan="4">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportCostId"
                        OnRowCommand="gvCost_RowCommand" Width="100%" OnRowDataBound="gvCost_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40%" />
                            <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <%--  <asp:TemplateField HeaderText="已使用金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsedCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" DataKeyNames="SupporterExpenseID"
                        OnRowCommand="gvExpense_RowCommand" Width="100%" OnRowDataBound="gvExpense_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="费用描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40%" />
                            <asp:TemplateField HeaderText="OOP金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpense" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="已使用金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsedCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="trTotalCost" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 40%; border: 0 0 0 0">
                    </td>
                    <td style="width: 25%; border: 0 0 0 0">
                    </td>
                    <td style="width: 25%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotalCost" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            各月完工百分比<asp:Button ID="btnAddPercent" runat="server" OnClientClick="return selectPercent();"
                Text="编辑各月完成百分比" CssClass="widebuttons" OnClick="btnAddPercent_Click" />&nbsp;<font
                    color="red">*</font>
            <asp:LinkButton runat="server" ID="btnPercent" OnClick="btnPercent_Click" />
        </td>
    </tr>
    <tr id="trNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">
                        序号
                    </th>
                    <th scope="col">
                        年
                    </th>
                    <th scope="col">
                        月
                    </th>
                    <th scope="col">
                        完工百分比(%)
                    </th>
                    <th scope="col">
                        当月Fee
                    </th>
                      <th scope="col">
                        当月Fee(含税)
                    </th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="6" align="center">
                        <span>没有填写预计完工百分比信息</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trGrid" runat="server" visible="true">
        <td colspan="4">
            <asp:GridView ID="gvPercent" Width="100%" runat="server" DataKeyNames="SupporterScheduleID"
                AutoGenerateColumns="false" EmptyDataText="没有填写预计完工百分比信息" OnRowDataBound="gvPercent_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="YearValue" HeaderText="年" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:BoundField DataField="monthValue" HeaderText="月" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="完工百分比(%)" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPercent" runat="server" Text='<%#Eval("MonthPercent") == null ? "0.00" : Convert.ToDecimal(Eval("MonthPercent")).ToString("0.00") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblFee" runat="server" Text='<%#Eval("Fee") == null ? "0.00" : Convert.ToDecimal(Eval("Fee")).ToString("#,##0.00")%>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee(含税)" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblFeeTax" runat="server" Text=''></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 5%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                     <td style="width: 20%; border: 0 0 0 0"></td>
                    <td style="width: 20%; border: 0 0 0 0">
                        <asp:Label ID="lblTotalPercent" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                    <td style="width: 20%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotalFee" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
