<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AssetView.aspx.cs" Inherits="FinanceWeb.ITAsset.AssetView" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">固定资产信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">资产名称: 
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="lblName"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">资产编号:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="lblCode"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">资产类别:
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="lblCategory"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">品牌: 
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="lblBrand"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">型号: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:Label runat="server" ID="lblModel"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">配置: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:Label runat="server" ID="lblConfig"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">价格: 
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="lblPrice"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">购买日期: 
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID="lblDate"></asp:Label>
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 20%">
                单据引用: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                   <asp:Label runat="server" ID="lblPR"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">附件: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:HyperLink runat="server" ID="hpFile" ImageUrl="/images/ico_04.gif" Visible="false" Target="_blank"></asp:HyperLink>
            </td>

        </tr>
         <tr>
            <td class="oddrow" style="width: 20%">
                照片: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
               <asp:Image runat="server" ID="imgPhoto" Visible="false" Width="200" Height="200"/> <br /> 
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">备注: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:Label runat="server" ID="lblDesc"></asp:Label>
            </td>

        </tr>
         <tr>
            <td colspan="4" class="heading">领用记录
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Label runat="server" ID="lblReceiveLog"></asp:Label>
            </td>
        </tr>
         <tr>
            <td colspan="4" class="heading">报废记录: 
            </td>
        </tr>
        <tr>
              <td colspan="4" class="oddrow-l">
                <asp:Label runat="server" ID="lblScrapLog"></asp:Label>
            </td>
        </tr>
    </table>

    <table width="100%" class="tableForm" id="tabReceiver" runat="server" visible="false">
        <tr>
            <td colspan="4" class="heading">领用登记
            </td>
        </tr>
         <tr>
            <td class="oddrow" style="width: 20%">领用人: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:TextBox runat="server" ID="txtUser"></asp:TextBox><asp:Button runat="server" ID ="btnUserAdd" Text="..." OnClick="btnUserAdd_Click"/>
                <asp:DropDownList runat="server" ID="ddlServer" OnSelectedIndexChanged="ddlServer_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Selected="True" Text="默认" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 20%">领用人: 
             </td>
             <td class="oddrow-l" style="width: 30%">
                  <asp:Label runat="server" ID ="lblReceiver"></asp:Label><asp:HiddenField runat="server" ID="hiddenUserid" />
             </td>
             <td class="oddrow" style="width: 20%">员工编号: 
             </td>
            <td class="oddrow-l" style="width: 30%">
                 <asp:Label runat="server" ID ="lblReceiverCode"></asp:Label>
            </td>
        </tr>
         <tr>
             <td class="oddrow" style="width: 20%">邮箱: 
             </td>
             <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID ="lblReceiverEmail"></asp:Label>
             </td>
              <td class="oddrow" style="width: 20%">手机号: 
             </td>
             <td class="oddrow-l" style="width: 30%">
                <asp:Label runat="server" ID ="lblMobile"></asp:Label>
             </td>
        </tr>
        <tr>
              <td class="oddrow" style="width: 20%">备注: 
             </td>
             <td class="oddrow-l"  colspan="3">
                  <asp:TextBox runat="server" ID="txtReceiveDesc" TextMode="MultiLine" Height="100" Width="500"></asp:TextBox>
             </td>
        </tr>
    </table>
     <table width="100%" class="tableForm" id="tabScrap" runat="server" visible="false">
          <tr>
            <td colspan="4" class="heading">报废登记
            </td>
        </tr>
         <tr>
               <td class="oddrow" style="width: 20%">报废原因: 
             </td>
             <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtScrap" TextMode="MultiLine" Height="100" Width="500"></asp:TextBox>
             </td>
         </tr>
     </table>
         <table width="100%" class="tableForm" id="tabScrptAudit" runat="server" visible="false">
          <tr>
            <td colspan="4" class="heading">报废审核
            </td>
        </tr>
             <tr>
               <td class="oddrow" style="width: 20%">报废原因: 
             </td>
             <td class="oddrow-l">
              <asp:Label runat="server" ID="lblScrapReason"></asp:Label>
             </td>
         </tr>
         <tr>
               <td class="oddrow" style="width: 20%">审批备注: 
             </td>
             <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtScrapAudit" TextMode="MultiLine" Height="100" Width="500"></asp:TextBox>
             </td>
         </tr>
     </table>
     <table width="100%" class="tableForm" id="tabReturn" runat="server" visible="false">
          <tr>
            <td colspan="4" class="heading">归还登记
            </td>
        </tr>
         <tr>
               <td class="oddrow" style="width: 20%">归还备注: 
             </td>
             <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtReturnRes" TextMode="MultiLine" Height="100" Width="500"></asp:TextBox>
             </td>
         </tr>
     </table>
    <table width="100%" class="XTable">
        <tr>
            <td style="flex-align:center; ">
                <asp:Button ID="btnReceiver" runat="server" Text=" 确定领用 " Visible="false" CssClass="widebuttons" OnClick="btnReceiver_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnScrap" runat="server" Text=" 确定报废 " Visible="false" CssClass="widebuttons" OnClick="btnScrap_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" Text=" 确定还回 " Visible="false" CssClass="widebuttons" OnClick="btnReturn_Click" />&nbsp;&nbsp;
                 <asp:Button ID="btnScrapAudit" runat="server" Text=" 报废审批 " Visible="false" CssClass="widebuttons" OnClick="btnScrapAudit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text=" 返回 " CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

