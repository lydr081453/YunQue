<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ReporterSelectMediaList.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.ReporterSelectMediaList" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
        function check()
        {
             hide = document.getElementById("<% =hidChecked.ClientID %>");
             if (hide.value == "0")
                alert("请选择一个媒体!");
        }
        
        function selected(obj)
        {
            if (obj.checked)
            {
                hide = document.getElementById("<% =hidChecked.ClientID %>");
                hide.value = obj.value;
            }
        }       
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidChecked" runat="server" value="0" />
    <table width="100%" border="0">
        <tr style="display:none">
            <td colspan="4">
            <table class="tablehead">
             <tr>
                  <td>
                <img src="/images/add.gif" border="0" style="vertical-align: bottom" />&nbsp;<asp:LinkButton
                    ID="btnAddReporter" runat="server" class="bigfont" Text="添加新媒体" OnClick="btnAdd_Click" />
                  </td>
            </tr>
           </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" class="tableForm">
        <tr>
            <td colspan="4" class="heading">选择已有媒体</td>
        </tr>
        <tr>
            <td colspan="4" class="heading">
                查找条件
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                媒体名称：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtCnName" runat="server"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 20%">
                形态：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:DropDownList ID="ddlMediaType" runat="server" AutoPostBack="true" CssClass="fixddl">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">
                覆盖区域：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:TextBox ID="txtIssueRegion" runat="server" />
            </td>
            <td class="oddrow" style="width: 20%">
                行业属性：
            </td>
            <td class="oddrow-l" style="width: 30%">
                <asp:DropDownList ID="ddlIndustry" runat="server" CssClass="fixddl" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" Text="查找" OnClick="btnSearch_Click" runat="server" CssClass="widebuttons">
                </asp:Button>
                <asp:Button ID="btnClear" runat="server" CssClass="widebuttons" Text="返回总库" CausesValidation="true"
                    OnClick="btnClear_OnClick" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border: 0px;height:30px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" class="headinglist">
                媒体列表
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <cc4:MyGridView ID="dgList" DataKeyNames="mediaitemid" runat="server" OnRowDataBound="dgList_RowDataBound">
                </cc4:MyGridView>
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td colspan="4" style="text-align: right">
                <asp:Button ID="btnSelect" Text="确定" OnClick="btnSelect_Click" runat="server" CssClass="widebuttons">
                </asp:Button>
                <%--<asp:Button ID="btnAdd" runat="server" Text="添加新媒体" OnClick="btnAdd_Click" CssClass="widebuttons" />--%>
                <asp:Button ID="btnBack" Text="返回" OnClick="btnBack_Click" runat="server" CssClass="widebuttons"></asp:Button>
                <asp:Button ID="btnClose" Text="关闭" OnClientClick="window.close();" runat="server" CssClass="widebuttons" />
            </td>
        </tr>
    </table>
</asp:Content>

