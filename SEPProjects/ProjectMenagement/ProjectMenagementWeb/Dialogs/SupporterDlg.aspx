<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Dialogs_SupporterDlg"  EnableEventValidation="false" Title="支持方选择" Codebehind="SupporterDlg.aspx.cs" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <link href="/public/css/treelist.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/dialog.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript">
        $().ready(function() {
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();
            Dialogs_SupporterDlg.getalist($("#<%=hidtype.ClientID %>").val(), init1);
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
            Dialogs_SupporterDlg.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
            function init2(r) {
                $("#<%=ddltype2.ClientID %>").empty();
                if (r.value != null)
                    for (j = 0; j < r.value.length; j++) {
                    if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
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

                Dialogs_SupporterDlg.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                    }
                }
                $("#<%=hidtype1.ClientID %>").val("-1");
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype1.ClientID %>").change(function() {
                $("#<%=ddltype2.ClientID %>").empty();

                Dialogs_SupporterDlg.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    if (r.value != null) {
                        for (i = 0; i < r.value.length; i++) {
                            $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                        }
                    }
                    if (r.value.length == 1) {
                        $("#<%=hidGroupID.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                        document.getElementById("<% =hidGroupName.ClientID %>").value = document.getElementById("<% =ddltype1.ClientID %>").options[document.getElementById("<% =ddltype1.ClientID %>").selectedIndex].innerHTML;
                    }
                }
                $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype2.ClientID %>").change(function() {
            $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            $("#<%=hidGroupID.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            document.getElementById("<% =hidGroupName.ClientID %>").value = document.getElementById("<% =ddltype2.ClientID %>").options[document.getElementById("<% =ddltype2.ClientID %>").selectedIndex].innerHTML;
                    
            });

        });

        function SupporterValid() {
            var msg = "";
            if (document.getElementById("<% =hidGroupID.ClientID %>").value == "-1" || document.getElementById("<% =hidGroupID.ClientID %>").value=="") {
                msg += "请选择部门" + "\n";
            }
            if (document.getElementById("<% =hidResponserID.ClientID %>").value == "" || document.getElementById("<% =hidResponserID.ClientID %>").value == "-1") {
                msg += "请选择部门负责人" + "\n";
            }
            if (document.getElementById("<% =ddlAmountType.ClientID %>").selectedIndex == 0) {
                msg += "请选择费用类型" + "\n";
            }
            if (document.getElementById("<% =txtAmount.ClientID %>").value == 0) {
                msg += "请输入费用金额" + "\n";
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
    <td class="heading" width="100%" colspan="4">
    支持方信息
    </td>
    </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                部门：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                <asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                <asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                <font color="red">*</font>
                <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
                <asp:HiddenField ID="hidGroupName" Value="" runat="server" />
                <asp:HiddenField ID="hidGroupID" Value="" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                支持方负责人：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtResponser" runat="server"></asp:TextBox>
                    <asp:Button id="btnSearch" runat="server" cssClass="widebuttons" Text="搜索" OnClick="btnSearch_Click"/>
                    <font color="red">*</font>
                <asp:HiddenField ID="hidResponserID" Value="-1" runat="server" />
                 <asp:HiddenField ID="hidResponserCode" Value="-1" runat="server" />
                  <asp:HiddenField ID="hidResponserUserName" Value="-1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                支持方费用：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtAmount" runat="server" MaxLength="11"></asp:TextBox><font color="red">*</font>
            </td>
            </tr>
            <tr>
            <td class="oddrow" style="width: 15%">
                费用类型：
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:DropDownList ID="ddlAmountType" runat="server">
                    <asp:ListItem Text="请选择..." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Fee" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Cost" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Fee & Cost" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
            <td class="oddrow" style="width: 15%">
                <asp:CheckBox ID="chkTax" runat="server" />是否冲抵税金
            </td>
            <td class="oddrow-l" style="width: 35%">
            <font color="red">选中则计算税金时扣除支持方费用</font>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnNewSupporter" runat="server" Text=" 提交 " class="widebuttons"
                    OnClientClick="return SupporterValid();" OnClick="btnNewSupporter_Click" />
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="widebuttons" OnClick="btnClose_Click" />
            </td>
        </tr>
    </table>
    <div id="divEmp" runat="server" style="display:none">
      <asp:GridView ID="gv" runat="server" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound" AutoGenerateColumns="false" DataKeyNames="SysUserID"  Width="100%" CellPadding="4">
        <Columns>
            <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="sysuserid" HeaderText="sysuserid" />
            <asp:BoundField DataField="userid" HeaderText="userid"  />
            <asp:BoundField DataField="usercode" HeaderText="员工帐号" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="username" HeaderText="员工姓名" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="userid" HeaderText="员工编号" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="telephone" HeaderText="电话" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="mobile" HeaderText="手机" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="positiondescription" HeaderText="职称" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="status" HeaderText="status" Visible="False" />
        </Columns>
    </asp:GridView>
    </div>
</asp:Content>
