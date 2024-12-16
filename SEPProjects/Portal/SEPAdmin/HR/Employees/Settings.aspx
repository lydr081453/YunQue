<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="SEPAdmin.HR.Employees.Settings"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../../public/CutImage/css/main.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../../public/CutImage/js/jquery1.2.6.pack.js"></script>
    <script type="text/javascript" src="../../public/CutImage/js/ui.core.packed.js"></script>
    <script type="text/javascript" src="../../public/CutImage/js/ui.draggable.packed.js"></script>
    <script type="text/javascript" src="../../public/CutImage/js/CutPic.js"></script>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 10px 0 10px 0;">
        <tr>
            <td class="nav">
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="title">
                            设置
                        </td>
                    </tr>
                </table>
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="30%" valign="top" class="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="40" align="right">
                                        基本资料
                                    </td>
                                </tr>
                                <tr>
                                    <td height="40" align="right">
                                        <%--<a href="/Account/IM.aspx">绑定设置</a>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="70%" valign="top" class="right">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="40" colspan="2" class="btn">
                                        <a href="/Account/Password.aspx">修改密码</a> <em>|</em> 头像 <em>|</em>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                                                        <tr>
                                                            <td colspan="2" style="font-size: 10pt; padding: 5px 10px 0 10px; font-weight: bold;">
                                                                上传头像:
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="padding: 5px 10px 0 10px;">
                                                                <input type="file" id="btnImage" runat="server" style="height: 28px;" onchange="headerInfoChange(this);" />
                                                                <asp:Button ID="uploadImage" runat="server" Text="上传头像" OnClick="uploadImage_Click"
                                                                    CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%" style="padding: 5px 10px 0 10px;">
                                                                <div id="Step2Container">
                                                                    <div class="uploadtooltip">
                                                                        您可以拖动照片以裁剪满意的头像</div>
                                                                    <div id="Canvas" class="uploaddiv">
                                                                        <div id="ImageDragContainer">
                                                                            <asp:Image ID="ImageDrag" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg"
                                                                                CssClass="imagePhoto" ToolTip="" />
                                                                        </div>
                                                                        <div id="IconContainer">
                                                                            <asp:Image ID="ImageIcon" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg"
                                                                                CssClass="imagePhoto" ToolTip="" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="uploaddiv">
                                                                        <table>
                                                                            <tr>
                                                                                <td id="Min">
                                                                                    <img alt="缩小" src="../../public/CutImage/image/_c.gif" onmouseover="this.src='../../public/CutImage/image/_c.gif';"
                                                                                        onmouseout="this.src='../../public/CutImage/image/_h.gif';" id="moresmall" class="smallbig" />
                                                                                </td>
                                                                                <td>
                                                                                    <div id="bar">
                                                                                        <div class="child">
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td id="Max">
                                                                                    <img alt="放大" src="../../public/CutImage/image/c.gif" onmouseover="this.src='../../public/CutImage/image/c.gif';"
                                                                                        onmouseout="this.src='../../public/CutImage/image/h.gif';" id="morebig" class="smallbig" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <div style="display: none">
                                                                        <asp:TextBox ID="txt_width" runat="server" Text="1"></asp:TextBox><br />
                                                                        <asp:TextBox ID="txt_height" runat="server" Text="1"></asp:TextBox><br />
                                                                        <asp:TextBox ID="txt_top" runat="server" Text="82"></asp:TextBox><br />
                                                                        <asp:TextBox ID="txt_left" runat="server" Text="73"></asp:TextBox><br />
                                                                        <asp:TextBox ID="txt_DropWidth" runat="server" Text="120"></asp:TextBox><br />
                                                                        <asp:TextBox ID="txt_DropHeight" runat="server" Text="120"></asp:TextBox><br />
                                                                        <asp:TextBox ID="txt_Zoom" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td style="padding: 5px 0 0 0;">
                                                                <div class="photocontainer">
                                                                    <asp:Image ID="imgphoto" runat="server" ImageUrl="../../public/CutImage/image/man.GIF" />
                                                                </div>
                                                                <div class="uploaddiv">
                                                                    <asp:Button ID="btn_Image" runat="server" Text="保存头像" OnClick="btn_Image_Click" Visible="false" CausesValidation="false"/>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>
