<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_View_SupplierView" Codebehind="SupplierView.ascx.cs" %>
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
    <asp:ScriptManager ID="manager1" runat="server">
    </asp:ScriptManager>
    <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%">
        <cc1:TabPanel ID="TabPanel1" HeaderText="基础信息" runat="server">
            <ContentTemplate>
                <table class="tableForm" width="100%">
                    <tr>
                        <td class="oddrow" style="width:20%">供应商全称</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labSupplierName" runat="server" /></td>
                        <td class="oddrow" style="width:20%">所在地区</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labsupplier_area" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">行业</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labsupplier_industry" runat="server" /></td>
                        <td class="oddrow" style="width:20%">规模</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labsupplier_scale" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">注册资本</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labsupplier_principal" runat="server" /></td>
                        <td class="oddrow" style="width:20%">成立年限</td>
                        <td class="oddrow-l" style="width:30%"><asp:Label ID="labsupplier_builttime" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width:20%">网址</td>
                        <td class="oddrow-l" style="width:30%"><asp:HyperLink ID="hpysupplier_website" runat="server" Target="_blank"></asp:HyperLink> </td>
                        <td class="oddrow">供应商来源</td>
                        <td class="oddrow-l"><asp:Label ID="labsupplier_source" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow">协议框架号</td>
                        <td class="oddrow-l"><asp:Label ID="labfa_no" runat="server" /></td>
                        <td class="oddrow">&nbsp;</td>
                        <td class="oddrow-l">&nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel9" HeaderText="目录物品" runat="server">
            <ContentTemplate>
                                <table width="100%" id="tabTop" runat="server" class="XTable">
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
        </table><br />
                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="id" PageSize="20" AllowPaging="True" Width="100%" OnPageIndexChanging="gvProduct_PageIndexChanging"
                    EmptyDataText="请添加采购目录物品">
                    <Columns>
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
                                <a href="ProductView.aspx?productId=<%#Eval("id").ToString() + "&supplierId=" + Request["supplierId"] + "&backUrl=" + HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Length - 1 ] %>">
                                    <img src="../../images/dc.gif" border="0px;"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView><br />
                        <table width="100%" id="tabBottom" runat="server" class="XTable">
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
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" HeaderText="联系信息" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td class="oddrow" style="width:20%">联系人</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labLinker" runat="server" /></td>
                                <td class="oddrow" style="width:20%">固话</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labTelPhone" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">移动电话</td>
                                <td class="oddrow-l"><asp:Label ID="labCellPhone" runat="server" /></td>
                                <td class="oddrow">Email</td>
                                <td class="oddrow-l"><asp:Label ID="labEmail" runat="server" /></td>
                            </tr>
                            <tr>
                               <td class="oddrow">传真</td>
                               <td class="oddrow-l"><asp:Label ID="labFax" runat="server" /></td>
                                <td class="oddrow">地址</td>
                                <td class="oddrow-l"><asp:Label ID="labAddress" runat="server" /></td>
                            </tr>
                         </table>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" HeaderText="产品服务信息" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td class="oddrow" style="width:20%">主要服务内容：</td>
                                <td class="oddrow-l" style="width:80%"><asp:Label ID="labservice_content" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="width:20%">案例：</td>
                                <td class="oddrow-l" style="width:80%"><asp:Label ID="labservice_forshunya" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">服务覆盖区域：</td>
                                <td class="oddrow-l"><asp:Label ID="labservice_area" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">可同时承接的工作量：</td>
                                <td class="oddrow-l"><asp:Label ID="labservice_workamount" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">定制服务：</td>
                                <td class="oddrow-l"><asp:Label ID="labservice_customization" runat="server" /></td>
                            </tr>
                            <tr>
                               <td class="oddrow">其他：</td>
                               <td class="oddrow-l"><asp:Label ID="labservice_ohter" runat="server" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" HeaderText="商务条款" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel3" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td class="oddrow" style="width:20%">账期：</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labbusiness_paytime" runat="server" /></td>
                                <td class="oddrow" style="width:20%">预付款：</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labbusiness_prepay" runat="server" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel8" HeaderText="参考报价" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel7" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td class="oddrow" style="width:20%">参考报价：</td>
                                <td class="oddrow-l" style="width:80%"><asp:Label ID="labbbusiness_price" runat="server" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel5" HeaderText="评估信息" runat="server">
            <ContentTemplate>
                    <asp:Panel ID="Panel4" runat="server">
                        <table class="tableForm" width="100%">
                            <tr>
                                <td class="oddrow" style="width:20%">采购部评价：</td>
                                <td class="oddrow-l" style="width:80%"><asp:Label ID="labevaluation_department" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">推荐等级：</td>
                                <td class="oddrow-l"><asp:Label ID="labevaluation_level" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">业务反馈：</td>
                                <td class="oddrow-l"><asp:Label ID="labevaluation_feedback" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">备注：</td>
                                <td class="oddrow-l"><asp:Label ID="labevaluation_note" runat="server" /></td>
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
                                <td class="oddrow" style="width:20%">开户公司名称：</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labaccount_name" runat="server" /></td>
                                <td class="oddrow" style="width:20%">开户银行：</td>
                                <td class="oddrow-l" style="width:30%"><asp:Label ID="labaccount_bank" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="oddrow">帐号：</td>
                                <td class="oddrow-l"><asp:Label ID="labaccount_number" runat="server" /></td>
                                <td class="oddrow">&nbsp;</td>
                                <td class="oddrow-l">&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel7" HeaderText="产品图片" runat="server">
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
    </cc1:TabContainer>
                        