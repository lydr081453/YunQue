<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_CustomerInfo"
    CodeBehind="CustomerInfo.ascx.cs" %>

<script type="text/javascript">
    function setDate(obj) {
        popUpCalendar(obj, obj, 'yyyy-mm-dd');
    }
</script>

<script type="text/javascript" src="/public/js/DatePicker.js"></script>

<script language="javascript">
    $().ready(function() {
        var operate = '<%=Request[ESP.Finance.Utility.RequestName.Operate] %>';
        if (operate == "update") {
            document.getElementById("btnCustomerSelect").style.display = "none";
            document.getElementById("btnHistory").style.display = "none";
        }
    });
    function CustomerClick() {
        var s = '<%= hidContractStatusID.Value == ESP.Finance.Configuration.ConfigurationManager.FCAStatus ? "fc":"" %>';
        var win = window.open('/Dialogs/CustomerDlg.aspx?s=fc&<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function HistoryClick() {
        var dept = '<%= GetDept %>';
        var cid = document.getElementById("<%=hidCustomerID.ClientID %>").value;

        var win = window.open('/Dialogs/CustomerHistoryDlg.aspx?<% =ESP.Finance.Utility.RequestName.DeptID %>=' + dept + '&<% =ESP.Finance.Utility.RequestName.CustomerID %>=' + cid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function AreaClick() {
        var win = window.open('/Dialogs/AreaDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function IndustryClick() {
        var win = window.open('/Dialogs/IndustryDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function showCustomer() {
        var cid = document.getElementById("<%=hidCustomerTmpID.ClientID %>").value;
        var win = window.open('/Customer/CustomerDiffer.aspx?<% =ESP.Finance.Utility.RequestName.CustomerID %>=' + cid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function selectFrame() {
        var cid = document.getElementById("<%=hidCustomerID.ClientID %>").value;
        if (cid == '') {
            alert("请先搜索客户信息");
            return;
        }
        var win = window.open('/Dialogs/FrameDlg.aspx?s=fc&<% =ESP.Finance.Utility.RequestName.CustomerID %>=' + cid + '&pid=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function bindFrame() {
        document.getElementById("<%= btnBindFrame.ClientID %>").click();
    }
    function chkFrame(obj, args) {
        args.IsValid = $("input[type='checkbox'][name='radAttach']:checked").length > 0
    }
    function setHid(obj) {
        var ids = "";
        $("input[type='checkbox'][name='radAttach']:checked").each(function () {
            if (ids == "")
                ids = ",";
            ids += $(this).val() + ",";
        });
        document.getElementById("<%= hidCustomerAttachID.ClientID %>").value = ids;
    }
</script>

<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ② 客户信息<a name="top_A" /><asp:HiddenField ID="hidRebateRate" Value="0" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            英文简称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtShortEN" runat="server" MaxLength="4" /><input type="hidden"
                id="hidCustomerTmpID" runat="server" /><input type="hidden" id="hidCustomerID" runat="server" /><input
                    type="hidden" id="hidCustomerCode" runat="server" />
                    <input type="hidden" id="hidShortEN" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShortEN"
                ErrorMessage="英文简称为必填" Display="None"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ValidationExpression="^[A-Z]{4}" ControlToValidate="txtShortEN" ErrorMessage="英文简称格式为4位大写字母"></asp:RegularExpressionValidator>
                <font color="red">*</font><input
                    type="button" id="btnCustomerSelect" onclick="return CustomerClick();" class="widebuttons"
                    value="搜索" />
            <input type="button" id="btnHistory" onclick="return HistoryClick();" class="widebuttons"
                value="查看历史" />
                <asp:LinkButton ID="linkCustomerBind" runat="server"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户名称:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtCN1" runat="server" Width="80%" MaxLength="35" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCN1" Display="None"
                 ErrorMessage="客户名称为必填"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
<%--        <td class="oddrow" style="width: 15%">
            中文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtCN2" runat="server" Width="80%" MaxLength="35" />
        </td>
    </tr>
    <tr>
       <td class="oddrow" style="width: 15%">
            中文简称：
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtShortCN" runat="server" Width="80%" MaxLength="35" />
        </td>--%>
        <td class="oddrow">
            发票抬头:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtTitle" runat="server" Width="80%" MaxLength="50" /><asp:RequiredFieldValidator
                ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle" Display="None"
                ErrorMessage="发票抬头为必填"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
    </tr>
<%--    <tr>
        <td class="oddrow" style="width: 15%">
            英文名称1:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtEN1" runat="server" Width="80%" MaxLength="35" /><asp:RequiredFieldValidator
                ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEN1" Display="None"
                ErrorMessage="英文名称为必填"></asp:RequiredFieldValidator><font color="red">*</font>
        </td>
        <td class="oddrow" style="width: 15%">
            英文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtEN2" runat="server" Width="80%" MaxLength="35" />
        </td>
    </tr>--%>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAddress1" runat="server" Width="40%" MaxLength="35" />
        </td>
    </tr>
<%--    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址2:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAddress2" runat="server" Width="40%" MaxLength="35" />
        </td>
    </tr>--%>
    <tr>
<%--        <td class="oddrow" style="width: 15%">
            公司邮编:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtPostCode" runat="server" Width="80%" MaxLength="30" />
        </td>--%>
        <td class="oddrow" style="width: 15%">
            公司网址:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtWebSite" runat="server" Width="40%" MaxLength="50" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            所在地区:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtArea" runat="server" onkeyDown="return false; " Style="cursor: hand">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtArea"
                Display="None" ErrorMessage="所在地区为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <input type="button" id="btnAreaSelect" onclick="return AreaClick();" value="搜索"
                class="widebuttons" />
            <input type="hidden" id="hidAreaID" runat="server" />
            <input type="hidden" id="hidAreaCode" runat="server" />
        </td>
        <td class="oddrow" style="width: 15%">
            所在行业:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtIndustry" runat="server" onkeyDown="return false; " Style="cursor: hand"> </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIndustry"
                Display="None" ErrorMessage="所在行业为必填"></asp:RequiredFieldValidator><font color="red">*</font>
            <input type="button" id="btnIndustrySelect" onclick="return IndustryClick();" value="搜索"
                class="widebuttons" />
            <input type="hidden" id="hidIndustryID" runat="server" />
            <input type="hidden" id="hidIndustryCode" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            联系人信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户联系人:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtContact" runat="server" MaxLength="15" /><font color="red">*</font>&nbsp;<asp:RequiredFieldValidator
                ControlToValidate="txtContact" ID="rfvContact" runat="server" Display="None" ErrorMessage="客户联系人姓名为必填"></asp:RequiredFieldValidator>
        </td>
        <td class="oddrow" style="width: 15%">
            联系人职务:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtContactPosition" runat="server" MaxLength="15" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            联系人电话:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtContactMobile" runat="server" MaxLength="15" /><font color="red">*</font>&nbsp;<asp:RequiredFieldValidator
                ControlToValidate="txtContactMobile" ID="rfvContactMobile"  Display="None" runat="server" ErrorMessage="客户联系人电话为必填"></asp:RequiredFieldValidator>
        </td>
        <td class="oddrow" style="width: 15%">
            联系人传真:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtContactFax" runat="server" MaxLength="15" />
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            联系人Email:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtContactEmail" runat="server" Width="40%" MaxLength="50" /><font
                color="red">*</font>&nbsp;<asp:RequiredFieldValidator Display="None" ControlToValidate="txtContactEmail"
                    ID="rfvContactEmail" runat="server" ErrorMessage="客户联系人邮箱为必填"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <asp:HiddenField ID="hidContractStatusID" runat="server" />
    <asp:HiddenField ID="hidCustomerAttachID" runat="server" />
    <asp:Button ID="btnBindFrame" runat="server" OnClick="btnBindFrame_Click" CausesValidation="false" style="display:none;" />
    <tr runat="server" id="TrAttach" visible="false">
        <td colspan="4">
            <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">框架协议信息&nbsp;<input style="<%= hidContractStatusID.Value == ESP.Finance.Configuration.ConfigurationManager.FCAStatus ? "" : "display:none;"%>" type="button" class="widebuttons" id="btnAttach" value="添加" onclick="return selectFrame();" /></td>
                    </tr>
                    <tr>
                        <td class="oddrow-l">
                            <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="AttachID" OnRowCommand="gvG_RowCommand"
                                OnRowDataBound="gvG_RowDataBound" EmptyDataText="暂时没有相关记录" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="选择" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <input type="checkbox" onclick="setHid(this);" name="radAttach" <%# hidCustomerAttachID.Value.Contains("," +Eval("AttachID").ToString()+",") ? "checked" : "" %> value="<%# Eval("AttachID") %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FrameContractTitle" HeaderText="协议描述" ItemStyle-HorizontalAlign="Center"
                                         />
                                    <asp:BoundField DataField="FrameBeginDate" HeaderText="起始日期" ItemStyle-HorizontalAlign="Center"
                                         />
                                    <asp:BoundField DataField="FrameEndDate" HeaderText="结束日期" ItemStyle-HorizontalAlign="Center"
                                        />
                                    <asp:TemplateField HeaderText="备注" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <span title="<%# Eval("Description") %>"><%# Eval("Description").ToString().Length<=100 ? Eval("Description").ToString() : Eval("Description").ToString().Substring(0,100)+"..." %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="附件" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="labDown" runat="server" Text='' />
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
                            <asp:CustomValidator ID="CustomValidator1" Enabled="false" ClientValidationFunction="chkFrame" Display="None" runat="server" EnableClientScript="true" ErrorMessage="请选择客户框架协议"></asp:CustomValidator>
                        </td>
                    </tr>
            </table>
        </td>
    </tr>
</table>
<div id="divCustomer" runat="server">
    <table width="100%">
        <tr>
            <td class="oddrow-1" colspan="4">
                <a href="#" onclick="return showCustomer();">
                    <img src="/images/differ.jpg" alt="客户信息变更" width="30px" height="25px" /><font color="red">客户信息有变动，请点这里查看比对数据</font></a>
            </td>
        </tr>
    </table>
</div>
