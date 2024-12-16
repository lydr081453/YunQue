<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControl_MediaControl_DABMediaAddAndEdit" Codebehind="DABMediaAddAndEdit.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="/public/js/jquery-1.2.3.pack.js"></script>

<script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

<script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

<script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

<script src="/public/js/dimensions.js" type="text/javascript"></script>

<script src="/public/js/jquery.autocomplete.js" type="text/javascript"></script>

<link rel="Stylesheet" type="text/css" href="/public/js/jquery.autocomplete.css" />

<script type="text/javascript">


    function FileChange(Value) {
        document.getElementById("<% = uploadimage.ClientID %>").width = 20;
        document.getElementById("<% = uploadimage.ClientID %>").height = 20;
        document.getElementById("<% = uploadimage.ClientID %>").alt = "";
        document.getElementById("<% = uploadimage.ClientID %>").src = Value;
    }


    function check() {
        onSubmit();
        var meg = "";
        //媒体中文名称
        if (document.getElementById("<% = txtDABName.ClientID %>").value == "") {
            meg += "媒体中文名称不能为空！" + "\n";
        }
        //频道名称
        if (document.getElementById("<% = txtDABChannelName.ClientID %>").value == "") {
            meg += "频道名称不能为空！" + "\n";
        }



        //调频
        if (document.getElementById("<% = txtDABFM.ClientID %>").value == "") {
            meg += "调频不能为空！" + "\n";


        }


        // 总机
        if (document.getElementById("<% = txtTelephoneExchange.ClientID %>").value != "") {
            if (document.getElementById("<% = txtTelephoneExchange.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
                meg += "总机输入错误！" + "\n";


            }
        }
        //传真
        if (document.getElementById("<% = txtFax.ClientID %>").value != "") {
            if (document.getElementById("<% = txtFax.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
                meg += "传真输入错误！" + "\n";

            }
        }

        //热线1
        if (document.getElementById("<% = txtPhoneOne.ClientID %>").value != "") {
            if (document.getElementById("<% = txtPhoneOne.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
                meg += "热线1输入错误！" + "\n";

            }
        }
        //热线2
        if (document.getElementById("<% = txtPhoneTwo.ClientID %>").value != "") {
            if (document.getElementById("<% = txtPhoneTwo.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
                meg += "热线2输入错误！" + "\n";

            }
        }
        //广告部电话txtTopicTime
        if (document.getElementById("<% = txtAdsPhone.ClientID %>").value != "") {
            if (document.getElementById("<% = txtAdsPhone.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/) == -1) {
                meg += "广告部电话输入错误！" + "\n";
            }
        }
        //播放时长
//        if (document.getElementById("<% = txtTopicTime.ClientID %>").value != "") {
//            if (document.getElementById("<% = txtTopicTime.ClientID %>").value.search(/^\d+$/) == -1) {
//                meg += "播放时长输入错误！" + "\n";
//            }
//        }
        //频道网址
        if (document.getElementById("<%= txtWebAddress.ClientID %>").value != "") {
            if (document.getElementById("<%= txtWebAddress.ClientID %>").value.search(/^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/) == -1) {
                meg += "频道网址输入错误！";


            }
        }
        //媒体英文简称
        var str = document.getElementById("<% = txtDABEngHTCName.ClientID %>").value;
        var reg = /^[0-9A-Z\-]+$/;
        if (str != "") {

            if (!reg.test(str)) {
                meg += "媒体英文简称必须是大写！" + "\n";
            }
        }
        if (meg != "") {
            alert(meg);
            return false;
        }
    }
    function openAddIndustry() {
        window.open("../Media/NewIndustry.aspx?Operate=ADD&openType=opener", "addIndustryWindow", "<%=ESP.Media.Access.Utilities.Global.OpenClass.Common %>");
    }
     
</script>

<script language="javascript">
		<!--
    function onSubmit() {
        var _tmp = "";
        for (var i = 0; i < document.getElementById("<% = lbxBranchSelected.ClientID %>").options.length; i++) {
            _tmp += document.getElementById("<% = lbxBranchSelected.ClientID %>").options(i).value + ",";
        }
        if (_tmp.trim() != "") {
            _tmp = _tmp.substring(0, _tmp.length - 1);
        }
        document.getElementById("<% = RoleColl.ClientID %>").value = _tmp;
    }

    function AddRoles() {
        for (var i = 0; i < document.getElementById("<% = lbxBranchSelected.ClientID %>").options.length; i++) {
            if (document.getElementById("<% = lbxBranchSelected.ClientID %>").options(i).selected == true) {
                var _opt = document.createElement("option");
                _opt.text = document.getElementById("<% = lbxBranchSelected.ClientID %>").options(i).text;
                _opt.value = document.getElementById("<% = lbxBranchSelected.ClientID %>").options(i).value;
                document.getElementById("<% = lbxBranchSelected.ClientID %>").options.remove(i);
                document.getElementById("<% = lbxBranch.ClientID %>").options.add(_opt);
                i--;
            }
        }
    }

    function RemoveRoles() {
        for (var i = 0; i < document.getElementById("<% = lbxBranch.ClientID %>").options.length; i++) {
            if (document.getElementById("<% = lbxBranch.ClientID %>").options(i).selected == true) {
                var _opt = document.createElement("option");
                _opt.text = document.getElementById("<% = lbxBranch.ClientID %>").options(i).text;
                _opt.value = document.getElementById("<% = lbxBranch.ClientID %>").options(i).value;
                document.getElementById("<% = lbxBranch.ClientID %>").options.remove(i);
                document.getElementById("<% = lbxBranchSelected.ClientID %>").options.add(_opt);
                i--;
            }
        }
    }
	-->
</script>

<script type="text/javascript">
    $().ready(function() {
        //频道覆盖范围初始化 begin
        //频道覆盖范围初始化 end

        //名称、频道、栏目 begin
        ESP.Media.BusinessLogic.MediaitemsManager.GetAllNames(popblock);
        function popblock(r) {
            $("#<%=txtDABName.ClientID %>").autocomplete(r.value);
        }

        $("#<%=txtDABName.ClientID %>").change(function() {
            ESP.Media.BusinessLogic.MediaitemsManager.GetChannelNameByCnName($("#<%=txtDABName.ClientID %>").val(), popchan);
            function popchan(cr) {
                $("#<%=txtDABChannelName.ClientID %>").autocomplete(cr.value);
            }
        });


        $("#<%=txtDABChannelName.ClientID %>").change(function() {
            ESP.Media.BusinessLogic.MediaitemsManager.GetTopicNameByCnName($("#<%=txtDABName.ClientID %>").val(), $("#<%=txtDABChannelName.ClientID %>").val(), poptopic);
            function poptopic(tr) {
                $("#<%=txtDABTopicName.ClientID %>").autocomplete(tr.value);
            }
        });
        //名称、频道、栏目 end

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

            ESP.Media.BusinessLogic.ProvinceManager.getAllListByCountryA(popProvince);
            function popProvince(r) {
                ;
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
        //地域属性省市联动
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

        //频道覆盖省市联动

        //地址省市联动
        $("#<%=ddlProvinceAddr1.ClientID %>").change(function() {
            $("#<%=ddlCityAddr1.ClientID %>").empty();
            ESP.Media.BusinessLogic.CityManager.getAllListByProvinceA($("#<%=ddlProvinceAddr1.ClientID %>").val(), popCity);
            function popCity(r) {
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddlCityAddr1.ClientID %>").append("<option value=\"" + r.value[i][0] + "\">" + r.value[i][1] + "</option>");
                }
            }
        });

        $("#<%=ddlCityAddr1.ClientID %>").change(function() {
            $("#<%=hidCityAddr1.ClientID %>").val($("#<%=ddlCityAddr1.ClientID %>").val());
        });
        //省市联动end

        //频道覆盖范围change begin
        //频道覆盖范围change end
    });
</script>

<table width="100%" border="1" class="tableForm">
    <tr>
        <td colspan="4" class="menusection-Packages">
            广播媒体信息维护
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            基本信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            媒体中文名称：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:TextBox ID="txtDABName" runat="server" MaxLength="50"></asp:TextBox><font color="red">
                *</font>
        </td>
        <td class="oddrow" style="width: 15%">
            频道名称：
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:TextBox ID="txtDABChannelName" runat="server" MaxLength="30" /><font color="red">
                *</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            栏目名称：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtDABTopicName" runat="server" MaxLength="30"></asp:TextBox>
        </td>
        <td class="oddrow">
            调频：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtDABFM" runat="server" MaxLength="20" /><font color="red"> *</font>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 20%">
            媒体英文名称：
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:TextBox ID="txtDABEngName" runat="server" MaxLength="50"></asp:TextBox>
        </td>
        <%--<td class="oddrow">
            媒体中文简称：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtDABHTCName" runat="server" MaxLength="50"></asp:TextBox>
        </td>--%>
        <td class="oddrow">
            媒体英文简称：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtDABEngHTCName" runat="server" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            形态属性：
        </td>
        <td class="oddrow-l" >
                          
            <asp:DropDownList ID="txtMediumSort" runat="server" CssClass="fixddl" OnSelectedIndexChanged="mediumsort_SelectedIndexChanged">
                <asp:ListItem>请选择..</asp:ListItem>
                <asp:ListItem>平面媒体</asp:ListItem>
                <asp:ListItem>网络媒体</asp:ListItem>
                <asp:ListItem>电视媒体</asp:ListItem>
                <asp:ListItem>广播媒体</asp:ListItem>
            </asp:DropDownList>
            
             <font
                color="red">*</font>
        </td>
        <td class="oddrow">
            栏目属性：
        </td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlLanMu" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            行业属性：
        </td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlIndustry" runat="server" />
            <input type="button" value="添加行业属性" class="bigwidebuttons" onclick="openAddIndustry();" />
        </td>
        <td class="oddrow">
            地域属性：
        </td>
        <td class="oddrow-l">
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
                        <td class="oddrow" style="width: 15%; border-width: 0px">
                            市：
                        </td>
                        <td class="oddrow-l" style="width: 35%; border-width: 0px" align="left">
                            <asp:DropDownList ID="ddlCity" CssClass="fixddl" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <%--<tr>
        <td class="oddrow">
            覆盖区域：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtIssueRegion" runat="server"  MaxLength="20" />
        </td>
        <td class="oddrow">
            覆盖范围：
        </td>
        <td class="oddrow-l" nowrap>
          <table style="width:100%" border="0">
                <tr>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlOverrideRange" runat="server" AutoPostBack="false" Style="width: 70px;"></asp:DropDownList>
                    </td>
                    <td align="left" width="30%">
                        <asp:Panel ID="pnlORP" runat="server">
                            <asp:DropDownList ID="ddlCoverProvince" runat="server" Style="width: 70px;">
                            </asp:DropDownList>
                            <asp:Label ID="labCoverProvince" runat="server" Text="省"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td align="left" width="30%">
                        <asp:Panel ID="pnlORC" runat="server">
                            <asp:DropDownList ID="ddlCoverCity" runat="server" Style="width: 70px;">
                            </asp:DropDownList>
                            <asp:Label ID="labCoverCity" runat="server" Text="市"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td>
                        <font color="red">*</font>
                    </td>
                </tr>
            </table>
        </td>
    </tr>--%>
    <tr>
        <td class="oddrow">
            栏目播出时间：
        </td>
        <td class="oddrow-l">
         <asp:TextBox ID="dpTopicBegin" runat="server" MaxLength="50"></asp:TextBox>
        </td>
        <td class="oddrow">
            近期收听率：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtRating" runat="server" MaxLength="20"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            栏目时长：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtTopicTime" runat="server"></asp:TextBox>
        </td>
        <td class="oddrow">
            主管单位：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtDirectorUnit" runat="server" MaxLength="20"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            台长：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtManager" runat="server" MaxLength="20"></asp:TextBox>
        </td>
        <td class="oddrow">
            总编：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtChiefEditor" runat="server" MaxLength="20"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            主任：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtZhuren" runat="server" MaxLength="20"></asp:TextBox>
        </td>
        <td class="oddrow">
            制片人：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtProducer" runat="server" MaxLength="20"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            栏目合作方式：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtCooperate" runat="server" MaxLength="1000" TextMode="MultiLine"
                Rows="3" Width="80%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            受众描述：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtReaderSort" runat="server" MaxLength="1000" TextMode="MultiLine"
                Rows="3" Width="80%">                
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            分部所在地(可多选)：
        </td>
        <td class="oddrow-l" colspan="3">
            <table border="0">
                <tr>
                    <td>
                        <asp:ListBox ID="lbxBranch" runat="server" Width="200px" Height="160px" SelectionMode="Multiple">
                        </asp:ListBox>
                    </td>
                    <td align="center">
                        <table style="width: 100%" border="0">
                            <tr>
                                <td align="center" class="oddcol">
                                    <input type="button" class="widebuttons" value="->" style="width: 120px" onclick="RemoveRoles();">
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td align="center" class="oddcol">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="oddcol">
                                    <input type="button" class="widebuttons" value="<-" style="width: 120px" onclick="AddRoles();">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:ListBox ID="lbxBranchSelected" runat="server" Width="200px" Height="160px" SelectionMode="Multiple">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 30px">
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            联系方式
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
            <asp:TextBox ID="txtPhoneTwo" runat="server" MaxLength="20"></asp:TextBox><br />
            <asp:Label ID="labPhoneTwo" runat="server">例如: 010-88888888</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            广告部电话：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtAdsPhone" runat="server" MaxLength="20"></asp:TextBox><br />
            <asp:Label ID="labAdsPhone" runat="server">例如: 010-88888888</asp:Label>
        </td>
        <td class="oddrow">
            网址：
        </td>
        <td class="oddrow-l">
            <asp:TextBox ID="txtWebAddress" runat="server" MaxLength="100"></asp:TextBox><br />
            <asp:Label ID="labWebAddress" runat="server">例如: http://www.sina.com.cn</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            省：
        </td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlProvinceAddr1" runat="server" AutoPostBack="false" CssClass="fixddl">
            </asp:DropDownList>
        </td>
        <td class="oddrow">
            市：
        </td>
        <td class="oddrow-l">
            <asp:DropDownList ID="ddlCityAddr1" runat="server" CssClass="fixddl">
            </asp:DropDownList>
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
        <td colspan="4" style="height: 30px">
        </td>
    </tr>
    <tr>
        <td colspan="4" class="heading">
            其他信息
        </td>
    </tr>
<%--    <tr>
        <td class="oddrow">
            广告报价：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:FileUpload ID="adsUploadPrice" runat="server" Width="80%" unselectable="on" />
        </td>
    </tr>--%>
    <tr>
        <td class="oddrow" valign="bottom">
            媒体LOGO：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:FileUpload ID="mediaUploadLogo" runat="server" Width="80%" Height="24px" unselectable="on"
                onchange="FileChange(this.value)" />
            <asp:Image ID="uploadimage" runat="server" ImageAlign="bottom" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            剪报：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:FileUpload ID="briefingUpload" runat="server" Width="80%" unselectable="on" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            栏目简介：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtTopicIntro" runat="server" TextMode="MultiLine" Width="80%" Height="98px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            英文简介：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtEngIntro" runat="server" TextMode="MultiLine" Width="80%" Height="98px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            备注：
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="80%" Height="98px"></asp:TextBox>
        </td>
    </tr>

    <script type="text/javascript">
        document.getElementById("<% = uploadimage.ClientID %>").width = 1;
        document.getElementById("<% = uploadimage.ClientID %>").height = 1;
    </script>

    <input type="hidden" id="RoleColl" runat="server">
    <input type="hidden" id="hidCityAddr1" runat="server">
    <input type="hidden" id="hidCity" runat="server">
    <input type="hidden" id="hidPro" runat="server">
    <input type="hidden" id="hidCountry" runat="server">
    <input type="hidden" id="hidCoverCity" runat="server">
    <input type="hidden" id="hidCoverPro" runat="server">
</table>
