<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  EnableEventValidation="false" CodeBehind="PNSelectAuditor.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.PNSelectAuditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
<link href="/public/css/treelist.css" type="text/css" rel="stylesheet" />
<script language="javascript" src="/public/js/dialog.js"></script>
<script language="javascript" src="/public/js/jquery-1.2.6.js"></script>


<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>



<script language="javascript">

    function show(){
        
        dialog("组织机构图", "id:testID", "500px", "300px", "text");
    }

</script>
<script type="text/javascript">
    $().ready(function() {
        $("#<%=ddltype1.ClientID %>").empty();
        $("#<%=ddltype2.ClientID %>").empty();
        PurchaseWeb.Purchase.Requisition.PNSelectAuditor.getalist($("#<%=hidtype.ClientID %>").val(), init1);
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
        PurchaseWeb.Purchase.Requisition.PNSelectAuditor.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
        function init2(r) {
            $("#<%=ddltype2.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>请选择...</option>");
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

            PurchaseWeb.Purchase.Requisition.PNSelectAuditor.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
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

            PurchaseWeb.Purchase.Requisition.PNSelectAuditor.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
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
        });

    });
</script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="2" class="heading">检索</td>     
            <td colspan="2" class="heading" style="text-align:right"><span id="sp" onclick="show();" style="color:#7282a9;cursor:pointer">组织机构图</span></td>       
        </tr>
        <tr>
            <td class="oddrow">人员类型：<asp:HiddenField ID="hidIds" runat="server" /></td>
            <td colspan="3" class="oddrow-l">
                <asp:RadioButtonList ID="radUserType" runat="server" CssClass="XTable" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="radUserType_SelectedIndexChanged">
                    <asp:ListItem Value="YS" Selected="True">预审</asp:ListItem>
                    <asp:ListItem Value="ZH">知会</asp:ListItem>
                    <asp:ListItem Value="FJ">附加审批</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td style="width:15%" class="oddrow">检索关键字：</td>
            <td class="oddrow-l" style="width:20%"><asp:TextBox ID="txtName" runat="server" />&nbsp;</td>
            <td class="oddrow" style="width: 15%">部门：</td>
            <td class="oddrow-l"><asp:DropDownList ID="ddltype" Width="100px" runat="server"  AutoPostBack="false" />
                            &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                            &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                            <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                            <asp:HiddenField ID="hidtype2" Value="-1"  runat="server" /></td>
        </tr>
        <tr>
            <td colspan="4"><asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="widebuttons" Text=" 检索 " />&nbsp;<asp:Button ID="btnClean" runat="server" CssClass="widebuttons" OnClick="btnClean_Click" Text=" 重新搜索 " /></td>
        </tr>
    </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" DataKeyNames="SysUserID" OnRowCommand="gv_RowCommand" Width="100%" CellPadding="4">
        
        <Columns>
            <asp:ButtonField Text="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="sysuserid" HeaderText="sysuserid" Visible="False" />
            <asp:BoundField DataField="userid" HeaderText="userid" Visible="False" />
            <asp:BoundField DataField="usercode" HeaderText="usercode" Visible="False" />
            <asp:BoundField DataField="username" HeaderText="姓名" />
            <asp:BoundField DataField="userid" HeaderText="员工编号" />
            <asp:BoundField DataField="email" HeaderText="Email" />
            <asp:BoundField DataField="telephone" HeaderText="电话" />
            <asp:BoundField DataField="mobile" HeaderText="手机" />
            <asp:BoundField DataField="positiondescription" HeaderText="职称" />
            <asp:BoundField DataField="status" HeaderText="status" Visible="False" />
        </Columns>
    </asp:GridView>
    <input type="button" class="widebuttons" value=" 关闭 " onclick="window.close();" />
</asp:Content>

