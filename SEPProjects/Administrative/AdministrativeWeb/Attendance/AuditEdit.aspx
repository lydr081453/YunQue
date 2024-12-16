<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditEdit.aspx.cs" MasterPageFile="~/Default.Master"
    Inherits="AdministrativeWeb.Attendance.AuditEdit" %>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Matters" TagName="LeaveAudit" Src="~/UserControls/MatterAudit/LeaveAudit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="TavelAudit" Src="~/UserControls/MatterAudit/TravelAudit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="OutAudit" Src="~/UserControls/MatterAudit/OutAudit.ascx" %>

<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/tabStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <br />
    <asp:HiddenField ID="hidApproveId" runat="server" />
    <asp:HiddenField ID="hidOverTimeId" runat="server" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 30px;">
        <tr>
            <td class="ts-l" width="10px">
            </td>
            <td class="ts-m">
                <ComponentArt:TabStrip ID="tabMatters" runat="server" DefaultItemLookId="DefaultTabLook"
                    DefaultSelectedItemLookId="SelectedTabLook" MultiPageId="MultiPage1">
                    <ItemLooks>
                        <ComponentArt:ItemLook LookId="DefaultTabLook" CssClass="tab" HoverCssClass="tab-h" />
                        <ComponentArt:ItemLook LookId="SelectedTabLook" CssClass="tab-s" />
                    </ItemLooks>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="TabTemplate">
                            <div class="t">
                                <div class="l">
                                </div>
                                <div class="m">
                                    ## DataItem.get_text(); ##</div>
                                <div class="r">
                                </div>
                            </div>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:TabStrip>
            </td>
            <td class="ts-r" width="10px">
            </td>
        </tr>
    </table>

	<div style="width:100%;">
		<div style="width:100%; height:5px;">
			<div class="mpt-l"><span></span></div>
			<div class="mpt-m"><span></span></div>
			<div class="mpt-r"><span></span></div>
		</div>
		<table border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td class="mpc-l">&nbsp;&nbsp;</td>
		        <td class="mpc">
                    <ComponentArt:MultiPage ID="MultiPage1" runat="server" Width="100%">
                        <ComponentArt:PageView>
                            <p>
                                <Matters:LeaveAudit ID="matLeave" runat="server" Visible="false"/>
                            </p>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView>
                            <p>
                                <Matters:OutAudit ID="matOut" runat="server" Visible="false"/>
                            </p>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView>
                            <p>
                                <Matters:TavelAudit ID="matTavel" runat="server" Visible="false"/>
                            </p>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
			    </td>
		        <td class="mpc-r">&nbsp;&nbsp;</td>
		    </tr>
		</table>
	</div>
	<table border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 10px;">
        <tr>
            <td class="mpb-l" width="10px">
            </td>
            <td class="mpb-m" width="99%">
            </td>
            <td class="mpb-r" width="10px">
            </td>
        </tr>
    </table>
</asp:Content>