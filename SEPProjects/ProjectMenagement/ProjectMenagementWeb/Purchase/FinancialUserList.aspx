<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_FinancialUserList" Title="������Աѡ��" Codebehind="FinancialUserList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />
<link href="/public/css/treelist.css" type="text/css" rel="stylesheet" />
<script language="javascript" src="/public/js/dialog.js"></script>
<script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

<script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

<script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

<script src="/public/js/dimensions.js" type="text/javascript"></script>

<script type="text/javascript">

    function selectAll(obj) {
        var theTable = obj.parentElement.parentElement.parentElement;
        var i;
        var j = obj.parentElement.cellIndex;

        for (i = 0; i < theTable.rows.length; i++) {
            var objCheckBox = theTable.rows[i].cells[j].firstChild;
            if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
        }
    }

</script>

    <table width="100%" class="tableForm">
        <tr>
            <td colspan="2" class="heading">����</td>     
         </tr>
        <tr>
            <td style="width:15%" class="oddrow">�����ؼ��֣�</td>
            <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtName" runat="server" />&nbsp;</td>
            <td style="width:15%" class="oddrow">����ѡ��</td>
            <td class="oddrow-l" style="width:35%">
            <asp:DropDownList runat="server" ID="ddlDept" ></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4"><asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="widebuttons" Text=" ���� " />&nbsp;<asp:Button ID="btnClean" runat="server" CssClass="widebuttons" OnClick="btnClean_Click" Text=" �������� " /></td>
        </tr>
    </table>
     <asp:Button ID="btnSubMit1" runat="server" Text=" ȷ�� " CssClass="widebuttons" 
        onclick="btnSubMit_Click" />
    <input type="button" class="widebuttons" value=" �ر� " onclick="window.close();" />
    <asp:GridView ID="gv" runat="server" OnDataBinding="gv_DataBinding" OnRowDataBound="gv_RowDataBound" AutoGenerateColumns="false" DataKeyNames="SysUserID"  Width="100%" CellPadding="4">
        
        <Columns>
            <asp:TemplateField>
                   <ItemTemplate>
                       <asp:CheckBox ID="chkEmp" runat="server" Checked="false" Text='' />
                    </ItemTemplate>
                   <HeaderTemplate>
                      &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />ȫѡ
                    </HeaderTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="sysuserid" HeaderText="sysuserid" />
            <asp:BoundField DataField="userid" HeaderText="userid"  />
            <asp:BoundField DataField="usercode" HeaderText="Ա���ʺ�" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="username" HeaderText="Ա������" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="userid" HeaderText="Ա�����" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Center" />
            <%--<asp:BoundField DataField="telephone" HeaderText="�绰" ItemStyle-HorizontalAlign="Center" />--%>
            <asp:TemplateField HeaderText="�绰" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("telephone") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="mobile" HeaderText="�ֻ�" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="positiondescription" HeaderText="ְ��" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="status" HeaderText="status" Visible="False" />
        </Columns>
    </asp:GridView>
     <asp:Button ID="btnSubMit" runat="server" Text=" ȷ�� " CssClass="widebuttons" 
        onclick="btnSubMit_Click" />
    <input type="button" class="widebuttons" value=" �ر� " onclick="window.close();" />
    <div id="testID" style="display:none" >
       <div class="tree" id="divModuleTree" style="BORDER-RIGHT:1px inset; BORDER-TOP:1px inset; OVERFLOW:auto; BORDER-LEFT:1px inset; WIDTH:400px; BORDER-BOTTOM:1px inset; HEIGHT:300px; BACKGROUND-COLOR:white"
			 runat="server"></div>
	</div>
     
</asp:Content>

