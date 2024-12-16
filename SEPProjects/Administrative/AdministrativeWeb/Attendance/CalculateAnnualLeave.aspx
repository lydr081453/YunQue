<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalculateAnnualLeave.aspx.cs" Inherits="AdministrativeWeb.Attendance.CalculateAnnualLeave" 
    MasterPageFile="~/Default.Master"%>

<%@ OutputCache Duration="1" Location="none" %>   
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list_2">
            <tr>
                <td width="20px" align="right">
                    年份：
                </td>
                <td width="100px" align="left">
                    <asp:DropDownList ID="drpYear" runat="server">
                    </asp:DropDownList>年
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Button Text="计算年假" ID="btnCalculateAnnual" runat="server" 
                        onclick="btnCalculateAnnual_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>