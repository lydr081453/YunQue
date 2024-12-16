<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_Filiale_RequisitionEdit" CodeBehind="Filiale_RequisitionEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register src="../../UserControls/Edit/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/Edit/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfoV"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/Edit/productInfo.ascx" tagname="productInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/paymentInfo.ascx" tagname="paymentInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/View/operationAuditLog.ascx" tagname="operationAuditLog"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/auditLog.ascx" tagname="auditLog" tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <script language="javascript">
        function EmplyeeClick1(type, con) {
            if (document.getElementById("ctl00_ContentPlaceHolder1_genericInfo_" + con) != null) {
                var name = document.getElementById("ctl00_ContentPlaceHolder1_genericInfo_" + con).value;
                var win = window.open('EmployeeList.aspx?clientId=ctl00_ContentPlaceHolder1_genericInfo_&name=' + name + '&type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            } else {
                var name = "";
                var win = window.open('EmployeeList.aspx?name=' + name + '&type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            }
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function settype(con) {
            document.getElementById("ctl00_ContentPlaceHolder1_txtsupplier_type").value = con.options[con.selectedIndex].value;
        }
        function selectone() {
            var Element = document.getElementsByName("chkRep");
            hidChkID = document.getElementById("<% = hidChkID.ClientID %>");
            hidChkID.value = "";
            var ids = "";
            for (var j = 0; j < Element.length; j++) {
                if (Element[j].checked) {
                    ids += Element[j].value + ",";
                    hidChkID.value += Element[j].value + ",";
                }
            }
            ids = ids.substring(0, ids.length - 1);
            hidChkID.value = ids;
            if (ids != "") {
                return ids;
            } else {
                alert("请选择需要复制的采购物品");
                return false;
            }
        }
        function showdivafterwards(viewer) {
            document.getElementById("<%=divafterwards.ClientID %>").style.display = viewer;
        }
        function showdivEmBuy(viewer) {
            document.getElementById("<%=divEmBuy.ClientID %>").style.display = viewer;
        }
        function showdivCusAskYes(viewer) {
            document.getElementById("<%=divCusAskYes.ClientID %>").style.display = viewer;
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="RequisitionSaveList.aspx">返回申请单信息浏览</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--        项目号申请单信息          --%>
                <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
                <%--        项目号申请单信息          --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:genericInfo ID="genericInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--        采购物品信息          --%>
                <uc1:productInfo ID="productInfo" runat="server" />
                <%--        采购物品信息          --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:supplierInfo ID="supplierInfo" runat="server" />
                <uc1:supplierInfoV ID="supplerInfoV" runat="server" Visible="false" />
            </td>
        </tr>
                <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            账期设定
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">
                            账期类型:
                        </td>
                        <td class="oddrow-l"  colspan="3">
                            <asp:DropDownList ID="ddlPeriodType" runat="server">
                                <asp:ListItem Selected="True" Text="固定式账期" Value="0"></asp:ListItem>
                                <asp:ListItem Text="开放式账期" Value="1"></asp:ListItem>
                            </asp:DropDownList>注：<font color="red">固定式账期在申请付款时以账期做为唯一的参考依据；开放式账期则按照实际发生金额为准，但不能超出申请单总金额。</font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--       付款账期信息         --%>
                <uc1:paymentInfo ID="paymentInfo" runat="server" />
                <%--       付款账期信息         --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:RequirementDescInfo ID="RequirementDescInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            审核信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            订单编号:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label ID="txtorderid" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            谈判类型:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Value="垫付类">垫付类</asp:ListItem>
                                <asp:ListItem Value="谈判类">谈判类</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            比价节约:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtcontrast" runat="server" Width="200px"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtcontrast"
                                Display="Dynamic" ErrorMessage="比价节约金额格式错误" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator><br />
                            <asp:FileUpload ID="upContrast" runat="server" />&nbsp;<asp:Label ID="labdownContrast"
                                runat="server" />&nbsp;<asp:CheckBox ID="chkContrast" runat="server" Text="<img src='/images/ico_05.gif' border='0' />" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            议价节约:
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtconsult" runat="server" Width="200px"></asp:TextBox><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtconsult"
                                Display="Dynamic" ErrorMessage="议价节约金额格式错误" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator><br />
                            <asp:FileUpload ID="upConsult" runat="server" />&nbsp;<asp:Label ID="labdownConsult"
                                runat="server" />&nbsp;<asp:CheckBox ID="chkConsult" runat="server" Text="<img src='/images/ico_05.gif' border='0' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            初审人:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtfirst_assessor" runat="server" Width="200px" /><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirst_assessor"
                                ErrorMessage="初审人为必填" Display="None"></asp:RequiredFieldValidator><font color="red">
                                    * </font>
                            <asp:HiddenField ID="hidfirst_assessor" runat="server" />
                            <input type="button" value="变更" class="widebuttons" onclick="return EmplyeeClick1('first_assessor','txtfirst_assessor');" />
                        </td>
                        <td class="oddrow" style="width: 20%;">
                            事后申请:
                        </td>
                        <td class="oddrow-l">
                            <asp:RadioButtonList ID="radafterwards" runat="server" RepeatDirection="horizontal"
                                CssClass="XTable" BorderStyle="None">
                                <asp:ListItem Value="是" onclick="showdivafterwards('block');">是</asp:ListItem>
                                <asp:ListItem Value="否" Selected="true" onclick="showdivafterwards('none');">否</asp:ListItem>
                            </asp:RadioButtonList>
                            <div id="divafterwards" runat="server" style="display: none">
                                理由：<asp:TextBox ID="txtafterwardsReason" runat="server" Width="200px" />
                            </div>
                        </td>
                    </tr>
                    <tr id="trFiliale" runat="server">
                    <td class="oddrow" style="width: 15%">
                            分公司审核人:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtFiliale" runat="server" Width="200px" />
                            <asp:HiddenField ID="hidFiliale" runat="server" />
                            <input type="button" value="变更" class="widebuttons" onclick="return EmplyeeClick1('filiale','txtFiliale');" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%;">
                            紧急采购:
                        </td>
                        <td class="oddrow-l">
                            <asp:RadioButtonList ID="radEmBuy" runat="server" RepeatDirection="horizontal" BorderStyle="None"
                                CssClass="XTable">
                                <asp:ListItem Value="是" onclick="showdivEmBuy('block');">是</asp:ListItem>
                                <asp:ListItem Value="否" Selected="true" onclick="showdivEmBuy('none');">否</asp:ListItem>
                            </asp:RadioButtonList>
                            <div id="divEmBuy" runat="server" style="display: none">
                                理由：<asp:TextBox ID="txtEmBuyReason" runat="server" Width="200px" />
                            </div>
                        </td>
                        <td class="oddrow" style="width: 20%;">
                            客户指定:
                        </td>
                        <td class="oddrow-l">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0" cssclass="XTable">
                                <tr>
                                    <td style="padding: 0px">
                                        <table border="0" cellpadding="0" cellspacing="0" class="XTable">
                                            <tr>
                                                <td align="left">
                                                    <input id="CusAskYes" type="radio" runat="server" value="是" onclick="showdivCusAskYes('block');" />
                                                    <label for="<% = CusAskYes.ClientID %>">
                                                        是</label>&nbsp;
                                                    <input id="CusAskNo" type="radio" runat="server" checked="true" value="否" onclick="showdivCusAskYes('none');" />
                                                    <label for="<% = CusAskNo.ClientID %>">
                                                        否</label>&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divCusAskYes" runat="server" style="display: none">
                                                        客户名称：<asp:TextBox ID="txtCusName" runat="server" Width="200px" /><br />
                                                        理 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;由：<asp:TextBox ID="txtCusAskYesReason" runat="server"
                                                            Width="200px" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%;">
                            合同号码:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtContractNo" runat="server" Width="200px" />
                        </td>
                        <td class="oddrow" style="width: 20%;">
                            其它:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtothers" runat="server" Width="200px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:operationAuditLog ID="operationAuditLog" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td style="width: 15%" class="oddrow">
                            最后编辑时间:
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:Label ID="lablasttime" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="palOverrule" runat="server" Visible="false">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="2" class="heading" style="height: 1px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                申请单审批备注:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labOverrule" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <uc1:auditLog ID="auditLog" runat="server" />
    <table style="width: 100%;" class="tableForm" id="tabOverrule">
        <tr>
            <td style="width: 15%" class="oddrow">
                分公司审核备注:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtoverrule" TextMode="multiLine" Height="100px" Width="400px" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtoverrule"
                    ErrorMessage="分公司审核备注为必填" Display="None"></asp:RequiredFieldValidator><font color="red">*</font>
            </td>
        </tr>
        <td class="oddrow">
            下级分公司审核人:
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtNextFili" runat="server" onfocus="javascript:this.blur();" />&nbsp;<asp:HiddenField
                ID="hidNextFili" runat="server" />
            <input type="button" value="变更初审人" class="widebuttons" onclick="return EmplyeeClick1('NextFili','txtNextFili');" />
        </td>
    </table>
    <table width="100%">
        <tr>
            <td height="25" colspan="2" align="center">
                <prc:CheckPRInputButton runat="server" ID="btnSave" value=" 保存 " type="button" onclick="this.disabled=true;"
                    CausesValidation="false" class="widebuttons" OnServerClick="btnSave_Click" />
                &nbsp;<prc:CheckPRInputButton runat="server" ID="btnAdd" value="审核通过" type="button"
                    onclick="if(confirm('您确定要审核通过吗？')){this.disabled=true;}else{return false;}" CausesValidation="false"
                    class="widebuttons" OnServerClick="btnAdd_Click" />
                &nbsp;<prc:CheckPRInputButton runat="server" ID="btnReturn" value="审核驳回" type="button"
                    onclick="if(confirm('您确定要审核驳回吗？')){this.disabled=true;}else{return false;}"
                    CausesValidation="false" class="widebuttons" OnServerClick="btnReturn_Click" />
                &nbsp;<asp:Button ID="btnCancel" CssClass="widebuttons" CausesValidation="false"
                    PostBackUrl="~/Purchase/Requisition/FilialeAuditList.aspx" runat="server" Text=" 返回 ">
                </asp:Button>
            </td>
        </tr>
    </table>
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
    <asp:HiddenField ID="hidisMajordomoUndo" runat="server" />

    <script language="javascript">
            function commitClick() {
                var value = document.getElementById("<% =hidisMajordomoUndo.ClientID %>").value
                if (Page_ClientValidate()) {
                    if (confirm('您确定要审核通过吗？')) {
                        if (value != "") {
                            if (confirm('是否需要订单变更？\n\n[确定]-需要，[取消]-不需要。')) {
                                document.getElementById("<% =hidisMajordomoUndo.ClientID %>").value = "yes";
                            } else {
                                document.getElementById("<% =hidisMajordomoUndo.ClientID %>").value = "no";
                            }
                        }
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return false;
                }
            }
    </script>

</asp:Content>
