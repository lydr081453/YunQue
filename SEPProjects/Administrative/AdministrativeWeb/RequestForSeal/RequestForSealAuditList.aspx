<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="RequestForSealAuditList.aspx.cs" Inherits="AdministrativeWeb.RequestForSeal.RequestForSealAuditList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td>关键字：<asp:TextBox ID="txtKey" runat="server" /> <asp:Button ID="btnSearch" runat="server" Text="检索" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
            <tr>
                <td>用印申请列表</td>
            </tr>
        <tr>
         
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td>
                            <asp:GridView ID="gvList" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="SA号" DataField="SANo" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="公司" DataField="BranchName" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="关联单据号" DataField="DataNum" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="申请人" DataField="RequestorName" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="部门" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("DeptName1").ToString() + "-" + Eval("DeptName2").ToString() + "-" + Eval("DeptName3").ToString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="文件名称" DataField="FileName" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="日期" DataField="RequestDate" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="印章类型" DataField="SealType" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="文件类别" DataField="FileType" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="创建日期" DataField="CreatedDate" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span title="<%# (int)Eval("status") == (int)ESP.Administrative.Common.Status.RequestForSealStatus.Auditing ? ("待审批人：" + Eval("AuditorName")) : "" %>">
                                                <%# ESP.Administrative.Common.Statics.RequestForSealStatus_Names[(int)Eval("status")] %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="RequestForSealView.aspx?burl=RequestForSealAuditList.aspx&RfsId=<%# Eval("Id").ToString() %>">查看</a>
                                            <asp:HyperLink ID="hylAudit" runat="server" Text="审批" NavigateUrl='<%# "~/RequestForSeal/RequestForSealAudit.aspx?RfsId=" + Eval("id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
