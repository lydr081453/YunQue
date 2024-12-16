<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Project_SetAuditor" Codebehind="SetAuditor.ascx.cs" %>
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
        var deptid = '<%=DeptID %>';
        var win = window.open('selectOperationAuditor.aspx?title=' + title + '&sysids=' + sysids + '&curU=' + currentUser + '&<% =ESP.Finance.Utility.RequestName.DeptID %>=' + deptid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }
    function openEmployee1(title) {
        var currentUser = '<%= CurrentUser.SysID %>';
        var deptid = '<%=DeptID %>';
        var win = window.open('selectOperationAuditor.aspx?title=' + title + '&sysids=0&curU=' + currentUser + '&<% =ESP.Finance.Utility.RequestName.DeptID %>=' + deptid, null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
        win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);
    }

    function addUser(control, userId, userName) {
        if (control == "FA") {
            var ids = document.getElementById('<%= hidFA.ClientID  %>').value;
            if (!checkExist(ids, userId)) {
                document.getElementById('<%= hidFA.ClientID  %>').value += userId + ",";
                document.getElementById('<%= lblFA.ClientID %>').innerHTML += userName + "<img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('FA','" + userId + "');\" />" + "&nbsp;";
            }
        }
        else if (control == "N1") {
            var ids = document.getElementById('<%= hidprejudication.ClientID  %>').value;
            if (!checkExist(ids, userId)) {
                document.getElementById('<%= hidprejudication.ClientID  %>').value += userId + ",";
                document.getElementById('<%= labPrejudication.ClientID %>').innerHTML += userName + "<img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('N1','" + userId + "');\" />" + "&nbsp;";
            }
        } else if (control == "N2") {
            var ids = document.getElementById('<%= hidAppend1.ClientID  %>').value;
            if (!checkExist(ids, userId)) {
                document.getElementById('<%= hidAppend1.ClientID  %>').value += userId + ",";
                document.getElementById('<%= labAppend1.ClientID %>').innerHTML += userName + "<img src=\"/images/ico_05.gif\" border=\"0\" onclick=\"removeUser('N2','" + userId + "');\" />" + "&nbsp;";
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
    
    function checkExist(ids, id) {
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
    
    function checkExistTotal(){
        var totalaudit="";
        var idres=document.getElementById('<%= hidResponser.ClientID %>').value;
        var divGeneral=document.getElementById("<%=palGeneral.ClientID %>");
            totalaudit += document.getElementById('<%= hidFA.ClientID %>').value;
            totalaudit += document.getElementById('<%= hidAddMajordomo.ClientID %>').value;
            totalaudit += document.getElementById('<%= hidprejudication.ClientID %>').value;
            if (divGeneral != null && (divGeneral.style.display == "block" || divGeneral.style.display == "")) {
                totalaudit += document.getElementById('<%= hidAddgeneral.ClientID %>').value;
                totalaudit += document.getElementById('<%= hidAppend1.ClientID %>').value;
            }
            var idsArr = totalaudit.split(",");
            var idsArr2 = totalaudit.split(",");
            var totalcount = 0;
            for (i = 0; i < idsArr.length; i++) {
            totalcount=0;
                for (j = 0; j < idsArr2.length; j++) {
                if(idsArr[i]==idres)
                {
                   totalcount++;
                }
                    if (idsArr[i] == idsArr2[j]) {
                        totalcount++;
                    }
                    if (totalcount > 1)
                        return true;
                }
                
            }

        return false;
    }

    function setHZUser(control, chk) {
        if (control == "N4") {
            var zh = document.getElementById('<%=hidZHMajordomo.ClientID %>').value.split(",");
            if (chk.checked) {
                document.getElementById('<%=hidZHMajordomo.ClientID %>').value += chk.value + ",";
                removeUserHZ(control, chk.value);
            } else {
                document.getElementById('<%=hidZHMajordomo.ClientID %>').value = "";    
                for (i = 0; i < (zh.length - 1); i++) {
                    if (zh[i] != chk.value) {
                        document.getElementById('<%=hidZHMajordomo.ClientID %>').value += zh[i] + ",";
                    }
                }
                addUserHZ(control, chk.value);
            }
          
        } else if (control == "N5") {
            var zh = document.getElementById('<%=hidZHgeneral.ClientID %>').value.split(",");
            if (chk.checked) {
                document.getElementById('<%=hidZHgeneral.ClientID %>').value += chk.value + ",";
                removeUserHZ(control, chk.value);
            } else {
                document.getElementById('<%=hidZHgeneral.ClientID %>').value = "";
                for (i = 0; i < (zh.length - 1); i++) {
                    if (zh[i] != chk.value) {
                        document.getElementById('<%=hidZHgeneral.ClientID %>').value += zh[i] + ",";
                    }
                }
                addUserHZ(control, chk.value);
            }
        }
    }

    function removeUserHZ(control, id) {
      if (control == "N4") {
            var ids = document.getElementById('<%= hidAddMajordomo.ClientID  %>').value.split(",");
            document.getElementById('<%= hidAddMajordomo.ClientID  %>').value = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if (ids[i] != id) {
                    document.getElementById('<%= hidAddMajordomo.ClientID  %>').value += ids[i] + ",";
                }
            }
        }
        else if (control == "N5") {
            var ids = document.getElementById('<%= hidAddgeneral.ClientID  %>').value.split(",");
            document.getElementById('<%= hidAddgeneral.ClientID  %>').value = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if (ids[i] != id) {
                    document.getElementById('<%= hidAddgeneral.ClientID  %>').value += ids[i] + ",";
                }
            }
        }
    }

    function addUserHZ(control, id) {
        if (control == "N4") {
            var ids = document.getElementById('<%= hidAddMajordomo.ClientID  %>').value;
            if (!checkExist(ids, id)) {
                document.getElementById('<%= hidAddMajordomo.ClientID  %>').value += id + ",";
           }
        } else if (control == "N5") {
            var ids = document.getElementById('<%= hidAddgeneral.ClientID  %>').value;
            if (!checkExist(ids, id)) {
                document.getElementById('<%= hidAddgeneral.ClientID  %>').value += id + ",";
           }
        }
    }
    
    function removeUser(control, id) {
    if(control=="FA")
    {
            var ids = document.getElementById('<%= hidFA.ClientID  %>').value.split(",");
            var names = document.getElementById('<%= lblFA.ClientID %>').innerHTML.split("&nbsp;");
            document.getElementById('<%= hidFA.ClientID  %>').value = "";
            document.getElementById('<%= lblFA.ClientID %>').innerHTML = "";
            for (i = 0; i < (ids.length - 1); i++) {
                if(ids[i] != id){
                    document.getElementById('<%= hidFA.ClientID  %>').value += ids[i] + ",";
                    document.getElementById('<%= lblFA.ClientID %>').innerHTML += names[i] + "&nbsp;";
                }
            }
    }
     else  if (control == "N1") {
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
        if (document.getElementById('<%= hidFA.ClientID %>').value == "") {
            msg += "- 请添加FA审核人 \r\n";
        }
        if (document.getElementById('<%= hidAddMajordomo.ClientID %>').value == "") {
            msg += "- 请添加总监审核人 \r\n";
        }
        if (document.getElementById('<%= palGeneral.ClientID %>') != null) {
            if (document.getElementById('<%= hidAddgeneral.ClientID %>').value == "" ) {
                msg += "- 请添加总经理审核人 \r\n";
            }
        }
        if (checkExistTotal())
        {
                msg += "- 请检查审核人，有重复 \r\n";
        }
        if (msg != "") {
            alert(msg);
            return false;
        }
        return true;
    }

</script>
<table width="100%" border="0" cellpadding="10" cellspacing="1" bgcolor="#eaedf1">
  <tr>
    <td bgcolor="#FFFFFF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
        <td width="10%" height="40" align="center" background="/images/s_01_03.jpg" class="post">FA<font color="red"> * </font></td></td>
        <td width="85%" class="name"><asp:Label ID="lblFA" runat="server" /><asp:HiddenField ID="hidFA" runat="server" /><asp:HiddenField ID="hidResponser" runat="server" /></td>
        <td width="5%" class="Add_btn"><a href="#"><img src="/images/s_01_10.jpg" width="50" height="20" border="0" onclick="openEmployee1('FA');return false;" /></a></td>
      </tr>
      <tr>
        <td align="center" valign="top"><img src="/images/s_01_06.jpg" width="13" height="8" style="margin-bottom:5px;"></td>
        <td></td>
        <td></td>
      </tr>
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
    </table>
<br /> 
<table class="XTable" width="100%">
  <tr>
    <td class="oddrow-l">
    <input runat="server" id="btnSave" value="设置并提交"  type="button" onclick="if (commitCheck()) { showLoading(); } else { return false; }" causesvalidation="false" class="widebuttons" onserverclick="btnSave_Click" />
    <asp:Button ID="btnReturn" runat="server" Text=" 返回 " onclick="btnReturn_Click" cssClass="widebuttons"  />
    </td>
  </tr>
</table>