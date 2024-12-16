<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportLeaveInfo.aspx.cs"
    Inherits="AdministrativeWeb.Attendance.ImportLeaveInfo" MasterPageFile="~/Default.Master" %>
<%@ OutputCache Duration="1" Location="none" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../css/a.css" rel="stylesheet" type="text/css" />

    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="17">
                            <img src="../images/t2_03.jpg" width="21" height="20" />
                        </td>
                        <td align="left">
                            <strong>年假导入 </strong>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td align="right" width="15%">
                            <strong>年假数据导入：</strong>
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="fileARLeave" runat="server" Width="40%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;                        
                        </td>
                        <td>
                            <asp:Button ID="btnImport" runat="server" Text=" 导 入 " 
                                onclick="btnImport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <strong>模板下载：</strong>
                        </td>
                        <td align="left">
                            <img src="../images/ex.jpg" width="16" height="16" style="margin: 0 2px -4px 0px;" />
                            <a href="/ExcelTemplate/AnnualLeaveTemplate.xls" style="color: #000000; text-decoration: underline;">年假数据统计模板.xls</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;                        
                        </td>
                        <td>
            	            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                           <strong>注意事项：</strong>
                        </td>
                        <td>
                           1、请使用本模版导入年假信息；<br />
                           2、请不要修改本模版的标题及其位置； <br />
                           3、请保证员工编号的正确性。<br />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="border">
                    <tr>
                        <td class="td">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
