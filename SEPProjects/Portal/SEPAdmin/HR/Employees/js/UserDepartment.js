

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


//    if (_objSelectedModuleArr.value != "") {
//        var iArr = new Array();
//        iArr = _objSelectedModuleArr.value.split(",");
//        var moduleNameArray = new Array();

//        moduleNameArray = setRoleCheck(iArr);
//        if (moduleNameArray.length > 0) {
//            uparr = moduleNameArray.split(';');
//            _objSelectedModuleArr.value = uparr[0];
//            _objSelectedModuleName.value = uparr[1];
//        }
//    }

//    //显示部门负责人
//    if (_objSelectedBossArr.value != "") {
//        var iArr = new Array();
//        iArr = _objSelectedBossArr.value.split(",");
//        var bossNameArray = new Array();

//        bossNameArray = setBossCheck(iArr);
//        if (bossNameArray.legth > 0) {
//            bossarr = bossNameArray.split(';');
//            _objSelectedBossArr.value = bossarr[0];
//            _objSelectedBossName.value = bossarr[1];
//        }
//    }
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

    //显示部门负责人
//    if (_objSelectedBossArr.value != "") {
//        var iArr = new Array();
//        iArr = _objSelectedBossArr.value.split(",");
//        var jArr = new Array();
//        for (var j = 0; j < iArr.length; j++) {
//            var arr = new Array();
//            arr = iArr[j].split('-');
//            jArr[j] = arr[0];
//        }
//            setBossCheck2(jArr);
//    }
}
//function clickDepBoss() {
//    var evt = getEvent();
//    var element = evt.srcElement || evt.target;
//    
//    clickSingleDepBoss(element);
//}

//function clickDepParent() {
//    var evt = getEvent();
//    var element = evt.srcElement || evt.target;
//    click

//}

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

//点击了是部门经理checkbox
//function clickSingleDepBoss(oSrc)
//{
//	//如果是选中，左侧的所属部门必须选中，而且置为Disabled，所有子部门checkbox全部清空，而且置为Disabled
//    //如果是取消选中，左侧的所属部门恢复为可以非Disabled，所有子部门checkbox全部恢复为非Disabled
//    var oCollections = document.getElementById("floatDiv").getElementsByTagName('input');
//   
//	if(oCollections.length < 1) return false;
//	
//	var sCurValue = oSrc.getAttribute(_nodepathAttribute);
//	var sValue = '';
//	
//	for(var i = 0;i<oCollections.length;i++)
//	{
//		//左侧//
//		if (oCollections.item(i).type=='checkbox' && oCollections.item(i).getAttribute(_keyAttribute) != null) {
//			sValue = oCollections.item(i).getAttribute(_keyAttribute);
//			if (sValue==sCurValue) {
//				if (oSrc.checked) {
//					oCollections.item(i).checked = oSrc.checked;
//					oCollections.item(i).disabled = true;
//				}else{
//					oCollections.item(i).disabled = false;
//				}
//			}
//		}
//		//子节点//
//		if(oCollections.item(i).type=='checkbox' && (oCollections.item(i).getAttribute(_keyAttribute) != null || oCollections.item(i).getAttribute(_nodepathAttribute) != null))
//		{
//			if (oCollections.item(i).getAttribute(_keyAttribute) != null) {
//				sValue = oCollections.item(i).getAttribute(_keyAttribute);
//			}
//			if (oCollections.item(i).getAttribute(_nodepathAttribute) != null) {
//				sValue = oCollections.item(i).getAttribute(_nodepathAttribute);
//			}
//			//check the parent
//			if(sValue.substring(0,sCurValue.length)==sCurValue && sCurValue.length < sValue.length)
//			{
//				if(oSrc.checked)
//				{
//					oCollections.item(i).checked = false;
//					oCollections.item(i).disabled = true;
//				}else{
//					oCollections.item(i).disabled = false;
//				}
//			}
//		}
//	}
//}

//function clickSingleDepParent(oSrc) {
//    var oCollections = document.getElementsByTagName('input');

