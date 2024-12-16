<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    Inherits="FinanceWeb.sendMail" CodeBehind="sendMail.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">项目筛选<a name="top_A" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">按项目结束时间:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtBegin"></asp:TextBox>
                -
                <asp:TextBox runat="server" ID="txtEnd"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">按指定的项目号:<br />
                (逗号分隔)
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" Width="70%" Height="100px" ID="txtProject" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">邮件标题:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtTitle" Width="70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow">邮件内容:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine" Width="70%" Height="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="oddrow" colspan="4">
                <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="检索项目" />
                <asp:Button runat="server" ID="btnSend" Text="发送项目关闭通知邮件" OnClick="btnSend_Click" />
            </td>
        </tr>
    </table>

    <br />
    <br />
    <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
        EmptyDataText="暂时没有相关记录" AllowPaging="false" Width="60%">
        <Columns>
            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" >
                <ItemTemplate>
                    <%#(Container.DataItemIndex+1).ToString()%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
            <asp:BoundField DataField="BusinessDescription" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="30%" />
            <asp:BoundField DataField="ApplicantEmployeeName" HeaderText="负责人" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" />
        </Columns>
    </asp:GridView>

</asp:Content>
