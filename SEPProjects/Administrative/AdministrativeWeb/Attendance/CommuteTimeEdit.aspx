<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommuteTimeEdit.aspx.cs" Inherits="AdministrativeWeb.CommuteTimeEdit" 
    MasterPageFile="~/Default.Master"%>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/combobox.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendar.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">

        function TreeView1_onNodeSelect(sender, eventArgs) {
            var val = eventArgs.get_node().get_value();
            if (val.indexOf(',') != -1) {
                var valflag = val.substring(val.indexOf(',') + 1);
                document.getElementById("hidApproveUserID").value = val.substring(0, val.indexOf(','));
                if (valflag == "0") {
                    cmbApprove.set_text(eventArgs.get_node().get_text());
                    cmbApprove.collapse();
                }
            }
        }
    </script>
    <div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list_2">
        <tr>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="200" valign="top" background="../images/t1_28.jpg" style="background-repeat:repeat-x;">
                <!-- 人力节点树 -->
                <asp:UpdatePanel ID="upTreeView" runat="server" RenderMode="Block">
                    <ContentTemplate>
                        <ComponentArt:TreeView ID="userTreeView" Height="500" Width="220" DragAndDropEnabled="false"
                            NodeEditingEnabled="false" KeyboardEnabled="true" CssClass="TreeView" NodeCssClass="TreeNode"
                            SelectedNodeCssClass="SelectedTreeNode" HoverNodeCssClass="HoverTreeNode" NodeEditCssClass="NodeEdit"
                            LineImageWidth="19" LineImageHeight="20" DefaultImageWidth="16" DefaultImageHeight="16"
                            ItemSpacing="0" ImagesBaseUrl="images/" NodeLabelPadding="3" ShowLines="true"
                            LineImagesFolderUrl="../images/lines/" EnableViewState="true" runat="server">
                        </ComponentArt:TreeView>
                    </ContentTemplate>
                </asp:UpdatePanel>
              </td>
              <td valign="top">
              <asp:UpdatePanel ID="upUserInfo" runat="server" RenderMode="Inline" Visible="false">
                <ContentTemplate>
                    <table width="100%" border="0" cellspacing="10" cellpadding="0" backgsround="../images/t1_28.jpg" style="margin:10px 0 0 20px;">
                      <tr>
                        <td>员工姓名：<asp:Label ID="labUserName" runat="server"/><br />
                        <asp:HiddenField ID="hidUserid" runat="server" />
                        <asp:HiddenField ID="hidUserCardID" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td>员工编号：<asp:Label ID="labUserCode" runat="server"/><br /></td>
                      </tr>
                      <tr>
                        <td>员工账号：<asp:Label ID="labUserITCode" runat="server"/><br /></td>
                      </tr>
                      <tr>
                        <td>员工电话：<asp:Label ID="labUserTel" runat="server"/><br /></td>
                      </tr>
                      <tr>
                        <td>所属部门：<asp:Label ID="labUserDept" runat="server"/></td>
                      </tr>
                        <tr>
                            <td>
                                <table width="500" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="80">
                                            上下班时间开始启用时间：
                                        </td>
                                        <td width="">
                                            <ComponentArt:Calendar ID="cldBeginDate" runat="server" PickerFormat="Custom"
                                                PickerCustomFormat="yyyy-MM-dd" ControlType="Picker" PickerCssClass="picker" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="500" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="80">
                                            上班时间：
                                        </td>
                                        <td width="">
                                            <asp:TextBox ID="txtGoWorkTime" runat="server" Width="120"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="500" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="80">
                                            下班时间：
                                        </td>
                                        <td width="">
                                            <asp:TextBox ID="txtOffWorkTime" runat="server" Width="120"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="500" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="80">
                                            考勤类型：
                                        </td>
                                        <td width="">
                                            <asp:DropDownList ID="drpAttendanceType" runat="server" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                       
                      <tr>
                        <td>
                          <asp:ImageButton ImageUrl="../images/t2_03-20.jpg" Width="56" ID="btnSave" 
                            onclick="btnSave_Click" runat="server" Height="24" />
                        </td>
                      </tr>
                      <tr>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                        <td>&nbsp;</td>
                      </tr>
                    </table>
                </ContentTemplate>
              </asp:UpdatePanel>
              </td>
            </tr>
          </table></td>
        </tr>
      </table>
    </div>
</asp:Content>
