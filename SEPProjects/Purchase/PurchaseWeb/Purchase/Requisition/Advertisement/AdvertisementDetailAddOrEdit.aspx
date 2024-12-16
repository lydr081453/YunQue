<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AdvertisementDetailAddOrEdit.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.Advertisement.AdvertisementDetailAddOrEdit" %>

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
                                        折扣:
                                    </td>
                                    <td class="oddrow-l" style="width: 18%">
                                        <asp:TextBox ID="txtDiscount" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        返点:
                                    </td>
                                    <td class="oddrow-l" style="width: 18%">
                                        <asp:TextBox ID="txtReturn" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        账期:
                                    </td>
                                    <td class="oddrow-l" style="width: 18%">
                                        <asp:TextBox ID="txtPeriod" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        配送下限:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtMin" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow">
                                        配送上限:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtMax" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow">
                                        配送额度:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtPercent" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        配送备注:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtDistributionDescription" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="oddrow">
                                        备注:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox id="txtDesc" runat="server" TextMode="MultiLine" Height="80px" Width="500px"></asp:TextBox>
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