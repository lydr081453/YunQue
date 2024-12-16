<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="Media_AuditedMediaList" Title="ý���б�" Codebehind="AuditedMediaList.aspx.cs" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">


<script type="text/javascript" src="/public/js/jquery-1.2.3.pack.js"></script>

<script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

<script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

<script src="/public/js/dimensions.js" type="text/javascript"></script>
    <script type="text/javascript">
        //ת���ʼ�ҳ
        function MailOpen() {
            MID = document.getElementById("<% = hidChkID.ClientID %>");
            selectone();
            if (MID.value == "") return false;
            window.open("../ShortMsg/SendMail.aspx?MID=" + MID.value + "&action=Media", "�����ʼ�", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
        }
        //ת�����ҳ
        function MsgOpen() {
            selectone();
            if (hidChkID.value == "") return false;
            MID = document.getElementById("<% = hidChkID.ClientID %>");

            window.open("../ShortMsg/SendShortMsg.aspx?MID=" + MID.value + "&action=Media", "���Ͷ���", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
        }
        //ѡ��ý��
        function selectone() {
            var Element = document.getElementsByName("chkRep");
            hidChkID = document.getElementById("<% = hidChkID.ClientID %>");
            hidChkID.value = "";
            var ids = "";
            for (var j = 0; j < Element.length; j++) {
                if (Element[j].checked) {
                    ids += Element[j].value + ",";
                    hidChkID.value += Element[j].value + ",";
                }
            }
            ids = ids.substring(0, ids.length - 1);
            hidChkID.value = ids;
            if (ids != "") {
                return ids;
            } else {
                alert("��ѡ��ý��");
                return false;
            }
        }
        function selectedcheck(parent, sub) {
            var chkSelect = document.getElementById("chk" + parent);
            var elem = document.getElementsByName("chk" + sub);
            for (i = 0; i < elem.length; i++) {
                if (elem[i].type == "checkbox") {
                    elem[i].checked = chkSelect.checked;
                }
            }
        }
        function btnReporterSign_ClientClick(fn) {
            if (selectone()) {
            hidChkID = document.getElementById("<% = hidChkID.ClientID %>");
            window.open('/DownLoad/AuditedMediaList.aspx?ExportType=sign&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
        function btnReporterContact_ClientClick(fn) {
            if (selectone()) {
                window.open('/DownLoad/AuditedMediaList.aspx?ExportType=contact&FileName=' + fn + '&Term=' + hidChkID.value, "");
                return false;
            }
            else return false;
        }
    </script>


    
<script type="text/javascript">
    $(document).ready(function() {
        //�������Գ�ʼ�� begin
        if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 0) {
            $("#<%=pnlRAC.ClientID %>").css("display", "none");
            $("#<%=pnlRAPC.ClientID %>").css("display", "none");
        }
        if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 1) {
            $("#<%=pnlRAC.ClientID %>").css("display", "none");
            $("#<%=pnlRAPC.ClientID %>").css("display", "none");
        }
        if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 2) {
            $("#<%=pnlRAC.ClientID %>").css("display", "none");
            $("#<%=ddlProvince.ClientID %>").empty();
            $("#<%=ddlCity.ClientID %>").empty();

            ESP.Media.BusinessLogic.ProvinceManager.getAllListByCountryA(popProvince);
            function popProvince(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlProvince.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
                $("#<%=ddlProvince.ClientID %>").val($("#<%=hidPro.ClientID %>").val());
            }

            ESP.Media.BusinessLogic.CityManager.getAllListByProvinceA($("#<%=hidPro.ClientID %>").val(), popCity);
            function popCity(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCity.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
                $("#<%=ddlCity.ClientID %>").val($("#<%=hidCity.ClientID %>").val());
            }
        }
        if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 3) 
        {
            $("#<%=pnlRAPC.ClientID %>").css("display", "none");
            
              ESP.Media.BusinessLogic.CountryManager.getListByRegionAttributeIDA($("#<%=ddlRegionAttribute.ClientID %>").val(), popCountry);
            function popCountry(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCountry.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
                
                 $("#<%=ddlCountry.ClientID %>").val($("#<%=hidCountry.ClientID %>").val());
            }
        }
        //�������Գ�ʼ�� end

        //��������change begin
        $("#<%=ddlRegionAttribute.ClientID %>").change(function() {
            $("#<%=pnlRAC.ClientID %>").css("display", "block");
            $("#<%=pnlRAPC.ClientID %>").css("display", "block");
            $("#<%=ddlCountry.ClientID %>").empty();
            $("#<%=ddlProvince.ClientID %>").empty();
            $("#<%=ddlCity.ClientID %>").empty();

            ESP.Media.BusinessLogic.ProvinceManager.getAllListByCountryA(popProvince);
            function popProvince(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlProvince.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }

            ESP.Media.BusinessLogic.CountryManager.getListByRegionAttributeIDA($("#<%=ddlRegionAttribute.ClientID %>").val(), popCountry);
            function popCountry(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCountry.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
            ESP.Media.BusinessLogic.CityManager.getAllListByProvinceA($("#<%=ddlProvince.ClientID %>").val(), popCity);
            function popCity(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCity.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }

            if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 0) {
                $("#<%=pnlRAC.ClientID %>").css("display", "none");
                $("#<%=pnlRAPC.ClientID %>").css("display", "none");
            }
            if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 1) {
                $("#<%=pnlRAC.ClientID %>").css("display", "none");
                $("#<%=pnlRAPC.ClientID %>").css("display", "none");
            }
            if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 2) {
                $("#<%=pnlRAC.ClientID %>").css("display", "none");
            }
            if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 3) {
                $("#<%=pnlRAPC.ClientID %>").css("display", "none");
            }
        });
        //��������change end

        //ʡ������ begin

        $("#<%=ddlCountry.ClientID %>").change(function() {
            $("#<%=hidCountry.ClientID %>").val($("#<%=ddlCountry.ClientID %>").val());
        });
        //��������ʡ������
        $("#<%=ddlProvince.ClientID %>").change(function() {
            $("#<%=hidPro.ClientID %>").val($("#<%=ddlProvince.ClientID %>").val());
            $("#<%=ddlCity.ClientID %>").empty();
            ESP.Media.BusinessLogic.CityManager.getAllListByProvinceA($("#<%=ddlProvince.ClientID %>").val(), popCity);
            function popCity(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCity.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
        });
        $("#<%=ddlCity.ClientID %>").change(function() {
            $("#<%=hidCity.ClientID %>").val($("#<%=ddlCity.ClientID %>").val());
        });

    });
    
    function AreaChanged()
    {
        //�������Գ�ʼ�� begin
         if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 0) {
            $("#<%=pnlRAC.ClientID %>").css("display", "none");
            $("#<%=pnlRAPC.ClientID %>").css("display", "none");
         }
        if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 1) {
            $("#<%=pnlRAC.ClientID %>").css("display", "none");
            $("#<%=pnlRAPC.ClientID %>").css("display", "none");
        }
        if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 2) {
            $("#<%=pnlRAC.ClientID %>").css("display", "none");
        }
        if ($("#<%=ddlRegionAttribute.ClientID %>").val() == 3) {
            $("#<%=pnlRAPC.ClientID %>").css("display", "none");
        }
        //�������Գ�ʼ�� end
    }
