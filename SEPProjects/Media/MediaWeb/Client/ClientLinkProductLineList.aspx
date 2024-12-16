<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Client_ClientLinkProductLineList" Codebehind="ClientLinkProductLineList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">

    function selected(obj)
        {
            if (obj.checked)
            {
                hide = document.getElementById("<% =hidChecked.ClientID %>");
                str = obj.value + ",";
                hide.value = hide.value.replace(str,"");
                hide.value += str;
            }
        }
       function check(){
            hide = document.getElementById("<% =hidChecked.ClientID %>");
               if(hide.value=="0")
               {
                  alert("请选择产品线！");
                  return false;
               }
       }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidChecked" runat="server" value="0" />
    <table border="1" width="100%" class="tableForm">
        <tr>
            <td>
                <table border="1" width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            产品线名称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtProductLineName" runat="server" Width="60%"></asp:TextBox>
                        </td>
                        <td style="width: 20%" class="oddrow">
                            描述中的关键字：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtDesKey" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSearch" runat="server" CssClass="widebuttons" Text="查找" CausesValidation="true">
                            </asp:Button>
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true"
                                OnClick="btnClear_OnClick" />
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
                <table border="0" width="100%">
                    <tr>
                        <td class="headinglist" colspan="4">
                            产品线列表
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" DataKeyNames="ProductLineID">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                                class="widebuttons" />
                            <asp:Button ID="btnAdd" runat="server" CssClass="widebuttons" Text="添加产品线" CausesValidation="true"
                                OnClick="btnAdd_Click"></asp:Button>
                            <asp:Button ID="btnLink" runat="server" Text="关联到客户" CssClass="widebuttons" OnClick="btnLink_Click"
                                OnClientClick="return check();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
