<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
 CodeBehind="ESPAndSupplyTypeInfo.aspx.cs" Inherits="ESPAndSupplyTypeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="/public/js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/public/js/dialog.js"></script>
<script type="text/javascript">
    function btnTypeClick() {
        window.location.href = "ESPAndSupplyTypeInfo.aspx?tid=0";
    }
    function EmplyeeClick(type) {
        var win = window.open('../Requisition/EmployeeList.aspx?type=' + type, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function AreYouDelTrue() {
        var bConfirmed = window.confirm("系统间将失去关联！")
        if (bConfirmed == true)
            return true;
        else
            return false;
    }
    function selectType(keys) {
        if (keys == "") {
            dialog("选择关联的供应链物料", "iframe:SelectTypeSupply.aspx", "400px", "600px", "text");
        }
    }
</script>
    <style type="text/css">
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
    </style>
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
        .userLabel
        {
	        cursor:pointer;
	        text-decoration:none;
	        color:#7282A9;
        }
    </style>
    <style type="text/css">
.tdTag1{background-image: url(/images/100222_03.jpg);background-repeat: no-repeat;width: 107px;height: 26px;color:#000;font-size:12px;font-weight:bold;}
.tdTag2{background-image: url(/images/100222_05.jpg);background-repeat: no-repeat;width: 101px;height: 26px;color:#000;font-size:12px;font-weight:bold;}
    </style> 
<div><ul style="width:100%; margin:0 auto;">
<li style=" list-style-type:none;float:left; width:20%">
 <table><tr>
        <td class="tdTag1" align="center">
        <asp:HyperLink ID="lbesp" runat="server"  Text="采购系统" NavigateUrl="ESPAndSupplyTypeInfo.aspx?tid=0" ForeColor="White"  />
        </td>
        <td class="tdTag2" align="center">
        <asp:HyperLink ID="lbsupply" runat="server" Text="供应链" NavigateUrl="SupplyAndESPTypeInfo.aspx?tid=0"  ForeColor="Black"/>
        </td>
</tr>
            <tr>
                <td colspan="2" style="height: 20px" class="oddrow-l">
                </td>
            </tr>
</table>
         <table width="80%"  border="0">
            <tr>
                <td valign="top">
                    <div style="position: relative; vertical-align: top; padding-left: 10px;
                        height: 100%">
                        <span id="Span0" runat="server" onclick="btnTypeClick();" style="cursor:pointer" >物料类别</span>
                        <yyc:SmartTreeView ID="stv0" runat="server" AllowCascadeCheckbox="True" ImageSet="Msdn" ShowLines="true"
                            NodeIndent="20" Width="200px" >
                            <ParentNodeStyle Font-Bold="False" />
                            <HoverNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                            <SelectedNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                            <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="1px" VerticalPadding="2px" Width="100px" />
                        </yyc:SmartTreeView>
                    </div>
                </td>
            </tr>
        </table>
</li>

<li style=" list-style-type:none;float:left;width:75%">
       <table class="tableForm" border="0" width="100%">
            <tr id="trEdit" runat="server">
                <td colspan="4">
                    <table id="tab1" width="100%" border="0" runat="server">
                        <tr>
                            <td class="heading" colspan="4">
                                <asp:Label ID="lblEditTitle" runat="server" Style="font-size: 14px; color: #333333; font-weight: bold;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 20%">
                                物料类别名称：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtName" runat="server" /><font color="red"> * </font>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="物料名称为必填"
                                    Display="None" ControlToValidate="txtName" ValidationGroup="g1"></asp:RequiredFieldValidator>
                            </td>
                            <td class="oddrow" style="width: 20%">
                                初审人：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtAuditor" runat="server" Enabled="false" /><font color="red"><asp:Label
                                    ID="labaudit1" runat="server"> * </asp:Label></font>
                                <asp:Button ID="btnSelect" OnClientClick="EmplyeeClick('producttype1');return false;"
                                    CssClass="widebuttons" runat="server" Text="选择" />
                                <asp:HiddenField ID="hidAuditor" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择初审人"
                                    Display="None" ControlToValidate="txtAuditor" ValidationGroup="g1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 20%">
                                上海分公司审核人：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtSHAuditor" runat="server" Enabled="false" />
                                <asp:Button ID="btnSelectSH" OnClientClick="EmplyeeClick('producttype3');return false;"
                                    CssClass="widebuttons" runat="server" Text="选择" />
                                <asp:HiddenField ID="hidSHAuditor" runat="server" />
                            </td>
                            <td class="oddrow" style="width: 20%">
                                广州分公司审核人：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtGZAuditor" runat="server" Enabled="false" />
                                <asp:Button ID="btnSelectGZ" OnClientClick="EmplyeeClick('producttype4');return false;"
                                    CssClass="widebuttons" runat="server" Text="选择" />
                                <asp:HiddenField ID="hidGZAuditor" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="oddrow-l">
                                <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClick="btnSave_Click" ValidationGroup="g1"
                                    CssClass="widebuttons" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lab1" runat="server" Style="font-size: 14px; color: #333333; font-weight: bold;
                        padding: 0 0 0 10px;"></asp:Label>
                </td>
            </tr>
            <tr id="trEdit1" runat="server">
                <td colspan="4" style="height: 20px" class="oddrow-l">
                </td>
            </tr>
            
            <tr runat="server" id="tr2">
                <td colspan="4">
                    <table id="tab2" width="100%" border="0" runat="server">
                            <tr>
                                <td colspan="4" class="heading">
                                    <asp:Label ID="lblAddTitle" runat="server" Style="font-size: 14px; color: #333333; font-weight: bold;"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">
                                    物料类别名称：
                                </td>
                                <td class="oddrow-l" style="width: 30%">
                                    <asp:TextBox ID="txtName1" runat="server" /><font color="red"> * </font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="物料名称为必填"
                                        Display="None" ControlToValidate="txtName1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                </td>
                                <td class="oddrow" style="width: 20%">
                                    初审人：
                                </td>
                                <td class="oddrow-l" style="width: 30%">
                                    <asp:TextBox ID="txtAuditor1" runat="server" Enabled="false" /><font color="red"><asp:Label
                                        ID="labaudit2" runat="server"> * </asp:Label></font>
                                    <asp:Button ID="btnSelect1" OnClientClick="EmplyeeClick('producttype2');return false;"
                                        CssClass="widebuttons" runat="server" Text="选择" />
                                    <asp:HiddenField ID="hidAuditor1" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请选择初审人"
                                        Display="None" ControlToValidate="txtAuditor1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">
                                    上海分公司审核人：
                                </td>
                                <td class="oddrow-l" style="width: 30%">
                                    <asp:TextBox ID="txtSHAuditor1" runat="server" Enabled="false" />
                                    <asp:Button ID="btnSelectSH1" OnClientClick="EmplyeeClick('producttype5');return false;"
                                        CssClass="widebuttons" runat="server" Text="选择" />
                                    <asp:HiddenField ID="hidSHAuditor1" runat="server" />
                                </td>
                                <td class="oddrow" style="width: 20%">
                                    广州分公司审核人：
                                </td>
                                <td class="oddrow-l" style="width: 30%">
                                    <asp:TextBox ID="txtGZAuditor1" runat="server" Enabled="false" />
                                    <asp:Button ID="btnSelectGZ1" OnClientClick="EmplyeeClick('producttype6');return false;"
                                        CssClass="widebuttons" runat="server" Text="选择" />
                                    <asp:HiddenField ID="hidGZAuditor1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="oddrow-l">
                                    <asp:Button ID="btnSave1" runat="server" ValidationGroup="g2" CssClass="widebuttons"
                                        Text=" 添加 " OnClick="btnSave1_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="oddrow-l" style="height: 20px">
                                </td>
                            </tr>
                    </table>
                </td>
            </tr>
            
            <tr  runat="server" id="tr3">
                 <td colspan="4">
                    <table id="tab3" width="100%" border="0" runat="server">
                        <tr>
                            <td colspan="4" class="heading">
                                <asp:Label ID="lblListTitle" runat="server" Style="font-size: 14px; color: #333333; font-weight: bold;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="oddrow-l">
                                <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                    OnRowDataBound="Items_RowDataBound" OnRowCommand="Items_RowCommand" DataKeyNames="typeid">
                                    <Columns>
                                        <asp:BoundField HeaderText="物料类别名称" DataField="typename" />
                                        <asp:TemplateField HeaderText="采购初审人">
                                            <ItemTemplate>
                                                <%# Eval("auditorid").ToString() == "" ? "" : new ESP.Compatible.Employee(int.Parse(Eval("auditorid").ToString())).Name%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="业务初审人">
                                            <ItemTemplate>
                                                <%# Eval("operationflow") == DBNull.Value ? "" : ESP.Purchase.Common.State.typeoperationflowAuditNames[int.Parse(Eval("operationflow").ToString())]%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                            <asp:Literal ID="litEdit" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="image" ImageUrl="~/images/disable.gif" HeaderText="停用"
                                            ControlStyle-Font-Underline="true" CommandName="Del" CausesValidation="false"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="image" ImageUrl="~/images/used.gif" HeaderText="启用"
                                            ControlStyle-Font-Underline="true" CommandName="Use" CausesValidation="false"
                                            ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow-l" colspan="4" style="height: 20px"> 
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr  runat="server" id="tr4" visible="false">
                <td colspan="4">
                    <table id="tab4" width="100%" border="0" runat="server">
                        <tr>
                            <td colspan="4" class="heading">
                                系统间物料关联列表  <asp:Button ID="btnShowAddLink" runat="server" Text="添加关联 " CssClass="widebuttons" OnClick="btnShowAddLink_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="oddrow-l">
                                <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                    OnRowDataBound="gv1_RowDataBound" OnRowCommand="gv1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="采购系统物料名称">
                                            <ItemTemplate>
                                               <asp:Literal ID="litEspType" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="供应链物料名称">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSupplyType" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelLink" OnClientClick="return AreYouDelTrue();" CommandArgument='<%# Eval("ID") %>' CommandName="DelLink" runat="server" ImageUrl="~/images/i.p.cancel.gif" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="oddrow-l" style="height: 20px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr  runat="server" id="tr5" visible="false">
                <td colspan="4">
                    <table id="tab5" width="100%" border="0" runat="server">
                        <tr>
                            <td colspan="4" class="heading">
                                系统间物料关联维护 
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 20%">
                                采购物料类别名称：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtEspTypeName" runat="server" Enabled="false" />
                            </td>
                            <td class="oddrow" style="width: 20%">
                                供应链物料类别名称：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtSupplyTypeName" runat="server" Enabled="false" /><font color="red"><asp:Label
                                    ID="Label1" runat="server"> * </asp:Label></font>
                                <asp:Button ID="btnSelectType" OnClientClick="selectType('');return false;"
                                    CssClass="widebuttons" runat="server" Text="选择" />
                                <asp:HiddenField ID="hidSupplyTypeid" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请选关联物料"
                                    Display="None" ControlToValidate="txtSupplyTypeName" ValidationGroup="g3"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" class="oddrow-l">
                                <asp:Button ID="btnSave2" runat="server" Text=" 保存 " OnClick="btnSave2_Click" ValidationGroup="g3"
                                    CssClass="widebuttons" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="oddrow-l" style="height: 20px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trBtn" runat="server" visible="false">
                <td class="oddrow-l" colspan="4">
                    <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" Visible="false" />
                    &nbsp;<asp:Button ID="btnPre" runat="server" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"  Visible="false" />
                </td>
            </tr>
        </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ValidationGroup="g1"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ValidationGroup="g2"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ValidationGroup="g3"
        ShowSummary="false" />    
</li>        
</ul></div>  
</asp:Content>
