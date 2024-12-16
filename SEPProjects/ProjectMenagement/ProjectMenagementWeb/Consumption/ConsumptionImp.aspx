<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ConsumptionImp.aspx.cs" Inherits="FinanceWeb.Consumption.ConsumptionImp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4"><asp:Label runat="server" ID="lblTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">消耗数据文件:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:FileUpload ID="fileId" runat="server" Width="40%" />
                <br />
                <font color="gray">消耗模板下载</font>&nbsp;&nbsp;<a href="/tmp/PurchaseBatch/ConsumptionTemplate.xlsx" target="_blank"><img src="/images/ico_04.gif" alt="消耗模板下载" /></a>
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
              <td class="oddrow" style="width: 15%">&nbsp;
            </td>
            <td class="oddrow-l" colspan="3">
                 <asp:Label runat="server" ID="lblResult" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color:white;" colspan="4" align="center">
                <asp:Button ID="btnImport" runat="server" Text=" 导入消耗 " OnClick="btnImport_Click" OnClientClick="  showLoading();" CssClass="widebuttons" />&nbsp;&nbsp;
                 <asp:Button ID="btnCommit" runat="server" Text=" 提交数据 " OnClick="btnCommit_Click" OnClientClick="  showLoading();" CssClass="widebuttons" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" align="center">异常消耗数据
            </td>
        </tr>
    </table>
    <asp:GridView ID="GvError" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvError_RowDataBound"
        DataKeyNames="Id" Width="100%">
        <Columns>
             <asp:BoundField DataField="RowNo" HeaderText="行号" ItemStyle-HorizontalAlign="Center" 
                ItemStyle-Width="5%" />
            <asp:BoundField DataField="OrderYM" HeaderText="年月" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="20%" />
            <asp:BoundField DataField="Amount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="Media" HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="OrderType" HeaderText="类别" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="ErrorContent" HeaderText="错误提示" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Red"
                ItemStyle-Width="15%" />
        </Columns>
    </asp:GridView>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" align="center">验证通过的消耗数据
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvImp" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  OnRowDataBound="gvImp_RowDataBound" Width="100%">
        <Columns>
            <asp:BoundField DataField="RowNo" HeaderText="行号" ItemStyle-HorizontalAlign="Center" 
                ItemStyle-Width="5%" />
            <asp:BoundField DataField="OrderYM" HeaderText="年月" ItemStyle-HorizontalAlign="Center" 
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="25%" />
            <asp:BoundField DataField="Amount" HeaderText="金额" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="10%" />
            <asp:BoundField DataField="Media" HeaderText="媒体主体" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="20%" />
            <asp:BoundField DataField="OrderType" HeaderText="类别" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
        </Columns>
    </asp:GridView>

</asp:Content>
