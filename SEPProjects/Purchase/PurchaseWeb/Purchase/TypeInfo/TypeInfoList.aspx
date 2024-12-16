<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Purchase_TypeInfo_TypeInfoList" Codebehind="TypeInfoList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function btnTypeClick() {
        window.location.href = "TypeInfoEdit.aspx?tid=0";
    }
</script>
    <table width="100%"  border="0">
        <tr>
            <td valign="top">
                <div style="position: relative; vertical-align: top; padding-left: 20px;
                    height: 100%">
                    <span runat="server" onclick="btnTypeClick();" style="cursor:pointer" >物料类别</span>
                    <yyc:SmartTreeView ID="stv1" runat="server" AllowCascadeCheckbox="True" ImageSet="Msdn" ShowLines="true"
                        NodeIndent="20">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                        <SelectedNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                        <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="1px" VerticalPadding="2px" />
                    </yyc:SmartTreeView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
