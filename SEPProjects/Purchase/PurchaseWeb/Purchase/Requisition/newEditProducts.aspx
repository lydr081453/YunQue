<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="newEditProducts.aspx.cs" Inherits="PurchaseWeb.Purchase.newEditProducts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script language="javascript" src="/public/js/dialog.js"></script>
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


        function isIdCardNo(num) {
            // num = num.toUpperCase();
            //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X。   
            if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(num))) {
                alert('输入的身份证号长度不对，或者号码不符合规定！\n15位号码应全为数字，18位号码末位可以为数字或X。');
                return false;
            }
            //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
            //下面分别分析出生日期和校验位 
            var len, re;
            len = num.length;
            if (len == 15) {
                re = new RegExp(/^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/);
                var arrSplit = num.match(re);

                //检查生日日期是否正确 
                var dtmBirth = new Date('19' + arrSplit[2] + '/' + arrSplit[3] + '/' + arrSplit[4]);
                var bGoodDay;
                bGoodDay = (dtmBirth.getYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
                if (!bGoodDay) {
                    alert('输入的身份证号里出生日期不对！');
                    return false;
                }
                else {
                    //将15位身份证转成18位 
                    //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
                    var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                    var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                    var nTemp = 0, i;
                    num = num.substr(0, 6) + '19' + num.substr(6, num.length - 6);
                    for (i = 0; i < 17; i++) {
                        nTemp += num.substr(i, 1) * arrInt[i];
                    }
                    num += arrCh[nTemp % 11];
                    return num;
                }
            }
            if (len == 18) {
                re = new RegExp(/^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/);
                var arrSplit = num.match(re);

                //检查生日日期是否正确 
                var dtmBirth = new Date(arrSplit[2] + "/" + arrSplit[3] + "/" + arrSplit[4]);
                var bGoodDay;
                bGoodDay = (dtmBirth.getFullYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
                if (!bGoodDay) {
                    alert(dtmBirth.getYear());
                    alert(arrSplit[2]);
                    alert('输入的身份证号里出生日期不对！');
                    return false;
                }
                else {
                    //检验18位身份证的校验码是否正确。 
                    //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。 
                    var valnum;
                    var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                    var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                    var nTemp = 0, i;
                    for (i = 0; i < 17; i++) {
                        nTemp += num.substr(i, 1) * arrInt[i];
                    }
                    valnum = arrCh[nTemp % 11];
                    if (valnum != num.substr(17, 1)) {
                        alert('18位身份证的校验码不正确！应该为：' + valnum);
                        return false;
                    }
                    return num;
                }
            }
            return false;
        }



        function showSupplierPOList(supplierName, pageIndex) {
            var url = "/Purchase/Requisition/SupplierPOList.aspx?supplierName=" + supplierName + "&pageIndex=" + pageIndex;
            dialog("供应商采购列表", "iframe:" + url, "800px", "480px", "text");
        }

        function showLinker(supplierId) {
            var url = "/Purchase/Requisition/SupplierUsers.aspx?supplierId=" + supplierId;
            dialog("供应商联系人", "iframe:" + url, "800px", "480px", "text");
        }

        function showPriceFies(supplierId) {
            var url = "/Purchase/Requisition/SupplierPriceFils.aspx?supplierId=" + supplierId;
            dialog("供应商报价", "iframe:" + url, "800px", "480px", "text");
        }

        function showPriceAtt() {
            var atts = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidNames").value;
            var msgid = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_Tab2_txtPriceAtt").value;
            var typeid = document.getElementById("<%=hidCurrentTypeId.ClientID %>").value;
            var generalid = document.getElementById("ctl00_ContentPlaceHolder1_hidGeneralID").value;

            var showlocal = "&typeid=" + typeid;

            if (msgid == "") {
                dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?atts=" + atts + "&page=add&generalid=" + generalid + showlocal, "1000px", "480px", "text");
            }
            else if (msgid.indexOf("#") != 0 || msgid.length < 10) {
                alert("填写批号有误，请重新填写！");
            }
            else {
                msgid = msgid.replace("#", "");
                tmp = msgid.substring(0, 8);
                msgid = msgid.replace(tmp, "");
                if (!isNaN(msgid)) {
                    dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?atts=" + atts + "&msgid=" + msgid + "&page=add&generalid=" + generalid + showlocal, "1000px", "480px", "text");
                }
                else {
                    alert("填写批号有误，请重新填写！");
                }
            }
        }
        function showPriceAtt1() {
            var atts = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_Tab2_hidNames1").value;
            var msgid = document.getElementById("ctl00_ContentPlaceHolder1_TabContainer1_Tab2_txtPriceAtt1").value;
            var generalid =  document.getElementById("ctl00_ContentPlaceHolder1_hidGeneralID").value;
            msgid = msgid.trim();
            var typeid = document.getElementById("<%=hidCurrentTypeId.ClientID %>").value;
            var showlocal = "";
            showlocal = "&typeid=" + typeid;

            if (msgid == "") {
                dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?atts=" + atts + "&type=f&page=add&generalid=" + generalid + showlocal, "800px", "480px", "text");
            }
            else if (msgid.indexOf("#") != 0 || msgid.length < 10) {
                alert("填写批号有误，请重新填写！");
            }
            else {
                msgid = msgid.replace("#", "");
                tmp = msgid.substring(0, 8);
                msgid = msgid.replace(tmp, "");
                if (!isNaN(msgid)) {
                    dialog("选择回复附件列表", "iframe:/Purchase/Requisition/AddPriceAtt.aspx?atts=" + atts + "&type=f&msgid=" + msgid + "&page=add&generalid=" + generalid + showlocal, "800px", "480px", "text");
                }
                else {
                    alert("填写批号有误，请重新填写！");
                }
            }
        }

        function checkMLPrice(sender, args) {
            var newPrice = document.getElementById("<%= labPrice.ClientID %>").value.replace(/,/g, "");
            var MLPrice = document.getElementById("<%= hidMLPrice.ClientID %>").value;
            if (MLPrice > 0 && float.Parse(newPrice) > float.Parse(MLPrice)) {
                args.IsValid = false;
            }
            else
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

        function chkEmailFile(sender, args) {
            var div = document.getElementById('<%=divEmail.ClientID %>');
            var lab = document.getElementById('<%= labEmailFile.ClientID %>');
            var file = document.getElementById("<%= filEmailFile.ClientID %>");
            if (div.style.display == "block" && lab.innerHTML == "" && file.value == "") {
                args.IsValid = false;
            } else
                args.IsValid = true;
        }
        function clkFlow(clt) {
            // 通过 name 属性获取 radio 组
            var selectedValue = $('input[name="<%= rblrequisitionflow.ClientID.Replace('_','$') %>"]:checked').val();
            if (selectedValue == '<%= ESP.Purchase.Common.State.requisitionflow_toFC %>')
            {
                document.getElementById("<%= txtProductName.ClientID%>").value = "框架合同";
                document.getElementById("<%= desctiprtion1.ClientID%>").value = "框架合同";
                document.getElementById("<%= txtPrice.ClientID%>").value = "0";
                document.getElementById("<%= txtUnit1.ClientID%>").value = "0";
                document.getElementById("<%= quantity1.ClientID%>").value = "0";
                document.getElementById("<%= intend_receipt_date1.ClientID%>").value = "1900-01-01";
            }
        }
    </script>

    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    </script>

    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page */ /* Default tab */

        .AjaxTabStrip .ajax__tab_tab {
            font-size: 12px;
            padding: 4px;
            width: 105px; /* Your proper width */
            height: 20px; /* Your proper height */
            background-color: #EAEAEA;
        }
        /* When mouse over */ .AjaxTabStrip .ajax__tab_hover .ajax__tab_tab {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */ .AjaxTabStrip .ajax__tab_active .ajax__tab_tab {
            background-color: #C2E2ED;
            font-weight: bold;
        }
        /* TabPanel Content */ .AjaxTabStrip .ajax__tab_body {
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
            margin-right: 9px; /* Your proper right-margin, make your header and the content have the same width */
            margin-top: 0px;
        }

        .border {
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

        .border2 {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
        }

        .border_title_left {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_title_right {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_datalist {
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
    <asp:ScriptManager ID="manager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidCurrentTypeId" runat="server" Value="0" />
    <asp:HiddenField ID="hidGeneralID" runat="server" Value="0" />
    <asp:HiddenField ID="hidCurrentSupplierId" runat="server" Value="0" />
    <asp:HiddenField ID="hidSupplyId" runat="server" />
    <asp:TextBox runat="server" ID="txtIds" Style="display: none"></asp:TextBox>
    <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
        <cc1:TabPanel ID="Tab1" runat="server" HeaderText="信息检索">
            <ContentTemplate>
                <asp:Panel ID="palType" runat="server">
                    <img src="/images/01.jpg" />

                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="heading">检索
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow-l">检索关键字：&nbsp;<asp:TextBox ID="txtKey" runat="server" />&nbsp;<asp:Button ID="btnSearch1"
                                OnClick="btnSearch1_Click" runat="server" Text=" 检索 " CausesValidation="False"
                                CssClass="widebuttons" />

                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button runat="server" Text="返回" CssClass="widebuttons" CausesValidation="False" OnClick="btnBack_Click" />
                    <asp:Repeater ID="rep1" runat="server" OnItemDataBound="rep1_ItemDataBound">
                        <ItemTemplate>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="30">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%" height="15">&nbsp;<a id='<%# Eval("typename") %>' />
                                                </td>
                                                <td width="15%" rowspan="2" align="center">
                                                    <asp:Label ID="lab" runat="server" Font-Size="14px" Font-Bold="true" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="15" class="border_title_left">&nbsp;
                                                </td>
                                                <td class="border_title_right">&nbsp;
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
                                                                ItemStyle-VerticalAlign="Top" OnItemDataBound="dg3_ItemDataBound" RepeatDirection="Horizontal">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk" CausesValidation="false" CommandArgument='<%# Eval("typeid") %>'
                                                                        OnClick="lnk_Click" Font-Underline="true" Font-Size="10px" ForeColor="Black"
                                                                        runat="server" />
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
                                    <td style="height: 10px"></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="palSupplier" runat="server" Visible="False">
                    <img src="/images/02.jpg" />
                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="oddrow" style="width: 10%">供应商：
                            </td>
                            <td class="oddrow-l" style="width: 20%">
                                <asp:TextBox ID="txtSupplier" runat="server" />
                            </td>
                            <td class="oddrow" style="width: 10%">采购物品：
                            </td>
                            <td class="oddrow-l" style="width: 20%">
                                <asp:TextBox ID="txtProduct" runat="server" />
                            </td>
                            <td class="oddrow-l">
                                <asp:Button ID="btnSearch2" runat="server" OnClick="btnSearch2_Click" Text="检索" CssClass="widebuttons" />
                            </td>
                        </tr>
                    </table>
                    <br />

                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="heading">供应商列表</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="repFXY" runat="server" OnItemDataBound="repFXY_ItemDataBound" OnItemCommand="repFXY_ItemCommand">
                                    <HeaderTemplate>
                                        <table width="100%" border="0" class="gridView">
                                            <tr class="Gheading">
                                                <td style="height: 30px" width="18%">供应商名称
                                                </td>
                                                <td>联系人</td>
                                                <td width="5%">地区
                                                </td>
                                                <td>历史订单数</td>
                                                <td>好评</td>
                                                <td>中评</td>
                                                <td>差评</td>
                                                <td>价格水平</td>
                                                <td>参考报价</td>
                                                <td width="4%" align="center">选择
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td style="height: 25px">
                                                <%# Eval("supplier_name")%>
                                            </td>
                                            <td><%# Eval("contact_name")%></td>
                                            <td>
                                                <%#Eval("supplier_city")%>
                                            </td>
                                            <td>
                                                <a style="cursor: pointer" onclick="showSupplierPOList('<%# Eval("supplier_name") %>',0);"
                                                    title="历史订单"><%#Eval("OrderAmount")%></a>
                                            </td>
                                            <td>
                                                <%#Eval("goodAmount")%>
                                            </td>
                                            <td>
                                                <%#Eval("normalAmount")%>
                                            </td>
                                            <td>
                                                <%#Eval("badAmount")%>
                                            </td>
                                            <td><%# ViewSupplierPriceLevel(Eval("PriceLevel"))%></td>
                                            <td>&nbsp;</td>
                                            <td align="center">
                                                <asp:Button ID="btnSelectSupplier" runat="server" Text="选择" CausesValidation="false" CommandArgument='<%# Eval("id").ToString()+"-"+ Eval("supplyId").ToString() %>'
                                                    CssClass="widebuttons" OnClick="btnSelectSupplier_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <asp:Button ID="btnFP2" runat="server" Text="首页" CssClass="widebuttons" OnClick="btnFP2_Click" />
                                            <asp:Button ID="btnPP2" runat="server" Text="上页" CssClass="widebuttons" OnClick="btnPP2_Click" />
                                            <asp:Button ID="btnNP2" runat="server" Text="下页" CssClass="widebuttons" OnClick="btnNP2_Click" />
                                            <asp:Button ID="btnLP2" runat="server" Text="末页" CssClass="widebuttons" OnClick="btnLP2_Click" />
                                        </td>
                                        <td align="right" width="50%">页数：<asp:Literal ID="litCurrentPage2" runat="server" />/<asp:Literal ID="litTotalPage2" runat="server" />
                                            &nbsp;&nbsp;记录数：<asp:Literal ID="litCount2" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="heading">供应商历史</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="rpHis" runat="server">
                                    <HeaderTemplate>
                                        <table width="100%" border="0" class="gridView">
                                            <tr class="Gheading">
                                                <td style="height: 30px" width="18%">供应商名称
                                                </td>
                                                <td>联系人</td>
                                                <td width="5%">地区
                                                </td>
                                                <td>历史订单数</td>
                                                <td>好评</td>
                                                <td>中评</td>
                                                <td>差评</td>
                                                <td>价格水平</td>
                                                <td>参考报价</td>
                                                <td width="4%" align="center">选择
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td style="height: 25px">
                                                <%# Eval("supplier_name")%>
                                            </td>
                                            <td><%# Eval("contact_name")%></td>
                                            <td>
                                                <%#Eval("supplier_city")%>
                                            </td>
                                            <td>
                                                <a style="cursor: pointer" onclick="showSupplierPOList('<%# Eval("supplier_name") %>',0);"
                                                    title="历史订单"><%#Eval("OrderAmount")%></a>
                                            </td>
                                            <td>
                                                <%#Eval("goodAmount")%>
                                            </td>
                                            <td>
                                                <%#Eval("normalAmount")%>
                                            </td>
                                            <td>
                                                <%#Eval("badAmount")%>
                                            </td>
                                            <td><%# ViewSupplierPriceLevel(Eval("PriceLevel"))%></td>
                                            <td>&nbsp;</td>
                                            <td align="center">
                                                <asp:Button ID="btnSelectSupplierHist" runat="server" Text="选择" CausesValidation="false" CommandArgument='<%# Eval("id").ToString() %>'
                                                    CssClass="widebuttons" OnClick="btnSelectSupplierHist_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <asp:Button ID="btnFP3" runat="server" Text="首页" CssClass="widebuttons" OnClick="btnFP3_Click" />
                                            <asp:Button ID="btnPP3" runat="server" Text="上页" CssClass="widebuttons" OnClick="btnPP3_Click" />
                                            <asp:Button ID="btnNP3" runat="server" Text="下页" CssClass="widebuttons" OnClick="btnNP3_Click" />
                                            <asp:Button ID="btnLP3" runat="server" Text="末页" CssClass="widebuttons" OnClick="btnLP3_Click" />
                                        </td>
                                        <td align="right" width="50%">页数：<asp:Literal ID="litCurrentPage3" runat="server" />/<asp:Literal ID="litTotalPage3" runat="server" />
                                            &nbsp;&nbsp;记录数：<asp:Literal ID="litCount3" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="palSecretAdd" runat="server" Visible="False">
                        <br />
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tableForm">
                            <tr>
                                <td class="heading">手工添加供应商</td>
                            </tr>
                            <tr>
                                <td class="oddrow-l">
                                    <asp:Button ID="btnSecret" runat="server" Text="添加" Visible="False" CssClass="widebuttons" OnClick="btnSecret_Click" /></td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <br />
                    <asp:Button ID="btnPSuplier" runat="server" Text="上一步" OnClick="btnPSuplier_Click" CssClass="widebuttons" CausesValidation="False" />
                    <asp:Button ID="btnBSupplier" runat="server" Text="返回" OnClick="btnBack_Click" CssClass="widebuttons" CausesValidation="False" />
                </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="Tab2" runat="server" HeaderText="采购物品" Visible="false">
            <ContentTemplate>
                <asp:Panel ID="palML" runat="server" Visible="false">
                    <a href="/Purchase/Requisition/BijiaDlg.aspx" onclick="window.open('/Purchase/Requisition/BijiaDlg.aspx','','width=800,height=400');return false;" style="cursor: hand;">
                        <img src="/images/03.jpg" /></a>
                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="oddrow" style="width: 20%">供应商名称:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labSupplierName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">物料类别:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labProductType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">采购物品:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">具体描述:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox ID="desctiprtion" runat="server" Height="100px" Width="260"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator16" runat="server" ControlToValidate="desctiprtion" Display="None"
                                    ErrorMessage="具体描述为必填" /><font color="red"> * </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">预计收货时间:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="intend_receipt_date"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="intend_receipt_date"
                                    Display="None" ErrorMessage="预计收货起始时间为必填" /><font color="red"> * </font>&nbsp;<img
                                        src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=intend_receipt_date.ClientID %>'), document.getElementById('<%=intend_receipt_date.ClientID %>'), 'yyyy-mm-dd');" />
                                -
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="Eintend_receipt_date"></asp:TextBox>&nbsp;<img
                                    src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=Eintend_receipt_date.ClientID %>'), document.getElementById('<%=Eintend_receipt_date.ClientID %>'), 'yyyy-mm-dd');" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">单价:
                            </td>
                            <td class="oddrow-l">
                                <asp:TextBox ID="labPrice" runat="server" MaxLength="10" onblur="account();" /><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator7" runat="server" ControlToValidate="labPrice" Display="None"
                                    ErrorMessage="单价为必填" /><font color="red"> * </font>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="labPrice"
                                    ErrorMessage="请输入正确单价" ValidationExpression="^[+-]?(\d{1,3},?)+(\.\d+)?$">请输入正确单价</asp:RegularExpressionValidator>
                                <asp:HiddenField ID="hidMLPrice" runat="server" />
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="checkMLPrice"
                                    Display="None" ErrorMessage="不能上调目录物品单价" />
                            </td>
                        </tr>

                        <tr>
                            <td class="oddrow">货币:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labMoneyType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">单位:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labUnit" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">数量:
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
                            <td class="oddrow">小计:
                            </td>
                            <td class="oddrow-l">
                                <asp:Label ID="labtotal" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                <%--报价附件:--%>
                                报价信息:
                            </td>
                            <td class="oddrow-l">
                                 <asp:TextBox ID="txtPriceAtt" runat="server" Width="150"></asp:TextBox>
                                <input type="button" class="widebuttons" value="添加附件" id="btnPriceAtt" runat="server"  onclick="showPriceAtt();return false;" />&nbsp;供应商报价
                                <br />
                                <asp:Label ID="lbPriceAtt" runat="server"></asp:Label>
                                
                                <asp:HiddenField ID="hidNames" runat="server" />
                                <br />
                                <span id="spBJ2" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSave1" runat="server" Text=" 保存 " CssClass="widebuttons"
                                    OnClick="btnSave1_Click" />&nbsp;<asp:Button ID="btnNext1" runat="server" Text=" 添加 "
                                        CssClass="widebuttons" OnClick="btnNext1_Click" />
                                &nbsp;<asp:Button ID="btnBack1" runat="server" Text=" 返回 " CssClass="widebuttons"
                                    CausesValidation="false" OnClick="btnBack_Click" />&nbsp;<asp:Button ID="btnP1" CausesValidation="false"
                                        runat="server" Text=" 上一步 " CssClass="widebuttons" OnClick="btnP_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palFML" runat="server" Visible="false">
                    <a href="/Purchase/Requisition/BijiaDlg.aspx" onclick="window.open('/Purchase/Requisition/BijiaDlg.aspx','','width=800,height=400');return false;" style="cursor: hand;">
                        <img src="/images/03.jpg" /></a>
                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="oddrow" style="width: 20%">供应商名称:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtSupplierName" runat="server" Enabled="false" Width="270" /><font
                                    color="red"> * </font>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                    runat="server" ControlToValidate="txtSupplierName" Display="None" ErrorMessage="请添加供应商" />
                            </td>
                        </tr>
                        <asp:Panel ID="palSecret" runat="server" Visible="false">
                            <tr>
                                <td class="oddrow" width="20%">供应商地址:</td>
                                <td class="oddrow-l" width="30%">
                                    <asp:TextBox ID="txtAddress" runat="server" />
                                    <font color="red">* </font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtAddress" Display="None" ErrorMessage="请填写供应商地址"></asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" width="20%">供应商联系人:</td>
                                <td class="oddrow-l" width="30%">
                                    <asp:TextBox ID="txtLinker" runat="server" />
                                    <font color="red">* </font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtLinker" Display="None" ErrorMessage="请填写供应商联系人"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">供应商联系电话:</td>
                                <td class="oddrow-l">
                                    <table border="0" cellpadding="0" cellspacing="0" class="XTable">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtsupplier_con" runat="server" />

                                            <td>
                                                <font color="red">* </font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">例:86-010-85078888-0001
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="oddrow">供应商传真:</td>
                                <td class="oddrow-l">
                                    <table border="0" cellpadding="0" cellspacing="0" class="XTable">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtsupplierfax_con" runat="server" />
                                        </tr>
                                        <tr>
                                            <td colspan="5">例:86-010-85078888-0001
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">供应商手机:</td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtMobile" runat="server" /><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtMobile"
                                        Display="Dynamic" ErrorMessage="供应商手机格式错误" ValidationExpression="(\d)*"></asp:RegularExpressionValidator></td>
                                <td class="oddrow">供应商邮件:</td>
                                <td class="oddrow-l">
                                    <asp:TextBox ID="txtEmail" runat="server" /><font color="red"> * </font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage="请填写供应商邮件"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                        ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="供应商邮件格式错误"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>例:mail@sohu.com
                                </td>
                            </tr>
                        </asp:Panel>

                        <asp:Panel ID="palFX" runat="server" Visible="false">
                            <tr>
                                <td class="oddrow" style="width: 20%">审批流向:
                                </td>
                                <td class="oddrow-l" colspan="3">
                                    <asp:RadioButtonList ID="rblrequisitionflow" runat="server" RepeatDirection="horizontal" name="radFlow" onclick="clkFlow(this);"
                                        CssClass="XTable">
                                    </asp:RadioButtonList>
                                    <br />
                                    <span style="color: Red">PR-PO: 选择PR-PO, 系统自动生成订单(PO). 若供应商提供邮件地址的, 系统发出PO至供应商处进行确认, 同时附公司订单标准条款". 订单方式流转速度快,
                                                    操作简单. 适用于多数情况.<br />
                                        PR-合同: 当需要签署采购合同时,选择PR-合同. 合同需要进行线下签署,合同号与本申请单号码相同,以便检索查询. 
                                        PR-框架合同:当与供应商签署框架合同时，需要填写押金金额，采购物品金额为零，账期系统自动录入.
                                    </span>
                                </td>
                            </tr>

                            <tr>
                                <td class="oddrow">供应商来源:</td>
                                <td class="oddrow-l" colspan="3">
                                    <asp:DropDownList ID="ddlsource" runat="server" onchange="setsource(this);">
                                        <asp:ListItem Value="0">请选择</asp:ListItem>
                                        <asp:ListItem>业务推荐</asp:ListItem>
                                        <asp:ListItem>客户指定</asp:ListItem>
                                        <asp:ListItem>采购部推荐</asp:ListItem>
                                        <asp:ListItem>临时供应商</asp:ListItem>
                                    </asp:DropDownList>
                                    <font color="red">* </font>
                                    <asp:CompareValidator ID="CompareValidator3" ControlToValidate="ddlsource" ValueToCompare="0"
                                        Operator="NotEqual" runat="server" Display="None" ErrorMessage="请选择供应商来源">
                                    </asp:CompareValidator>
                                    <div id="divEmail" runat="server" style="display: none">
                                        客户指定邮件:
                        <asp:FileUpload ID="filEmailFile" runat="server" /><font color="red"> * </font>
                                        <asp:Label ID="labEmailFile" runat="server" />
                                    </div>
                                    <asp:CustomValidator runat="server" ID="CustomValidator2" ClientValidationFunction="chkEmailFile" ErrorMessage="请上传客户指定邮件附件" Display="None" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">开户公司名称:
                                </td>
                                <td class="oddrow-l" colspan="3">
                                    <asp:TextBox ID="txtaccountName" runat="server" Width="270" /><font color="red"> *
                                    </font>
                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator10"
                                        runat="server" ControlToValidate="txtaccountName" Display="None" ErrorMessage="请填写开户公司名称" />
                                    <font color="red">开户公司名称必须与供应商名称一致，否则请提供证明文件！</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">开户银行:
                                </td>
                                <td class="oddrow-l" colspan="3">
                                    <asp:TextBox ID="txtaccountBank" runat="server" Width="270" /><font color="red"> *
                                    </font>
                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator11"
                                        runat="server" ControlToValidate="txtaccountBank" Display="None" ErrorMessage="请填写开户银行" />
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow">帐号:
                                </td>
                                <td class="oddrow-l" colspan="3">
                                    <asp:TextBox ID="txtaccountNum" runat="server" Width="270" /><font color="red"> *</font>
                                    <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator12" runat="server"
                                        ControlToValidate="txtaccountNum" Display="None" ErrorMessage="请填写帐号" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="oddrow">物料类别:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:DropDownList ID="ddlType1" runat="server" AutoPostBack="true" />
                                &nbsp;<asp:DropDownList ID="ddlType2" runat="server" AutoPostBack="true" />
                                <asp:HiddenField ID="hidType2" runat="server" />
                                &nbsp<asp:DropDownList ID="ddlType3" runat="server" onclick="" />
                                <font color="red">* </font>
                                <asp:CompareValidator ID="CompareValidator2" ControlToValidate="ddlType3" Display="None"
                                    Type="String" ValueToCompare="-1" Operator="NotEqual" ErrorMessage="请选择物料类别"
                                    runat="server"></asp:CompareValidator><asp:CompareValidator ID="CompareValidator1"
                                        ControlToValidate="ddlType3" Display="None" Type="String" ValueToCompare="" Operator="NotEqual"
                                        ErrorMessage="请选择物料类别" runat="server"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">采购物品:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtProductName" runat="server" Width="270"></asp:TextBox>
                                <font color="red">* </font>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProductName"
                                    Display="None" ErrorMessage="采购物品为必填"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">具体描述:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="desctiprtion1" runat="server" Width="270" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="desctiprtion1"
                                    Display="None" ErrorMessage="具体描述为必填" /><font color="red"> * </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">预计收货时间:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="intend_receipt_date1"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="intend_receipt_date1"
                                    Display="None" ErrorMessage="预计收货起始时间为必填" /><font color="red"> * </font>&nbsp;<img
                                        src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=intend_receipt_date1.ClientID %>'), document.getElementById('<%=intend_receipt_date1.ClientID %>'), 'yyyy-mm-dd');" />
                                -
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="Eintend_receipt_date1"></asp:TextBox>&nbsp;<img
                                    src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=Eintend_receipt_date1.ClientID %>'), document.getElementById('<%=Eintend_receipt_date1.ClientID %>'), 'yyyy-mm-dd');" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">单价:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtPrice" runat="server" Width="130" onblur="account1();" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPrice"
                                    Display="None" ErrorMessage="单价为必填" /><font color="red"> * </font>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPrice"
                                    ErrorMessage="请输入正确单价" ValidationExpression="^[+-]?(\d{1,3},?)+(\.\d+)?$">请输入正确单价</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">货币:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:Label ID="labMoneyType1" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="oddrow">单位:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtUnit1" runat="server" MaxLength="20" Width="130"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUnit1" Display="None"
                                    ErrorMessage="单位为必填" /><font color="red"> * </font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">数量:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="quantity1" runat="server" Width="130" onblur="account1();" MaxLength="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="quantity1"
                                    Display="None" ErrorMessage="数量为必填" /><font color="red"> * </font>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="quantity1"
                                    ErrorMessage="请输入正确数量" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确数量</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">小计:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txttotal" Enabled="false" runat="server" Width="130px" MaxLength="20" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">报价信息:
                            </td>
                            <td class="oddrow-l" colspan="3">
                                                        <asp:TextBox ID="txtPriceAtt1" runat="server" Width="150"></asp:TextBox>
                                <input type="button" class="widebuttons" value="添加附件" id="btnPriceAtt1" onclick="showPriceAtt1();return false;" runat="server" />&nbsp;供应商报价
                                <br />
                                <asp:Label ID="lbPriceAtt1" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidNames1" runat="server" />
                                <br />
                                <span id="spBJ1" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnSave2" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave2_Click" />&nbsp;<asp:Button
                                    ID="btnNext2" runat="server" Text=" 添加 " OnClick="btnNext2_Click" CssClass="widebuttons" />
                                <asp:Button ID="btnBack2" runat="server" Text=" 返回 " CssClass="widebuttons" CausesValidation="false"
                                    OnClick="btnBack_Click" />&nbsp;<asp:Button ID="btnP2" runat="server" Text=" 上一步 "
                                        CausesValidation="false" CssClass="widebuttons" OnClick="btnP_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" />
</asp:Content>
