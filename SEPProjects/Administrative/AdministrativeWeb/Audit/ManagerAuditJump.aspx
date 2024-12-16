<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerAuditJump.aspx.cs" Inherits="AdministrativeWeb.Audit.ManagerAuditJump" MasterPageFile="~/Default.Master"%>
<%@ OutputCache Duration="1" Location="none" %>
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">    
        <script language="javascript">
            onload = function jump() {
                window.location = "MonthStatAuditList.aspx?type=5";
            }
    </script>  
    
</asp:Content>
