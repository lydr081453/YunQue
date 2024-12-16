<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_ProductEdit" Title="Untitled Page" EnableEventValidation="false" Codebehind="ProductEdit.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.DataAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript">
function SupplierClick()
{       
		var win = window.open('SupplierList.aspx?source=product',null,'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
		win.resizeTo(screen.availWidth*0.8,screen.availHeight*0.8);
}
</script>
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <link href="/public/css/treelist.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/dialog.js"></script>

    <script language="javascript">
        function show() {

            dialog("物料类别结构图", "id:testID", "500px", "300px", "text");
        }
    </script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript">
        $().ready(function() {
            ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=hidtype.ClientID %>").val(), pop11);
            function pop11(r) {
                if (null != r.value) {
                    $("#<%=ddltype1.ClientID %>").empty();
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        if ($("#<%=hidtype1.ClientID %>").val() == r.value[i].typeid)
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "  \" selected>" + r.value[i].typename + "</option>");
                        else
                            $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                }
            }
            ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=hidtype1.ClientID %>").val(), pop22);
            function pop22(r) {
                $("#<%=ddltype2.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                if (r.value != null && r.value.length > 0) {
                    for (i = 0; i < r.value.length; i++) {
                        if ($("#<%=hidtype2.ClientID %>").val() == r.value[i].typeid)
                            $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\" selected>" + r.value[i].typename + "</option>");
                        else
                            $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                }
            }

            $("#<%=ddltype.ClientID %>").change(function() {
                $("#<%=ddltype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=ddltype2.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                $("#<%=hidtype1.ClientID %>").val("");
                $("#<%=hidtype2.ClientID %>").val("");

                ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    $("#<%=ddltype1.ClientID %>").empty();
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                }
            });

            $("#<%=ddltype1.ClientID %>").change(function() {

            ESP.Purchase.DataAccess.TypeDataProvider.GetListByParentIdA($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    $("#<%=ddltype2.ClientID %>").empty();
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                    $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                }
            });

            $("#<%=ddltype2.ClientID %>").change(function() {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });
        });
    </script>
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="2">物品编辑</td>
        
        <td class="heading" style="text-align: right" colspan="2">
            <span id="sp" onclick="show();" style="color:#7282a9; cursor: pointer">物料类别结构图</span>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">供应商:</td>
        <td class="oddrow-l"><asp:TextBox onkeyDown="return false;" ID="txtSupplier" runat="server" Width="200px" /></td>
        <td class="oddrow" style="width:20%">物品名称:</td>
        <td class="oddrow-l" style="width:30%"><asp:TextBox ID="txtProductName" runat="server"  MaxLength="100"  Width="200px"  />&nbsp;<font color="red">*</font>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProductName"
                Display="None" ErrorMessage="请填写物品名称"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td class="oddrow" style="width:20%">物品种类:</td>
        <td class="oddrow-l"><asp:TextBox ID="txtproductClass" runat="server" MaxLength="200" Width="200px" />
        </td>
        <td class="oddrow" style="width:20%">物品价格:</td>
        <td class="oddrow-l"><asp:TextBox ID="txtproductPrice" runat="server" MaxLength="12" Width="200px"/><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator19" runat="server" ErrorMessage="物品价格格式错误! " ControlToValidate="txtproductPrice" 
                    Display="None" ValidationExpression="^[+-]?(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td class="oddrow">物品单位:</td>
        <td class="oddrow-l"><asp:TextBox ID="txtproductUnit" runat="server" MaxLength="100"  Width="200px"  /></td>
        <td class="oddrow">是否显示:</td>
        <td class="oddrow-l"><asp:RadioButtonList ID="rdlShow" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Text="是" Value="1" Selected></asp:ListItem>
            <asp:ListItem Text="否" Value="0"></asp:ListItem></asp:RadioButtonList></td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">物料类别:</td>
        <td class="oddrow-l" colspan="3">
            <asp:DropDownList ID="ddltype" runat="server" Width="100px" />
            &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
            &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
            <asp:HiddenField ID="hidtype" runat="server" />
            <asp:HiddenField ID="hidtype1" runat="server" />
            <asp:HiddenField ID="hidtype2" runat="server" />
            &nbsp;<font color="red">*</font>
            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddltype2" ValueToCompare="-1"
                Display="None" Operator="notequal" runat="server" ErrorMessage="请选择物料类别"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="oddrow">物品描述:</td>
        <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtproductDes" runat="server" TextMode="MultiLine"  Width="840px" Height="50px"  /></td>
    </tr>
    <tr>
        <td colspan="4" align="right">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
            <input runat="server" id="txtSave" value=" 保存  "  type="button" causesvalidation="true" class="widebuttons" onserverclick="btnSave_Click" />&nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " OnClick="btnBack_Click"  CausesValidation="false" CssClass="widebuttons" /></td>
    </tr>
</table>

</asp:Content>

