<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_MediaLinkReporterList"
    Title="记者列表" Codebehind="MediaLinkReporterList.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="uc" TagName="Experience" Src="~/Media/skins/Experience.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
       function check()
        {
             var meg="";

             
//           if(document.getElementById("<% =txtIdCard.ClientID %>").value!="")
//           {
//               if (document.getElementById("<% =txtIdCard.ClientID %>").value.search(/^\d{15}(\d{2}[A-Za-z0-9])?$/) == -1)
//                {
//                    meg += "身份证输入错误"+"\n";
//       
//                }
//            }
//            if(document.getElementById("<% =txtEmail.ClientID %>").value!=""){    
//                if (document.getElementById("<% =txtEmail.ClientID %>").value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) == -1)
//                {
//                    meg += "邮箱输入错误"+"\n";
//             
//                }
//            }
//            if(document.getElementById("<% =txtMobile.ClientID %>").value!=""){
//                if (document.getElementById("<% =txtMobile.ClientID %>").value.search(/^((\(\d{2,3}\))|(\d{3}\-))?13\d{9}$/) == -1)
//                {
//                   meg += "手机号码输入错误"+"\n";
//             
//                }
//            }
           
            if(meg!=""){
            alert(meg);
             return  false;
            }
        }
        
        function selected(obj)
        {
            if (obj.checked)
            {
                hide = document.getElementById("<% =hidChecked.ClientID %>");
                str = obj.value + ",";
                hide.value = hide.value.replace(str,"");
                hide.value += str;
            }
        }
    function linkcheck()
    {
     hide = document.getElementById("<% =hidChecked.ClientID %>");
       if( hide.value=="0")
       {
           alert("请选择记者");
           return false;
       }
    
    }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <input type="hidden" id="hidChecked" runat="server" value="0" />
    <table width="100%">
            <tr>
            <td> 
            <table class="tablehead">
        <tr>
            <td>
               <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                    ID="btnAddReporter" runat="server" class="bigfont" Text="添加新记者" OnClick="btnAdd_Click" />
                    </td>
        </tr>
        </table>
                    </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="1" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="20%">
                            记者姓名：
                        </td>
                        <td class="oddrow-l" width="30%">
                            <asp:TextBox ID="txtReporterName" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="oddrow">
                            所属媒体：
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtMedia" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" width="20%">
                            手机号：
                        </td>
                        <td class="oddrow-l" width="30%">
                            <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox><br />
                            <asp:Label ID="labMobile" runat="server">例如: 13682160586</asp:Label>
                        </td>
                        <td class="oddrow">
                            身份证号：
                        </td>
                        <td class="oddrow-l">
                            <asp:TextBox ID="txtIdCard" runat="server"></asp:TextBox><br />
                            <asp:Label ID="labIdCard" runat="server">请正确输入身份证号码</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            邮箱：
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
                            <asp:Label ID="labEmail" runat="server">例如: aa@163.com</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnFind" runat="server" Text="查找" Width="83px" OnClientClick="return check();"
                                CssClass="widebuttons" OnClick="btnFind_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />
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
                        <td colspan="4" class="headinglist">
                            记者列表
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" DataKeyNames="ReporterID" runat="server" OnRowDataBound="dgList_RowDataBound" OnSorting="dgList_Sorting">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="btnLink" runat="server" Text="关联到媒体" CssClass="widebuttons" OnClick="btnLink_Click"
                                OnClientClick="return linkcheck();" />
                            <input type="button" value="关闭" onclick="javascipt:window.close();return false;"
                                class="widebuttons" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
