<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Title="媒体行业属性" CodeBehind="IndustryList.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.IndustryList" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="server">
    <table width="100%">
        <tr>
            <td colspan="4">
                <%--            <table class="tablehead">
        <tr>
            <td>
                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                    ID="btnAddReporter" runat="server" class="bigfont" Text="添加新行业属性" OnClick="btnAdd_Click" />
                    </td>
        </tr>
        </table>--%>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            行业属性名称
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            &nbsp;
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSearch" Text="查找" OnClick="btnSearch_Click" runat="server" CssClass="widebuttons">
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
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="4" class="headinglist" align="left" width="20%">
                                        媒体行业属性列表
                                    </td>
                                    <td width="62%"></td>
                                    <td class="tablehead" width="18%" align="right">
                                        <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                            ID="btnAddReporter" runat="server" class="bigfont" Text="添加新行业属性" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
