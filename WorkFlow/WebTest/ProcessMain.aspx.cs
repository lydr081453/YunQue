using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WorkFlowDAO;
using WorkFlow.Model;
using WorkFlowLibary;
using WorkFlowImpl;
using ModelTemplate;
using ModelTemplate.BLL;

public partial class ProcessMain : System.Web.UI.Page
{
    WFUSERS[] initiators;//工作流的发起者，有可能是由多个人同时发起创建的
    IList<PROCESSINSTANCES> processes;//获取持久层现有的工作流列表，实际使用中不需要，测试用
    WFProcessMgrImpl processMgr = new WFProcessMgrImpl();//持久层工作流的管理类
    ModelProcess mp;//模板工作流的实例
    IWFProcess np;//持久层的工作流实例(接口对象)
    Hashtable context = new Hashtable();//所有工作流对外对象的存储器
    WorkFlowDAO.ProcessInstanceDao p;//工作流数据访问对象
    PROCESSINSTANCES pi;//一个工作流实例
    ModelTemplate.BLL.ModelManager manager = new ModelTemplate.BLL.ModelManager();//模板工作流的管理类，用于操作模板工作流的
    private int ret=0;

    protected void Page_Load(object sender, EventArgs e)
    {

            CssHelper.AddStyleSheet(this, "StyleSheet.css");
 
                if (!string.IsNullOrEmpty(Request["processid"]))
                {
                    //根据模板工作流ID获取模板工作流的结构
                    mp = manager.loadProcessModelByID(Convert.ToInt32(Request["processid"]));
                    context = new Hashtable();
                    //创建一个发起者，实际应用时就是单据的创建者
                    WFUSERS wfuser = new WFUSERS();
                    wfuser.Id = 1;
                    initiators = new WFUSERS[1];
                    initiators[0] = wfuser;
                    context.Add(ContextConstants.CURRENT_USER, wfuser);//将发起人加入上下文
                    context.Add(ContextConstants.CURRENT_USER_ASSIGNMENT, initiators);//将发起人数组加入上下文
                    context.Add(ContextConstants.SUBMIT_ACTION_TYPE, "1");//提交操作代码：1
                    context.Add(ContextConstants.SUBMIT_ACTION_NAME, "PR单业务审核");//提交操作代码：1
                    context.Add(ContextConstants.SUBMIT_ACTION_DISPLAYNAME, "PR单业务审核");//提交操作代码：1
                    //设置生成工作流的必备项
                    np = processMgr.preCreate_process(Convert.ToInt32(mp.ProcessID), context);
                 
#region 测试用
                    p = new WorkFlowDAO.ProcessInstanceDao();
                    processes = p.getProcessFillPage();

                    this.GridView1.DataSource = processes;
                    this.GridView1.DataBind();

#endregion
                }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GridView2.DataSource = p.load_processinstance(this.GridView1.SelectedRow.Cells[1].Text).WORKITEMSs;
        this.GridView2.DataBind();
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        np.load_task(this.GridView2.SelectedRow.Cells[1].Text, this.GridView2.SelectedRow.Cells[2].Text);

        ((WFActivityImpl)np.get_lastActivity()).ActiveEvent += new MySubject.ActiveHandler(ProcessMain_ActiveEvent);
        np.get_lastActivity().active();
        Response.Redirect("ProcessMain.aspx?processid=" + Request["processid"]);
    }

    void ProcessMain_ActiveEvent(Hashtable context)
    {
        
    }

    protected void btnCompleteTask_Click(object sender, EventArgs e)
    {
        np.load_task(this.GridView2.SelectedRow.Cells[1].Text, this.GridView2.SelectedRow.Cells[2].Text);

        ((MySubject)np).TaskCompleteEvent += new MySubject.TaskCompleteHandler(ProcessMain_TaskCompleteEvent);

        np.complete_task(this.GridView2.SelectedRow.Cells[1].Text, this.GridView2.SelectedRow.Cells[2].Text, context[ContextConstants.SUBMIT_ACTION_NAME].ToString());
        // 试图结束流程，具体流程状态由WfProcess自行决定

        ((MySubject)np).TryCompleteEvent += new MySubject.TrycompleteHandler(ProcessMain_TryCompleteEvent);

        np.trycomplete();

    }

