<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master"
    CodeBehind="AddActity.aspx.cs" Inherits="SEPAdmin.Actity.AddActity" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker.js"></script>
    
    <script language="javascript" type="text/javascript" src="../../public/js/DatePicker1.js"></script>

    <script language="javascript" type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/publicjs/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/publicjs/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    <script src="/public/js/dimensions.js" type="text/javascript"></script>

    <script type="text/javascript" src="/public/js/jquery.blockUI.js"></script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                培训主题:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" ></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
                培训讲师:
            </td>
            <td class="oddrow-l" style="width: 35%">
                <asp:TextBox ID="txtLecturer" runat="server" MaxLength="10" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                培训内容:
            </td>
            <td class="oddrow-l" colspan="3" style="width: 85%">
                <asp:TextBox ID="txtContent" runat="server" MaxLength="4000" Height="84px" 
                    Width="842px" Columns="76" Rows="5" TextMode="MultiLine" 
                    ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">
                培训时间:
            </td>
            <td class="oddrow-l">
                <%--<asp:TextBox ID="txtTime" runat="server" onfocus="javascript:this.blur();" Width="100px" />&nbsp;<img
                                            src="../Images/dynCalendar.gif" border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtTime'), document.getElementById('ctl00_ContentPlaceHolder1_txtTime'), 'yyyy-mm-dd');" />--%>
                <asp:TextBox ID="txtTime" runat="server" onfocus="javascript:this.blur();" onclick="setDayHM(this);"/>
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" align="center">
                <asp:Button ID="Button1" runat="server" Text=" 保存 " CssClass="widebuttons" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:TextBox ID="txtId" runat="server" Visible="false"></asp:TextBox>
            </td>
            <td class="oddrow-l" align="center">
                <asp:Button ID="Button2" runat="server" Text="取消" OnClick="btnCancle_Click" CssClass="widebuttons"/>
            </td>
        </tr>
    </table>
</asp:Content>
