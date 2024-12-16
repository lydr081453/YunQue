<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsuranceCompensationEdit.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SEPAdmin.HR.Employees.InsuranceCompensationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../public/js/jquery.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/dialog.js"></script>

    <script language="javascript" type="text/javascript" src="../../public/js/jquery-ui.js"></script>
    <script type="text/javascript">
        var val = "";
        $(document).ready(function() {
            $("#btnAddUsersDialog1").click(function() {
                if (!document.getElementById("btnAddUsersDialog1").__sep_dialog) {
                    document.getElementById("btnAddUsersDialog1").__sep_dialog = $("#userList1").dialog({
                        modal: true, overlay: { opacity: 0.5, background: "black" },
                        height: 500, width: 800,
                        buttons: {
                            "取消": function() { $(this).dialog("close"); },
                            "确定": function() {
                                var hiddenId = $("#btnAddUsersDialog1").attr("server-hidden-id");
                                var selects = document.getElementById("userListFrame1").contentWindow.__sep_dialogReturnValue;
                                if (selects && selects != "") {
                                    var str = selects.substring(0, selects.indexOf(","));
                                    $("#" + hiddenId).val(str);
                                    document.getElementById("<%=labUsers.ClientID%>").innerHTML =
                                     str.split('-')[1];
                                }
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                document.getElementById("btnAddUsersDialog1").__sep_dialog.dialog("open");


            });
        });

        function vals() {
            var userid = document.getElementById("<%= hidUserID.ClientID %>").value;
            alert(userid);         
            if (userid == "0" || userid == "") {
                alert("请添加员工");
                return false;
            }
        }
     </script>
    <div>       
            
        <table id="tab1" runat="server" width="100%" >
        <tr>
            <td class="oddrow" style="width: 20%">员工姓名：</td>
            <td class="oddrow-l" >
               <asp:Label ID="labUsers" runat="server"></asp:Label></td>
            <td class="oddrow-l" style="width: 20%">
               <input type="button" value="添加用户" id="btnAddUsersDialog1" class="widebuttons" 
            server-hidden-id="<% = hidUserID.ClientID %>" />
            <input type="hidden" id="hidUserID" value="" runat="server" /> 
            </td>
            <td class="oddrow-l" colspan="3" ></td> 
         </tr>   
        </table> 
        <table width="100%" class="tableForm">   
        <tr>
            <td class="oddrow" style="width: 20%">补交时间</td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="drpYear" runat="server"></asp:DropDownList>年
                            <asp:DropDownList ID="drpMonth" runat="server"></asp:DropDownList>月
                        </td> 
             <td class="oddrow-l" colspan="4" >            
         </tr>       
            <tr>                     
                <td class="oddrow" style="width: 20%">
                    养老保险费用:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtEndowmentInsurance" runat="server"   />元
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" 
                        ControlToValidate="txtEndowmentInsurance" Display="Dynamic" ErrorMessage="请输入正确养老保险费用" 
                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确养老保险费用</asp:RegularExpressionValidator>
                </td> 
                <td class="oddrow" style="width: 20%">
                    失业保险费用:
                </td>
                <td class="oddrow-l" >
                    <asp:TextBox ID="txtUnemploymentInsurance" runat="server"  />元
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" 
                        ControlToValidate="txtUnemploymentInsurance" Display="Dynamic" ErrorMessage="请输入正确失业保险费用" 
                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确失业保险费用</asp:RegularExpressionValidator>
                </td>       
                <td class="oddrow" style="width: 20%">
                    医疗保险费用:
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtMedicalInsurance" runat="server"  />元
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" 
                        ControlToValidate="txtMedicalInsurance" Display="Dynamic" ErrorMessage="请输入正确医疗保险费用" 
                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确医疗保险费用</asp:RegularExpressionValidator>
                </td>                                                                 
            </tr>                     
            <tr>                  
                <td class="oddrow" style="width: 20%">
                    公积金费用:
                </td>
                <td class="oddrow-l" >
                    <asp:TextBox ID="txtPublicReserveFunds" runat="server" />元
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" 
                        ControlToValidate="txtPublicReserveFunds" Display="Dynamic" ErrorMessage="请输入正确公积金费用" 
                        ValidationExpression="^(\d{1,3},?)+(\.\d+)?$">请输入正确公积金费用</asp:RegularExpressionValidator>
                </td>                                                     
                <td class="oddrow-l" colspan="4" >                            
                 </td>                                       
            </tr>  
            <tr>
            <td class="oddrow" colspan="6" >
                备注:
            </td>
        </tr>
        <tr  >
            <td class="oddrow-l" colspan="6" >
                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Height="100px" Width="100%" runat="server"  />
            </td>
        </tr>                                                        
    </table>
    <table width="100%">
	    <tr>
	        <td>
	            <asp:Button ID="btnSave" runat="server" CssClass="widebuttons" OnClientClick="return vals();" OnClick="btnSave_Click" Text=" 保 存 " />
	            &nbsp;&nbsp;&nbsp;
	            <asp:Button ID="btnReturn" runat="server" CssClass="widebuttons" OnClick="btnReturn_Click" Text=" 返 回 " />
	        </td>
	    </tr>
	</table>	
    </div>
    <div style="width: 0px; height: 0px; overflow: hidden" class="oddrow">
        <div id="userList1">
            <iframe src="UserList.aspx" id="userListFrame1" height="90%" width="100%"></iframe>
        </div>
    </div> 
</asp:Content>
