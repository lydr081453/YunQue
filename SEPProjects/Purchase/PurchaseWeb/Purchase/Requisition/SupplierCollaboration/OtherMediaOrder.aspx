<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="OtherMediaOrder.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.SupplierCollaboration.OtherMediaOrder" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../../public/js/DatePicker.js"></script>
    <script language="javascript">
        function MoreMediaClick() {
            var orderid = document.getElementById("<%=hidOrderID.ClientID %>");
            var win = window.open('OtherMediaProductsReference.aspx?type=backup&&orderID=' + orderid.value + '&&GeneralID=<%=Request[RequestName.GeneralID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script><div id="ad" runat="server"></div>
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
                                    <td class="oddrow" style="width: 15%"><asp:HiddenField ID="hidOrderID" runat="server" />
                                        ��Ŀ���:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label ID="lblProNO" runat="server"></asp:Label>
                                    </td>
                                    <td class="oddrow" style="width: 15%">
                                        �ͻ�����:
                                    </td>
                                    <td class="oddrow-l" style="width: 35%">
                                        <asp:Label ID="lblCusName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center">
                                        <asp:Button ID="btnSearch" runat="server" Text="���ý����ϸ" CssClass="widebuttons" OnClientClick="MoreMediaClick();return false;" />&nbsp;<asp:LinkButton ID="btnRefsh" runat="server" OnClick="btnRefsh_Click"></asp:LinkButton>
                                        <asp:Button ID="btnSave" runat="server" Text="  ��  ��  " ValidationGroup="Save" CssClass="widebuttons" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater ID="rptItems" runat="server" onitemcommand="rptItems_ItemCommand" 
                                onitemdatabound="rptItems_ItemDataBound">
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="1" border="1">
                                        <tr>
                                            <td width="20%" height="25px" class="heading" style="text-align:center">ý������</td>
                                            <td width="8%" height="25px" class="heading" style="text-align:center">����</td>
                                            <td width="12%" height="25px" class="heading" style="text-align:center">����</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">��λ</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">����ʱ��</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">����</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">����</td>
                                            <td width="10%" height="25px" class="heading" style="text-align:center">��λ</td>
                                            <td width="5%" height="25px" class="heading" style="text-align:center">��ϸ</td>
                                            <td width="5%" height="25px" class="heading" style="text-align:center">ɾ��</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="10">
                                            <table width="100%">
                                                <tr>
                                                    <td width="20%" align="center">
                                                        <asp:Label ID="lblMediaName" runat="server" Width="100%" Text='<%# Eval("MediaName") %>'></asp:Label>
                                                        <asp:HiddenField ID="hidID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:HiddenField ID="hidMediaID" runat="server" Value='<%# Eval("MediaID") %>' />
                                                    </td>
                                                    <td width="8%" align="center">
                                                        <asp:Label ID="txtMediaArea" runat="server" Width="100%" Text='<%# Eval("MediaArea") %>'></asp:Label>
                                                    </td>
                                                    <td width="12%" align="center">
                                                        <asp:TextBox ID="txtTitle" runat="server" Width="100%" Text='<%# Eval("Title") %>'></asp:TextBox>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:TextBox ID="txtOfSpace" runat="server" Width="100%" Text='<%# Eval("OfSpace") %>'></asp:TextBox>                                            
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="100%" 
                                                        onclick="popUpCalendar(this, this, 'yyyy-mm-dd');" Text='<%# Eval("StartDate") %>'></asp:TextBox>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:TextBox ID="txtWordCount" Width="75%" runat="server" Text='<%# Eval("WordsCount") %>'></asp:TextBox><asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtWordCount" ErrorMessage="����" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:TextBox ID="txtPrice" runat="server" Width="75%" Text='<%# Eval("Price") %>'></asp:TextBox><asp:RangeValidator ID="RangeValidator2" ControlToValidate="txtPrice" ErrorMessage="����" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator>
                                                    </td>
                                                    <td width="10%" align="center">
                                                        <asp:TextBox ID="txtUnit" runat="server" Width="100%" Text='<%# Eval("Unit") %>'></asp:TextBox>
                                                    </td>
                                                    <td width="5%" align="center">
                                                        <asp:ImageButton ID="imgbtnDetails" runat="server" ImageUrl="~/images/edit.gif" ToolTip="��ϸ����" CommandName="Show" /><asp:HiddenField ID="hidIsShow" runat="server" Value="false" />
                                                    </td>
                                                    <td width="5%" align="center">
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/disable.gif" ToolTip="ɾ��" CommandName="deleteItem" CommandArgument='<%# Eval("ID") %>' /><asp:HiddenField ID="hidIndexID" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                                    <div id="divDetails" visible="false" runat="server">
                                                        <table width="100%" class="tableForm">
                                                            <tr>
                                                                <td class="oddrow">ͼƬ�ߴ�</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtPicSize" runat="server" Text='<%# Eval("PicSize") %>'></asp:TextBox></td>
                                                                <td class="oddrow">����ߴ�</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtSpaceSize" runat="server" Text='<%# Eval("LayoutSize") %>'></asp:TextBox></td>
                                                                <td class="oddrow">�Ƿ���ͼ</td>
                                                                <td class="oddrow-l">
                                                                    <asp:RadioButton ID="rdoYes" runat="server" GroupName="pic" Text="��" />
                                                                    <asp:RadioButton ID="rdoNo" runat="server" GroupName="pic" Text="��" />
                                                                    <asp:HiddenField ID="hidYN" runat="server" Value='<%# Eval("IsAccessories") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="oddrow">ɫ��</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtColor" runat="server" Text='<%# Eval("Color") %>'></asp:TextBox></td>
                                                                <%--<td class="oddrow">����</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:TextBox></td>--%>
                                                                <td class="oddrow">�ۿ�</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtDiscount" runat="server" Text='<%# Eval("Discount") %>'></asp:TextBox><asp:RangeValidator ID="RangeValidator3" ControlToValidate="txtDiscount" ErrorMessage="����" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator></td>
                                                                <td class="oddrow">��������</td>
                                                                <td class="oddrow-l"><asp:TextBox ID="txtOtherFee" runat="server" Text='<%# Eval("OtherFees") %>'></asp:TextBox><asp:RangeValidator ID="RangeValidator4" ControlToValidate="txtOtherFee" ErrorMessage="����" ValidationGroup="Save" Type="Double"  runat="server"></asp:RangeValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="oddrow">��ע</td>
                                                                <td colspan="5" class="oddrow-l"><asp:TextBox ID="txtDescription" TextMode="MultiLine" Width="300px" runat="server" Text='<%# Eval("Description") %>'></asp:TextBox></td>

                                                            </tr>
                                                        </table>
                                                    </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>