<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" CodeBehind="ProjectMediaChangeList.aspx.cs" Inherits="FinanceWeb.project.ProjectMediaChangeList" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">

    </script>


    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">检索 <asp:HiddenField runat="server" ID="hidShowAlert" Value="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">项目组别:
            </td>
            <td class="oddrow-l" align="left">
                <asp:DropDownList ID="ddlDepartment1" style="width:100px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment1_SelectedIndexChanged" />
                <asp:DropDownList ID="ddlDepartment2" runat="server" style="width:100px;" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment2_SelectedIndexChanged" />
                <asp:DropDownList ID="ddlDepartment3" runat="server" style="width:100px;" />
            </td>
            <td class="oddrow" style="width: 15%">项目类型:
            </td>
            <td class="oddrow-l" align="left">
            <asp:DropDownList ID="ddlProjectType" runat="server" AutoPostBack="true" style="width:100px;" OnSelectedIndexChanged="ddlProjectType_SelectedIndexChanged">
            </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlTypeLevel2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeLevel2_SelectedIndexChanged" style="width:100px;">
            </asp:DropDownList>&nbsp;<asp:DropDownList ID="ddlTypeLevel3" runat="server" style="width:130px;" />
            </td>
        </tr>
        <tr>

            <td class="oddrow" style="width: 15%">公司:
            </td>
            <td class="oddrow-l" align="left">
                <asp:DropDownList runat="server" ID="ddlBranch" style="width:250px;" />
            </td>
            <td class="oddrow" style="width: 15%">合同状态:
            </td>
            <td class="oddrow-l" align="left">
                <asp:DropDownList ID="ddlContactStatus" runat="server">
            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 15%">负责人:
            </td>
            <td class="oddrow-l" align="left"><asp:TextBox ID="txtApplicant" runat="server" ></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">客户名称:
            </td>
            <td class="oddrow-l" align="left"><asp:TextBox ID="txtCN1" runat="server" />
            </td>
        </tr>
                <tr>
            <td class="oddrow" style="width: 15%">项目名称:
            </td>
            <td class="oddrow-l" align="left"><asp:TextBox ID="txtBizDesc" runat="server"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">媒体付款主体:
            </td>
            <td class="oddrow-l" align="left"><asp:TextBox runat="server" ID="txtMediaName"  />
            </td>
        </tr>
                      <tr>
            <td class="oddrow" style="width: 15%">项目号:
            </td>
            <td class="oddrow-l" align="left"><asp:TextBox ID="txtProjectCode" runat="server"></asp:TextBox>
            </td>
            <td class="oddrow" style="width: 15%">
            </td>
            <td class="oddrow-l" align="left">
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text=" 检索 " OnClick="btnSearch_Click" CssClass="widebuttons"/>&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="导出 " CssClass="widebuttons" OnClick="btnExport_Click" />&nbsp;
                <asp:FileUpload ID="fil" style="border:0px;padding:0px;" runat="server" />
                <asp:Button ID="btnImport" runat="server" Text="批量导入" OnClientClick="" CssClass="widebuttons" OnClick="btnImport_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">项目列表
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvG_RowDataBound" PageSize="20" OnPageIndexChanging="gvG_PageIndexChanging"
                    EmptyDataText="暂时没有相关记录"  AllowPaging="true" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="项目号" DataField="ProjectCode" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="项目名称" DataField="BusinessDescription" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField HeaderText="负责人" DataField="ApplicantEmployeeName" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField HeaderText="公司" DataField="BranchName" ItemStyle-HorizontalAlign="Center"/>
                        <asp:TemplateField HeaderText="所属部门" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Literal ID="litGroup" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="媒体付款主体" DataField="MediaName" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField HeaderText="项目金额" DataField="TotalAmount" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:#,##0.00}" />
                        <asp:BoundField HeaderText="充值金额" DataField="Recharge" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:#,##0.00}"/>
                        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <a href='ProjectMediaChange.aspx?ProjectID=<%# Eval("ProjectId")%>'>
                                    <img src="../images/edit.gif" border="0px;" title="编辑"></a>
                            </ItemTemplate>
                        </asp:TemplateField>
            <%--            <asp:BoundField HeaderText="" DataField="" />--%>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
