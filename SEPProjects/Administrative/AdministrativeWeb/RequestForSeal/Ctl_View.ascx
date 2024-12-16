<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ctl_View.ascx.cs" Inherits="AdministrativeWeb.RequestForSeal.Ctl_View" %>
<%@ Register Src="~/RequestForSeal/Ctl_AuditLog.ascx" TagPrefix="uc1" TagName="Ctl_AuditLog" %>

<table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td colspan="2" style="font-size: 15px; font-weight: bold;">用印申请</td>
                    </tr>
                                        <tr>
                        <td style="text-align: right">SA号：</td>
                        <td>
                            <asp:Label ID="labSANo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">申请人：</td>
                        <td>
                            <asp:Label ID="labRequestorName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" style="text-align: right">公司：</td>
                        <td>
                            <asp:Label ID="ddlBrandch" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">部门：</td>
                        <td>
                            <asp:Label ID="ddlDepartments" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">关联单据号：</td>
                        <td>
                            <asp:Label ID="txtDataNum" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">日期：</td>
                        <td>
                            <asp:Label ID="PickerFrom1" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">用印文件名称：</td>
                        <td>
                            <asp:Label ID="txtFileName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">文件份数：</td>
                        <td>
                            <asp:Label ID="txtFileQuantity" runat="server"/></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">文件类别：</td>
                        <td>
                            <asp:Label ID="ddlFileType" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">印章类型：</td>
                        <td>
                            <asp:Label ID="ddlSealType" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">用印文件：<asp:HiddenField ID="hidFiles" runat="server" /></td>
                        <td>
                            <table style="margin-left:-12px;">
                                <tr>
                                    <td>
                                        <table><tr>
                                        <asp:Repeater runat="server" ID="repFiles">
                                            <ItemTemplate>
                                                <td>
                                                <table style="border:1px;">
                                                    <tr>
                                                        <td><asp:LinkButton ID="lnkFile" runat="server" CausesValidation="false" OnClick="lnkFile_Click" CommandArgument="<%# Container.DataItem.ToString() %>" Text="<%# Container.DataItem.ToString().Split('_')[1] %>" /></td>
                                                    </tr>
                                                </table>
                                                    </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </tr></table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">备注：</td>
                        <td>
                            <asp:Label ID="txtRemark" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<br />
<uc1:Ctl_AuditLog runat="server" id="Ctl_AuditLog" />
