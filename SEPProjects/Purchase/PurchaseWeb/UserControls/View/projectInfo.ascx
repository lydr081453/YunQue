<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_View_projectInfo" Codebehind="projectInfo.ascx.cs" %>
<table width="100%" border="0" cellpadding="0" runat="server" id="tabTitle">
    <tr>
        <td background="../../images/allinfo_bg.gif" width="100%">
            <table width="100%" border="0" cellspacing="8" cellpadding="0">
                <tr>
                    <td width="38%" class="f_16px" style="height:20px">
                       <span class="f_12px"><strong>申请人：</strong><asp:Label ID="labTitleUser" runat="server" />&nbsp;&nbsp;<span style="font-size:14px; font-weight:bold; color:Red"><asp:Label ID="labMessage" runat="server" /></span>
                    </td>
                    <td width="62%" align="right" class="f_16px">
                       <strong>最后编辑时间：</strong><asp:Label ID="labTitleDateTime" runat="server" /></span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>    
    <tr>
        <td style="height: 15px">
        </td>
    </tr>
</table>
<table style="width: 100%;" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ① 项目信息
        </td>
    </tr>
    <tr>
        <td class="oddrow"  style="width: 15%">
            流水号:
        </td>
        <td class="oddrow-l"  style="width: 35%">
            <asp:Label ID="labGlideNo" runat="server" />
        </td>
        <td class="oddrow"  style="width: 15%">
            申请单号:
        </td>
        <td class="oddrow-l"  style="width: 35%">
            <asp:Label ID="txtprNo" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            项目号:
        </td>
        <td class="oddrow-l">
            <asp:HyperLink runat="server" style =" cursor:pointer;" ID="hyperProjectCode"></asp:HyperLink>&nbsp;<asp:Label ID="labOldProjectCode" runat="server" ForeColor="Red" />
            
        </td>
        <td class="oddrow">
            项目名称:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtproject_descripttion" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            成本所属组:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labDepartment" runat="server" />
        </td>
        <td class="oddrow">
            货币:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labMoneyType" runat="server" />
        </td>
    </tr>
</table>
