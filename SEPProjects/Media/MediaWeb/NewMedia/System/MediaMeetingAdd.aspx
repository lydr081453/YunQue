<%@ Page Language="C#" AutoEventWireup="true" Title="���ý������" EnableEventValidation="false"
    MasterPageFile="~/MasterPage.master" Inherits="NewMedia_System_MediaMeetingAdd" Codebehind="MediaMeetingAdd.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<script type="text/javascript">
    function isNum(s) {
        var pattern = /^\d+(\.\d+)?$/;
        if (pattern.test(s)) {
            return true;
        }
        return false;
    }

    function validInput() {
        var msg = "";
        if (document.getElementById("<%= txtSubject.ClientID %>").value == "") {
            msg += "�������������." + "\n";
        }
        if (!isNum(document.getElementById("<%= txtCycle.ClientID %>").value)) {
            msg += "��������ȷ���ں�." + "\n";
        }
        if (msg == "")
            return true;
        else {
            alert(msg);
            return false;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidClientId" runat="server" value="0" /><input type="hidden"
        id="hidUrl" runat="server" />
    <table width="100%">
        <tr>
            <td>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td class="heading" colspan="4">
                            ���ý������
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            �ںţ�
                        </td>
                        <td class="oddrow-l" >
                            ��<asp:TextBox ID="txtCycle" runat="server" Width="10%"></asp:TextBox>��
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            ���⣺
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtSubject" runat="server" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            ѡ���ļ���
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:FileUpload ID="fplTitle" runat="server" Width="80%" Height="24px" unselectable="on" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            ��ע��
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtRemark" TextMode="MultiLine" Height="137px" runat="server" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnOk" runat="server" Text="����" OnClientClick="return validInput();" OnClick="btnOk_Click" CssClass="widebuttons" />
            </td>
        </tr>
        <tr>
        <td>
        <table width="100%" border="0">
                    <tr>
                        <td class="headinglist" colspan="3">
                            ý�����������б�
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" DataKeyNames="path">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
        </td>
        </tr>
    </table>
</asp:Content>
