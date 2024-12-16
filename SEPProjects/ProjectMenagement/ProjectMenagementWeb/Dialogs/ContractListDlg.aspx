<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Title="合同信息"
    Inherits="Dialogs_ContractListDlg" Codebehind="ContractListDlg.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function DownFile() {
            return String.format("<input type='button' id='btnSelect' value='下载'/>");
        }
        function EnterTextBox() {
            if (event.keyCode == 13) {
                return false;
            }
        }
        function filechange(e) {
            if (e.files[0].name != "") {
                document.getElementById("<% =txtContractDescription.ClientID %>").value = e.files[0].name.split('.')[0];
            }
        }
    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                申请文件上传
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow-l" colspan="4" align="center">
                <font color="red">如果在“替代原有合同”中选择了相关合同，将视为新合同替代原有合同。否则将视为新合同（含追加）。</font>
                <br />
                <font color="red">附件可以是电子挡合同或客户确认邮件。</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                合同文件:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:FileUpload ID="fileupContract" onchange="filechange(this);" runat="server" Width="60%" /><asp:HyperLink ID="hypAttach" runat="server" ImageUrl="/images/ico_04.gif" Visible="false"></asp:HyperLink>
            </td>
            </tr>
        <tr>
            <td class="oddrow" width="15%">
               双章合同:
            </td>
              <td class="oddrow-l" colspan="3">
                   <asp:CheckBox ID="chkDouble" Checked="false" runat="server" ForeColor="Red" Text="如果该附件为补充的双章合同，勾选后不需要项目审批"  />
              </td>
        </tr>
            <tr>
            <td class="oddrow" width="15%">
                替代原有合同:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:DropDownList ID="ddlOldContract" runat="server" Width="60%">
                </asp:DropDownList><asp:Label ID="lblParent" runat="server"></asp:Label>
                <br />
            </td>
        </tr>
<%--        <tr>
            <td class="oddrow">
                合同金额:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtTotalAmount" Text="0.00" runat="server" MaxLength=10 onkeypress='return EnterTextBox();'></asp:TextBox><font color="red">*</font><asp:RequiredFieldValidator
                    ID="rfvTotalAmount" runat="server" ControlToValidate="txtTotalAmount" ErrorMessage="合同总金额必填"
                    ValidationGroup="Save"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
        <tr id="trDes" runat="server">
            <td class="oddrow">
                合同描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtContractDescription" runat="server" Width="80%" TextMode="MultiLine" Height="100px"></asp:TextBox><%--<font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="合同描述必填" ValidationGroup="Save" ControlToValidate="txtContractDescription"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
          <tr>
            <td class="oddrow">
                &nbsp;
            </td>
            <td class="oddrow-l" colspan="3">
               <asp:CheckBox ID="chkDelay" Checked="false" runat="server" Text="项目延期" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow-1" align="center">
                <asp:Button ID="btnSaveContract" CssClass="widebuttons" runat="server" ValidationGroup="Save"
                    Text="提交" OnClick="btnSaveContract_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