</script>

    <table width="100%">
        <tr>
            <td>
                <%-- <table class="tablehead">
                    <tr>
                        <td>
                            <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                ID="btnAddReporter" runat="server" class="bigfont" Text="�����ý��" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>--%>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            ��������
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            ý�����ƣ�
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtCnName" runat="server" Width="119px"></asp:TextBox>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            ��̬��
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlMediaType" runat="server" AutoPostBack="true" CssClass="fixddl">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            �������ԣ�
                        </td>
                        <td class="oddrow-l">
                           <asp:DropDownList ID="ddlRegionAttribute" runat="server" AutoPostBack="false" 
                                Height="16px" Width="119px" />
                        </td>
                        <td class="oddrow" style="width: 20%">
                            ��ҵ���ԣ�
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlIndustry" runat="server" CssClass="fixddl" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                       <tr style="border-width: 0px">
        <td colspan="4" style="border-width: 0px">
            <asp:Panel ID="pnlRAC" runat="server" BorderWidth="0" >
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="border-width: 0px">
                        <td class="oddrow" style="width: 20%; border-width: 0px">
                            ���ң�
                        </td>
                        <td class="oddrow-l" style="width: 80%; border-width: 0px">
                            <asp:DropDownList ID="ddlCountry" CssClass="fixddl" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr style="border-width: 0px">
        <td colspan="4" style="border-width: 0px">
            <asp:Panel ID="pnlRAPC" runat="server" BorderWidth="0" >
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="border-width: 0px">
                        <td class="oddrow" style="width: 20%; border-width: 0px">
                            ʡ��
                        </td>
                        <td class="oddrow-l" style="width: 30%; border-width: 0px">
                            <asp:DropDownList ID="ddlProvince" CssClass="fixddl" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 20%; border-width: 0px">
                            �У�
                        </td>
                        <td class="oddrow-l" style="width: 30%; border-width: 0px">
                            <asp:DropDownList ID="ddlCity" CssClass="fixddl" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSearch" Text="����" OnClick="btnSearch_Click" runat="server" CssClass="widebuttons">
                            </asp:Button>
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="�����ܿ�" CausesValidation="true"
                                OnClick="btnClear_OnClick" />
                            <%--<asp:Button ID="btnAdd" runat="server" Text="�����ý��" OnClick="btnAdd_Click" CssClass="widebuttons" />--%>
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
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4" class="headinglist" align="left" style="width: 10%">
                                        ý���б�
                                    </td>
                                    <td width="73%">
                                    </td>
                                    <td align="right" style="width: 17%" class="tablehead">
                                        <img src="/images/add.gif" border="0" style="vertical-align: bottom;" />
                                        &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" class="bigfont" Text="�����ý��"
                                            OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <table width="100%" border="0">
        <tr>
            <td align="right">
                <asp:Button ID="btnSendMail" runat="server" CssClass="widebuttons" Text="�����ʼ�" CausesValidation="true"
                    OnClientClick="return MailOpen() ;"></asp:Button>
                <asp:Button ID="btnSendMsg" runat="server" CssClass="widebuttons" Text="���Ͷ���" CausesValidation="true"
                    OnClientClick="return MsgOpen() ;"></asp:Button>
                <asp:Button ID="btnReporterSign" runat="server" CssClass="widebuttons" Text="����ǩ����"
                    OnClick="btnReporterSign_Click" />
                <asp:Button ID="btnReporterContact" runat="server" CssClass="widebuttons" Text="����ͨ����"
                    OnClick="btnReporterContact_Click" />
            </td>
        </tr>
    </table>
    <input type="hidden" value="0" runat="server" id="hidChkID" />
    <input type="hidden" id="hidCity" runat="server"/>
    <input type="hidden" id="hidPro" runat="server"/>
    <input type="hidden" id="hidCountry" runat="server"/>
</asp:Content>
