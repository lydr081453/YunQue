<%@ Page Language="C#" AutoEventWireup="true" Inherits="Media_ReporterFavoriteList" MasterPageFile="~/MasterPage.master" Codebehind="ReporterFavoriteList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
  
      function selectone(obj)
        {
            if (obj.checked)
            {
               hide = "";
                 str = obj.value ;
                hide.value = hide.value.replace(str,"");
                hide.value = str;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table width="100%">              
        <tr>
            <td>
                <table width="100%" border="1">
                    <tr>
                        <td colspan="4" class="heading">
                            添加收藏：
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0">
                                <tr>
                                    <td colspan="5" class="heading">搜索记者：</td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="oddrow-l">记者名：
                                    <asp:TextBox ID="txtReporter" runat="server" Width="40%"></asp:TextBox></td>
                                    <td colspan="2" class="oddrow-l">所属媒体：<asp:TextBox ID="txtMedia" runat="server" Width="40%"></asp:TextBox></td>
                                    <td class="oddrow"><asp:Button ID="btnSerach" runat="server" Text="搜索" Width="83px" CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
                                </tr>                        
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="保存到收藏" Width="83px" CssClass="widebuttons"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

