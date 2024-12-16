<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_Requisition_SearchPaymentApply"
 Title="Untitled Page" EnableEventValidation="false" Codebehind="SearchPaymentApply.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>
 <%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        function SupplierClick() {
            var generalid = '<%= Request[RequestName.GeneralID]%>';
            var win = window.open('PaymentSearchSupplier.aspx?<% =RequestName.GeneralID %>=' + generalid + '&name=' + document.getElementById("<%= txtsupplier_name.ClientID %>").value, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }
        function SelectAll() {
            for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                var e = document.getElementsByName("chkItem")[i];
                
                    e.checked = document.forms[0].chkAll.checked;
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

           //   var win = window.open('/Purchase/Requisition/Print/PaymantPrint.aspx?recid=' + strid + '', null, 'height=600px,width=600px,scrollbars=yes,top=100px,left=100px');
            var win = window.open('SearchPaymentApply.aspx?recid='+ strid +'', null, 'height=10px,width=10px,scrollbars=no,top=100px,left=100px');
             win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
         }
         function printPr() {
             var chks = document.getElementsByName("chkItem");
             var hids = document.getElementsByName("hidItem");
             var gids = "";
             for (var i = 0; i < chks.length; i++) {
                 if (chks[i].checked == true) {
                     gids += hids[i].value + ",";
                 }
             }
             if (gids == "") {
                 alert("请选择您要打印的数据！");
                 return false;
             }
             gids = gids.substring(0, gids.length - 1);
             window.open("Print/RequisitionPrint.aspx?id=" + gids);
             return false;
         }
         function printGet() {
             var chks = document.getElementsByName("chkItem");
             var hids = document.getElementsByName("hidItem");
             var gids = "";
             for (var i = 0; i < chks.length; i++) {
                 if (chks[i].checked == true) {
                     gids += hids[i].value + ",";
                 }
             }
             if (gids == "") {
                 alert("请选择您要打印的数据！");
                 return false;
             }
             gids = gids.substring(0, gids.length - 1);
             window.open("Print/MultiRecipientPrint.aspx?id=" + gids);
             return false;
         }
    </script>
    <table width="100%"  class="tableForm">
        <tr>
            <td colspan="4" class="heading">
                检索</td>
        </tr>
        <tr>
            <td class="oddrow" style="width:15%">流水号:</td>
            <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtGlideNo" MaxLength="200" runat="server" /></td>
            <td class="oddrow" style="width:15%">订单号:</td>
            <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtPrNo" MaxLength="200" runat="server" /></td>
        </tr>
        <tr>
            <td class="oddrow" style="width:15%">收货单号:</td>
            <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtRNo" MaxLength="200" runat="server" /></td>
            <td class="oddrow">
                协议供应商名称:</td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtsupplier_name" runat="server" MaxLength="200" Width="200px"/>&nbsp;<asp:Button ID="btn" runat="server" OnClientClick="SupplierClick();return false;"
                Text="请选择..." CssClass="widebuttons" Visible="false" />&nbsp;<br /><asp:HiddenField ID="hidsupplierType" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" 
                    onclick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" id="tabTop" runat="server">
        <tr>
            <td align="right" colspan="2">
                <input type="button" id="exportexcel"  value=" 导出 " class="widebuttons" onclick="return exportexcels();"/>
                <input type="button" id="btnPrint" value="批量打印PR单" class="widebuttons" onclick="return printPr();" />
                <input type="button" id="btnPrintGet" value="批量打印收货单" class="widebuttons" onclick="return printGet();" />
               <%-- <asp:Button ID="btnExport" runat="server" Text=" 导出 " CssClass="widebuttons" OnClientClick="return exportexcels();"  />--%>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <cc2:NewGridView ID="gvSupplier" runat="server" AllowSorting="true" AutoGenerateColumns="False" OnRowDataBound="gvSupplier_RowDataBound"
        DataKeyNames="recipientId" PageSize="20" AllowPaging="True" Width="100%" OnRowCommand="gvSupplier_RowCommand" PagerSettings-Position="TopAndBottom">
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate> 
                    <input name="chkAll" id="chkAll" onclick="SelectAll();" type="checkbox" />全选
                </HeaderTemplate> 
                <ItemStyle HorizontalAlign="Center"/> 
                <HeaderStyle HorizontalAlign="Left" Width="50px"/> 
                <ItemTemplate> 
                    <input name="chkItem" id="chkItem" type="checkbox" value='<%# Eval("recipientId").ToString() %>'/>
                    <input type="hidden" id="hidItem" name="hidItem" value='<%# Eval("Gid").ToString() %>' />
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="流水号"  HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# int.Parse(Eval("GID").ToString()).ToString("0000000") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="orderid" HeaderText="订单号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%"/>  
            <asp:BoundField DataField="RecipientNo" HeaderText="收货单号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%"/>  
            <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="6%"/>   
            <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                <ItemTemplate>
                    <%# Eval("requisition_committime").ToString() == State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString()%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="11%"/>
            <asp:TemplateField HeaderText="收货金额" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                  <%# Eval("moneytype").ToString() == "美元" ? "＄" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##") : "￥" + decimal.Parse(Eval("RecipientAmount").ToString()).ToString("#,##0.##")%>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" SortExpression="supplier_name"/>
        </Columns>
    </cc2:NewGridView>

</asp:Content>