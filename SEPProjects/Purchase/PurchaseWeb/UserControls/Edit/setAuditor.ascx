<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Edit_setAuditor" Codebehind="setAuditor.ascx.cs" %>
<style type="text/css">
.post {
	color:#333333;
	font-size:12px;
	font-weight:bold;
}
.name {
	font-size:12px;
	color:#333333;
	padding-left:10px;
	border-top-width: 1px;
	border-bottom-width: 1px;
	border-top-style: solid;
	border-bottom-style: solid;
	border-top-color: #eaedf1;
	border-bottom-color: #eaedf1;
}
.Add_btn {
	border-top-width: 1px;
	border-bottom-width: 1px;
	border-right-width: 1px;
	border-top-style: solid;
	border-bottom-style: solid;
	border-right-style: solid;
	border-top-color: #eaedf1;
	border-bottom-color: #eaedf1;
	border-right-color: #eaedf1;
}
</style>
    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>
<script language="javascript">

    function openEmployee(title, sysids) {
        var currentUser = '<%= CurrentUser.SysID %>';
        var win = window.open('selectOperationAuditor.aspx?title='+title+'&sysids='+sysids+'&curU='+currentUser, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function openEmployee1(title) {
        var currentUser = '<%= CurrentUser.SysID %>';
        var win = window.open('selectOperationAuditor.aspx?title=' + title+'&sysids=0&curU='+currentUser, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function addUser(control, userId, userName) {
        if (control == "N1") { 
            var ids = document.getElementById('<%= hidprejudication.ClientID  %>').value;
            if(!checkExist(ids,userId)){
                document.getElementById('<%= hidprejudication.ClientID  %>').value += userId + ",";
                document.getElementById('<%= labPrejudication.ClientID %>').innerHTML += userName + "<img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('N1','" + userId + "');\" />" + "&nbsp;";
            }
        } else if (control == "N2") {
            var ids = document.getElementById('<%= hidAppend1.ClientID  %>').value;
            if(!checkExist(ids,userId)){
                document.getElementById('<%= hidAppend1.ClientID  %>').value += userId + ",";
                document.getElementById('<%= labAppend1.ClientID %>').innerHTML += userName + "<img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('N2','" + userId + "');\" />" + "&nbsp;";
            }
        } else if (control == "N3") {
            var ids = document.getElementById('<%= hidAppend2.ClientID  %>').value;
            if(!checkExist(ids,userId)){
                document.getElementById('<%= hidAppend2.ClientID  %>').value += userId + ",";
                document.getElementById('<%= labAppend2.ClientID %>').innerHTML += userName + "<img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('N3','" + userId + "');\" />" + "&nbsp;";
            }
        } else if (control == "N4") {
            var ids = document.getElementById('<%= hidAddMajordomo.ClientID  %>').value;
            if (!checkExist(ids, userId)) {
                document.getElementById('<%= hidAddMajordomo.ClientID  %>').value += userId + ",";
                document.getElementById('<%= labAddMajordomo.ClientID %>').innerHTML += userName + "<input type='checkbox' value='" + userId + "' onclick='setHZUser(\"N4\",this);' title='是否为知会人员' /><img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('N4','" + userId + "');\" />" + "&nbsp;";
            }
        } else if (control == "N5") {
            var ids = document.getElementById('<%= hidAddgeneral.ClientID  %>').value;
            if (!checkExist(ids, userId)) {
                document.getElementById('<%= hidAddgeneral.ClientID  %>').value += userId + ",";
                document.getElementById('<%= labAddgeneral.ClientID %>').innerHTML += userName + "<input type='checkbox' value='" + userId + "' onclick='setHZUser(\"N5\",this);' title='是否为知会人员' /><img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('N5','" + userId + "');\" />" + "&nbsp;";
            }
        }
    }
    
    function checkExist(ids, id){
        if (ids.length > 0) {
            var idsArr = ids.split(",");
            for (i = 0; i < idsArr.length; i++) {
                if (idsArr[i] == id) {
                    return true;
                }
            }
        }
        return false;
    }

    function setHZUser(control, chk) {
        if (control == "N4") {
            var zh = document.getElementById('<%=hidZHMajordomo.ClientID %>').value.split(",");
            if (chk.checked) {
                document.getElementById('<%=hidZHMajordomo.ClientID %>').value += chk.value + ",";
            } else {
                document.getElementById('<%=hidZHMajordomo.ClientID %>').value = "";    
                for (i = 0; i < (zh.length - 1); i++) {
                    if (zh[i] != chk.value) {
                        document.getElementById('<%=hidZHMajordomo.ClientID %>').value += zh[i] + ",";
                    }
                }
            }
        } else if (control == "N5") {
            var zh = document.getElementById('<%=hidZHgeneral.ClientID %>').value.split(",");
            if (chk.checked) {
                document.getElementById('<%=hidZHgeneral.ClientID %>').value += chk.value + ",";
            } else {
                document.getElementById('<%=hidZHgeneral.ClientID %>').value = "";
                for (i = 0; i < (zh.length - 1); i++) {
                    if (zh[i] != chk.value) {
                        document.getElementById('<%=hidZHgeneral.ClientID %>').value += zh[i] + ",";
                    }
                }
            }
        }
    }

    function removeUser(control, id) {
        if (control == "N1") {
            var ids = document.getElementById('<%= hidprejudication.ClientID  %>').value.split(",");
            var names = document.getElementById('<%= labPrejudication.ClientID %>').innerHTML.split("&nbsp;");
            document.getElementById('<%= hidprejudication.ClientID  %>').value = "";
            document.getElementById('<%= labPrejudication.ClientID %>').innerHTML = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if(ids[i] != id){
                    document.getElementById('<%= hidprejudication.ClientID  %>').value += ids[i] + ",";
                    document.getElementById('<%= labPrejudication.ClientID %>').innerHTML += names[i] + "&nbsp;";
                }
            }
        }
        else if(control == "N2"){
            var ids = document.getElementById('<%= hidAppend1.ClientID  %>').value.split(",");
            var names = document.getElementById('<%= labAppend1.ClientID %>').innerHTML.split("&nbsp;");
            document.getElementById('<%= hidAppend1.ClientID  %>').value = "";
            document.getElementById('<%= labAppend1.ClientID %>').innerHTML = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if (ids[i] != id) {
                    document.getElementById('<%= hidAppend1.ClientID  %>').value += ids[i] + ",";
                    document.getElementById('<%= labAppend1.ClientID %>').innerHTML += names[i] + "&nbsp;";
                }
            }
        }
        else if(control == "N3"){
            var ids = document.getElementById('<%= hidAppend2.ClientID  %>').value.split(",");
            var names = document.getElementById('<%= labAppend2.ClientID %>').innerHTML.split("&nbsp;");
            document.getElementById('<%= hidAppend2.ClientID  %>').value = "";
            document.getElementById('<%= labAppend2.ClientID %>').innerHTML = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if (ids[i] != id) {
                    document.getElementById('<%= hidAppend2.ClientID  %>').value += ids[i] + ",";
                    document.getElementById('<%= labAppend2.ClientID %>').innerHTML += names[i] + "&nbsp;";
                }
            }
        }
        else if (control == "N4") {
            var ids = document.getElementById('<%= hidAddMajordomo.ClientID  %>').value.split(",");
            var names = document.getElementById('<%= labAddMajordomo.ClientID %>').innerHTML.split("&nbsp;");
            document.getElementById('<%= hidAddMajordomo.ClientID  %>').value = "";
            document.getElementById('<%= labAddMajordomo.ClientID %>').innerHTML = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if (ids[i] != id) {
                    document.getElementById('<%= hidAddMajordomo.ClientID  %>').value += ids[i] + ",";
                    document.getElementById('<%= labAddMajordomo.ClientID %>').innerHTML += names[i] + "&nbsp;";
                }
            }
            var ZHids = document.getElementById('<%= hidZHMajordomo.ClientID  %>').value.split(",");
            for(i = 0;i < (ZHids.length -1); i++){
                if (ZHids[i] != id) {
                    document.getElementById('<%= hidZHMajordomo.ClientID  %>').value += ZHids[i] + ",";
                }
            }
        }
        else if (control == "N5") {
            var ids = document.getElementById('<%= hidAddgeneral.ClientID  %>').value.split(",");
            var names = document.getElementById('<%= labAddgeneral.ClientID %>').innerHTML.split("&nbsp;");
            document.getElementById('<%= hidAddgeneral.ClientID  %>').value = "";
            document.getElementById('<%= labAddgeneral.ClientID %>').innerHTML = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if (ids[i] != id) {
                    document.getElementById('<%= hidAddgeneral.ClientID  %>').value += ids[i] + ",";
                    document.getElementById('<%= labAddgeneral.ClientID %>').innerHTML += names[i] + "&nbsp;";
                }
            }
            var ZHids = document.getElementById('<%= hidZHgeneral.ClientID  %>').value.split(",");
            for (i = 0; i < (ZHids.length - 1); i++) {
                if (ZHids[i] != id) {
                    document.getElementById('<%= hidZHgeneral.ClientID  %>').value += ZHids[i] + ",";
                }
            }
        }
    }

    function commitCheck() {
        var msg = "";
        if (document.getElementById('<%= hidAddMajordomo.ClientID %>').value == "" || (document.getElementById('<%= hidAddMajordomo.ClientID %>').value.split(',').length == document.getElementById('<%=hidZHMajordomo.ClientID %>').value.split(',').length)) {
            msg += "- 请添加总监审核人 \r\n";
        }
        if (document.getElementById('<%= palGeneral.ClientID %>') != null) {
            if (document.getElementById('<%= hidAddgeneral.ClientID %>').value == "" || (document.getElementById('<%= hidAddgeneral.ClientID %>').value.split(',').length == document.getElementById('<%=hidZHgeneral.ClientID %>').value.split(',').length)) {
                msg += "- 请添加总经理审核人 \r\n";
            }
        }
        if (msg != "") {
            alert(msg);
            return false;
        }
        return true;
    }


