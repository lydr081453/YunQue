<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_EditProducts" EnableEventValidation="false" CodeBehind="EditProducts.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>

    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page *//* Default tab */.AjaxTabStrip .ajax__tab_tab
        {
            font-size: 12px;
            padding: 4px;
            width: 105px; /* Your proper width */
            height: 20px; /* Your proper height */
            background-color: #EAEAEA;
        }
        /* When mouse over */.AjaxTabStrip .ajax__tab_hover .ajax__tab_tab
        {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */.AjaxTabStrip .ajax__tab_active .ajax__tab_tab
        {
            background-color: #C2E2ED;
            font-weight: bold;
        }
        /* TabPanel Content */.AjaxTabStrip .ajax__tab_body
        {
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
            margin-right: 9px; /* Your proper right-margin, make your header and the content have the same width */
            margin-top: 0px;
        }
        .border
        {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-bottom-color: #CC3333;
            border-left-color: #CC3333;
        }
        .border2
        {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
        }
        .border_title_left
        {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }
        .border_title_right
        {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }
        .border_datalist
        {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #CC3333;
        }
    </style>

    <script language="javascript">
        function account() {
            var num = document.getElementById("<%=quantity.ClientID %>").value;
            if (document.all) {
                var price = document.getElementById("<%=labPrice.ClientID %>").value.replace(/,/g, "");
                document.getElementById("<%=labtotal.ClientID %>").innerText = setCurrency(parseFloat(price) * num + "");
            } else {
                var price = document.getElementById("<%=labPrice.ClientID %>").value.replace(/,/g, "");
                document.getElementById("<%=labtotal.ClientID %>").textContent = setCurrency(parseFloat(price) * num + "");
            }
        }

        function account1() {
            var num = document.getElementById("<%=quantity1.ClientID %>").value;
            var price = document.getElementById("<%=txtPrice.ClientID %>").value;
            document.getElementById("<%=txttotal.ClientID %>").value = setCurrency(parseFloat(price) * num + "");
        }
        function SupplierClick() {
            //alert(document.getElementById("<%=ddlType1.ClientID %>"));
            var generalid = '<%= Request[RequestName.GeneralID]%>';
            var type1 = document.getElementById("<%=ddlType1.ClientID %>").value;
            var type2 = document.getElementById("<%=ddlType2.ClientID %>").value;
            var type3 = document.getElementById("<%=ddlType3.ClientID %>").value;
            var type3name = document.getElementById("<%=ddlType3.ClientID %>").innerText;
            var supplyId = document.getElementById("<%= hidABC.ClientID %>").value;
            document.getElementById('<%= hidABC.ClientID %>').value = "";
            var win = "";
            win = window.open('SupplierInfoForProduct.aspx?<% =RequestName.GeneralID %>=' + generalid + '&supplyId=' + supplyId + '&productType=' + type3 + '&source=product&name=' + document.getElementById("<%= txtSupplierName.ClientID %>").value, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            if (win != null)
                win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

        }

        function clearType() {
            document.getElementById("<%=ddlType2.ClientID %>").options.length = 0;
            addOption(document.getElementById("<%=ddlType2.ClientID %>"), "-1", "请选择");
        }

        function setType(id, text) {
            addOption(document.getElementById("<%=ddlType2.ClientID %>"), id, text);
        }
        function addOption(obj, id, text) {
            var ss = document.createElement("option");
            ss.value = id;
            ss.text = text
            if (document.all)
                obj.add(ss);
            else
                obj.insertBefore(ss, null);
        }

        function setType1(id) {
            document.getElementById("<%=ddlType1.ClientID %>").value = id;
            document.getElementById("<%=ddlType1.ClientID %>").disabled = true;
            document.getElementById("<%= txtSupplierName.ClientID %>").disabled = true;
        }

        function setType2(id) {
            document.getElementById("<%=ddlType2.ClientID %>").value = id;
            document.getElementById("<%=ddlType2.ClientID %>").disabled = true;
        }
        function setSName(name) {
            document.getElementById("<%=txtSupplierName.ClientID %>").value = name;
            document.getElementById("<%=hidSupplierName.ClientID %>").value = name;
        }
        function setSName1(name, id) {
            document.getElementById("<%=txtSupplierName.ClientID %>").value = name;
            document.getElementById("<%=hidSupplierName.ClientID %>").value = name;
            document.getElementById("<%=hidSupplierId1.ClientID %>").value = id;
        }
        function GoToAD(id) {
            window.location = "Advertisement/AdvertisementOrder.aspx?GeneralID=" + id + "&pageUrl=AddRequisitionStep6.aspx";
        }
        function show(sname, sid) {
            var win = window.open('SupplierInfoView.aspx?isback=1&supplierId=' + sid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
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
        function changedTabIndex() {
            var tabBehavior = document.getElementById("<%=TabContainer1.ClientID %>").control;
            tabBehavior.set_activeTabIndex(1);
        }

        function setValue() {
            document.getElementById("<%=hidType2.ClientID %>").value = document.getElementById("<%=ddlType2.ClientID %>").value;
        }

        function showSupplierPOList(supperId, pageIndex) {
            dialog("供应商采购列表", "url:post?/Purchase/Requisition/SupplierPOList.aspx?supplierId=" + supperId + "&pageIndex=" + pageIndex, "800px", "480px", "text");
        }
        function showPriceAtt(generalid) {
            var msgid = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_ctl00_txtPriceAtt").value;
            //            msgid = "#2006060631";
            if (msgid == "") {
                //                alert('请填写批号！');
                dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?page=add&generalid=" + generalid, "800px", "480px", "text");
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
                    dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?msgid=" + msgid + "&page=add", "800px", "480px", "text");
                }
                else {
                    alert("填写批号有误，请重新填写！");
                }
            }
        }
        function showPriceAtt1(generalid) {
            var msgid = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_Tab2_txtPriceAtt1").value;
            //            msgid = "#2006060631";
            if (msgid == "") {
                //                alert('请填写批号！');
                dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?type=f&page=add&generalid=" + generalid, "800px", "480px", "text");
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
                    dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?type=f&msgid=" + msgid + "&page=add", "800px", "480px", "text");
                }
                else {
                    alert("填写批号有误，请重新填写！");
                }
            }
        }        
    </script>

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <asp:ScriptManager ID="manager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidABC" runat="server" />
    <asp:HiddenField ID="hidViewType1" runat="server" />
    <%--已经选中的一级物料类别ID--%>
    <asp:HiddenField ID="hidViewType2" runat="server" />
    <%--已经选中的二级物料类别ID--%>
    <asp:HiddenField ID="hidCurrentTypeId" runat="server" />
    <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" AutoPostBack="true"
        Width="98%">
        <cc1:TabPanel HeaderText="目录物品" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pal1" runat="server">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="heading">
                                检索
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow-l">
                                检索关键字：&nbsp;<asp:TextBox ID="txtKey" runat="server" />&nbsp;<asp:Button ID="btnSearch"
                                    runat="server" Text=" 检索 " CausesValidation="false" CssClass="widebuttons" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack3_Click" />
                    <asp:Repeater ID="rep1" runat="server" OnItemDataBound="rep1_ItemDataBound">
                        <ItemTemplate>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="30">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%" height="15">
                                                    &nbsp;<a id='<%# Eval("typename") %>' />
                                                </td>
                                                <td width="15%" rowspan="2" align="center">
                                                    <asp:Label ID="lab" runat="server" Font-Size="14px" Font-Bold="true" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="15" class="border_title_left">
                                                    &nbsp;
                                                </td>
                                                <td class="border_title_right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DataList ID="rep2" runat="server" OnItemDataBound="rep2_ItemDataBound" CssClass="border_datalist"
                                            Width="100%" ItemStyle-VerticalAlign="Top" RepeatColumns="4" RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lab" runat="server" Font-Size="12px" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 40px; padding-top: 5px;" valign="top">
                                                            <asp:DataList ID="dg3" runat="server" Width="100%" RepeatColumns="1" ItemStyle-Height="25px"
                                                                ItemStyle-VerticalAlign="Top" OnItemDataBound="dg3_ItemDataBound" OnItemCommand="dg3_ItemCommand"
                                                                RepeatDirection="Horizontal">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk" CausesValidation="false" CommandArgument='<%# Eval("typeid") %>'
                                                                        CommandName="SET" Font-Underline="true" Font-Size="10px" ForeColor="Black" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                    <br />
                    <asp:Button ID="btnBack3" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack3_Click" />
                </asp:Panel>
                <asp:Panel ID="pal2" runat="server" Visible="false">
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <table width="100%" border="0">
                                    <tr>
                                        <td style="width: 80px">
                                            物料类别：
                                        </td>
                                        <td>
                                            <asp:DataList ID="dgType" runat="server" OnItemDataBound="dgType_ItemDataBound" OnItemCommand="dgType_ItemCommand"
                                                RepeatColumns="10" RepeatDirection="Horizontal" CellSpacing="5">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk" CausesValidation="false" CommandArgument='<%# Eval("typeid") %>'
                                                        CommandName="SET" Font-Underline="true" Font-Size="10px" ForeColor="Black" Text='<%# Eval("typename") %>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" class="tableForm">
                                    <tr>
                                        <td class="heading" colspan="4">
                                            检索
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="oddrow">
                                            <font color="red">* 请先检索协议供应商,不存在协议供应商才可以添加非目录物品.</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 15%">
                                            供应商名称:
                                        </td>
                                        <td class="oddrow-l" style="width: 35%">
                                            <asp:TextBox ID="txtSupplierNameS" runat="server" />
                                        </td>
                                        <td class="oddrow" style="width: 15%">
                                            物品名称:
                                        </td>
                                        <td class="oddrow-l" style="width: 35%">
                                            <asp:TextBox ID="txtProductNameS" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="btnSearch1" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" class="tableForm">
                                    <tr>
                                        <td class="heading">
                                            目录物品列表
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc2:NewGridView ID="gvItem" runat="server" AllowPaging="false" Width="100%" AutoGenerateColumns="false"
                                                OnRowDataBound="gvItem_RowDataBound" OnRowCommand="gvItem_RowCommand" AllowSorting="true">
                                                <Columns>
                                                    <asp:BoundField DataField="productName" HeaderText="物品名称" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="20%" SortExpression="productName" />
                                                    <asp:BoundField DataField="productDes" HeaderText="描述" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30%" />
                                                    <asp:TemplateField HeaderText="参考价格" ItemStyle-HorizontalAlign="Right" SortExpression="ProductPrice"
                                                        ItemStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <%# decimal.Parse(Eval("productprice").ToString()).ToString("#,##0.####")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="productUnit" HeaderText="单位" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="8%" />
                                                    <asp:TemplateField HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <span id="sp1" style="cursor: pointer" onclick="show('<%#Eval("suppliername")%>','<%#Eval("supplierId")%>');"
                                                                style="color: blue">
                                                                <%#Eval("suppliername")%></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnS" runat="server" Text="目录物品" CausesValidation="false" CommandArgument='<%# Eval("id") %>'
                                                                CssClass="widebuttons" CommandName="Add" />
                                                            &nbsp;
                                                            <asp:Button ID="btnSup" OnClick="btnSup_Click" runat="server" Text="供应商" CausesValidation="false"
                                                                CommandArgument='<%#Eval("supplierId")%>' CssClass="widebuttons" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="labMsg" runat="server" Text=" 暂无目录物品，请选择添加非目录物品。" />
                                                </EmptyDataTemplate>
                                            </cc2:NewGridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" class="tableForm" border="0">
                                    <tr>
                                        <td class="heading">
                                            协议供应商列表
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSupplier_RowDataBound"
                                                DataKeyNames="id" OnRowCommand="gvSupplier_RowCommand" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <span id="sp1" style="cursor: pointer" style="color: blue">
                                                                <%#Eval("supplyName")%></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="supplier_area" HeaderText="所属地区" />
                                                    <asp:BoundField DataField="contact_name" HeaderText="联系人" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="联系电话" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="labPhone" runat="server" Text='<%#Eval("contact_tel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="contact_mobile" HeaderText="手机" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="传真" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="labFax" runat="server" Text='<%#Eval("contact_fax") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="contact_email" HeaderText="电子邮件" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="labTypeName" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="supplier_source" HeaderText="供应商来源" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="supplier_frameNO" HeaderText="框架协议号" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="历史采购" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <a style="cursor: pointer" onclick='showSupplierPOList(<%# Eval("id") == DBNull.Value ? "0" : Eval("id") %>,0);'
                                                                title="历史采购信息">
                                                                <%# Eval("id") == DBNull.Value ? "" : "查看"%></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnS" runat="server" Text="选择" CausesValidation="false" CommandArgument='<%# Eval("id").ToString()+"-"+ Eval("supplyId").ToString() %>'
                                                                CssClass="widebuttons" CommandName="Add" />
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
                            <td align="left">
                                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="widebuttons" />&nbsp<asp:Button ID="btnP3" runat="server" Text="上一步" CausesValidation="false"
                                        OnClick="btnP3_Click" CssClass="widebuttons" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pal3" runat="server" Visible="false">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="oddrow" style="width: 20%">
                                供应商名称:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labSupplierName" runat="server" /><asp:HiddenField ID="hidSupplierId"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                物料类别:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labProductType" runat="server" /><asp:HiddenField ID="hidProductTypeId"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                采购物品:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                具体描述:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox ID="desctiprtion" runat="server" Height="100px" Width="260"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                预计收货时间:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="intend_receipt_date"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" ValidationGroup="panel1" runat="server" ControlToValidate="intend_receipt_date"
                                    Display="None" ErrorMessage="预计收货起始时间为必填" /><font color="red"> * </font>&nbsp;<img
                                        src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=intend_receipt_date.ClientID %>'), document.getElementById('<%=intend_receipt_date.ClientID %>'), 'yyyy-mm-dd');" />
                                -
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="Eintend_receipt_date"></asp:TextBox>&nbsp;<img
                                    src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=Eintend_receipt_date.ClientID %>'), document.getElementById('<%=Eintend_receipt_date.ClientID %>'), 'yyyy-mm-dd');" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                单价:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox ID="labPrice" runat="server" MaxLength="10" onblur="account();" /><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="labPrice" Display="None"
                                    ErrorMessage="单价为必填" ValidationGroup="panel1" /><font color="red"> * </font>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="labPrice"
                                    ErrorMessage="请输入正确单价" ValidationGroup="panel1" ValidationExpression="^[+-]?(\d{1,3},?)+(\.\d+)?$">请输入正确单价</asp:RegularExpressionValidator>
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
                                <asp:Label ID="labUnit" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                数量:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox ID="quantity" runat="server" Width="130" onblur="account();" MaxLength="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="panel1"
                                    runat="server" ControlToValidate="quantity" Display="None" ErrorMessage="数量为必填" /><font
                                        color="red">*</font>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="quantity"
                                    ErrorMessage="请输入正确数量" ValidationGroup="panel1" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确数量</asp:RegularExpressionValidator>
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
                                <%--报价附件:--%>
                                询价信息:
                            </td>
                            <td class="oddrow-l">
                                <%--                            <asp:FileUpload ID="fil2" runat="server" Width="330px" style="display:none" />
                            <input type="button" value="添加附件" class="widebuttons" onclick="addFileControl('spBJ2','filBJ2');" />--%>
                                <asp:TextBox ID="txtPriceAtt" runat="server" Width="150"></asp:TextBox>
                                <input type="button" class="widebuttons" value="添加附件" id="btnPriceAtt" runat="server" />&nbsp;供应商报价
                                <br />
                                <asp:Label ID="lbPriceAtt" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidIds" runat="server" />
                                <asp:HiddenField ID="hidNames" runat="server" />
                                <br />
                                <span id="spBJ2" />
                            </td>
                        </tr>
                        <%--                        <tr>
                            <td class="oddrow">
                                询价附件:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox ID="txtPriceAtt" runat="server" Width="150"></asp:TextBox>
                                <input type="button"  class="widebuttons" value="添加附件" ID="btnPriceAtt" runat="server"  />--%>
                        <%--<asp:Button ID="btnPriceAtt" runat="server" class="widebuttons" Text="添加附件" OnClientClick="showPriceAtt();return false;" />--%>
                        <%--                                <br />
                                <asp:Label ID ="lbPriceAtt" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidIds" runat="server" />
                                <asp:HiddenField ID="hidNames" runat="server" />
                                
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSave1" runat="server" Text=" 保存 " ValidationGroup="panel1" CssClass="widebuttons"
                                    OnClick="btnSave1_Click" />&nbsp;<asp:Button ID="btnNext1" runat="server" Text=" 添加 "
                                        ValidationGroup="panel1" CssClass="widebuttons" OnClick="btnNext1_Click" />
                                &nbsp;<asp:Button ID="btnBack1" runat="server" Text=" 返回 " CssClass="widebuttons"
                                    CausesValidation="false" OnClick="btnBack1_Click" />&nbsp;<asp:Button ID="btnP1"
                                        runat="server" Text=" 上一步 " CssClass="widebuttons" OnClick="btnP1_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary runat="server" ValidationGroup="panel1" ShowMessageBox="true"
                        ShowSummary="false" />
                </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="非目录物品" ID="Tab2" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            供应商名称:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtSupplierName" runat="server" Enabled="false" Width="270" /><font
                                color="red"> * </font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="panel2"
                                runat="server" ControlToValidate="txtSupplierName" Display="None" ErrorMessage="请添加供应商" /><input
                                    type="button" value="添加" runat="server" id="btnSelect" visible="false" class="widebuttons"
                                    onclick="SupplierClick();return false;" />
                            <asp:HiddenField ID="hidSupplierId1" runat="server" />
                            <asp:HiddenField ID="hidSupplierName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="oddrow-l">
                            <asp:Panel ID="palSupplier" runat="server" Visible="false">
                                <table width="100%" class="tableForm">
                                    <tr>
                                        <td class="oddrow" style="width: 20%">
                                            审批流向:
                                        </td>
                                        <td class="oddrow-l">
                                            <asp:RadioButtonList ID="rblrequisitionflow" runat="server" RepeatDirection="horizontal" OnSelectedIndexChanged="rbl_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="XTable">
                                            </asp:RadioButtonList>
                                            <br />
                                            <span style="color: Red">PR-PR : 一般适用于供应商或收款单位为个人, 且采购订单或合同的不适用的情况.
                                                <br />
                                                PR-PO: 选择PR-PO, 系统自动生成订单(PO). 若供应商提供邮件地址的, 系统发出PO至供应商处进行确认, 同时附公司订单标准条款. 订单方式流转速度快,
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
                                                        <asp:RadioButtonList ID="radOperationType" CssClass="XTable" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="radOperationType_SelectIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:RadioButtonList>
                                                        <asp:Label ID="labOperationType" runat="server" />
                                                    </td>
                                                    <td>
                                                        &nbsp;<asp:Panel ID="palGR" runat="server" Visible="false"><asp:CheckBox ID="chkHaveInvoice" runat="server" Text="是否存在发票" />
                                                        &nbsp;身份证：<asp:TextBox ID="txtCardNum" runat="server" /><font color="red"> * </font><asp:RequiredFieldValidator ValidationGroup="panel2"
                                                ForeColor="Red" ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCardNum"
                                                Display="None" ErrorMessage="请填写身份证" /></asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                            * <span style='color: red; font-weight: bold;'>对公与个人选择非常关键，请务必仔细确认，填写错误会影响您的后续流程！</span><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow">
                                            开户公司名称:
                                        </td>
                                        <td class="oddrow-l">
                                            <asp:TextBox ID="txtaccountName" runat="server" Width="270" /><font color="red"> * </font><asp:RequiredFieldValidator ValidationGroup="panel2"
                                                ForeColor="Red" ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtaccountName"
                                                Display="None" ErrorMessage="请填写开户公司名称" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow">
                                            开户银行:
                                        </td>
                                        <td class="oddrow-l">
                                            <asp:TextBox ID="txtaccountBank" runat="server" Width="270" /><font color="red"> * </font><asp:RequiredFieldValidator  ValidationGroup="panel2"
                                                ForeColor="Red" ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtaccountBank"
                                                Display="None" ErrorMessage="请填写开户银行" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow">
                                            帐号:
                                        </td>
                                        <td class="oddrow-l">
                                            <asp:TextBox ID="txtaccountNum" runat="server"  Width="270"/><font color="red"> * </font><asp:RequiredFieldValidator ValidationGroup="panel2"
                                                ForeColor="Red" ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtaccountNum"
                                                Display="None" ErrorMessage="请填写帐号" />
                                        </td>
                                    </tr>
                                </table>
                </asp:Panel> </td> </tr>
                <tr>
                    <td class="oddrow">
                        物料类别:
                    </td>
                    <td class="oddrow-l">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlType1" runat="server" AutoPostBack="true" />
                                &nbsp;<asp:DropDownList ID="ddlType2" runat="server" AutoPostBack="true" />
                                <asp:HiddenField ID="hidType2" runat="server" />
                                &nbsp<asp:DropDownList ID="ddlType3" runat="server" onclick="" />
                                <font color="red">* </font>
                                <asp:CompareValidator ID="CompareValidator2" ControlToValidate="ddlType3" Display="None"
                                    Type="String" ValueToCompare="-1" Operator="NotEqual" ValidationGroup="panel2"
                                    ErrorMessage="请选择物料类别" runat="server"></asp:CompareValidator><asp:CompareValidator
                                        ID="CompareValidator1" ControlToValidate="ddlType3" Display="None" Type="String"
                                        ValueToCompare="" Operator="NotEqual" ValidationGroup="panel2" ErrorMessage="请选择物料类别"
                                        runat="server"></asp:CompareValidator></ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        采购物品:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtProductName" runat="server" Width="270"></asp:TextBox>
                        <font color="red">* </font>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="panel2"
                            runat="server" ControlToValidate="txtProductName" Display="None" ErrorMessage="采购物品为必填"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        具体描述:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="desctiprtion1" runat="server" Width="270" TextMode="MultiLine" Height="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        预计收货时间:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="intend_receipt_date1"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator3" ValidationGroup="panel2" runat="server" ControlToValidate="intend_receipt_date1"
                            Display="None" ErrorMessage="预计收货起始时间为必填" /><font color="red"> * </font>&nbsp;<img
                                src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=intend_receipt_date1.ClientID %>'), document.getElementById('<%=intend_receipt_date1.ClientID %>'), 'yyyy-mm-dd');" />
                        -
                        <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="Eintend_receipt_date1"></asp:TextBox>&nbsp;<img
                            src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=Eintend_receipt_date1.ClientID %>'), document.getElementById('<%=Eintend_receipt_date1.ClientID %>'), 'yyyy-mm-dd');" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        单价:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtPrice" runat="server" Width="130" onblur="account1();" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="panel2"
                            runat="server" ControlToValidate="txtPrice" Display="None" ErrorMessage="单价为必填" /><font
                                color="red"> * </font>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="panel2"
                            runat="server" ControlToValidate="txtPrice" ErrorMessage="请输入正确单价" ValidationExpression="^[+-]?(\d{1,3},?)+(\.\d+)?$">请输入正确单价</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        货币:
                    </td>
                    <td class="oddrow-l">
                        <asp:Label ID="labMoneyType1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        单位:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txtUnit1" runat="server" MaxLength="20" Width="130"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUnit1" Display="None"
                            ErrorMessage="单位为必填" ValidationGroup="panel2" /><font color="red"> * </font>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        数量:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="quantity1" runat="server" Width="130" onblur="account1();" MaxLength="8"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="quantity1"
                            Display="None" ErrorMessage="数量为必填" ValidationGroup="panel2" /><font color="red"> *
                            </font>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="quantity1"
                            ErrorMessage="请输入正确数量" ValidationGroup="panel2" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确数量</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        小计:
                    </td>
                    <td class="oddrow-l">
                        <asp:TextBox ID="txttotal" Enabled="false" runat="server" Width="130px" MaxLength="20" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        <%--报价附件:--%>询价信息:
                    </td>
                    <td class="oddrow-l">
                        <%--                            <asp:FileUpload ID="fil1" runat="server" Width="330px" style="display:none" />
                            <input type="button" value="添加附件" class="widebuttons" onclick="addFileControl('spBJ1','filBJ1');" />&nbsp;供应商报价--%>
                        <asp:TextBox ID="txtPriceAtt1" runat="server" Width="150"></asp:TextBox>
                        <input type="button" class="widebuttons" value="添加附件" id="btnPriceAtt1" runat="server" />&nbsp;供应商报价
                        <br />
                        <asp:Label ID="lbPriceAtt1" runat="server"></asp:Label>
                        <asp:HiddenField ID="hidIds1" runat="server" />
                        <asp:HiddenField ID="hidNames1" runat="server" />
                        <br />
                        <span id="spBJ1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave2" runat="server" Text=" 保存 " ValidationGroup="panel2" CssClass="widebuttons"
                            OnClick="btnSave2_Click" />&nbsp;<asp:Button ID="btnNext2" runat="server" ValidationGroup="panel2"
                                Text=" 添加 " OnClick="btnNext2_Click" CssClass="widebuttons" />
                        <asp:Button ID="btnBack2" runat="server" Text=" 返回 " CssClass="widebuttons" CausesValidation="false"
                            OnClick="btnBack2_Click" />&nbsp;<asp:Button ID="btnP2" runat="server" Text=" 上一步 "
                                CssClass="widebuttons" OnClick="btnP2_Click" />
                    </td>
                </tr>
                </table>
                <asp:ValidationSummary runat="server" ValidationGroup="panel2" ShowMessageBox="true"
                    ShowSummary="false" />
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>

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
