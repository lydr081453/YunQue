<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Purchase_Requisition_ProposedSupplierEdit" Codebehind="ProposedSupplierEdit.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script type="text/javascript" language="javascript" src="/public/js/dialog.js"></script>
    <script type="text/javascript" language="javascript">
        function show(url) {
            document.getElementById("img1").src = url.replace(".jpg", "_full.jpg");
            dialog("图片", "id:divshow", "900px", "400px", "text");
        }
        function SelectAll() {
            for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                var e = document.getElementsByName("chkItem")[i];

                e.checked = document.forms[0].chkAll.checked;
            }
        }
    </script>
    <%--<table style="width: 100%">
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">--%>
    <table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">推荐供应商信息查看</td>
    </tr>
    
    <tr>
        <td colspan="4">
            <asp:ScriptManager ID="manager1" runat="server">
            </asp:ScriptManager>
            <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
                <cc1:TabPanel ID="TabPanel1" HeaderText="基础信息" runat="server">
                    <ContentTemplate>
                        <table width="100%" class="tableForm">
                            <tr>
                                <td class="heading" colspan="4">推荐供应商信息编辑
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">供应商全称:
                                </td>
                                <td class="oddrow-l" colspan="3"><asp:TextBox ID="txtSupplierName" runat="server" 
                                        MaxLength="200" Width="80%" />&nbsp;<font color="red">*</font>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupplierName"
                                         Display="None" ErrorMessage="请填写供应商名称"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">所在地区:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtsupplier_area1" runat="server" MaxLength="100" Width="50%"/></td>
                                <td class="oddrow" style="width: 20%">行业:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtsupplier_industry" runat="server" MaxLength="100" Width="50%"/></td>
                            </tr>
                            <tr>
                                <td class="oddrow">规模:</td>
                                <td class="oddrow-l"><asp:TextBox ID="txtsupplier_scale" runat="server" MaxLength="100" Width="50%"/></td>
                                <td class="oddrow">注册资本:</td>
                                <td class="oddrow-l"><asp:TextBox ID="txtsupplier_principal" runat="server" MaxLength="50" Width="50%"/></td>
                            </tr>
                            <tr>
                                <td class="oddrow">成立年限:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtsupplier_builttime" runat="server" MaxLength="50" Width="50%"/></td>
                                <td class="oddrow">网址:</td>
                                <td class="oddrow-l"><asp:TextBox ID="txtsupplier_website" runat="server" MaxLength="100" Width="50%"/></td>
                            </tr>
                            <tr>
                                <td class="oddrow">供应商来源:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtsupplier_source" runat="server" MaxLength="50" Width="50%"/></td>
                                <td class="oddrow">协议框架号:</td>
                                <td class="oddrow-l"><asp:TextBox ID="txtfa_no" runat="server" MaxLength="50" Width="50%"/></td>
                            </tr>
                        </table>
                    <%--</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                        padding-top: 4px;">--%>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" HeaderText="联系信息" runat="server">
                    <ContentTemplate>
                            <asp:Panel ID="Panel2" runat="server">
                                <table width="100%" class="tableForm">
                                    <tr>
                                        <td class="heading" colspan="4">联系信息
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 20%">联系人:</td>
                                        <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtLinker" runat="server" MaxLength="50" Width="50%" /></td>
                                        <td class="oddrow" style="width: 20%">固话:</td>
                                        <td class="oddrow-l" style="width: 30%">
                                            <asp:TextBox ID="txtsupplier_con" runat="server" Width="30px" MaxLength="4" /><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator2" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_con"
                                                 Display="None" ValidationExpression="^\d{2,4}$"></asp:RegularExpressionValidator>-
                                            <asp:TextBox ID="txtsupplier_area" runat="server" Width="30px" MaxLength="5" /><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator4" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_area"
                                                 Display="None" ValidationExpression="^\d{2,5}$"></asp:RegularExpressionValidator>-
                                            <asp:TextBox Width="60px" ID="txtsupplier_phone" runat="server" MaxLength="8" /><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator7" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_phone"
                                                 Display="None" ValidationExpression="^\d{6,8}$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsupplier_phone"
                                                 Display="None" ErrorMessage="请填写固话"></asp:RequiredFieldValidator>-
                                            <asp:TextBox ID="txtsupplier_ext" runat="server" Width="30px" MaxLength="4" /><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator18" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplier_ext"
                                                 Display="Dynamic" ValidationExpression="^\d{2,4}$"></asp:RegularExpressionValidator>
                                            <br />例:86-101-66666666-234
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow">移动电话:</td>
                                        <td class="oddrow-l"><asp:TextBox ID="txtCellPhone" runat="server" MaxLength="50" Width="50%"/></td>
                                        <td class="oddrow">Email:</td>
                                        <td class="oddrow-l"><asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="50%" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11"  runat="server" ControlToValidate="txtEmail"
                                                Display="None" ErrorMessage="供应商邮件格式错误" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />例:mail@sohu.com
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow">传真:</td>
                                        <td class="oddrow-l">
                                            <asp:TextBox ID="txtsupplierfax_con" runat="server" Width="30px" MaxLength="5"/><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator10" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_con"
                                                 Display="None" ValidationExpression="^\d{2,5}$"></asp:RegularExpressionValidator>-
                                            <asp:TextBox ID="txtsupplierfax_area" runat="server" Width="30px" MaxLength="4"/><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator14" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_area"
                                                 Display="None" ValidationExpression="^\d{2,4}$"></asp:RegularExpressionValidator>-
                                            <asp:TextBox Width="60px" ID="txtsupplierfax_phone" runat="server" MaxLength="8"/>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_phone"  Display="None" ValidationExpression="^\d{6,8}$">
                                            </asp:RegularExpressionValidator>-
                                            <asp:TextBox ID="txtsupplierfax_ext" runat="server" Width="30px" MaxLength="4"/>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ErrorMessage=" ! " ControlToValidate="txtsupplierfax_ext"  Display="None" ValidationExpression="^\d{2,4}$">
                                            </asp:RegularExpressionValidator><br />例:86-010-85078888-0001
                                        </td>
                                        <td class="oddrow">地址:</td>
                                        <td class="oddrow-l"><asp:TextBox ID="txtAddress" runat="server" MaxLength="200" Width="50%" /><font color="red"> * </font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress"
                                                 Display="None" ErrorMessage="请填写地址"></asp:RequiredFieldValidator></td>
                                    </tr>
                                </table>
                    <%--</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                        padding-top: 4px;">--%>
                            </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel3" HeaderText="产品服务信息" runat="server">
                    <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server">
                                <table width="100%" class="tableForm">
                                    <tr>
                                        <td class="heading" colspan="2">
                                            产品服务信息
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 20%">主要服务内容:</td>
                                        <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtservice_content" 
                                                runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 20%"> 案例:</td>
                                        <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtservice_forshunya" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 20%">服务覆盖区域:</td>
                                        <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtservice_area" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 20%">可同时承接的工作量:</td>
                                        <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtservice_workamount" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 20%">定制服务:</td>
                                        <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtservice_customization" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                                    </tr>
                                    <tr>
                                        <td class="oddrow" style="width: 20%">其他:</td>
                                        <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtservice_ohter" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                                    </tr>
                                </table>
                    <%--</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                        padding-top: 4px;">--%>
                        
                        </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel4" HeaderText="商务条款" runat="server">
                    <ContentTemplate>
                            <asp:Panel ID="Panel3" runat="server">
                        <table width="100%" class="tableForm">
                            <tr>
                                <td class="heading" colspan="4">
                                    商务条款
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">参考报价:</td>
                                <td class="oddrow-l" style="width: 80%" colspan="3"><asp:FileUpload ID="filbusiness_price" runat="server" Width="44%" />&nbsp;<asp:Label ID="labdowPrice" runat="server" />&nbsp;<asp:CheckBox ID="chkPrice" Visible="false" runat="server" Text="<img src='/images/ico_05.gif' border='0' />" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">账期:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtbusiness_paytime" runat="server" MaxLength="50" Width="50%"/></td>
                                <td class="oddrow" style="width: 20%">预付款:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtbusiness_prepay" runat="server" MaxLength="50" Width="50%"/></td>
                            </tr>
                        </table>
                </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel5" HeaderText="评估信息" runat="server">
                    <ContentTemplate>
                            <asp:Panel ID="Panel4" runat="server">
                    <%--</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                        padding-top: 4px;">--%>
                        <table width="100%" class="tableForm">
                            <tr>
                                <td class="heading" colspan="4">
                                    评估信息
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">采购部评价:</td>
                                <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtevaluation_department" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">推荐等级:</td>
                                <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtevaluation_level" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">业务反馈:</td>
                                <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtevaluation_feedback" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">备注:</td>
                                <td class="oddrow-l" style="width: 80%"><asp:TextBox ID="txtevaluation_note" runat="server" TextMode="MultiLine" Height="50px" Width="82%"/></td>
                            </tr>
                        </table>
                    <%--</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                        padding-top: 4px;">--%>
                        
                            </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel6" HeaderText="帐户信息" runat="server">
                    <ContentTemplate>
                            <asp:Panel ID="Panel5" runat="server">
                        <table width="100%" class="tableForm">
                            <tr>
                                <td class="heading" colspan="4">
                                    帐户信息
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">开户公司名称:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtaccount_name" runat="server" MaxLength="100" Width="50%"/></td>
                                <td class="oddrow" style="width: 20%">开户银行:</td>
                                <td class="oddrow-l" style="width: 30%"><asp:TextBox ID="txtaccount_bank" runat="server" MaxLength="100" Width="50%"/></td>
                            </tr>
                            <tr>
                                <td class="oddrow">帐号:</td>
                                <td class="oddrow-l"><asp:TextBox ID="txtaccount_number" runat="server" MaxLength="100" Width="50%"/></td>
                                <td class="oddrow">&nbsp;</td>
                                <td class="oddrow-l">&nbsp;</td>
                            </tr>
                        </table><%--
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                        padding-top: 4px;">--%>
                        
                            </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel7" HeaderText="产品图片" runat="server">
                    <ContentTemplate>
                            <asp:Panel ID="Panel6" runat="server">
                        <table width="100%" class="tableForm">
                            <tr>
                                <td class="heading" colspan="2">
                                    产品图片
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width: 20%">添加图片:</td>
                                <td class="oddrow-l" style="width: 80%">
                                    <asp:FileUpload ID="imageupload" Width="44%" runat="server"/>                            
                                </td>   
                                                     
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DataList ID="dlimage" runat="server" RepeatDirection="Horizontal" RepeatColumns="10">
                                    <ItemTemplate>
                                    <img src="<%# Eval("imageurl").ToString() %>" border="0px;" onclick="show('<%# Eval("imageurl").ToString() %>')"><br/>    
                                       <span style="text-align: center;"><%# Eval("imagename").ToString() %></span>                  
                                    </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                        <div id="divshow" style="display:none;"><div style="table-layout: fixed;word-wrap: break-word;width: 300px;height:300px;overflow: hidden; "><img id="img1" src="" border="0px;" style="max-height:300px"  alt=""></div></div>
                        
                            </asp:Panel>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click"
                     />&nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 "
                        CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
                    ShowMessageBox="true" DisplayMode="bulletList" />
            </td>
        </tr>
        <tr>
            <td style="height:10px">&nbsp;</td>
        </tr>
    </table>
        <asp:Panel ID="pnlPro" runat="server">
        <table width="100%" class="tableForm">
            <tr>
                <td class="heading" colspan="2">
                    目录采购物品信息
                </td>
            </tr>
        </table>
        <li>
            <asp:LinkButton ID="lnkaddP" runat="server" OnClick="lnkaddP_Click" CausesValidation="false">添加新目录物品</asp:LinkButton>
        </li>
        <br />
        <br />
        <table width="100%" id="tabTop" runat="server">
            <tr>
                <td width="50%">
                    <asp:Panel ID="PageTop" runat="server">
                        <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                        <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                        <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                        <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                    </asp:Panel>
                </td>
                <td align="right" class="recordTd">
                    记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT"
                        runat="server" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvProduct_RowDeleting"
            DataKeyNames="id" PageSize="20" AllowPaging="True" Width="100%" OnPageIndexChanging="gvProduct_PageIndexChanging"
            EmptyDataText="请添加采购目录物品">
            <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                <HeaderTemplate> 
                    <input name="chkAll" id="chkAll" onclick="SelectAll();" type="checkbox" />全选
                </HeaderTemplate> 
                <ItemStyle HorizontalAlign="Center"/> 
                <ItemTemplate> 
                    <input name="chkItem" id="chkItem" type="checkbox" value='<%# Eval("id").ToString() %>'/>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="productName" HeaderText="物品名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="productDes" HeaderText="描述" ItemStyle-HorizontalAlign="Center"/>
                <asp:TemplateField HeaderText="参考价格" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# decimal.Parse(Eval("productprice").ToString()).ToString("#,##0.####")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="productUnit" HeaderText="单位" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="suppliername" HeaderText="供应商名称" ItemStyle-HorizontalAlign="Center"/>
                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="ProductView.aspx?backUrl=ProposedSupplierEdit.aspx&productId=<%#Eval("id") %>&sid=<%#Eval("supplierId") %>">
                            <img src="../../images/dc.gif" border="0px;"></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <a href='ProductEdit.aspx?backUrl=ProposedSupplierEdit.aspx&productId=<%#DataBinder.Eval(Container.DataItem,"id")%>&sid=<%#Eval("supplierId") %>'>
                            <img src="../../images/edit.gif" border="0px;"></a>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('您确定删除吗？');"
                                    Text="<img src='/images/disable.gif' border='0' />" CommandName="Delete" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
            </Columns>
            <PagerSettings Visible="false" />
        </asp:GridView>
        <table width="100%" id="tabBottom" runat="server">
            <tr>
                <td width="50%">
                    <asp:Panel ID="PageBottom" runat="server">
                        <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                        <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                        <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                        <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                    </asp:Panel>
                </td>
                <td align="right" class="recordTd">
                    记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount"
                        runat="server" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td><asp:Button ID="btnDis" runat="server" Text="批量屏蔽" CausesValidation="false" CssClass="widebuttons" OnClientClick="return selectedCheck();" OnClick="btnDis_Click" />&nbsp;<asp:Button ID="btnDel" runat="server" CausesValidation="false" Text="批量删除" CssClass="widebuttons" OnClientClick="return selectedCheck();" OnClick="btnDel_Click" /></td>
            </tr>
        </table>
        <script language="javascript">
            function selectedCheck() {
                var checkedCount = 0;
                for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                    var e = document.getElementsByName("chkItem")[i];
                    if (e.checked) {
                        checkedCount++;
                    }
                }
                if (checkedCount == 0) {
                    alert('请选择要屏蔽或删除的目录物品！');
                    return false;
                }
                if (confirm('您确定要屏蔽或删除选择的目录物品吗？')) {
                    return true;
                }
                return false;
            }
        </script>
    </asp:Panel>
</asp:Content>
