<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ProjectType_ProjectTypeEdit" Codebehind="ProjectTypeEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function HeadClick() {
            var win = window.open('/Dialogs/EmployeeList.aspx?showSelectAll=hidden&<% =ESP.Finance.Utility.RequestName.SearchType %>=ProjectType', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
               win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                项目类型信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                所属类型:<asp:HiddenField ID="hidProjectTypeID" runat="server" />
            </td>
            <td class="oddrow-l">
                <asp:DropDownList ID="ddlParent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlParent_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlParent2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlParent2_SelectedIndexChanged" />
            </td>
            </tr>
        <tr>
            <td class="oddrow" >
                类型代码:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox runat="server" ID="txtCode" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Enabled="false" Display="Dynamic" runat="server" ControlToValidate="txtCode" ErrorMessage="分类代码为必填项">*</asp:RequiredFieldValidator>
            </td>
                        </tr>
        <tr>
            <td class="oddrow" >
                类型名称:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox runat="server" ID="txtProjectTypeName"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtProjectTypeName" ErrorMessage="分类名称为必填项">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                负责人:
            </td>
            <td class="oddrow">
            <asp:TextBox ID="txtProjectHead" runat="server" onkeyDown="return false; " Enabled="false" Style="cursor:pointer;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtProjectHead" ErrorMessage="负责人为必填项">*</asp:RequiredFieldValidator>
                <input type="button"
                id="btnHead" onclick="return HeadClick();" runat="server" class="widebuttons" disabled="disabled" value="  选择  " />
                <asp:HiddenField ID="hidHead" runat="server" Value="0" />
            </td>
        </tr>
                <tr>
            <td class="oddrow" >
                预估媒体成本比例:
            </td>
            <td class="oddrow-l" >
                <asp:TextBox runat="server" ID="txtCostRate" Text="100"></asp:TextBox>%
                <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Double" Operator="GreaterThanEqual" ValueToCompare="0" ControlToValidate="txtCostRate"
                            ErrorMessage="预估成本比例错误"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                描述:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" ID="txtDesc" Width="300px" Height="80px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%" class="XTable">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" OnClientClick="" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false"
                    CssClass="widebuttons" OnClick="btnBack_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