    void ProcessMain_TryCompleteEvent(Hashtable context)
    {
        
    }

    void ProcessMain_TaskCompleteEvent(Hashtable context)
    {
        
    }

    protected void btnTmp_Click(object sender, EventArgs e)
    {
        
           
                ModelTemplate.BLL.ModelManager manager = new ModelManager();
                List<ModelTemplate.ModelTask> lists = new List<ModelTask>();
                ModelTemplate.ModelTask task = new ModelTask();
                task.TaskName = "start";
                task.TaskType = WfStateContants.TASKTYPE_SERIALTASK;
                task.DisPlayName = "start";
                task.RoleName = "garry zhang";


                ModelTemplate.ModelTask task2 = new ModelTask();
                task2.TaskName = "peng.wang";
                task2.TaskType =WfStateContants.TASKTYPE_EXCLUSIVE;
                task2.DisPlayName = "peng.wang";
                task2.RoleName = "peng.wang";

                ModelTemplate.Transition trans3 = new Transition();
                trans3.TransitionName = task2.TaskName;
                trans3.TransitionTo = "end";
                task2.Transations.Add(trans3);

                ModelTemplate.ModelTask task3 = new ModelTask();
                task3.TaskName = "david.duan";
                task3.TaskType = WfStateContants.TASKTYPE_EXCLUSIVE;
                task3.DisPlayName = "david.duan";
                task3.RoleName = "david.duan";

                ModelTemplate.Transition trans4 = new Transition();
                trans4.TransitionName = task3.TaskName;
                trans4.TransitionTo = "end";
                task3.Transations.Add(trans4);


                ModelTemplate.Transition trans = new Transition();
                trans.TransitionName = task.TaskName;
                trans.TransitionTo = task2.TaskName;

                ModelTemplate.Transition trans1 = new Transition();
                trans1.TransitionName = task.TaskName;
                trans1.TransitionTo = task3.TaskName;
               
                task.Transations.Add(trans);
                task.Transations.Add(trans1);

                lists.Add(task);
                lists.Add(task2);
                lists.Add(task3);
                ret = manager.ImportData("test", "test", "1.0", "garry zhang", lists);

                Response.Redirect("ProcessMain.aspx?processid="+ret.ToString());
               

              

    }
    protected void txtCreate_Click(object sender, EventArgs e)
    {
        //在内存中创建工作流
        processMgr.create_process(np, initiators, "start");


        pi = np.get_instance_data();//获取工作流数据
        pi.ACTIVEPERSONID = 1;
        pi.ACTIVEWOEKITEMID = 1;
        pi.NOTIFYPARENTPROCESS = 1;
        pi.PARENTADDRESS = null;
        //以上4参数暂时没有特别用出，使用默认值即可
        //持久化工作流的事件处理
        ((MySubject)np).PersistEvent += new MySubject.PersistHandler(ProcessMain_PersistEvent);
        np.persist();//持久化

        Response.Redirect("ProcessMain.aspx?processid=" +Request["processid"]);
    }

    void ProcessMain_PersistEvent(Hashtable context)
    {
        
    }
    protected void btnSuspend_Click(object sender, EventArgs e)
    {
        ((MySubject)np).CompleteEvent += new MySubject.CompleteHandler(ProcessMain_CompleteEvent);
        np.complete();
    }

    void ProcessMain_CompleteEvent(Hashtable context)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('complete');", true);
    }
    protected void btnSuspendTask_Click(object sender, EventArgs e)
    {

    }
    protected void btnResumeTask_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        WorkFlowDAO.ProcessInstanceDao dao = new ProcessInstanceDao();
        dao.TerminateProcess(186,181);
    }
}
