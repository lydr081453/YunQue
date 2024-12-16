<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationManageEdit.aspx.cs"
    Inherits="AdministrativeWeb.Attendance.OperationManageEdit" MasterPageFile="~/Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
      //<![CDATA[
      function userTreeView_onNodeCheckChange(sender, eventArgs)
      {
        var status = "Unchecked";
        if (eventArgs.get_node().get_checked()) status = "Checked";
        document.getElementById("spnUserInfo").innerHTML += status + ": '" + eventArgs.get_node().get_text() + "'";
      }
      
      function ManagerClick() {
        var win = window.open('EmployeeList.aspx', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
      }
      //]]>
    </script>

    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list_2">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="200" valign="top" background="../images/t1_28.jpg" style="background-repeat: repeat-x;">
                                <!-- 人力节点树 -->
                                <asp:UpdatePanel ID="upTreeView" runat="server" RenderMode="Block">
                                    <ContentTemplate>
                                        <ComponentArt:TreeView ID="userTreeView" Height="500" Width="220" DragAndDropEnabled="false"
                                            NodeEditingEnabled="false" KeyboardEnabled="true" CssClass="TreeView" NodeCssClass="TreeNode"
                                            SelectedNodeCssClass="SelectedTreeNode" HoverNodeCssClass="HoverTreeNode" NodeEditCssClass="NodeEdit"
                                            LineImageWidth="19" LineImageHeight="20" DefaultImageWidth="16" DefaultImageHeight="16"
                                            ItemSpacing="0" ImagesBaseUrl="images/" NodeLabelPadding="3" ShowLines="true"
                                            LineImagesFolderUrl="../images/lines/" EnableViewState="true" runat="server">
                                            <ClientEvents>
                                                <NodeCheckChange EventHandler="userTreeView_onNodeCheckChange" />
                                            </ClientEvents>
                                        </ComponentArt:TreeView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td valign="top">
                                <asp:UpdatePanel ID="upUserInfo" runat="server" RenderMode="Inline">
                                    <ContentTemplate>
                                        <table width="100%" border="0" cellspacing="10" cellpadding="0" backgsround="../images/t1_28.jpg"
                                            style="margin: 10px 0 0 20px;">
                                            <tr>
                                                <td>
                                                    <table width="500" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="60">
                                                                上级主管：
                                                            </td>
                                                            <td width="">
                                                                <asp:TextBox ID="txtGoWorkTime" runat="server" Width="120px"></asp:TextBox>&nbsp;
                                                                <input type="button" id="btnApplicant" onclick="return ManagerClick();" class="widebuttons"
                                                                    value="  选择  " /><input type="hidden" id="hidApplicantID" runat="server" />
                                                                <font color="red">*</font>
                                                                <input type="hidden" id="hidApplicantUserID" runat="server" />
                                                                <input type="hidden" id="hidApplicantUserCode" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>   
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="500" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="60">
                                                                备注：
                                                            </td>
                                                            <td width="">
                                                                <asp:TextBox TextMode="MultiLine" Width="60%" Rows="3" ID="txtRemark" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ImageUrl="../images/t2_03-20.jpg" Width="56" ID="btnSave" OnClick="btnSave_Click"
                                                        runat="server" Height="24" />&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
