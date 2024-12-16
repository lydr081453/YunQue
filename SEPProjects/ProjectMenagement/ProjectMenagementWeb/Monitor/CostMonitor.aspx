<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="CostMonitor.aspx.cs" Inherits="FinanceWeb.Monitor.CostMonitor" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript">

        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function ApplicantClick() {
            var username = document.getElementById("<% =txtUser.ClientID %>").value;
            var dept = document.getElementById("<% =hidGroupID.ClientID %>").value;
            username = encodeURIComponent ? encodeURIComponent(username) : escape(username);
            var win = window.open('/Dialogs/EmployeeList.aspx?<% =ESP.Finance.Utility.RequestName.SearchType %>=Monitor&<% =ESP.Finance.Utility.RequestName.UserName %>=' + username + '&<% =ESP.Finance.Utility.RequestName.DeptID %>=' + dept, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
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
                                        员工姓名:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                       <asp:TextBox ID="txtUser" runat="server" onkeyDown="return false; " Style="cursor: hand"></asp:TextBox><input type="button"
                id="btnApplicant" onclick="return ApplicantClick();" class="widebuttons" value="  选择  " />
                    <input type="hidden" id="hidUserId" runat="server" />
                    <input type="hidden" id="hidGroupID" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        申请日期:
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
                                        项目号:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtProject" runat="server" />
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        供应商
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtSupplier" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />&nbsp;
                                         <asp:Button ID="btnSearchAll" runat="server" Text=" 检索全部 " CssClass="widebuttons" OnClick="btnSearchAll_Click" />&nbsp;
                                        <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" OnClick="btnExport_Click" />
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
                                        第三方列表
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                            OnPageIndexChanging="gvG_PageIndexChanging" OnRowDataBound="gvG_RowDataBound"
                                            PageSize="10" EmptyDataText="暂时没有相关记录" PagerSettings-Mode="NumericFirstLast"
                                            AllowPaging="true" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%"/>
                                                 <asp:TemplateField HeaderText="PR单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblPrno" runat="server"></asp:Label>       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Project_Code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="RequestorName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="ReceiverName" HeaderText="收货人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="AppendReceiverName" HeaderText="附加收货" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="ApplicantEmployeeName" HeaderText="项目负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="dept" HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="businessdescription" HeaderText="项目描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="Supplier_Name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="ItemNo" HeaderText="费用描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%"/>
                                                <asp:BoundField DataField="TotalPrice" HeaderText="申请总额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" />
                                                <asp:BoundField DataField="App_Date" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%"  />
                                                <asp:TemplateField HeaderText="单据类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" >
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labPrtype" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单据状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" >
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="labState" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="打印" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblPrint" runat="server"></asp:Label>       
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
