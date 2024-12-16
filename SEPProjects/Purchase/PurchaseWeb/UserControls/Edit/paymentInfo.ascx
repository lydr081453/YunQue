<%@ Import Namespace="ESP.Purchase.Common"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Edit_paymentInfo" Codebehind="paymentInfo.ascx.cs" %>

<script src="../../public/js/jquery-1.2.6.js" type="text/javascript"></script>
<script language="javascript">
    function openEdit(paymentId) {
        var win = window.open('EditPayment.aspx?<%=RequestName.GeneralID %>=<%=Request[RequestName.GeneralID] %>&paymentId='+paymentId, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

    }
    
</script>
<asp:LinkButton ID="btnBind" runat="server" OnClick="btnBind_Click" />
<table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="8">
            ⑤ 付款条件信息
        </td>
    </tr>
    <asp:Panel ID="Edit" runat="server" Visible="false">
        <tr>
            <td class="oddrow" style="width: 10%">
                账期类型:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:DropDownList ID="drpPeriodType" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="drpPeriodType_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td style="width: 10%" class="oddrow">
                账期基准点:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:DropDownList ID="drpPeriodDatumPoint" runat="server" Width="200px"></asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 10%">
                账期:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:TextBox ID="txtPeriodDay" runat="server" Width="100px" Text="0" MaxLength="6"></asp:TextBox>天
            </td>
            <td style="width: 10%" class="oddrow">
                日期类型:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:DropDownList ID="drpDateType" runat="server" Width="200px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%" class="oddrow">
                预计支付时间:
            </td>
            <td style="width: 40%" class="oddrow-l" colspan="3">
                <asp:TextBox runat="server" onfocus="javascript:this.blur();" ID="txtBegin"></asp:TextBox><Font color="red">*</Font>
                <img src="../../images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('<%=txtBegin.ClientID %>'), document.getElementById('<%=txtBegin.ClientID %>'), 'yyyy-mm-dd','setEndDate()');" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtBegin" Display="None" ErrorMessage="请选择预计支付起始时间"></asp:RequiredFieldValidator>
           </td>
            <td style="width: 10%" class="oddrow">
                预计支付百分比:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:TextBox ID="txtExpectPaymentPercent" runat="server" Width="100px" Text="100" ></asp:TextBox>%<Font color="red">*</Font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtExpectPaymentPercent" Display="None" 
                    ErrorMessage="请填写预计支付百分比"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtExpectPaymentPercent" Display="None" 
                    ErrorMessage="前填写正确百分比" ValidationExpression="^((\d{1,2}(\.\d{1,2})?)|(100(\.0{1,2})?))$"></asp:RegularExpressionValidator>
            &nbsp;</td>
            <td class="oddrow" style="width: 10%">
                预计支付金额:
            </td>
            <td style="width: 15%" class="oddrow-l">
                <asp:TextBox ID="txtExpectPaymentPrice" runat="server"  Width="100px" ></asp:TextBox>元
            </td>
            <input type="hidden" value="0" id="hidTotalPrice" runat="server" />
            <input type="hidden" value="0" id="hitTotalPercent" runat="server" />
            <input type="hidden" value="0" id="hidCurrentPercent" runat="server" />
            <input type="hidden"  id="hidPaymentPeriodId" runat="server" />
        </tr>
        <tr>
            <td class="oddrow" style="width: 10%">
                备注:
            </td>
            <td style="width: 90%" class="oddrow-l" colspan="7">
                <asp:TextBox ID="txtPeriodRemark" runat="server" Width="90%" MaxLength="1000"></asp:TextBox>
            </td>
        </tr>
    </asp:Panel>
    <tr runat="server" id="TrGridView">
        <td colspan="8" class="oddrow-l">
            <asp:GridView ID="gvPayment" runat="server" Width="100%" DataKeyNames="id" AutoGenerateColumns="false" OnRowDeleting="gdvItem_RowDeleting" OnRowEditing="gdvItem_RowEditing" OnRowDataBound="gvPayment_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="序号"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Literal ID="litNo" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="账期类型"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_PeriodType[int.Parse(Eval("periodType").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="账期基准点"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_PeriodDatumPoint[int.Parse(Eval("periodDatumPoint").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="账期"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("periodDay")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="日期类型"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# State.Period_DateType[int.Parse(Eval("dateType").ToString())]%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="预计支付时间"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# DateTime.Parse(Eval("beginDate").ToString()).ToString("yyyy-MM-dd")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="预计支付金额"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# GetExpectPaymentPrice(decimal.Parse(Eval("expectPaymentPrice").ToString()))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="预计支付百分比"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("expectPaymentPercent") + "%"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="剩余金额"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Literal ID="LitOverplusPrice" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="备注" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Eval("periodRemark")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编辑"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="<img src='/images/edit.gif' border='0' />"   CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" OnClientClick="return confirm('您确定删除吗？');"
                                Text="<img src='/images/disable.gif' border='0' />" CommandName="Delete" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr runat="server" id="TrBtnAdd" >
        <td colspan="8" class="oddrow-l" align="left" >
            <div align="right">
            <%--<asp:Button ID="btnShow" runat="server" Text="添加" CssClass="widebuttons"  OnClick="btnShow_Click" OnClientClick="if(Page_ClientValidate()){return checkPercent();} else{return false;}" />--%>
            <asp:Button ID="btnShow" runat="server" Text=" 添加付款条件 " CausesValidation="false" CssClass="widebuttons" OnClientClick="openEdit('');return false;" />
            <asp:Button ID="btnNotShow" runat="server" Text="返回" CssClass="widebuttons" CausesValidation="false"  OnClick="btnNotShow_Click" Visible="false"  />
            </div>
        </td>
    </tr>
</table>
