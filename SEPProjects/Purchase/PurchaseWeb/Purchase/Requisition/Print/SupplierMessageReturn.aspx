<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierMessageReturn.aspx.cs" 
Inherits="Purchase_Requisition_Print_SupplierMessageReturn" %>--%>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierMessageReturn.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.Print.SupplierMessageReturn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server"></head>
<body>
    <table width="630" border="1"  class="tableForm" align="center" cellpadding="0" cellspacing="0" id="bottomButton" runat="server" style="font-size:20px; margin-top:60px;" >
        <tr>
            <td class="heading" colspan="2">
                回复信息
            </td>
        </tr>
        <tr >
            <td class="oddrow" style="width:100px;padding:5px 5px;">回复标题:</td>
            <td class="oddrow-l"  style="padding:5px 5px;" colspan="3"><asp:Label ID="lbRtitle" runat="server"></asp:Label></td>
        </tr>
        <tr >
            <td class="oddrow" style="width:100px;padding:5px 5px;">回 复 人:</td>
            <td class="oddrow-l" style="padding:5px 5px;"><asp:Label ID="lbRname" runat="server"></asp:Label></td>
            <td class="oddrow" style="width:100px;padding:5px 5px;">接 收 人:</td>
            <td class="oddrow-l" style="padding:5px 5px;"><asp:Label ID="lbCname" runat="server"></asp:Label></td>
        </tr>
        <tr >
            <td class="oddrow" style="width:100px;padding:5px 5px;">回复时间:</td>
            <td class="oddrow-l" style="padding:5px 5px;"><asp:Label ID="lbRtime" runat="server"></asp:Label></td>
            <td class="oddrow" style="width:100px;padding:5px 5px;">系统编号:</td>
            <td class="oddrow-l" style="padding:5px 5px;"><asp:Label ID="lbNo" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="oddrow" style="width:100px;padding:5px 5px;">回复内容:</td>
            <td class="oddrow-l" style="padding:5px 5px;" colspan="3"><asp:Literal ID="lbRinfo" runat="server"></asp:Literal></td>
        </tr>
    </table>
</body>
</html>