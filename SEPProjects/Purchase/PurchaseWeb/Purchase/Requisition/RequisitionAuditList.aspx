<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"  Inherits="Purchase_Requisition_RequisitionAuditList" Codebehind="RequisitionAuditList.aspx.cs" %>
<%@ Import Namespace="ESP.Purchase.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" src="../../public/js/DatePicker.js"></script>
    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
    
        $().ready(function() {
            TypeDataProvider.GetListByParentIdA($("#<%=ddltype.ClientID %>").val(), pop11);
            function pop11(r) {
                $("#<%=ddltype1.ClientID %>").empty();
                $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                for (i = 0; i < r.value.length; i++) {
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                }
                $("#<%=ddltype1.ClientID %>").val($("#<%=hidtype1.ClientID %>").val());
            }
            TypeDataProvider.GetListByParentIdA($("#<%=hidtype1.ClientID %>").val(), pop22);
            function pop22(r) {
                $("#<%=ddltype2.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                if (r.value != null && r.value.length > 0) {
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                    $("#<%=ddltype2.ClientID %>").val($("#<%=hidtype2.ClientID %>").val());
                }
            }

            $("#<%=ddltype.ClientID %>").change(function() {
                $("#<%=ddltype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                $("#<%=ddltype2.ClientID %>").empty();
                $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                $("#<%=hidtype1.ClientID %>").val("");
                $("#<%=hidtype2.ClientID %>").val("");

                TypeDataProvider.GetListByParentIdA($("#<%=ddltype.ClientID %>").val(), pop1);
                function pop1(r) {
                    $("#<%=ddltype1.ClientID %>").empty();
                    $("#<%=ddltype1.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype1.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                }
            });

            $("#<%=ddltype1.ClientID %>").change(function() {

                TypeDataProvider.GetListByParentIdA($("#<%=ddltype1.ClientID %>").val(), pop2);
                function pop2(r) {
                    $("#<%=ddltype2.ClientID %>").empty();
                    $("#<%=ddltype2.ClientID %>").append("<option value=\"-1\">请选择...</option>");
                    for (i = 0; i < r.value.length; i++) {
                        $("#<%=ddltype2.ClientID %>").append("<option value=\"" + r.value[i].typeid + "\">" + r.value[i].typename + "</option>");
                    }
                    $("#<%=hidtype1.ClientID %>").val($("#<%=ddltype1.ClientID %>").val());
                }
            });

            $("#<%=ddltype2.ClientID %>").change(function() {
                $("#<%=hidtype2.ClientID %>").val($("#<%=ddltype2.ClientID %>").val());
            });
        });
    </script>
	
    <table style="width: 100%">
        <tr>
            <td colspan="2" style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table width="100%" class="tableForm">
                                <tr>
                                    <td class="heading" colspan="4">检索</td>
                                </tr>
                                <tr>
                                    <td class="oddrow">流水号:</td>
                                    <td class="oddrow-l"><asp:TextBox ID="txtGlideNo" runat="server" /></td>
                                    <td class="oddrow">申请单号:</td>
                                    <td class="oddrow-l"><asp:TextBox ID="txtPrNo" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="oddrow">初审人:</td>
                                    <td class="oddrow-l"><asp:TextBox ID="txtAudit" runat="server" /></td>
                                    <td class="oddrow">物料类别:</td>
                                    <td class="oddrow-l">
                                        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="false" Width="120px" />
                                        &nbsp;<asp:DropDownList ID="ddltype1" runat="server" Width="150px" />
                                        &nbsp;<asp:DropDownList ID="ddltype2" runat="server" Width="150px" />
                                        <asp:HiddenField ID="hidtype" runat="server" />
                                        <asp:HiddenField ID="hidtype1" runat="server" />
                                        <asp:HiddenField ID="hidtype2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4" align="center"><asp:Button ID="btnSearch" runat="server" Text=" 检索 " CssClass="widebuttons" OnClick="btnSearch_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" id="tabTop" runat="server">
                            <tr><td width="50%">
                            <asp:Panel ID="PageTop" runat="server">
                            <asp:Button ID="btnFirst2" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                            <asp:Button ID="btnPrevious2" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext2" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnLast2" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />
                            </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNumT" runat="server" />&nbsp;页数:<asp:Label ID="labPageCountT" runat="server" /></td>
                            </tr>
                            </table><br />
                            <asp:GridView ID="gvG" runat="server"  AutoGenerateColumns="False" DataKeyNames="id" 
                                PageSize="20" AllowPaging="True" Width="100%"  OnRowCommand="gvG_RowCommand" OnRowDataBound="gvG_RowDataBound" OnPageIndexChanging="gvG_PageIndexChanging">
                                
                                <Columns>
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号"  ItemStyle-HorizontalAlign="Center"/>
                                    <asp:TemplateField HeaderText="申请单号">
                                        <ItemTemplate>
                                            <a href="ShowRequisitionDetail.aspx?vis=false&helpfile=3&<% =RequestName.GeneralID %>=<%#Eval("id") %>&backUrl=RequisitionAuditList.aspx"><%#Eval("prNo") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="订单号" DataField="orderid" />
                                    <asp:BoundField DataField="requestorname" HeaderText="申请人" />
                                    <asp:TemplateField HeaderText="申请时间">
                                        <ItemTemplate>
                                            <%# Eval("app_date").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("app_date").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="requestor_group" HeaderText="业务组别" />
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" />
                                    <asp:BoundField DataField="first_assessorname" HeaderText="初审人" />
                                    <asp:TemplateField HeaderText="申请单提交时间">
                                        <ItemTemplate>
                                            <%# Eval("requisition_committime").ToString() == ESP.Purchase.Common.State.datetime_minvalue ? "" : DateTime.Parse(Eval("requisition_committime").ToString()).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="状态">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="labState" Text='<%#Eval("status") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="OrderDetail.aspx?vis=false&helpfile=3&<% =RequestName.GeneralID %>=<%#Eval("id") %>&backUrl=RequisitionAuditList.aspx"><img src="../../images/dc.gif" border="0px;"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>									
									<asp:TemplateField HeaderText="打印" ItemStyle-HorizontalAlign="Center">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a href='Print/RequisitionPrint.aspx?id=<%#DataBinder.Eval(Container.DataItem,"id")%>' target="_blank"><img title="打印申请单" src="../../images/Icon_PrintPr.gif" /></a>
										</ItemTemplate>
									</asp:TemplateField>
                                </Columns>
                                
                                <PagerSettings Visible="false"/>
                            </asp:GridView>
                            <table width="100%" id="tabBottom" runat="server">
                            <tr><td width="50%">
                            <asp:Panel ID="PageBottom" runat="server">
                            <asp:Button ID="btnFirst" runat="server" CssClass="widebuttons" Text="首页" OnClick="btnFirst_Click" />&nbsp;
                            <asp:Button ID="btnPrevious" runat="server" CssClass="widebuttons" Text="上页" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext" runat="server" CssClass="widebuttons" Text="下页" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnLast" runat="server" CssClass="widebuttons" Text="末页" OnClick="btnLast_Click" />&nbsp;
                            </asp:Panel>
                            </td>
                            <td align="right" class="recordTd">记录数:<asp:Label ID="labAllNum" runat="server" />&nbsp;页数:<asp:Label ID="labPageCount" runat="server" /></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

