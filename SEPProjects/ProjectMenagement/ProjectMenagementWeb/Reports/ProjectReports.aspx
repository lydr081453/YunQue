<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectReports.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="FinanceWeb.Reports.ProjectReports"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function validation() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtBeginDate").value == "" || document.getElementById("ctl00_ContentPlaceHolder1_txtEndDate").value == "") {
                alert("��ѡ��ʱ�䷶Χ��");
                return false;
            }
            //            var beginyear = document.getElementById("ctl00_ContentPlaceHolder1_ddlYear").value;
            //            var beginmonth = document.getElementById("ctl00_ContentPlaceHolder1_ddlMonth").value;
            //            var endyear = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndYear").value;
            //            var endmonth = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndMonth").value;
            //            if (beginyear > endyear || beginyear == endyear && beginmonth > endmonth) {
            //                    alert("��ѡ����ȷ��ʱ�䷶Χ��");
            //                    return false;    
            return true;
        }
        function projectvalidation() {
            if (document.getElementById("ctl00_ContentPlaceHolder1_txtProjectCode").value == "") {
                alert("����д��Ŀ��ţ�");
                return false;
            }
            var beginyear = document.getElementById("ctl00_ContentPlaceHolder1_ddlYear").value;
            var beginmonth = document.getElementById("ctl00_ContentPlaceHolder1_ddlMonth").value;
            var endyear = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndYear").value;
            var endmonth = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndMonth").value;
            if (beginyear > endyear || beginyear == endyear && beginmonth > endmonth) {
                alert("��ѡ����ȷ��ʱ�䷶Χ��");
                return false;
            }
            return true;
        }
        $().ready(function() {
            $("#<%=ddltype1.ClientID %>").empty();
            $("#<%=ddltype2.ClientID %>").empty();
            Purchase_selectOperationAuditor.getalist($("#<%=hidtype.ClientID %>").val(), init1);
            function init1(r) {
                if (r.value != null)
                    for (k = 0; k < r.value.length; k++) {
                    if (r.value[k][0] == $("#<%=hidtype1.ClientID %>").val()) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\" selected>" + r.value[k][1] + "</option>");
                    }
                    else {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[k][0] + "\">" + r.value[k][1] + "</option>");
                    }
                }

            }
            if ($("#<%=hidtype.ClientID %>").val() == "") {
                $("#<%=ddltype.ClientID %>").val("-1");
            }
            Purchase_selectOperationAuditor.getalist($("#<%=hidtype1.ClientID %>").val(), init2);
            function init2(r) {
                $("#<%=ddltype2.ClientID %>").empty();
                //            $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\" selected>��ѡ��...</option>");
                if (r.value != null)
                    for (j = 0; j < r.value.length; j++) {
                    if (r.value[j][0] == $("#<%=hidtype2.ClientID %>").val()) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\" selected>" + r.value[j][1] + "</option>");
                    }
                    else {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[j][0] + "\">" + r.value[j][1] + "</option>");
                    }
                }
            }
            $("#<%=ddltype.ClientID %>").val($("#<%=hidtype.ClientID %>").val());




            $("#<%=ddltype.ClientID %>").change(function() {
                $("#<%=hidtype.ClientID %>").val($("#<%=ddltype.ClientID %>").val());
                $("#<%=ddltype1.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").empty();

                Purchase_selectOperationAuditor.getalist($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                    }
                }
                $("#<%=ddltype2.ClientID %>").append("<option value='-1'>��ѡ��...</option>");
                $("#<%=hidtype1.ClientID %>").val("-1");
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype1.ClientID %>").change(function() {
                $("#<%=ddltype2.ClientID %>").empty();

                Purchase_selectOperationAuditor.getalist($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    if (r.value != null)
                        for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                    }
                }
                $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=hidtype2.ClientID %>").val("-1");
            });

            $("#<%=ddltype2.ClientID %>").change(function() {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });

        });
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                �¶ȱ���--�Ŷ� �� �ܣ��£��ȱ���--����
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                �Ŷ�����:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:DropDownList ID="ddltype" Width="100px" runat="server" AutoPostBack="false" />
                &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="100px" />
                &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="100px" />
                <asp:HiddenField ID="hidtype" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype1" Value="-1" runat="server" />
                <asp:HiddenField ID="hidtype2" Value="-1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                ���ڷ�Χ:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <%--<asp:DropDownList ID="ddlYear" runat="server" Width="30px"></asp:DropDownList>&nbsp;
                <asp:DropDownList ID="ddlMonth" runat="server" Width="30px">
                    <asp:ListItem Value="1">1��</asp:ListItem>
                    <asp:ListItem Value="2">2��</asp:ListItem>
                    <asp:ListItem Value="3">3��</asp:ListItem>
                    <asp:ListItem Value="4">4��</asp:ListItem>
                    <asp:ListItem Value="5">5��</asp:ListItem>
                    <asp:ListItem Value="6">6��</asp:ListItem>
                    <asp:ListItem Value="7">7��</asp:ListItem>
                    <asp:ListItem Value="8">8��</asp:ListItem>
                    <asp:ListItem Value="9">9��</asp:ListItem>
                    <asp:ListItem Value="10">10��</asp:ListItem>
                    <asp:ListItem Value="11">11��</asp:ListItem>
                    <asp:ListItem Value="12">12��</asp:ListItem>
                </asp:DropDownList>--%>
                <asp:TextBox ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />&nbsp;&nbsp;��&nbsp;&nbsp;
                <asp:TextBox ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server"
                    onclick="setDate(this);" />
                <%--<asp:DropDownList ID="ddlEndYear" runat="server" Width="30px"></asp:DropDownList>&nbsp;
                <asp:DropDownList ID="ddlEndMonth" runat="server" Width="30px">
                    <asp:ListItem Value="1">1��</asp:ListItem>
                    <asp:ListItem Value="2">2��</asp:ListItem>
                    <asp:ListItem Value="3">3��</asp:ListItem>
                    <asp:ListItem Value="4">4��</asp:ListItem>
                    <asp:ListItem Value="5">5��</asp:ListItem>
                    <asp:ListItem Value="6">6��</asp:ListItem>
                    <asp:ListItem Value="7">7��</asp:ListItem>
                    <asp:ListItem Value="8">8��</asp:ListItem>
                    <asp:ListItem Value="9">9��</asp:ListItem>
                    <asp:ListItem Value="10">10��</asp:ListItem>
                    <asp:ListItem Value="11">11��</asp:ListItem>
                    <asp:ListItem Value="12">12��</asp:ListItem>
                </asp:DropDownList>--%>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                &nbsp;
                <asp:CheckBox ID="chkMonth" runat="server" Checked="true" Text="�Ŷ��¶ȱ���" />&nbsp;
                <asp:CheckBox ID="chkWeek" runat="server" Checked="true" Text="Ա����/�¶ȱ���" />&nbsp;
                <asp:Button ID="btnExport" runat="server" Text=" �������� " CssClass="widebuttons" Style="width: 100px"
                    OnClick="btnExport_Click" OnClientClick="return validation();" />&nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td colspan="4">
               <asp:Label runat="server" ForeColor="Red" ID="lblMsg"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                ��Ŀ����
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                ��Ŀ��:
            </td>
            <td class="oddrow-l" align="left">
                <asp:TextBox ID="txtProjectCode" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnProjectView" Text=" �鿴��Ŀ��Ϣ " OnClick="btnProjectView_click" />
            </td>
             <td class="oddrow" style="width: 15%">
                �ɱ�������:
            </td>
              <td class="oddrow-l" align="left">
              <asp:DropDownList runat="server" ID="ddlDept"></asp:DropDownList>
              </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                ��Ŀ������:
            </td>
            <td class="oddrow-l" align="left">
                <asp:Label ID="lblLeader" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                ��Ŀ����:
            </td>
            <td class="oddrow-l" align="left">
                <asp:Label runat="server" ID="lblPrjName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                ��ʼ����:
            </td>
            <td class="oddrow-l" align="left">
                <asp:Label ID="lblBegin" runat="server"></asp:Label>
            </td>
            <td class="oddrow" style="width: 15%">
                ��������:
            </td>
            <td class="oddrow-l" align="left">
                <asp:Label runat="server" ID="lblEnd"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                ���ڷ�Χ:
            </td>
            <td class="oddrow-l" align="left" colspan="3">
                <asp:DropDownList ID="ddlYear" runat="server" Width="30px">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlMonth" runat="server" Width="30px">
                    <asp:ListItem Value="1">1��</asp:ListItem>
                    <asp:ListItem Value="2">2��</asp:ListItem>
                    <asp:ListItem Value="3">3��</asp:ListItem>
                    <asp:ListItem Value="4">4��</asp:ListItem>
                    <asp:ListItem Value="5">5��</asp:ListItem>
                    <asp:ListItem Value="6">6��</asp:ListItem>
                    <asp:ListItem Value="7">7��</asp:ListItem>
                    <asp:ListItem Value="8">8��</asp:ListItem>
                    <asp:ListItem Value="9">9��</asp:ListItem>
                    <asp:ListItem Value="10">10��</asp:ListItem>
                    <asp:ListItem Value="11">11��</asp:ListItem>
                    <asp:ListItem Value="12">12��</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlEndYear" runat="server" Width="30px">
                </asp:DropDownList>
                &nbsp;��&nbsp;
                <asp:DropDownList ID="ddlEndMonth" runat="server" Width="30px">
                    <asp:ListItem Value="1">1��</asp:ListItem>
                    <asp:ListItem Value="2">2��</asp:ListItem>
                    <asp:ListItem Value="3">3��</asp:ListItem>
                    <asp:ListItem Value="4">4��</asp:ListItem>
                    <asp:ListItem Value="5">5��</asp:ListItem>
                    <asp:ListItem Value="6">6��</asp:ListItem>
                    <asp:ListItem Value="7">7��</asp:ListItem>
                    <asp:ListItem Value="8">8��</asp:ListItem>
                    <asp:ListItem Value="9">9��</asp:ListItem>
                    <asp:ListItem Value="10">10��</asp:ListItem>
                    <asp:ListItem Value="11">11��</asp:ListItem>
                    <asp:ListItem Value="12">12��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="Button1" runat="server" Text=" ������Ŀ���� " CssClass="widebuttons" Style="width: 100px"
                    OnClick="btnExportProject_Click" OnClientClick="return projectvalidation();" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
