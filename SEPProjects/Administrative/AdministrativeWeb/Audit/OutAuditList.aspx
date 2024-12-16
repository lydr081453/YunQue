<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutAuditList.aspx.cs" Inherits="AdministrativeWeb.Audit.OutAuditList" MasterPageFile="~/Default.Master" %>
<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/gridStyle2.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />

    <script language="javascript">
        function checkedAll() {
            for (var i = 0; i < document.getElementsByName("chkAudit").length; i++) {
                var e = document.getElementsByName("chkAudit")[i];
                e.checked = document.getElementById("chkAll").checked;
            }
        }

        function gettype(type) {
            var sum = "";
            if (type == "8") {
                sum = "<span>外出</span>";
            }
            else if (type == "10") {
                sum = "<span>其他</span>";
            }
            return sum;
        }

        function getUrl(id, userid, type) {
            var sum = "<a href='OutMatterEdit.aspx?matterid=" + id + "&userid=" + userid + "&tabtype=" + type + "'><img src='../images/Audit.gif' /></a>";
            return sum;
        }
        
        function submitMatter() {
            var hid = document.getElementById("<%= hidMatter.ClientID %>");
            hid.value = "";
            var boxes = document.getElementsByName("chkAudit");
            for (var i = 0; i < boxes.length; i++) {
                //   var e = document.getElementsByName("ctl00$ContentPlaceHolder1$ctl00_ContentPlaceHolder1_GridNoNeed_0_1_0$chkAudit")[i];
                var e = boxes[i];
                if (e.checked)
                    hid.value += e.value + ",";
            }
            if (hid.value == "") {
                alert("请选择要审批的事由！");
                return false;
            }
            else {
                hid.value = hid.value.substring(0, hid.value.length - 1);
                return confirm("确认审批通过吗？");
            }
        }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
          <td>
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                      <td width="17">
                          <img src="../images/t2_03.jpg" width="21" height="20" />
                      </td>
                      <td align="left">
                          <strong>搜索 </strong>
                      </td>
                  </tr>
              </table>
              <table width="100%" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                      <td>
                          <asp:DropDownList ID="drpYear" runat="server">
                          </asp:DropDownList>
                          年<asp:DropDownList ID="drpMonth" runat="server">
                          </asp:DropDownList>
                          月&nbsp;&nbsp;
                      </td>
                      <td align="left">
                          <br />
                          <asp:ImageButton ImageUrl="../images/t2_03-07.jpg" Width="56" Height="24" ID="btnSearch"
                              runat="server" OnClick="btnSearch_Click" />
                      </td>
                  </tr>
              </table>
            <br />
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
              <tr>
                <td class="td">
                    <ComponentArt:Grid ID="GridNoNeed"                       
                        DataAreaCssClass="GridData" 
                        CssClass="Grid"
                        EnableViewState="true" 
                        EditOnClickSelectedItem="false"
                        ShowHeader="true"
                        FooterCssClass="GridFooter" 
                        FooterHeight="40"
                        HeaderHeight="30"
                        HeaderCssClass="GridHeader"
                        GroupingNotificationTextCssClass="GridHeaderText"
                        GroupingNotificationText="未审批(外出/其他)事由列表"
                        PagerInfoClientTemplateId="ClientTemplate3"                        
                        PagerStyle="Numbered"
                        PagerTextCssClass="GridFooterText"
                        PagerInfoPosition="BottomLeft"
                        PagerPosition="BottomRight"
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
                                DataKeyField="ID"                               
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
                                    <ComponentArt:GridColumn DataField="ID" Visible="false"  /> 
                                    <ComponentArt:GridColumn DataField="UserID" Visible="false"  /> 
                                    <ComponentArt:GridColumn DataField="mattertype" Visible="false"  />
                                    <ComponentArt:GridColumn HeadingText="选择" DataCellClientTemplateId="checkTemplate"  Width="50" Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="姓名" DataCellServerTemplateId="UserNameTemplate" Width="70" Align="Center"/>                                   
                                    <ComponentArt:GridColumn HeadingText="开始时间" DataField="begintime" Align="Center" />                                    
                                    <ComponentArt:GridColumn HeadingText="结束时间" DataField="endtime"  Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="事由类型" DataCellClientTemplateId="typeTemplate" Align="Center" Width="70" /> 
                                    <ComponentArt:GridColumn HeadingText="事由" DataField="mattercontent" Align="Center" />                                                                        
                                    <ComponentArt:GridColumn HeadingText="编辑" DataCellClientTemplateId="auditTemplate" Width="50" Align="Center"/>
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>                                                                  
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="PreHeaderTemplate">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);background-repeat: repeat-x;">
                                            <span style="color: White; font-weight: bold;">未审批外出\其他事由列表</span>
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
                            <ComponentArt:ClientTemplate ID="checkTemplate">                        
                        <input type="checkbox" id="chkAudit" name="chkAudit" value="## DataItem.GetMember('ID').Value ##" />                        
                        </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="ClientTemplate3" >
                            <table border="0" cellpadding="0"  cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left"><input type="checkbox" id="chkAll" onclick="checkedAll();"   />全选</td>                                     
                                    </tr>
                                </table>    
                            </ComponentArt:ClientTemplate>                            
                            <ComponentArt:ClientTemplate ID="auditTemplate">
                                ##getUrl(DataItem.GetMember("ID").Value,DataItem.GetMember("UserID").Value,DataItem.GetMember("mattertype").Value)##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="typeTemplate">
                                ## gettype(DataItem.GetMember("mattertype").Value) ##
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        
                        <ServerTemplates>
                            <ComponentArt:GridServerTemplate ID="UserNameTemplate">
                                <Template>
                                    <%#GetUserName(Container.DataItem["UserID"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                        </ServerTemplates>
                    </ComponentArt:Grid>
                </td>
              </tr>
                <tr>
                    <td>
                        <br />
                        <asp:ImageButton ID="btnAudit" runat="server" OnClientClick="return submitMatter(DataItem);"
                            OnClick="btnAudit_Click" ImageUrl="~/images/apppass.jpg" /><input type="hidden" id="hidMatter"
                                value="" runat="server" />
                    </td>
                </tr>
              </table>
              <br />
              <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
              <tr>
                <td class="td">
                    <ComponentArt:Grid ID="GridPassed" 
                        DataAreaCssClass="GridData" 
                        CssClass="Grid"
                        EnableViewState="true" 
                        ShowHeader="true"
                        FooterCssClass="GridFooter"     
                        GroupingNotificationTextCssClass="GridHeaderText"    
                        GroupingNotificationText="已审批(外出/其他)事由列表"                
                        PagerStyle="Numbered" 
                        HeaderHeight="30"
                        HeaderCssClass="GridHeader"
                        IndentCellWidth="22"
                        PagerTextCssClass="GridFooterText"
                        PagerButtonWidth="44" 
                        PagerButtonHeight="26" 
                        PagerButtonHoverEnabled="true" 
                        SliderHeight="26"
                        SliderWidth="150" 
                        SliderGripWidth="9" 
                        SliderPopupOffsetX="50" 
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
                                    <ComponentArt:GridColumn DataField="mattertype" Visible="false"  />
                                    <ComponentArt:GridColumn HeadingText="姓名" DataCellServerTemplateId="UserNameTemplate2" Width="70" Align="Center"/>                                   
                                    <ComponentArt:GridColumn HeadingText="开始时间" DataField="begintime" Align="Center" />                                    
                                    <ComponentArt:GridColumn HeadingText="结束时间" DataField="endtime"  Align="Center" />
                                    <ComponentArt:GridColumn HeadingText="事由类型" DataCellClientTemplateId="typeTemplate2" Align="Center" Width="70" /> 
                                    <ComponentArt:GridColumn HeadingText="事由" DataField="mattercontent" Align="Center" />                                                                        
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>                       
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_01.jpg);"></td>
                                        <td style="height: 34px; background-image: url(../images/gridview/grid_preheader_02.jpg);background-repeat: repeat-x;">
                                            <span style="color: White; font-weight: bold;">已审批外出事由列表</span>
                                        </td>
                                        <td style="width: 10px; height: 34px; background-image: url(../images/gridview/grid_preheader_03.jpg);"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="ClientTemplate2">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_01.jpg);background-repeat: repeat-x;"></td>
                                        <td style="background-image: url(../../images/gridview/grid_postfooter_02.jpg); background-repeat: repeat-x;">&nbsp;</td>
                                        <td style="width: 10px; background-image: url(../../images/gridview/grid_postfooter_03.jpg);background-repeat: repeat-x;"></td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>                           
                            <ComponentArt:ClientTemplate ID="typeTemplate2">
                                ## gettype(DataItem.GetMember("mattertype").Value) ##
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        <ServerTemplates>
                            <ComponentArt:GridServerTemplate ID="UserNameTemplate2">
                                <Template>
                                    <%#GetUserName(Container.DataItem["UserID"].ToString())%>
                                </Template>
                            </ComponentArt:GridServerTemplate>
                        </ServerTemplates>
                    </ComponentArt:Grid>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>      
</asp:Content>

