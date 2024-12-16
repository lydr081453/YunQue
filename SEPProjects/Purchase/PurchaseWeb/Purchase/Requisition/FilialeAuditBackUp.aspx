<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_FilialeAuditBackUp" MasterPageFile="~/MasterPage.master" Codebehind="FilialeAuditBackUp.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript">        
        function removefocus() {
            document.getElementById("<%=btnSave.ClientID %>").focus();
        }
    </script>
    
<table width="100%" class="tableForm">
    <tr>
        <td class="heading">分公司备用初审人</td>
    </tr>
    <tr>
        <td class="oddrow">当前用户：</td>
        <td class="oddrow-l" colspan="3"><asp:Label ID="labCurrentUser" runat="server" /></td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">启用备用初审人：</td>
        <td class="oddrow-l" style="width:30%"><input type="checkbox" id="chk" runat="server" /></td>        
    </tr>
    <tr>
        <td><asp:Button ID="btnSave" runat="server" Text="保存" CssClass="widebuttons" OnClick="btnSave_Click" />
        </td>
    </tr>
</table>
</asp:Content>
