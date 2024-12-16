<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFiles.aspx.cs" Inherits="FinanceWeb.project.UploadFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>多文件上传</title>
    <link href="../public/css/Upload.css" rel="stylesheet" />
    <script>
        function back(){
            window.location.href = '/contractfiles/ContractEdit.aspx?ProjectID='+document.getElementById("pid").value+'&backurl=/Search/ProjectTabList.aspx';
        }
    </script>
    <style>
        .btn {
            border:1px solid #000;
        }
    </style>
</head>
<body>
    <input type="hidden" value="<%=Request["pid"] %>" id="pid" />
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div style="font-size:9pt;color:red;">
            上传文件说明：
            <ul>
                <li>文件格式：.pdf, .doc, .docx, .xls, .xlsx, .jpeg, .jpg, .png, .gif, .eml, .msg</li>
                <li>文件大小：单个文件小于等于10MB</li>
                <li>文件命名：建议以证据类型命名，如“上刊截图”，“完工证明”。。。</li>
            </ul>
        </div>
        <br />
        <p id="errorMessage" class="error"></p>
        <p id="errorFileSize" class="error"></p>
        <input type="file" id="fileInput" name="files" multiple />
        <button id="uploadButton" class="widebuttons">上传</button>
        <div id="progressContainer"></div>
        <ul class="file-list" id="fileList"></ul>
        <input type="button" class="widebuttons" onclick="back()" value="返回" />
    </form>
   <%-- <script src="../public/js/Upload.js"></script>--%>
        <script>
        
            // 存储选择的文件列表
            let selectedFiles = [];
        const supportedFormats = ['pdf', 'doc', 'docx', 'xls', 'xlsx', 'jpeg', 'jpg', 'png', 'gif', 'eml', 'msg'];

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
            xhr.open('POST', 'UploadHandler.ashx?pid='+ document.getElementById("pid").value, true);
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
</body>
</html>
