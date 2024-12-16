<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Client_ClientAddAndEdit" Codebehind="ClientAddAndEdit.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">

    function check()
    {
    var meg="";
        if(document.getElementById("<% = txtChFullName.ClientID %>").value=="")
        {
            meg += "  客户中文全称不能为空!"+"\n";
        }

        if(meg!="")
        {
          alert(meg);
            return false;
        }
        return true;
    }
    function returnurl()
        {
            var hidurl=document.getElementById("<% = hidUrl.ClientID %>");
            window.location = hidurl.value;
        }
        
  
  function   WinOpen()   
      {   
       Cid = document.getElementById("<% =hidClientId.ClientID%>");
      window.open("ProductLineAddAndEdit.aspx?alert=0&Operate=ADD&Cid="+Cid.value,"添加产品线","<%= ESP.Media.Access.Utilities.Global.OpenClass.Common%>");  
      }

      function getCategory(id, text) {
          document.getElementById("<% = hidCategoryid.ClientID %>").value = id;
          document.getElementById("<% = HidCategoryName.ClientID %>").value = text;
      }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <input type="hidden" id="hidClientId" runat="server" value="0" />
    <table width="100%" >
        <tr>
            <td>
            <input type="hidden" runat="server" id="hidCategoryid"/>
            <input type="hidden" runat="server" id="HidCategoryName"/>
                <table width="100%" class="tableForm">
                    <tr>
                        <td class="menusection-Packages" colspan="4">
                            <asp:Label ID="labHeading" runat="server">添加客户</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="heading">
                            客户信息
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            客户中文全称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtChFullName" runat="server" Width="90%" MaxLength="100"></asp:TextBox><font color="red"> *</font>
                        </td>
                        <td style="width: 20%" class="oddrow">
                            客户中文简称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtChShortName" runat="server" Width="90%" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="oddrow">
                            客户英文全称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtEnFullName" runat="server" Width="90%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td style="width: 20%" class="oddrow">
                            客户英文简称：
                        </td>
                        <td style="width: 30%" class="oddrow-l">
                            <asp:TextBox ID="txtEnShortName" runat="server" Width="90%" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                     <td style="width: 20%" class="oddrow" colspan="1">
                            客户所属分类：
                        </td>
                        <td style="width: 30%" class="oddrow-l" colspan="3">
                        <asp:DropDownList ID="ddlCategory" runat="server" Height="16px" Width="205px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            Logo：
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:FileUpload ID="fplLogo" runat="server" Width="90%"
                                unselectable="on" />&nbsp;<asp:Image ID="imgLogo" runat="server" Height="24px" Width="24px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow">
                            描述：
                        </td>
                        <td colspan="3" class="oddrow-l">
                            <asp:TextBox ID="txtDes" runat="server" Height="60px" Width="90%" 
                                TextMode="MultiLine" MaxLength="1024"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: right">
                <asp:Button ID="btnOk" runat="server" CssClass="widebuttons" Text="保存" CausesValidation="true"
                    OnClientClick="return check();" OnClick="btnOk_Click"></asp:Button>
                <input type="reset" class="widebuttons" value="重置" />
                <%--<input type="button" value="返回" onclick="returnurl();" class="widebuttons" />--%>
                <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="widebuttons" OnClick="btnBack_Click" />
            </td>
        </tr>
        <tr><td style="height:10px"></td></tr>
        <tr>
            <td>
                <asp:Panel ID="pProduct" runat="server">
                    <table width="100%" border="0">
                        <tr>
                            <td class="headinglist" colspan="4">
                                已关联产品线列表
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left:20px">
                            <table class="tablehead">
                            <tr>
                                <td>
                                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                                    ID="btnLink" runat="server" class="bigfont" Text="添加相关产品线" />
                                    </td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                                </cc4:MyGridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>

        <%--<script type="text/javascript">
                   document.getElementById("<% = imgLogo.ClientID %>").width=1;
                 document.getElementById("<% = imgLogo.ClientID %>").height=1;
        </script>--%>

    </table>
</asp:Content>