//    if (oCollections.length < 1) return false;

//    var sCurValue = oSrc.getAttribute(_parentAttribute);
//    var sValue = '';
//    
//    for (var i = 0; i < oCollections.length; i++) {
//        if (oCollections.item(i).type == 'checkbox' && oCollections.item(i).getAttribute(_parentAttribute) != null) {
//            sValue = oCollections.item(i).getAttribute(_parentAttribute);
//        }
//    }

//}

//function setBossCheck(iArr) {
//    var oCollections = document.getElementsByName('chkdeps');
//    var sStrID = '';
//    var sStrName = '';
//	
//	if(oCollections.length < 1) return;
//	
//	var j = 0;
//	
//	for(var i = 0;i<oCollections.length;i++)
//	{
//		if((oCollections.item(i).type=='checkbox' || oCollections.item(i).type=='radio') && oCollections.item(i).getAttribute(_bossAttribute) != null )
//		{
//			for(j=0;j<iArr.length;j++)
//			{
//				if(iArr[j]==parseInt(oCollections.item(i).getAttribute(_posAttribute)))
//				{
//				    oCollections.item(i).checked = true;

//				    sStrID += oCollections.item(i).getAttribute(_posAttribute);
//				    sStrName += oCollections.item(i).getAttribute(_posName);
//				    var parentid = oCollections.item(i).getAttribute(_parentAttribute);
//				    while (parentid != "-1") {
//				        var parent = getParentValue(parentid);

//				        var p = new Array();
//				        p = parent.split(',');
//				        parentid = p[2];

//				        sStrID += "-" + p[0];
//				        sStrName += "-" + p[1];

//				    }
//				    sStrID += ",";
//				    sStrName += ",";
//					j = iArr.length;
//					clickSingleDepBoss(oCollections.item(i));
//				}
//			}
//		}
//}
//if (sStrID == "" || sStrName == "") {
//    return "";
//}
//else {
//    sStrID = sStrID.substring(0, sStrID.length - 1);
//    sStrName = sStrName.substring(0, sStrName.length - 1);
//    return sStrID + ";" + sStrName;
//}
//}

//function setBossCheck2(iArr) {
//    var oCollections = document.getElementById("floatDiv").getElementsByTagName("input")   

//    if (oCollections.length < 1) return;

//    var j = 0;

//    for (var i = 0; i < oCollections.length; i++) {
//        if ((oCollections.item(i).type == 'checkbox' || oCollections.item(i).type == 'radio') && oCollections.item(i).getAttribute(_bossAttribute) != null) {
//            for (j = 0; j < iArr.length; j++) {
//                if (iArr[j] == parseInt(oCollections.item(i).getAttribute(_posAttribute))) {
//                    oCollections.item(i).checked = true;                    
//                    j = iArr.length;
//                    clickSingleDepBoss(oCollections.item(i));
//                }
//            }
//        }
//    }

//}

//function getBossCheck()
//{
//    var sStrID = '';
//    var sStrName = '';

//	var oCollections = document.getElementById("floatDiv").getElementsByTagName("input");
//	
//	if(oCollections.length < 1) return;
//	
//	for(var i = 0;i<oCollections.length;i++)
//	{
//	    if ((oCollections.item(i).type == 'checkbox' || oCollections.item(i).type == 'radio') && oCollections.item(i).getAttribute(_bossAttribute) != null)
//		{
//			if(oCollections.item(i).checked)
//			{			    
//			    sStrID += oCollections.item(i).getAttribute(_posAttribute);
//			    sStrName += oCollections.item(i).getAttribute(_posName);
//			    var parentid = oCollections.item(i).getAttribute(_parentAttribute);
//			    while (parentid != "-1") {
//			        var parent = getParentValue(parentid);

//			        var p = new Array();
//			        p = parent.split(',');
//			        parentid = p[2];

//			        sStrID += "-" + p[0];
//			        sStrName += "-" + p[1];

//			    }
//			    sStrID += ",";
//			    sStrName += ",";
//			}
//		}
//	}

