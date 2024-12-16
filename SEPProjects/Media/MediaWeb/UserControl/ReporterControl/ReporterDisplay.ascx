<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_ReporterControl_ReporterDisplay" Codebehind="ReporterDisplay.ascx.cs" %>
<dl class="edit_tittle" style="width: 100%">
    <dt>记者详细信息</dt>
    <dt class="divline" style="width: 100%"></dt>
</dl>
<table cellpadding="0" border="0" width="100%">
    <tr>
        <td>
            <asp:Image runat="server" ID="imgPic" AlternateText="记者照片" Width="113" Height="139" />
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%;">
                        最后更新人：
                    </td>
                    <td style="width: 30%">
                        <asp:Label ID="labLastModifyUser" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        最后更新时间：
                    </td>
                    <td style="width: 30%">
                        <asp:Label ID="labLastModifyDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table style="width: 100%" border="1" class="tableForm">
                <tr>
                    <td colspan="4" class="heading">
                        基本信息：
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" style="width: 20%">
                        姓名：
                    </td>
                    <td style="width: 30%" class="oddrow-l">
                        <asp:Label ID="labName" runat="server"></asp:Label>
                    </td>
                    <td class="oddrow" style="width: 20%">
                        笔名：
                    </td>
                    <td style="width: 30%;" class="oddrow-l">
                        <asp:Label ID="labPenName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" style="width: 20%">
                        所属媒体名称：
                    </td>
                    <td colspan="3" class="oddrow-l">
                        <asp:Label ID="labMediaName" runat="server"></asp:Label>
                        <%--<asp:LinkButton ID="lnkMediaName" runat="server" />--%>
                        <%--<asp:Button ID="btnChangeMedia" runat="server" Text="变更所属媒体" CssClass="bigwidebuttons"
                    OnClick="btnChangeMedia_Click" />--%>
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
            </table>
            <table style="width: 100%" border="0">
                <tr>
                    <td height="25">
                    </td>
                </tr>
            </table>
            <table style="width: 100%" border="1" class="tableForm">
                <tr>
                    <td colspan="4" class="heading">
                        个人信息
                    </td>
                </tr>
                <tr>
                    <td class="oddrow" style="width: 20%">
                        生日：
                    </td>
                    <td style="width: 30%" class="oddrow-l">
                        <asp:Label ID="labBirthday" runat="server" />
                    </td>
                    <td class="oddrow" style="width: 20%">
                        身份证号：
                    </td>
                    <td style="width: 30%" class="oddrow-l">
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
                    <td class="oddrow-l" colspan="3">
                        <asp:Label ID="labWriting" runat="server" />
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
                    <td colspan="3" class="oddrow-l">
                        <asp:Label ID="labAddress" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        家庭成员：
                    </td>
                    <td colspan="3" class="oddrow-l">
                        <asp:Label ID="labFamily" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        教育背景：
                    </td>
                    <td colspan="3" class="oddrow-l">
                        <asp:Label ID="labEducation" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="oddrow">
                        职业经历：
                    </td>
                    <td colspan="3" class="oddrow-l">
                        <asp:Label ID="labWorkStory" runat="server" />
                    </td>
                </tr>
                <asp:Panel ID="palPrivateInfo" runat="server" Visible="false">
                    <tr>
                        <td colspan="4" style="height: 30px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="heading">
                            私密信息
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            开户银行：
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:Label ID="labbankname" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            开户人姓名：
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
                        <td colspan="3" class="oddrow-l">
                            <asp:Label ID="labcooperatecircs" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            备注：
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:Label ID="labPrivateRemark" runat="server" />
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </td>
    </tr>
</table>
