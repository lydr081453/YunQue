<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_ProjectInfo"
    CodeBehind="ProjectInfo.ascx.cs" %>

<script type="text/javascript" src="/public/js/DatePicker.js"></script>

<script type="text/javascript">
    function setDate(obj) {
        popUpCalendar(obj, obj, 'yyyy-mm-dd');
    }
    function BranchClick() {
        var win = window.open('/Dialogs/BranchDlg.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function retClick() {
        document.getElementById("<%=btnRet.ClientID%>").click();
    }

    function CreateDynamic() {
        var date2 = document.getElementById("<% =txtBeginDate.ClientID %>").value;
        var date1 = document.getElementById("<% =txtEndDate.ClientID %>").value;
        if (date1 <= date2)
            return -1;
        var year = parseFloat(date1.substr(0, 4)) - parseFloat(date2.substr(0, 4));
        var month = parseFloat(date1.substr(5, 2)) - parseFloat(date2.substr(5, 2)) + parseFloat("1");
        var differ = 0;

        differ = year * 12 + month;
        return differ;

    }
    function MediaClick() {
        var win = window.open('/Dialogs/searchSupplier.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function setMedia(id, name, rate) {
        $("#<% =hidMediaId.ClientID %>").val(id);
        $("#<% =txtMediaName.ClientID %>").val(name); 

        if (rate == 100) {
            $("#<% =txtSupplierCostRate.ClientID %>").val(rate);
        }
        mediaMath();
    }

    function ContractsClick() {
        var win = window.open('/Dialogs/ContractListDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function testNum(a) {
        a = a.replace(/,/g, '').replace(/(^[\\s]*)|([\\s]*$)/g, "");
        if (a != "" && !isNaN(a) && Number(a) >= 0) {
            return true;
        }
        else {
            return false;
        }
    }

    function selectPercent() {
        var monthAmount = CreateDynamic();
        var date2 = document.getElementById("<% =txtBeginDate.ClientID %>").value;
        var year = date2.substr(0, 4);
        var month = date2.substr(5, 2);
        var msg = "";
        if (monthAmount == "undefined" || monthAmount == "" || monthAmount <= 0) {
            msg += "请输入正确的项目起始日期和结束日期." + "\n";
        }
        if (msg != "") {
            alert(msg);
            return false;
        }
        monthAmount = monthAmount + 2;
        var win = window.open('/Dialogs/PercentForProjectDlg.aspx?<% =ESP.Finance.Utility.RequestName.ProjectID %>=<%=Request[ESP.Finance.Utility.RequestName.ProjectID] %>&<% =ESP.Finance.Utility.RequestName.Percent %>=' + monthAmount + '&<% =ESP.Finance.Utility.RequestName.BeginYear %>=' + year + '&<% =ESP.Finance.Utility.RequestName.BeginMonth %>=' + month, null, 'height=600px,width=800px,scrollbars=yes,top=400px,left=400px');
        win.resizeTo(screen.availWidth * 0.4, screen.availHeight * 0.4);
    }

    function showContract(contractid, usable) {
        if (usable == "False") {
            alert("该合同已经不可用，无法编辑!");
            return false;
        }
        var win = window.open('/Dialogs/ContractListDlg.aspx?ProjectID=<%=Request["ProjectID"] %>&ContractID=' + contractid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function formatNum(v) {
        return v.replace(/,/g, '').replace(/(^[\\s]*)|([\\s]*$)/g, "");
    }

    function customerMath() {
        var total = formatNum($("#<% =txtTotalAmount.ClientID %>").val());
        var rate = formatNum($("#<% =txtCustomerRebateRate.ClientID %>").val());
        if (!testNum(total) || !testNum(rate))
            return;
        $("#<% =labAccountsReceivable.ClientID %>").val(parseFloat(total * (1 - rate / 100)).toFixed(2));
        $("#<% =labCustomerRebateRatePrice.ClientID %>").val(parseFloat(total * rate / 100).toFixed(2));
    }

    function mediaMath() {
        var total = formatNum($("#<% =txtRechargeAmount.ClientID %>").val());
        var rate = formatNum($("#<% =txtSupplierCostRate.ClientID %>").val());
        if (!testNum(total) || !testNum(rate))
            return;
        $("#<% =labSupplierCost.ClientID %>").val(parseFloat(total * rate / 100).toFixed(2));
        }
</script>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%" class="tableForm" style="margin-bottom: 0px;">
            <tr>
                <td class="heading" colspan="4">③ 合同信息<a name="top_A" />
                </td>
            </tr>

            <tr>
                <td class="oddrow" style="width: 15%;">公司选择:</td>
                <td class="oddrow-l" colspan="3">
                    <asp:TextBox runat="server" onfocus="this.blur();" Style="cursor: hand;" ID="txtBranchName"
                        Width="30%" />&nbsp;<input type="button" id="btnBranchSelect" class="widebuttons" onclick="return BranchClick();"
                            class="widebuttons" value="搜索" /><font color="red"> *</font><asp:RequiredFieldValidator
                                ID="rfvBranch" runat="server" ControlToValidate="txtBranchName" Display="None" ErrorMessage="公司为必填项"></asp:RequiredFieldValidator>
                    <asp:LinkButton runat='server' ID="btnRet" OnClick="btnRet_Click" />
                    <asp:HiddenField ID="hidBranchID" runat="server" />
                    <asp:HiddenField ID="hidBranchCode" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%;">项目总金额：</td>
                <td class="oddrow-l" style="width: 35%;">
                    <asp:TextBox ID="txtTotalAmount" Text="0.00" onchange="customerMath();" runat="server" MaxLength="12"></asp:TextBox><font color="red"> *</font><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTotalAmount" Display="None" ErrorMessage="项目总金额为必填项"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" Type="Currency" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtTotalAmount" runat="server" ErrorMessage="项目总金额错误"></asp:CompareValidator></td>
                <td class="oddrow" style="width: 15%;">合同税率（%）:
                </td>
                <td class="oddrow-l">
                    <asp:DropDownList ID="ddlTaxRate" runat="server" OnSelectedIndexChanged="ddlTaxRate_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>

            <%if (project.isRecharge)
              {%>
            <% if (gvMedia.Rows.Count <= 1)
               { %>
            <tr>
                <td class="oddrow" style="width: 15%;">媒体付款主体:
                </td>
                <td class="oddrow-l">
                    <asp:HiddenField ID="hidMediaId" runat="server" />
                    <asp:TextBox runat="server" onfocus="this.blur();" Style="cursor: hand;" ID="txtMediaName"
                        Width="74%" />&nbsp;<input type="button" id="Button1" class="widebuttons" onclick="return MediaClick();"
                            class="widebuttons" value="搜索" /><font color="red"> *</font><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMediaName" Display="None" ErrorMessage="媒体付款主体为必填项"></asp:RequiredFieldValidator></td>
                                <td class="oddrow" style="width: 15%;">充值金额：</td>
                <td class="oddrow-l" style="width: 35%;">
                    <asp:TextBox ID="txtRechargeAmount" Text="0" runat="server" onchange="mediaMath();" /><font color="red"> *</font><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRechargeAmount" Display="None" ErrorMessage="充值金额为必填项"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator2" Type="Currency" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtRechargeAmount" runat="server" ErrorMessage=" 充值金额错误"></asp:CompareValidator></td>
            </tr>
            <tr>
                                <td class="oddrow">预估媒体成本比例：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtSupplierCostRate" onfocus="this.blur();" Text="0" runat="server" onchange="mediaMath();" />%<font color="red"> *</font><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSupplierCostRate" Display="None" ErrorMessage="预估媒体成本比例为必填项"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator3" Type="Double" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtSupplierCostRate" runat="server" ErrorMessage=" 预估媒体成本比例错误"></asp:CompareValidator>
                </td>
                                <td class="oddrow">预估媒体成本：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="labSupplierCost" runat="server" SkinID="Label" /></td>
            </tr>
            <%}
               else
               { %>
                    <tr>
            <td colspan="4">
                <asp:GridView ID="gvMedia" runat="server" AllowPaging="false" Width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="媒体付款主体" DataField="MediaName" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="充值金额" DataField="Recharge" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="预估媒体成本比例" DataField="CostRate" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预估媒体成本" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# (decimal.Parse(Eval("Recharge").ToString()) * decimal.Parse(Eval("CostRate").ToString())).ToString("#,##0.00") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="起始时间" DataField="BeginDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField HeaderText="结束时间" DataField="EndDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    <%} %>
            <tr>
                                <td class="oddrow" style="width: 15%;">预估客户返点比例：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtCustomerRebateRate" Text="0"  onfocus="this.blur();" onchange="customerMath();" runat="server" />%<font color="red"> *</font><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCustomerRebateRate" Display="None" ErrorMessage="预估客户返点比例为必填项"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator4" Type="Double" ValueToCompare="0" Operator="GreaterThanEqual" ControlToValidate="txtCustomerRebateRate" runat="server" ErrorMessage=" 预估客户返点比例错误"></asp:CompareValidator>
                </td>
                <td class="oddrow">预估客户返点金额：</td>
                <td class="oddrow-l">
                    <asp:TextBox ID="labCustomerRebateRatePrice" runat="server" SkinID="Label" /></td>
            </tr>
            <tr>

                <td class="oddrow">应收账款：</td>
                <td class="oddrow-l" colspan="3">
                    <asp:TextBox ID="labAccountsReceivable" runat="server" SkinID="Label" /></td>
            </tr>
            <%} %>
            <tr>
                <td class="oddrow" style="width: 15%;">不含增值税金额:
                </td>
                <td class="oddrow-l" style="width: 35%;">
                    <asp:TextBox ID="lblTotalNoVAT" runat="server" SkinID="Label"></asp:TextBox>
                </td>
                <td class="oddrow" style="width: 15%;">附加税（主申请方）:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="lblTaxFee" runat="server" SkinID="Label"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">支持方合计:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="lblTotalSupporter" runat="server" SkinID="Label"></asp:TextBox>
                </td>
                <td class="oddrow">附加税（支持方）:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="lblTaxSupporter" runat="server" SkinID="Label"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">合同服务费:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="lblServiceFee" runat="server" SkinID="Label"></asp:TextBox>
                </td>
                <td class="oddrow">项目毛利率:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="lblProfileRate" runat="server" SkinID="Label"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow" style="width: 15%">业务起始日期:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server" />
                    <font color="red"> *</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtBeginDate" Display="None" ErrorMessage="业务起始日期必填"></asp:RequiredFieldValidator>
                </td>
                <td class="oddrow" style="width: 15%">业务结束日期:
                </td>
                <td class="oddrow-l" style="width: 35%">
                    <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                        onclick="setDate(this);" />
                    <font color="red"> *</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="txtEndDate" Display="None" ErrorMessage="业务结束日期必填"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="DateCompareValidator" runat="server" ControlToValidate="txtEndDate" ControlToCompare="txtBeginDate"
                        Type="Date" Operator="GreaterThanEqual"  ErrorMessage="业务结束日期必须大于业务起始日期" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table border="0" width="100%">
                        <tr>
                            <td class="heading">申请文件上传&nbsp;&nbsp;
                        <asp:Button ID="btnAddContracts" runat="server" OnClientClick="if(Page_ClientValidate()){ return ContractsClick();}else{return false;}" 
                            Text=" 保存合同信息并上传合同附件 " CssClass="widebuttons" OnClick="btnAddContracts_Click"></asp:Button>&nbsp;<font
                                color="red">*</font>
                                <asp:LinkButton runat="server" ID="btnContract" OnClick="btnContract_Click" />
                            </td>
                        </tr>
                        <tr id="trNoRecord" runat="server" visible="false">
                            <td>
                                <table class="gridView" cellspacing="0" border="0" style="background-color: White; width: 100%; border-collapse: collapse;">
                                    <tr class="Gheading" align="center">
                                        <th scope="col">序号
                                        </th>
                                        <th scope="col">合同描述
                                        </th>
                                        <%--                                <th scope="col">
                                    合同金额
                                </th>--%>
                                        <th scope="col">是否有效
                                        </th>
                                        <th scope="col">上一版合同
                                        </th>
                                        <th scope="col">附件
                                        </th>
                                    </tr>
                                    <tr class="td" align="left">
                                        <td colspan="5" align="center">
                                            <span>暂时没有上传的合同信息</span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trGrid" runat="server" visible="true">
                            <td>
                                <asp:GridView ID="gvContracts" Width="100%" runat="server" DataKeyNames="ContractID"
                                    AutoGenerateColumns="false" EmptyDataText="没有上传的合同信息" OnRowDataBound="gvContracts_RowDataBound"
                                    OnRowCommand="gvContracts_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="ContractID" HeaderText="ContractID" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usable" HeaderText="Usable" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40%" HeaderText="合同描述" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDes" runat="server" Text='<%# Eval("Description") %>' ToolTip='<%# Eval("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="40%"></ItemStyle>
                                        </asp:TemplateField>
                                        <%--                                <asp:TemplateField ItemStyle-Width="20%" HeaderText="合同金额" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Width="100px" Text='<%# Eval("TotalAmounts") %>'></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
                                </asp:TemplateField>--%>
                                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsable" runat="server" Text="是"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="上一版合同" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOldContract" runat="server" Text="无"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img src="/images/edit.gif" style="cursor: hand" onclick="return showContract('<%# Eval("ContractID") %>','<%# Eval("Usable") %>');" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ContractID") %>'
                                                    CommandName="Del" Text="<img src='/images/disable.gif' title='删除' border='0'>"
                                                    OnClientClick="return confirm('你确定删除吗？');" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="附件" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <a id="aDownLoad" target="_blank" href='/Dialogs/ContractFileDownLoad.aspx?ContractID=<%# Eval("ContractID") %>'>
                                                    <img src="/images/ico_04.gif" border="0" /></a>
                                                <asp:HiddenField ID="hidId" runat="server" Value='<%# Eval("ContractID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
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
</asp:UpdatePanel>
