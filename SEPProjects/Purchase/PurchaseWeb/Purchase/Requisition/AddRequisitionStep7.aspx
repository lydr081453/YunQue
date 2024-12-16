<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_AddRequisitionStep7"
    Title="Untitled Page" Codebehind="AddRequisitionStep7.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="../../public/js/DatePicker.js" type="text/javascript"></script>

    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/Edit/paymentInfo.ascx" tagname="paymentInfo"
        tagprefix="uc1" %>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <li>
                    <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="RequisitionSaveList.aspx">返回申请单信息浏览</asp:HyperLink></li>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%--          项目号申请单信息        --%>
                <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
                <%--          项目号申请单信息        --%>

            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <uc1:genericInfo ID="GenericInfo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            ③ 采购物品信息
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gdvItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                Width="100%" DataKeyNames="ID,producttype" OnRowDataBound="gdvItem_RowDataBound"
                                Font-Size="14pt" SkinID="gridviewSkin" EmptyDataText="请添加采购物品">
                                <SelectedRowStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedRowStyle>
                                <AlternatingRowStyle HorizontalAlign="Left"></AlternatingRowStyle>
                                <RowStyle Wrap="False" HorizontalAlign="Left" Font-Size="16px"></RowStyle>
                                <HeaderStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center" Font-Size="12px">
                                </HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="序号" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("productAttribute").ToString() == ((int)State.productAttribute.ml).ToString() ? ("* " + Eval("Item_No").ToString()) : Eval("Item_No")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="desctiprtion" HeaderText="描述" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="intend_receipt_date" HeaderText="预计收货时间" ItemStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.####")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="uom" HeaderText="单位" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="8%" />
                                    <asp:BoundField DataField="quantity" HeaderText="数量" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.####")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="labDown" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="id" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right" style="font-size: 12px">
                            共<asp:Label ID="DgRecordCount" runat="server"></asp:Label>条数据,总金额
                            <asp:Label ID="labTotalPrice" runat="server" />
                            元
                        </td>
                    </tr>
                        <tr>
        <td class="oddrow" colspan="4">
            <table width="100%" id="FCTable" runat="server" visible="false" >
                <tr>
                    <td class="heading">供应商框架合同</td>
                </tr>
                <tr>
                    <td class="oddrow-l">
                        <asp:GridView runat="server" ID="GvFCPr" Width="100%" AllowPaging="false" EmptyDataText="暂时没有相关记录" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="PrNo" HeaderText="Pr单号" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="supplier_name" HeaderText="供应商名称" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="desctiprtion" HeaderText="框架合同描述" />
                                <asp:TemplateField HeaderText="附件" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a target='_blank' href="/Purchase/Requisition/UpfileDownload.aspx?OrderId=<%# Eval("orderId").ToString() %>&Index=0"><img src='/images/ico_04.gif' border='0' /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
                     <tr>
                        <td class="oddrow" style="width: 15%">
                            采购审批流向
                        </td>
                        <td colspan="3" class="oddrow-l">
                           <font color="red"><asp:Label ID="lblValueLevel" runat="server" Width="400px"></asp:Label></font>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 15%">
                            备注
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:Label ID="labNote" runat="server" Width="400px"></asp:Label>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <%-- *********供应商信息********* --%>
                <uc1:supplierInfo ID="supplierInfo" runat="server" />
                <%-- *********供应商信息********* --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;"><a name="top_A" />
                    <%--       付款账期信息         --%>
                    <uc1:paymentInfo ID="paymentInfo" runat="server" />
                    <%--       付款账期信息         --%>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnPre" Text="上一步" CssClass="widebuttons" OnClick="btnPre_Click"
                    runat="server" CausesValidation="false" />&nbsp;
                <prc:CheckPRInputButton runat="server" id="btnNext" value="下一步" type="button" 
                    causesvalidation="false" class="widebuttons" onserverclick="btnNext_Click" />&nbsp;
                <prc:CheckPRInputButton runat="server" id="btnSave" value=" 保存  " type="button"
                    causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="CustomValidator1" runat="server" 
        ErrorMessage="CustomValidator" Display="None"></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
