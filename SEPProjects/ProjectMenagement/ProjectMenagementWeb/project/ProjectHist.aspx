<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ProjectHist.aspx.cs" Inherits="project_ProjectHist" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>


    <table style="width: 100%">
        <tr>
            <td width="100%" align="center">
                <img id="imgtitle" src="/images/l_05.jpg" />
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
               <table width="100%" class="tableForm">
   <tr>
        <td class="heading" colspan="2">
            ① 项目准备信息
        </td>
        <td  class="heading" colspan="2">
        最后编辑日期:&nbsp;&nbsp;<asp:Label runat="server" ID="lblUpdateTime"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            确认项目号:
        </td>
        <td class="oddrow-l" style="width: 35%">
          <asp:Label ID="lblProjectCode" runat="server">（财务填写）</asp:Label>&nbsp;<asp:Label ID="labOldProjectCode" runat="server" ForeColor="Red" />
        </td>
        <td class="oddrow" style="width: 15%">
            项目流水:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblSerialCode" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            负责人:
        </td>
           <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblApplicant" runat="server" CssClass="userLabel"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            合同状态:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContactStatus" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            相关BD项目号:
        </td>
        <td class="oddrow-l" style="width: 35%">
          <asp:Label ID="lblBDProject" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            业务类型:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblBizType" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            项目来自合资方：
        </td>
        <td class="oddrow-l" style="width: 35%">
             <asp:Label ID="lblFromJoint" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            项目类型：
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblProjectType" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            项目组别:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblGroup" runat="server"></asp:Label>
        </td>
          <td class="oddrow" style="width: 15%">
            项目名称:
        </td>
          <td class="oddrow-l" style="width: 35%">
         <asp:Label ID="lblBizDesc" runat="server"></asp:Label>
        </td>
    </tr>
