<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatistictiansAuditJump.aspx.cs" Inherits="AdministrativeWeb.Audit.StatistictiansAuditJump" MasterPageFile="~/Default.Master" %>
<%@ OutputCache Duration="1" Location="none" %>
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">    
        <script language="javascript">
            onload = function jump() {
                window.location = "MonthStatAuditList.aspx?type=3";
            }
    </script>  
    
</asp:Content>
