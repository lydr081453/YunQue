<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DimissionFormEdit.aspx.cs"
    Inherits="DimissionFormEdit" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <style>
        .loading {
            position: relative;
        }

            .loading::after {
                content: '';
                position: absolute;
                top: 50%;
                left: 50%;
                width: 24px;
                height: 24px;
                border: 4px solid #f3f3f3;
                border-top: 4px solid #3498db;
                border-radius: 50%;
                animation: spin 2s linear infinite;
                transform: translate(-50%, -50%);
            }

        @keyframes spin {
            0% {
                transform: translate(-50%, -50%) rotate(0deg);
            }

            100% {
                transform: translate(-50%, -50%) rotate(360deg);
            }
        }

        .disabled {
            pointer-events: none;
            opacity: 0.6;
        }
    </style>


    <link href="/public/css/jquery-ui-new.css" rel="stylesheet" />
    <script src="/public/js/jquery1.12.js"></script>
    <script src="/public/js/jquery-ui-new.js"></script>
    <script src="/public/js/jquery.ui.datepicker.cn.js"></script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["zh-CN"]);
            $("#ctl00_ContentPlaceHolder1_txtdimissionDate").datepicker();
        });

        function setLoading(button) {
            if (Page_ClientValidate()) {
                button.classList.add('loading');
                button.classList.add('disabled');
            }
        }
    </script>

    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">离职申请单
                <asp:HiddenField ID="hidDimissionFormID" runat="server" />
            </td>
        </tr>

        <asp:Panel ID="pnlTip" runat="server" Visible="false">
            <tr>
                <td colspan="4" class="oddrow">
                    <asp:Label runat="server" ID="labTip" />
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlCash" runat="server" Visible="false">
            <tr>
                <td colspan="4" class="heading">未处理单据
                </td>
            </tr>
            <tr>
                <td colspan="4" class="oddrow">
                    <asp:GridView ID="gvCashList" runat="server" AutoGenerateColumns="False" DataKeyNames="FormId,FormType"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="FormId" Visible="false" />
                            <asp:TemplateField HeaderText="单据编号">
                                <ItemTemplate>
                                    <a href='http://<%# Eval("Website").ToString() + Eval("Url").ToString() %>' target="_blank"
                                        title="<%# Eval("FormCode") %>">
                                        <%# Eval("FormCode") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FormType" HeaderText="单据类型" />
                            <asp:BoundField DataField="ProjectCode" HeaderText="项目号" />
                            <asp:BoundField DataField="Description" HeaderText="项目号描述" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="总金额" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlAsset" runat="server">
                <tr>
                    <td class="heading" colspan="4" >固定资产领用
                    </td>
                </tr>
                <tr>
                    <td colspan="4" >
                        <asp:GridView ID="gvAsset" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                            Width="100%">
                            <Columns>
                               <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <%#(Container.DataItemIndex+1).ToString()%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SerialCode" HeaderText="资产编号" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="AssetName" HeaderText="资产名称" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Brand" HeaderText="品牌" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Model" HeaderText="型号" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="ReceiveDate" HeaderText="领用日期" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" />
                            </Columns>
                            <PagerSettings Visible="false" />
                        </asp:GridView>
                    </td>
                </tr>

        </asp:Panel>
        <tr><td colspan="4" >&nbsp;</td></tr>
        <tr>
            <td class="oddrow" style="width: 20%">用户编号:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labCode" runat="server" />
            </td>
            <td class="oddrow">所在部门:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtdepartmentName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hiddepartmentId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" style="width: 20%">姓名:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtuserName" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">所在公司:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtcompanyName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hidcompanyId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">职务:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPosition" runat="server" Enabled="false" />
            </td>
            <td class="oddrow">所属团队:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtgroupName" runat="server" Enabled="false" />
                <asp:HiddenField ID="hidgroupId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">手机:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtMobilePhone" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMobilePhone"
                    ErrorMessage="请填写您的手机号码。" ForeColor="Red" />
            </td>
            <td class="oddrow">分机:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtPhone" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">私人邮箱:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtEmail" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="请填写您的私人邮箱。" ForeColor="Red" />
            </td>
            <td class="oddrow">入职日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox runat="server" ID="txtjoinJobDate" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="oddrow">期望离职日期:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtdimissionDate" onkeyDown="return false;" runat="server" />&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdimissionDate"
                    ErrorMessage="请填写您期望离职日期。" ForeColor="Red" />
            </td>
            <td class="oddrow" style="width: 20%">&nbsp;
            </td>
            <td class="oddrow-l" style="width: 30%">&nbsp;
            </td>
        </tr>
        <tr>
            <td class="oddrow">离职原因:
            </td>
            <td class="oddrow-l" colspan="3">
                <asp:TextBox ID="txtdimissionCause" runat="server" TextMode="MultiLine" Width="90%"
                    Height="80px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写离职原因。"
                    ControlToValidate="txtdimissionCause" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="oddrow-l">
                <asp:Button ID="btnSave" runat="server" Text=" 保存 " OnClientClick="if(Page_ClientValidate()){showLoading();}else {return false;} " CssClass="widebuttons" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text=" 提交 "  OnClientClick="if(Page_ClientValidate()){showLoading();}else {return false;}"  CssClass="widebuttons" OnClick="btnSubmit_Click" />&nbsp;
                <input type="reset" id="btnReset" class="button_org" value=" 重置 " />
            </td>
        </tr>
    </table>
</asp:Content>
