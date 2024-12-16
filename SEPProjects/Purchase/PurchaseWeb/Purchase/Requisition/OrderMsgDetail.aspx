<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeBehind="OrderMsgDetail.aspx.cs" Inherits="OrderMsgDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>
    <script type="text/javascript">
//        function validtext() {
//            var msg = "";
//            if (msg != "") {
//                alert(msg);
//                return false;
//            }
//            else
//                return true;
//        }

//        function testNum(a) {
//            a += "";
//            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
//            if (a != "" && !isNaN(a) && Number(a) > 0) {
//                return true;
//            }
//            else {
//                return false;
//            }
//        }

//        function loadpage() {
//            parent.$('#floatBoxBg').hide(); 
//            parent.$('#floatBox').hide();
        //        }
        function vnameClick(vinfo) {
            ShowMsg(vinfo);
        }
        function nameClick(spid, Url) {
            if (Url == 1) {
                dialog("供应商资质信息", "iframe:/supplierchain/Question/AuditedPersonQuestionView.aspx?backUrl=floatBox&sid=" + spid, "1000px", "650px", "text");
            }
            if (Url == 2) {
                dialog("供应商信息", "iframe:/supplierchain/ManageInfo/PersonDetailInfoView.aspx?backUrl=floatBox&sid=" + spid, "700px", "500px", "text");
            }
            if (Url == 3) {
                dialog("供应商资质信息", "iframe:/supplierchain/Question/AuditedSupplierQuestionView.aspx?backUrl=floatBox&sid=" + spid, "1000px", "650px", "text");
            }
            if (Url == 4) {
                dialog("供应商信息", "iframe:/supplierchain/ManageInfo/SupplierDetailInfoView.aspx?backUrl=floatBox&sid=" + spid, "700px", "500px", "text");
            }
            //            alert(keys);
        }
    </script>
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
    <div style=" overflow-y:scroll ; overflow-x:hidden ;width:auto;height:400px;">
    <asp:Label ID="lblShowError" runat="server"></asp:Label>
        <asp:DataList ID="rp1" runat="server" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px"  RepeatDirection="Vertical" Width="100%"
         OnItemDataBound="rp1_ItemDataBound">
            <HeaderTemplate>
            <table width="98%" border="0">
<%--               <tr  class="gridView">
                    <td width="70%" class="heading">选择询价回复附件列表</td>
               </tr>--%>
            </HeaderTemplate>
            <ItemTemplate>
                <tr runat="server" id="trNewslist" >
                    <td>
                        <table width="100%" class="tableForm">
                            <tr><td class="heading" style="font-size:12px;" colspan="2">已选的新闻</td></tr>
                            <tr>
                                <td class="oddrow" style="font-size:12px; width:120px;">标题：</td>
                                <td class="oddrow-l"  style="font-size:12px;">
                                    <asp:HiddenField ID="hmsgid" runat="server"  Value='<%# Eval("ID") %>' />
                                    <asp:Label ID="lblMsgTitle" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="font-size:12px;width:120px;">发布人/发布日期：</td>
                                <td class="oddrow-l"   style="font-size:12px;"><asp:Label ID ="lblDate" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="oddrow" style="font-size:12px;width:120px;">内容：</td>
                                <td class="oddrow-l"   style="font-size:12px;"><asp:Label ID="lblMsgBody" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>  
                <!-- 已选的回复 -->
                <tr><td><br /></td></tr>
                <tr><td class="heading" style="font-size:12px;">已选的回复</td></tr>
                <tr>
                    <td>
                        <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="false" HeaderStyle-Height="0" Font-Size="12px">
                            <Columns>
                                <asp:TemplateField HeaderText="回复人" ItemStyle-HorizontalAlign="Center"   ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hrID" runat="server"  Value='<%# Eval("ID") %>' />
                                            <asp:Label ID="lblUser" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedDate"  ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ShowHeader="false" HeaderText="提交时间"  />
                                <%--<asp:BoundField DataField="FileUrl" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" ShowHeader="false" HeaderText="附件"  />--%>
                                <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblFileUrl" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="回复内容" ItemStyle-HorizontalAlign="Left"   ControlStyle-Width="300px" ItemStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblbody" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="新闻有效期" ItemStyle-HorizontalAlign="center"   ControlStyle-Width="110px" Visible=false>
                                        <ItemTemplate>
                                            <asp:Label ID="lbmsgEndDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>   
                <!-- 未选的回复 -->
                <tr><td><br /></td></tr>
                <tr><td class="heading" style="font-size:12px;">未选的回复</td></tr>
                <tr>
                    <td>
                        <asp:GridView ID="gv2" runat="server" AutoGenerateColumns="false" HeaderStyle-Height="0"  Font-Size="12px">
                            <Columns>
                                <asp:TemplateField HeaderText="回复人" ItemStyle-HorizontalAlign="Center"   ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hrID" runat="server"  Value='<%# Eval("ID") %>' />
                                            <asp:Label ID="lblUser" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedDate"  ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" ShowHeader="false" HeaderText="提交时间"  />
                               <%-- <asp:BoundField DataField="FileUrl" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" ShowHeader="false" HeaderText="附件"  />--%>
                               <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblFileUrl" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="回复内容" ItemStyle-HorizontalAlign="Left"   ControlStyle-Width="300px" ItemStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Literal ID="lblbody" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <ItemStyle Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="新闻有效期" ItemStyle-HorizontalAlign="center"   ControlStyle-Width="110px" Visible=false>
                                        <ItemTemplate>
                                            <asp:Label ID="lbmsgEndDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr> 
                <tr><td><br /></td></tr>
                <tr><td><hr /></td></tr> 
                <tr><td><br /></td></tr>  
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:DataList>
    </div>
</asp:Content>
