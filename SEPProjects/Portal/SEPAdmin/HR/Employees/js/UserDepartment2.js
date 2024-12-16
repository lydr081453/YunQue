

var iCallID = 0;

//var _keyAttribute = 'privilegeID';
var _posAttribute = 'positionID';
var _parentAttribute = 'parentID';
var _posName = "positionName";
var _objTree = null;
var _objSelectedModuleArr = "";
var _objSelectedModuleName = "";
var _objSelectedBossArr = "";
var _objSelectedBossName = "";
var _objUpdateModuleArr = "";
var _objUpdateModuleName = "";
var _objUpdateBossArr = "";
var _objUdpateBossName = "";

//新增的部门负责人相关变量
var _bossAttribute = 'bossbox';
var _nodepathAttribute = 'curnodepath';
var _objSelectedBossArr = null;
var _objUpdateBossArr = null;

function show() {

    _objSelectedModuleArr = document.getElementById("ctl00_ContentPlaceHolder1_SelectedModuleArr");
    _objSelectedModuleName = document.getElementById("ctl00_ContentPlaceHolder1_SelectedModuleName");
    _objUpdateModuleArr = document.getElementById('ctl00_ContentPlaceHolder1_UpdateModuleArr');
    _objUpdateModuleName = document.getElementById("ctl00_ContentPlaceHolder1_UpdateModuleName");

    _objSelectedBossArr = document.getElementById('ctl00_ContentPlaceHolder1_SelectedBossArr');
    _objSelectedBossName = document.getElementById("ctl00_ContentPlaceHolder1_SelectedBossName");
    _objUpdateBossArr = document.getElementById('ctl00_ContentPlaceHolder1_UpdateBossArr');
    _objUdpateBossName = document.getElementById("ctl00_ContentPlaceHolder1_UpdateBossName");

}

function showSelect() {
    if (_objSelectedModuleArr.value != "") {
        var iArr = new Array();
        iArr = _objSelectedModuleArr.value.split(",");
        var jArr = new Array();
        for (var j = 0; j < iArr.length; j++) {
            var arr = new Array();
            arr = iArr[j].split("-");
            jArr[j] = arr[0];
        }
        //  setRoleCheck2(jArr);        
    }



function getEvent() {
    if (document.all) return window.event; //如果是ie
    func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) { if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) { return arg0; } }
        func = func.caller;
    }
    return null;
}

function onSubmit() {
    var uparr = new Array();

    var upmvalue = getTreeCheck();
    if (upmvalue.length > 0) {
        _objSelectedModuleArr.value = "";
        _objSelectedModuleName.value = "";
        uparr = upmvalue.split(';');
        _objSelectedModuleArr.value = uparr[0];
        _objSelectedModuleName.value = uparr[1];
    } 
}

function getTreeCheck() {
    var sStrID = '';
    var sStrName = '';


    var oCollections = document.getElementById("floatDiv").getElementsByTagName("input");

    if (oCollections.length < 1) return;

    for (var i = 0; i < oCollections.length; i++) {
        if ((oCollections.item(i).type == 'checkbox' || oCollections.item(i).type == 'radio') && oCollections.item(i).getAttribute(_posAttribute) != null) {
            if (oCollections.item(i).checked) {

                sStrID += oCollections.item(i).getAttribute(_posAttribute);
                sStrName += oCollections.item(i).getAttribute(_posName);
                var parentid = oCollections.item(i).getAttribute(_parentAttribute);
                while (parentid != "0") {
                    var parent = getParentValue(parentid);

                    var p = new Array();
                    p = parent.split(',');
                    parentid = p[2];

                    sStrID += "-" + p[0];
                    sStrName += "-" + p[1];

                }
                sStrID += ",";
                sStrName += ",";

            }
        }
    }

    if (sStrID == "" || sStrName == "") {
        return "";
    }
    else {
        sStrID = sStrID.substring(0, sStrID.length - 1);
        sStrName = sStrName.substring(0, sStrName.length - 1);
        return sStrID + ";" + sStrName;
    }
}

function getParentValue(tagvalue) {
    if (tagvalue == "0") return false;
    var sStr = '';

    var oCollections = document.getElementsByName("chkdep");

    if (oCollections.length < 1) return;

    for (var i = 0; i < oCollections.length; i++) {
        if ((oCollections.item(i).type == 'checkbox' || oCollections.item(i).type == 'radio') && oCollections.item(i).getAttribute(_posAttribute) == tagvalue) {
            sStr += oCollections.item(i).getAttribute(_posAttribute) + ",";
            sStr += oCollections.item(i).getAttribute(_posName) + ",";
            sStr += oCollections.item(i).getAttribute(_parentAttribute);
            break;
        }
    }

    var hiddep = document.getElementsByName("hiddep");
    if (hiddep.length < 1) return;

    for (var i = 0; i < hiddep.length; i++) {
        if ((hiddep.item(i).type == 'hidden') && hiddep.item(i).getAttribute(_posAttribute) == tagvalue) {
            sStr += hiddep.item(i).getAttribute(_posAttribute) + ",";
            sStr += hiddep.item(i).getAttribute(_posName) + ",";
            sStr += hiddep.item(i).getAttribute(_parentAttribute);
            break;
        }
    }

    return sStr;

}