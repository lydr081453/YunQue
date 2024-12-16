<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="SupplierWeb.UserProfile.UserProfile"  MasterPageFile="~/MainPage.Master"%>
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
    我的资料
    <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
            
            <uc1:TabPanel ID="TabPanel5" HeaderText="我的新闻" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Repeater ID="rtNews" runat="server" OnItemDataBound="rtNews_ItemDataBound" >
                                    <ItemTemplate>
                                        <table class="itemlist">
                                            <tr>
                                                <td colspan="2">标题：<asp:Label ID="labTitle" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">内容：<asp:Label ID="labContent" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td >创建时间：<asp:Label ID="labCreatTime" runat="server" /></td>
                                                <td >最后修改时间：<asp:Label ID="labLastUpdateTime" runat="server" /></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                            <asp:HyperLink ID="hypUpdateNews" runat="server" Text="修改"></asp:HyperLink>
                                        
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div id="Div1">
                                <asp:Label ID="labNewShowing" runat="server" Visible="false" Text="没有查询到任何相关记录"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </uc1:TabPanel>
            
            <uc1:TabPanel ID="TabPanel3" HeaderText="我的图片" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Repeater ID="rtPhoto" runat="server" OnItemDataBound="rtPhoto_ItemDataBound" >
                                    <ItemTemplate>
                                        <table class="itemlist">
                                            <tr>
                                                <td><asp:Literal ID="htmlPhoto" runat="server"></asp:Literal></td>
                                            </tr>
                                            <tr>
                                                <td>描述：<asp:Label ID="labShowTxt" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td >创建时间：<asp:Label ID="labCreatTime" runat="server" /></td>
                                                <td >最后修改时间：<asp:Label ID="labLastUpdateTime" runat="server" /></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div id="Div2">
                                <asp:Label ID="labPhotoShowing" runat="server" Visible="false" Text="没有查询到任何相关记录"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </uc1:TabPanel>
            <uc1:TabPanel ID="TabPanel2" HeaderText="我的产品" runat="server">
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
            <uc1:TabPanel ID="TabPanel4" HeaderText="我的文件" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </uc1:TabPanel>
    </uc1:TabContainer>
    <asp:Button ID="btnAddNews" runat="server" Text="添加新闻" onclick="btnAddNews_Click" />
    <asp:Button ID="btnAddPhoto" runat="server" Text="添加图片" onclick="btnAddPhoto_Click" />
    <asp:Button ID="btnAddProduct" runat="server" Text="添加产品"  onclick="btnAddProduct_Click" />
    <asp:Button ID="btnAddFile" runat="server" Text="添加文件"  onclick="btnAddFile_Click" />
</asp:Content>
