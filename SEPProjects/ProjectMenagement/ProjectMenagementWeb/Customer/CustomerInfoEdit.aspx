<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Customer_CustomerInfoEdit" MasterPageFile="~/MasterPage.master" Codebehind="CustomerInfoEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" language="javascript" src="/public/js/dialog.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script language="javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function AreaClick() {
            var win = window.open('/Dialogs/AreaDlg.aspx?clientid=ctl00_ContentPlaceHolder1_', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function IndustryClick() {
            var win = window.open('/Dialogs/IndustryDlg.aspx?clientid=ctl00_ContentPlaceHolder1_', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function CompanyClick() {
            var win = window.open('/Dialogs/BranchDlg.aspx?type=customer&clientid=ctl00_ContentPlaceHolder1_', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function CustomerFileClick() {
            var win = window.open('/Dialogs/CustomerFiles.aspx?<% =ESP.Finance.Utility.RequestName.CustomerID %>=<%=Request[ESP.Finance.Utility.RequestName.CustomerID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function selectFrame(attachId) {
            var cid = '<%=Request[ESP.Finance.Utility.RequestName.CustomerID] %>';
            var win = window.open('/Dialogs/FrameDlg.aspx?<% =ESP.Finance.Utility.RequestName.CustomerID %>=' + cid+'&attachId='+attachId, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            客户信息编辑
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            客户代码(系统生成):
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtCustomerCode" runat="server" MaxLength="100"
                                Width="200px" />
                        </td>
                        <td class="oddrow" style="width: 15%">
                            地址代码:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:Label runat="server" ID="txtAddressCode"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            客户缩写:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtShortEN" runat="server" MaxLength="100" Width="200px" />&nbsp;<font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="rfvShortEN" runat="server" ControlToValidate="txtShortEN"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="客户缩写为必填项"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[A-Z]{4}" ControlToValidate="txtShortEN" Display="Dynamic" ErrorMessage="格式为4位大写字母"></asp:RegularExpressionValidator>
                        </td>
                        <td class="oddrow">预估返点比例：</td>
                        <td class="oddrow-l"><asp:TextBox ID="txtRebateRate" runat="server" Width="200px" />%<asp:CompareValidator ID="CompareValidator1" runat="server" Type="Double" Operator="GreaterThanEqual" ValueToCompare="0" ControlToValidate="txtRebateRate"
                            ErrorMessage="预估返点比例错误"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            客户名称:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtNameCN1" runat="server" MaxLength="100" Width="200px" />&nbsp;<font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="rfvNameCN1" runat="server" ControlToValidate="txtNameCN1"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="客户名称为必填项"></asp:RequiredFieldValidator>
                        </td>
                        <td class="oddrow" style="width: 15%">
                            发票抬头:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtInvoiceTitle" runat="server" MaxLength="100" Width="200px" />&nbsp;<font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInvoiceTitle"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="发票抬头为必填项"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            地址:
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtAddress1" runat="server" MaxLength="100" Width="200px" />&nbsp;<font
                                color="red">*</font>
                            <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1"
                                ValidationGroup="Save" Display="Dynamic" ErrorMessage="地址为必填项"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                                                <td class="oddrow">
                            网址
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtContactWebsite" runat="server" MaxLength="100" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            初次申请公司:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtAppCompany" runat="server" Width="200px" onkeyDown="return false;" Style="cursor: hand" />
                            <asp:Button ID="btnAppCompany" runat="server" OnClientClick="return CompanyClick();"
                                Text="搜索" />
                            <asp:HiddenField ID="hidCompanyID" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 15%">
                            初次申请时间:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtAppDate" runat="server" Width="200px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            联系信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            联系人姓名:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtContactName" runat="server" MaxLength="50" Width="200px" />
                        </td>
                        <td class="oddrow">
                            A0:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtA0" runat="server" MaxLength="50" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            固话:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtContactTel" runat="server" Width="200px" MaxLength="50" />
                        </td>
                        <td class="oddrow" style="width: 15%">
                            传真:
                        </td>
                        <td class="oddrow-l" style="width: 35%">
                            <asp:TextBox ID="txtContactFax" runat="server" Width="200px" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>

                        <td class="oddrow">
                            Email:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtContactEmail" runat="server" MaxLength="50" Width="200px" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="Save"
                                runat="server" ControlToValidate="txtContactEmail" Display="Dynamic" ErrorMessage="供应商邮件格式错误"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                            例:mail@sohu.com
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            所在地区:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtArea" runat="server" MaxLength="100" Width="200px" onkeyDown="return false;" Style="cursor: hand"/>
                            <input type="button" id="btnAreaSelect" onclick="return AreaClick();" class="widebuttons"
                                value="搜索" />
                            <asp:HiddenField ID="hidAreaID" runat="server" />
                            <asp:HiddenField ID="hidAreaCode" runat="server" />
                        </td>
                        <td class="oddrow">
                            所在行业:
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtIndustry" runat="server" MaxLength="100" Width="200px" onkeyDown="return false;" Style="cursor: hand" />
                            <input type="button" id="btnIndustrySelect" class="widebuttons" onclick="return IndustryClick();"
                                value="搜索" />
                            <asp:HiddenField ID="hidIndustryID" runat="server" />
                            <asp:HiddenField ID="hidIndustryCode" runat="server" />
                        </td>
                    </tr>
                    <tr id="trFrame" runat="server">
                        <td class="oddrow" id="tdFrame">
                            框架协议&nbsp;&nbsp;
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <img align="absbottom" src="/images/btn_0011.jpg" onclick="return selectFrame(0);"
                                style="cursor: hand" alt="添加框架协议" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            协议列表
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="AttachID"
                                OnRowDataBound="gvG_RowDataBound" OnRowCommand="gvG_RowCommand" EmptyDataText="暂时没有相关记录" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="AttachID" HeaderText="AttachID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FrameContractTitle" HeaderText="协议描述" ItemStyle-HorizontalAlign="Center"
                                         />
                                    <asp:BoundField DataField="FrameBeginDate" HeaderText="起始日期" ItemStyle-HorizontalAlign="Center"
                                        />
                                    <asp:BoundField DataField="FrameEndDate" HeaderText="结束日期" ItemStyle-HorizontalAlign="Center"
                                         />
                                    <asp:BoundField DataField="FrameContractCode" HeaderText="协议号" ItemStyle-HorizontalAlign="Center"
                                         />
                                    <asp:TemplateField HeaderText="备注" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span title="<%# Eval("Description") %>"><%# Eval("Description").ToString().Length<=100 ? Eval("Description").ToString() : Eval("Description").ToString().Substring(0,100)+"..." %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="附件" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="labDown" runat="server" Text='' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a onclick='return selectFrame(<%# Eval("AttachID")%>);'><img src="../images/edit.gif" border="0px;" title="编辑"></a>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("AttachID") %>' CausesValidation="false"
                                                CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>"
                                                OnClientClick="return confirm('你确定删除吗？');" />
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
    <table style="width: 100%">
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click"
                    ValidationGroup="Save" />&nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 "
                        CausesValidation="false" CssClass="widebuttons" OnClick="btnBack_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
                    ShowMessageBox="true" DisplayMode="bulletList" />
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
