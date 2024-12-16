<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Title="用户列表" Inherits="logManage_LogUserList" Codebehind="LogUserList.aspx.cs" %>

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
           function closereturn()
        {
          hide = document.getElementById("<% =hidChkID.ClientID %>");
         var bb= hide.value.split(",")
         opener.document.getElementById("<% =hidChkID.ClientID %>").value=bb[0];  
          if(hide.value=="0")  
             {
                 bb[1]="";
             }  
          opener.document.getElementById("<% =txtOperateName.ClientID %>").value=bb[1];
          window.parent.close();   
   
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <input type="hidden" value="" runat="server" id="txtOperateName" />
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="oddrow-l">
                            请输入用户编号或名称：
                            <asp:TextBox ID="txtInput" runat="server"></asp:TextBox>
                            <asp:Button ID="btnFind" runat="server" Text="查找" CssClass="widebuttons"
                                OnClick="btnFind_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td class="headinglist" colspan="4">
                            用户列表
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <cc4:MyGridView ID="dgEmployeeList" runat="server" OnRowDataBound="dgEmployeeList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="btnSel" runat="server" Text="确定" CssClass="widebuttons"
                                OnClientClick="closereturn();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
