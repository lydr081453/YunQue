<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RebateRegistrationImp.aspx.cs" Inherits="FinanceWeb.RebateRegistration.RebateRegistrationImp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">导入媒体返点数据
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">媒体返点数据文件:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:FileUpload ID="fileId" runat="server" Width="40%" />
                <br />
                <font color="gray">媒体返点模板下载</font>&nbsp;&nbsp;<a href="/tmp/PurchaseBatch/RebateRegistrationTemplate.xlsx" target="_blank"><img src="/images/ico_04.gif" alt="返点模板下载" /></a>
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 15%">证明附件:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:FileUpload ID="fileAtt" runat="server" Width="40%" />
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 15%">备注信息:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtDesc" Width="40%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="background-color:white;" colspan="4" align="center">
                <asp:Button ID="btnImport" runat="server" Text=" 导入媒体返点 " OnClick="btnImport_Click" OnClientClick="  showLoading();" CssClass="widebuttons" />&nbsp;&nbsp;
                 <asp:Button ID="btnCommit" runat="server" Text=" 提交数据 " OnClick="btnCommit_Click" OnClientClick="  showLoading();" CssClass="widebuttons" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" align="center">异常媒体返点数据
            </td>
        </tr>
    </table>
    <asp:GridView ID="GvError" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvError_RowDataBound"
        DataKeyNames="Id" Width="100%">
        <Columns>
             <asp:BoundField DataField="RowNo" HeaderText="#" ItemStyle-HorizontalAlign="Center" 
                ItemStyle-Width="5%" />
            <asp:BoundField DataField="CreditedDate" HeaderText="日期" ItemStyle-Width="5%"  />
            <asp:TemplateField HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" >
                <ItemTemplate>
                    <%#  Eval("Project") == null ? "" : ((ESP.Finance.Entity.ProjectInfo)Eval("Project")).ProjectCode %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" >
                <ItemTemplate>
                    <%#  Eval("Supplier") == null ? "" : ((ESP.Purchase.Entity.SupplierInfo)Eval("Supplier")).supplier_name %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="RebateAmount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="5%" />
                       <asp:BoundField DataField="Remark" HeaderText="返点内容" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="AccountingNum" HeaderText="返点核算信息编号" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="SettleType" HeaderText="结算类型" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="Branch" HeaderText="我方主体名称" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="ErrorContent" HeaderText="错误提示" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Red"
                 />
        </Columns>
    </asp:GridView>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" align="center">验证通过的媒体返点数据
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvImp" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  OnRowDataBound="gvImp_RowDataBound" Width="100%">
        <Columns>
             <asp:BoundField DataField="RowNo" HeaderText="#" ItemStyle-HorizontalAlign="Center" 
                ItemStyle-Width="5%" />
            <asp:BoundField DataField="CreditedDate" HeaderText="日期" ItemStyle-Width="5%"  />
            <asp:TemplateField HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" >
                <ItemTemplate>
                    <%#  Eval("Project") == null ? "" : ((ESP.Finance.Entity.ProjectInfo)Eval("Project")).ProjectCode %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" >
                <ItemTemplate>
                    <%#  Eval("Supplier") == null ? "" : ((ESP.Purchase.Entity.SupplierInfo)Eval("Supplier")).supplier_name %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="RebateAmount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="5%" />
                       <asp:BoundField DataField="Remark" HeaderText="返点内容" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="AccountingNum" HeaderText="返点核算信息编号" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="SettleType" HeaderText="结算类型" ItemStyle-HorizontalAlign="Center"
                 />
            <asp:BoundField DataField="Branch" HeaderText="我方主体名称" ItemStyle-HorizontalAlign="Center"
                 />
        </Columns>
    </asp:GridView>

</asp:Content>
