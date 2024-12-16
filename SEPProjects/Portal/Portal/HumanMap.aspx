<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HumanMap.aspx.cs" Inherits="Portal.WebSite.HumanMap"
    MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">

    function AutoLink() {
        window.open("AllJob.aspx", "所有职位", null, null);
    }

    function FormatDateTime(dt) {
        function toFixedLengthString(val, len) {
            var n = Math.pow(10, len);
            var s = new String();
            s += (val + n);
            s = s.substr(s.length - len, len);
            return s;
        }

        var ret = new String();
        ret += dt.getFullYear();
        ret += '-';
        ret += toFixedLengthString(dt.getMonth() + 1, 2);
        ret += '-';
        ret += toFixedLengthString(dt.getDate(), 2);
        ret += ' ';
        ret += toFixedLengthString(dt.getHours(), 2);
        ret += ':';
        ret += toFixedLengthString(dt.getMinutes(), 2);
        ret += ':';
        ret += toFixedLengthString(dt.getSeconds(), 2);
        return ret;
    }
    </script>

    <script language="javascript" type="text/javascript">
        function getUserInfo(userid) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "API/TaskItemMessages.aspx?ApplicantID=" + userid,
                data: "",
                beforeSend: function() { },
                complete: function() { },
                success: function(result) {
                    var div = document.getElementById("userIntroduction");
                    div.innerHTML = result;
                }
            });
        }

        function SearchUserInfo() {
            document.getElementById("treeview").style.display = "none";
            document.getElementById("userinfo").style.display = "block";
            var keyword = document.getElementById("txtKeyword").value;
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "API/TaskItemMessages.aspx?keyword=" + encodeURI(keyword),
                data: "",
                beforeSend: function() { },
                complete: function() { },
                success: function(result) {
                    var script = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                    script += result;
                    script += "</table>";
                    var d = document.getElementById("userinfo");
                    d.style.overflowY = "auto";
                    d.style.overflowX = "hidden";
                    d.style.height = "500px";
                    d.innerHTML = script;
                }
            });
        }
        function SearchAllUser() {
            document.getElementById("treeview").style.display = "block";
            document.getElementById("userinfo").style.display = "none";
        }

        function AboutTaskItems() {
            jQuery.blockUI({ message: $("#aboutTaskItems"), hideHeader: true, centerX: true,
                css: {
                    padding: '10px',
                    margin: 0,
                    width: '60%',
                    top: '20%',
                    left: '20%',
                    textAlign: 'center',
                    color: '#000',
                    border: 'none',
                    backgroundColor: 'transparent',
                    overflow: "auto"
                }
            });
        }
        function AboutTaskItems2() {
            jQuery.blockUI({ message: $("#aboutTaskItems2"), hideHeader: true, centerX: true,
                css: {
                    padding: '10px',
                    margin: 0,
                    width: '60%',
                    top: '20%',
                    left: '20%',
                    textAlign: 'center',
                    color: '#000',
                    border: 'none',
                    backgroundColor: 'transparent',
                    overflow: "auto"
                }
            });
        }
    </script>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 10px 0 10px 0;">
        <tr>
            <td valign="top" class="nav">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/menu_bg.jpg">
                    <tr>
                        <td width="5">
                            <img src="images/a1_113.gif" width="5" height="32" />
                        </td>
                        <td class="top">
                            人力地图...
                        </td>
                        <td width="75" valign="bottom">
                            <a href="Default.aspx">
                                <img src="images/renlibtn_11.jpg" width="75" height="27" border="0" /></a>
                        </td>
                        <td width="3">
                        </td>
                        <td width="75" valign="bottom">
                            <a href="HumanMap.aspx">
                                <img src="images/renlibtn_03.jpg" width="75" height="27" border="0" /></a>
                        </td>
                        <td width="3">
                        </td>
                        <td width="75" valign="bottom">
                            <a href="AttendanceSelect.aspx">
                                <img src="images/attendance2.jpg" width="75" height="27" border="0" /></a>
                        </td>
                         <td width="6">
                        </td>
                        <td width="5">
                            <img src="images/a1_19.gif" width="5" height="32" />
                        </td>
                    </tr>
                </table>
                <table id="Table2" width="100%">
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="61" background="images/renli_03.jpg">
                                        <table width="30%" border="0" cellspacing="0" cellpadding="0" style="margin-left: 25px;">
                                            <tr>
                                                <td width="171" height="24" background="images/renli_06.jpg">
                                                    <input name="txtKeyword" type="text" class="input" id="txtKeyword" style="width: 130px;
                                                        margin-left: 30px; border: 1px solid #FFFFFF;">
                                                </td>
                                                <td width="53">
                                                    <a href="#" id="searchuserinfo" onclick="SearchUserInfo();">
                                                        <img src="images/renli_08.jpg" width="53" height="24" hspace="6" /></a>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="SearchAllUser();">
                                                        <img src="images/renli_10.jpg" width="73" height="24" /></a>
                                                </td>
                                                <td>
                                                    <asp:Button Style="border: 0px; background-color: #c4c4c4; margin-left: 15px; font-weight: bold;
                                                        height: 24px; color: #fff" ID="AddAddressBook" Text="通讯录快照" runat="server" Visible="false"
                                                        OnClick="AddAddressBook_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;">
                                <tr>
                                    <td width="25%" valign="top">
                                        <div style="height: 500px; overflow: auto">
                                            <span id="treeview">
                                                <asp:TreeView ID="stv1" runat="server" AllowCascadeCheckbox="True" ImageSet="Msdn"
                                                    ShowLines="true" Style="border-right: 0px inset; border-top: 0px inset; overflow: auto;
                                                    border-left: 0px inset; width: 100%; border-bottom: 0px inset; height: 100%;
                                                    background-color: white" NodeIndent="20" ExpandDepth="1">
                                                    <ParentNodeStyle Font-Bold="False" />
                                                    <HoverNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                                                    <SelectedNodeStyle BackColor="#ffffff" BorderColor="#888888" BorderStyle="None" Font-Underline="False" />
                                                    <NodeStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" HorizontalPadding="2px"
                                                        NodeSpacing="1px" VerticalPadding="2px" />
                                                </asp:TreeView>
                                            </span>
                                            <div id="userinfo" style="background: images/renli_24.gif; background-repeat: repeat-x;
                                                padding: 10px 25px 10px 25px;" class="seachresult">
                                            </div>
                                        </div>
                                    </td>
                                    <td width="1%">
                                    </td>
                                    <td valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="1%" background="images/renli_19.jpg">
                                                    <img src="images/renli_17.jpg" width="7" height="30" />
                                                </td>
                                                <td width="98%" background="images/renli_19.jpg">
                                                    <strong style="color: #FFFFFF; font-size: 14px;">人员基本信息</strong>
                                                </td>
                                                <td width="1%" align="right" background="images/renli_19.jpg">
                                                    <img src="images/renli_20.jpg" width="7" height="30" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="180" colspan="3" valign="top" background="images/renli_24.gif" style="line-height: 200%;
                                                    padding: 20px 0 0 20px;">
                                                    <span id="userIntroduction"></span>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="1%" background="images/renli_19.jpg">
                                                    <img src="images/renli_17.jpg" width="7" height="30" />
                                                </td>
                                                <td width="98%" background="images/renli_19.jpg">
                                                    <strong style="color: #FFFFFF; font-size: 14px;">公司地址信息</strong>
                                                </td>
                                                <td width="1%" align="right" background="images/renli_19.jpg">
                                                    <img src="images/renli_20.jpg" width="7" height="30" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="300" colspan="3" valign="top" background="images/renli_24.gif" style="line-height: 200%;
                                                    padding: 20px 0 0 20px;">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="30">
                                                                总部:
                                                            </td>
                                                            <td>
                                                                北京市朝阳区双桥路12号 电子城数字新媒体创新产业园D1楼
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                D1, Digital New Media Innovation Industrial Park, Electronics City, No.12, Shuangqiao Road, Chaoyang District, Beijing
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                总机：010 - 8509 5766&nbsp;传真：010 - 8509 5795<br />
                                                                Tel: &nbsp;010 - 8509 5766&nbsp;Fax: 010 - 8509 5795
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30">
                                                                重庆:
                                                            </td>
                                                            <td>
                                                                重庆市渝北区大竹林街道杨柳路6号三狼公园6号D4-102 邮编:401120
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                D4-102, No.6 Sanlang Park,Yangliu Road,Dazhulin Street,Yubei District,Chongqing 401120
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;<br />
                                                                &nbsp;
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
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="300" valign="top" class="navright">
                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="about">
                    <tr>
                        <td height="190" valign="top">
                            <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="title" colspan="2">
                                        <img src="images/a1_30.gif" width="17" height="15" />
                                        我的信息
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1" bgcolor="#565656" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 10px 0;" colspan="2">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="94">
                                                    <table width="90" border="0" cellpadding="1" cellspacing="1" bgcolor="#999999">
                                                        <tr>
                                                            <td bgcolor="#FFFFFF">
                                                                <img id="imgUserIcon" runat="server" width="90" height="90" hspace="0" vspace="0" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="174" style="padding-left: 10px;">
                                                    <asp:Label ID="labUserInfo" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="title">
                                        <img src="images/a1_asset.gif" width="17" height="15" />
                                        资产领用
                                    </td>
                                     <td class="title">
                                          <a href='http://xf.shunyagroup.com/Return/SalaryView.aspx' target="_blank" style="text-decoration:none; color:black;"><img src="/images/salary.gif"/>
                                        薪资查询 </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1" bgcolor="#565656" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 10px 0;" colspan="2">
                                        <asp:Label ID="lblAsset" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="notice" style="margin: 5px 0 5px 0;">
                    <tr>
                        <td valign="top">
                            <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="title">
                                        <img src="images/a1_48.gif" width="19" height="16" />
                                        公告
                                    </td>
                                </tr>
                                <tr>
                                    <td height="1">
                                        <img src="images/a1_51.gif" width="269" height="2" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="财务部重要提醒2024年12月25日前需协助完成事宜.docx">财务部重要提醒 - 2024年12月25日前需协助完成事宜</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                  <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="费用申请报销与借款管理条例.docx">费用申请、报销与借款管理条例</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-top: 10px;">
                                    ·<a href="费用申请报销细则.docx">费用申请报销细则</a><img src="images/a1_54.gif" width="23"
                                            height="12" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 10px;">
                                    &nbsp;<br />
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
