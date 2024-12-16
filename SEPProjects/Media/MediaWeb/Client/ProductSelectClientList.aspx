<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Client_ProductSelectClientList"
    Title="客户列表" Codebehind="ProductSelectClientList.aspx.cs" %>

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
                
                hide.value = str;
            }
        }
        
      function closereturn()
        {
          hide = document.getElementById("<% =hidChkID.ClientID %>");
         var bb= hide.value.split(",")
          if(hide.value=="0")  
         {
             bb[0]="";
             bb[1]="";
         }  
         opener.document.getElementById("<% =hidChkID.ClientID %>").value=bb[0];  
          opener.document.getElementById("<% =txtChFullName.ClientID %>").value=bb[1];
          window.parent.close();   
          
   
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <table width="100%">
        <tr>
            <td>
                <table border="1" width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            客户管理
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            客户中文全称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtChFullName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td style="width: 20%" class="oddrow">
                            客户中文简称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtChShortName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            客户英文全称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtEnFullName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td style="width: 20%" class="oddrow">
                            客户英文简称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtEnShortName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSearch" runat="server" CssClass="widebuttons" Text="查找" CausesValidation="true">
                            </asp:Button>
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 30px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%">
                    <tr>
                        <td class="headinglist" colspan="4">
                            客户列表
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting"
                                DataKeyNames="ClientID">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="btnOk" runat="server" CssClass="widebuttons" Text="确定" CausesValidation="true" OnClick="btnOk_Click"></asp:Button>
                            <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Text="返回" OnClick="btnBack_Click" />&nbsp;<asp:Button ID="btnClose" runat="server" Text="关闭" CssClass="widebuttons" Visible="false" OnClientClick="window.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
