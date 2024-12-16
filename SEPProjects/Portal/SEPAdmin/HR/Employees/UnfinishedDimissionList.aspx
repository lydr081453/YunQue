﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnfinishedDimissionList.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Employees.UnfinishedDimissionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                离职检索
            </td>
        </tr>
        <tr>
        <td class="oddrow" style="width: 15%">
                员工编号:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtUserCode" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtuserName" runat="server" />
            </td>
            
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtDepartments" runat="server" />
            </td>
            <td class="oddrow" style="width: 15%">
                离职日期:
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
            <asp:TemplateField HeaderText="姓名">
                <ItemTemplate>
                    <a href='DimissionApplyDetail.aspx?dimissionApplyId=<%#Eval("id") %>'><%# Eval("username") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="groupName" HeaderText="所属团队" />
            <asp:BoundField DataField="departmentName" HeaderText="所在部门" />
            <asp:TemplateField HeaderText="创建时间">
                <ItemTemplate>
                    <%# Eval("createDate").ToString().Split(' ')[0]%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href='DimissionApplyEdit.aspx?dimissionApplyId=<%#Eval("id") %>'>
                        <img src="../../images/edit.gif" border="0px;"></a>
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
