<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchExpenseAccountList.aspx.cs" Inherits="FinanceWeb.ExpenseAccount.SearchExpenseAccountList" MasterPageFile="~/MasterPage.master" Theme="" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="css/syshomepage.css" rel="stylesheet" type="text/css" />
    <link href="css/a.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/public/js/DatePicker.js"></script>

    <link href="css/gridStyle2.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
        function setDate(obj) {
            popUpCalendar(obj, obj, 'yyyy-mm-dd');
        }

        function getTitle(groupItem) {
            var rows = groupItem.Rows;
            return groupItem.Grid.Table.GetRow(rows[0]).GetMember("RequestEmployeeName").Value;
        }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="17">
                            <img src="images/t2_03.jpg" width="21" height="20" />
                        </td>
                        <td align="left">
                            <strong>搜索 </strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            关 键 字:&nbsp;&nbsp;<asp:TextBox ID="txtKey" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            提交日期:&nbsp;&nbsp;<asp:TextBox ID="txtBeginDate" runat="server" onclick="setDate(this);"
                                onkeyDown="return false; "></asp:TextBox>
                            --
                            <asp:TextBox ID="txtEndDate" runat="server" onclick="setDate(this);" onkeyDown="return false; "></asp:TextBox>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <br />
                            <asp:ImageButton ImageUrl="/images/t2_03-07.jpg" ID="btnSearch" OnClick="btnSearch_Click"
                                Width="56" Height="24" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
     
    
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
        <tr class="GridHeader2" >
            <td style="height:30" class="GridHeaderText">审核列表</td>
        </tr>
        <tr>
            <td class="td">
                <ComponentArt:Grid ID="GridHist" DataAreaCssClass="GridData" CssClass="Grid" EnableViewState="true"  GroupingMode="ConstantGroups"
                    GroupByTextCssClass="HeadingCellText" GroupBySectionCssClass="grp"  
                    EditOnClickSelectedItem="false" FooterCssClass="GridFooter"
                    FooterHeight="40" HeaderHeight="30" HeaderCssClass="GridHeader" GroupingNotificationTextCssClass="GridHeaderText"
                    GroupingNotificationText="审核列表"  PagerStyle="Numbered"
                    PagerTextCssClass="GridFooterText" PagerInfoPosition="BottomLeft" PagerPosition="BottomRight"
                    SliderHeight="20" SliderWidth="150" SliderGripWidth="9" SliderPopupOffsetX="35"
                    ImagesBaseUrl="images/gridview2/" PagerImagesFolderUrl="images/gridview2/pager/"
                    TreeLineImagesFolderUrl="images/gridview2/lines/" TreeLineImageWidth="11" TreeLineImageHeight="11" PageSize="200"  ShowHeader="false"
                    PreExpandOnGroup="true" Width="100%" Height="100%" runat="server" >
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ReturnID" ShowTableHeading="false" TableHeadingCssClass="GridHeader" ShowSelectorCells="false"
                            RowCssClass="Row" ColumnReorderIndicatorImageUrl="reorder.gif" DataCellCssClass="DataCell"
                            HeadingCellCssClass="HeadingCell" HeadingCellHoverCssClass="HeadingCellHover"
                            HeadingCellActiveCssClass="HeadingCellActive" HeadingRowCssClass="HeadingRow"
                            HeadingTextCssClass="HeadingCellText" SelectedRowCssClass="SelectedRow" SortedDataCellCssClass="SortedDataCell"
                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageWidth="9"  GroupHeadingClientTemplateId="GroupByTemplate"
                            SortImageHeight="5">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ReturnType" Visible="false" />
                                <ComponentArt:GridColumn DataField="ReturnID" Visible="false" />
                                <ComponentArt:GridColumn DataField="CostDetailID" Visible="false" />
                                <ComponentArt:GridColumn DataField="RequestEmployeeName" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExpenseType" Visible="false" />
                                <ComponentArt:GridColumn DataField="ID" Visible="false" />
                                <ComponentArt:GridColumn HeadingText="申请单号" DataField="ReturnCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="项目号" DataField="ProjectCode" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="项目名称" DataField="ReturnContent" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="费用所属组" DataField="DepartmentName" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="费用发生日期" DataField="ExpenseDate" Align="Center" 
                                                FormatString="yyyy-MM-dd"  />
                                <ComponentArt:GridColumn HeadingText="提交日期" DataField="CommitDate" Align="Center"
                                                FormatString="yyyy-MM-dd" Visible="false" />
                                <ComponentArt:GridColumn HeadingText="项目成本明细" Align="Center" DataCellServerTemplateId="costDetailTemplate"/>
                                <ComponentArt:GridColumn HeadingText="费用类型" Align="Center" DataCellServerTemplateId="expenseTypeTemplate"/>
                                <ComponentArt:GridColumn HeadingText="费用明细描述" DataField="ExpenseDesc" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="数量" DataField="ExpenseTypeNumber" Align="Center" />
                                <ComponentArt:GridColumn HeadingText="金额" DataField="ExpenseMoney" Align="Center" FormatString="0.00" />
                                <ComponentArt:GridColumn HeadingText="打印" DataCellServerTemplateId="printTemplate"  Align="Center" />
         
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="GroupByTemplate">
                            <span>## getTitle(DataItem) ##</span>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ServerTemplates>
                        <ComponentArt:GridServerTemplate ID="expenseTypeTemplate">
                            <Template>
                                <%#getExpenseTypeName(Container.DataItem["ExpenseType"].ToString(), Container.DataItem["ID"].ToString())%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="costDetailTemplate">
                            <Template>
                                <%#getCostDetailName(Container.DataItem["CostDetailID"].ToString())%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                        <ComponentArt:GridServerTemplate ID="printTemplate">
                            <Template>
                                <%#getPrintUrl(Container.DataItem["ReturnID"].ToString(),Container.DataItem["ReturnType"].ToString())%>
                            </Template>
                        </ComponentArt:GridServerTemplate>
                    </ServerTemplates> 
                </ComponentArt:Grid>
            </td>
        </tr>
        
    </table>
            

</asp:Content>
