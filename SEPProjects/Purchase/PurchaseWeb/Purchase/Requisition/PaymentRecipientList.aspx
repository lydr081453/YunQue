<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_PaymentRecipientList" CodeBehind="PaymentRecipientList.aspx.cs" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="PurchaseWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Src="../../UserControls/View/PRTopMessage.ascx" TagName="PRTopMessage"
        TagPrefix="uc1" %>

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript">
        function SelectAll() {
            for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                var e = document.getElementsByName("chkItem")[i];

                e.checked = document.forms[0].chkAll.checked;
            }
        }
        function SelectAll1() {
            for (var i = 0; i < document.getElementsByName("chkPeriod").length; i++) {
                var e = document.getElementsByName("chkPeriod")[i];

                e.checked = document.forms[0].chkAll1.checked;
            }
        }
        function exportexcels() {
            var strid = "";
            for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                var e = document.getElementsByName("chkItem")[i];
                if (e.checked == true) {
                    strid += (e.value + ",");
                }
            }
            if (strid == "") {
                alert("请选择您要导出的数据！");
                return false;
            }

            var win = window.open('SearchPaymentApply.aspx?recid=' + strid + '', null, 'height=10px,width=10px,scrollbars=no,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function printGet() {
            var chks = document.getElementsByName("chkItem");
            var ids = "";
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked == true) {
                    ids += chks[i].value + ",";
                }
            }
            if (ids == "") {
                alert("请选择您要打印的数据！");
                return false;
            }
            ids = ids.substring(0, ids.length - 1);
            window.open("Print/MultiRecipientPrint.aspx?newPrint=true&id=" + ids);
            return false;
        }
        function openPayment() {

            var generalid = '<%= Request[RequestName.GeneralID]%>';
            var chks = document.getElementsByName("chkItem");
            var recipientIds = "";
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked == true) {
                    recipientIds += chks[i].value + ",";
                }
            }
            if (recipientIds == "") {
                alert("请选择收货单！");
                return false;
            }

            recipientIds = recipientIds.substring(0, recipientIds.length - 1);
            var win = window.open("PaymentPeriodList.aspx?<%=RequestName.GeneralID%>=" + generalid + "&recipientIds=" + recipientIds, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
            return false;
        }

        function openPaymentPreview(ppid) {
            var win = window.open("Print/PaymentPreview.aspx?ppid=" + ppid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
            return false;
        }
    </script>
    <uc1:PRTopMessage ID="PRTopMessage" runat="server" IsEditPage="true" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                <asp:Label ID="labPrno" runat="server" />-付款申请明细
            </td>
        </tr>
    </table>
    <table border="1" style="color: red; width: 100%" cellspacing="5px" cellpadding="5px">
        <tr>
            <td>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="6">帐户信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 10%">开户公司名称:
                        </td>
                        <td class="oddrow-l" style="width: 23%">
                            <asp:TextBox ID="txtaccountName" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">开户银行:
                        </td>
                        <td class="oddrow-l" style="width: 23%">
                            <asp:TextBox ID="txtaccountBank" runat="server" />
                        </td>
                        <td class="oddrow" style="width: 10%">帐号:
                        </td>
                        <td class="oddrow-l" style="width: 23%">
                            <asp:TextBox ID="txtaccountNum" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="oddrow-l">
                            <asp:Button ID="btnUpate" runat="server" Text="更新帐户信息" CssClass="widebuttons" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">支付条款
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l">
                            <asp:GridView ID="gvPayment" runat="server" Width="100%" DataKeyNames="id" AutoGenerateColumns="false"
                                OnRowDataBound="gvPayment_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="选择">
                                        <ItemTemplate>
                                            <input type="radio" id="radPeriod" name="radPeriod" value='<%# Eval("id") %>#<%# Eval("periodType") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="账期类型" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# State.Period_PeriodType[int.Parse(Eval("periodType").ToString())]%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="账期基准点" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# State.Period_PeriodDatumPoint[int.Parse(Eval("periodDatumPoint").ToString())]%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="账期" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("periodDay")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="日期类型" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# State.Period_DateType[int.Parse(Eval("dateType").ToString())]%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="预计支付时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="预计支付金额" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# GetExpectPaymentPrice(decimal.Parse(Eval("expectPaymentPrice").ToString()))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="预计支付百分比" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Eval("expectPaymentPercent") + "%"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("periodRemark")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td align="right" colspan="2">
                            <input type="button" id="exportexcel" value=" 导出 " class="widebuttons" onclick="return exportexcels();" />
                            <input type="button" id="btnPrintGet" value="批量打印收货单" class="widebuttons" onclick="return printGet();" />
                            <%--<input type="button" id="btnPayment" value="付款账期" class="widebuttons" onclick="return openPayment();" />--%>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading">收货信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow-l">
                            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="recipientId" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                        <HeaderTemplate>
                                            <input name="chkAll" id="chkAll" onclick="SelectAll();" type="checkbox" />全选
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <input name="chkItem" id="chkItem" type="checkbox" value='<%# Eval("recipientId").ToString() %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="流水号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# int.Parse(Eval("GID").ToString()).ToString("0000000") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="8%" />
                                    <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="8%" />
                                    <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="11%" />
                                    <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="11%">
                                        <ItemTemplate>
                                            <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="supplier_name" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <%--                <table width="100%">
                    <tr>
                        <td class="heading" colspan="2">
                            付款信息
                        </td>
                    </tr>
                    <tr>
                        <td>
                        付款金额计算方式：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="radCalPaymentType" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                        </td>
                    </tr>
                </table>--%>
                <table>
                    <tr>
                        <td align="right" colspan="2" style="height: 20px;" vertical-align="bottom">
                            <prc:CheckPRInputButton type="button" value=" 创建付款申请 " ID="btnCommit" runat="server" class="widebuttons"
                                onclick="if(!check()){return false;}" OnServerClick="btnCommit_Click" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">待提交付款申请
            </td>
        </tr>
        <%--        <asp:Panel ID="Edit" runat="server" Visible="false">
            <tr>
                <td style="width: 15%" class="oddrow">
                    预计支付时间:
                </td>
                <td style="width: 35%" class="oddrow-l">
                    <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="txtBegin"></asp:TextBox><font
                        color="red">*</font>
                    <img src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=txtBegin.ClientID %>'), document.getElementById('<%=txtBegin.ClientID %>'), 'yyyy-mm-dd');" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBegin"
                        Display="None" ErrorMessage="请选择预计支付起始时间"></asp:RequiredFieldValidator>
                    -
                    <asp:TextBox runat="server" ID="txtEnd" onfocus="javascript:this.blur();"></asp:TextBox>&nbsp;<img
                        src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=txtEnd.ClientID %>'), document.getElementById('<%=txtEnd.ClientID %>'), 'yyyy-mm-dd');" />
                </td>
                <td class="oddrow" style="width: 15%">
                    申请付款金额:
                </td>
                <td style="width: 35%" class="oddrow-l">
                    <asp:TextBox ID="txtInceptPrice" runat="server" Width="100px"></asp:TextBox>元
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInceptPrice"
                        Display="None" ErrorMessage="请填写申请付款金额"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtInceptPrice"
                        ErrorMessage="请输入正确申请付款金额" Display="None" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator>
                </td>
                <input type="hidden" id="hidPaymentPeriodId" runat="server" />
                <input type="hidden" id="hidInceptPrice" runat="server" />
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnPeriodSave" runat="server" CssClass="widebuttons" OnClick="btnPeriodSave_Click"
                        Text="保存" OnClientClick="if(Page_ClientValidate()){return checkPrice();} else{return false;}" />&nbsp;
                    <input type="button" id="btnPeriodCommit" runat="server" class="widebuttons" onserverclick="btnPeriodCommit_Click"
                        value="提交" onclick="if(Page_ClientValidate()){if(checkPrice()){if(!confirm('您确定要支付吗？')){ return false;}}else {return false;}} else{return false;}" />&nbsp;
                    <asp:Button ID="btnPeriodCancel" runat="server" CssClass="widebuttons" OnClick="btnPeriodCancel_Click"
                        Text="取消" CausesValidation="false" />
                </td>
            </tr>
        </asp:Panel>--%>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:GridView ID="gvPaymentPeriod" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gvPaymentPeriod_RowCancelingEdit"
                    OnRowDataBound="gvPaymentPeriod_RowDataBound" OnRowUpdating="gvPaymentPeriod_RowUpdating"
                    DataKeyNames="id" Width="100%" OnRowCommand="gvPaymentPeriod_RowCommand" OnRowEditing="gvPaymentPeriod_RowEditing">
                    <Columns>
 <%--                       <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                            <HeaderTemplate>
                                <input name="chkAll1" id="chkAll1" onclick="SelectAll1();" type="checkbox" />全选
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# GetSubmitUrl(int.Parse(Eval("id").ToString()), int.Parse(Eval("status").ToString()))%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# int.Parse(Eval("Gid").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("prno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"  />        --%>
                        <asp:TemplateField HeaderText="预计支付时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="txtBegin" Text='<%#DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd") %>' /><font
                                    color="red">*</font>
                                <img src="../../images/dynCalendar.gif" runat="server" id="img1" border="0" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBegin"
                                    Display="None" ErrorMessage="请选择预计支付起始时间"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请付款金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# GetFormatPrice(decimal.Parse(Eval("inceptPrice").ToString()))%>
                            </ItemTemplate>
                        </asp:TemplateField>
