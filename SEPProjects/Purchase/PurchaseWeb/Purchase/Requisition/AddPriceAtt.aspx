<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="AddPriceAtt.aspx.cs" Inherits="AddPriceAtt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/public/css/dialog.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script language="javascript" src="/public/js/dialog.js"></script>
    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>
    <script type="text/javascript">
        //        function validtext() {
        //            var msg = "";
        //            if (msg != "") {
        //                alert(msg);
        //                return false;
        //            }
        //            else
        //                return true;
        //        }

        //        function testNum(a) {
        //            a += "";
        //            a = a.replace(/(^[\\s]*)|([\\s]*$)/g, "");
        //            if (a != "" && !isNaN(a) && Number(a) > 0) {
        //                return true;
        //            }
        //            else {
        //                return false;
        //            }
        //        }

        //        function loadpage() {
        //            parent.$('#floatBoxBg').hide(); 
        //            parent.$('#floatBox').hide();
        //        }
        function vnameClick(vinfo) {
            ShowMsg(vinfo);
        }
        function nameClick(spid, Url) {
            if (Url == 1) {
                dialog("供应商资质信息", "iframe:/supplierchain/Question/AuditedPersonQuestionView.aspx?backUrl=floatBox&sid=" + spid, "1000px", "650px", "text");
            }
            if (Url == 2) {
                dialog("供应商信息", "iframe:/supplierchain/ManageInfo/PersonDetailInfoView.aspx?backUrl=floatBox&sid=" + spid, "700px", "500px", "text");
            }
            if (Url == 3) {
                dialog("供应商资质信息", "iframe:/supplierchain/Question/AuditedSupplierQuestionView.aspx?backUrl=floatBox&sid=" + spid, "1000px", "650px", "text");
            }
            if (Url == 4) {
                dialog("供应商信息", "iframe:/supplierchain/ManageInfo/SupplierDetailInfoView.aspx?backUrl=floatBox&sid=" + spid, "700px", "500px", "text");
            }
            //            alert(keys);
        }
    </script>
    <style type="text/css">
        /* Following are tab control styles, Special for Outsourcing Set-up Process Page */ /* Default tab */

        .AjaxTabStrip .ajax__tab_tab {
            font-size: 12px;
            padding: 4px;
            width: 105px; /* Your proper width */
            height: 20px; /* Your proper height */
            background-color: #EAEAEA;
        }
        /* When mouse over */ .AjaxTabStrip .ajax__tab_hover .ajax__tab_tab {
            font-weight: bold;
            text-decoration: underline;
        }
        /* Current selected tab */ .AjaxTabStrip .ajax__tab_active .ajax__tab_tab {
            background-color: #C2E2ED;
            font-weight: bold;
        }
        /* TabPanel Content */ .AjaxTabStrip .ajax__tab_body {
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
            margin-right: 9px; /* Your proper right-margin, make your header and the content have the same width */
            margin-top: 0px;
        }

        .border {
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-bottom-color: #CC3333;
            border-left-color: #CC3333;
        }

        .border2 {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
        }

        .border_title_left {
            border-left-width: 1px;
            border-left-style: solid;
            border-left-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_title_right {
            border-right-width: 1px;
            border-right-style: solid;
            border-right-color: #CC3333;
            border-top-width: 1px;
            border-top-style: solid;
            border-top-color: #CC3333;
        }

        .border_datalist {
            border-right-width: 1px;
            border-left-width: 1px;
            border-right-style: solid;
            border-left-style: solid;
            border-right-color: #CC3333;
            border-left-color: #CC3333;
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #CC3333;
        }

        .userLabel {
            cursor: pointer;
            text-decoration: none;
            color: #7282A9;
        }
    </style>
    <div style="overflow-y: scroll; overflow-x: hidden; width: 950px; height: 450px">
        <asp:ScriptManager ID="manager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <uc1:TabContainer ID="TabContainer1" runat="server" CssClass="AjaxTabStrip" Width="98%"
            ActiveTabIndex="0">
            <uc1:TabPanel ID="TabPanel2" HeaderText="添加本地附件" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="oddrow">报价附件:
                            </td>
                            <td class="oddrow-l">
                                <asp:FileUpload ID="fil2" runat="server" Width="330px" Style="display: none" />
                                <input type="button" id="btnLocalUp" value="添加附件" class="widebuttons" onclick="addFileControl('spBJ2', 'filBJ2');" />&nbsp;供应商报价
                    <br />
                                <span id="spBJ2" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hFileSize" runat="server" Value="0" />
                    <asp:HiddenField ID="hfile" runat="server" Value="0" />
                    <asp:Button ID="btnAdd1" Text="添 加" runat="server" OnClick="btnAdd1_click" CssClass="widebuttons" />
                    <asp:Label ID="lblFileSize" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </uc1:TabPanel>
        </uc1:TabContainer>
    </div>
    <script language="javascript">
        function addFileControl(control, name) {
            var spBJ = document.getElementById(control);
            var childCount = 1;
            for (i = 0; i < spBJ.childNodes.length; i++) {
                if (spBJ.childNodes[i].type == "file")
                    childCount++;
            }
            //            if (childCount != 1 && (childCount % 3) == 0) {
            //                insertHtml("beforeEnd", spBJ, "<input type='file' name='"+name+"' /><br />");
            //            }
            //            else
            //                insertHtml("beforeEnd", spBJ, "<input type='file' name='" + name + "' />");
            if (childCount != 1 && (childCount % 3) == 0) {
                insertHtml("beforeEnd", spBJ, "<input type='file' name='" + name + "' id='" + name + childCount + "'  onchange='validateFileSize(\"" + name + childCount + "\",\"4096000\",\"" + childCount + "\");'/><asp:HiddenField ID='hsize" + childCount + "'/><br />");
            }
            else
                insertHtml("beforeEnd", spBJ, "<input type='file' name='" + name + "' id='" + name + childCount + "' onchange='validateFileSize(\"" + name + childCount + "\",\"4096000\",\"" + childCount + "\");'/><asp:HiddenField ID='hsize" + childCount + "'/>");
        }
        function insertHtml(where, el, html) {
            where = where.toLowerCase();
            if (el.insertAdjacentHTML) {
                switch (where) {
                    case "beforebegin":
                        el.insertAdjacentHTML('BeforeBegin', html);
                        return el.previousSibling;
                    case "afterbegin":
                        el.insertAdjacentHTML('AfterBegin', html);
                        return el.firstChild;
                    case "beforeend":
                        el.insertAdjacentHTML('BeforeEnd', html);
                        return el.lastChild;
                    case "afterend":
                        el.insertAdjacentHTML('AfterEnd', html);
                        return el.nextSibling;
                }
                throw 'Illegal insertion point -> "' + where + '"';
            }
            var range = el.ownerDocument.createRange();
            var frag;
            switch (where) {
                case "beforebegin":
                    range.setStartBefore(el);
                    frag = range.createContextualFragment(html);
                    el.parentNode.insertBefore(frag, el);
                    return el.previousSibling;
                case "afterbegin":
                    if (el.firstChild) {
                        range.setStartBefore(el.firstChild);
                        frag = range.createContextualFragment(html);
                        el.insertBefore(frag, el.firstChild);
                        return el.firstChild;
                    } else {
                        el.innerHTML = html;
                        return el.firstChild;
                    }
                case "beforeend":
                    if (el.lastChild) {
                        range.setStartAfter(el.lastChild);
                        frag = range.createContextualFragment(html);
                        el.appendChild(frag);
                        return el.lastChild;
                    } else {
                        el.innerHTML = html;
                        return el.lastChild;
                    }
                case "afterend":
                    range.setStartAfter(el);
                    frag = range.createContextualFragment(html);
                    el.parentNode.insertBefore(frag, el.nextSibling);
                    return el.nextSibling;
            }
            throw 'Illegal insertion point -> "' + where + '"';
        }
        function validateFileSize(id, maxsize, num) {
            var hfile = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_hfile');
            hfile.value = num;//隐藏控件，记录自动添加上传控件的Id数
            var filepath = "";
            var fileupload = document.getElementById(id);
            if (fileupload.value.length < 5) { alert('请选择文件！'); return; }
            var agent = window.navigator.userAgent;
            if (document.all) {
                var isIE7 = agent.indexOf('MSIE 7.0') != -1;
                var isIE8 = agent.indexOf('MSIE 8.0') != -1;
                //IE7和IE8获得文件路径
                if (isIE7 || isIE8) {
                    fileupload.select();
                    filepath = document.selection.createRange().text;
                }
                    //IE6获得文件路径
                else { filepath = file.value; }
                PageMethods.ValidateFile(filepath, maxsize, ieCallBack);
            }
            if (agent.indexOf("Firefox") >= 1) {
                if (fileupload.files) {
                    var size = fileupload.files[0].fileSize;
                    if (size > parseInt(maxsize)) {
                        var btn1 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_btnAdd1');
                        btn1.disabled = true;
                        var btnLocalUp = document.getElementById('btnLocalUp');
                        btnLocalUp.disabled = true;
                        alert("文件超过4M大小！");
                    }
                    else {
                        var btn1 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_btnAdd1');
                        btn1.disabled = false;
                        var btnLocalUp = document.getElementById('btnLocalUp');
                        btnLocalUp.disabled = false;
                    }
                }
            }
        }
        function ieCallBack(response) {
            var btn1 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_btnAdd1');
            var btnLocalUp = document.getElementById('btnLocalUp');
            var hFilSize = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_hFileSize');
            var lblFilSize = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_lblFileSize');//显示总大小
            var hfile = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabPanel2_hfile');//页面中保存的自动添加的id值
            var hsize = document.getElementById('hsize' + hfile.value);//通过（名字+隐藏控件id数）得到真实自动添加的隐藏控件Id值，后得到其隐藏控件中保存的文件大小
            hsize.value = response; //隐藏控件，记录自动添加上传控件的文件大小
            //            alert(hsize.value);
            //如果单个文件小于4m，怎不提示，并且增加和添加按钮均可用；如果单个文件大于4m，则提示，并且增加和添加按钮均不可用；
            if (response > 4096000) {
                btn1.disabled = true;
                btnLocalUp.disabled = true;
                alert("单个文件超过4M大小限制！");
            }
            else {
                btn1.disabled = false;
                btnLocalUp.disabled = false;
            }


            //循环计算当前所有添加的附件的总大小
            var spBJ = document.getElementById('spBJ2');
            //            alert(spBJ.childNodes.length);
            var alls = 0;
            for (i = 1; i <= spBJ.childNodes.length / 2; i++) {
                var hs = document.getElementById('hsize' + i);
                alls += parseInt(hs.value);
            }
            //            alert(alls);
            lblFilSize.innerHTML = "总大小：" + changeTwoDecimal_f(alls / 1024 / 1024) + "M";
            //            //如果总大小大于4m，则提示，并且增加和添加按钮均不可用；
            //            if (alls > 4096000) {
            //                alert("一次上传的多个文件总大小超过了4M！");
            //                btnLocalUp.disabled = true;
            //                btn1.disabled = true;
            //            }
        }

        function changeTwoDecimal_f(x) { //保留2位小数
            var f_x = parseFloat(x);
            if (isNaN(f_x)) {
                alert('function:changeTwoDecimal->parameter error');
                return false;
            }
            var f_x = Math.round(x * 100) / 100;
            var s_x = f_x.toString();
            var pos_decimal = s_x.indexOf('.');
            if (pos_decimal < 0) {
                pos_decimal = s_x.length;
                s_x += '.';
            }
            while (s_x.length <= pos_decimal + 2) {
                s_x += '0';
            }
            return s_x;
        }
    </script>
</asp:Content>
