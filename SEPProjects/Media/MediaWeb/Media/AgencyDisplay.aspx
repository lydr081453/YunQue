<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgencyDisplay.aspx.cs" Inherits="MediaWeb.Media.AgencyDisplay" MasterPageFile="~/MasterPage.master"  %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<link href="../css/gridStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
 
function showRep(ID, Name) {
    var ret = "<a onclick=\"window.open('ReporterDisplay.aspx?alert=1&Rid=" + ID + "','','height=600, width=1000, top=50, left=50, toolbar=no, menubar=no, scrollbars=false, resizable=no,location=no, status=no')\";>"+Name+"</a>";

    return ret;
}

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%;" border="1" class="tableForm">
    <tr>
        <td colspan="4" class="menusection-Packages">
            机构信息维护
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            基本信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            机构中文名称：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="labAgencyName" runat="server" MaxLength="50"></asp:Label><
        </td>
         <td class="oddrow" style="width: 20%">
            机构英文名称：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="labAgencyEngName" runat="server" MaxLength="50"></asp:Label>
        </td>   
    </tr>
    <tr>        
        <td class="oddrow">
            地域属性：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labRegionAttribute" runat="server" />
        </td>
        <td class="oddrow" >
            国家：
        </td>
        <td class="oddrow-l" >
            <asp:Label ID="labCountry" runat="server"/>            
        </td>
    </tr>    
    <tr >
                        <td class="oddrow">
                            省：
                        </td>
                        <td class="oddrow-l" >
                            <asp:Label ID="labProvince"  runat="server" />                            
                        </td>
                        <td class="oddrow" >
                            市：
                        </td>
                        <td class="oddrow-l"  >
                            <asp:Label ID="labCity" runat="server"/>
                            
                        </td>
                    </tr>
    <tr>
        <td class="oddrow">
            负责人：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labResponsiblePerson" runat="server" MaxLength="20"></asp:Label>
        </td>
        <td class="oddrow">
            联系人：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labContacter" runat="server" MaxLength="20"></asp:Label>
        </td>
    </tr>   

    <tr>
        <td colspan="4" class="heading">
            联系方式及所在地址
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            总机：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labTelephoneExchange" runat="server" MaxLength="20"></asp:Label>           
        </td>
        <td class="oddrow">
            传真：
        </td>
        <td class="oddrow-l">            
            <asp:Label ID="labFax" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            热线1：
        </td>
        <td class="oddrow-l">            
            <asp:Label ID="labPhoneOne" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            热线2：
        </td>
        <td class="oddrow-l">            
            <asp:Label ID="labPhoneTwo" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            详细地址：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labAddress1" runat="server" MaxLength="50"></asp:Label>
        </td>
        <td class="oddrow">
            邮政编码：
        </td>
        <td class="oddrow-l">
            <asp:Label ID="labPostCode" runat="server" MaxLength="10" />
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td colspan="4" style="height: 30px">
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            其他信息
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            机构简介：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labAgencyIntro" runat="server" TextMode="MultiLine" Height="98px"
                Width="80%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            英文简介：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labEngIntro" runat="server" TextMode="MultiLine" Height="98px" Width="80%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            备注：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="labRemarks" runat="server" TextMode="MultiLine" Height="98px" Width="80%"></asp:Label>
        </td>
    </tr>
<tr>
            <td colspan="6" style="text-align: right">                
                                
                <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
            <asp:HiddenField ID="hidBackUrl" runat="server" />
            <asp:HiddenField ID="hidMid" runat="server" />
        </tr>
    <input type="hidden" id="RoleColl" runat="server">
    <input type="hidden" id="hidCityAddr1" runat="server">
    <input type="hidden" id="hidCity" runat="server">
    <input type="hidden" id="hidPro" runat="server">
    <input type="hidden" id="hidCountry" runat="server">
</table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="height:25px" />
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="pReaporter">
                    <table style="width: 100%;" border="0">
                        <tr>
                            <td class="headinglist" colspan="3">
                                相关记者列表
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left:20px">
                            <table class="tablehead">
                            <tr>
                                <td>
                                <img src="/images/add.gif" border="0" style=" vertical-align:bottom" />&nbsp;<asp:LinkButton ID="btnLink" runat="server" class="bigfont" Text="添加记者" OnClientClick="WinOpen();return false;" />
                                </td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <%--<cc4:MyGridView ID="dgList" DataKeyNames="ReporterID" runat="server" OnRowDataBound="dgList_RowDataBound">
                                </cc4:MyGridView>--%>
                                <ComponentArt:Grid ID="dgList" PreHeaderClientTemplateId="PreHeaderTemplate"
                                    PostFooterClientTemplateId="PostFooterTemplate" DataAreaCssClass="GridData" EnableViewState="false" ClientTarget="Uplevel"
                                    ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Buttons"
                                    PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
                                    PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
                                    SliderPopupOffsetX="50" ImagesBaseUrl="../images/gridview/" PagerImagesFolderUrl="../images/gridview/pager/"
                                    TreeLineImagesFolderUrl="../images/grid/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
                                    Width="100%" Height="100%" runat="server" EditOnClickSelectedItem="false">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="ReporterID" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                                            SortImageHeight="19">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="ReporterID" Visible="false" />
                                                <ComponentArt:GridColumn DataField="ReporterName" Visible="false" />                                                
                                                <ComponentArt:GridColumn HeadingText="姓名" DataCellClientTemplateId="showTemplate" Align="Center" />
                                                <ComponentArt:GridColumn HeadingText="所属媒体" DataField="medianame" Align="Center" />
                                                <ComponentArt:GridColumn HeadingText="性别" DataField="sex" Align="Center" />
                                                <ComponentArt:GridColumn HeadingText="职务" DataField="ReporterPosition" Align="Center" />
                                                <ComponentArt:GridColumn HeadingText="负责领域" DataField="responsibledomain" Align="Center" />                                                
                                                <ComponentArt:GridColumn HeadingText="手机" DataField="mobile" Align="Center" />
                                                <ComponentArt:GridColumn HeadingText="固话" DataField="tel" Align="Center" />
                                                <ComponentArt:GridColumn HeadingText="邮箱" DataField="email" Align="Center" /> 
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates> 
                            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);background-repeat: repeat-x;">
                                            <span style="color: White; font-weight: bold;">相关记者列表</span>
                                        </td>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; background-image: url(../images/gridview/grid_postfooter_01.jpg);background-repeat: repeat-x;"></td>
                                        <td style="background-image: url(../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;</td>
                                        <td style="width: 10px; background-image: url(../images/gridview/grid_postfooter_03.jpg);background-repeat: repeat-x;"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate> 
                            <ComponentArt:ClientTemplate ID="showTemplate">
                                 ## showRep(DataItem.GetMember('ReporterID').Value,DataItem.GetMember('ReporterName').Value); ##                                
                            </ComponentArt:ClientTemplate>                            
                        </ClientTemplates>
                                </ComponentArt:Grid>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
