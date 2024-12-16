<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="System_IntegralDisplay" Title="积分浏览" Codebehind="IntegralDisplay.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">

 <table width="100%" border="0">
        <tr>
            <td class="headinglist" style="border: 0">
                已配置积分
            </td>
        </tr>
        <tr>
            <td style="width: 100%; border: 0">
                <asp:GridView ID="gvList" runat="server" OnRowDataBound="gvList_RowDataBound" OnSorting="gvList_Sorting"
                    Width="100%" DataKeyNames="id" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="altname" HeaderText="操作名称" />
                        <asp:TemplateField HeaderText="积分">
                            <ItemTemplate>
                                <asp:label ID="txtIntegral" Text='<%# Eval("Integral") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>