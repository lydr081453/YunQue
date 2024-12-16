<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectorAuditJump.aspx.cs" Inherits="AdministrativeWeb.Audit.DirectorAuditJump" MasterPageFile="~/Default.Master" %>
<%@ OutputCache Duration="1" Location="none" %>
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">    
        <script language="javascript">
            onload = function jump() {
                window.location = "MonthStatAuditList.aspx?type=2";
            }
    </script>  
    
</asp:Content>
