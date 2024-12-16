<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="RecipientConfirm.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.RecipientConfirm" %>


<%@ Import Namespace="ESP.Purchase.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Src="../../UserControls/View/genericInfo.ascx" TagName="genericInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="../../UserControls/View/supplierInfo.ascx" TagName="supplierInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="../../UserControls/View/projectInfo.ascx" TagName="projectInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="../../UserControls/View/productInfo.ascx" TagName="productInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="/UserControls/View/paymentInfo.ascx" TagName="paymentInfo" TagPrefix="uc1" %>
    <%@ Register Src="/UserControls/View/RequirementDescInfo.ascx" TagName="RequirementDescInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="/UserControls/Edit/Score.ascx" TagName="Score" TagPrefix="uc1" %>

    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript" language="javascript">

        function showDetailInfo(id) {
            dialog("查看工作单明细", "iframe:ShunyaXiaomi/DetailView.aspx?id=" + id, "1000px", "500px", "text");
        }
    </script>

    <asp:HiddenField ID="hidSupplierEmail" runat="server" />
    <%--          项目号申请单信息        --%>
    <uc1:projectInfo ID="projectInfo" runat="server" IsEditPage="true" />
    <uc1:genericInfo ID="GenericInfo" runat="server" />
    <%--          项目号申请单信息        --%>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">③ 采购物品信息
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gdvItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Width="100%" DataKeyNames="ID,producttype" OnRowDataBound="gdvItem_RowDataBound"
                    Font-Size="14pt" SkinID="gridviewSkin" EmptyDataText="请添加采购物品">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="序号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="4%" />
                        <asp:TemplateField HeaderText="采购物品" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Eval("productAttribute").ToString() == ((int)State.productAttribute.ml).ToString() ? ("* " + Eval("Item_No").ToString()) : Eval("Item_No")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="物料类别" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="desctiprtion" HeaderText="描述" />
                        <asp:BoundField DataField="intend_receipt_date" HeaderText="预计收货时间"></asp:BoundField>
                        <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("price").ToString()).ToString("#,##0.####")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="uom" HeaderText="单位" HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="quantity" HeaderText="数量" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="6%" />
                        <asp:TemplateField HeaderText="小计" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("total").ToString()).ToString("#,##0.####")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="labDown" runat="server" Text='<%#Eval("upfile") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right" style="font-size: 12px" class="recordTd">共<asp:Label ID="DgRecordCount" runat="server"></asp:Label>条数据,总金额
                <asp:Label ID="labTotalPrice" runat="server" />
                元
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">备注
            </td>
            <td colspan="3" class="oddrow-l">
                <asp:Label ID="labNote" runat="server" Width="400px"></asp:Label>
        </tr>
    </table>
    <%-- *********供应商信息********* --%>
    <uc1:supplierInfo ID="supplierInfo" runat="server" />
    <%-- *********供应商信息********* --%>
    <%--       付款账期信息         --%>
    <uc1:paymentInfo ID="paymentInfo" runat="server" />
    <%--       付款账期信息         --%>
    <uc1:RequirementDescInfo ID="RequirementDescInfo" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">审核信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">订单编号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="txtorderid" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">谈判类型:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txttype" runat="server" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px">比价节约:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="txtcontrast" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownContrast" runat="server" />
            </td>
            <td class="oddrow" style="width: 100px">议价节约:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="txtconsult" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownConsult" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px">初审人:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="ddlfirst_assessor" runat="server" SkinID="userLabel" />
            </td>
            <td class="oddrow" style="width: 100px">事后申请:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="labafterwards" runat="server" /><br />
                <asp:Label ID="labafterwardsReason" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px;">紧急采购:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmBuy" runat="server" Width="200px"></asp:Label><br />
                <asp:Label ID="labEmBuyReason" runat="server" />
            </td>
            <td class="oddrow" style="width: 120px;">客户指定:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCusAsk" runat="server" Width="200px"></asp:Label><br />
                <asp:Label ID="labCusName" runat="server" Width="200px"></asp:Label><br />
                <asp:Label ID="labCusAskYesReason" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px;">合同号码:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labContractNo" runat="server" Width="200px" />
            </td>
            <td class="oddrow" style="width: 100px;">其它:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtothers" runat="server" Width="200px" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td style="width: 15%" class="oddrow">最后编辑时间:
            </td>
            <td style="width: 35%" class="oddrow-l">
                <asp:Label ID="lablasttime" runat="server" />
            </td>
            <td style="width: 20%" class="oddrow">申请单提交时间:
            </td>
            <td style="width: 30%" class="oddrow-l">
                <asp:Label ID="labrequisition_committime" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">订单生成时间:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="laborder_committime" runat="server" />
            </td>
            <td class="oddrow">订单审批时间:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="laborder_audittime" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="palFili" runat="server" Visible="false">
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="2" class="heading" style="height: 1px"></td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">分公司审核备注:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labFili" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="palOverrulP" runat="server" Visible="false">
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="2" class="heading" style="height: 1px"></td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">申请单审批备注:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labOverruleP" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="palOverrule" runat="server" Visible="false">
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="2" class="heading" style="height: 1px"></td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">订单审核备注:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labOverrule" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>

   <%-- <table width="100%" class="tableForm">
        <tr>
            <td colspan="2" class="heading">品推宝平台信息
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvPTB" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDataBound="gvPTB_RowDataBound" EmptyDataText="暂时没有品推宝平台数据" AllowPaging="false"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="XiaoMiNo" HeaderText="订单编号" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="OrderDesc" HeaderText="需求描述" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="卖家名称" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSaler" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Fee" HeaderText="实付金额" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="6%" />
                        <asp:BoundField DataField="ReleaseContent" HeaderText="发布内容" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Buyer" HeaderText="买家" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="订单状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblOrderStatus" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="代付状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPayByOther" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="需求单号">
                            <ItemTemplate>
                                <a target="_blank" href="ShunyaXiaomi/RequireDetail.aspx?xiaomiId=<%# Eval("id") %>"><%# Eval("RequireNo") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="详细信息">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDetail" OnClientClick='<%# "showDetailInfo(" + Eval("id") +");return false;" %>'
                                    runat="server" ImageUrl="~/images/dc.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>--%>


    <table width="100%" class="tableForm" border="0">
        <tr>
            <td colspan="2">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="2">历史收货信息
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="oddrow-l">
                            <asp:GridView ID="gvFP" runat="server" AllowPaging="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="RecipientName" HeaderText="收货人" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="收货时间" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# DateTime.Parse(Eval("RecipientDate").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.00")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="结算单" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <a target="_blank" href="/Purchase/Requisition/UpfileDownload.aspx?RecipientId=<%#Eval("Id").ToString() %>">
                                                <img src="/images/ico_04.gif" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="备注" DataField="Note" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="oddrow-l" style="height: 20px"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow" style="width: 15%">收货金额：
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtAmount" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">备注：
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtNote" runat="server" Width="50%" TextMode="MultiLine" Height="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">上传结算单：
                        </td>
                        <td class="oddrow-l">
                            <asp:HyperLink runat="server" ID="hpFile" Target="_blank"></asp:HyperLink><br />
                            <asp:FileUpload ID="fileupContract" runat="server" Width="60%" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnSave" Text=" 确认  "
                    type="button" CausesValidation="false" class="widebuttons" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text=" 返回 " CausesValidation="false" CssClass="widebuttons"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
