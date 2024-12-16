<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="CollateDetail.aspx.cs" Inherits="Purchase_Requisition_CollateDetail" %>

<%@ Import Namespace="ESP.Purchase.Common" %>
<%@ Register Src="../../UserControls/View/genericInfo.ascx" TagName="genericInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/View/projectInfo.ascx" TagName="projectInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/View/productInfo.ascx" TagName="productInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript">
        function viewTR(controlId) {
            var control = document.getElementById(controlId);
            if (control.style.display == "none") {
                control.style.display = "block";
            } else if (control.style.display == "block") {
                control.style.display = "none";
            }
        }

        function openPrint() {
            var oldPRID = '<%= Request["oldPRID"] %>';
            var win = window.open("Print/CollatePrint.aspx?oldPRID=" + oldPRID, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
            return false;
        }
    </script>

    <table width="100%">
        <tr>
            <td>
                <asp:Button ID="butPrint" runat="server" Text=" 打印 " CssClass="widebuttons" OnClientClick=" return openPrint();" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons"
                    OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" style="border: 1px solid #eaedf1">
        <tr onclick="viewTR('trOld');" style="cursor: pointer">
            <td class="heading">
                原始申请单
            </td>
        </tr>
        <tr style="display: none" id="trOld">
            <td style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px">
                <%--          项目号申请单信息        --%>
                <uc1:projectInfo ID="projectInfo" runat="server" />
                <%-- *********一般信息********* --%>
                <uc1:genericInfo ID="GenericInfo" runat="server" />
                <%-- *********采购物料信息********* --%>
                <uc1:productInfo ID="productInfo" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" style="border: 1px solid #eaedf1">
        <tr onclick="viewTR('trNew');" style="cursor: pointer">
            <td class="heading">
                新申请单
            </td>
        </tr>
        <tr style="display: none" id="trNew">
            <td style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px; padding-top: 4px">
                <%--          项目号申请单信息        --%>
                <uc1:projectInfo ID="projectInfo1" runat="server" />
                <%-- *********一般信息********* --%>
                <uc1:genericInfo ID="GenericInfo1" runat="server" />
                <%-- *********采购物料信息********* --%>
                <uc1:productInfo ID="productInfo1" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" style="border: 1px solid #eaedf1">
        <tr onclick="viewTR('trdown3000');" style="cursor: pointer">
            <td class="heading">
                小于3000生成付款申请
            </td>
        </tr>
        <tr style="display: none" id="trdown3000">
            <td>
                <asp:GridView ID="down3000GV" runat="server" Width="100%" OnRowCommand="down3000GV_RowCommand" OnRowDataBound="down3000GV_RowDataBound"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="returnid" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPR"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                             <asp:Label runat="server" ID="lblPN"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="起始时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# Eval("PReBeginDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("PReEndDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatusName" />
                                <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发送邮件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMail" runat="server" CommandArgument='<%# Eval("returnid") %>'
                                    CommandName="Mail" Text="<img src='/images/Icon_Sendmail.gif' title='发送邮件' border='0'>"
                                    OnClientClick="return confirm('你确定给原始PR单的申请人发送邮件吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" style="border: 1px solid #eaedf1">
        <tr onclick="viewTR('trnewGV');" style="cursor: pointer">
            <td class="heading">
                新申请单付款申请
            </td>
        </tr>
        <tr style="display: none" id="trnewGV">
            <td>
                <asp:GridView ID="newGV" runat="server" Width="100%" AutoGenerateColumns="false"
                    OnRowDataBound="newGV_RowDataBound" OnRowCommand="newGV_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="returnid" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PurchasePayID" HeaderText="帐期流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="ProjectID" HeaderText="项目流水" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPR"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PN号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPN"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="预付金额" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="起始时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# Eval("PReBeginDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="结束时间" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("PReEndDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="付款状态" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatusName" />
                                <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="帐期类型" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="发送邮件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMail" runat="server" CommandArgument='<%# Eval("returnid") %>'
                                    CommandName="Mail" Text="<img src='/images/Icon_Sendmail.gif' title='发送邮件' border='0'>"
                                    OnClientClick="return confirm('你确定给原始PR单的申请人发送邮件吗？');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
