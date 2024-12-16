<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Client_ProductLineList" Codebehind="ProductLineList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">

  function   WinOpen()   
      {  
      window.open("ProductSelectClientList.aspx?alert=1","选择客户","<%= ESP.Media.Access.Utilities.Global.OpenClass.Common%>");  
      } 
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <table width="100%">
        <tr>
            <td>
                <%--  <table class="tablehead">
             <tr>
              <td>
                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                    ID="btnAddReporter" runat="server" class="bigfont" Text="添加产品线" OnClick="btnAdd_Click" />
                </td>
               </tr>
          </table>--%>
            </td>
        </tr>
        <tr>
            <td>
                <table border="1" width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            产品线名称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtProductLineName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            描述中的关键字：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtDesKey" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            所属客户：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtChFullName" runat="server" Width="120px"></asp:TextBox>
                            <%--<asp:Button ID="btnClient" runat="server" Text="请选择" OnClientClick="WinOpen();" CssClass="widebuttons" />--%>
                        </td>
                        <td class="oddrow" style="width: 20%">
                        </td>
                        <td class="oddrow-l" style="width: 30%">
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
                        <td>
                            <table>
                                <tr>
                                    <td class="headinglist" colspan="4" width="10%" align="left">
                                        产品线列表
                                    </td>
                                    <td width="74%">
                                    </td>
                                    <td  class="tablehead" width="16%" align="right">
                                        <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                            ID="btnAddReporter" runat="server" class="bigfont" Text="添加产品线" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
