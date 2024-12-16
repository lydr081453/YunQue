<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="CityList.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.CityList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <link href="../css/demos.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <asp:UpdatePanel ID="Panel" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td width="25%">
                        国家:
                    </td>
                    <td width="35%">
                        <div style="margin-bottom: 15px;">
                            <ComponentArt:ComboBox ID="ddlCountry" runat="Server" Width="192" Height="20" AutoHighlight="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_OnSelectedIndexChanged"
                                AutoComplete="true" AutoFilter="true" DataTextField="CountryName" DataValueField="CountryID"
                                TextBoxCssClass="txt" DropHoverImageUrl="../images/ddn-hover.png" DropImageUrl="../images/ddn.png"
                                DropDownResizingMode="bottom" DropDownWidth="190" DropDownHeight="300" DropDownCssClass="ddn"
                                DropDownContentCssClass="ddn-con">
                                <DropDownFooter>
                                    <div class="ddn-ftr">
                                    </div>
                                </DropDownFooter>
                            </ComponentArt:ComboBox>
                        </div>
                    </td>
                    <td width="25%">
                        省份:
                    </td>
                    <td width="35%">
                        <ComponentArt:ComboBox ID="ddlProvince" runat="Server" Width="192" Height="20" AutoHighlight="false"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_OnSelectedIndexChanged"
                            AutoComplete="true" AutoFilter="true" DataTextField="Province_Name" DataValueField="Province_ID"
                            ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" CssClass="cmb" HoverCssClass="cmb-hover"
                            TextBoxCssClass="txt" DropHoverImageUrl="../images/ddn-hover.png" DropImageUrl="../images/ddn.png"
                            DropDownResizingMode="bottom" DropDownWidth="190" DropDownHeight="300" DropDownCssClass="ddn"
                            DropDownContentCssClass="ddn-con">
                            <DropDownFooter>
                                <div class="ddn-ftr">
                                </div>
                            </DropDownFooter>
                        </ComponentArt:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        城市:
                    </td>
                    <td colspan="3">
                        <ComponentArt:ComboBox ID="ddlCity" runat="Server" Width="192" Height="20" AutoHighlight="false"
                            AutoComplete="true" AutoFilter="true" DataTextField="City_Name" DataValueField="City_ID"
                            ItemCssClass="ddn-item" ItemHoverCssClass="ddn-item-hover" CssClass="cmb" HoverCssClass="cmb-hover"
                            TextBoxCssClass="txt" DropHoverImageUrl="../images/ddn-hover.png" DropImageUrl="../images/ddn.png"
                            DropDownResizingMode="bottom" DropDownWidth="190" DropDownHeight="300" DropDownCssClass="ddn"
                            DropDownContentCssClass="ddn-con">
                            <DropDownFooter>
                                <div class="ddn-ftr">
                                </div>
                            </DropDownFooter>
                        </ComponentArt:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" />&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text=" 新建 " OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <ComponentArt:Grid ID="grdCity" PreHeaderClientTemplateId="PreHeaderTemplate" PostFooterClientTemplateId="PostFooterTemplate"
        DataAreaCssClass="GridData" EnableViewState="true" ShowHeader="false" FooterCssClass="GridFooter"
        PageSize="20" PagerStyle="Buttons" PagerTextCssClass="GridFooterText" PagerButtonWidth="44"
        PagerButtonHeight="26" PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150"
        SliderGripWidth="9" SliderPopupOffsetX="50" ImagesBaseUrl="../images/gridview/"
        PagerImagesFolderUrl="../images/gridview/pager/" TreeLineImagesFolderUrl="../images/grid/lines/"
        TreeLineImageWidth="11" TreeLineImageHeight="11" Width="100%" Height="100%" runat="server">
        <Levels>
            <ComponentArt:GridLevel DataKeyField="city_id" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                SortImageHeight="19">
                <Columns>
                    <ComponentArt:GridColumn HeadingText="编辑" DataCellClientTemplateId="EditTemplate"
                        Align="Center" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="EditTemplate">
                <asp:HyperLink ID="hylEdit" runat="server" />
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);">
                        </td>
                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);
                            background-repeat: repeat-x;">
                            <span style="color: White; font-weight: bold;">城市列表</span>
                        </td>
                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);">
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg);
                            background-repeat: repeat-x;">
                        </td>
                        <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">
                            &nbsp;
                        </td>
                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg);
                            background-repeat: repeat-x;">
                        </td>
                    </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
    </ComponentArt:Grid>
</asp:Content>
