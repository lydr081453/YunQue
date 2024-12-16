<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Media_ReporterSelectMedia" Title="媒体列表" Codebehind="ReporterSelectMedia.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
      
        
        function selected(obj)
        {
            if (obj.checked)
            {
                hide = document.getElementById("<% =hidMediaId.ClientID %>");
                hide.value = obj.value;
            }
        }
        function closereturn()
        {
          hide = document.getElementById("<% =hidMediaId.ClientID %>");
         var bb= hide.value.split(",")
         opener.document.getElementById("<% =hidMediaId.ClientID %>").value=bb[0];
         if(hide.value=="0")  
         {
         bb[1]="";
         } 
          opener.document.getElementById("<% =txtMedia.ClientID %>").value=bb[1];
//          if(hide.value!="0")  
//           {
//                var bb= hide.value.split(",")
//                opener.document.getElementById("<% =hidMediaId.ClientID %>").value=bb[0];
//                opener.document.getElementById("<% =txtMedia.ClientID %>").value=bb[1];
//          }
          window.parent.close();   
   
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidMediaId" runat="server" value="0" />
    <input type="hidden" id="txtMedia" runat="server" value="" />
    <table width="100%" border="0">
        <tr>
            <td>
                <table width="100%" border="0" class="tableForm">
                    <tr>
                        <td colspan="4" class="heading">
                            查找条件
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            媒体类型：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlMediaType" runat="server" CssClass="fixddl">
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            媒体中文名称：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:TextBox ID="txtCnName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            行业属性：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlIndustry" runat="server" CssClass="fixddl">
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            所在国家：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlCountry" CssClass="fixddl" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            所在省：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlProvince" CssClass="fixddl" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="oddrow" style="width: 20%">
                            所在市：
                        </td>
                        <td class="oddrow-l" style="width: 30%">
                            <asp:DropDownList ID="ddlCity" CssClass="fixddl" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="oddrow" style="width: 20%">
                            地址：
                        </td>
                        <td class="oddrow-l" colspan="3">
                            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSearch" Text="查找" OnClick="btnSearch_Click" runat="server" CssClass="widebuttons">
                            </asp:Button>
                            <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true" OnClick="btnClear_OnClick" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border: 0px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 30px">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td colspan="4" class="headinglist">
                            媒体列表
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="btnSelect" Text="确定" OnClientClick="return closereturn();" runat="server"
                                CssClass="widebuttons"></asp:Button>
                        </td>
                    </tr>
                    <%--<tr>
                <td colspan="4">
                    <asp:Button ID="btnAdd" runat="server" Text="添加新媒体"  OnClick="btnAdd_Click"  CssClass="widebuttons"/>
                </td>
            </tr>--%>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
