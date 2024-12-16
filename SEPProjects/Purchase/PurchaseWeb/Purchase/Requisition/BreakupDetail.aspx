<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Purchase_Requisition_BreakupDetail" CodeBehind="BreakupDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <%@ register src="../../UserControls/View/genericInfo.ascx" tagname="genericInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/supplierInfo.ascx" tagname="supplierInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/projectInfo.ascx" tagname="projectInfo"
        tagprefix="uc1" %>
    <%@ register src="../../UserControls/View/productInfo.ascx" tagname="productInfo"
        tagprefix="uc1" %>
    <%@ register src="/UserControls/View/paymentInfo.ascx" tagname="paymentInfo" tagprefix="uc1" %>
    <%@ register src="/UserControls/View/RequirementDescInfo.ascx" tagname="RequirementDescInfo"
        tagprefix="uc1" %>
    <%--          项目号申请单信息        --%>
    <uc1:projectinfo id="projectInfo" runat="server" />
    <uc1:genericinfo id="GenericInfo" runat="server" />
    <%--          项目号申请单信息        --%>
    <%-- *********采购物料信息********* --%>
    <uc1:productinfo id="productInfo" runat="server" />
    <%-- *********采购物料信息********* --%>
    <%-- *********供应商信息********* --%>
    <uc1:supplierinfo id="supplierInfo" runat="server" />
    <%-- *********供应商信息********* --%>
    <%--       付款账期信息         --%>
    <uc1:paymentinfo id="paymentInfo" runat="server" />
    <%--       付款账期信息         --%>
    <uc1:requirementdescinfo id="RequirementDescInfo" runat="server" />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                审核信息:
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                订单编号:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:Label ID="txtorderid" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                谈判类型:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="txttype" runat="server" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px">
                比价节约:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="txtcontrast" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownContrast" runat="server" />
            </td>
            <td class="oddrow" style="width: 100px">
                议价节约:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="txtconsult" runat="server" Width="200px"></asp:Label>&nbsp;<asp:Label
                    ID="labdownConsult" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px">
                初审人:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="ddlfirst_assessor" runat="server" SkinID="userLabel" />
            </td>
            <td class="oddrow" style="width: 100px">
                事后申请:
            </td>
            <td class="oddrow-l" style="width: 300px">
                <asp:Label ID="labafterwards" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px;">
                紧急采购:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmBuy" runat="server" Width="200px"></asp:Label>
            </td>
            <td class="oddrow" style="width: 120px;">
                客户指定:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCusAsk" runat="server" Width="200px"></asp:Label>
                <asp:Label ID="labCusName" runat="server" Width="200px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 100px;">
                合同号码:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labContractNo" runat="server" Width="200px" />
            </td>
            <td class="oddrow" style="width: 100px;">
                其它:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="txtothers" runat="server" Width="200px" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td style="width: 15%" class="oddrow">
                最后编辑时间:
            </td>
            <td style="width: 35%" class="oddrow-l">
                <asp:Label ID="lablasttime" runat="server" />
            </td>
            <td style="width: 20%" class="oddrow">
                申请单提交时间:
            </td>
            <td style="width: 30%" class="oddrow-l">
                <asp:Label ID="labrequisition_committime" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                订单生成时间:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="laborder_committime" runat="server" />
            </td>
            <td class="oddrow">
                订单审批时间:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="laborder_audittime" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="palFili" runat="server" Visible="false">
        <table width="100%" class="tableForm">
            <tr>
                <td colspan="2" class="heading" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">
                    分公司审核备注:
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
                <td colspan="2" class="heading" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">
                    申请单审批备注:
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
                <td colspan="2" class="heading" style="height: 1px">
                </td>
            </tr>
            <tr>
                <td style="width: 15%" class="oddrow">
                    订单审核备注:
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labOverrule" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="2">
                收货信息
            </td>
        </tr>
        <tr>
            <td colspan="2" class="oddrow-l">
                <asp:GridView ID="gvFP" runat="server" OnRowDataBound="gvFP_RowDataBound" AllowPaging="false"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="RecipientName" HeaderText="收货人" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="收货时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DateTime.Parse(Eval("RecipientDate").ToString()).ToString("yyyy-MM-dd")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.00")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货方式" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="labStatus" runat="server" Text='<%# Eval("Status") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="备注" DataField="Note" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td height="25" colspan="2" align="center">
                <asp:Button ID="btn" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
