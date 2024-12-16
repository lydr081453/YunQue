<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="System_Default" Title="Untitled Page" Codebehind="Default.aspx.cs" %>

<%@ Register Assembly="MattBerseth.WebControls.AJAX" Namespace="MattBerseth.WebControls.AJAX.TabAnimationControl"
    TagPrefix="mb" %>
       
<%@ Register TagPrefix="cc4" Namespace="MyControls.GridView" Assembly="MyControls" %>
<%@ Register TagPrefix="DNG" TagName="DotNetGraph" Src="barchart.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="contentHead" runat="server">
    <link rel="stylesheet" type="text/css" href="tabsheet.css" />
    <script type="text/javascript" src="/public/js/jquery-1.2.6.js"></script>
    <script type="text/javascript">
        $().ready(function() {
            selectMonth();
        });
        function returnurl() {
            var hidurl = document.getElementById("<% = hidUrl.ClientID %>");
            window.location = hidurl.value;
        }

        function selectMonth() {
            document.getElementById("rdMonth").checked = true;
            document.getElementById("rdQuarter").checked = false;
            document.getElementById("rdYear").checked = false;
                document.getElementById("divMonth").style.display = "block";
                document.getElementById("divQuarter").style.display = "none";
                document.getElementById("divYear").style.display = "none";
            
        }

        function selectQuarter() {
            document.getElementById("rdMonth").checked = false;
            document.getElementById("rdQuarter").checked = true;
            document.getElementById("rdYear").checked = false;
                document.getElementById("divQuarter").style.display = "block";
                document.getElementById("divMonth").style.display = "none";
                document.getElementById("divYear").style.display = "none";
        
        }
        function selectYear() {
            document.getElementById("rdMonth").checked = false;
            document.getElementById("rdQuarter").checked = false;
            document.getElementById("rdYear").checked = true;
                document.getElementById("divMonth").style.display = "none";
                document.getElementById("divQuarter").style.display = "none";
                document.getElementById("divYear").style.display = "block";
          
        }

    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="Server">
    <input type="hidden" id="hidUrl" runat="server" />
    <ajaxToolkit:TabContainer ID="jquery" runat="server" CssClass="ajax__tab_jquery-theme"
        Width="100%">
        <ajaxToolkit:TabPanel runat="server" HeaderText="公告信息">
            <ContentTemplate>
                <asp:PlaceHolder ID="phPosts" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="生日提醒">
            <ContentTemplate>
                <asp:PlaceHolder ID="phBirthday" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="积分排行">
            <ContentTemplate>
             <table width="90%">
                    <tr>
                        <td colspan="4">
                            <div id="divMonth">
                            <DNG:DotNetGraph ID="dngchart" UserWidth="330" UserHeight="10" runat="server" /></div>
                            <div id="divQuarter">
                            <DNG:DotNetGraph ID="quarterChart" UserWidth="330" UserHeight="10" runat="server" /></div>
                            <div id="divYear">
                            <DNG:DotNetGraph ID="yearChart" UserWidth="330" UserHeight="10" runat="server" /></div>
                        </td>
                        <tr>
                         <td>
                           <input type="radio" id="rdMonth" onclick="selectMonth();" />月度排行榜
                            <input type="radio" id="rdQuarter" onclick="selectQuarter();"/>季度排行榜
                             <input type="radio" id="rdYear" onclick="selectYear();"/>年度排行榜
                         </td>
                        </tr>
                </table>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <mb:TabAnimationExtender runat="server" TargetControlID="jquery">
        <Animations>
                    <OnActiveTabChanged>
                        <FadeIn />
                    </OnActiveTabChanged>
                    
        </Animations>
    </mb:TabAnimationExtender>
    <br />
    <br />
    <br />
    <br />
    <ajaxToolkit:TabContainer ID="Tabcontainer2" runat="server" CssClass="ajax__tab_jquery-theme"
        Width="100%">
        <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="媒介视线">
            <ContentTemplate>
                <asp:PlaceHolder ID="phMediaView" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="媒体面对面">
            <ContentTemplate>
                <asp:PlaceHolder ID="face2faceholder" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="媒介例会">
            <ContentTemplate>
                <asp:PlaceHolder ID="MeetingHolder" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <mb:TabAnimationExtender ID="TabAnimationExtender1" runat="server" TargetControlID="Tabcontainer2">
        <Animations>
                    <OnActiveTabChanged>
                        <FadeIn />
                    </OnActiveTabChanged>
                    
        </Animations>
    </mb:TabAnimationExtender>
</asp:Content>
