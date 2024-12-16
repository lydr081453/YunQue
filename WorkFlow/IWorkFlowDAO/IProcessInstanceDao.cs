using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlow.Model;

namespace IWorkFlowDAO
{
    public interface IProcessInstanceDao
    {
       int update_process_state(long instanceid, int state);

       int update_workitem_state(long instanceid, int state);

       int save_workitem(WORKITEMS workitem);

       WORKITEMS load_workitem(String workitemid);

       PROCESSINSTANCES load_processinstance(string instanceid);

       int save_synchroactivity(WORKITEMS workitem);

       int delete_synchroactivitylist(string instanceid, string taskname);

       int update_workitem(WORKITEMS workitem);

       int save_processinstance(PROCESSINSTANCES processinstance);

       int get_workitem_activity(long instanceid);

       int get_workitem_total_activity(long instanceid);


        int get_workitem_state(string workitemid);

        IList<PROCESSINSTANCES> getProcessFillPage();

        int updateXorWorkItemActive(string instanceid,string workitemid);
        ArrayList getNorifyList(int modeltaskid, int notifyValue);

        IList<WorkFlow.Model.WORKITEMS> GetProcessRepresent(int instanceid, int processid);

        int TerminateProcess(int processid, int instanceid);
    }
}
