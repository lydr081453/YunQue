<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Edit_supplierInfo"
    CodeBehind="supplierInfo.ascx.cs" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
    <link href="../../public/css/dialog.css" type="text/css" rel="stylesheet" />

<script language="javascript" src="../../public/js/jquery-1.2.6.js"></script>

<script language="javascript" src="../../public/js/dialog.js"></script>
<script language="javascript">
    function SupplierClick() {
        var generalid = '<%= Request[RequestName.GeneralID]%>';
        var win = window.open('SupplierList.aspx?<% = RequestName.GeneralID%>=' + generalid + '&name=' + document.getElementById("<%= txtsupplier_name.ClientID %>").value, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function setsource(con) {
        var source = con.options[con.selectedIndex].value;
        document.getElementById('<%= txtsource.ClientID%>').value = source;
        if (source == "客户指定") {
            alert('请上传客户指定邮件附件！');
            document.getElementById('<%=divEmail.ClientID %>').style.display = "block";
        } else {
            document.getElementById('<%=divEmail.ClientID %>').style.display = "none";
        }
    }

    function chkEmailFile(sender, args) {
        var div = document.getElementById('<%=divEmail.ClientID %>');
        var lab = document.getElementById('<%= labEmailFile.ClientID %>');
        var file = document.getElementById("<%= filEmailFile.ClientID %>");
        if (div.style.display == "block" && lab.innerHTML == "" && file.value == "") {
            args.IsValid = false;
        }else
            args.IsValid = true;
    }
    

    function showUserList(gid) {
        dialog("查看联系人", "iframe:/Purchase/Requisition/PrLinkManEditList.aspx?gid=" + gid, "1000px", "500px", "text");
    }
    function onPageRefresh() {
        window.location.reload();
    }
</script>
<style type="text/css">
.ddn-con { width:188px;height:auto;background-color:#f6f6f6; }
.ddn { cursor:default;color:#666;font-family:Tahoma,"Lucida Grande",Verdana,Arial,Helvetica,sans-serif;font-size:11px;border:1px solid #000;z-index:20000 !important; }
.ddn-item { width:182px;padding-left:8px;line-height:22px;background:none; }
.ddn-item-hover { background:transparent url("images/static.png") no-repeat;width:182px;padding-left:8px;line-height:22px;color:#000; }
.cmb-hover { background-color:#fff; }
</style>
<asp:ScriptManager ID="Ma" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="U1" runat="server">
    <ContentTemplate>
        <table width="100%" class="tableForm">
            <tr id="trTitle" runat="server">
                <td class="heading" colspan="4">
                    ④ 供应商信息
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    审批流向:
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:RadioButtonList ID="rblrequisitionflow" runat="server" RepeatDirection="horizontal"
                        AutoPostBack="true" OnSelectedIndexChanged="rbl_SelectedIndexChanged" CssClass="XTable">
                    </asp:RadioButtonList>
                    <br />
                    <span style="color: Red">
                        PR-PO: 选择PR-PO, 系统自动生成订单(PO). 若供应商提供邮件地址的, 系统发出PO至供应商处进行确认, 同时附我司订单标准条款. 订单方式流转速度快,
                        操作简单. 适用于多数情况.<br />
                        PR-合同: 当需要签署采购合同时,选择PR-合同. 合同需要进行线下签署,合同号与本申请单号码相同,以便检索查询. <br />
                        PR-框架合同:当与供应商签署框架合同时，需要填写押金金额，采购物品金额为零，账期系统自动录入.
                    </span>
                </td>
            </tr>
<%--            <tr>
                <td class="oddrow">
                    供应商类型:
                </td>
                <td class="oddrow-l" colspan="3">
                    <table border="0" class="XTable">
                        <tr><td><asp:RadioButtonList ID="radOperationType" CssClass="XTable" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radOperationType_SelectIndexChanged">
                    </asp:RadioButtonList><asp:Label ID="labOperationType" runat="server" /></td><td>&nbsp;<asp:CheckBox ID="chkHaveInvoice" runat="server" Text="是否存在发票" Visible="false" /></td></tr>
                    </table>
                    * <span style='color: red; font-weight: bold;'>对公与个人选择非常关键，请务必仔细确认，填写错误会影响您的后续流程！</span><br />
                </td>
            </tr>--%>
            <tr>
                            <td class="oddrow" style="width: 15%">
                    供应商来源:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <table width="100%" class="XTable">
                        <tr>
                            <td style="width: 130px">
                                <asp:Label ID="labsource" runat="server" /><asp:TextBox ID="txtsource" runat="server"></asp:TextBox>
                                <asp:DropDownList ID="ddlsource" runat="server" onchange="setsource(this);">
                                    <asp:ListItem Value="0">请选择</asp:ListItem>
                                    <asp:ListItem>业务推荐</asp:ListItem>
                                    <asp:ListItem>客户指定</asp:ListItem>
                                    <asp:ListItem>采购部推荐</asp:ListItem>
                                    <asp:ListItem>临时供应商</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <font color="red">* </font>
                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ddlsource" ValueToCompare="0"
                                    Operator="NotEqual" runat="server" Display="None" ErrorMessage="请选择供应商来源">
                                </asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="oddrow" style="width: 15%">
                    <asp:Label ID="labSNShow" runat="server">供应商名称</asp:Label>:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <table width="100%" class="XTable">
                        <tr>
                            <td style="width: 98%">
                                <asp:Label ID="labsupplier_name" runat="server" />
                                <ComponentArt:ComboBox ID="txtsupplier_name" runat="Server" Width="192" Height="20" AutoHighlight="false"
                                AutoPostBack="true" OnSelectedIndexChanged="supplier_OnSelectedIndexChanged"
                                ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" HoverCssClass="cmb-hover"
                                AutoComplete="true" AutoFilter="true" DataTextField="supplier_name" DataValueField="id"
                                DropDownResizingMode="bottom" DropDownWidth="190" DropDownHeight="300" DropDownCssClass="ddn"
                                DropDownContentCssClass="ddn-con">
                                <DropDownFooter>
                                    <div class="ddn-ftr">
                                    </div>
                                </DropDownFooter>
                            </ComponentArt:ComboBox><%--<asp:TextBox ID="txtsupplier_name"
                                    Width="100%" runat="server" />--%><asp:Button ID="btn" runat="server" Visible="false"
                                        OnClientClick="SupplierClick();return false;" Text="请选择..." CssClass="widebuttons" />
                            </td>
                            <td>
                                <font color="red">* </font>
                                <asp:HiddenField ID="hidsupplierType" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsupplier_name"
                                    Display="None" ErrorMessage="请填写供应商名称"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr>
                <td class="oddrow">
                    <asp:Label ID="labSAShow" runat="server">供应商地址</asp:Label>:
                </td>
                <td class="oddrow-l" style="padding-left: 5px">
                    <table width="100%" class="XTable">
                        <tr>
                            <td style="width: 98%">
                                <asp:Label ID="labsupplier_address" runat="server" /><asp:TextBox ID="txtsupplier_address"
                                    Width="100%" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server" ControlToValidate="txtsupplier_address" Display="None" ErrorMessage="请填写供应商地址"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <font color="red">* </font>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="oddrow">
                    客户指定邮件附件:
                </td>
                <td class="oddrow-l">
                    <%--<asp:Panel ID="palEmailFile" runat="server" Visible="false">--%>
                    <div id="divEmail" runat="server" style="display: none">
                        <asp:FileUpload ID="filEmailFile" runat="server" /><font color="red"> * </font>
                        <asp:Label ID="labEmailFile" runat="server" /></div>
                    <%--</asp:Panel>--%>
                    <asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="chkEmailFile" ErrorMessage="请上传客户指定邮件附件" Display="None" />
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    供应商联系电话:
                </td>
                <td class="oddrow-l">
                    <table border="0" cellpadding="0" cellspacing="0" class="XTable">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtsupplier_con" runat="server" />
                                <%--<asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator2" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_con"
                                    Display="Dynamic" ValidationExpression="^\d{2,4}$"></asp:RegularExpressionValidator>--%>
                            </td>
                           <%-- <td>
                                <asp:TextBox ID="txtsupplier_area" runat="server" Width="30px" /><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator4" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_area"
                                    Display="Dynamic" ValidationExpression="^\d{2,5}$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox Width="60px" ID="txtsupplier_phone" runat="server" /><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator7" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_phone"
                                    Display="Dynamic" ValidationExpression="^\d{6,8}$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtsupplier_ext" runat="server" Width="30px" /><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator18" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_ext"
                                    Display="Dynamic" ValidationExpression="^\d{2,4}$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Label ID="labsupplier_phone" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server" ControlToValidate="txtsupplier_phone" Display="None" ErrorMessage="请填写供应商联系电话"></asp:RequiredFieldValidator>
                            </td>--%>
                            <td>
                                <%--<font color="red">* </font>--%>
                            </td>
                        </tr>
<%--                        <tr>
                            <td colspan="6">
                                例:86-010-85078888-0001
                            </td>
                        </tr>--%>
                    </table>
                </td>
                <td class="oddrow">
                    供应商联系人:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labsupplier_linkman" runat="server" /><asp:TextBox ID="txtsupplier_linkman"
                        runat="server" />
                        <input type="button"  class="widebuttons" value="更换" ID="btnEditUser" runat="server"  />
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    供应商传真:
                </td>
                <td class="oddrow-l">
                    <table border="0" cellpadding="0" cellspacing="0" class="XTable">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtsupplierfax_con" runat="server" />
                                <%--<asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator10" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_con"
                                    Display="Dynamic" ValidationExpression="^\d{2,5}$"></asp:RegularExpressionValidator>--%>
                            </td>
                           <%-- <td>
                                <asp:TextBox ID="txtsupplierfax_area" runat="server" Width="30px" /><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator14" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_area"
                                    Display="Dynamic" ValidationExpression="^\d{2,4}$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox Width="60px" ID="txtsupplierfax_phone" runat="server" /><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator15" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_phone"
                                    Display="Dynamic" ValidationExpression="^\d{6,8}$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtsupplierfax_ext" runat="server" Width="30px" /><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator19" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_ext"
                                    Display="Dynamic" ValidationExpression="^\d{2,4}$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Label ID="labsupplier_fax" runat="server" />
                            </td>--%>
                        </tr>
<%--                        <tr>
                            <td colspan="5">
                                例:86-010-85078888-0001
                            </td>
                        </tr>--%>
                    </table>
                </td>
                <td class="oddrow">
                    供应商手机:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labsupplier_cellphone" runat="server" />
                    <asp:TextBox ID="txtsupplier_cellphone" runat="server" Width="200px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtsupplier_cellphone"
                        Display="Dynamic" ErrorMessage="供应商手机格式错误" ValidationExpression="(\d)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    框架协议号码:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labfa_no" runat="server" />&nbsp;<asp:TextBox ID="txtfa_no" runat="server" />
                </td>
                <td class="oddrow">
                    供应商邮件:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labsupplier_email" runat="server" /><asp:TextBox ID="txtsupplier_email"
                        runat="server" Width="200px" />
                    <asp:Label ID="labStar" runat="server" Text=" * " ForeColor="Red" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtsupplier_email"
                        Display="None" ErrorMessage="请填写供应商邮件"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                        ControlToValidate="txtsupplier_email" Display="Dynamic" ErrorMessage="供应商邮件格式错误"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>例:mail@sohu.com
                </td>
            </tr>
        </table>

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="6">
            帐户信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 10%">
            开户公司名称:
        </td>
        <td class="oddrow-l" style="width: 23%">
            <asp:TextBox ID="txtaccountName" runat="server" /><asp:RequiredFieldValidator Text=" * "
                ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtaccountName"
                Display="Dynamic" ErrorMessage="请填写开户公司名称" />
        </td>
        <td class="oddrow" style="width: 10%">
            开户银行:
        </td>
        <td class="oddrow-l" style="width: 23%">
            <asp:TextBox ID="txtaccountBank" runat="server" /><asp:RequiredFieldValidator Text=" * "
                ForeColor="Red" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtaccountBank"
                Display="Dynamic" ErrorMessage="请填写开户银行" />
        </td>
        <td class="oddrow" style="width: 10%">
            帐号:
        </td>
        <td class="oddrow-l" style="width: 23%">
            <asp:TextBox ID="txtaccountNum" runat="server" /><asp:RequiredFieldValidator Text=" * "
                ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtaccountNum"
                Display="Dynamic" ErrorMessage="请填写帐号" />
        </td>
    </tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>
<br />
<asp:Panel ID="palView" runat="server">
    <table width="100%" class="tableForm" style="color: black">
        <tr id="tr1" runat="server">
            <td class="heading" colspan="4" style="color: black">
                预览--请确认您填写的供应商信息的正确性，错误信息可能导致无法付款，请慎重填写
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="color: black">
                供应商类型:
            </td>
            <td class="oddrow-l" colspan="3" style="color: black">
                <asp:Label ID="labOperationType1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%" style="color: black">
                <asp:Label ID="labSNShow1" runat="server">供应商名称</asp:Label>:
            </td>
            <td class="oddrow-l" style="width: 35%" style="color: black">
                <asp:Label ID="labsupplier_name1" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%" style="color: black">
                供应商来源:
            </td>
            <td class="oddrow-l" style="width: 35%" style="color: black">
                <asp:Label ID="labsource1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="color: black">
                <asp:Label ID="labSAShow1" runat="server">供应商地址</asp:Label>:
            </td>
            <td class="oddrow-l" style="padding-left: 5px" style="color: black">
                <asp:Label ID="labsupplier_address1" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="color: black">
                客户指定邮件附件:
            </td>
            <td class="oddrow-l" style="color: black">
                <asp:Label ID="labEmailFile1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="color: black">
                供应商联系电话:
            </td>
            <td class="oddrow-l" style="color: black">
                <asp:Label ID="labsupplier_phone1" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="color: black">
                供应商联系人:
            </td>
            <td class="oddrow-l" style="color: black">
                <asp:Label ID="labsupplier_linkman1" runat="server"></asp:Label>
                <input type="button"  class="widebuttons" value="更换" ID="btnEditUser1" runat="server"  />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="color: black">
                供应商传真:
            </td>
            <td class="oddrow-l" style="color: black">
                <asp:Label ID="labsupplier_fax1" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="color: black">
                供应商手机:
            </td>
            <td class="oddrow-l" style="color: black">
                <asp:Label ID="labsupplier_cellphone1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="color: black">
                框架协议号码:
            </td>
            <td class="oddrow-l" style="color: black">
                <asp:Label ID="labfa_no1" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="color: black">
                供应商邮件:
            </td>
            <td class="oddrow-l" style="color: black">
                <asp:Label ID="labsupplier_email1" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="6" style="color: black">
                帐户信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%" style="color: black">
                开户公司名称:
            </td>
            <td class="oddrow-l" style="width: 23%" style="color: black">
                <asp:Label ID="labaccountName1" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%" style="color: black">
                开户银行:
            </td>
            <td class="oddrow-l" style="width: 23%" style="color: black">
                <asp:Label ID="labaccountBank1" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 10%" style="color: black">
                帐号:
            </td>
            <td class="oddrow-l" style="width: 23%" style="color: black">
                <asp:Label ID="labaccountNum1" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
