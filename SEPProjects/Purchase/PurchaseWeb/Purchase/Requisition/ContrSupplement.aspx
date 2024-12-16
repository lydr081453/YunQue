<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Title="合同追加" CodeBehind="ContrSupplement.aspx.cs" Inherits="PurchaseWeb.Purchase.Requisition.ContrSupplement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Src="../../UserControls/View/genericInfo.ascx" TagName="genericInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="/UserControls/View/projectInfo.ascx" TagName="projectInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="/UserControls/View/supplierInfo.ascx" TagName="supplierInfo"
        TagPrefix="uc1" %>
    <%@ Register Src="/UserControls/View/productInfo.ascx" TagName="productInfo"
        TagPrefix="uc1" %>

    <%--          项目号申请单信息        --%>
    <uc1:projectInfo ID="projectInfo" runat="server" />
    <uc1:genericInfo ID="GenericInfo" runat="server" />
    <%--          项目号申请单信息        --%>
    <%-- *********采购物料信息********* --%>
    <uc1:productInfo ID="productInfo" runat="server" />
    <%-- *********采购物料信息********* --%>
    <%-- *********供应商信息********* --%>
    <uc1:supplierInfo ID="supplierInfo" runat="server" />
    <table style="width: 100%">
        <tr>
            <td class="heading" colspan="2">合同或其他报价信息补充上传
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">补充说明:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtRemark" Width="60%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">选择文件:
            </td>
            <td class="oddrow-l">
                <asp:FileUpload ID="FileUpML" runat="server" Width="60%" />
            </td>
        </tr>
        <tr>
              <td class="oddrow-l">&nbsp;</td>
            <td class="oddrow-l">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons"
                    OnClick="btnSave_Click" />&nbsp;&nbsp;
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 关闭 " CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
