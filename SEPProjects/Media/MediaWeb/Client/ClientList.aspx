<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Client_ClientList" Title="客户列表" Codebehind="ClientList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table border="0" width="100%">
        <tr>
            <td>
                <%-- <table class="tablehead">
                            <tr>
                                <td>
                           <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                    ID="btnAddReporter" runat="server" class="bigfont" Text="添加新客户" OnClick="btnAdd_Click" />
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
                            客户中文全称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtChFullName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            客户中文简称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtChShortName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            客户英文全称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtEnFullName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            客户英文简称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtEnShortName" runat="server" Width="120px"></asp:TextBox>
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
                                        客户列表
                                    </td>
                                    <td width="74%"></td>
                                    <td class="tablehead" width="16%" align="right">
                                        <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                            ID="btnAddReporter" runat="server" class="bigfont" Text="添加新客户" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
