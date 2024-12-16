using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace FinanceWeb.Tools
{
    public partial class ResignationHandover : System.Web.UI.Page
    {
        protected Dictionary<string, string> sqlList = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region init sql 
            //离职审批交接
            //@1
                sqlList.Add("select * from SEP_AuditLog where AuditorId =@oldUserId and AuditStatus =0", "update SEP_AuditLog set AuditorId=@newUserId,AuditorName=@newUserName where AuditorId =@oldUserId and AuditStatus =0");

                //报销审批
                //@2
                sqlList.Add(@"select users  from  wf_WorkflowInstances where InstanceId in(
select WorkflowInstanceId from wf_WorkItems where Status =0)  and Users like '%'+@oldUserId+'%'", @"update  wf_WorkflowInstances set Users =replace(convert(nvarchar(4000),users),@oldUserId,@newUserId)   where InstanceId in(
select WorkflowInstanceId from wf_WorkItems where Status =0) and  Users like '%'+@oldUserId+'%'");
                //@3    
            sqlList.Add(@"select * from wf_WorkItemAssignees where  AssigneeId= @oldUserId and WorkItemId in(
select WorkItemId from wf_WorkItems where Status =0 )", @"update wf_WorkItemAssignees set AssigneeId =@newUserId  where  AssigneeId= @oldUserId and WorkItemId in(
select WorkItemId from wf_WorkItems where Status =0 )");
            //@4    
            sqlList.Add(@"select * from F_ExpenseAuditerList where Auditer= @oldUserId and ReturnID in(
select EntityId from wf_WorkItems where Status =0 )", @"update F_ExpenseAuditerList set Auditer=@newUserId ,AuditerName =@newUserName where  Auditer= @oldUserId and ReturnID in(
select EntityId from wf_WorkItems where Status =0 )");

                //付款申请财务第一级
            //@5    
            sqlList.Add(@"select * from  F_ReturnAuditHist where ReturnID in(
select ReturnID from F_Return where PaymentUserID =@oldUserId and ReturnStatus >=100 )
and AuditorUserID = @oldUserId", @"update F_ReturnAuditHist set AuditorUserID =@newUserId ,AuditorEmployeeName=@newUserName where ReturnID in(
select ReturnID from F_Return where PaymentUserID =@oldUserId and ReturnStatus >=100 )
and AuditorUserID = @oldUserId");
            //@6    
            sqlList.Add(@"select * from F_Return where PaymentUserID =@oldUserId and ReturnStatus >=100", @"update F_Return set PaymentUserID =@newUserId ,PaymentEmployeeName=@newUserName 
where PaymentUserID =@oldUserId and ReturnStatus >=100 ");

                //付款申请业务审批
            //@7
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2
)) and TransitionName ='PN付款审核'+@oldUserName+@oldUserId order by ModelTaskID", @"update WE_TRANSITION set TransitionName ='PN付款审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2
)) and TransitionName ='PN付款审核'+@oldUserName+@oldUserId ");
                //@8
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2
)) and TransitionTo ='PN付款审核'+@oldUserName+@oldUserId order by ModelTaskID", @"update  WE_TRANSITION set TransitionTo ='PN付款审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2
)) and TransitionTo ='PN付款审核'+@oldUserName+@oldUserId  ");
                //@9
                sqlList.Add(@"select * from WE_MODELTASK where ModelProcessID in(
select ProcessID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2
) and TaskName ='PN付款审核'+@oldUserName+@oldUserId", @"update WE_MODELTASK set TaskName='PN付款审核'+@newUserName+@newUserId,RoleName =@newUserId where ModelProcessID in(
select ProcessID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2
) and TaskName ='PN付款审核'+@oldUserName+@oldUserId");
                //@10
                sqlList.Add(@"select * from WE_WORKITEMS where INSTANCEID in(
select INSTANCEID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2)
and TASKDISPLAYNAME ='PN付款审核'+@oldUserName+@oldUserId", @"update WE_WORKITEMS set TASKDISPLAYNAME='PN付款审核'+@newUserName+@newUserId,TASKNAME ='PN付款审核'+@newUserName+@newUserId,roleid=@newUserId 
where INSTANCEID in(
select INSTANCEID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2)
and TASKDISPLAYNAME ='PN付款审核'+@oldUserName+@oldUserId");

                //@11    
                sqlList.Add(@"select * from  F_ReturnAuditHist where ReturnID in(
select ReturnID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2 )
and AuditorUserID = @oldUserId", @"update F_ReturnAuditHist set AuditorUserID =@newUserId ,AuditorEmployeeName=@newUserName where ReturnID in(
select ReturnID from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2 )
and AuditorUserID = @oldUserId");
                //@12    
                sqlList.Add(@"select * from F_Return where PaymentUserID =@oldUserId and ReturnStatus =2", @"update F_Return set PaymentUserID =@newUserId ,PaymentEmployeeName=@newUserName 
where PaymentUserID =@oldUserId and ReturnStatus =2 ");

                //采购单业务审批交接
                //@13
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from T_GeneralInfo where status =12
)) and TransitionName ='PR单审核'+@oldUserName+@oldUserId order by ModelTaskID", @"update WE_TRANSITION set TransitionName ='PR单审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from T_GeneralInfo where status =12
)) and TransitionName ='PR单审核'+@oldUserName+@oldUserId");
                //@14
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from T_GeneralInfo where status =12
)) and TransitionTo ='PR单审核'+@oldUserName+@oldUserId order by ModelTaskID", @"update  WE_TRANSITION set TransitionTo ='PR单审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID from T_GeneralInfo where status =12
)) and TransitionTo ='PR单审核'+@oldUserName+@oldUserId ");
                //@15
                sqlList.Add(@"select * from WE_MODELTASK where ModelProcessID in(
select ProcessID from T_GeneralInfo where status =12
) and TaskName ='PR单审核'+@oldUserName+@oldUserId", @"update WE_MODELTASK set TaskName='PR单审核'+@newUserName+@newUserId,RoleName =@newUserId where ModelProcessID in(
select ProcessID from T_GeneralInfo where status =12
) and TaskName ='PR单审核'+@oldUserName+@oldUserId");
                //@16
                sqlList.Add(@"select * from WE_WORKITEMS where INSTANCEID in(
select instanceId from T_GeneralInfo where status =12)
and TASKDISPLAYNAME ='PR单审核'+@oldUserName+@oldUserId", @"update WE_WORKITEMS set TASKDISPLAYNAME='PR单审核'+@newUserName+@newUserId,TASKNAME ='PR单审核'+@newUserName+@newUserId,roleid=@newUserId 
where INSTANCEID in(
select instanceId from T_GeneralInfo where status =12)
and TASKDISPLAYNAME ='PR单审核'+@oldUserName+@oldUserId");

                //项目号审批交接
                //@17
                sqlList.Add(@"select * from F_AuditHistory where AuditStatus=0 and AuditorUserID = @oldUserId", @"update F_AuditHistory set AuditorUserID = @newUserId,AuditorEmployeeName =@newUserName 
where AuditStatus=0 and AuditorUserID = @oldUserId");
                //@18
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionName ='项目号申请单总监业务审核'+@oldUserName+@oldUserId order by ModelTaskID
", @"update WE_TRANSITION set TransitionName ='项目号申请单总监业务审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionName ='项目号申请单总监业务审核'+@oldUserName+@oldUserId ");
                //@19
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionTo ='项目号申请单总监业务审核'+@oldUserName+@oldUserId order by ModelTaskID", @"update  WE_TRANSITION set TransitionTo ='项目号申请单总监业务审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionTo ='项目号申请单总监业务审核'+@oldUserName+@oldUserId ");
                //@20
                sqlList.Add(@"select * from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
) and TaskName ='项目号申请单总监业务审核'+@oldUserName+@oldUserId
", @"update WE_MODELTASK set TaskName='项目号申请单总监业务审核'+@newUserName+@newUserId,RoleName =@newUserId where ModelProcessID in(
select ProcessID  from F_Project where Status =11
) and TaskName ='项目号申请单总监业务审核'+@oldUserName+@oldUserId");
                //@21
                sqlList.Add(@"select * from WE_WORKITEMS where INSTANCEID in(
select INSTANCEID  from F_Project where Status =11)
and TASKDISPLAYNAME ='项目号申请单总监业务审核'+@oldUserName+@oldUserId", @"update WE_WORKITEMS set TASKDISPLAYNAME='项目号申请单总监业务审核'+@newUserName+@newUserId,TASKNAME ='项目号申请单总监业务审核'+@newUserName+@newUserId,roleid=@newUserId 
where INSTANCEID in(
select INSTANCEID  from F_Project where Status =11)
and TASKDISPLAYNAME ='项目号申请单总监业务审核'+@oldUserName+@oldUserId");
                //@22
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionName ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId order by ModelTaskID
", @"update WE_TRANSITION set TransitionName ='项目号申请单总经理业务审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionName ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId ");
                //@23
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionTo ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId order by ModelTaskID", @"update  WE_TRANSITION set TransitionTo ='项目号申请单总经理业务审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionTo ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId ");
                //@24
                sqlList.Add(@"select * from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
) and TaskName ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId
", @"update WE_MODELTASK set TaskName='项目号申请单总经理业务审核'+@newUserName+@newUserId,RoleName =@newUserId where ModelProcessID in(
select ProcessID  from F_Project where Status =11
) and TaskName ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId");
                //@25
                sqlList.Add(@"select * from WE_WORKITEMS where INSTANCEID in(
select INSTANCEID  from F_Project where Status =11)
and TASKDISPLAYNAME ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId", @"update WE_WORKITEMS set TASKDISPLAYNAME='项目号申请单总经理业务审核'+@newUserName+@newUserId,TASKNAME ='项目号申请单总经理业务审核'+@newUserName+@newUserId,roleid=@newUserId 
where INSTANCEID in(
select INSTANCEID  from F_Project where Status =11)
and TASKDISPLAYNAME ='项目号申请单总经理业务审核'+@oldUserName+@oldUserId");
                //@26
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionName ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId order by ModelTaskID
", @"update WE_TRANSITION set TransitionName ='项目号申请单CEO业务审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionName ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId ");
                //@27
                sqlList.Add(@"select * from WE_TRANSITION where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionTo ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId order by ModelTaskID", @"update  WE_TRANSITION set TransitionTo ='项目号申请单CEO业务审核'+@newUserName+@newUserId where  modeltaskid in(select TaskID from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
)) and TransitionTo ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId ");
                //@28
                sqlList.Add(@"select * from WE_MODELTASK where ModelProcessID in(
select ProcessID  from F_Project where Status =11
) and TaskName ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId
", @"update WE_MODELTASK set TaskName='项目号申请单CEO业务审核'+@newUserName+@newUserId,RoleName =@newUserId where ModelProcessID in(
select ProcessID  from F_Project where Status =11
) and TaskName ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId");
                //@29
                sqlList.Add(@"select * from WE_WORKITEMS where INSTANCEID in(
select INSTANCEID  from F_Project where Status =11)
and TASKDISPLAYNAME ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId", @"update WE_WORKITEMS set TASKDISPLAYNAME='项目号申请单CEO业务审核'+@newUserName+@newUserId,TASKNAME ='项目号申请单CEO业务审核'+@newUserName+@newUserId,roleid=@newUserId 
where INSTANCEID in(
select INSTANCEID  from F_Project where Status =11)
and TASKDISPLAYNAME ='项目号申请单CEO业务审核'+@oldUserName+@oldUserId");
            #endregion
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@oldUserId", hidUserId.Value));
            parms.Add(new SqlParameter("@oldUserName", txtUser.Text));
            parms.Add(new SqlParameter("@newUserId", hidHead.Value));
            parms.Add(new SqlParameter("@newUserName", txtProjectHead.Text));

            lab1.Text = "";
            for(int i=0;i<sqlList.Count;i++)
            {
                DataSet ds = DbHelperSQL.Query(sqlList.ElementAt(i).Key, parms.ToArray());
                lab1.Text += "查询"+(i+1)+"数据条数：" + ds.Tables[0].Rows.Count + "条;<br />";
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@oldUserId", hidUserId.Value));
            parms.Add(new SqlParameter("@oldUserName", txtUser.Text));
            parms.Add(new SqlParameter("@newUserId", hidHead.Value));
            parms.Add(new SqlParameter("@newUserName", txtProjectHead.Text));

            lab2.Text = "";
            for (int i = 0; i < sqlList.Count; i++)
            {
                int count = DbHelperSQL.ExecuteSql(sqlList.ElementAt(i).Value, parms.ToArray());
                lab2.Text += "更新" + (i + 1) + "数据条数：" + count + "条;<br />";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("resignationhandover.aspx");
        }
    }
}