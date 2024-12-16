<%@ Page Language="C#" AutoEventWireup="true" Inherits="Media_ReporterFavorite" MasterPageFile="~/MasterPage.master" Codebehind="ReporterFavorite.aspx.cs" %>

<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">

    <script type="text/javascript">
   //选择记者
      function selectone(status)
     {     
     if(status == "1")
    {
        var   Element=document.getElementsByName("chkRep1"); 
    }
    else if(status =="2")
    {
        var   Element=document.getElementsByName("chkRep2");            
    }      
        var ids = "";
        for(var  j=0;j<Element.length;j++)   
        {  
            if(Element[j].checked)   
            { 
                ids += Element[j].value + ",";                 
            }
         }
         ids = ids.substring(0,ids.length-1); 
               
         if(ids != ""){
         
            if(status == "1")
               {
               
               window.location.href='ReporterFavorite.aspx?ids=' + ids +'&action=del';
                 
               }
             else if(status =="2")
              {
              
                 window.location.href='ReporterFavorite.aspx?ids='+ ids +'&action=add';
              } 
            
              
         }else{
            alert("请选择记者");
            return false;
         }
      }
function selectedcheck(parent,sub)
{   

   
    var chkSelect = document.getElementById("chk"+parent);
    var elem = document.getElementsByName("chk"+sub);
   
    for(i=0;i<elem.length;i++)
    {
        if(elem[i].type == "checkbox")
        {
            elem[i].checked = chkSelect.checked;
        }
    }
}  
      

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">

<input type="hidden" id="hidUnList" name="hidUnList" runat="server" />
<input type="hidden" id="hidList"  name="hidList" runat="server" />
    <table width="100%">              
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td colspan="4" class="headinglist">
                            已收藏的记者：
                        </td>
                    </tr>  
                     <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgList" runat="server" OnRowDataBound="dgList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr>                                     
                     <tr>
                        <td align="right">
                            <input type="button" id="btnDel" value="删除收藏" class="widebuttons" onclick="selectone('1');"
                                style="width: 83px" />
                            <asp:Button ID="btnReporterSign" runat="server" CssClass="widebuttons" Text="下载签到表" OnClick="btnReporterSign_Click" />
                            <asp:Button ID="btnReporterContact" runat="server" CssClass="widebuttons" Text="下载联络表" OnClick="btnReporterContact_Click" />
                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr> 
        <tr><td style="height:30px"></td></tr>       
        <tr>
            <td>
                <table width="100%" border="0">                 
                <tr>
                        <td colspan="4" class="headinglist">
                            未收藏的记者：
                        </td>
                    </tr>                     
                    <tr>
                        <td colspan="4" align="center">
                            <cc4:MyGridView ID="dgUnList" runat="server" OnRowDataBound="dgUnList_RowDataBound">
                            </cc4:MyGridView>
                        </td>
                    </tr> 
                    <tr>
                        <td align="right">
                            
                            <input type="button" id="btnAdd" value="添加收藏" class="widebuttons" onclick="selectone('2');"
                                style="width: 83px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

