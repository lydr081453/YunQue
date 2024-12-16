<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentPositionForm.aspx.cs"
    MasterPageFile="~/MainMaster.Master" Inherits="SEPAdmin.Management.UserManagement.DepartmentPositionForm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table>
            <tr>
                <td class="oddrow">
                    编号：
                </td>
                <td class="oddrow-l">
                    <asp:Label ID="lblID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    名称：
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><asp:HiddenField ID="hidPositionBaseId"
                        runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                        Display="Dynamic" ErrorMessage="名称为必填"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    描述：
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    ChargeRate：
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtChargeRate" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    工资上限：
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtSalaryHigh" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    工资下限：
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtSalaryLow" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    工资级别：
                </td>
                <td class="oddrow-l">
                    <asp:TextBox ID="txtSalaryLevel" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="oddrow">
                    职位级别：
                </td>
                <td class="oddrow-l">
                    <asp:DropDownList runat="server" ID ="ddlPositionLevel">
                    <asp:ListItem Text="请选择..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="MD" Value="1"></asp:ListItem>
                    <asp:ListItem Text="EVP" Value="2"></asp:ListItem>
                    <asp:ListItem Text="VP" Value="3"></asp:ListItem>
                    <asp:ListItem Text="AD" Value="4"></asp:ListItem>
                    <asp:ListItem Text="AAD" Value="5"></asp:ListItem>
                    <asp:ListItem Text="SAM" Value="6"></asp:ListItem>
                    <asp:ListItem Text="AM" Value="7"></asp:ListItem>
                    <asp:ListItem Text="AAM" Value="8"></asp:ListItem>
                    <asp:ListItem Text="SAE" Value="9"></asp:ListItem>
                    <asp:ListItem Text="AE" Value="10"></asp:ListItem>
                    <asp:ListItem Text="AC" Value="11"></asp:ListItem>
                    <asp:ListItem Text="Helper" Value="12"></asp:ListItem>
                    </asp:DropDownList><asp:HiddenField
                        ID="hidPositionLevelId" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPositionLevel"
                        Display="Dynamic" ErrorMessage="职位级别为必填"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="widebuttons" OnClick="btnSave_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="返回" CausesValidation="false" CssClass="widebuttons"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <table width="100%">
        <tr>
            <td class="oddrow">
                名称：
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtKey" runat="server" />
                &nbsp;
                <asp:Button ID="btnSerach" runat="server" Text="检索" CausesValidation="false" CssClass="widebuttons"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" Width="80%">
        <Columns>
            <asp:BoundField HeaderText="名称" DataField="PositionName" />
            <asp:BoundField HeaderText="等级" DataField="LevelName" />
            <asp:TemplateField HeaderText="引用">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkLink" OnClick="lnkLink_Click" runat="server" CausesValidation="false"
                        CommandArgument='<%# Eval("Id") %>' Text="引用" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <table width="100%">
                <tr>
                    <td align="center">
                        没有符合条件的数据存在！
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
