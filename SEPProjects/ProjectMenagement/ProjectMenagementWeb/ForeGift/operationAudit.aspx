<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="operationAudit.aspx.cs" Inherits="ForeGift_operationAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/public/css/buttonLoading.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">

        function setLoading(button) {
                button.classList.add('loading');
                button.classList.add('disabled');
            }
        
    </script>

    <%@ Register Src="../UserControls/ForeGift/ViewForeGift.ascx" TagName="ViewForeGift"
        TagPrefix="uc1" %>
    <uc1:ViewForeGift ID="ViewForeGift" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">付款确认
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
            <td class="oddrow">审批批示:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="80%" Height="80px"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemark"
                        runat="server" ValidationGroup="audit" ErrorMessage="<br />必须填写审批批示!"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <input type="button" id="btnYes" runat="server" value="审批通过" validationgroup="audit"  OnClientClick="setLoading(this); " 
                    class="widebuttons" onserverclick="btnYes_Click" />
                &nbsp;<input type="button" id="btnNo" runat="server" value="审批驳回" validationgroup="audit"  OnClientClick="setLoading(this); " 
                    class="widebuttons" onserverclick="btnNo_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" OnClick="btnReturn_Click"
                    runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
