<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Customer_CustomerView" Codebehind="CustomerView.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
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
    </script>
    <%--<asp:ScriptManager ID="manager1" runat="server">
    </asp:ScriptManager>
    <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
        <cc1:TabPanel ID="TabPanel1" HeaderText="基础信息" runat="server">
            <ContentTemplate>
                <table class="tableForm" width="100%">
                    <tr>
                        <td class="oddrow" style="width:20%">客户中文全称:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labNameCN" runat="server" /></td>
                        <td class="oddrow" style="width:20%">客户英文全称:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labNameEN" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">客户中文简称:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labShortCN" runat="server" /></td>
                        <td class="oddrow" style="width:20%">客户英文简称:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labShortEN" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">所在地区:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labArea" runat="server" /></td>
                        <td class="oddrow" style="width:20%">所在行业:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labIndustry" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">规模:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labScale" runat="server" /></td>
                        <td class="oddrow" style="width:20%">注册资本:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labPrincipal" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">成立年限:</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labBuilttime" runat="server" /></td>
                        <td class="oddrow">&nbsp;</td>
                        <td class="oddrow-l">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">备注:</td>
                        <td class="oddrow-l" style="width:80%" colspan="3"><asp:Label ID="labNote" runat="server" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" HeaderText="联系信息" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td class="oddrow" style="width:20%">联系人:</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labContactName" runat="server" /></td>
                                <td class="oddrow" style="width:20%">职务:</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labContactTitil" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width:20%">固话:</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labContactTel" runat="server" /></td>
                               <td class="oddrow">传真:</td>
                               <td class="oddrow-l"><asp:Label ID="labContactFax" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">移动电话:</td>
                                <td class="oddrow-l"><asp:Label ID="labContactMobile" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="oddrow">网址:</td>
                                <td class="oddrow-l"><asp:HyperLink ID="hypContactWebsite" runat="server"></asp:HyperLink></td>
                                <td class="oddrow">Email:</td>
                                <td class="oddrow-l"><asp:Label ID="labContactEmail" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">地址:</td>
                                <td class="oddrow-l"><asp:Label ID="labContactAddress" runat="server" /></td>
                                <td class="oddrow">邮编:</td>
                                <td class="oddrow-l"><asp:Label ID="labContactZIP" runat="server" /></td>
                            </tr>
                         </table>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel6" HeaderText="帐户信息" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel5" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td class="oddrow" style="width:20%">开户公司名称:</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labAccountName" runat="server" /></td>
                                <td class="oddrow" style="width:20%">开户银行:</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labAccountBank" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">帐号:</td>
                                <td class="oddrow-l"><asp:Label ID="labAccountNumber" runat="server" /></td>
                                <td class="oddrow">发票抬头:</td>
                                <td class="oddrow-l"><asp:Label ID="labAccountTitle" runat="server" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel7" HeaderText="图片" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel6" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td  style="width:100%">
                                <asp:DataList ID="dlimage" runat="server" RepeatDirection="Horizontal" RepeatColumns="10">
                            <ItemTemplate>
                            <img src="<%# Eval("imageurl").ToString() %>" border="0px;" onclick="show('<%# Eval("imageurl").ToString() %>')"><br/>    
                               <span style="text-align: center;"><%# Eval("imagename").ToString() %></span>                  
                            </ItemTemplate>
                            </asp:DataList>
                                </td>                                
                            </tr>
                            
                        </table>
                        <div id="divshow" style="display:none;"><div style="table-layout: fixed;word-wrap: break-word;width: 300px;height:300px;overflow: hidden; "><img id="img1" src="" border="0px;" style="max-height="300px"  alt=""></div></div>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>--%>