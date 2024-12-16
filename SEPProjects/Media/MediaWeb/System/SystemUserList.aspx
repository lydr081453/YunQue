<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="System_SystemUserList" Codebehind="SystemUserList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
  
      function selectone(obj)
        {
            if (obj.checked)
            {
               hide = document.getElementById("<% =hidChkID.ClientID %>");
                 str = obj.value ;
                hide.value = hide.value.replace(str,"");
                hide.value = str;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            查找条件</td>
                    </tr>
                    <tr>
                        <td colspan="4" class="oddrow-l">
                            请输入名称：
                            <asp:TextBox ID="txtInput" runat="server"></asp:TextBox>   
                            <asp:Button ID="Button2" runat="server" Text="查找" Width="83px" CssClass="widebuttons"
                                OnClick="btnFind_Click" />
                            <asp:Button ID="Button3" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />                         
                        </td>                        
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr>
        <td style="height:30px"></td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td class="headinglist" colspan="4">
                            用户列表</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <cc4:MyGridView ID="dgEmployeeList"  runat="server" OnRowDataBound="dgEmployeeList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <input type="hidden" value="" runat="server" id="hidChkID" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
