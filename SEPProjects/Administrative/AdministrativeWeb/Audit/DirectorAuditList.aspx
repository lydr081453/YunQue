<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectorAuditList.aspx.cs" Inherits="AdministrativeWeb.Audit.DirectorAuditList" MasterPageFile="~/Default.Master"%>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
          <td>
            <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="17"><img src="../images/t2_03.jpg" width="21" height="20" /></td>
                        <td align="left"><strong>搜索 </strong></td>
                    </tr>
                </table>
                <table width="60%" border="0" cellspacing="3" cellpadding="0">
                    <tr>
                        <td >
                            申请人：
                            <asp:TextBox runat="server" ID="txtApp" Width="128px"></asp:TextBox>
                        </td>                      
                        <td align="left">                            
                            <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" 
                                ID="btnSearch" runat="server" onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
              <tr>
                <td class="td">
                    <ComponentArt:Grid ID="Grid1"                    
                        DataAreaCssClass="GridData" 
                        CssClass="Grid"
                        EnableViewState="true" 
                        EditOnClickSelectedItem="false"
                        ShowHeader="true"
                        HeaderHeight="30"
                        HeaderCssClass="GridHeader"
                        GroupingNotificationText="待审批事由信息列表"
                        GroupingNotificationTextCssClass="GridHeaderText"
                        FooterCssClass="GridFooter" 
                        PageSize="20"
                        PagerStyle="Slider" 
                        PagerTextCssClass="GridFooterText"
                        PagerButtonWidth="41"
                        PagerButtonHeight="22" 
                        PagerButtonHoverEnabled="true" 
                        SliderHeight="20"
                        SliderWidth="150"
                        SliderGripWidth="9"
                        SliderPopupOffsetX="35"  
                        ImagesBaseUrl="../images/gridview2/"
                        PagerImagesFolderUrl="../images/gridview2/pager/" 
                        TreeLineImagesFolderUrl="../images/gridview2/lines/"
		                TreeLineImageWidth="11"
		                TreeLineImageHeight="11"
		                PreExpandOnGroup="true"
                        Width="100%" Height="100%"
                        runat="server">
                        <Levels>
                            <ComponentArt:GridLevel   
                                DataKeyField="UserID"                               
                                ShowTableHeading="false"
                                TableHeadingCssClass="GridHeader" 
                                RowCssClass="Row" 
                                ColumnReorderIndicatorImageUrl="reorder.gif"
                                DataCellCssClass="DataCell" 
                                HeadingCellCssClass="HeadingCell" 
                                HeadingCellHoverCssClass="HeadingCellHover"
                                HeadingCellActiveCssClass="HeadingCellActive" 
                                HeadingRowCssClass="HeadingRow"
                                HeadingTextCssClass="HeadingCellText" 
                                SelectedRowCssClass="SelectedRow" 
                                SortedDataCellCssClass="SortedDataCell"
                                SortAscendingImageUrl="asc.gif" 
                                SortDescendingImageUrl="desc.gif" 
                                SortImageWidth="9"
                                SortImageHeight="5">
                                <Columns>                                    
                                    <ComponentArt:GridColumn DataField="UserID" Visible="false"  />
                                    <ComponentArt:GridColumn DataField="fullnamecn" HeadingText="姓名" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="公司" DataField="companyName" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="团队" DataField="departmentName"  Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="组别" DataField="groupName" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="职位" DataField="joinjob"  Align="Center" />                                    
                                    <ComponentArt:GridColumn HeadingText="外出审批" DataCellClientTemplateId="auditTemplate" Width="100" Align="Center"/>
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>                                           
                        <ClientTemplates>
                             <ComponentArt:ClientTemplate ID="ClientTemplate3" >
                            <table border="0" cellpadding="0"  cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left"><input type="checkbox" id="chkAll" onclick="checkedAll();"   />全选</td>                                     
                                    </tr>
                                </table>    
                            </ComponentArt:ClientTemplate> 
                            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);background-repeat: repeat-x;">
                                            <span style="color: White; font-weight: bold;">月审核人员列表</span>
                                        </td>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="PostFooterTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg);background-repeat: repeat-x;"></td>
                                        <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;</td>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg);background-repeat: repeat-x;"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate> 
                            <ComponentArt:ClientTemplate ID="auditTemplate">
                                <a href="OutAuditList.aspx?userid=## DataItem.GetMember('UserID').Value ##">
                                    <img src="../images/Audit.gif" /></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>    
</asp:Content>