//	if (sStrID == "" || sStrName == "") {
//	    return "";
//	}
//	else {
//	    sStrID = sStrID.substring(0, sStrID.length - 1);
//	    sStrName = sStrName.substring(0, sStrName.length - 1);
//	    return sStrID + ";" + sStrName;
//	}
//}



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
  //  alert(_objSelectedModuleArr.value + "/" + _objSelectedModuleName.value);

//    var bossarr = new Array();
//    _objSelectedBossArr.value = "";
//    _objSelectedBossName.value = "";
//    var bossvalue = new Array();
//    bossarr = getBossCheck();
//    if (bossarr.length > 0) {
//        _objSelectedModuleArr.value = "";
//        _objSelectedModuleName.value = "";
//        bossvalue = bossarr.split(';');       
//        _objSelectedBossArr.value = bossvalue[0];
//        _objSelectedBossName.value = bossvalue[1];

 //   }
  //  alert(_objSelectedBossArr.value + "//" + _objSelectedBossName.value);
   
}

//function setRoleCheck2(iArr) {
//    var oCollections = document.getElementById("floatDiv").getElementsByTagName("input");   

//    if (oCollections.length < 1) return;

//    var j = 0;

//    for (var i = 0; i < oCollections.length; i++) {
//        if ((oCollections.item(i).type == 'checkbox' || oCollections.item(i).type == 'radio') && oCollections.item(i).getAttribute(_keyAttribute) != null) {
//            for (j = 0; j < iArr.length; j++) {
//                if (iArr[j] == parseInt(oCollections.item(i).getAttribute(_posAttribute))) {
//                    oCollections.item(i).checked = true;
//                    
//                    j = iArr.length;
//                }
//            }
//        }
//    }
//}

//function setRoleCheck(iArr) {
//    var oCollections = document.getElementsByName('chkdep');
//    var sStrID = '';
//    var sStrName = '';

//    if (oCollections.length < 1) return;

//    var j = 0;

//    for (var i = 0; i < oCollections.length; i++) {
//        if ((oCollections.item(i).type == 'checkbox' || oCollections.item(i).type == 'radio') && oCollections.item(i).getAttribute(_posAttribute) != null) {
//            for (j = 0; j < iArr.length; j++) {
//                if (iArr[j] == parseInt(oCollections.item(i).getAttribute(_posAttribute))) {
//                    oCollections.item(i).checked = true;
//                    sStrID += oCollections.item(i).getAttribute(_posAttribute);
//                    sStrName += oCollections.item(i).getAttribute(_posName);
//                    var parentid = oCollections.item(i).getAttribute(_parentAttribute);
//                    while (parentid != "-1") {
//                        var parent = getParentValue(parentid);

//                        var p = new Array();
//                        p = parent.split(',');
//                        parentid = p[2];

//                        sStrID += "-" + p[0];
//                        sStrName += "-" + p[1];

//                    }
//                    sStrID += ",";
//                    sStrName += ",";
//                    j = iArr.length;
//                }
//            }
//        }
//    }

//    if (sStrID == "" || sStrName == "") {
//        return "";
//    }
//    else {
//        sStrID = sStrID.substring(0, sStrID.length - 1);
//        sStrName = sStrName.substring(0, sStrName.length - 1);
//        return sStrID + ";" + sStrName;
//    }

//}

function getTreeCheck() {
    var sStrID = '';
    var sStrName = '';


    var oCollections = document.getElementById("floatDiv").getElementsByTagName("input");

    if (oCollections.length < 1) return;

    for (var i = 0; i < oCollections.length; i++) {
        if ((oCollections.item(i).type == 'checkbox' || oCollections.item(i).type == 'radio') && oCollections.item(i).getAttribute(_posAttribute) != null) {
            if (oCollections.item(i).checked) {

                sStrID +=  oCollections.item(i).getAttribute(_posAttribute);
                sStrName +=  oCollections.item(i).getAttribute(_posName);
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
        sStrID = sStrID.substring(0, sStrID.length-1);
        sStrName = sStrName.substring(0, sStrName.length-1);
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