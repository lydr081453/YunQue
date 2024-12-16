<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchTab.aspx.cs" Inherits="FinanceWeb.UserControls.Project.BatchTab" %>

<script type="text/javascript">
    function changeClass(id) { 
       id.className="button_th";
   }
   function changeClass2(id) {
       id.className = "button_over";
   }
</script>

<table width="100%" id="abc" border="0" cellspacing="0" cellpadding="0" style="border-bottom: solid 1px #15428b;">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" class="button_on" runat="server"
                id="Table1">
                <tr>
                    <td>
                        <asp:linkbutton id="Tab1" runat="server" text=" 待审批 " />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="button_over" runat="server"
                id="Table2">
                <tr>
                    <td>
                        <asp:linkbutton id="Tab2" runat="server" text=" 已审批 " />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
