<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="PRPriList.aspx.cs" Inherits="PRPriList" %>
<%@ Import Namespace="ESP.Purchase.Common"%>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <%@ Register Namespace="MyControls" TagPrefix="cc2" Assembly="MyControls" %>
    <script language="javascript" src="../../public/js/DatePicker.js"></script>

    <script language="javascript" src="/public/js/jquery-1.2.6.js"></script>

    <script type="text/javascript" src="/public/js/jquery.jcarousellite.min.js"></script>

    <script type="text/javascript" src="/public/js/script_jcarousellite.js"></script>

    <script src="/public/js/jquery.bgiframe.min.js" type="text/javascript"></script>

    

    <script language="javascript" type="text/javascript">

        function SelectAll() {
            for (var i = 0; i < document.getElementsByName("chkItem").length; i++) {
                var e = document.getElementsByName("chkItem")[i];

                e.checked = document.forms[0].chkAll.checked;
            }
        }

        function allClick() {
            var haveSelect = false;
            var items = document.getElementsByName("chkItem");
            for (i = 0; i < items.length; i++) {
                if (items[i].checked == true) {
                    haveSelect = true;
                    break;
                }
            }
            if (haveSelect) {
                return true;
            }
            else {
                alert('请选择要处理的数据！');
                return false;
            }
        }
    </script>
  <table style="width: 100%">
        <tr>
            <td style="padding: 4px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table width="100%" class="tableForm" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="heading" colspan="4">检索</td>
                                </tr>
                                <tr>
                                    <td class="oddrow" style="width:15%">项目号:</td>
                                    <td class="oddrow-l" style="width:35%"><asp:TextBox ID="txtProjectNo" runat="server" MaxLength="300" /></td>
                                    <td  class="oddrow" style="width:15%">申请时间:</td>
                                    <td class="oddrow-l" style="width:35%">
                                        <asp:TextBox ID="txtBegin" runat="server" onfocus="javascript:this.blur();" Width="100px" />&nbsp;<img src="../../images/dynCalendar.gif"
                                            border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), document.getElementById('ctl00_ContentPlaceHolder1_txtBegin'), 'yyyy-mm-dd');" />-<asp:TextBox
                                                ID="txtEnd" onfocus="javascript:this.blur();" runat="server" Width="100px" />&nbsp;<img src="../../images/dynCalendar.gif"
                                                    border="0" onclick="popUpCalendar(document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), document.getElementById('ctl00_ContentPlaceHolder1_txtEnd'), 'yyyy-mm-dd');" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow">采购物品:</td>
                                    <td class="oddrow-l"><asp:TextBox ID="txtItemNo" runat="server" MaxLength="300" /></td>
                                    <td  class="oddrow">采购总金额:</td>
                                    <td class="oddrow-l">
                                        (Min)<asp:TextBox ID="txtTotalMin" runat="server" MaxLength="300"></asp:TextBox>----<asp:TextBox ID="txtTotalMax" runat="server" MaxLength="300"></asp:TextBox>(Max)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="oddrow-l" colspan="4">
                                        <asp:Button ID="SearchBtn" runat="server" Text=" 检索 "  CssClass="widebuttons" OnClick="SearchBtn_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cc2:NewGridView ID="gvG" runat="server"  AutoGenerateColumns="False"
                                DataKeyNames="id" PageSize="20" Width="100%" AllowPaging="True" PagerSettings-Position="Bottom" OnRowCommand="gvG_RowCommand"
                                OnRowDataBound="gvG_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="id" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="addstatus" HeaderText="addstatus" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <input name="chkAll" id="chkAll" onclick="SelectAll();" type="checkbox" />全选
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                        <ItemTemplate>
                                            <input name="chkItem" id="chkItem" type="checkbox" value='<%# Eval("id").ToString() %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="glideNo" HeaderText="流水号"  ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="requestorname" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="申请时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%"  >
                                        <ItemTemplate>
                                            <%# Eval("app_date").ToString() == State.datetime_minvalue ? "" : DateTime.Parse(Eval("app_date").ToString()).ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="project_code" HeaderText="项目号" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="project_descripttion" HeaderText="项目号内容描述" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%"/>
                                    <asp:BoundField DataField="supplier_name" HeaderText="供应商" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%"/>
                                    <asp:BoundField DataField="itemno" HeaderText="采购物品" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="15%"/>
                                      <asp:BoundField DataField="ototalprice" HeaderText="采购总金额" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="6%" />

                                   <%-- <asp:TemplateField HeaderText="分公司审核人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" >
                                         <ItemTemplate>
                                              <asp:Label runat="server" ID="labFiliAudi" Text='<%#Eval("Filiale_Auditor") %>' />
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="first_assessorname" HeaderText="初审人" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%"  />
                                    --%>
                                    <asp:TemplateField HeaderText="对公" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                         <asp:LinkButton ID="btnChangeType" runat="server" CommandArgument='<%# Eval("id") %>'
                                                            CommandName="Change" Text="<img src='../../images/edit.gif' title='对公' border='0'>"
                                                            OnClientClick="return confirm('你确定要调整为对公PR单吗？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="处理" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <a href='PRPriOrderList.aspx?<% =RequestName.GeneralID %>=<%#DataBinder.Eval(Container.DataItem,"id")%>'>
                                                <%# int.Parse(Eval("InUse").ToString()) != (int)ESP.Purchase.Common.State.PRInUse.Use ? "" : "<img src='../../images/edit.gif' border='0px;' title='处理'>"  %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="查看" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <a href="/Purchase/Requisition/OrderDetail.aspx?backUrl=/PRMedia/PRPriList.aspx&<%#RequestName.GeneralID %>=<%#Eval("id") %>&<%#RequestName.ProjectID %>=<%#Eval("projectid") %>">
                                                <img src="/images/dc.gif" border="0px;"  title="查看"/></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>      
                                     <asp:BoundField DataField="projectid" HeaderText="projectid" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                                          
                                </Columns>
                            </cc2:NewGridView>
                            <br />
                            <input type="button" class="widebuttons" id="btnAll" value="批量处理" runat="server" onclick="if(allClick()){this.disabled=true;}else{return false;}" onserverclick="btnAll_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>
                
</asp:Content>