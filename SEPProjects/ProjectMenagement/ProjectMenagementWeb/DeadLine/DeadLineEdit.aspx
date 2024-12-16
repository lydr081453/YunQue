<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeadLineEdit.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="FinanceWeb.DeadLine.DeadLineEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function checkValid() {
            var msg = "";
            if (document.getElementById("<%=txtEndDate.ClientID %>").value == "") {
                msg += "请输入第一结帐日期!" + "\n";
            }
            if (document.getElementById("<%=txtEndDate2.ClientID %>").value == "") {
                msg += "请输入第二结帐日期!" + "\n";
            }
            if (document.getElementById("<%=txtProjectDate.ClientID %>").value == "") {
                msg += "请输入项目结帐日期!" + "\n";
            }
            if (document.getElementById("<%=txtSalary.ClientID %>").value == "") {
                msg += "请输入工资发放日!" + "\n";
            }
            if (document.getElementById("<%=txtAuditDeadLine.ClientID %>").value == "") {
                msg += "请计算报销审批截止日期!" + "\n";
            }
            if (document.getElementById("<%=txtAuditDeadLine2.ClientID %>").value == "") {
                msg += "请计算第二报销审批截止日期!" + "\n";
            }
            if (msg == "")
                return true;
            else {
                alert(msg);
                return false;
            }
        }

        function createValid() {
            var msg = "";
            if (document.getElementById("<%=txtEndDate.ClientID %>").value == "") {
            msg += "请输入第一结帐日期!" + "\n";
        }
        if (document.getElementById("<%=txtEndDate2.ClientID %>").value == "") {
            msg += "请输入第二结帐日期!" + "\n";
        }
        if (msg == "")
            return true;
        else {
            alert(msg);
            return false;
        }
    }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="6">结账日信息<a name="top_A" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="10%">月度选择:
            </td>
            <td class="oddrow-l" width="35%" colspan="5">
                <asp:DropDownList runat="server" ID="ddlMonth">
                    <asp:ListItem Text="一月" Value="1"></asp:ListItem>
                    <asp:ListItem Text="二月" Value="2"></asp:ListItem>
                    <asp:ListItem Text="三月" Value="3"></asp:ListItem>
                    <asp:ListItem Text="四月" Value="4"></asp:ListItem>
                    <asp:ListItem Text="五月" Value="5"></asp:ListItem>
                    <asp:ListItem Text="六月" Value="6"></asp:ListItem>
                    <asp:ListItem Text="七月" Value="7"></asp:ListItem>
                    <asp:ListItem Text="八月" Value="8"></asp:ListItem>
                    <asp:ListItem Text="九月" Value="9"></asp:ListItem>
                    <asp:ListItem Text="十月" Value="10"></asp:ListItem>
                    <asp:ListItem Text="十一月" Value="11"></asp:ListItem>
                    <asp:ListItem Text="十二月" Value="12"></asp:ListItem>
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="10%">第一结账日:
            </td>
            <td class="oddrow-l" width="20%">
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
                <font color="red">*</font>
            </td>
            <td class="oddrow" width="10%">起始日期:
            </td>
            <td class="oddrow-l" width="20%">
                <asp:TextBox ID="txtExpDeadLine" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
            <td class="oddrow" width="10%">审批截止日期:
            </td>
            <td class="oddrow-l" width="20%">
                <asp:TextBox ID="txtAuditDeadLine" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="10%">第二结账日:
            </td>
            <td class="oddrow-l" width="20%">
                <asp:TextBox ID="txtEndDate2" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
                <font color="red">*</font>
            </td>
            <td class="oddrow" width="10%">起始日期:
            </td>
            <td class="oddrow-l" width="20%">
                <asp:TextBox ID="txtExpDeadLine2" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
            <td class="oddrow" width="10%">审批截止日期:
            </td>
            <td class="oddrow-l" width="20%">
                <asp:TextBox ID="txtAuditDeadLine2" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="10%">项目结账日:
            </td>
            <td class="oddrow-l" width="35%" colspan="5">
                <asp:TextBox ID="txtProjectDate" onkeyDown="return false; " Style="cursor: hand"
                    runat="server" onclick="setDate(this);" />
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="10%">工资发放日:
            </td>
            <td class="oddrow-l" width="35%" colspan="5">
                <asp:TextBox ID="txtSalary" onkeyDown="return false; " Style="cursor: hand"
                    runat="server" onclick="setDate(this);" />
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="6">
                <asp:Button runat="server" ID="btnCreate" Text=" 计算 " CssClass="widebuttons" OnClientClick="return createValid();"
                    OnClick="btnCreate_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnSave" Text=" 保存 " CssClass="widebuttons" OnClientClick="return checkValid();"
                    OnClick="btnSave_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnCancel" Text=" 返回 " CssClass="widebuttons" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
