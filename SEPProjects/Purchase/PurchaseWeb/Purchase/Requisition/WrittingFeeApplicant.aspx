<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    EnableEventValidation="false" Inherits="Purchase_Requisition_WrittingFeeApplicant"
    CodeBehind="WrittingFeeApplicant.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }
        function DisplayDetail(id, dis, idshow, idnone) {
            document.getElementById(id).style.display = dis;
            document.getElementById(idshow).style.display = "block";
            document.getElementById(idnone).style.display = "none";
        }
        function MediaClick() {
            var mediatype = document.getElementById('<% =ddlOption.ClientID %>').selectedIndex;
            var medianame = document.getElementById('<% = txtMediaSelect.ClientID %>').value;
            var win = window.open('MediaDlg.aspx?<% =ESP.Purchase.Common.RequestName.MediaType %>=' + mediatype + '&<% =ESP.Purchase.Common.RequestName.MediaName %>=' + medianame, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function testNum(a) {
            a += "";
            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
            if (a != "" && !isNaN(a) && Number(a) > 0) {
                return true;
            }
            else {
                return false;
            }
        }


        function ReporterClick() {
            var mid = document.getElementById('<% = hidMediaID.ClientID %>').value;
            if (mid == null || mid == "" || mid == "undefined") {
                alert("请先选择媒体." + mid);
                return false;
            }
            var win = window.open('ReporterDlg.aspx?Mid=' + mid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
            win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
        }

        function ShowImage() {
            var ddl = document.getElementById("<%=radioList.ClientID %>").value;
            if (ddl == "1") {
                document.getElementById("<%=lblImgSize.ClientID %>").style.display = "block";
                document.getElementById("<%=txtImgSize.ClientID %>").style.display = "block";
            }
            else {
                document.getElementById("<%=lblImgSize.ClientID %>").style.display = "none";
                document.getElementById("<%=txtImgSize.ClientID %>").style.display = "none";
            }

        }

        function checkInfo() {
            var msg = "";
 
            if (document.getElementById("<% = ddlPaymentType.ClientID%>").selectedIndex == 2)//刊前支付
            {
                if (document.getElementById("<% = txtDateBegin.ClientID%>").value == "")
                    msg += "请填写刊前支付的开始时间." + "\n";
                if (document.getElementById("<% = txtDateEnd.ClientID%>").value == "")
                    msg += "请填写刊前支付的结束时间." + "\n";
                var amount = document.getElementById('<%=txtAmountQ.ClientID %>').value;
                amount = amount.replace(',', '');

                if (!testNum(amount)) {
                    msg += "金额输入错误！" + "\n";
                }

                var preWordLength = document.getElementById('<%=txtPreWordLength.ClientID %>').value;
                preWordLength = preWordLength.replace(',', '');

                if (!testNum(preWordLength)) {
                    msg += "发稿字数输入错误！" + "\n";
                }
            }
            if (document.getElementById("<% = ddlPaymentType.ClientID%>").selectedIndex == 1)//刊后支付
            {
                if (document.getElementById("<% = txtTitle.ClientID%>").value == "")
                { msg += "请填写刊后支付的刊出标题." + "\n"; }
                if (document.getElementById("<% = txtWordLength.ClientID%>").value == "")
                { msg += "请填写刊后支付的字数." + "\n"; }
                else if (document.getElementById("<% = txtWordLength.ClientID%>").value.search(/^[-\+]?\d+(\.\d+)?$/) == -1) {
                    msg += "字数输入错误！" + "\n";
                }
                var imageradio = document.getElementById("<% = rdImageList.ClientID%>" + "_0");
                if (imageradio.checked == true) {
                    if (document.getElementById("<% = txtImgSize.ClientID%>").value == "")
                    { msg += "请填写图片尺寸." + "\n"; }
                    else if (document.getElementById("<% = txtImgSize.ClientID%>").value.search(/^[-\+]?\d+(\.\d+)?$/) == -1) {
                        msg += "图片尺寸输入错误！" + "\n";
                    }
                }
                if (document.getElementById("<% = txtReleaseDate.ClientID%>").value == "") {
                    msg += "请填写发稿日期！" + "\n";
                }
                var amount = document.getElementById('<%=txtAmount.ClientID %>').value;
                amount = amount.replace(',', '');
                if (!testNum(amount)) {
                    msg += "金额输入错误！" + "\n";
                }
                if ((document.getElementById("<% = filSow.ClientID%>").value == "" && document.getElementById("<% = lblFileName.ClientID%>").innerHTML == "") && document.getElementById("<% = txtHref.ClientID%>").value == "") {
                    msg += "请上传附件或输入联接！" + "\n";
                }
            }
            if (document.getElementById("<% = ddlPaymentType.ClientID%>").selectedIndex == 0) {
                msg += "请选择支付形式！" + "\n";
            }
            var mid = document.getElementById('<% = hidMediaID.ClientID %>').value;
            if (mid == null || mid == "" || mid == "undefined") {
                msg += "请选择媒体！" + "\n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else
                return true;
        }

        function selectPayType(id, text) {

            if (text == "刊前支付") {
                document.getElementById("<% =palH.ClientID %>").style.display = "none";
                document.getElementById("<% =palQ.ClientID %>").style.display = "block";
            }
            else if (text == "刊后支付") {
                document.getElementById("<% =palH.ClientID %>").style.display = "block";
                document.getElementById("<% =palQ.ClientID %>").style.display = "none";
            }
            else {
                document.getElementById("<% =palH.ClientID %>").style.display = "none";
                document.getElementById("<% =palQ.ClientID %>").style.display = "none";
            }
        }

        function showBirthday() {
            var val = document.getElementById("<% =txtIC.ClientID %>").value;
            var birthdayValue = "";
            if (15 == val.length) { //15位身份证号码
                birthdayValue = val.charAt(6) + val.charAt(7);
                if (parseInt(birthdayValue) < 10) {
                    birthdayValue = '20' + birthdayValue;
                }
                else {
                    birthdayValue = '19' + birthdayValue;
                }
                birthdayValue = birthdayValue + '-' + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11);
                return birthdayValue;
            }
            else if (18 == val.length) { //18位身份证号码
                birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11) + '-' + val.charAt(12) + val.charAt(13);

                return birthdayValue;
            }
            else {
                return '1900-01-01';
            }
        }

        function showNomal() {
            var tr1 = document.getElementById("tr1");
            var tr2 = document.getElementById("tr2");
            var tr3 = document.getElementById("tr3");
            var tr4 = document.getElementById("tr4");

            tr1.style.display = "";
            tr2.style.display = "";
            tr3.style.display = "none";
            tr4.style.display = "none";
        }
        function showImport() {
            var tr1 = document.getElementById("tr1");
            var tr2 = document.getElementById("tr2");
            var tr3 = document.getElementById("tr3");
            var tr4 = document.getElementById("tr4");

            tr1.style.display = "none";
            tr2.style.display = "none";
            tr3.style.display = "";
            tr4.style.display = "";
        }

    </script>

    <table width="100%" class="tableform">
        <tr>
            <td>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="oddrow">
                            稿件费用描述
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtDescription" runat="server" Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescription"
                                Display="None" ErrorMessage="稿件费用描述为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                        </td>
                    </tr>
                    <tr onclick="showNomal();">
                        <td class="heading" colspan="4" style="cursor: hand">
                            手动查询模式
                        </td>
                    </tr>
                    <tr id="tr1">
                        <td class="oddrow" style="width: 15%;">
                            媒体类型：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList runat="server" ID="ddlOption">
                                <asp:ListItem Text="请选择.." Value="0"></asp:ListItem>
                                <asp:ListItem Text="平面媒体" Value="1"></asp:ListItem>
                                <asp:ListItem Text="网络媒体" Value="2"></asp:ListItem>
                                <asp:ListItem Text="电视媒体" Value="3"></asp:ListItem>
                                <asp:ListItem Text="广播媒体" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 15%">
                            媒体名称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtMediaSelect" runat="server"></asp:TextBox><input type="hidden"
                                id="hidMediaID" runat="server" />
                            <input type="button" value="请选择..." class="widebuttons" onclick="return MediaClick();" /><font
                                color="red">点击请选择添加新媒体</font>
                        </td>
                    </tr>
                    <tr id="tr2">
                        <td class="oddrow" style="width: 15%">
                            记者：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtReporterSelect" runat="server"></asp:TextBox><input type="hidden"
                                id="hidReporterID" runat="server" />
                            <input type="button" value="请选择..." class="widebuttons" onclick="return ReporterClick();" /><font
                                color="red">点击请选择添加新记者</font>
                        </td>
                        <td class="oddrow" colspan="2" align="center">
                            <asp:Button ID="btnExport" runat="server" Text="模板导入模式" OnClick="btnExport_Click"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr onclick="showImport();">
                        <td class="heading" colspan="4" style="cursor: hand">
                            导出/导入模式
                        </td>
                    </tr>
                    <tr style="display: none" id="tr3">
                        <td colspan="4" class="oddrow">
                            <a href="WrittingFeeExport.aspx" target="_blank">导出记者信息模板</a>
                        </td>
                    </tr>
                    <tr style="display: none" id="tr4">
                        <td class="oddrow">
                            导入记者信息：
                        </td>
                        <td colspan="3">
                            <asp:FileUpload ID="fupReportersImport" runat="server" Width="300px" />&nbsp;&nbsp;<asp:Button
                                ID="btnUpload" runat="server" Text=" 上传 " CausesValidation="false" OnClick="btnUpload_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ID="pnlReporter" Style="display: none">
                    <table width="100%" border="0" class="tableform">
                        <tr>
                            <td class="heading" colspan="4">
                                记者信息
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                所属媒体：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="lblMedia" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="lblMedia"
                                    Display="None" ErrorMessage="媒体名称为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                记者姓名：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="lblReporter" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lblReporter"
                                    Display="None" ErrorMessage="记者为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                记者性别：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:DropDownList runat="server" ID="ddlSex">
                                    <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="男" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="女" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="保密" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                记者手机：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtReporterPhone" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReporterPhone"
                                    Display="None" ErrorMessage="记者手机为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="heading" colspan="4">
                                费用支付信息
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                收款人：
                            </td>
                            <td colspan="3" class="oddrow-l">
                                <asp:TextBox ID="txtBankAcountName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBankAcountName"
                                    Display="None" ErrorMessage="收款人为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                记者身份证号：
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtIC" runat="server" MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIC"
                                    Display="None" ErrorMessage="记者身份证号为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                                <br />
                                若收款人与记者不是同一人,请填写收款人身份证号
                            </td>
                            <td style="width: 15%" class="oddrow">
                                收款人所在城市：
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCity"
                                    Display="None" ErrorMessage="收款人所在城市为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                收款人联系方式：
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone"
                                    Display="None" ErrorMessage="收款人联系方式为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                            <td style="width: 15%" class="oddrow">
                                开户行全称：
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtBank" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBank"
                                    Display="None" ErrorMessage="开户行为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" class="oddrow">
                                帐号：
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAccount"
                                    Display="None" ErrorMessage="帐号为必填"></asp:RequiredFieldValidator><font color="red">*</font>
                            </td>
                            <td style="width: 15%" class="oddrow">
                                是否需要税单：
                            </td>
                            <td style="width: 30%" class="oddrow-l">
                                <asp:RadioButtonList runat="server" ID="radioTax" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                支付形式：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:DropDownList runat="server" ID="ddlPaymentType">
                                    <asp:ListItem Text="请选择.." Value="-1"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="刊后支付" />
                                    <asp:ListItem Value="1" Text="刊前支付" />
                                </asp:DropDownList>
                                <font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                代理发稿：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:RadioButtonList runat="server" ID="radioList" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palH" Style="display: none" runat="server">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td colspan="4" class="heading">
                                稿件发布信息 (刊后请填写如下信息)
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                稿件标题：
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow">
                                发稿日期：
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtReleaseDate" onkeyDown="return false; " Style="cursor: hand"
                                    runat="server" onclick="setDate(this);" /><font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                实际刊发字数：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox runat="server" ID="txtWordLength"></asp:TextBox><font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                支付金额：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                                <font color="red">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                是否配图：
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:RadioButtonList runat="server" ID="rdImageList" BorderStyle="None" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                <asp:Label runat="server" ID="lblImgSize">图片尺寸(平方厘米)：</asp:Label>
                            </td>
                            <td class="oddrow-l" style="width: 30%">
                                <asp:TextBox ID="txtImgSize" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                稿件附件：
                            </td>
                            <td colspan="3" class="oddrow-l">
                                <asp:FileUpload ID="filSow" runat="server" Width="400px" />&nbsp;<asp:Label ID="lblFileName"
                                    runat="server" />&nbsp;
                                <asp:CheckBox ID="chkdownSow" runat="server" Text="<img src='/images/ico_05.gif' border='0' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                稿件链接：
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtHref" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="palQ" Style="display: none" runat="server">
                    <table width="100%" class="tableForm">
                        <tr>
                            <td class="heading">
                                刊前请注明刊发时间：
                            </td>
                        </tr>
                        <tr>
                         <td class="oddrow" style="width: 15%">
                                刊发时间：
                            </td>
                            <td class="oddrow-l" colspan="3">
                                <asp:TextBox ID="txtDateBegin" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                    onclick="setDate(this);" /><font color="red"> *</font> --
                                <asp:TextBox ID="txtDateEnd" onkeyDown="return false; " Style="cursor: hand" runat="server"
                                    onclick="setDate(this);" /><font color="red"> *</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="oddrow" style="width: 15%">
                                支付金额：
                            </td>
                            <td class="oddrow-l" style="width: 35%">
                                <asp:TextBox ID="txtAmountQ" runat="server"></asp:TextBox>
                                <font color="red">*</font>
                            </td>
                            <td class="oddrow" style="width: 15%">
                                发稿字数：
                            </td>
                            <td class="oddrow-l" style="width: 35%">
                                <asp:TextBox ID="txtPreWordLength" runat="server"></asp:TextBox>
                                <font color="red">*</font>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <font color="red">新增记者请在上方的查询区域选择媒体和记者信息</font>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:LinkButton runat="server" ID="btnHid" OnClick="btnHid_onclick"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnHidReturn" OnClick="btnHidReturn_onclick"></asp:LinkButton>
                <input runat="server" id="btnAdd" value=" 保存并添加 " type="button" onclick="if(checkInfo() ){if(Page_ClientValidate())this.disabled=true;}else{return false;}"
                    onserverclick="btnAdd_onclick" class="widebuttons" causesvalidation="false" />&nbsp;
                <input runat="server" id="btnNext" value=" 保存并返回 " type="button" onclick="if(checkInfo() ){if(Page_ClientValidate())this.disabled=true;}else{return false;}"
                    onserverclick="btnSave_onclick" class="widebuttons" causesvalidation="false" />&nbsp;
                <input id="btnReturnNoSave" runat="server" class="widebuttons" onserverclick="btnReturnNoSave_onclick"
                    type="button" value=" 直接返回 " causesvalidation="false" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <input type="hidden" id="hidMediaOrderID" runat="server" />
                <input type="hidden" id="hidOrderID" runat="server" />
                <input type="hidden" id="hidEditPrice" runat="server" />
                <asp:Repeater runat="server" ID="rptParent" OnItemDataBound="rptParent_ItemDataBound">
                    <%--OnItemCommand="rptParent_ItemCommand">--%>
                    <HeaderTemplate>
                        <table class="gridView" cellspacing="0" cellpadding="4" border="0" style="background-color: White;
                            width: 100%; border-collapse: collapse;">
                            <tr class="Gheading" align="center">
                                <th scope="col">
                                    项目
                                </th>
                                <th scope="col">
                                    收款人
                                </th>
                                <th scope="col">
                                    身份证号
                                </th>
                                <th scope="col">
                                    支付金额
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="td" align="left" style="background-color: SkyBlue">
                            <td align="center" style="width: 10%;">
                                <%-- CommandName="Expand" CommandArgument='<%#Eval("ReporterID") %>' CommandName="Shrink" CommandArgument='<%#Eval("ReporterID") %>' --%>
                                <asp:HyperLink ID="btnViewChild" runat="server" ImageUrl="/images/Expand.gif" />
                                <asp:HyperLink ID="btnCancelChild" runat="server" ImageUrl="/images/Shrink.gif" />
                            </td>
                            <td align="center" style="width: 30%;">
                                <%#Eval("ReceiverName") %>
                            </td>
                            <td align="center" style="width: 30%;">
                                <%#Eval("CardNumber")%>
                            </td>
                            <td align="center" style="width: 30%;">
                                <%#Eval("totalamount")%>
                            </td>
                        </tr>
                        <tr class="td" align="left">
                            <td align="center" style="width: 10%;">
                            </td>
                            <td colspan="3">
                                <asp:GridView ID="GVChild" runat="server" AutoGenerateColumns="false" DataKeyNames="MeidaOrderID"
                                    OnRowDataBound="GVChild_RowDataBound" OnRowEditing="GVChild_RowEditing" OnRowCommand="GVChild_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReporterID" HeaderText="ReporterID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="MediaID" HeaderText="MediaID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Attachment" HeaderText="Attachment" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="wordLength" HeaderText="wordLength" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ReporterName" HeaderText="记者" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="MediaName" HeaderText="所属媒体" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Subject" HeaderText="发布主题" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" />
                                        <asp:TemplateField HeaderText="支付类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <%#Eval("PayType").ToString()=="1"?"刊后支付":"刊前支付" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="平均价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvgPrice" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="totalamount" HeaderText="支付金额" ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitPrice" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                                        <asp:BoundField DataField="WrittingURL" HeaderText="链接" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="25%" />
                                        <asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkDownLoad" runat="server" Text="<img src='/images/ico_04.gif' border='0'>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("MeidaOrderID") %>'
                                                    CommandName="Edit" Text="<img src='/images/edit.gif' title='编辑' border='0'>"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="删除" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("MeidaOrderID") %>'
                                                    CommandName="Del" Text="<img src='../../images/disable.gif' title='删除' border='0'>"
                                                    OnClientClick="return confirm('你确定删除吗？');" CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
        ShowMessageBox="true" />
</asp:Content>
