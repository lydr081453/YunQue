<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_Print_supplierPrint" Codebehind="supplierPrint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>打印供应商</title>
    <style type="text/css">
        .white_font
        {
            font-size: 12px;
            color: #FFF;
            text-decoration: none;
            vertical-align: text-top;
            padding-top: 5px;
            background-repeat: no-repeat;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="tableForm" width="100%">
        <asp:Repeater runat="server" ID="repSupplier">
            <HeaderTemplate>
                <tr class="Gheading" style="font-size: 15px; font-weight: bold">
                    <td align="center" style="width: 15%">
                        供应商名称
                    </td>
                    <td align="center" style="width: 10%">
                        所属地区
                    </td>
                    <td align="center" style="width: 10%">
                        联系人
                    </td>
                    <td align="center" style="width: 10%">
                        手机
                    </td>
                    <td align="center" style="width: 10%">
                        联系电话
                    </td>
                    <td align="center" style="width: 10%">
                        传真
                    </td>
                    <td align="center" style="width: 15%">
                        电子邮件
                    </td>
                    <td align="center" style="width: 10%">
                        物料类别
                    </td>
                    <td align="center" style="width: 10%">
                        框架协议号
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="td">
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("supplier_name") %>
                    </td>
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("supplier_area") %>
                    </td>
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("contact_name" ) %>
                    </td>
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("contact_mobile") %>
                    </td>
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("contact_tel").ToString().Replace("---", "").Replace("--", "")%>
                    </td>
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("contact_fax").ToString().Replace("---","").Replace("--","") %>
                    </td>
                    <td style="word-break: break-all;padding-left:5px; padding-right:5px">
                        <%# Eval("contact_email") %>
                    </td>
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("productTypes") %>
                    </td>
                    <td style="padding-left:5px; padding-right:5px">
                        <%# Eval("supplier_frameNO") %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr class="noprint">
            <td height="20" align="center" valign="bottom" class="white_font">
                &nbsp;
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnPrint" runat="server" align="center" valign="bottom" background="images/btnbgimg.gif"
                class="white_font">
                <a style="cursor: pointer" onclick="javascript:window.print();">打印</a>
            </td>
            <td width="1">
                <img src="images/space.gif" width="1" height="1" />
            </td>
            <td width="50" id="btnClose" runat="server" align="center" valign="bottom" background="images/btnbgimg.gif"
                class="white_font" onclick="javascript:window.close();" style="cursor: pointer">
                关闭
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
