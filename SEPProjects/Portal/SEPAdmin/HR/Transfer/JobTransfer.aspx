<%@ Page Language="C#" AutoEventWireup="true" Inherits="Transfer_JobTransfer" MasterPageFile="~/MasterPage.master" Codebehind="JobTransfer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
<script type="text/javascript" src="/public/js/DatePicker.js"></script>
<script src="/public/js/jquery.js" type="text/javascript"></script>
    <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
    </script>    
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                部门调整检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                生效日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtBeginTime" runat="server" onclick="setDate(this);" />&nbsp -- &nbsp<asp:TextBox ID="txtEndTime" runat="server" onclick="setDate(this);" />
            </td>
        </tr>        
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
<table width="100%" > 
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
            <td >
            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
         OnRowCommand="gvList_RowCommand" OnPageIndexChanging="gvList_PageIndexChanging" DataKeyNames="id" Width="100%" >
         <Columns>
            <asp:TemplateField HeaderText="姓名">
                <ItemTemplate>
                    <a href='JobTransferDetail.aspx?jobid=<%#Eval("id") %>'><%# Eval("sysUserName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nowCompanyName" HeaderText="原公司" />
            <asp:BoundField DataField="nowDepartmentName" HeaderText="原部门" />
            <asp:BoundField DataField="nowGroupName" HeaderText="原团队" />
            <asp:BoundField DataField="newCompanyName" HeaderText="新公司" />
            <asp:BoundField DataField="newDepartmentName" HeaderText="新部门" />
            <asp:BoundField DataField="newGroupName" HeaderText="新团队" />         
            <%--<asp:TemplateField HeaderText="编辑">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <a href='JobTransferEdit.aspx?jobid=<%#Eval("id") %>&userid=<%#Eval("sysid") %>'>
                        <img src="../../images/edit.gif" border="0px;"></a>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%--<asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" CommandName="Del" runat="server" ImageUrl="../../images/disable.gif" CommandArgument='<%# Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
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
