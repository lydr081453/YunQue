<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="OtherMediaDetailAddOrEdit.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.SupplierCollaboration.OtherMediaDetailAddOrEdit" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../../public/js/DatePicker.js"></script>
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        区域:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtArea" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        稿件类型:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                    <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        新闻报价:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtNewsPrice" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        价格单位:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtUnit" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        版面:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtLayout" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        标题价格:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtTitlePrice" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        折扣:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtDiscount" runat="server" style="text-align:right"></asp:TextBox>%
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        备注:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtDesc" TextMode="MultiLine" runat="server" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        期望价格:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:TextBox ID="txtHopePrice" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        包含配图:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:RadioButton ID="rdoHavePicY" runat="server" Text="是" GroupName="pic" />
                                        <asp:RadioButton ID="rdoHavePicN" runat="server" Text="否" GroupName="pic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 15%">
                                        备注:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtShunYaDesc" TextMode="MultiLine" runat="server" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="  保  存  " CssClass="widebuttons" OnClick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="Button1" runat="server" Text="  返  回  " CssClass="widebuttons" OnClientClick="window.close();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>