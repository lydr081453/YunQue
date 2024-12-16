<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardNoEdit.aspx.cs" Inherits="AdministrativeWeb.Attendance.CardNoEdit" 
    MasterPageFile="~/Default.Master"%>
<%@ OutputCache Duration="1" Location="none" %>   
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />
    <link href="../css/treeStyle.css" rel="stylesheet" type="text/css" />
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
                        <td>员工姓名：<asp:Label ID="labUserName" runat="server"></asp:Label><br />
                            <asp:HiddenField ID="hidUserid" runat="server" />
                            <asp:HiddenField ID="hidUserCardID" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td>员工编号：<asp:Label ID="labUserCode" runat="server"></asp:Label><br /></td>
                      </tr>
                      <tr>
                        <td>公司电话：<asp:Label ID="labUserTel" runat="server"></asp:Label><br /></td>
                      </tr>
                      <tr>
                        <td>所属部门：<asp:Label ID="labUserDept" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidDeptId" runat="server" />
                        </td>
                      </tr>
                        <tr>
                            <td>
                                <table width="600" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="60">
                                            门 卡 号：
                                        </td>
                                        <td valign="top" >
                                            <asp:TextBox ID="txtCardno" runat="server" Width="120" Height="24" MaxLength="50" ></asp:TextBox>
                                           <%-- <asp:ImageButton ImageUrl="../images/getnewcard.jpg" Width="63" ID="btnGetNewCard" OnClick="btnGetNewCard_Click"
                                                runat="server" Height="24" ToolTip="获取新卡" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="600" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="60">
                                            备    注：
                                        </td>
                                        <td width="">
                                            <asp:TextBox ID="txtDesc" runat="server" Width="300" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="600" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="60">
                                            历史记录：
                                        </td>
                                        <td width="">
                                            <asp:Label ID="labHistoryCard" runat="server" Width="540"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                      <tr>
                        <td>
                            <asp:ImageButton ImageUrl="../images/enable.jpg" Width="45" ID="btnEnable" OnClick="btnEnable_Click" OnClientClick="return confirm('您确认要启用该门卡？');"
                                runat="server" Height="24" ToolTip="启用门卡" Visible="false" />
                            <asp:ImageButton ImageUrl="../images/unenable.jpg" Width="45" ID="btnUnEnable" OnClick="btnUnEnable_Click" OnClientClick="return confirm('您确认要停用该门卡？');"
                                runat="server" Height="24" ToolTip="停用门卡" Visible="false" />
                            <asp:ImageButton ImageUrl="../images/exchange.jpg" Width="45" ID="btnExchange" OnClick="btnExchange_Click" OnClientClick="return confirm('您确认要更换该门卡？');"
                                runat="server" Height="24" ToolTip="更换门卡" Visible="false" />
                            <asp:ImageButton ImageUrl="../images/blankout.jpg" Width="45" ID="btnBlankOut" OnClick="btnBlankOut_Click" OnClientClick="return confirm('您确认要作废该门卡？');"
                                runat="server" Height="24" ToolTip="作废门卡" Visible="false" />
                            <asp:LinkButton ID="btnBack" Text="<img src='../images/t2_03-22.jpg'/>" runat="server"
                                Width="56" Height="24" OnClick="btnBack_Click" CausesValidation="false" ToolTip="返回" />
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
