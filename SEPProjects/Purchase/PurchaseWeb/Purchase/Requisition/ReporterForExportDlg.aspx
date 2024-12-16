<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Purchase_Requisition_ReporterForExportDlg" Title="记者选择" Codebehind="ReporterForExportDlg.aspx.cs" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
<link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
<link href="/public/css/treelist.css" type="text/css" rel="stylesheet" />
<script language="javascript" src="/public/js/dialog.js"></script>
<script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>



<script language="javascript">

    function show(){
        
        dialog("组织机构图", "id:testID", "500px", "300px", "text");
    }


</script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4" class="heading">检索</td>     
          <%--  <td colspan="2" class="heading" style="text-align:right"><span id="sp" onclick="show();" style="color:#7282a9;cursor:pointer">组织机构图</span></td>       --%>
        </tr>
        <tr>
            <td style="width:15%" class="oddrow">检索关键字：</td>
            <td class="oddrow-l" style="width:20%"><asp:TextBox ID="txtName" runat="server" />&nbsp;</td>
            <td class="oddrow" colspan="2">
            <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="widebuttons" Text=" 检索 " />&nbsp;
            <asp:Button ID="btnClean" runat="server" CssClass="widebuttons" OnClick="btnClean_Click" Text=" 重新搜索 " />&nbsp;
            <asp:Button ID="btnNew" runat="server" CssClass="widebuttons" OnClick="btnNew_Click" Text=" 新建 " />
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
    </table>

    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" DataKeyNames="reporterid" OnRowCommand="gv_RowCommand" Width="100%" CellPadding="8">
        <Columns>
        <asp:TemplateField>
            <HeaderTemplate>选择</HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkSelected" runat="server" /><asp:HiddenField ID="hidReporterID" runat="server" Value='<%# Eval("reporterid") %>' /><asp:HiddenField ID="hidMediaID" runat="server" Value='<%# Eval("reporterid") %>' />
            </ItemTemplate>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
        </asp:TemplateField>
            <%--<asp:ButtonField Text="选择" HeaderText="选择" CommandName="Add" ButtonType="button" ItemStyle-HorizontalAlign="Center" />--%>
            <asp:BoundField DataField="reporterid" HeaderText="reporterid" Visible="False" />
            <asp:BoundField DataField="reportername" HeaderText="记者姓名" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="medianame" HeaderText="所属媒体" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="sex" HeaderText="性别" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CityName" HeaderText="所在城市" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="tel" HeaderText="电话" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="mobile" HeaderText="手机" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="cardnumber" HeaderText="身份证号" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="bankname" HeaderText="开户行" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="PayType" HeaderText="支付形式" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="bankaccountname" HeaderText="帐号" ItemStyle-HorizontalAlign="Center"  />
             <asp:BoundField DataField="bankcardname" HeaderText="收款人" ItemStyle-HorizontalAlign="Center"  />

        </Columns>
    </asp:GridView>
    <asp:Button ID="btnSelected" runat="server" Text=" 选择 " OnClick="btnSelected_Click" />&nbsp;<input type="button" class="widebuttons" value=" 关闭 " onclick="window.close();" />
    <div id="testID" style="display:none" >
       <div class="tree" id="divModuleTree" style="BORDER-RIGHT:1px inset; BORDER-TOP:1px inset; OVERFLOW:auto; BORDER-LEFT:1px inset; WIDTH:400px; BORDER-BOTTOM:1px inset; HEIGHT:300px; BACKGROUND-COLOR:white"
			 runat="server"></div>
	</div>
     
                
</asp:Content>
