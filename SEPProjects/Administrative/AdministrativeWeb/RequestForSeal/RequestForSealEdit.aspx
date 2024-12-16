<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="RequestForSealEdit.aspx.cs" Inherits="AdministrativeWeb.RequestForSeal.RequestForSealEdit" %>

<%@ Register Src="~/UserControls/ddlDepartments.ascx" TagPrefix="uc1" TagName="ddlDepartments" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/RequestForSeal/Ctl_AuditLog.ascx" TagPrefix="uc1" TagName="Ctl_AuditLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/calendarStyleSelect.css" rel="stylesheet" type="text/css" />
    <script src="../js/DatePicker.js" type="text/javascript"></script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td colspan="2" style="font-size: 15px; font-weight: bold;">用印申请</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">申请人：</td>
                        <td>
                            <asp:Label ID="labRequestorName" runat="server" /><asp:HiddenField ID="hidRequestorId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" style="text-align: right">公司：</td>
                        <td>
                            <asp:DropDownList ID="ddlBrandch" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">部门：</td>
                        <td>
                            <uc1:ddlDepartments runat="server" ID="ddlDepartments" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">关联单据号：</td>
                        <td>
                            <asp:TextBox ID="txtDataNum" runat="server" AutoPostBack="true" OnTextChanged="txtDataNum_TextChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">日期：</td>
                        <td>
                            <ComponentArt:Calendar ID="PickerFrom1" runat="server" PickerFormat="Custom" PickerCustomFormat="yyyy-MM-dd"
                                ControlType="Picker" PickerCssClass="picker">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="pckOverDateTime_OnDateChange" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                            <img id="calendar_from_button1" alt="" onclick="ButtonFrom1_OnClick(event)" onmouseup="ButtonFrom1_OnMouseUp(event)"
                                class="calendar_button" src="../images/calendar/btn_calendar.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">用印文件名称：</td>
                        <td>
                            <asp:TextBox ID="txtFileName" runat="server" Width="350px" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">文件份数：</td>
                        <td>
                            <asp:TextBox ID="txtFileQuantity" runat="server" Text="1" Width="50px" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">文件类别：</td>
                        <td>
                            <asp:DropDownList ID="ddlFileType" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">印章类型：</td>
                        <td>
                            <asp:DropDownList ID="ddlSealType" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">用印文件：<asp:HiddenField ID="hidFiles" runat="server" /></td>
                        <td>
                            <p id="errorMessage" class="error"></p>
                            <p id="errorFileSize" class="error"></p>
                            
                            <table style="margin-left:-5px;">
                                <tr>
                                    <td><input type="file" id="fileInput" name="files" multiple /></td>
                                    <td><button id="uploadButton" class="widebuttons">上传</button></td>
                                    <td>
                                        <table><tr>
                                        <asp:Repeater runat="server" ID="repFiles">
                                            <ItemTemplate>
                                                <td>
                                                <table style="border:1px;">
                                                    <tr>
                                                        <td><asp:LinkButton ID="lnkFile" runat="server" CausesValidation="false" OnClick="lnkFile_Click" CommandArgument="<%# Container.DataItem.ToString() %>" Text="<%# Container.DataItem.ToString().Split('_')[1] %>" /></td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDel" runat="server" Text="<img src='/new_images/icon-close.gif' />" CausesValidation="false" CommandArgument="<%# Container.DataItem.ToString() %>" OnClick="lnkDel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                    </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </tr></table>
                                    </td>
                                </tr>
                            </table>
                            <div id="progressContainer"></div>
                            <ul class="file-list" id="fileList"></ul>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">备注：</td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="60%" Height="100px" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                            &nbsp;<asp:Button ID="btnSubmit" runat="server" Text="提交审核" OnClick="btnSubmit_Click" />
                            &nbsp;<asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <uc1:Ctl_AuditLog runat="server" ID="Ctl_AuditLog" />
    <ComponentArt:Calendar
        runat="server"
        ID="CalendarFrom1"
        AllowMultipleSelection="false"
        AllowWeekSelection="false"
        AllowMonthSelection="false"
        ControlType="Calendar"
        PopUp="Custom"
        PopUpExpandControlId="calendar_from_button1"
        CalendarTitleCssClass="title"
        DayHeaderCssClass="dayheader"
        DayCssClass="day"
        DayHoverCssClass="dayhover"
        OtherMonthDayCssClass="othermonthday"
        SelectedDayCssClass="selectedday"
        CalendarCssClass="calendar"
        NextPrevCssClass="nextprev"
        MonthCssClass="month"
        SwapSlide="Linear"
        SwapDuration="300"
        DayNameFormat="Short"
        ImagesBaseUrl="../images/calendar"
        PrevImageUrl="cal_prevMonth.gif"
        NextImageUrl="cal_nextMonth.gif">
        <ClientEvents>
            <SelectionChanged EventHandler="cldOverDateTime_OnChange" />
        </ClientEvents>
    </ComponentArt:Calendar>

    <script>
        
        // 存储选择的文件列表
        let selectedFiles = [];
        const supportedFormats = ['pdf', 'doc', 'docx', 'xls', 'xlsx', 'jpeg', 'jpg', 'png', 'gif', 'eml', 'msg','zip','rar'];

        function formatFileSize(size) {
            if (size >= 1048576) {
                return (size / 1048576).toFixed(2) + ' MB';
            } else {
                return (size / 1024).toFixed(2) + ' KB';
            }
        }

        document.getElementById('fileInput').addEventListener('change', () => {
            const files = document.getElementById('fileInput').files;
            const fileList = document.getElementById('fileList');
        fileList.innerHTML = '';
        selectedFiles = [];
            const errorMessage = document.getElementById('errorMessage');
            const errorFileSize = document.getElementById('errorFileSize');

        errorMessage.textContent = '';
        errorFileSize.textContent = '';

        for (let i = 0; i < files.length; i++) {
        const file = files[i];
        selectedFiles.push(file);
        const fileExtension = file.name.split('.').pop().toLowerCase();

        const listItem = document.createElement('li');
        listItem.setAttribute('id', `file_${i}`);

        const fileInfo = document.createElement('div');
        const fileName = document.createElement('span');
        const fileSize = document.createElement('span');

        if (file.name.length > 20) {
            fileName.textContent = file.name.substring(0, 15)+"...";
        }
        else {
            fileName.textContent = file.name;
        }

        const deleteButton = document.createElement('button');
        deleteButton.textContent = '删除';
        deleteButton.addEventListener('click', () => {
            selectedFiles = selectedFiles.filter((_, index) => index !== i);
            updateFileList();
            updateFileInput();
        });

        fileSize.textContent = formatFileSize(file.size);
        fileSize.className = 'file-size';


        if (!supportedFormats.includes(fileExtension)) {
            listItem.style.color = 'red';
            errorMessage.textContent = `不支持的文件格式: ${fileExtension}`;
        }
        if (file.size > 10 * 1024 * 1024) {
            listItem.style.color = 'red';
            errorFileSize.textContent = `只能上传10MB以内的文件`;
        }
        fileInfo.appendChild(fileName);
        fileInfo.appendChild(fileSize);
        fileInfo.appendChild(deleteButton);
        listItem.appendChild(fileInfo);
        fileList.appendChild(listItem);


        const progressWrapper = document.createElement('div');
        progressWrapper.classList.add('progress-wrapper');

        const progressBar = document.createElement('progress');
        progressBar.max = 100;
        progressBar.value = 0;
        progressBar.setAttribute('id', `progress_${file.name}`); // 设置进度条的 ID

        const statusText = document.createElement('span');
        statusText.textContent = '0%';

        progressWrapper.appendChild(progressBar);
        progressWrapper.appendChild(statusText);
        fileList.appendChild(progressWrapper);

        }
        });

        document.getElementById('uploadButton').addEventListener('click', (event) => {
        event.preventDefault(); // 阻止表单默认提交行为
        const files = document.getElementById('fileInput').files;

        if (selectedFiles.length === 0) {
        alert('请选择上传文件.');
        return;
        }
        const totalFiles = selectedFiles.length;
        let uploadedFiles = 0;

        for (let i = 0; i < selectedFiles.length; i++) {
        const file = selectedFiles[i];
        const progressBar = document.getElementById(`progress_${file.name}`);
        const statusText = progressBar.nextElementSibling;

        uploadFile(file, progressBar, statusText, () => {
            uploadedFiles++;
            if (uploadedFiles === totalFiles) {
            // 所有文件上传完成后清空 selectedFiles 和重置文件输入框
                selectedFiles = [];
                document.getElementById('fileInput').value = '';
        }
        });
        }
        });

        function uploadFile(file, progressBar, statusText) {
        const formData = new FormData();
        formData.append('file', file);
        const xhr = new XMLHttpRequest();
        xhr.open('POST', 'UploadHandler.ashx', true);
        xhr.upload.addEventListener('progress', (e) => {
        if (e.lengthComputable) {
            const percentComplete = (e.loaded / e.total) * 100;
            progressBar.value = percentComplete;
            statusText.textContent = `${Math.round(percentComplete)}%`;
        }
        });
        xhr.onreadystatechange = () => {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    statusText.textContent = '100%';
                    statusText.style.color = 'green';
                    document.getElementById("<%=hidFiles.ClientID%>").value += xhr.responseText + "#";
                } else {
                    statusText.textContent = '上传失败';
                    statusText.style.color = 'red';
        }
            }
        };

                    xhr.send(formData);
        }


        function updateFileInput() {
            const dataTransfer = new DataTransfer();
            selectedFiles.forEach(file => dataTransfer.items.add(file));
            fileInput.files = dataTransfer.files;
        }

        function updateFileList() {
            const fileList = document.getElementById('fileList');
            fileList.innerHTML = '';
            const errorMessage = document.getElementById('errorMessage');
            const errorFileSize = document.getElementById('errorFileSize');

            errorMessage.textContent = '';
            errorFileSize.textContent = '';

            for (let i = 0; i < selectedFiles.length; i++) {
                const file = selectedFiles[i];
                const fileExtension = file.name.split('.').pop().toLowerCase();

                const listItem = document.createElement('li');
                listItem.setAttribute('id', `file_${i}`);

        const fileInfo = document.createElement('div');
        const fileName = document.createElement('span');
        const fileSize = document.createElement('span');

        if (file.name.length > 20)
        {
            fileName.textContent = file.name.substring(0,15)+"...";
        }
        else
        {
            fileName.textContent = file.name;
        }
        fileSize.textContent = formatFileSize(file.size);
        fileSize.className = 'file-size';

        const deleteButton = document.createElement('button');
        deleteButton.textContent = '删除';
        deleteButton.addEventListener('click', () => {
            selectedFiles = selectedFiles.filter((_, index) => index !== i);
        updateFileList();
        updateFileInput();
        });

        fileInfo.appendChild(fileName);
        fileInfo.appendChild(fileSize);
        fileInfo.appendChild(deleteButton);
        listItem.appendChild(fileInfo);
        fileList.appendChild(listItem);

        if (!supportedFormats.includes(fileExtension)) {
            listItem.style.color = 'red';
            errorMessage.textContent = `不支持的文件格式: ${fileExtension}`;
        }
        if (file.size > 10 * 1024 * 1024) {
            listItem.style.color = 'red';
            errorFileSize.textContent = `只能上传10MB以内的文件`;
        }
        const progressWrapper = document.createElement('div');
        progressWrapper.classList.add('progress-wrapper');

        const progressBar = document.createElement('progress');
        progressBar.setAttribute('id', `progress_${file.name}`);
        progressBar.max = 100;
        progressBar.value = 0;

        const statusText = document.createElement('span');
        statusText.textContent = '0%';

        progressWrapper.appendChild(progressBar);
        progressWrapper.appendChild(statusText);
        fileList.appendChild(progressWrapper);
        }
        }

    </script>
</asp:Content>
