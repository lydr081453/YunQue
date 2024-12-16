<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_ModifyProducts" Codebehind="ModifyProducts.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
<script language="javascript">
    function account() {
        var num = document.getElementById("<%=quantity.ClientID %>").value;
        if (document.all) {
            var price = document.getElementById("<%=txtPrice.ClientID %>").value.replace(/,/g, "");
            document.getElementById("<%=labtotal.ClientID %>").innerText = setCurrency(parseFloat(price) * num + "");
        } else {
            var price = document.getElementById("<%=txtPrice.ClientID %>").value.replace(/,/g, "");
            document.getElementById("<%=labtotal.ClientID %>").textContent = setCurrency(parseFloat(price) * num + "");
        }
    }

    function setCurrency(s) {
        if (s == "NaN")
            return "";
        var prefix;
        var str;
        if (s.substring(0, 1) == "-" || s.substring(0, 1) == "+") {
            prefix = s.substring(0, 1);
            str = s.substring(1, s.length);
        } else {
            str = s;
            prefix = "";
        }
        if (/[+-][^0-9\.]/.test(str)) {
            return "";
        }
        str = str.replace(/^(\d*)$/, "$1.");
        str = (str + "0000").replace(/(\d*\.\d\d\d\d)\d*/, "$1");
        str = str.replace(".", ",");
        var re = /(\d)(\d{3},)/;
        while (re.test(str)) {
            str = str.replace(re, "$1,$2");
        }
        str = str.replace(/,(\d\d\d\d)$/, ".$1");
        return prefix + str.replace(/^\./, "0.")
    }

    function changeSupplier() {
        var generalid = '<%= Request[RequestName.GeneralID] %>';
        var win = window.open('ChangedSupplier.aspx?<% =RequestName.GeneralID %>=' + generalid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function changeProductType() {
        var generalid = '<%= Request[RequestName.GeneralID] %>';
        var win = window.open('ModifyProductType.aspx?<% = RequestName.GeneralID%>=' + generalid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setSName2(name, supplyId) {
        if (document.all)
            document.getElementById("<%=labSupplierName.ClientID %>").innerText = name;
        else
            document.getElementById("<%=labSupplierName.ClientID %>").textContent = name;
        document.getElementById("<%=hidSupplierName.ClientID %>").value = name;
        document.getElementById("<%=hidSupplyId.ClientID %>").value = supplyId;
        document.getElementById("<%=hidSupplierId.ClientID %>").value = "";
        document.getElementById("<%= lnkShowFX.ClientID %>").click();
    }

    function setSName1(name, id) {
        if (document.all)
            document.getElementById("<%=labSupplierName.ClientID %>").innerText = name;
        else
            document.getElementById("<%=labSupplierName.ClientID %>").textContent = name;
        document.getElementById("<%=hidSupplierName.ClientID %>").value = name;
        document.getElementById("<%=hidSupplierId.ClientID %>").value = id;
        document.getElementById("<%=hidSupplyId.ClientID %>").value = "";
        document.getElementById("<%= lnkShowFX.ClientID %>").click();
    }
    function setSName(name) {
        if (document.all)
            document.getElementById("<%=labSupplierName.ClientID %>").innerText = name;
        else
            document.getElementById("<%=labSupplierName.ClientID %>").textContent = name;
        document.getElementById("<%=hidSupplierName.ClientID %>").value = name;
        document.getElementById("<%=hidSupplierId.ClientID %>").value = "0";
    }
    function showPriceAtt(generalid, orderid) {
        //        alert(generalid);
        var ids = document.getElementById("ctl00_ContentPlaceHolder1_hidIds").value;
        var atts = document.getElementById("ctl00_ContentPlaceHolder1_hidNames").value;
        var msgid = document.getElementById("ctl00_ContentPlaceHolder1_txtPriceAtt").value;
        //                  msgid = "aaa";
        msgid = msgid.trim();
        if (msgid == "") {
            //            alert('请填写批号！');
            //            alert(orderid); 
            //            alert(generalid);
            dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?ids=" + ids + "&atts=" + atts + "&page=mod&orderid=" + orderid + "&generalid=" + generalid, "1000px", "480px", "text");
        }
        else if (msgid.indexOf("#") != 0 || msgid.length < 10) {
            alert("填写批号有误，请重新填写！");
        }
        else {
            msgid = msgid.replace("#", "");
            tmp = msgid.substring(0, 8);
            msgid = msgid.replace(tmp, "");
            //            msgid = msgid.replace(msgid.substring(0, 8), "");
            if (!isNaN(msgid)) {
                dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?ids=" + ids + "&atts=" + atts + "&msgid=" + msgid + "&page=mod&orderid=" + orderid + "&generalid=" + generalid, "1000px", "480px", "text");
            }
            else {
                alert("填写批号有误，请重新填写！");
            }
        }
    }
    function chkEmailFile(sender, args) {
        var div = document.getElementById('<%=divEmail.ClientID %>');
        var lab = document.getElementById('<%= labEmailFile.ClientID %>');
        var file = document.getElementById("<%= filEmailFile.ClientID %>");
        if (div.style.display == "block" && lab.innerHTML == "" && file.value == "") {
            args.IsValid = false;
        } else
            args.IsValid = true;
    }
    function setsource(con) {
        var source = con.options[con.selectedIndex].value;
        if (source == "客户指定") {
            alert('请上传客户指定邮件附件！');
            document.getElementById('<%=divEmail.ClientID %>').style.display = "block";
        } else {
            document.getElementById('<%=divEmail.ClientID %>').style.display = "none";
        }
    }
    //    function checkPriceAtt() { 
    //    
    //    }
</script>
<script language="javascript" src="../../public/js/DatePicker.js"></script>
<asp:LinkButton ID="lnkShowFX" runat="server" OnClick="lnkShowFX_Click" CausesValidation="false" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="oddrow" style="width: 20%">
                供应商名称:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labSupplierName" runat="server" /><asp:HiddenField ID="hidSupplyId" runat="server" /> <asp:HiddenField ID="hidSupplierId"
                    runat="server" /><asp:HiddenField ID="hidSupplierName" runat="server" />&nbsp;
                   <%-- <asp:Button ID="btnChange" runat="server" CausesValidation="false" Text="变更供应商" CssClass="widebuttons" OnClientClick="changeSupplier();return false;" />--%>
            </td>
        </tr>
                                <tr>
                            <td colspan="2" class="oddrow-l">
                                <asp:Panel ID="palFX" runat="server" Visible="false">
                                    <table width="100%" class="tableForm">
                                        <tr>
                                            <td class="oddrow" style="width: 20%">
                                                审批流向:
                                            </td>
                                            <td class="oddrow-l">
                                                <asp:RadioButtonList ID="rblrequisitionflow" runat="server" RepeatDirection="horizontal"
                                                    CssClass="XTable">
                                                </asp:RadioButtonList>
                                                <br />
                                                <span style="color: Red">PR-PR : 一般适用于供应商或收款单位为个人, 且采购订单或合同的不适用的情况.
                                                    <br />
                                                    PR-PO: 选择PR-PO, 系统自动生成订单(PO). 若供应商提供邮件地址的, 系统发出PO至供应商处进行确认, 同时附"宣亚订单标准条款1.0". 订单方式流转速度快,
                                                    操作简单. 适用于多数情况.<br />
                                                    PR-合同: 当需要签署采购合同时,选择PR-合同. 合同需要进行线下签署,合同号与本申请单号码相同,以便检索查询. </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow">
                                                供应商类型:
                                            </td>
                                            <td class="oddrow-l">
                                                <table border="0" class="XTable">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="radOperationType" CssClass="XTable" runat="server" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="radOperationType_SelectIndexChanged" AutoPostBack="true">
                                                            </asp:RadioButtonList>
                                                            <asp:Label ID="labOperationType" runat="server" />
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Panel ID="palGR" runat="server" Visible="false">
                                                                <asp:CheckBox ID="chkHaveInvoice" runat="server" Text="是否存在发票" />
                                                                &nbsp;身份证：<asp:TextBox ID="txtCardNum" runat="server" /><font color="red"> * </font>
                                                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator13"
                                                                    runat="server" ControlToValidate="txtCardNum" Display="None" ErrorMessage="请填写身份证" /></asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                * <span style='color: red; font-weight: bold;'>对公与个人选择非常关键，请务必仔细确认，填写错误会影响您的后续流程！</span><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow">供应商来源:</td>
                                            <td class="oddrow-l">
                                               <asp:DropDownList ID="ddlsource" runat="server" onchange="setsource(this);">
                                                    <asp:ListItem Value="0">请选择</asp:ListItem>
                                                    <asp:ListItem>业务推荐</asp:ListItem>
                                                    <asp:ListItem>客户指定</asp:ListItem>
                                                    <asp:ListItem>采购部推荐</asp:ListItem>
                                                    <asp:ListItem>临时供应商</asp:ListItem>
                                                </asp:DropDownList>
                                                        <font color="red"> * </font>
                                            <asp:CompareValidator ID="CompareValidator3" ControlToValidate="ddlsource" ValueToCompare="0"
                                                Operator="NotEqual" runat="server" Display="None" ErrorMessage="请选择供应商来源">
                                            </asp:CompareValidator>
                                                                <div id="divEmail" runat="server" style="display: none">客户指定邮件:
                        <asp:FileUpload ID="filEmailFile" runat="server" /><font color="red"> * </font>
                        <asp:Label ID="labEmailFile" runat="server" /></div>
                        <asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="chkEmailFile" ErrorMessage="请上传客户指定邮件附件" Display="None" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow">
                                                开户公司名称:
                                            </td>
                                            <td class="oddrow-l">
                                                <asp:TextBox ID="txtaccountName" runat="server" Width="270" /><font color="red"> *
                                                </font>
                                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator10"
                                                    runat="server" ControlToValidate="txtaccountName" Display="None" ErrorMessage="请填写开户公司名称" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow">
                                                开户银行:
                                            </td>
                                            <td class="oddrow-l">
                                                <asp:TextBox ID="txtaccountBank" runat="server" Width="270" /><font color="red"> *
                                                </font>
                                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator11"
                                                    runat="server" ControlToValidate="txtaccountBank" Display="None" ErrorMessage="请填写开户银行" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="oddrow">
                                                帐号:
                                            </td>
                                            <td class="oddrow-l">
                                                <asp:TextBox ID="txtaccountNum" runat="server" Width="270" /><font color="red"> *
                                                </font>
                                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator12"
                                                    runat="server" ControlToValidate="txtaccountNum" Display="None" ErrorMessage="请填写帐号" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
        <tr>
            <td class="oddrow">
                物料类别:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labProductType" runat="server" /><asp:HiddenField ID="hidProductTypeId"
                    runat="server" />&nbsp;
                    <%--<asp:Button ID="Button1" runat="server" CausesValidation="false" Text="变更物料类别" CssClass="widebuttons" OnClientClick="changeProductType();return false;" />--%>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                采购物品:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                具体描述:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="desctiprtion" runat="server" TextMode="MultiLine" Height="100px"
                    Width="260"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                预计收货时间:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="intend_receipt_date"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="intend_receipt_date" Display="None" ErrorMessage="预计收货时间为必填" /><font
                                                color="red"> * </font><img src="../../images/dynCalendar.gif"
                    border="0" onclick="popUpCalendar(document.getElementById('<%=intend_receipt_date.ClientID %>'), document.getElementById('<%=intend_receipt_date.ClientID %>'), 'yyyy-mm-dd');" />
                 - <asp:TextBox runat="server" ID="Eintend_receipt_date" onfocus="javascript:this.blur();"></asp:TextBox>&nbsp;<img
                                        src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=Eintend_receipt_date.ClientID %>'), document.getElementById('<%=Eintend_receipt_date.ClientID %>'), 'yyyy-mm-dd');" />        
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                单价:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPrice" runat="server" Width="130" onblur="account();" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPrice"
                    Display="None" ErrorMessage="单价为必填" /><font color="red"> * </font>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPrice"
                    ErrorMessage="请输入正确单价" ValidationExpression="^[+-]?(\d{1,3},?)+(\.\d+)?$">请输入正确单价</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                货币:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labMoneyType" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                单位:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="labUnit" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="labUnit"
                    Display="None" ErrorMessage="单位为必填" /><font color="red"> * </font>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                数量:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="quantity" runat="server" Width="130" onblur="account();" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                    runat="server" ControlToValidate="quantity" Display="None" ErrorMessage="数量为必填" /><font
                        color="red">*</font>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="quantity"
                    ErrorMessage="请输入正确数量" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确数量</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                小计:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labtotal" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <%--报价附件:--%>报价信息:
            </td>
            <td class="oddrow-l">
<%--                <asp:FileUpload ID="fil" runat="server"
                    Width="250" />&nbsp;供应商报价&nbsp;<asp:Label ID="labdown" runat="server" />&nbsp;<asp:CheckBox ID="chkdown" runat="server" Text="<img src='/images/ico_05.gif' border='0' />" />--%>
                                                <%--<asp:FileUpload ID="fil1" runat="server" Width="330px" style="display:none" />--%>
                            <%--<input type="button" value="添加附件" class="widebuttons" onclick="addFileControl('spBJ1','filBJ1');" />--%>
                                            <asp:TextBox ID="txtPriceAtt" runat="server" Width="150"></asp:TextBox>
                <input type="button"  class="widebuttons" value="添加附件" ID="btnPriceAtt" runat="server"  />
                            &nbsp;供应商报价
                                            <br />
                <asp:Label ID ="lbPriceAtt" runat="server"></asp:Label>
                <asp:HiddenField ID="hidIds" runat="server" />
                <asp:HiddenField ID="hidNames" runat="server" />
                         <%--   <br />--%>
                            <span id="spBJ1" />
                    <asp:PlaceHolder ID="plcontrastDown" runat="server" />
            </td>
        </tr>
<%--        <tr>
            <td class="oddrow">
                询价附件:
            </td>
            <td class="oddrow-l">--%>
<%--                <asp:TextBox ID="txtPriceAtt" runat="server" Width="150"></asp:TextBox>
                <input type="button"  class="widebuttons" value="添加附件" ID="btnPriceAtt" runat="server"  />--%>
                <%--<asp:Button ID="btnPriceAtt" runat="server" class="widebuttons" Text="添加附件" OnClientClick="showPriceAtt();return false;" />--%>
<%--                <br />
                <asp:Label ID ="lbPriceAtt" runat="server"></asp:Label>
                <asp:HiddenField ID="hidIds" runat="server" />
                <asp:HiddenField ID="hidNames" runat="server" />--%>
                
<%--            </td>
        </tr>--%>
        <tr>
            <td colspan="2">
                <input runat="server" id="btnSave" value=" 保存  "  type="button" causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
        <script language="javascript">
            function addFileControl(control, name) {
                var spBJ = document.getElementById(control);
                var childCount = 1;
                for (i = 0; i < spBJ.childNodes.length; i++) {
                    if (spBJ.childNodes[i].type == "file")
                        childCount++;
                }
                if (childCount != 1 && (childCount % 3) == 0) {
                    insertHtml("beforeEnd", spBJ, "<input type='file' name='" + name + "' /><br />");
                }
                else
                    insertHtml("beforeEnd", spBJ, "<input type='file' name='" + name + "' />");
            }
            function insertHtml(where, el, html) {
                where = where.toLowerCase();
                if (el.insertAdjacentHTML) {
                    switch (where) {
                        case "beforebegin":
                            el.insertAdjacentHTML('BeforeBegin', html);
                            return el.previousSibling;
                        case "afterbegin":
                            el.insertAdjacentHTML('AfterBegin', html);
                            return el.firstChild;
                        case "beforeend":
                            el.insertAdjacentHTML('BeforeEnd', html);
                            return el.lastChild;
                        case "afterend":
                            el.insertAdjacentHTML('AfterEnd', html);
                            return el.nextSibling;
                    }
                    throw 'Illegal insertion point -> "' + where + '"';
                }
                var range = el.ownerDocument.createRange();
                var frag;
                switch (where) {
                    case "beforebegin":
                        range.setStartBefore(el);
                        frag = range.createContextualFragment(html);
                        el.parentNode.insertBefore(frag, el);
                        return el.previousSibling;
                    case "afterbegin":
                        if (el.firstChild) {
                            range.setStartBefore(el.firstChild);
                            frag = range.createContextualFragment(html);
                            el.insertBefore(frag, el.firstChild);
                            return el.firstChild;
                        } else {
                            el.innerHTML = html;
                            return el.firstChild;
                        }
                    case "beforeend":
                        if (el.lastChild) {
                            range.setStartAfter(el.lastChild);
                            frag = range.createContextualFragment(html);
                            el.appendChild(frag);
                            return el.lastChild;
                        } else {
                            el.innerHTML = html;
                            return el.lastChild;
                        }
                    case "afterend":
                        range.setStartAfter(el);
                        frag = range.createContextualFragment(html);
                        el.parentNode.insertBefore(frag, el.nextSibling);
                        return el.nextSibling;
                }
                throw 'Illegal insertion point -> "' + where + '"';
            }
    </script>
</asp:Content>
