
var TopList = 10;//填充数据数量

function getValue(url) {
    var xmlHttpRequest;
    var str = "";
    if (window.XMLHttpRequest) //For general cases. 
    {
        xmlHttpRequest = new XMLHttpRequest();
        xmlHttpRequest.open('post', url, false);
        xmlHttpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded;");
        xmlHttpRequest.send('');
        if (xmlHttpRequest.status == 200)
            str = xmlHttpRequest.responseText;
    }
    else //For IE. 
    {
        if (window.ActiveXObject) {
            xmlHttpRequest = new ActiveXObject("Microsoft.XMLHTTP");
            xmlHttpRequest.open("get", url, false);
            xmlHttpRequest.send();
            str = xmlHttpRequest.responseText;
        }
    }
    return str;
}


function window.onfocus() {
    clearValue();
}

function procBusiness(http,obj) {
    document.getElementById("divResult").style.display = "";
    var divResult = document.getElementById("divResult");
    var p = document.getElementById(obj.id).value;

    var regS = new RegExp(p, "gi");
    http = http.substr(1);
    http=http.replace(regS, "<strong><font color=black>" + p + "</font></strong>"); //全部替换

    
    var response = http.split(',');
    var str = "";
    str += "<table width=\"100%\" style=\"background-color:#ffffff;\">";
    for (var i = 0; i < response.length; i++) {
        var ppstr = response[i].split('|');
        str += "<tr onmouseover=\"aa(this);\" onmouseout=\"bb(this);\"><td onclick=\"completeField(this,'" + obj.id + "','" + ppstr[0] + "');\" style=\"cursor:pointer;\">" + ppstr[0] + "</td></tr>";
    }
    divResult.style.width = document.getElementById(obj.id).offsetWidth; +"px";
    var left = calculateOffset(document.getElementById(obj.id), "offsetLeft");
    var top = calculateOffset(document.getElementById(obj.id), "offsetTop") + document.getElementById(obj.id).offsetHeight;
    divResult.style.border = "black 1px solid";
    divResult.style.left = left + "px";
    divResult.style.top = top + "px";


    divResult.innerHTML = str;
    if (document.getElementById(obj.id).value == "" || response.length == 0) {
        clearValue();
    }
} 
function completeField(tdvalue,obj,id) {
    document.getElementById(obj).value = tdvalue.innerText;
    document.getElementById("divResult").style.display = "none";
    
}
function clearValue() {

    document.getElementById("divResult").innerHTML = "";

    document.getElementById("divResult").style.border = "none";
}

function calculateOffset(field, attr) {

    var offset = 0;
    while (field) {
        offset += field[attr];

        field = field.offsetParent;
    }
    return offset;
}


function btnOnClick(obj) {
    var name = document.getElementById("divResult");
    var txtValue = document.getElementById(obj.id).value;
    if (txtValue == "")
        return;
    if (!isIncSym(txtValue)) {
        var str = getValue("/SearchKeyWord.aspx?name=" + escape(txtValue) + "&top=" + TopList);
        if (str != "") {
            str = procBusiness(str, obj);
        }
        else {
            clearValue();
        }
    }
}
function KeyDown() {

    if (event.keyCode == 32) {
        event.keyCode = 0;
        event.returnValue = false;
        return false;
    }
}
function aa(tr) {
    tr.bgColor = "#B8C2D3";
}
function bb(tr) {
    tr.bgColor = "#ffffff";
}

//建立DIV
document.write("<div id=\"divResult\" style=\"position:absolute; font-size:12px;background-color:#ffffff;\"></div>");

/*是否包含系统禁用的字符*/ 
function   isIncSym(ui)   { 
    var   valid=/[\ '\ "\,\ <\> \+\-\*\/\%\^\=\\\!\&\|\(\)\[\]\{\}\:\;\~\`\#\$]+/; 
    return   (valid.test(ui));}