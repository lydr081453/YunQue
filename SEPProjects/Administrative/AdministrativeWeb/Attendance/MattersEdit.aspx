<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MattersEdit.aspx.cs"  MasterPageFile="~/Default.Master"
    Inherits="AdministrativeWeb.Attendance.MattersEdit" %>
<%@ OutputCache Duration="1" Location="none" %>    
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="Matters" TagName="LeaveEdit" Src="~/UserControls/Matter/LeaveEdit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="OverTimeEdit" Src="~/UserControls/Matter/OverTimeEdit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="OffTuneEdit" Src="~/UserControls/Matter/OffTuneEdit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="OutEdit" Src="~/UserControls/Matter/OutEdit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="TavelEdit" Src="~/UserControls/Matter/TravelEdit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="OtherEdit" Src="~/UserControls/Matter/OtherEdit.ascx" %>
<%@ Register TagPrefix="Matters" TagName="OTLateEdit" Src="~/UserControls/Matter/OTLateEdit.ascx" %>
<asp:Content ID="contenct1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/tabStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 40%;border:solid 1px #c2c3c2" class="table_list2">
        <tr>
            <td align="center" style="height: 20px;"></td>
            <td align="center"><%=DateTime.Parse(SelectDateTime).AddDays(-1).ToString("yyyy-MM-dd") %></td>
            <td align="center"><%=DateTime.Parse(SelectDateTime).ToString("yyyy-MM-dd") %></td>
            <td align="center"><%=DateTime.Parse(SelectDateTime).AddDays(1).ToString("yyyy-MM-dd") %></td>
        </tr>
        <tr>
            <td align="center" style="height: 20px;">上班时间</td>
            <td align="center"><%=ClockIn[0] %></td>
            <td align="center"><%=ClockIn[1] %></td>
            <td align="center"><%=ClockIn[2] %></td>
        </tr>
        <tr>
            <td align="center" style="height: 20px;">下班时间</td>
            <td align="center"><%=ClockOut[0] %></td>
            <td align="center"><%=ClockOut[1]%></td>
            <td align="center"><%=ClockOut[2]%></td>
        </tr>
    </table><br/>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%; height:30px;">
        <tr>
            <td class="ts-l" width="10px"></td>
            <td class="ts-m" width="99%">
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
            <td class="ts-r" width="10px"></td>
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
                                <Matters:LeaveEdit ID="matLeave" runat="server" Visible="false"/>
                            </p>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView>
                            <p>
                                <Matters:OutEdit ID="matOut" runat="server" Visible="false"/>
                            </p>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView>
                            <p>
                                <Matters:TavelEdit ID="matTavel" runat="server" Visible="false"/>
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