<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ContractAudit.aspx.cs" Inherits="FinanceWeb.ContractFiles.ContractAudit" %>

<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="Prepare" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <uc1:Prepare ID="PrepareDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" class="tableForm">
                    <tr>
                        <td colspan="2" class="heading">证据链信息</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvContract" runat="server" AutoGenerateColumns="false" AllowPaging="false">
                                <Columns>
                                  <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                       <ItemTemplate>
                                           <input type="checkbox" name="chkContractId" id="chkContractId" value="<%# Eval("ContractID") %>" checked="checked" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : ESP.Finance.Utility.ContractStatus.Status_Names[int.Parse( Eval("Status").ToString())] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="上传时间" ItemStyle-HorizontalAlign="Center" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:BoundField HeaderText="描述" ItemStyle-HorizontalAlign="Center" DataField="Description" />
                                    <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a id="aDownLoad" target="_blank" href='/Dialogs/ContractFileDownLoad.aspx?ContractID=<%# Eval("ContractID") %>'>
                                                <img src="/images/ico_04.gif" border="0" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="上传人" ItemStyle-HorizontalAlign="Center" DataField="CreatorUserName" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="15%">审批历史:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:Label ID="lblLog" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l" colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="15%">审批批示:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtAuditRemark" runat="server" TextMode="MultiLine" Height="80px"
                                Width="70%"></asp:TextBox>
                            <asp:LinkButton runat="server" ID="btnTip" OnClick="btnTip_Click" CausesValidation="false"
                                ToolTip="暂时留个Message,以后再操作"><font style=" text-decoration:underline; color:Blue; font-weight:bold">留言</font></asp:LinkButton><font
                                    color="red">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnOk" runat="server" Text="审批通过" CssClass="widebuttons" OnClick="btnOk_Click" />
                            &nbsp;<asp:Button ID="btnNo" runat="server" Text="审批驳回" CssClass="widebuttons" OnClick="btnNo_Click" />
                            &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
