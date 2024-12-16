<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="BatchPurchase.aspx.cs" Inherits="Purchase_BatchPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript">
        function selectAll(obj) {
            var theTable = obj.parentElement.parentElement.parentElement;
            var i;
            var j = obj.parentElement.cellIndex;

            for (i = 0; i < theTable.rows.length; i++) {
                var objCheckBox = theTable.rows[i].cells[j].firstChild;
                if (objCheckBox.checked != null) objCheckBox.checked = obj.checked;
            }
        }
    </script>

    <asp:HiddenField ID="hidBatchId" runat="server" />
    <table width="100%" border="0" background="../images/allinfo_bg.gif">
        <tr style="height: 30px">
            <td width="50%">
               <font style="font-weight: bold; font-size: 15px"> ������:<asp:Label ID="labCreateUser"
                    runat="server" /></font>
            </td>
            <td align="right">
                <font style="font-weight: bold; font-size: 15px">���κ�:<asp:Label ID="labPurchaeBatchCode"
                    runat="server" /></font>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="4">
                ���θ���
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                ��˾ѡ��:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChangeed" />
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="��ѡ��˾"
                    Display="None" ControlToValidate="ddlCompany" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
            </td>
            <td class="oddrow" width="15%">
                ���ʽ:
            </td>
            <td class="oddrow-l" width="35%">
                <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChangeed">
                    <asp:ListItem Text="����ת��" Value="3" />
                </asp:DropDownList>
                <font color="red">* </font>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="��ѡ�񸶿ʽ"
                    Display="None" ControlToValidate="ddlPaymentType" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="oddrow">
                ��Ӧ��:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtSupplier" runat="server" MaxLength="50" Width="200px" />
            </td>
            <td class="oddrow">
                �ؼ���:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtKey" runat="server" MaxLength="20" Width="200px" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l" colspan="4">
                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" Text=" ���� " CssClass="widebuttons"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="palPN" runat="server">
        <table width="100%" class="tableForm">
            <tr>
                <td class="heading">
                    ����˸�������
                </td>
            </tr>
            <tr>
                <td class="oddrow-l">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="��ʱû����ؼ�¼"
                        AllowPaging="False" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input id="chkItem" name="chkItem" type="checkbox" value='<%#Eval("ReturnID") %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    &nbsp;<input id="CheckAll" type="checkbox" onclick="selectAll(this);" />ȫѡ
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReturnID" HeaderText="ReturnID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PurchasePayID" HeaderText="������ˮ" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            <asp:BoundField DataField="PRID" HeaderText="pr��ˮ" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            <asp:BoundField DataField="ProjectID" HeaderText="��Ŀ��ˮ" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR��" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPR"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PN��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <a href="/Purchase/ReturnDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>"
                                        target="_blank">
                                        <%#Eval("ReturnCode")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GR��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Literal ID="litGRNo" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="������" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProjectCode" HeaderText="��Ŀ��" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Ԥ�����" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ԥ�Ƹ���ʱ��" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBeginDate" Text='<%# DateTime.Parse(Eval("PReBeginDate").ToString()).ToString("yyyy-MM-dd") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��Ӧ��" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("supplierName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="����״̬" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatusName" />
                                    <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                    <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��������" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="����" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="<img title='���ظ�������' src='../../images/Icon_Cancel.gif' border='0' />"
                                        OnClientClick="return confirm('���Ƿ�ȷ�ϲ��ظ������룿');" CausesValidation="false" CommandArgument='<%# Eval("ReturnID") %>'
                                        CommandName="Return" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text=" ��� " CausesValidation="false" CssClass="widebuttons"
                        OnClick="btnCreate_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading">
                �����ڸ�������<asp:Button ID="btnEditList" runat="server" Visible="false" Text="�༭�����б�"
                    OnClick="btnEditList_Click" CssClass="widebuttons" />
            </td>
        </tr>
        <tr>
            <td class="oddrow-l">
                <asp:GridView ID="gvG" runat="server" AutoGenerateColumns="False" DataKeyNames="ReturnID"
                    OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" EmptyDataText="��ʱû����ؼ�¼"
                    AllowPaging="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ReturnID" HeaderText="ReturnID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PurchasePayID" HeaderText="������ˮ" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="PRID" HeaderText="pr��ˮ" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="ProjectID" HeaderText="��Ŀ��ˮ" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="PR��" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPR"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PN��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <a href="/Purchase/ReturnDisplay.aspx?<% =ESP.Finance.Utility.RequestName.ReturnID %>=<%#Eval("ReturnID") %>"
                                    target="_blank">
                                    <%#Eval("ReturnCode")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GR��" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Literal ID="litGRNo" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="������" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("RequestEmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProjectCode" HeaderText="��Ŀ��" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Ԥ�����" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labExpectPaymentPrice" Text='<%# Eval("PreFee") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ԥ�Ƹ�������" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblBeginDate" Text='<%# DateTime.Parse(Eval("PReBeginDate").ToString()).ToString("yyyy-MM-dd") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��Ӧ��" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("supplierName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����״̬" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatusName" />
                                <asp:HiddenField ID="hidStatusNameID" Value='<%# Eval("ReturnStatus") %>' runat="server" />
                                <asp:HiddenField ID="hidReturnID" Value='<%# Eval("ReturnID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��������" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPaymentType" Text='<%# Eval("PaymentTypeName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�༭" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEdit" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�Ƴ�" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ReturnID") %>'
                                    CommandName="Del" Text="<img src='../../images/disable.gif' title='�Ƴ�' border='0'>"
                                    OnClientClick="return confirm('��ȷ���Ƴ���');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��ӡԤ��" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="hylPrint" Target="_blank" NavigateUrl='<%# "Print/PrintPRGR.aspx?" + ESP.Finance.Utility.RequestName.ReturnID+"="+Eval("ReturnID").ToString()  %>'
                                    runat="server" ImageUrl="/images/Icon_Output.gif" ToolTip="��ӡԤ��" Width="4%"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                �����ܶ�:<asp:Label ID="labTotal" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" class="tableForm">
        <tr>
            <td class="heading" colspan="2">
                ������Ϣ
            </td>
        </tr>
        <tr>
            <td width="15%" class="oddrow">
                ������ʷ:
            </td>
            <td class="oddrow-l">
                <asp:Label ID="labAuditLog" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="oddrow" width="15%">
                ������ע:
            </td>
            <td class="oddrow-l">
                <asp:TextBox ID="txtRemark" runat="server" Width="50%" /><font color="red"> * </font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRemark"
                    Display="None" runat="server" ErrorMessage="����д������ע"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%" class="XTable">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" Text=" ���� " Visible="false" CssClass="widebuttons"
                    CausesValidation="false" OnClick="btnSave_Click" />
                <asp:Button ID="btnYes" runat="server" Text="����ͨ��" OnClientClick="if(Page_ClientValidate()){return confirm('��ȷ������ͨ����');}"
                    CausesValidation="false" CssClass="widebuttons" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" Text="������������" Visible="false" OnClientClick="return confirm('��ȷ����������������');"
                    CssClass="widebuttons" OnClick="btnNo_Click" />
                <asp:Button ID="btnNo1" runat="server" Text=" ���� " Visible="false" OnClientClick="if(Page_ClientValidate()){return confirm('��ȷ��������');}"
                    CausesValidation="false" CssClass="widebuttons" OnClick="btnNo1_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" ���� " CssClass="widebuttons" OnClick="btnBack_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
        runat="server" />
</asp:Content>
