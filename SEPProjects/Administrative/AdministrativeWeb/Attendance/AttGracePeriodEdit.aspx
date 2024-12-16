<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttGracePeriodEdit.aspx.cs" Inherits="AdministrativeWeb.Attendance.AttGracePeriodEdit" 
    MasterPageFile="~/Default.Master" %>

<%@ OutputCache Duration="1" Location="none" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/treeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendarStyle.css" rel="stylesheet" type="text/css" />
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
                                        </ComponentArt:TreeView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td valign="top">
                                <asp:UpdatePanel ID="upUserInfo" runat="server" RenderMode="Inline" Visible="false">
                                    <ContentTemplate>
                                        <table width="100%" border="0" cellspacing="10" cellpadding="0" backgsround="../images/t1_28.jpg"
                                            style="margin: 10px 0 0 20px;">
                                            <tr>
                                                <td>
                                                    员工姓名：<asp:Label ID="labUserName" runat="server"></asp:Label><br />
                                                    <asp:HiddenField ID="hidUserid" runat="server" />
                                                    <asp:HiddenField ID="hidUserCardID" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    员工编号：<asp:Label ID="labUserCode" runat="server"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    员工账号：<asp:Label ID="labUserITCode" runat="server"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    员工电话：<asp:Label ID="labUserTel" runat="server"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    所属部门：<asp:Label ID="labUserDept" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="500" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="85">
                                                                开通截止时间：
                                                            </td>
                                                            <td align="left">
                                                                <ComponentArt:Calendar ID="txtEndTime" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd HH:mm"
                                                                    ControlType="Picker" PickerCssClass="picker">
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="500" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="85">
                                                                备注：
                                                            </td>
                                                            <td width="">
                                                                <asp:TextBox TextMode="MultiLine" Width="60%" Rows="3" ID="txtRemark" runat="server"></asp:TextBox>
                                                                <font color="red">*</font>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="备注信息为必填项" ControlToValidate="txtRemark"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ImageUrl="../images/t2_03-20.jpg" Width="56" ID="btnSave" OnClick="btnSave_Click"
                                                        runat="server" Height="24" />&nbsp;
                                                    <asp:ImageButton ImageUrl="../images/t2_03-22.jpg" Width="56" ID="btnBack" OnClick="btnBack_Click"
                                                        runat="server" Height="24" CausesValidation="false"/>
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