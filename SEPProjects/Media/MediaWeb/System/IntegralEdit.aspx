<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="System_IntegralEdit" EnableEventValidation="false" Codebehind="IntegralEdit.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript" src="../../DataExport/TeamLeaderDIV.js"></script>

    <script type="text/javascript">
        var selectedLeaderID = "";
        var selectedLeaderName = "";
        function selectOneLeader(obj, leaderID, leadername) {
            selectedLeaderID = leaderID;
            selectedLeaderName = leadername;
            var radios = document.getElementsByName("rdSelect");

            for (i = 0; i < radios.length; i++) {
                radios[i].checked = false;
            }
            obj.checked = true;

        }

        function confirmLeader() {
            var txtLeaderName;
            var txtLeaderID;
            txtLeaderID = document.getElementById("<%= hidLeaderID.ClientID %>");
            txtLeaderName = document.getElementById("<%= txtLeaderName.ClientID %>");
            txtLeaderID.value = selectedLeaderID;
            txtLeaderName.value = selectedLeaderName;
        }

        function onclickName(integralid, usercode, username, counts) {
            document.getElementById("<%= lblUserCode.ClientID %>").value = usercode;
            document.getElementById("<%= txtEditUsername.ClientID %>").value = username;
            document.getElementById("<%= txtUserIntegral.ClientID %>").value = counts;
            document.getElementById("<%= hidIntegralID.ClientID %>").value = integralid;
        }

        //检查是否数字
        function isNum(s) {
            var pattern = /^\d+(\.\d+)?$/;
            if (pattern.test(s)) {
                return true;
            }
            return false;
        }
        //检查是否整数
        function isInt(s) {
            var pattern = /^-?\d+$/;
            if (s.search(pattern) != 0) {
                return false;
            }
            return true;
        }

        function validModify() {
            var msg = "";
            if (document.getElementById("<%= chkAll.ClientID %>").checked) {
                if (confirm("确定要将所有用户积分清零吗？")) {
                    return true;
                }
                else
                { return false; }
            }
            else {
                if (document.getElementById("<%= lblUserCode.ClientID %>").value == "" || document.getElementById("<%= txtEditUsername.ClientID %>").value == "") {
                    msg += "请选择列表中的用户." + "\n";
                }
                if (document.getElementById("<%= txtUserIntegral.ClientID %>").value == "") {
                    msg += "请输入重新修改的积分值." + "\n";
                }
                if (!isNum(document.getElementById("<%= txtUserIntegral.ClientID %>").value)) {
                    msg += "请输入正确的积分值." + "\n";
                }
            }

            if (msg == "")
                return true;
            else {
                alert(msg);
                return false;
            }
        }
    
    </script>

    <style type="text/css">
        .orderTip
        {
            margin-left: 3px;
            color: Gray;
        }
        .orderClick
        {
            cursor: pointer;
            color: Navy;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="contentMain">
    <table width="100%">
        <tr>
            <td>
                <table border="0" width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="2">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            用户名称：<asp:TextBox ID="txtLeaderName" BackColor="LightGray" Width="152px" runat="server"
                                Text=""></asp:TextBox>
                            <input type="button" id="Button1" value="选择" onclick='TurnToPageLeader(1, "/DataExport/TeamLeaderData.aspx","/DataExport/TeamLeaderData.aspx",<%=strEmployeeJsonSource %>, document.getElementById("<%= txtLeaderName.ClientID %>"));' />
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:Button ID="btnSearch" runat="server" CssClass="widebuttons" Text="查找" CausesValidation="true"
                                OnClick="btnSearch_Click"></asp:Button>
                            <asp:Button ID="btnAll" runat="server" CssClass="widebuttons" Text="全部" CausesValidation="true"
                                OnClick="btnAll_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 20">
            <td>
            </td>
        </tr>
        <tr>
            <td class="heading" colspan="2" align="left">
                积分列表
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                </cc4:MyGridView>
                <input type="hidden" value="" runat="server" id="hidChkID" />
            </td>
        </tr>
        <tr style="height: 20">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="100%" class="tableForm">
                    <tr>
                        <td class="heading" colspan="3">
                            修改积分
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            用户名称：
                            <asp:TextBox ID="txtEditUsername" BackColor="LightGray" Width="152px" runat="server"
                                Text=""></asp:TextBox><asp:Label ID="lblUserCode" runat="server"></asp:Label>
                            <input type="hidden" id="hidLeaderID" runat="server" />
                            <input type="hidden" id="hidIntegralID" runat="server" />
                        </td>
                        <td class="oddrow">
                            累计积分：<asp:TextBox ID="txtUserIntegral" BackColor="LightGray" Width="152px" runat="server"
                                Text="" />
                        </td>
                        <td class="oddrow">
                            <asp:CheckBox ID="chkAll" runat="server" Text="所有用户积分清零" />
                            <asp:Button ID="btnModify" runat="server" CssClass="widebuttons" Text="修改" CausesValidation="true"
                                OnClientClick="return validModify();" OnClick="btnModify_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
