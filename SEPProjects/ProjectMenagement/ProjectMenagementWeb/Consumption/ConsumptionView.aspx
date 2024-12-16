<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ConsumptionView.aspx.cs" Inherits="FinanceWeb.Consumption.ConsumptionView" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="/public/css/buttonLoading.css" rel="stylesheet" />

     <script type="text/javascript">
       
         function setLoading(button) {
             button.classList.add('loading');
             button.classList.add('disabled');

             setTimeout(function () {
                 button.classList.remove('loading');
                 button.classList.remove('disabled');
             }, 5000);
         }


    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">批次信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%; height: 20px;">批次号: </td>
            <td class="oddrow-l" style="height: 20px">
                <asp:Label runat="server" ID="lblBatchCode"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%; height: 20px;">导入总额: </td>
            <td class="oddrow-l" style="height: 20px">
                <asp:Label runat="server" ID="lblAmount"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">创建日期: </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">创建人: </td>
            <td class="oddrow-l">
                <asp:Label runat="server" ID="lblCreator"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">证明文件: </td>
            <td class="oddrow-l" colspan="3">
                <asp:HyperLink ID="linkProve" Target="_blank" ImageUrl="/images/ico_04.gif" runat="server"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">备注信息: </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblDesc"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">审批日志: </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label runat="server" ID="lblLog"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: white;" colspan="4" align="center">
                 <asp:Button ID="btnExport" runat="server" Text=" 导出 " OnClick="btnExport_Click" CssClass="widebuttons"  OnClientClick="setLoading(this);" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvImp" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%">
        <Columns>
            <asp:BoundField DataField="RowNo" HeaderText="#" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="5%" />
            <asp:BoundField DataField="OrderYM" HeaderText="年月" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="JSCode" HeaderText="JSCode" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="XMCode" HeaderText="XMCode" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="20%" />
            <asp:BoundField DataField="Amount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="Media" HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="OrderType" HeaderText="类别" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
        </Columns>
    </asp:GridView>

</asp:Content>
