<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuxiliaryList.aspx.cs" Inherits="SEPAdmin.HR.Auxiliary.AuxiliaryList" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                待入职辅助工作
            </td>
        </tr>
        <tr>
			<td >
				<UL>
					<LI><A runat="server" id="NewUserUrl" style="color:#000000" href="AuxiliaryEdit.aspx"><span style="color:#4f556a">新增辅助工作</span></A></LI>
				</UL>
			</td>
		</tr>
    </table>
    <br />
    <table width="100%"> 
        <tr>
            <td >
    <table width="100%" id="tabTop" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageTop" runat="server">
                    <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                    runat="server" />
            </td>
        </tr>
    </table>    
    </td>
    </tr>
    <tr>
    <td>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
        PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvList_RowCommand"
        OnPageIndexChanging="gvList_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="auxiliaryname" HeaderText="辅助工作名称" />
            <asp:BoundField DataField="Description" HeaderText="描述" /> 
            <asp:TemplateField HeaderText="设置工作人员" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href='EmployeesInAuxiliariesEdit.aspx?auxiliaryid=<%#Eval("id") %>'>
                        <img src="../../images/edit.gif" border="0px;"></a>
                </ItemTemplate>
            </asp:TemplateField>         
            <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href='AuxiliaryEdit.aspx?auxiliaryid=<%#Eval("id") %>'>
                        <img src="../../images/edit.gif" border="0px;"></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Del" runat="server" ImageUrl="../../images/disable.gif" CommandArgument='<%# Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings Visible="false" />
    </asp:GridView>
    </td>
    </tr>
    <tr>
    <td>
    <table width="100%" id="tabBottom" runat="server">
        <tr>
            <td width="50%">
                <asp:Panel ID="PageBottom" runat="server">
                    <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                </asp:Panel>
            </td>
            <td align="right" class="recordTd">
                记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                    runat="server" />
            </td>
        </tr>
    </table>
    </td>
        </tr>
        </table> 
</asp:Content>
