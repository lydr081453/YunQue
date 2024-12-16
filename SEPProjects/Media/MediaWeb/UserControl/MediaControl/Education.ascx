<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_MediaControl_Education" Codebehind="Education.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="MyControls" Assembly="MyControls" %>
<table>
    <tr>
        <td style="width: 153px" class="heading" colspan="4">
            学校信息：
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 20px">
            <strong>大学 </strong>
        </td>
    </tr>
    <tr>
        <td style="width: 73px">
            大学名称：
        </td>
        <td style="width: 278px">
            <asp:TextBox ID="txtCollegeName" runat="server"></asp:TextBox>
        </td>
        <td style="width: 89px">
            所在的国家：
        </td>
        <td style="width: 350px">
            <asp:DropDownList ID="ddlCollegeCountry" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 73px">
            所在省市：
        </td>
        <td style="width: 278px">
            <asp:DropDownList ID="DdlCollegeProvince" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
        <td style="width: 89px">
            所在城市：
        </td>
        <td style="width: 350px">
            <asp:DropDownList ID="DdlCollegeCity" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 73px">
            院系：
        </td>
        <td style="width: 278px">
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
        <td style="width: 89px">
            学历类型：
        </td>
        <td style="width: 350px">
            <asp:DropDownList ID="ddlEducationType" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="height: 16px; width: 73px;">
            入学年份：
        </td>
        <td style="width: 278px; height: 16px">
            <cc2:DatePicker ID="dpGoToSchoolYear" runat="server" Width="120px"></cc2:DatePicker>
        </td>
        <td style="width: 89px; height: 16px">
        </td>
        <td style="width: 350px; height: 16px">
        </td>
    </tr>
    <tr>
        <td style="height: 16px" colspan="4">
            <strong>高中</strong>
        </td>
    </tr>
    <tr>
        <td style="width: 73px; height: 16px">
            学校名称：
        </td>
        <td style="width: 278px; height: 16px">
            <asp:TextBox ID="txtHighShoolName" runat="server"></asp:TextBox>
        </td>
        <td style="width: 89px; height: 16px">
            所在的国家：
        </td>
        <td style="width: 350px; height: 16px">
            <asp:DropDownList ID="ddlHighShoolCountry" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 73px; height: 16px">
            所在省市：
        </td>
        <td style="width: 278px; height: 16px">
            <asp:DropDownList ID="ddlHighShoolProvince" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
        <td style="width: 89px; height: 16px">
            所在城市：
        </td>
        <td style="width: 350px; height: 16px">
            <asp:DropDownList ID="ddlHighShoolCity" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="height: 16px" colspan="4">
            <strong>初中</strong>
        </td>
    </tr>
    <tr>
        <td style="width: 73px; height: 16px">
            学校名称：
        </td>
        <td style="width: 278px; height: 16px">
            <asp:TextBox ID="txtMiddleShoolName" runat="server"></asp:TextBox>
        </td>
        <td style="width: 89px; height: 16px">
            所在的国家：
        </td>
        <td style="width: 350px; height: 16px">
            <asp:DropDownList ID="ddlMiddleShoolCountry" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 73px; height: 16px">
            所在省市：
        </td>
        <td style="width: 278px; height: 16px">
            <asp:DropDownList ID="ddlMiddleShoolProvince" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
        <td style="width: 89px; height: 16px">
            所在城市：
        </td>
        <td style="width: 350px; height: 16px">
            <asp:DropDownList ID="ddlMiddleShoolCity" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="height: 16px" colspan="4">
            <strong>小学</strong>
        </td>
    </tr>
    <tr>
        <td style="width: 73px; height: 16px">
            学校名称：
        </td>
        <td style="width: 278px; height: 16px">
            <asp:TextBox ID="txtSmallShoolName" runat="server"></asp:TextBox>
        </td>
        <td style="width: 89px; height: 16px">
            所在的国家：
        </td>
        <td style="width: 350px; height: 16px">
            <asp:DropDownList ID="ddlSmallShoolCountry" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 73px; height: 16px">
            所在省市：
        </td>
        <td style="width: 278px; height: 16px">
            <asp:DropDownList ID="ddlSmallShoolProvince" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
        <td style="width: 89px; height: 16px">
            所在城市：
        </td>
        <td style="width: 350px; height: 16px">
            <asp:DropDownList ID="ddlSmallShoolCity" runat="server" CssClass="fixddl">
            </asp:DropDownList>
        </td>
    </tr>
</table>
