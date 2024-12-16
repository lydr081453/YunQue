<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="MediumDlg.aspx.cs" Inherits="FinanceWeb.Dialogs.MediumDlg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
<link href="/public/css/treelist.css" type="text/css" rel="stylesheet" />
<script language="javascript" src="/public/js/dialog.js"></script>
<script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

<script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

<script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

<script src="/public/js/dimensions.js" type="text/javascript"></script>

<script language="javascript">


    function show(){
        
        dialog("组织机构图", "id:testID", "500px", "300px", "text");
    }


</script>

    <table width="100%" class="tableForm">
    <tr>
    <td colspan="4">
     <font color="red">如果未找到您所需要的媒体名称，请保存当前信息，到媒介信息系统新增媒体。</font>
    </td>
    </tr>
        <tr> 
            <td colspan="4" class="heading">检索</td>     
        </tr>
        <tr>
            <td style="width:15%" class="oddrow">检索关键字：</td>
            <td class="oddrow-l" style="width:20%"><asp:TextBox ID="txtName" runat="server" />&nbsp;</td>
            <td class="oddrow" style="width: 15%">媒体类型：</td>
            <td class="oddrow-l"><asp:DropDownList runat="server" ID="ddlOption">
            <asp:ListItem Text="请选择.." Value="0"></asp:ListItem>
            <asp:ListItem Text="平面媒体" Value="1"></asp:ListItem>
            <asp:ListItem Text="网络媒体" Value="2"></asp:ListItem>
            <asp:ListItem Text="电视媒体" Value="3"></asp:ListItem>
            <asp:ListItem Text="广播媒体" Value="4"></asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4"><asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="widebuttons" Text=" 检索 " />&nbsp;<asp:Button ID="btnClean" runat="server" CssClass="widebuttons" OnClick="btnClean_Click" Text=" 重新搜索 " /></td>
        </tr>
    </table>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false" DataKeyNames="mediaitemid" OnRowCommand="gv_RowCommand" Width="100%" CellPadding="4">
        
        <Columns>
        <asp:TemplateField>
            <HeaderTemplate>选择</HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkSelected" runat="server" /><asp:HiddenField ID="hidMediaID" runat="server" Value='<%# Eval("mediaitemid") %>' />
            </ItemTemplate>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
        </asp:TemplateField>
            <asp:BoundField DataField="medianame" HeaderText="媒体名称" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="MediumSort" HeaderText="形态" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="headquarter" HeaderText="地域属性" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="IndustryName" HeaderText="行业属性" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="TelephoneExchange" HeaderText="总机" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnSelected" runat="server" Text=" 选择 " OnClick="btnSelected_Click" />&nbsp;<input type="button" class="widebuttons" value=" 关闭 " onclick="window.close();" />
    <div id="testID" style="display:none" >
       <div class="tree" id="divModuleTree" style="BORDER-RIGHT:1px inset; BORDER-TOP:1px inset; OVERFLOW:auto; BORDER-LEFT:1px inset; WIDTH:400px; BORDER-BOTTOM:1px inset; HEIGHT:300px; BACKGROUND-COLOR:white"
			 runat="server"></div>
	</div>
</asp:Content>