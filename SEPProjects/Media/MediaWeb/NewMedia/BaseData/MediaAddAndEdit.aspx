<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="MediaAddAndEdit.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.MediaAddAndEdit"
MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register TagPrefix="uc" TagName="PlaneMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/PlaneMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="DABMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/DABMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="TvMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/TvMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="WebMediaAddAndEdit" Src="~/NewMedia/BaseData/skins/WebMediaAddAndEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/NewMedia/BaseData/skins/Experience.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<link href="../css/gridStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
  function   WinOpen()   
      {   
       Mid = document.getElementById("<% =hidMediaId.ClientID%>");
       //window.open("MediaLinkReporterList.aspx?alert=1&Mid="+Mid.value,"选择记者","<%= ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
       window.open("ReporterAddAndEdit.aspx?Operate=ADD&alert=1&Mid=" + Mid.value, "选择记者", "<%= ESP.MediaLinq.Utilities.Global.OpenClass.Common %>");    
      }
         function returnurl()
        {
                var hidurl=document.getElementById("<% = hidUrl.ClientID %>");
    window.location = hidurl.value;
}
function showRep(ID, Name) {
    var ret = "<a onclick=\"window.open('ReporterDisplay.aspx?alert=1&Rid=" + ID + "','','height=600, width=1000, top=50, left=50, toolbar=no, menubar=no, scrollbars=false, resizable=no,location=no, status=no')\";>"+Name+"</a>";

    return ret;
}
function editRep(ID) {
    var mid = document.getElementById("<% =hidMediaId.ClientID%>");
    var page = "height=600, width=1000, top=50, left=50, toolbar=no, menubar=no, scrollbars=false, resizable=no,location=no, status=no";
    var ret = "<a onclick=\"window.open('ReporterAddAndEdit.aspx?alert=1&Operate=EDIT&Rid="+ ID+"&Mid="+mid.value+"','','"+ page+"');\" ><img src='/images/edit.gif' /></a>";
    ret += "<a href='MediaAddAndEdit.aspx?Operate=DELReporter&Rid=" + ID + "&Mid=" + mid.value + "' onclick= \"return confirm( '真的要删除吗?');\" ><img src='/images/disable.gif' /></a>";
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
                            <asp:Panel ID="panelMediaAddAndEdit" runat="server" Height="0px">
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: right">
                <asp:Button ID="btnOk" runat="server" CssClass="widebuttons" Text="保存" OnClick="btnOk_Click"
                    OnClientClick="return check();" />
                <asp:Button ID="btnSubmit" runat="server" CssClass="widebuttons" Text="保存" OnClick="btnSubmit_Click"
                    OnClientClick="return check();" Visible="false" /> <%--原提交按钮--%>
                <input type="reset" class="widebuttons" value="重置" />
                <%--<input type="button" value="返回" onclick="returnurl();" class="widebuttons" />--%>
                <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
            <asp:HiddenField ID="hidBackUrl" runat="server" />
            <asp:HiddenField ID="hidMid" runat="server" />
        </tr>
        <tr>
            <td style="height:25px" />
        </tr>
       <%-- <tr>
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
                               <%-- <ComponentArt:Grid ID="dgList" PreHeaderClientTemplateId="PreHeaderTemplate"
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
                                                <ComponentArt:GridColumn HeadingText="操作" DataCellClientTemplateId="editTemplate" Align="Center" />
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
                            <ComponentArt:ClientTemplate ID="editTemplate">
                                 ## editRep(DataItem.GetMember('ReporterID').Value); ##                                
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                                </ComponentArt:Grid>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>--%>
    </table>
</asp:Content>