<%--                        <asp:TemplateField HeaderText="打印" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                            <ItemTemplate>
                                <a href="" onclick="return openPaymentPreview('<%# Eval("id") %>');">
                                    <img src="../../images/pri_pn.gif" border="0px;" title="打印"></a>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href='/Purchase/Requisition/ReturnEdit.aspx?ReturnID=<%# Eval("returnId") %>'><img src='/images/edit.gif' title='编辑' border='0px' /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="撤销" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkDel" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Del"
                                    Text="<img src='../../images/Icon_Cancel.gif' title='撤销' border='0'>" OnClientClick="return confirm('你确定撤销吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                        <asp:TemplateField HeaderText="提交" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkSubmit" runat="server" CommandArgument='<%# Eval("id") %>'
                                    CommandName="Submit" Text="<img src='../../images/edit.gif' title='提交' border='0'>"
                                    OnClientClick="return confirm('你确定要提交吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
<%--    <asp:HiddenField ID="hidPeriodIds" runat="server" />
    <input type="button" value=" 提交 " id="btnSubmit" runat="server" class="widebuttons"
        onclick="if (!submitCheck()) { return false; } else { if (!confirm('你确定要提交吗？')) return false; }"
        onserverclick="btnSubmit_Click" />--%>
    <asp:HiddenField ID="hidRecipientIds" runat="server" />
    <br />
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">付款申请历史
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLogList" runat="server" CssClass="gridView" AllowPaging="false"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="流水号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# int.Parse(Eval("Gid").ToString()).ToString("0000000") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("prno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="预计支付时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="txtBegin" Text='<%#DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd") %>' /><font
                                    color="red">*</font>
                                <img src="../../images/dynCalendar.gif" runat="server" id="img1" border="0" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBegin"
                                    Display="None" ErrorMessage="请选择预计支付起始时间"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请付款金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# GetFormatPrice(decimal.Parse(Eval("inceptPrice").ToString()))%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInceptPrice" runat="server" Width="100px" Text='<%# GetFormatPrice(decimal.Parse(Eval("inceptPrice").ToString()))%>'></asp:TextBox><font
                                    color="red">*</font>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInceptPrice"
                                    Display="None" ErrorMessage="请填写申请付款金额"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtInceptPrice"
                                    ErrorMessage="请输入正确申请付款金额" Display="None" ValidationExpression="^(\d{1,3},?)+(\.\d+)?$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%" border="1" class="XTable">
        <tr>
            <td align="center">
                <asp:Button ID="btnBack" runat="server" CssClass="widebuttons" Text=" 返回 " PostBackUrl="~/Purchase/Requisition/PaymentGeneralList.aspx" />
            </td>
        </tr>
    </table>

    <script language="javascript">



        function check() {
            var errorsMsg = "";
            var chks = document.getElementsByName("chkItem");
            var recipientIds = "";
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked == true) {
                    recipientIds += chks[i].value + ",";
                }
            }

            var radios = document.getElementsByName("radPeriod");
            var radioValues = "";
            for (var i = 0; i < radios.length; i++) {
                if (radios[i].checked == true) {
                    radioValues = radios[i].value;
                    break;
                }
            }

            if (radioValues == "") {
                errorsMsg += "- 请选择支付条款！\r\n";
            }

            if (radioValues.split("#")[1] == "0") {
                if (recipientIds == "") {
                    //alert("请选择收货单！");
                    errorsMsg += "- 请选择收货单！";
                    //return false;
                }
            }
            if (errorsMsg != "") {
                alert(errorsMsg);
                return false;
            }
            recipientIds = recipientIds.substring(0, recipientIds.length - 1);
            document.getElementById("<%=hidRecipientIds.ClientID%>").value = recipientIds;

            var rads = document.getElementsByName("radPeriod");
            for (var i = 0; i < rads.length; i++) {
                if (rads[i].checked == true) {
                    return true;
                }
            }
            return false;
        }
    </script>

    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"
        Display="None"></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
