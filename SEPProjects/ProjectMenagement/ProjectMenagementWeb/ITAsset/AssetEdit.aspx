<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AssetEdit.aspx.cs" Inherits="FinanceWeb.ITAsset.AssetEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

    </script>
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                固定资产信息
            </td>
        </tr>
        <tr>
             <td class="oddrow"  style="width: 20%">
                资产编号:
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                 <asp:TextBox runat="server" ID="txtCode"></asp:TextBox><font color="red"> * </font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                资产名称: 
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" ID="txtName"></asp:TextBox><font color="red"> * </font>
            </td>
             <td class="oddrow" style="width: 20%">
                所属地区: 
            </td>
            <td class="oddrow-l" style="width: 30%">
               <asp:DropDownList runat="server" ID="ddlArea">
                   <asp:ListItem Text="北京" Value ="BJ" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="上海" Value="SH"></asp:ListItem>
                   <asp:ListItem Text="广州" Value="GZ"></asp:ListItem>
               </asp:DropDownList>  <font color="red"> * </font>
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 20%">
                资产类别:
            </td>
            <td class="oddrow-l" style="width: 30%">
              <asp:DropDownList runat="server" ID="ddlCategory"></asp:DropDownList>
            </td>
            <td class="oddrow" style="width: 20%">
                品牌: 
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox runat="server" ID="txtBrand"></asp:TextBox><font color="red"> * </font>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                型号: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:TextBox runat="server" ID="txtModel"></asp:TextBox><font color="red"> * </font>
            </td>

        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                配置: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:TextBox runat="server" ID="txtConfig" Width="630px"></asp:TextBox><font color="red"> * </font>
            </td>

        </tr>
          <tr>
            <td class="oddrow" style="width: 20%">
                价格: 
            </td>
            <td class="oddrow-l" style="width: 30%" >
                <asp:TextBox runat="server" ID="txtPrice" ></asp:TextBox><font color="red"> * </font>
            </td>
               <td class="oddrow" style="width: 20%">
                购买日期: 
            </td>
            <td class="oddrow-l" style="width: 30%" >
                <asp:TextBox runat="server" ID="txtDate"  onkeyDown="return false; " Style="cursor:pointer;"
                    onclick="setDate(this);"></asp:TextBox><font color="red"> * </font>
            </td>
        </tr>
        <tr>
             <td class="oddrow" style="width: 20%">
                单据引用: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                   <asp:TextBox runat="server" ID="txtPR" Width="630px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                附件: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                 <asp:FileUpload ID="fileUp" runat="server" Width="60%" /> <asp:HyperLink runat="server" ID="hpFile" ImageUrl="/images/ico_04.gif" Visible="false" Target="_blank"></asp:HyperLink>
            </td>

        </tr>
          <tr>
            <td class="oddrow" style="width: 20%">
                照片: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:Image runat="server" ID="imgPhoto" Visible="false" Width="200" Height="200"/> <br />
                <asp:FileUpload ID="filePhoto" runat="server" Width="60%" /> <br /><br />
            </td>

        </tr>
          <tr>
            <td class="oddrow" style="width: 20%">
                备注: 
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:TextBox runat="server" ID="txtDesc" Width="630px" Height="50px" TextMode="MultiLine"></asp:TextBox><font color="red"> * </font>
            </td>

        </tr>
    </table>
    <table width="100%" class="XTable">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
                &nbsp;<asp:Button ID="btnBack" runat="server" Text=" 返回 " CausesValidation="false"
                    CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
