<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeBehind="ProvinceList.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.ProvinceList" %>

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
                    </td>
                    <td width="35%">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text=" 检索 " />&nbsp;&nbsp;<asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" Text=" 添加 " />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <ComponentArt:Grid ID="grdProvince" PreHeaderClientTemplateId="PreHeaderTemplate" OnItemCommand="grdProvince_ItemCommond"
        PostFooterClientTemplateId="PostFooterTemplate" DataAreaCssClass="GridData" EnableViewState="true"
        ShowHeader="false" FooterCssClass="GridFooter" PageSize="20" PagerStyle="Buttons"
        PagerTextCssClass="GridFooterText" PagerButtonWidth="44" PagerButtonHeight="26"
        PagerButtonHoverEnabled="true" SliderHeight="26" SliderWidth="150" SliderGripWidth="9"
        SliderPopupOffsetX="50" ImagesBaseUrl="../images/gridview/" PagerImagesFolderUrl="../images/gridview/pager/"
        TreeLineImagesFolderUrl="../images/grid/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11"
        Width="100%" Height="100%" runat="server">
        <Levels>
            <ComponentArt:GridLevel DataKeyField="city_id" ShowTableHeading="false" TableHeadingCssClass="GridHeader"
                RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="10"
                SortImageHeight="19">
                <Columns>
                    <ComponentArt:GridColumn DataField="Province_ID" Visible="false" />
                    <ComponentArt:GridColumn HeadingText="省份名称" DataField="Province_Name" Align="Center" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>
        <ClientTemplates>
            <ComponentArt:ClientTemplate ID="EditTemplate">
                <a href="ProvinceEdit.aspx?pid=## DataItem.GetMember('Province_ID').Value ##">
                    <img src="../../images/edit.gif" alt="编辑" /></a>
            </ComponentArt:ClientTemplate>
            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);">
                        </td>
                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);
                            background-repeat: repeat-x;">
                            <span style="color: White; font-weight: bold;">省份列表</span>
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
        <ServerTemplates>
            <ComponentArt:GridServerTemplate ID="DeleteTemplate">
                <Template>
                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="删除" CommandName="deleteprovince" CommandArgument='<%# Eval("Province_ID") %>' ImageUrl="../../images/disable.gif" />
                </Template>
            </ComponentArt:GridServerTemplate>
        </ServerTemplates>
    </ComponentArt:Grid>
</asp:Content>
