<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="System_IntegralConfig" Title="Untitled Page" Codebehind="IntegralConfig.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="Server">

    <script type="text/javascript">
        function check() {

            //判断积分必须是数字
            if (document.getElementById("txtIntegral").value.search(/^\d+$/) == -1) {

                alert("积分必须是数字！");
                document.getElementById("txtIntegral").focus();
                return false;
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%" border="0">
        <tr>
            <td>
                <table class="tablehead">
                    <tr>
                        <td>
                            <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                ID="btnAddReporter" runat="server" class="bigfont" Text="添加积分配置" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
                                <asp:TextBox ID="txtIntegral" Text='<%# Eval("Integral") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="width: 100%" border="0">
        <tr>
            <td align="right">
                <asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" CssClass="widebuttons"
                    />
            </td>
        </tr>
    </table>
</asp:Content>
