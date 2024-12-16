<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" Inherits="Monitor_ProjectMonitorList" Codebehind="ProjectMonitorList.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        $().ready(function() {
            $("#<%=ddlBranch.ClientID %>").empty();
            Monitor_ProjectMonitorList.GetBranchList(initBranch);
            function initBranch(r) {
                if (r.value != null) {
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidBranchID.ClientID %>").val()) {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
                }
            }
        });

        function selectBranch(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBranchID.ClientID %>").value = "";
                document.getElementById("<% =hidBranchName.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidBranchID.ClientID %>").value = id;
                document.getElementById("<% =hidBranchName.ClientID %>").value = text;
            }
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                               <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        检索
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        关键字:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtKey" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        登记区间:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />--
                                        <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                            onclick="setDate(this);" />
                                    </td>
                                </tr>
                                <tr>
                                  <td class="oddrow" style="width: 15%">
                                        公司选择:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:DropDownList runat="server" ID="ddlBranch" style="width:auto"></asp:DropDownList>
                                        <input type="hidden" id="hidBranchID" runat="server" />
                                          <input type="hidden" id="hidBranchName" runat="server" />
                                    </td>
                                       <td class="oddrow" style="width: 15%"></td>
                                       <td class="oddrow-l" style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons"
                                            OnClick="btnSearchAll_Click" />
                                    </td>
                                </tr>
                            </table>  
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                            padding-top: 4px;">
                            <table width="100%">
                                <tr>
                                    <td class="heading" colspan="4">
                                        项目号列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
                                            OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvG_RowDataBound"
                                            PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Step" HeaderText="Step" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SerialCode" HeaderText="流水号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="7%" />
                                                <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="10%" />
                                                <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" >
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("ApplicantEmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="GroupName" HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"/>
                                                <asp:BoundField DataField="SubmitDate" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="BusinessTypeName" HeaderText="业务类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"/>
                                                <%--<asp:BoundField DataField="ProjectTypeName" HeaderText="项目类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"/>--%>
                                                 <asp:TemplateField HeaderText="总金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labTotal" Text='<%# Eval("TotalAmount") == null ? "0.00" : Convert.ToDecimal(Eval("TotalAmount")).ToString("#,##0.00") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ContractStatusName" HeaderText="合同状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"/>
                                                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labState" Text='<%#Eval("Status") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <a target="_blank" href="/project/ProjectDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>">
                                                            <img src="../images/dc.gif" border="0px;" title="查看"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="审批状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <a href="/project/ProjectWorkFlow.aspx?Type=project&FlowID=<%#Eval("ProjectID") %>"   target="_blank">
                                                            <img src="/images/AuditStatus.gif" border="0px;" title="审批状态"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印预览" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href='/project/ProjectPrint.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>'
                                                            target="_blank">
                                                            <img title="项目号申请单打印预览" src="/images/ProjectPrint.gif" border="0px;" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="成本" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <a href='/CostView/SinglePrjView.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%#DataBinder.Eval(Container.DataItem,"ProjectID")%>'
                                                            target="_blank">
                                                            <img title="项目号申请单" src="/images/dc.gif" border="0px;" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
