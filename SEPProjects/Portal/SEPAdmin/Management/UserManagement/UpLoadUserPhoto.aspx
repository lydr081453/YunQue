<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpLoadUserPhoto.aspx.cs" Inherits="SEPAdmin.Management.UserManagement.UpLoadUserPhoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../../public/CutImage/css/main.css"type="text/css" rel="Stylesheet"/>
    <script type="text/javascript" src="../../public/CutImage/js/jquery1.2.6.pack.js"></script>
    <script  type="text/javascript" src="../../public/CutImage/js/ui.core.packed.js" ></script>
    <script type="text/javascript" src="../../public/CutImage/js/ui.draggable.packed.js" ></script>
    <script type="text/javascript" src="../../public/CutImage/js/CutPic.js"></script>
    <script type="text/javascript">
        function Step1() {
            $("#Step2Container").hide();           
        }

        function Step2() {
            $("#CurruntPicContainer").hide();
        }
        function Step3() {
            $("#Step2Container").hide();          
       }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="left">
         <!--当前照片-->
         <div id="CurruntPicContainer">
            <div class="title"><b>当前照片</b></div>
            <div class="photocontainer">
                <asp:Image ID="imgphoto" runat="server" ImageUrl="../../public/CutImage/image/man.GIF" />
            </div>
         </div>
         <!--Step 2-->
         <div id="Step2Container">
           <div class="title"><b> 裁切头像照片</b></div>
           <div class="uploadtooltip">您可以拖动照片以裁剪满意的头像</div>                              
                   <div id="Canvas" class="uploaddiv">
                   
                            <div id="ImageDragContainer">                               
                               <asp:Image ID="ImageDrag" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg" CssClass="imagePhoto" ToolTip=""/>                                                        
                            </div>
                            <div id="IconContainer">                               
                               <asp:Image ID="ImageIcon" runat="server" ImageUrl="../../public/CutImage/image/blank.jpg" CssClass="imagePhoto" ToolTip=""/>                                                        
                            </div>                          
                    </div>
                    <div class="uploaddiv">
                       <table>
                            <tr>
                                <td id="Min">
                                        <img alt="缩小" src="../../public/CutImage/image/_c.gif" onmouseover="this.src='../../public/CutImage/image/_c.gif';" onmouseout="this.src='../../public/CutImage/image/_h.gif';" id="moresmall" class="smallbig" />
                                </td>
                                <td>
                                    <div id="bar">
                                        <div class="child">
                                        </div>
                                    </div>
                                </td>
                                <td id="Max">
                                        <img alt="放大" src="../../public/CutImage/image/c.gif" onmouseover="this.src='../../public/CutImage/image/c.gif';" onmouseout="this.src='../../public/CutImage/image/h.gif';" id="morebig" class="smallbig" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="uploaddiv">
                        <asp:Button ID="btn_Image" runat="server" Text="保存头像" OnClick="btn_Image_Click" />
                    </div>           
                    <div style="display:none">
                    <%--图片实际宽度： <asp:TextBox ID="txt_width" runat="server" Text="1" ></asp:TextBox><br />
                    图片实际高度： <asp:TextBox ID="txt_height" runat="server" Text="1" ></asp:TextBox><br />
                    距离顶部： <asp:TextBox ID="txt_top" runat="server"  Text="82"></asp:TextBox><br />
                    距离左边： <asp:TextBox ID="txt_left" runat="server" Text="73"></asp:TextBox><br />
                    截取框的宽： <asp:TextBox ID="txt_DropWidth" runat="server"  Text="120"></asp:TextBox><br />
                    截取框的高： <asp:TextBox ID="txt_DropHeight" runat="server" Text="120"></asp:TextBox><br />
                    放大倍数： <asp:TextBox ID="txt_Zoom" runat="server" ></asp:TextBox>--%>
                    
                    <asp:TextBox ID="txt_width" runat="server" Text="1" ></asp:TextBox><br />
                    <asp:TextBox ID="txt_height" runat="server" Text="1" ></asp:TextBox><br />
                    <asp:TextBox ID="txt_top" runat="server"  Text="82"></asp:TextBox><br />
                    <asp:TextBox ID="txt_left" runat="server" Text="73"></asp:TextBox><br />
                    <asp:TextBox ID="txt_DropWidth" runat="server"  Text="120"></asp:TextBox><br />
                    <asp:TextBox ID="txt_DropHeight" runat="server" Text="120"></asp:TextBox><br />
                    <asp:TextBox ID="txt_Zoom" runat="server" ></asp:TextBox>
                    
                    <%--<asp:HiddenField ID="txt_width" runat="server" Value="1" />
                    <asp:HiddenField ID="txt_height" runat="server" Value="1" />
                    <asp:HiddenField ID="txt_top" runat="server"  Value="82"/>
                    <asp:HiddenField ID="txt_left" runat="server" Value="73"/>
                    <asp:HiddenField ID="txt_DropWidth" runat="server"  Value="120"/>
                    <asp:HiddenField ID="txt_DropHeight" runat="server" Value="120"/>
                    <asp:HiddenField ID="txt_Zoom" runat="server" />--%>
                   </div>
         </div>
     
     </div>
     <div class="right">
         <!--Step 1-->
         <div id="Step1Container">
            <div class="title"><b>更换照片</b></div>
            <div id="uploadcontainer">
                <div class="uploadtooltip">请选择新的照片文件，文件需小于2.5MB</div>
                <div class="uploaddiv"><asp:FileUpload ID="fuPhoto"  runat="server" ToolTip="选择照片"/></div>
                <div class="uploaddiv"><asp:Button ID="btnUpload" runat="server" Text="上 传" onclick="btnUpload_Click" />&nbsp;<asp:Button ID="btnReturn" runat="server" Text="返 回" onclick="btnReturn_Click" /></div>
            </div>
         
         </div>
     </div>
    </div>
    </form>
</body>
</html>