</table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
               <table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            项目组成员
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="4">
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvMember" runat="server" AutoGenerateColumns="False" DataKeyNames="MemberID"
                        OnRowDataBound="gvMember_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="MemberID" HeaderText="MemberID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MemberUserID" HeaderText="系统ID" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="真实姓名" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("MemberEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberCode" HeaderText="成员编号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:BoundField DataField="MemberUserName" HeaderText="成员帐号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" />
                            <asp:BoundField DataField="GroupID" HeaderText="组ID" ItemStyle-HorizontalAlign="Center"
                                Visible="false" />
                            <asp:TemplateField HeaderText="业务组别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberEmail" HeaderText="邮箱" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="15%" />
                            <asp:TemplateField HeaderText="电话" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("MemberPhone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RoleName" HeaderText="职位" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="12%" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ②客户信息
            <input type="hidden"  runat="server" id="hidCustomerID"/>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            英文简称:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblShortEN" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            中文名称1:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblNameCN1" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            中文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblNameCN2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            中文简称：
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblShortCN" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            发票抬头:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblInvoiceTitle" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            英文名称1:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblNameEN1" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            英文名称2:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblNameEN2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址1:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblAddress1" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户地址2:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblAddress2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            公司邮编:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblPostCode" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            公司网址:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblWebSite" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            所在地区:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblArea" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            所在行业:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblIndustry" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="heading" colspan="4">
            联系人信息
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            客户联系人:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContact" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            联系人职务:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContactPosition" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            联系人电话:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContactMobile" runat="server"></asp:Label>
        </td>
        <td class="oddrow" style="width: 15%">
            联系人传真:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="lblContactFax" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            联系人Email:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblEmail" runat="server" Width="40%"></asp:Label>
        </td>
    </tr>
    <tr>
</table>
                <div id="divCustomer" runat="server">
    <table width="100%">
        <tr>
            <td class="oddrow-1" colspan="4">
                <a href="#" onclick="return showCustomer();">
                    <img src="/images/differ.jpg" alt="客户信息变更" width="30px" height="25px" /><font color="red">客户信息有变动，请点这里查看比对数据</font></a>
            </td>
        </tr>
    </table>
</div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
               <table width="100%" class="TableForm">
    <tr>
        <td class="heading" colspan="4">
            ③ 合同信息
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            公司选择:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label runat="server" ID="txtBranchName" />
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            项目总金额:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtTotalAmount" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            合同税率:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="txtTaxRate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td class="oddrow">
            不含增值税金额:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTotalNoVAT" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            附加税（主申请方）:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTaxFee" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            支持方合计:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="lblSupTotal"></asp:Label>
        </td>
          <td class="oddrow">
            附加税（支持方）:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblTaxSupporter" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
         <td class="oddrow">
            合同服务费:
        </td>
        <td class="oddrow-l">
            <asp:Label ID="lblServiceFee" runat="server"></asp:Label>
        </td>
        <td class="oddrow">
            成本合计:
        </td>
        <td class="oddrow-l">
            <asp:Label runat="server" ID="lblCostTot"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow">
            项目毛利率（%）:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblProfileRate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" style="width: 15%">
            业务起始日期:
        </td>
        <td class="oddrow-l" style="width: 35%">
            <asp:Label ID="txtBeginDate" onkeyDown="return false; " Style="cursor: hand" runat="server" />
        </td>
        <td class="oddrow" style="width: 20%">
            预计结束日期:
        </td>
        <td class="oddrow-l" style="width: 30%">
            <asp:Label ID="txtEndDate" onkeyDown="return false; " Style="cursor: hand" runat="server" />&nbsp;
        </td>
    </tr>
    <tr>
        <td class="heading">
            申请文件上传
        </td>
    </tr>
      <tr id="trContractNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color:White;width:100%;border-collapse:collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">序号</th><th scope="col">合同描述</th><th scope="col">合同金额</th><th scope="col">是否有效</th><th scope="col">上一版合同</th><th scope="col">附件</th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="6" align="center"><span>暂时没有上传的合同信息</span></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trContractGrid" runat="server" visible="true">
        <td colspan="4">
            <asp:GridView ID="gvContracts" Width="100%" runat="server" DataKeyNames="ContractID"
                AutoGenerateColumns="false" OnRowDataBound="gvContracts_RowDataBound" EmptyDataText="还没有上传合同信息">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="30%" HeaderText="合同描述" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDes" runat="server" Text='<%# Eval("Description") %>' ToolTip='<%# Eval("Description") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="合同金额" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Width="100px" Text='<%# Eval("TotalAmounts") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="是否有效" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblUsable" runat="server" Text="是"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="上一版合同" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOldContract" runat="server" Text="无"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="附件" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <a id="aDownLoad" target="_blank" href='/Dialogs/ContractFileDownLoad.aspx?ContractID=<%# Eval("ContractID") %>' onclick="return checked();">
                                <img src="/images/ico_04.gif" border="0" /></a>
                            <asp:HiddenField ID="hidId" runat="server" Value='<%# Eval("ContractID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trContractTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 25%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 25%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblContractTotal" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
               <table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            成本明细信息
        </td>
    </tr>
      <tr id="trCostNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color:White;width:100%;border-collapse:collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">序号</th><th scope="col">成本描述</th><th scope="col">成本金额</th>
	            </tr>
                <tr class="td" align="left">
                    <td colspan="3" align="center"><span>暂时没有相应的成本记录</span></td>
	            </tr>
	        </table>
	    </td>
	</tr>
    <tr id="trCostGrid" runat="server" visible="true">
        <td class="oddrow" colspan="4" style="width:100%">
            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" DataKeyNames="ContractCostID" onrowdatabound="gvCost_RowDataBound" Width="100%">
                        <Columns>
                           <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="70%" />
                            <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
<%--                            <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="35%" />--%>
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
                 <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectExpenseID"
                        Width="100%" OnRowDataBound="gvExpense_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" HeaderText="成本描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="70%" />
                            <asp:TemplateField HeaderText="成本金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpense" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
<%--                            <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40%" />--%>
                          
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
        </td>
    </tr>
    <tr id="trCostTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
            <td style="width:5%; border:0 0 0 0"></td>
            <td style="width:70%; border:0 0 0 0"></td>
            <td style="width:25%; border:0 0 0 0" align="right">
                <asp:Label ID="lblCostTotal" runat="server" style="text-align:right" Width="100%" />
            </td>
            </tr>
        </table>
        </td>
    </tr>
</table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            支持方信息
        </td>
    </tr>
    <tr id="trSupporterNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">
                        序号
                    </th>
                    <th scope="col">
                        支持方
                    </th>
                    <th scope="col">
                        支持方负责人
                    </th>
                    <th scope="col">
                        支持方费用
                    </th>
                    <th scope="col">
                        服务类型
                    </th>
                    <th scope="col">
                        业务描述
                    </th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="6" align="center">
                        <span>暂时没有相应的支持方记录</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trSupporterGrid" runat="server" visible="true">
        <td class="oddrow" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvSupporter" runat="server" AutoGenerateColumns="False" DataKeyNames="SupportID"
                        OnRowDataBound="gvSupporter_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SupportID" HeaderText="支持方ID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GroupID" HeaderText="GroupID" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支持方" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LeaderUserID" HeaderText="LeaderUserID" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" CssClass="userLabel" Text='<%# Eval("LeaderEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支持方费用" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBudgetAllocation" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ServiceType" HeaderText="服务类型" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20%" />
                            <asp:BoundField DataField="ServiceDescription" HeaderText="业务描述" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="30%" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="trSupporterTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 10%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 30%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblSupporterTotal" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

            </td>
        </tr>
                <tr>
            <td colspan="2" style="padding-right: 4px; padding-left: 4px; padding-bottom: 4px;
                padding-top: 4px;">
                <table width="100%" class="tableForm">
    <tr>
        <td class="heading" colspan="4">
            ④ 付款通知信息
        </td>
    </tr>
      <tr id="trPaymentNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color:White;width:100%;border-collapse:collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">序号</th><th scope="col">付款通知时间</th><th scope="col">付款通知内容</th><th scope="col">付款通知金额</th><th scope="col">备注</th>
	            </tr>
                <tr class="td" align="left">
                    <td colspan="5" align="center"><span>暂时没有相应的付款通知记录</span></td>
	            </tr>
	        </table>
	    </td>
	</tr>
    <tr id="trPaymentGrid" runat="server" visible="true">
        <td class="oddrow" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" DataKeyNames="PaymentID"
                        OnRowDataBound="gvPayment_RowDataBound" Width="100%">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="付款通知时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("PaymentPreDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PaymentContent" HeaderText="付款通知内容" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="25%" />
                            <asp:TemplateField HeaderText="付款通知金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentBudget" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="30%" />
                        </Columns>
                        <PagerSettings Visible="false" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="trPaymentTotal" runat="server">
        <td class="oddrow-l" colspan="6" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 15%; border: 0 0 0 0">
                    </td>
                    <td style="width: 40%; border: 0 0 0 0">
                    </td>
                    <td style="width: 15%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblPaymentTotal" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                    <td style="width: 30%; border: 0 0 0 0">
                        <asp:Label ID="lblPaymentBlance" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="3" style="width: 15%">
            预计付款周期:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblPayCycle" runat="server" Width="100%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="3" style="width: 15%">
            客户特殊要求:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lblCustomerRemark" runat="server" Width="100%"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="oddrow" colspan="3" style="width: 15%">
            是否需第三方发票:
        </td>
        <td class="oddrow-l" colspan="3">
            <asp:Label ID="lbl3rdInvoice" runat="server"></asp:Label>
        </td>
    </tr>
        <tr>
        <td class="heading" colspan="4">
            预计各月完工百分比
        </td>
    </tr>
     <tr id="trPercentNoRecord" runat="server" visible="false">
        <td colspan="4">
            <table class="gridView" cellspacing="0" border="0" style="background-color: White;
                width: 100%; border-collapse: collapse;">
                <tr class="Gheading" align="center">
                    <th scope="col">
                        序号
                    </th>
                    <th scope="col">
                        年
                    </th>
                    <th scope="col">
                        月
                    </th>
                    <th scope="col">
                        完工百分比(%)
                    </th>
                    <th scope="col">
                        当月Fee
                    </th>
                </tr>
                <tr class="td" align="left">
                    <td colspan="6" align="center">
                        <span>没有填写预计完工百分比信息</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trPercentGrid" runat="server" visible="true">
        <td colspan="4">
            <asp:GridView ID="gvPercent" Width="100%" runat="server" DataKeyNames="ScheduleID"
                AutoGenerateColumns="false" EmptyDataText="没有填写预计完工百分比信息" OnRowDataBound="gvPercent_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="YearValue" HeaderText="年" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:BoundField DataField="monthValue" HeaderText="月" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="20%" />
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="完工百分比(%)" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblPercent" runat="server" Text='<%#Eval("MonthPercent") == null ? "0.00" : Convert.ToDecimal(Eval("MonthPercent")).ToString("0.00") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="当月Fee" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblFee" runat="server" Text='<%#Eval("Fee") == null ? "0.00" : Convert.ToDecimal(Eval("Fee")).ToString("#,##0.00")%>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trPercentTotal" runat="server">
        <td class="oddrow-l" colspan="4" align="right">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                 <td style="width: 5%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                    </td>
                    <td style="width: 20%; border: 0 0 0 0">
                     <asp:Label ID="lblTotalPercent" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                    <td style="width: 20%; border: 0 0 0 0" align="right">
                        <asp:Label ID="lblTotalFee" runat="server" Style="text-align: right" Width="100%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
            </td>
        </tr>
    </table>
   <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" align="right">
                <asp:Label Font-Size="XX-Small" ForeColor="SteelBlue" ID="lblRepresent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnFirst" runat="server" Text="  |<  " class="widebuttons" OnClick="btnFirst_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnPret" runat="server" Text=" <<< " class="widebuttons" OnClick="btnPret_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNext" runat="server" Text=" >>> " class="widebuttons" OnClick="btnNext_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnLast" runat="server" Text="  >|  " class="widebuttons" OnClick="btnLast_Click" />
            </td>
        </tr>
        <tr>
        <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " class="widebuttons" OnClientClick="window.close();" />
            </td>
        </tr>
    </table>
</asp:Content>

