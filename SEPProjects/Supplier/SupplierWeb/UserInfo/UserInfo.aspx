<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="SupplierWeb.UserInfo.UserInfo"  MasterPageFile="~/MainPage.Master"%>
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="uc1" %>
    <asp:ScriptManager ID="manager1" runat="server"></asp:ScriptManager>
    我的信息
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
            <uc1:TabPanel HeaderText="基本信息" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td width="30%">用户名：</td>
                            <td><asp:Label ID="labLogName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">全称：</td>
                            <td><asp:Label ID="labSupplierName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">所在地区：</td>
                            <td><asp:Label ID="labSupplierArea" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">所处行业：</td>
                            <td><asp:Label ID="labSupplierIndustry" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">规模：</td>
                            <td><asp:Label ID="labSupplierScale" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">注册金额：</td>
                            <td><asp:Label ID="labSupplierPrincipal" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">成立时间：</td>
                            <td><asp:Label ID="labSupplierBuilttime" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">网站：</td>
                            <td><asp:Label ID="labSupplierWebsite" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </uc1:TabPanel>
            
            <uc1:TabPanel ID="TabPanel1" HeaderText="联系信息" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td width="30%">电话：</td>
                            <td><asp:Label ID="labSupplierTel" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">传真：</td>
                            <td><asp:Label ID="labSupplierFax" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">手机：</td>
                            <td><asp:Label ID="labSupplierMobile" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">Email：</td>
                            <td><asp:Label ID="labSupplierEmail" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">地址：</td>
                            <td><asp:Label ID="labSupplierAdress" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td width="30%">邮编：</td>
                            <td><asp:Label ID="labSupplierZIP" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </uc1:TabPanel>
            
            <uc1:TabPanel ID="TabPanel2" HeaderText="公司简介" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td><asp:Label ID="labSupplierIntro" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </uc1:TabPanel>
            <uc1:TabPanel ID="TabPanel3" HeaderText="联系人信息" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Repeater ID="listLinkMan" runat="server" OnItemDataBound="listLinkMan_ItemDataBound">
                                    <ItemTemplate>
                                        <table class="itemlist">
                                            <tr>
                                                <td>姓名：<asp:Label ID="labName" runat="server" /></td>
                                                <td>性别：<asp:Label ID="labSex" runat="server" /></td>
                                                <td>职务：<asp:Label ID="labTitle" runat="server" /></td>
                                                <td rowspan="6" style="vertical-align:middle;width:120px" align="right">
                                                    <asp:Image ID="imgIcon" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>座机：<asp:Label ID="labTel" runat="server" /></td>
                                                <td>手机：<asp:Label ID="labMobile" runat="server" /></td>
                                                <td>传真：<asp:Label ID="labFax" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">地址：<asp:Label ID="labAddress" runat="server" /></td>
                                                <td>邮编：<asp:Label ID="labZIP" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">Email：<asp:Label ID="labEmail" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>QQ：<asp:Label ID="labQQ" runat="server" /></td>
                                                <td colspan="2">MSN：<asp:Label ID="labMSN" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">备注：<asp:Label ID="labNote" runat="server" /></td>
                                            </tr>
                                        </table>
                                        <asp:HyperLink ID="hypUpdateLinkMan" runat="server" Text="修改联系人"></asp:HyperLink>
                                        <br />
                                        <br />
                                        
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div id="search">
                                <asp:Label ID="labNA" runat="server" Visible="false" Text="没有查询到任何相关记录"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </uc1:TabPanel>
            <uc1:TabPanel ID="TabPanel4" HeaderText="物料类别" runat="server">
                <ContentTemplate>
                    <div id="divPayment" runat="server" width="100%">
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
                                                 Width="100%" ItemStyle-VerticalAlign="Top"
                                                RepeatColumns="4" RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <table width="100%" style="border: 0 0 0; margin: 0 0 0 0">
                                                        <tr>
                                                            <td style=" font:small-caption">
                                                                <asp:Label ID="lblName" Style="width: 80px" runat="server" Text='<%# Eval("TypeName") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DataList ID="ListLevel3" ItemStyle-BorderStyle="None" runat="server" Width="100%"
                                                                    ItemStyle-VerticalAlign="Top" RepeatColumns="1" RepeatDirection="Horizontal" >
                                                                    <ItemTemplate>
                                                                        <table width="100%" style="border: 0 0 0; margin: 0 0 0 0">
                                                                            <tr>
                                                                                <td>
                                                                                    <%# Eval("TypeName") %>
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
                    </div>
                </ContentTemplate>
            </uc1:TabPanel>
    </uc1:TabContainer>
    <asp:Button ID="btnUpdate" runat="server" Text="修改" onclick="btnUpdate_Click" />
    <asp:Button ID="btnUpdatePassword" runat="server" Text="修改密码" onclick="btnUpdatePassword_Click" />
    <asp:Button ID="btnUpdateIntro" runat="server" Text="修改简介" 
        onclick="btnUpdateIntro_Click" />
        <asp:Button ID="btnAddLinkMan" runat="server" Text="添加联系人" 
        onclick="btnAddLinkMan_Click" />
        <asp:Button ID="btnAddType" runat="server" Text="修改物料项" 
        onclick="btnAddType_Click" />
</asp:Content>
