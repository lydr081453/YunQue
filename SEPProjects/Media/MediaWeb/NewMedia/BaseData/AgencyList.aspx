<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AgencyList.aspx.cs" Inherits="MediaWeb.NewMedia.BaseData.AgencyList" %>
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
<link href="/css/gridStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <table style="width: 100%;">
    <tr>
                        <td colspan="4" >
                            <a href="AgencyAddAndEdit.aspx" >添加新机构</a>
                        </td>
                    </tr>
        <tr>
    <tr>
                        <td colspan="4" class="headinglist">
                            机构列表
                        </td>
                    </tr>
        <tr>
            <td colspan="4">      
            <cc4:MyGridView ID="glist" runat="server" OnRowDataBound="gList_RowDataBound"
                                OnSorting="gList_Sorting">
            </cc4:MyGridView>         
            </td>
        </tr>
    </table>
</asp:Content>


