﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OMAuditList.aspx.cs"  MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.IT.OMAuditList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">关键字:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" style="width: 10%">运维类型:
            </td>
            <td class="oddrow-l">
                <asp:DropDownList runat="server" ID="ddlStatus">
                </asp:DropDownList>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvOM" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvOM_RowDataBound"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="标题" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="UserName" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FlowName" HeaderText="运维类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="CreateTime" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Description" HeaderText="工作内容" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
                        <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="OMAudit.aspx?id=<%#Eval("Id").ToString() %>" >审核</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
