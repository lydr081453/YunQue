<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Edit_productInfo"
    CodeBehind="productInfo.ascx.cs" %>
<link href="../../public/css/dialog.css" type="text/css" rel="stylesheet" />

<script language="javascript" src="../../public/js/jquery-1.2.6.js"></script>

<script language="javascript" src="../../public/js/dialog.js"></script>

<script language="javascript">
    function addFileControl() {
        var spBJ = document.getElementById("spBJ");
        var childCount = 1;
        for (i = 0; i < spBJ.childNodes.length; i++) {
            if (spBJ.childNodes[i].type == "file")
                childCount++;
        }
        if (childCount != 1 && (childCount % 3) == 0) {
            insertHtml("beforeEnd", spBJ, "<input type='file' name='filBJ' /><br />");
        }
        else
            insertHtml("beforeEnd", spBJ, "<input type='file' name='filBJ' />");
    }

    function openProductTypes() {
        var projectId = document.getElementById("<%=hidProjectId.ClientID %>").value;
        var departmentId = document.getElementById("<%=hidDepartmentId.ClientID %>").value;
        var generalId = "<%=Request[RequestName.GeneralID] %>";
        if (projectId == "" || departmentId == "") {
            alert(" - 请选择项目号和成本所属组！");
            return;
        }
        var win = window.open('selectProductTypes.aspx?<%=RequestName.GeneralID %>=' + generalId + '&projectId=' + projectId + "&deptId=" + departmentId, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function QAClick() {

        var win = window.open("WorkflowQA.aspx?<%= ESP.Purchase.Common.RequestName.GeneralID %>=<%=Request[RequestName.GeneralID]%>", null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function setProductTypes(ids, names, price) {
        hidIds = document.getElementById("<%=hidtypeIds.ClientID %>");
        txtDes = document.getElementById("<%=txtThirdParty.ClientID %>");
        hidIds.value = ids;
        txtDes.value = names;
        document.getElementById("<%=txtbugget.ClientID %>").value = price;
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
    function showOrderMsgInfo(orderid) {
        dialog("查看新闻明细", "iframe:/Purchase/Requisition/OrderMsgDetail.aspx?orderid=" + orderid, "1000px", "500px", "text");
    }

    function setHid(obj) {
        var ids = "";
        $("input[type='checkbox'][name='chkPrId']:checked").each(function () {
            if (ids == "")
                ids = ",";
            ids += $(this).val() + ",";
        });
        document.getElementById("<%= hidFCPrIds.ClientID %>").value = ids;
    }
</script>

<table border="0" cellspacing="1" runat="server" cellpadding="3" width="100%" id="tbNotify" style="background-color: Red;" visible="false">
    <tr>
        <td>
            <span style=" font-weight:bolder; color:White;">此项目利润率低于40%，请重点关注。</span>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidProjectId" runat="server" />
<asp:HiddenField ID="hidDepartmentId" runat="server" />
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ③ 采购物品信息
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            押金金额:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtforegift" runat="server" /><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                runat="server" ControlToValidate="txtforegift" Display="None" ErrorMessage="押金金额格式错误"
                ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <asp:Panel ID="palSup" runat="server" Visible="false">
        <tr>
            <td class="oddrow">
                选择供应商:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:DropDownList ID="ddlSup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSup_SelectedIndexChanged" />
            </td>
        </tr>
    </asp:Panel>
    <tr>
        <td class="oddrow" style="width: 15%">
            采购物料:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:HiddenField ID="hidtypeIds" runat="server" />
            <asp:TextBox ID="txtThirdParty" runat="server" Width="200px" /><asp:RequiredFieldValidator
                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtThirdParty"
                Display="None" ErrorMessage="采购物料为必填"></asp:RequiredFieldValidator><font color="red">
                    * </font>
            <input type="button" value="请选择..." id="btnSel" runat="server" class="widebuttons"
                onclick="openProductTypes();return false;" />
            <%--<br />例：礼品购买--%>
        </td>
        <td class="oddrow" style="width: 15%">
            采购成本预算:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtbugget" runat="server" Width="200px" MaxLength="8" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            发票类型:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:DropDownList ID="ddlInvoiceType" runat="server">
                <asp:ListItem Value="普票" Text="普票" />
                <asp:ListItem Value="专票" Text="专票" />
            </asp:DropDownList>
        </td>
        <td class="oddrow" style="width: 15%">
            税率:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:DropDownList ID="ddlTaxRate" runat="server" style="width:60px;">
                <asp:ListItem Value="1" Text="1" />
                <asp:ListItem Value="3" Text="3" />
                <asp:ListItem Value="6" Text="6" />
                <asp:ListItem Value="9" Text="9" />
                <asp:ListItem Value="13" Text="13" />
            </asp:DropDownList>%
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            备注:
        </td>
        <td colspan="3" class="oddrow-l">
            <asp:TextBox ID="txtNote" runat="server" Width="600px" /></br>请填写针对以下采购物品的注意事项或其他信息
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="gdvItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Width="100%" DataKeyNames="ID,producttype" OnRowDataBound="gdvItem_RowDataBound"
                OnRowDeleting="gdvItem_RowDeleting" Font-Size="14pt" OnRowEditing="gdvItem_RowEditing"
                SkinID="gridviewSkin" EmptyDataText="请添加采购物品">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="序号" HeaderStyle-Width="4%" />
                    <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <%# Eval("productAttribute").ToString() == ((int)State.productAttribute.ml).ToString() ? ("* " + Eval("Item_No").ToString()) : Eval("Item_No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" />
                    <%--<asp:BoundField DataField="desctiprtion" HeaderText="描述"  ControlStyle-Width="10px"/>--%>
                    <asp:TemplateField HeaderText="描述" HeaderStyle-Width="18%">
                        <ItemTemplate>
                            <%# Eval("desctiprtion")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="intend_receipt_date" HeaderText="预计收货时间"></asp:BoundField>
                    <asp:TemplateField HeaderText="单价" HeaderStyle-Width="8%">
                        <ItemTemplate>
                            <%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.##")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="uom" HeaderText="单位" HeaderStyle-Width="6%" />
                    <asp:TemplateField HeaderText="数量" HeaderStyle-Width="6%">
                        <ItemTemplate>
                            <%# decimal.Parse(Eval("quantity").ToString()).ToString("#,##0")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="小计" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.##")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="附件" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="labDown" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/edit.gif" CausesValidation="false"
                        HeaderText="编辑" CommandName="Edit" HeaderStyle-Width="5%" />
                    <asp:TemplateField HeaderText="删除" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" OnClientClick="return confirm('您确定删除吗？');" Text="<img src='/images/disable.gif' border='0' />"
                                CommandName="Delete" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="right" style="font-size: 12px" class="recordTd">
            共<asp:Label ID="DgRecordCount" runat="server"></asp:Label>条数据,总金额
            <asp:Label ID="labTotalPrice" runat="server" />
            元
        </td>
    </tr>
    <tr>
     <td colspan="2"  class="oddrow-l"><asp:Label runat="server" ID="txtMsg" style=" color:Red;"></asp:Label></td>
        <td colspan="2" align="right" style="text-align: right" class="oddrow-l">
            <input runat="server" id="btnAddItem" value="添加采购物品" type="button" 
                causesvalidation="false" class="widebuttons" onserverclick="btnAddItem_Click" />
        </td>
    </tr>
       <tr>
        <td class="oddrow" colspan="4"><asp:HiddenField ID="hidFCPrIds" runat="server" />
            <table width="100%" id="FCTable" runat="server" visible="false" >
                <tr>
                    <td class="heading">供应商框架合同</td>
                </tr>
                <tr>
                    <td class="oddrow-l">
                        <asp:GridView runat="server" ID="GvFCPr" Width="100%" AllowPaging="false" EmptyDataText="暂时没有相关记录" AutoGenerateColumns="False">
                            <Columns>
                                    <asp:TemplateField HeaderText="选择" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <input type="checkbox" onclick="setHid(this);" name="chkPrId" <%# hidFCPrIds.Value.Contains("," +Eval("prId").ToString()+",") ? "checked" : "" %> value="<%# Eval("prId") %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="PrNo" HeaderText="Pr单号" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="supplier_name" HeaderText="供应商名称" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="desctiprtion" HeaderText="框架合同描述" />
                                <asp:TemplateField HeaderText="附件" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a target='_blank' href="/Purchase/Requisition/UpfileDownload.aspx?OrderId=<%# Eval("orderId").ToString() %>&Index=0"><img src='/images/ico_04.gif' border='0' /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
 <tr>
        <td class="oddrow-l" colspan="4">
            <table runat="server" id="tb5000" border="0" cellspacing="1" cellpadding="3" width="100%"
                align="center">
                <tr>
                    <td class="oddrow">
                        采购审批流向:
                    </td>
                    <td colspan="3" class="oddrow-l">
                        <asp:DropDownList runat="server" ID="ddlValueLevel" Style="width: auto" Enabled="false">
                            <asp:ListItem Text="请选择..." Value="0"></asp:ListItem>
                            <asp:ListItem Text="5000元(含)以下，不需要采购部审批" Value="1"></asp:ListItem>
                            <asp:ListItem Text="5000元(含)以下，我仍希望采购部审批" Value="2" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;请点击<a style="font-weight: bolder; cursor: hand;" onclick="QAClick();">这里</a>进行风险评估，由系统建议审批流向。
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        <asp:Label runat="server" ID="lblResultTitle" />
                    </td>
                    <td colspan="3" class="oddrow-l">
                        <asp:Label runat="server" ID="lblResult" />
                        <asp:Label runat="server" ID="lblResult2" Font-Size="12px" Height="23px" ForeColor="#a83838"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
