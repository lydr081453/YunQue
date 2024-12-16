<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="syncSupplierInfo.aspx.cs" Inherits="PurchaseWeb.Purchase.supplier.syncSupplierInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/public/js/jquery.js" type="text/javascript"></script>

    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>

    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script src="/public/js/jquery.js" type="text/javascript"></script>

    <script src="/public/js/jquery.history_remote.pack.js" type="text/javascript"></script>

    <script src="/public/js/jquery.tabs.pack.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/dialog.js"></script>

    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page *//* Default tab */.AjaxTabStrip .ajax__tab_tab
        {
            line-height: 24px;
            width: 101px;
            color: #606787;
            background-image: url(/images/100222_05.jpg);
            background-repeat: no-repeat;
            background-position: left bottom;
            height: 24px;
            text-decoration: none;
        }
        /* When mouse over */.AjaxTabStrip .ajax__tab_hover .ajax__tab_tab
        {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */.AjaxTabStrip .ajax__tab_active .ajax__tab_tab
        {
            width: 107px;
            line-height: 26px;
            text-decoration: none;
            font-weight: bold;
            color: #FFFFFF;
            background-image: url(/images/100222_03.jpg);
            background-repeat: no-repeat;
            height: 26px;
        }
        /* TabPanel Content */.ajax__tab_header
        {
            height: 26px;
        }
        #floatBoxBg
        {
            display: none;
            width: 100%;
            height: 100%;
            background: #000;
            position: absolute;
            top: 0;
            left: 0;
        }
        .floatBox
        {
            border: #666 5px solid;
            width: 300px;
            position: absolute;
            top: 50px;
            left: 40%;
        }
        .floatBox .title
        {
            height: 23px;
            padding: 7px 10px 0;
            background: #333;
            color: #fff;
        }
        .floatBox .title h4
        {
            float: left;
            padding: 0;
            margin: 0;
            font-size: 14px;
            line-height: 16px;
        }
        .floatBox .title span
        {
            float: right;
            cursor: pointer;
        }
        .floatBox .content
        {
            padding: 20px 15px;
            background: #fff;
        }
        #floatBoxBg2
        {
            display: none;
            width: 100%;
            height: 100%;
            background: #000;
            position: absolute;
            top: 0;
            left: 0;
        }
        .floatBox2
        {
            border: #666 5px solid;
            width: 300px;
            position: absolute;
            top: 50px;
            left: 40%;
        }
        .floatBox2 .title
        {
            height: 23px;
            padding: 7px 10px 0;
            background: #333;
            color: #fff;
        }
        .floatBox2 .title h4
        {
            float: left;
            padding: 0;
            margin: 0;
            font-size: 14px;
            line-height: 16px;
        }
        .floatBox2 .title span
        {
            float: right;
            cursor: pointer;
        }
        .floatBox2 .content
        {
            padding: 20px 15px;
            background: #fff;
        }
    </style>

    <script language="javascript">
        function selectSupplier() {
            var key = "";
            if (document.all)
                key = document.getElementById("<%= labSName.ClientID %>").innerText;
            else
                key = document.getElementById("<%= labSName.ClientID %>").innerContent;
            flag = 1;
            dialog("检索供应商", "iframe:searchSupplier.aspx?key=" + key, "800px", "500px", "text");
        }

        function setValues(supplierId) {
            document.getElementById("<%= hidSupplierId.ClientID %>").value = supplierId;
            document.getElementById("<%= linkBind.ClientID %>").click();
        }

        function checkName() {
            var name1 = document.getElementById("<%=txtSupplierName.ClientID %>").value;
            var name2 = "";
            if (document.all)
                name2 = document.getElementById("<%= labSName.ClientID %>").innerText;
            else
                name2 = document.getElementById("<%= labSName.ClientID %>").innerContent;
            if (name1 != name2) {
                if (confirm("供应商名称不一致，您确定执行同步操作码？")) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return true;
            }
        }
        function btnTypeClick() {
            dialog("选择服务类型", "iframe:/supplierchain/manageinfo/SelectTypeList.aspx?sid=" + '<%= Request["sid"] %>', "1000px", "660px", "text");
        }
        function onPageRefresh1() {
            

        }
        function onPageRefresh() {
            if (document.getElementById("<%=TabPanel4.ClientID %>").style.display == "none") {
                document.getElementById("<%=lnk.ClientID %>").click();
            } else {
            document.getElementById("<%=lnk1.ClientID %>").click();
            }

        }
        function linkManClick(suid) {

            dialog("用户信息", "iframe:/supplierchain/ManageInfo/SupplierSubsidiaryUsersEdit.aspx?suid=" + suid, "700px", "450px", "text");
        }

        function editML(syncId, allIds) {
            dialog("目录物品", "iframe:mlProducts.aspx?syncId=" + syncId + "&allIds=" + allIds, "980px", "550px", "text");
        }
    </script>

    <asp:ScriptManager ID="manager1" runat="server">
    </asp:ScriptManager>
    <table width="98%" border="0" cellpadding="3" cellspacing="3" style="border: solid 1px gray">
        <tr>
            <td class="heading" colspan="4" align="left">
                供应链平台
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="20%">
                公司全称(中文)：
            </td>
            <td class="oddrow-l" width="30%">
                <asp:Label ID="labSName" runat="server" />
            </td>
            <td class="oddrow" width="20%">
                所在省/市：
            </td>
            <td class="oddrow-l" width="30%">
                <asp:Label ID="labCity" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                发票名称：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labInvoiceTitle" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;<asp:Button ID="btnSync" OnClick="btnSync_Click" CausesValidation="false" OnClientClick="return checkName();"
                    runat="server" CssClass="widebuttons" Text="同步到采购系统并建立关联" />
            </td>
        </tr>
    </table>
    <br />
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
        <uc1:TabPanel ID="TabPanel1" HeaderText="基本信息" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="5" cellspacing="5" style="border: solid 1px gray">
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellpadding="3" cellspacing="3" style="border: solid 1px gray">
                                <tr>
                                    <td class="heading" colspan="4" align="left">
                                        采购系统&nbsp;<asp:Button ID="btnSearch" runat="server" CausesValidation="false" OnClientClick="selectSupplier();return false;"
                                            Text="检索现有供应商" CssClass="widebuttons" />
                                        <asp:LinkButton ID="linkBind" runat="server" CausesValidation="false" OnClick="linkBind_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 20%">
                                        供应商全称:<asp:HiddenField ID="hidSupplierId" runat="server" />
                                    </td>
                                    <td class="oddrow-l" style="width: 30%">
                                        <asp:TextBox ID="txtSupplierName" runat="server" MaxLength="200" Width="50%" />&nbsp;<font
                                            color="red">*</font>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupplierName"
                                            Display="None" ErrorMessage="请填写供应商名称"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="oddrow" style="width: 20%">
                                        所在地区:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplier_area1" runat="server" MaxLength="100" Width="50%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 20%">
                                        行业:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplier_industry" runat="server" MaxLength="100" Width="50%" />
                                    </td>
                                    <td class="oddrow">
                                        规模:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplier_scale" runat="server" MaxLength="100" Width="50%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        注册资本:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplier_principal" runat="server" MaxLength="50" Width="50%" />
                                    </td>
                                    <td class="oddrow">
                                        成立年限:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplier_builttime" runat="server" MaxLength="50" Width="50%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        网址:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtsupplier_website" runat="server" MaxLength="100" Width="50%" />
                                    </td>
                                    <td class="oddrow">
                                        协议框架号:
                                    </td>
                                    <td class="oddrow-l">
                                        <asp:TextBox ID="txtfa_no" runat="server" MaxLength="50" Width="50%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        使用状态:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:DropDownList ID="ddlStatus" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="heading" colspan="4">
                                        帐户信息
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 20%">
                                        开户公司名称:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtaccount_name" runat="server" MaxLength="100" Width="50%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width: 20%">
                                        开户银行:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtaccount_bank" runat="server" MaxLength="100" Width="50%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">
                                        帐号:
                                    </td>
                                    <td class="oddrow-l" colspan="3">
                                        <asp:TextBox ID="txtaccount_number" runat="server" MaxLength="100" Width="50%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnSave1" OnClick="btnSave1_Click" runat="server" CssClass="widebuttons"
                                            Text="保存" />
                                        &nbsp;<asp:Button ID="btnSave2" runat="server" CssClass="widebuttons" CausesValidation="false"
                                            OnClick="btnSave2_Click" Text="编辑更多" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel4" runat="server" HeaderText="用户信息">
            <ContentTemplate>
                <asp:LinkButton ID="lnk1" runat="server" OnClick="lnk1_Click" />
                    <table width="100%" border="0" cellpadding="5" cellspacing="5" style="border:solid 1px gray">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvUser" runat="server" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvUser_RowDataBound" Font-Size="18px" >
            <Columns>
                 <asp:TemplateField HeaderText="姓名(中文)" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:HiddenField ID="hID" runat="server"  Value='<%# Eval("ID") %>' />
                        <asp:Label ID="lbUserNameCN" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名(英文)" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lbUserNameEN" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="固定电话" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lbUserPhone" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="手机" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lbUserMobile" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lbUserEmail" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所属部门" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lbUserDep" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="职位" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lbUserDuties" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Literal ID="litEdit" runat="server" />  
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
       </asp:GridView>
                                </td>
                            </tr>
                        </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel2" HeaderText="服务类型" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="5" cellspacing="5" style="border: solid 1px gray">
                    <tr>
                        <td width="50%" valign="top">
                            <input type="button" class="widebuttons" value="修改服务类型" id="btnType" onclick="btnTypeClick();" />
                            <asp:LinkButton ID="lnk" runat="server" OnClick="lnk_Click" />
                            <asp:DataList ID="rp1" ItemStyle-BorderStyle="None" runat="server" OnItemDataBound="rp1_ItemDataBound"
                                RepeatDirection="Vertical" Width="100%">
                                <ItemTemplate>
                                    <table width="100%" style="border: 0 0 0; margin: 0 0 0 0;">
                                        <tr>
                                            <td colspan="1" width="15%">
                                                <asp:Label ID="lblMain" runat="server" ForeColor="SteelBlue" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td colspan="3" style="border-bottom: 1px dotted #CC3333" width="85%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:DataList ID="ListLevel2" ItemStyle-BorderStyle="None" runat="server" OnItemDataBound="List2_ItemDataBound"
                                                    Width="100%" ItemStyle-VerticalAlign="Top" RepeatColumns="4" RepeatDirection="Horizontal">
                                                    <ItemTemplate>
                                                        <table width="100%" style="border: 0 0 0; margin: 0 0 0 0">
                                                            <tr>
                                                                <td style="font: small-caption">
                                                                    <asp:Label ID="lblName" Style="width: 80px" runat="server" Text='<%# Eval("Name") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:DataList ID="ListLevel3" ItemStyle-BorderStyle="None" runat="server" Width="100%"
                                                                        ItemStyle-VerticalAlign="Top" RepeatColumns="1" RepeatDirection="Horizontal">
                                                                        <ItemTemplate>
                                                                            <table width="100%" style="border: 0 0 0; margin: 0 0 0 0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <%# Eval("Name") %>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                            <br />
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
        <uc1:TabPanel ID="TabPanel3" HeaderText="协议服务类型" runat="server">
            <ContentTemplate>

                <script language="javascript">
                    function setTypeValues(obj) {
                        var hid = document.getElementById("<%= hidValues.ClientID %>");
                        if (hid.value == "")
                            hid.value = ";";
                        if (obj.checked) {
                            hid.value += obj.value + ";";
                        } else {
                            hid.value = hid.value.replace(";" + obj.value + ";", ";");
                        }
                    }
                </script>

                <table width="100%" border="0" cellpadding="5" cellspacing="5" style="border: solid 1px gray">
                    <tr>
                        <td colspan="2" style="padding: 4px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" class="tableForm">
                                            <tr>
                                                <td class="heading" colspan="4">
                                                    检索
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow" width="15%">
                                                    服务类型名称：
                                                </td>
                                                <td class="oddrow-l" width="35%">
                                                    <asp:TextBox ID="txtName" CssClass="input3" runat="server" Width="200px" MaxLength="20" />&nbsp;
                                                </td>
                                                <td class="oddrow" width="15%">
                                                    协议类型：
                                                </td>
                                                <td class="oddrow-l">
                                                    <asp:DropDownList ID="ddltypes" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow-l" colspan="4" align="center">
                                                    <asp:Button ID="Button1" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="oddrow-l" colspan="4">
                                                    <table border="0" cellpadding="0" cellspacing="0" style =" border-color:White; ">
                                                        <tr>
                                                            <td border="0">
                                                                将选中服务类型设置为:
                                                            </td>
                                                            <td border="0">
                                                                <asp:RadioButtonList ID="rdl" runat="server" RepeatDirection="Horizontal" CssClass="XTable">
                                                                    <asp:ListItem Text="一般" Selected="True" Value="0" />
                                                                    <asp:ListItem Text="协议" Value="1" />
                                                                    <asp:ListItem Text="推荐" Value="2" />
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style=" border:0 0 0 0;">
                                                                <asp:Button ID="btnSet" runat="server" Text=" 保存 " OnClick="btnSet_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:HiddenField ID="hidValues" runat="server" />
                                        <asp:GridView ID="gvTypeList" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                             Width="100%" PagerSettings-Mode="NumericFirstLast"
                                            OnRowDataBound="gvTypeList_RowDataBound" OnPageIndexChanging="gvTypeList_PageIndexChanging"
                                            AllowSorting="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <input type="checkbox" id="chk" runat="server" onclick="setTypeValues(this);" value='<%# Eval("id").ToString() + "-" + Eval("typeId").ToString() %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="服务类型" DataField="typeName" />
                                                <asp:TemplateField HeaderText="协议类型" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litSupplierType" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="目录物品" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button Visible="false" ID="btnML" runat="server" Text="目录物品" CommandArgument='<%# Eval("allTypeId").ToString() %>'
                                                            CssClass="widebuttons" />
                                                            
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </uc1:TabPanel>
    </uc1:TabContainer>
    <br />
    <asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="返回" CssClass="widebuttons"
        OnClick="btnBack_Click" />
</asp:Content>
