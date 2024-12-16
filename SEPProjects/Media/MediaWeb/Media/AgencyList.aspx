<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AgencyList.aspx.cs" Inherits="MediaWeb.Media.AgencyList" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<link href="/css/gridStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

function showRep(ID, Name) {
    var ret = "<a onclick=\"window.open('AgencyDisplay.aspx?alert=1&aid=" + ID + "','','height=600, width=1000, top=50, left=50, toolbar=no, menubar=no, scrollbars=false, resizable=no,location=no, status=no')\";>"+Name+"</a>";

    return ret;
}
function editRep(ID) {
    var mid = "";
    var page = "height=600, width=1000, top=50, left=50, toolbar=no, menubar=no, scrollbars=false, resizable=no,location=no, status=no";
    var ret = "<a href='AgencyAddAndEdit.aspx?alert=1&Operate=EDIT&aid="+ ID+"&Mid="+mid.value+"','','"+ page+"');\" ><img src='/images/edit.gif' /></a>";
    ret += "<a href='MediaAddAndEdit.aspx?Operate=DELReporter&Rid=" + ID + "&Mid=" + mid.value + "' onclick= \"return confirm( '真的要删除吗?');\" ><img src='/images/disable.gif' /></a>";
    return ret;
}

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%;">
    <tr>
                        <td colspan="4" >
                            <a href="AgencyAddAndEdit.aspx" >添加新机构</a>
                        </td>
                    </tr>
        <tr>
    <tr>
                        <td colspan="4" class="headinglist">
                            机构列表
                        </td>
                    </tr>
        <tr>
            <td colspan="4">      
            <cc4:MyGridView ID="glist" runat="server" OnRowDataBound="gList_RowDataBound"
                                OnSorting="gList_Sorting">
                            </cc4:MyGridView>          
                    <%--<table style="width: 100%;" border="0">
                        <tr>
                            <td class="headinglist" colspan="3">
                                
                            </td>
                        </tr>                       
                        <tr>
                            <td align="center" colspan="3">                                
                                <ComponentArt:Grid ID="dgList" PreHeaderClientTemplateId="PreHeaderTemplate"
                                    PostFooterClientTemplateId="PostFooterTemplate" DataAreaCssClass="GridData" EnableViewState="false" ClientTarget="Uplevel"
                                    ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Buttons"
                                    PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                    PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                    SliderPopupOffsetX="50" ImagesBaseUrl="/newmedia/images/gridview/" PagerImagesFolderUrl="/newmedia/images/gridview/pager/"
                                    TreeLineImagesFolderUrl="/newmedia/images/grid/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                    Width="100%" Height="100%" runat="server" EditOnClickSelectedItem="false">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="AgencyID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                            SortImageHeight="19">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="AgencyID" Visible="false" />    
                                                <ComponentArt:GridColumn DataField="AgencyCName" Visible="false" />                                                                                            
                                                <ComponentArt:GridColumn HeadingText="机构中文名" DataCellClientTemplateId="showTemplate" Align="Center" />
                                                <ComponentArt:GridColumn HeadingText="机构英文名" DataField="AgencyEName" Align="Center" />                                                                                                                                           
                                                <ComponentArt:GridColumn HeadingText="操作" DataCellClientTemplateId="editTemplate" Align="Center" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates> 
                            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; height: 34px; background-image: url(/newmedia/images/gridview/grid_preheader_01.jpg);"></td>
                                        <td style="height: 34px; background-image: url(/newmedia/images/gridview/grid_preheader_02.jpg);background-repeat: repeat-x;">
                                            <span style="color: White; font-weight: bold;">机构列表</span>
                                        </td>
                                        <td style="width: 10px; height: 34px; background-image: url(/newmedia/images/gridview/grid_preheader_03.jpg);"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; background-image: url(/newmedia/images/gridview/grid_postfooter_01.jpg);background-repeat: repeat-x;"></td>
                                        <td style="background-image: url(/newmedia/images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;</td>
                                        <td style="width: 10px; background-image: url(/newmedia/images/gridview/grid_postfooter_03.jpg);background-repeat: repeat-x;"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate> 
                            <ComponentArt:ClientTemplate ID="showTemplate">
                                 ## showRep(DataItem.GetMember('AgencyID').Value,DataItem.GetMember('AgencyCName').Value); ##                                
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="editTemplate">
                                 ## editRep(DataItem.GetMember('AgencyID').Value); ##                                
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                                </ComponentArt:Grid>
                            </td>
                        </tr>
                    </table>--%>
                
            </td>
        </tr>
    </table>
</asp:Content>

