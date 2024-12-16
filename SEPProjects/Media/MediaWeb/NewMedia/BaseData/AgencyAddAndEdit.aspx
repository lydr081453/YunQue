<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgencyAddAndEdit.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.AgencyAddAndEdit" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<script type="text/javascript" src="/public/js/jquery-1.2.3.pack.js"></script>
<script type="text/javascript">

    function check() {        
        var meg = "";
        //机构中文名称
        if (document.getElementById("<% = txtAgencyName.ClientID %>").value == "") {
            meg += "机构中文名称不能为空！" + "\n";
        }

        if (meg != "") {
            alert(meg);
            return false;
        }
    }

    function selectMedia() {
        var win = window.open("AgencySelectMediaList.aspx");
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);

    }
</script>

<script type="text/javascript">
    $().ready(function() {
       

        //地域属性初始化 begin
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
        //地域属性初始化 end

        //地域属性change begin
        $("#<%=ddlRegionAttribute.ClientID %>").change(function() {

            $("#<%=pnlRAC.ClientID %>").css("display", "block");
            $("#<%=pnlRAPC.ClientID %>").css("display", "block");
            $("#<%=ddlCountry.ClientID %>").empty();
            $("#<%=ddlProvince.ClientID %>").empty();
            $("#<%=ddlCity.ClientID %>").empty();

            ESP.MediaLinq.BusinessLogic.ProvinceManager.getAllListByCountryA(popProvince);
            function popProvince(r) {
                ;
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlProvince.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }

            ESP.MediaLinq.BusinessLogic.CountryManager.getListByRegionAttributeIDA($("#<%=ddlRegionAttribute.ClientID %>").val(), popCountry);
            function popCountry(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCountry.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
            ESP.MediaLinq.BusinessLogic.CityManager.getAllListByProvinceA($("#<%=ddlProvince.ClientID %>").val(), popCity);
            function popCity(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCity.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
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
        //地域属性change end

        //省市联动 begin

        $("#<%=ddlCountry.ClientID %>").change(function() {
            $("#<%=hidCountry.ClientID %>").val($("#<%=ddlCountry.ClientID %>").val());
        });
        //地域属性省市联动
        $("#<%=ddlProvince.ClientID %>").change(function() {
            $("#<%=hidPro.ClientID %>").val($("#<%=ddlProvince.ClientID %>").val());
            $("#<%=ddlCity.ClientID %>").empty();
            ESP.MediaLinq.BusinessLogic.CityManager.getAllListByProvinceA($("#<%=ddlProvince.ClientID %>").val(), popCity);
            function popCity(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCity.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
        });
        $("#<%=ddlCity.ClientID %>").change(function() {
            $("#<%=hidCity.ClientID %>").val($("#<%=ddlCity.ClientID %>").val());
        });
        //省市联动end
    });
</script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%;" border="1" class="tableForm">
    <tr>
        <td colspan="4" class="menusection-Packages">
            机构信息维护
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            基本信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            机构中文名称：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:TextBox ID="txtAgencyName" runat="server" MaxLength="50"></asp:TextBox><font
                color="red"> *</font>
        </td>
         <td class="oddrow" style="width: 20%">
            机构英文名称：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:TextBox ID="txtAgencyEngName" runat="server" MaxLength="50"></asp:TextBox>
        </td>   
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            所属媒体：
        </td>
        <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtMediaName" runat="server" /><font color="red"> *
                </font>
                <asp:HiddenField ID="hidMedia" runat="server" />
                &nbsp;<asp:Button ID="btnLink" runat="server" OnClientClick="selectMedia();" Text="变更所属媒体"
                    CssClass="bigwidebuttons" />
        </td>
         <td class="oddrow" style="width: 20%">
        </td>
        <td class="oddrow-l" style="width: 30%">
        </td>   
    </tr>
    <tr>        
        <td class="oddrow">
            地域属性：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:DropDownList ID="ddlRegionAttribute" runat="server" AutoPostBack="false" />
        </td>
    </tr>
    <tr style="border-width: 0px">
        <td colspan="4" style="border-width: 0px">
            <asp:Panel ID="pnlRAC" runat="server" BorderWidth="0">
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="border-width: 0px">
                        <td class="oddrow" style="width: 20%; border-width: 0px">
                            国家：
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
            <asp:Panel ID="pnlRAPC" runat="server" BorderWidth="0">
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="border-width: 0px">
                        <td class="oddrow" style="width: 20%; border-width: 0px">
                            省：
                        </td>
                        <td class="oddrow-l" style="width: 30%; border-width: 0px">
                            <asp:DropDownList ID="ddlProvince" CssClass="fixddl" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 20%; border-width: 0px">
                            市：
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
        <td class="oddrow">
            负责人：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtResponsiblePerson" runat="server" MaxLength="20"></asp:TextBox>
        </td>
        <td class="oddrow">
            联系人：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtContacter" runat="server" MaxLength="20"></asp:TextBox>
        </td>
    </tr>   

    <tr>
        <td colspan="4" class="heading">
            联系方式及所在地址
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            总机：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtTelephoneExchange" runat="server" MaxLength="20"></asp:TextBox><br />
            <asp:Label ID="labTelephoneExchange" runat="server">例如: 010-88888888</asp:Label>
        </td>
        <td class="oddrow">
            传真：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtFax" runat="server" MaxLength="20"></asp:TextBox><br />
            <asp:Label ID="labFax" runat="server">例如: 010-88888888</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            热线1：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtPhoneOne" runat="server" MaxLength="20"></asp:TextBox><br />
            <asp:Label ID="labPhoneOne" runat="server">例如: 010-88888888</asp:Label>
        </td>
        <td class="oddrow">
            热线2：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtPhoneTwo" runat="server"></asp:TextBox><br />
            <asp:Label ID="labPhoneTwo" runat="server">例如: 010-88888888</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            详细地址：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtAddress1" runat="server" MaxLength="50"></asp:TextBox>
        </td>
        <td class="oddrow">
            邮政编码：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtPostCode" runat="server" MaxLength="10" />
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td colspan="4" style="height: 30px">
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            其他信息
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            机构简介：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtAgencyIntro" runat="server" TextMode="MultiLine" Height="98px"
                Width="80%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            英文简介：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtEngIntro" runat="server" TextMode="MultiLine" Height="98px" Width="80%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            备注：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="98px" Width="80%"></asp:TextBox>
        </td>
    </tr>
<tr>
            <td colspan="6" style="text-align: right">                
                <asp:Button ID="btnSubmit" runat="server" CssClass="widebuttons" Text="保存" OnClick="btnSubmit_Click"
                    OnClientClick="return check();"  />
                
                <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
            <asp:HiddenField ID="hidBackUrl" runat="server" />
            <asp:HiddenField ID="hidMid" runat="server" />
        </tr>
    <input type="hidden" id="RoleColl" runat="server">
    <input type="hidden" id="hidCityAddr1" runat="server">
    <input type="hidden" id="hidCity" runat="server">
    <input type="hidden" id="hidPro" runat="server">
    <input type="hidden" id="hidCountry" runat="server">
</table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="height:25px" />
        </tr>

    </table>
</asp:Content>

