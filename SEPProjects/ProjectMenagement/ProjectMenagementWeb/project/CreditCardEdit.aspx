<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" CodeBehind="CreditCardEdit.aspx.cs" Inherits="FinanceWeb.project.CreditCardEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function UserClick() {
            var win = window.open('/Dialogs/EmployeeList.aspx?<% =ESP.Finance.Utility.RequestName.SearchType %>=BusinessCard', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        $().ready(function() {
            $("#<%=ddlBranch.ClientID %>").empty();
            FinanceWeb.project.CreditCardEdit.GetBranchs(initBranch);
            function initBranch(r) {
                if (r.value != null) {
                    for (k = 0; k < r.value.length; k++) {
                        if (r.value[k][0] == $("#<%=hidBranchId.ClientID %>").val()) {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                        }
                        else {
                            $("#<%=ddlBranch.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                        }
                    }
                }
            }
        });
        
        function selectBranch(id, text) {
            if (id == "-1") {
                document.getElementById("<% =hidBranchId.ClientID %>").value = "";
            }
            else {
                document.getElementById("<% =hidBranchId.ClientID %>").value = id ;
            }
        }

    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                商务卡信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                卡号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtCardNo"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" style="width: 20%">
                员工姓名:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:HiddenField runat="server" ID="hidUserId" />
                <asp:Label runat="server" ID="lblUserName"></asp:Label>&nbsp;<input
                    type="button" id="btnSelect" onclick="return UserClick();" class="widebuttons"
                    value="  选择  " />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                分户号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtCardNo2"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" style="width: 20%">
                员工编号:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="lblUserCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                授信额度:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" ID="txtLine"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" style="width: 20%">
                可用额度:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" ID="txtAvailable"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                发卡日:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox runat="server" onkeyDown="return false; " Style="cursor: hand" onclick="setDate(this);"
                    ID="txtBeginTime"></asp:TextBox>&nbsp;
            </td>
            <td class="oddrow" style="width: 20%">
                到期日:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" onkeyDown="return false; " Style="cursor: hand" onclick="setDate(this);"
                    ID="txtEndTime"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                卡状态:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlStatus">
                    <asp:ListItem Text="请选择..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="注销" Value="0"></asp:ListItem>
                    <asp:ListItem Text="正常" Value="1"></asp:ListItem>
                    <asp:ListItem Text="挂失" Value="2"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
             <td class="oddrow" style="width: 20%">
               注销日期:
            </td>
            <td class="oddrow-l" style="width: 30%">
               <asp:TextBox runat="server" onkeyDown="return false; " Style="cursor: hand" onclick="setDate(this);"
                    ID="txtCancelDate"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                公司代码:
            </td>
            <td class="oddrow-l"  style="width: 35%">
                <asp:DropDownList runat="server" ID="ddlBranch">
                </asp:DropDownList><asp:HiddenField runat="server" ID="hidBranchId" />
                &nbsp; <font color="red">*</font>
                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"  InitialValue="-1"
                    ErrorMessage="公司代码必填"></asp:RequiredFieldValidator>
            </td>
             <td class="oddrow" style="width: 20%">
                领用情况:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:DropDownList runat="server" ID="ddlDraw">
                    <asp:ListItem Text="请选择..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="卡丢失本人已挂失" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已领" Value="1"></asp:ListItem>
                    <asp:ListItem Text="卡已剪" Value="2"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnOk" Text=" 确定 " CssClass="widebuttons" runat="server" OnClick="btnOk_Click" />
                &nbsp;<asp:Button ID="btnReturn" Text=" 返回 " CssClass="widebuttons" runat="server" CausesValidation="false"
                    OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        DisplayMode="List" ShowMessageBox="true" />
</asp:Content>
