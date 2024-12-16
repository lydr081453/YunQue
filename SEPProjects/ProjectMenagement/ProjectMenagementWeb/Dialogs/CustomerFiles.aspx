<%@ Page Language="C#" AutoEventWireup="true" Inherits="Dialogs_CustomerFiles" Codebehind="CustomerFiles.aspx.cs" %>

<%@ Register Assembly="ExtExtenders" Namespace="ExtExtenders" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="frmMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                客户附件
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 15%">
                附件上传：
            </td>
            <td class="oddrow-l">
                <asp:FileUpload ID="fileupContract" runat="server" Width="60%" /> <asp:Button ID="btnSaveContract" runat="server" ValidationGroup="Save" Text="保存新附件" OnClick="btnSaveContract_Click" />
            </td>
        </tr>
        <tr id="trDes" runat="server">
            <td class="oddrow">
                附件描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtContractDescription" runat="server" Width="60%" TextMode ="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
        
        <asp:GridView ID="gvCustomerFiles" Width="100%" runat="server" AutoGenerateColumns="false" DataKeyNames="AttachID" OnRowDataBound="gvCustomerFiles_RowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="附件名称">
                    <ItemTemplate>
                        <asp:HyperLink ID="hypName" runat="server" Text='<%# Eval("Attachment") %>'></asp:HyperLink>
                        <asp:HiddenField ID="hidId" runat="server" Value='<%# Eval("AttachID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="70%" HeaderText="描述">
                    <ItemTemplate><asp:Label ID="lblDes" runat="server" Text='<%# Eval("Description") %>' ToolTip='<%# Eval("Description") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/disable.gif" OnClick="btnDelete_Click" ToolTip="删除" />
                        <act:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="btnDelete"
                            ConfirmText="您是否要删除此附件?">
                        </act:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
            <HeaderStyle CssClass="DataGridFixedHeader" />
            <RowStyle  height="27" CssClass="f_gray_tw_GVL" />
        </asp:GridView>
    <table width="100%" class="tableForm">
    <tr><td align="center">
        </td></tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="widebuttons" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