//    $().ready(function() {
//        if ("<% =Request[RequestName.BillID] %>" == null || "<% =generalInfo.PRType %>"!="<% =(int)PRTYpe.MediaPR %>") {
//            var tr = document.getElementById("trMedia");
//            tr.style.display = "none";
//        }
//    });
</script>
<table width="100%" border="0" cellpadding="10" cellspacing="1" bgcolor="#eaedf1">
  <tr>
    <td bgcolor="#FFFFFF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="10%" height="40" align="center" background="/images/s_01_03.jpg" class="post">预审人</td>
        <td width="85%" class="name"><asp:Label ID="labPrejudication" runat="server" /><asp:HiddenField ID="hidprejudication" runat="server" /></td>
        <td width="5%" class="Add_btn"><a href="#"><img src="/images/s_01_10.jpg" width="50" height="20" border="0" onclick="openEmployee1('N1');return false;" /></a></td>
      </tr>
      <tr>
        <td align="center" valign="top"><img src="/images/s_01_06.jpg" width="13" height="8" style="margin-bottom:5px;"></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td height="40" align="center" background="/images/s_01_03.jpg" class="post">总监<font color="red"> * </font></td>
        <td class="name"><asp:Label ID="labAddMajordomo" runat="server" /><asp:HiddenField ID="hidAddMajordomo" runat="server" /><asp:HiddenField ID="hidZHMajordomo" runat="server" /></td>
        <td class="Add_btn"><a href="#" runat="server" id="AddMajordomo"><img src="/images/s_01_10.jpg"  width="50" height="20" border="0" /></a></td>
      </tr>
      <asp:Panel ID="palGeneral" runat="server">
      <tr>
        <td align="center" valign="top"><img src="/images/s_01_06.jpg" width="13" height="8" style="margin-bottom:5px;" /></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td height="40" align="center" background="/images/s_01_03.jpg" class="post">附加审核</td>
        <td class="name"><asp:Label ID="labAppend1" runat="server" /><asp:HiddenField ID="hidAppend1" runat="server" /></td>
        <td class="Add_btn"><a href="#" onclick="openEmployee1('N2');return false;"><img src="/images/s_01_10.jpg" width="50" height="20" border="0" /></a></td>
      </tr>
      <tr>
        <td align="center" valign="top"><img src="/images/s_01_06.jpg" width="13" height="8" style="margin-bottom:5px;" /></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td height="40" align="center" background="/images/s_01_03.jpg" class="post">总经理<font color="red"> * </font></td>
        <td class="name"><asp:Label ID="labAddgeneral" runat="server" /><asp:HiddenField ID="hidAddgeneral" runat="server" /><asp:HiddenField ID="hidZHgeneral" runat="server" /></td>
        <td class="Add_btn"><a href="#" id="Addgeneral" runat="server"><img src="/images/s_01_10.jpg" width="50" height="20" border="0" /></a></td>
      </tr>
      </asp:Panel>
      <asp:Panel ID="palCEO" runat="server">
      <tr>
        <td align="center" valign="top"><img src="/images/s_01_06.jpg" width="13" height="8" style="margin-bottom:5px;" /></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td height="40" align="center" background="/images/s_01_03.jpg" class="post">附加审核</td>
        <td class="name"><asp:Label ID="labAppend2" runat="server" /><asp:HiddenField ID="hidAppend2" runat="server" /></td>
        <td class="Add_btn"><a href="#" onclick="openEmployee1('N3');return false;"><img src="/images/s_01_10.jpg" width="50" height="20" border="0" /></a></td>
      </tr>
      <tr>
        <td align="center" valign="top"><img src="/images/s_01_06.jpg" width="13" height="8" style="margin-bottom:5px;" /></td>
        <td></td>
        <td></td>
      </tr>
      <tr>
        <td height="40" align="center" background="/images/s_01_03.jpg" class="post">CEO</td>
        <td class="name"><asp:Label ID="labCEO" runat="server" /><asp:HiddenField ID="hidCEO" runat="server" /></td>
        <td class="Add_btn"><%--<a href="#"><img src="/images/s_01_10.jpg" width="50" height="20" border="0" /></a>--%></td>
      </tr>
      </asp:Panel>
    </table></td>
  </tr>
</table>
<br /> 
<table class="XTable" width="100%">
  <tr>
    <td class="oddrow-l"><input runat="server" id="btnSave" value="设置并提交"  type="button" onclick="if(commitCheck()){this.disabled=true;}else {return false;}" causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" /></td>
  </tr>
</table>