<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_ReporterContentsDisplay"
     Codebehind="ReporterContentsDisplay.aspx.cs" %>

<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
    <%-- function returnurl(url)
        {
            window.location = url;
        }--%>

    <script type="text/javascript">
  function PrintPage()
  {  
    window.print();
  }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%" border="0">
        <tr>
            <td align="right" colspan="4">
                <asp:Button ID="Button2" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons"
                    Text="打印" />&nbsp;
                <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                    class="widebuttons" />&nbsp;<asp:Button ID="Button3" runat="server" Text="返回" CssClass="widebuttons"
                        Visible="false" OnClick="btnBack_Click" />
        </tr>
    </table>
    <table style="width: 100%" border="1" class="tableForm">
        <tr>
            <td class="menusection-Packages" colspan="4">
                记者历史详细信息
            </td>
        </tr>
        <tr>
            <td class="menusection-Packages">
                当前版本号：
            </td>
            <td class="menusection-Packages" colspan="3">
                <asp:Label ID="labVersion" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="menusection-Packages" style="width: 20%">
                最后更新人：
            </td>
            <td class="menusection-Packages" style="width: 30%">
                <asp:Label ID="labLastModifyUser" runat="server"></asp:Label>
            </td>
            <td class="menusection-Packages" style="width: 20%">
                最后更新时间：
            </td>
            <td class="menusection-Packages" style="width: 30%">
                <asp:Label ID="labLastModifyDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                基本信息
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                姓名：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labName" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 20%">
                笔名：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:Label ID="labPenName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                所属媒体名称：
            </td>
            <td class="oddrow-l" style="width: 30%" colspan="3">
                <asp:LinkButton ID="lnkMediaName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                性别：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labSex" runat="server" />
            </td>
            <td class="oddrow">
                职务：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labreporterposition" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                负责领域：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labresponsibledomain" runat="server" />
            </td>
            <td class="oddrow">
                固话：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labOfficePhone" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                常用手机：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labUsualMobile" runat="server" />
            </td>
            <td class="oddrow">
                备用手机：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labBackupMobile" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                Msn 号码：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labMsn" runat="server"></asp:Label>
            </td>
            <td class="oddrow">
                QQ 号码：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labQq" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                其它通讯方式：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labOtherMessageSoftware" runat="server" MaxLength="50"></asp:Label>
            </td>
            <td class="oddrow">
                邮箱：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmailOne" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                备用邮箱1：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmailTwo" runat="server" MaxLength="50"></asp:Label>
            </td>
            <td class="oddrow">
                备用邮箱2：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labEmailThree" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="4">
                个人信息
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                生日：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labBirthday" runat="server" />
            </td>
            <td class="oddrow">
                身份证号：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labIdCard" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                籍贯：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labHometown" runat="server" />
            </td>
            <td class="oddrow">
                婚姻状况：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labMarriage" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                性格特点：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCharacter" runat="server" />
            </td>
            <td class="oddrow">
                兴趣爱好：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labHobby" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                主要作品：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labWriting" runat="server" />
            </td>
            <td class="oddrow">
                照片：
            </td>
            <td class="oddrow-l">
                <asp:Image ID="imgReporter" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                邮政编码：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labPostCord" runat="server" />
            </td>
            <td class="oddrow">
                家庭电话：
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labHomePhone" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                家庭地址：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labAddress" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                家庭成员：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labFamily" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                教育背景：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labEducation" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                职业经历：
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:Label ID="labWorkStory" runat="server" />
            </td>
        </tr>
        <asp:Panel ID="palPrivateInfo" runat="server">
            <tr>
                <td class="heading" colspan="4">
                    私密信息
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    开户银行：
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:Label ID="labbankname" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    开户姓名：
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labbankacountname" runat="server" />
                </td>
                <td class="oddrow">
                    账号：
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labbankcardcode" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    稿酬标准：
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labwritingfee" runat="server" />
                </td>
                <td class="oddrow">
                    付款方式：
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="labpaymentmode" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    合作情况：
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:Label ID="labcooperatecircs" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    备注：
                </td>
                <td class="oddrow-l" colspan="3">
                    <asp:Label ID="labPrivateRemark" runat="server" />
                </td>
            </tr>
        </asp:Panel>
    </table>
    <table style="width: 100%" border="0">
        <tr>
            <td align="right" colspan="4">
                <asp:Button ID="Button1" runat="server" OnClientClick="PrintPage();" CssClass="widebuttons"
                    Text="打印" />&nbsp;
                <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                    class="widebuttons" />&nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons"
                        Visible="false" OnClick="btnBack_Click" />
        </tr>
    </table>
</asp:Content>
