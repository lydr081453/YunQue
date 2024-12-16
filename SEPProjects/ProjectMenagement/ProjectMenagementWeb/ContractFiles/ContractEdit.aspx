<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ContractEdit.aspx.cs" Inherits="FinanceWeb.ContractFiles.ContractEdit" %>

<%@ Register Src="/UserControls/Project/PrepareDisplay.ascx" TagName="Prepare" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript">
        function openContractDlg() {
            var win = window.open('/project/UploadFiles.aspx?pid=<%= Request["ProjectID"] %>', "_self", 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.5, screen.availHeight * 0.5);
        }
        function check() {
            if ($("[type='checkbox']:checked").length == 0) {
                alert('请选择提交的证据链数据！');
                return false;
            }
        }
    </script>

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
                        <td class="heading">证据链信息&nbsp;<asp:Button ID="btnDlg" runat="server" Text="上传新的证据链" CssClass="widebuttons" OnClientClick="openContractDlg();return false;" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvContract" runat="server" AutoGenerateColumns="false" AllowPaging="false" OnRowCommand="gvContract_RowCommand">
                                <Columns>
                                   <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                       <ItemTemplate>
                                           <input type="checkbox" name="chkContractId" id="chkContractId" <%# Eval("Status") != null && (int)Eval("Status") == (int)ESP.Finance.Utility.ContractStatus.Status.Wait_Submit?"checked='checked'" :"disabled='disabled'" %>" value="<%# Eval("ContractID") %>" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : ESP.Finance.Utility.ContractStatus.Status_Names[int.Parse( Eval("Status").ToString())] %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="审核人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("Status") == null ? "" : AuditorName %>
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
                                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete"  Visible='<%# Eval("Status") != null && ((int)Eval("Status") == (int)ESP.Finance.Utility.ContractStatus.Status.Wait_Submit || (int)Eval("Status") == (int)ESP.Finance.Utility.ContractStatus.Status.Rejected ) ? true : false %>' runat="server" CommandArgument='<%# Eval("ContractID") %>'
                                                            CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>"
                                                            OnClientClick="return confirm('你确定删除吗？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>

                    <tr>
                        <td><asp:Button ID="btnSubmit" runat="server" OnClientClick="return check();" Text="提交" CssClass="widebuttons" OnClick="btnSubmit_Click"  />
                            &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                                        <tr>
                        <td class="oddrow" width="15%">审批历史:<asp:Label ID="lblLog" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
