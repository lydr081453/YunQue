function PickerFrom_OnDateChange(sender, eventArgs) {
    var fromDate = PickerFrom.getSelectedDate();
    var toDate = PickerTo.getSelectedDate();
    CalendarFrom.setSelectedDate(fromDate);
    if (fromDate > toDate) {
        PickerTo.setSelectedDate(fromDate);
        CalendarTo.setSelectedDate(fromDate);
    }
}
function PickerTo_OnDateChange(sender, eventArgs) {
    var fromDate = PickerFrom.getSelectedDate();
    var toDate = PickerTo.getSelectedDate();
    CalendarTo.setSelectedDate(toDate);
    if (fromDate > toDate) {
        PickerFrom.setSelectedDate(toDate);
        CalendarFrom.setSelectedDate(toDate);
    }
}
function CalendarFrom_OnChange(sender, eventArgs) {
    var fromDate = CalendarFrom.getSelectedDate();
    var toDate = PickerTo.getSelectedDate();
    PickerFrom.setSelectedDate(fromDate);
    if (fromDate > toDate) {
        PickerTo.setSelectedDate(fromDate);
        CalendarTo.setSelectedDate(fromDate);
    }
}
function CalendarTo_OnChange(sender, eventArgs) {
    var fromDate = PickerFrom.getSelectedDate();
    var toDate = CalendarTo.getSelectedDate();
    PickerTo.setSelectedDate(toDate);
    if (fromDate > toDate) {
        PickerFrom.setSelectedDate(toDate);
        CalendarFrom.setSelectedDate(toDate);
    }
}

