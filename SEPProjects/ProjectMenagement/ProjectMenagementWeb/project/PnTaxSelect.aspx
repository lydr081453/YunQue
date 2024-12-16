<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PnTaxSelect.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="FinanceWeb.project.PnTaxSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">

        function selectProject() {
            document.getElementById("tableProject").style.display = "Block";
            document.getElementById("<%=gvProject %>").style.display = "Block";
            document.getElementById("<%=lblReturnProjectCode %>").innerHTML="";
        }
    </script>

    <table id="tableProject" runat="server" width="100%" class="tableForm">
        <tr>
            <td id="tdProject" class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 85%" colspan="3">
                <asp:TextBox ID="txtProjectCode" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSelect" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="false" DataKeyNames="ProjectId" EmptyDataText="暂时没有相关的项目号记录"
        OnRowDataBound="gvProject_RowDataBound" OnRowCommand="gvProject_RowCommand" Width="100%"
        CellPadding="4">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:Button ID="btnAdd" OnClientClick="location.href='#tdReturn';" runat="server" Text="选择" CssClass="widebuttons" CommandName="Add"
                        CommandArgument='<%# Eval("projectId").ToString() == "0" ? Eval("projectCode") : Eval("projectId") %>' />
                        <asp:Button ID="btnAddProject" OnClientClick="location.href='#tdReturn';" runat="server" Text="直接选择" CssClass="widebuttons" CommandName="AddProject"
                        CommandArgument='<%# Eval("projectId").ToString() == "0" ? Eval("projectCode") : Eval("projectId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="serialCode" HeaderText="PA号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="projectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="30%" />
            <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" ItemStyle-Width="30%" />
            <asp:TemplateField HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                <ItemTemplate>
                    <asp:Literal ID="litGroup" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
        <table width="100%" class="tableForm">
         <tr>
            <td id="tdReturn" class="oddrow" style="width: 15%">
                项目号:
            </td>
            <td class="oddrow-l" style="width: 85%" colspan="3">
                <asp:label ID="lblReturnProjectCode" runat="server" />
                <asp:Button ID="btnProjectReSelect" runat="server" Text=" 重新选择项目号 " OnClientClick="location.href='#tdProejct';" OnClick="btnProjectReSelect_OnClick" CssClass="widebuttons"/>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                关键字:
            </td>
            <td class="oddrow-l" style="width: 85%" colspan="3">
                <asp:TextBox ID="txtReturnKey" runat="server" /><asp:Button ID="btnReturnSelect" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnReturnSelect_Click" />
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="gvPN" runat="server" AutoGenerateColumns="false" DataKeyNames="ReturnId" EmptyDataText="暂时没有相关的付款申请记录"
        OnRowDataBound="gvPN_RowDataBound" OnRowCommand="gvPN_RowCommand" Width="100%"
        CellPadding="4">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:Button ID="btnAdd" runat="server" Text="选择" CssClass="widebuttons" CommandName="Add"
                        CommandArgument='<%# Eval("ReturnId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ReturnCode" HeaderText="PN单号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="RequestEmployeename" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:TemplateField HeaderText="付款金额" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmounts"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成本所属组" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCostDept"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SupplierName" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="30%" />
        </Columns>
    </asp:GridView>
    <input type="button" value=" 关闭 " class="widebuttons" onclick="window.close();" />
</asp:Content>
