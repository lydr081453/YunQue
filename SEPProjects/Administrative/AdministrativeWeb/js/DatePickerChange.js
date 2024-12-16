// 病事假日期变换事件 ---- 开始
function LeavePickerFrom2_OnDateChange(sender, eventArgs) {
    var fromDate = LeavePickerFrom2.getSelectedDate();
    var toDate = LeavePickerTo2.getSelectedDate();
    if (fromDate > toDate) {
        var hours = fromDate.getHours() + 1;
        fromDate.setHours(hours);
        LeavePickerTo2.setSelectedDate(fromDate);
    }
    var minutes = fromDate.getMinutes();
    var toDate2 = LeavePickerTo2.getSelectedDate()
    toDate2.setMinutes(minutes);
    LeavePickerTo2.setSelectedDate(toDate2);
}

function LeavePickerTo2_OnDateChange(sender, eventArgs) {
    var fromDate = LeavePickerFrom2.getSelectedDate();
    var toDate = LeavePickerTo2.getSelectedDate();
    if (fromDate > toDate) {
        var hours = toDate.getHours() - 1;
        toDate.setHours(hours);
        LeavePickerFrom2.setSelectedDate(toDate);
    }
    var minutes = toDate.getMinutes();
    var fromDate2 = LeavePickerFrom2.getSelectedDate()
    fromDate2.setMinutes(minutes);
    LeavePickerFrom2.setSelectedDate(fromDate2);
}
// 病事假日期变换事件 ---- 结束

// 年假日期变换事件 ---- 开始
function LeavePickerFrom1_OnDateChange(sender, eventArgs) {
    var fromDate = LeavePickerFrom1.getSelectedDate();
    var toDate = LeavePickerTo1.getSelectedDate();
    if (fromDate > toDate) {
        LeavePickerTo1.setSelectedDate(fromDate);
    }
}

function LeavePickerTo1_OnDateChange(sender, eventArgs) {
    var fromDate = LeavePickerFrom1.getSelectedDate();
    var toDate = LeavePickerTo1.getSelectedDate();
    if (fromDate > toDate) {
        LeavePickerFrom1.setSelectedDate(toDate);
    }
}
// 年假日期变换事件 ---- 结束

// 婚假、产假、丧假日期变换事件 ---- 开始
function LeavePickerFrom3_OnDateChange(sender, eventArgs) {
    var fromDate = LeavePickerFrom3.getSelectedDate();
    var toDate = LeavePickerTo3.getSelectedDate();
    if (fromDate > toDate) {
        LeavePickerTo3.setSelectedDate(fromDate);
    }
}

function LeavePickerTo3_OnDateChange(sender, eventArgs) {
    var fromDate = LeavePickerFrom3.getSelectedDate();
    var toDate = LeavePickerTo3.getSelectedDate();
    if (fromDate > toDate) {
        LeavePickerFrom3.setSelectedDate(toDate);
    }
}
// 婚假、产假、丧假日期变换事件 ---- 结束
