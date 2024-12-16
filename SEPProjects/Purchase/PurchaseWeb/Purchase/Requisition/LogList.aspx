<%@ Page Language="C#" AutoEventWireup="true" Inherits="Purchase_Requisition_LogList" MasterPageFile="~/MasterPage.master" Codebehind="LogList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 100%">
        <tr>        
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">检索</td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width:15%">流水号:</td>
                                    <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtGlideNo" MaxLength="200" runat="server" /></td>
                                    <td class="oddrow" style="width:15%">申请单号:</td>
                                    <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtPrNo" MaxLength="200" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center"><asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>                 
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tabTop" runat="server">
                            <tr><td width="50%">
                            <asp:Panel ID="PageTop" runat="server">
                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                            </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT" runat="server" /></td>
                            </tr>
                            </table><br />
                            <asp:GridView ID="gvL" runat="server" AutoGenerateColumns="False" PageSize="20" AllowPaging="True" Width="100%" OnPageIndexChanging="gvL_PageIndexChanging">                                
                                <Columns>
                                    <asp:BoundField DataField="glideno" HeaderText="流水号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%"/>
                                    <asp:BoundField DataField="PrNo" HeaderText="申请单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="7%"/>
                                    <asp:BoundField DataField="Des" HeaderText="操作日志" ItemStyle-HorizontalAlign="Left" />
                                                                                                       
                                     
                                </Columns>
                                
                                <PagerSettings Visible="false"/>
                            </asp:GridView>
                            <table width="100%" id="tabBottom" runat="server">
                            <tr><td width="50%">
                            <asp:Panel ID="PageBottom" runat="server">
                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;                                            
                            </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount" runat="server" /></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