function ButtonFrom_OnClick(event) {
    if (CalendarFrom.get_popUpShowing()) {
        CalendarFrom.hide();
    }
    else {
        CalendarFrom.setSelectedDate(PickerFrom.getSelectedDate());
        CalendarFrom.show();
    }
}
function ButtonTo_OnClick(event) {
    if (CalendarTo.get_popUpShowing()) {
        CalendarTo.hide();
    }
    else {
        CalendarTo.setSelectedDate(PickerTo.getSelectedDate());
        CalendarTo.show();
    }
}
function ButtonFrom_OnMouseUp(event) {
    if (CalendarFrom.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function ButtonTo_OnMouseUp(event) {
    if (CalendarTo.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function OkClickFrom() {
    PickerFrom.value = null;
    PickerTo.value = null;
    CalendarFrom.hide();
}

function PickerFrom2_OnDateChange(sender, eventArgs) {
    var PickerFrom = sender;
    var CalendarFrom = window[Calendars[PickerFrom.get_id()]];
    var fromDate = PickerFrom.getSelectedDate();
    CalendarFrom.setSelectedDate(fromDate);
}
function CalendarFrom2_OnChange(sender, eventArgs) {
    var CalendarFrom = sender;
    var PickerFrom = window[Calendars[CalendarFrom.get_id()]];
    var fromDate = CalendarFrom.getSelectedDate();
    PickerFrom.setSelectedDate(fromDate);
}

function ButtonFrom2_OnClick(event, CalendarFromId) {
    var CalendarFrom = window[CalendarFromId];
    if (CalendarFrom.get_popUpShowing()) {
        CalendarFrom.hide();
    }
    else {
        var PickerFrom = window[Calendars[CalendarFrom.get_id()]];
        CalendarFrom.setSelectedDate(PickerFrom.getSelectedDate());
        CalendarFrom.show();
    }
}
function ButtonFrom2_OnMouseUp(event, CalendarFromId) {
    var CalendarFrom = window[CalendarFromId];
    if (CalendarFrom.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

// 开始小时数发生变化时出发的事件
function PickForm1SelectChanged(sender, eventArgs) {
    var PickForm1 = sender;
    var PickForm2 = window[Calendars[PickForm1.get_id()]];
    var txtTime = window.document.getElementById(Calendars[PickForm1.get_id() + "txtTime"]);
    var form1date = PickForm1.getSelectedDate();
    var form2date = PickForm2.getSelectedDate();
    if (form1date > form2date) {
        PickForm2.setSelectedDate(PickForm1.getSelectedDate());
    }
    else {
        form1date = PickForm1.getSelectedDate();
        form2date = PickForm2.getSelectedDate();
        txtTime.value = parseFloat(((form2date - form1date) / 3600000)).toFixed(1);
    }
}
// 结束小时数发生变化时出发的事件
function PickForm2SelectChanged(sender, eventArgs) {
    var PickForm2 = sender;
    var PickForm1 = window[Calendars[PickForm2.get_id()]];
    var txtTime = window.document.getElementById(Calendars[PickForm2.get_id() + "txtTime"]);
    var form1date = PickForm1.getSelectedDate();
    var form2date = PickForm2.getSelectedDate();
    if (form1date > form2date) {
        PickForm1.setSelectedDate(form1date);
    }
    else {
        form1date = PickForm1.getSelectedDate();
        form2date = PickForm2.getSelectedDate();
        txtTime.value = parseFloat(((form2date - form1date) / 3600000)).toFixed(1);
    }
}

function PickerFrom1_OnDateChange(sender, eventArgs) {
    var fromDate = PickerFrom1.getSelectedDate();
    var toDate = PickerTo1.getSelectedDate();
    CalendarFrom1.setSelectedDate(fromDate);
    if (fromDate > toDate) {
        PickerTo1.setSelectedDate(fromDate);
        CalendarTo1.setSelectedDate(fromDate);
    }
}
function PickerTo1_OnDateChange(sender, eventArgs) {
    var fromDate = PickerFrom1.getSelectedDate();
    var toDate = PickerTo1.getSelectedDate();
    CalendarTo1.setSelectedDate(toDate);
    if (fromDate > toDate) {
        PickerFrom1.setSelectedDate(toDate);
        CalendarFrom1.setSelectedDate(toDate);
    }
}
function CalendarFrom1_OnChange(sender, eventArgs) {
    var fromDate = CalendarFrom1.getSelectedDate();
    var toDate = PickerTo1.getSelectedDate();
    PickerFrom1.setSelectedDate(fromDate);
    if (fromDate > toDate) {
        PickerTo1.setSelectedDate(fromDate);
        CalendarTo1.setSelectedDate(fromDate);
    }
}
function CalendarTo1_OnChange(sender, eventArgs) {
    var fromDate = PickerFrom1.getSelectedDate();
    var toDate = CalendarTo1.getSelectedDate();
    PickerTo1.setSelectedDate(toDate);
    if (fromDate > toDate) {
        PickerFrom1.setSelectedDate(toDate);
        CalendarFrom1.setSelectedDate(toDate);
    }
}

function ButtonFrom1_OnClick(event) {
    if (CalendarFrom1.get_popUpShowing()) {
        CalendarFrom1.hide();
    }
    else {
        CalendarFrom1.setSelectedDate(PickerFrom1.getSelectedDate());
        CalendarFrom1.show();
    }
}
function ButtonTo1_OnClick(event) {
    if (CalendarTo1.get_popUpShowing()) {
        CalendarTo1.hide();
    }
    else {
        CalendarTo1.setSelectedDate(PickerTo1.getSelectedDate());
        CalendarTo1.show();
    }
}
function ButtonFrom1_OnMouseUp(event) {
    if (CalendarFrom1.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}
function ButtonTo1_OnMouseUp(event) {
    if (CalendarTo1.get_popUpShowing()) {
        event.cancelBubble = true;
        event.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function cldOverDateTime_OnChange(event) {
    var fromDate = CalendarFrom1.getSelectedDate();
    PickerFrom1.setSelectedDate(fromDate);
}

function pckOverDateTime_OnDateChange(event) {
    var fromDate = PickerFrom1.getSelectedDate();
    CalendarFrom1.setSelectedDate(fromDate);
}
