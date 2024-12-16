<%@ Control Language="C#" AutoEventWireup="true" Inherits="System_barchart" Codebehind="barchart.ascx.cs" %>
<table width="100%">
    <tr>
        <td align="center">
            <asp:Label id="lblChartTitle" runat="server" />            
        </td>
    </tr>
    <tr>
        <td>
            <table border="1" cellspacing="0" cellpadding="0" width="85%">
                <tr>
                    <td>
                        <table width="85%">
                            <asp:Label id="lblItems" runat="server" />
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Label id="lblXAxisTitle" runat="server" />
        </td>
    </tr>
</table>

