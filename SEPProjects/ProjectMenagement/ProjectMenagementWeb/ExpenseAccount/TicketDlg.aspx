<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" CodeBehind="TicketDlg.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.TicketDlg" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript" type="text/javascript">

        function CheckForm() {

            var errorMessage = "";

            if (document.getElementById("<%=txtBoarder.ClientID %>").value == "") {
                errorMessage += "-- 请填写登机人!\n";
            }
            if (document.getElementById("<%=txtID.ClientID %>").value == "") {
                        errorMessage += "-- 请填写登机人的身份证号!\n";
                    }
                    if (document.getElementById("<%=txtPhone.ClientID %>").value == "") {
                        errorMessage += "-- 请填写登机人的联系电话!\n";
                    }

                    if (errorMessage != "") {
                        alert(errorMessage);
                        return false;
                    }
                    else {
                        return true;
                    }
                }

        $().ready(function() {
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();
            FinanceWeb.ExpenseAccount.TicketDlg.getalist($("#<%=hidtype.ClientID %>").val(), init1);
            function init1(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                    if (r.value[k][0] == $("#<%=hidtype1.ClientID %>").val()) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                    }
                    else {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                    }
                }
            }
            if ($("#<%=hidtype.ClientID %>").val() == "") {
                $("#<%=ddltype.ClientID %>").val("-1");
            }
            FinanceWeb.ExpenseAccount.TicketDlg.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
            function init2(r) {
                $("#<%=ddltype2.ClientID %>").empty();
                if (r.value != null)
                    for (j = 0; j < r.value.length; j++) {
                    if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                        document.getElementById("<% =hidGroupName.ClientID %>").value = r.value[j][1];
                    }
                    else {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\">" + r.value[j][1] + "</option>");
                    }
                }
            }
            $("#<%=ddltype.ClientID %>").val($("#<%=hidtype.ClientID %>").val());

            $("#<%=ddltype.ClientID %>").change(function() {
                $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
                $("#<%=ddltype1.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").empty();

                FinanceWeb.ExpenseAccount.TicketDlg.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                    }
                }
                $("#<%=ddltype2.ClientID %>").append("<option value='-1'>请选择...</option>");
                $("#<%=hidtype1.ClientID %>").val("-1");
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype1.ClientID %>").change(function() {
                $("#<%=ddltype2.ClientID %>").empty();

                FinanceWeb.ExpenseAccount.TicketDlg.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                    }
                }
                $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype2.ClientID %>").change(function() {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
                var ddl = document.getElementById("<%=ddltype2.ClientID %>");
                var deptname = "";
                if (ddl.options[ddl.selectedIndex].text != "请选择...") {
                    deptname = ddl.options[ddl.selectedIndex].text;
                    document.getElementById("<% =hidGroupName.ClientID %>").value = deptname;
                }
            });

        });

  

    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="tableForm">
        <tr>
            <td colspan="4" class="oddrow-l">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow-l" colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="heading">
                            检索
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%" class="oddrow">
                            检索关键字：
                        </td>
                        <td class="oddrow-l" style="width: 20%">
                            <asp:TextBox ID="txtName" runat="server" />&nbsp;
                        </td>
                        <td class="oddrow" style="width: 15%">
                            部门：
                        </td>
                        <td class="oddrow-l">
                            <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                            &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                            &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                            <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidGroupName" Value="" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="oddrow-l">
                            <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="widebuttons"
                                Text=" 检索 " />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AutoGenerateColumns="false"
                    OnRowCommand="gv_RowCommand" DataKeyNames="SysUserID" Width="100%" CellPadding="4">
                    <Columns>
                        <asp:BoundField DataField="sysuserid" HeaderText="sysuserid" />
                        <asp:BoundField DataField="userid" HeaderText="userid" />
                        <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="usercode" HeaderText="员工帐号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="username" HeaderText="员工姓名" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="userid" HeaderText="员工编号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="电话" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("telephone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="mobile" HeaderText="手机" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="positiondescription" HeaderText="职称" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="status" HeaderText="status" Visible="False" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%" id="tbBoarderOthers" runat="server" visible="false" border="0" cellpadding="0" cellspacing="0" class="tableForm">
        <tr>
            <td colspan="2" class="heading">
                <font color="red">非公司内部员工</font>&nbsp;&nbsp;&nbsp;&nbsp;<input class="widebuttons"
                    type="button" value=" 列表中未找到，添加新信息 " onclick="document.getElementById('ctl00_ContentPlaceHolder1_trAdd').style.display = 'block';" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr id="trAdd" style="display: none;">
            <td class="oddrow-l" colspan="4">
                登机人:
                <asp:TextBox runat="server" ID="txtBoarder"></asp:TextBox><font color="red">*</font>
                证件类型:
                <asp:DropDownList ID="ddlIDType" runat="server">
                <asp:ListItem Selected="True" Value="0" Text="身份证"></asp:ListItem>
                <asp:ListItem value="1" Text="护照"></asp:ListItem>
                <asp:ListItem value="1" Text="军人证"></asp:ListItem>
                <asp:ListItem value="1" Text="回乡证"></asp:ListItem>
                <asp:ListItem value="1" Text="台胞证"></asp:ListItem>
                <asp:ListItem value="1" Text="港澳通行证"></asp:ListItem>
                <asp:ListItem value="1" Text="国际海员证"></asp:ListItem>
                <asp:ListItem value="1" Text="外国人永久居留证"></asp:ListItem>
                <asp:ListItem value="1" Text="旅行证"></asp:ListItem>
                <asp:ListItem value="1" Text="户口薄"></asp:ListItem>
                <asp:ListItem value="1" Text="出生证明"></asp:ListItem>
                <asp:ListItem value="1" Text="其他"></asp:ListItem>                                 
                </asp:DropDownList>
                证件号:
                <asp:TextBox runat="server" ID="txtID"></asp:TextBox><font color="red">*</font>
                登记人电话:
                <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox><font color="red">*</font>
                &nbsp;
                <asp:Button runat="server" ID="btnSave" Text=" 保存 " CssClass="widebuttons" OnClientClick="return CheckForm();"
                    OnClick="btnSave_OnClick" />
                &nbsp;
                <asp:Button runat="server" ID="btnCancel" Text=" 取消 " CssClass="widebuttons" OnClick="btnCancel_OnClick" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                已使用的非公司内部员工列表
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" OnRowCommand="gvUsers_RowCommand"
                    OnRowDataBound="gvUsers_RowDataBound" DataKeyNames="Id" Width="100%" CellPadding="4">
                    <Columns>
                        <asp:BoundField DataField="UserId" HeaderText="UserId" />
                        <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Boarder" HeaderText="登机人" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="CardType" HeaderText="证件类型" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CardNo" HeaderText="证件号" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Mobile" HeaderText="电话" ItemStyle-HorizontalAlign="Center" />
                          <asp:ButtonField Text="编辑" CommandName="BoarderEdit" ButtonType="button" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="6%" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
