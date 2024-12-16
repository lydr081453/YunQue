using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelTemplate.IDAL;
using ModelTemplate;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace ModelTemplate.DB
{
    public class ModelProcess:IModelProcess
    {
        
        public ModelTemplate.ModelProcess GetModelProcessByID(int processid)
        {

            return initialize("ModelProcessID="+processid+"");

        }

        public ModelTemplate.ModelProcess GetModelProcessByName(string processname)
        {
           
            return initialize("name='"+processname+"'");

        }

        private ModelTemplate.ModelProcess initialize(string sqlwhere)
        {
           Database db = DatabaseFactory.CreateDatabase();
           string sqlCommand = "select ModelProcessID,Name,DisplayName,Version,Author from we_modelprocess where " + sqlwhere;
           DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
           ModelTemplate.ModelProcess process = new ModelTemplate.ModelProcess();

           System.Data.SqlClient.SqlDataReader reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);
           while (reader.Read())
           {
               process.ProcessID = reader.GetValue(0).ToString();
               process.Processname = reader.GetValue(1).ToString();
               process.DisPlayName = reader.GetValue(2).ToString();
               process.Version = reader.GetValue(3).ToString();
               process.Author = reader.GetValue(4).ToString();
           }
           reader.Close();

           sqlCommand = "select TaskID,ModelProcessID,TaskName,DisplayName,TaskType,DeadlineQuantity,OpenType,RoleName,FormData,AutoExeActionName,TaskEndDateCount,FormName from we_modeltask where ModelProcessID=" + process.ProcessID + "";
           dbCommand = db.GetSqlStringCommand(sqlCommand);
           reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);
           while (reader.Read())
           {
               ModelTemplate.ModelTask task = new ModelTask();
               task.TaskID = reader.GetValue(0).ToString();
               task.ModelProcessID = reader.GetValue(1)==DBNull.Value?0:Convert.ToInt32(reader.GetValue(1));
               task.TaskName = reader.GetValue(2).ToString();
               task.DisPlayName = reader.GetValue(3).ToString();
               task.TaskType = reader.GetValue(4).ToString();
               task.DeadLineQuantity = reader.GetValue(5)==System.DBNull.Value?0:Convert.ToInt32(reader.GetValue(5));
               task.OpenType =reader.GetValue(6)==DBNull.Value?0: Convert.ToInt32(reader.GetValue(6));
               task.RoleName = reader.GetValue(7).ToString();
               task.FormData = reader.GetValue(8).ToString();
               task.AutoExeActionName = reader.GetValue(9).ToString();
               task.TaskEndDateCount = reader.GetValue(10)==DBNull.Value? 0:Convert.ToInt32(reader.GetValue(10));
               task.FormName = reader.GetValue(11).ToString();

               process.ModelTaskList.Add(task);
           }
           reader.Close();

           for (int i = 0; i < process.ModelTaskList.Count; i++)
           {
               sqlCommand = "select TransID,ModelTaskID,TransitionName,TransitionTo,ScriptName from we_transition where modeltaskid=" + process.ModelTaskList[i].TaskID + "";
               dbCommand = db.GetSqlStringCommand(sqlCommand);
               reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);
               while (reader.Read())
               {
                   Transition trans = new Transition();
                   trans.TransitionID = reader.GetValue(0)==DBNull.Value ? 0:Convert.ToInt32(reader.GetValue(0));
                   trans.ModelTaskID = reader.GetValue(1)==DBNull.Value ? 0: Convert.ToInt32(reader.GetValue(1));
                   trans.TransitionName = reader.GetValue(2).ToString();
                   trans.TransitionTo = reader.GetValue(3).ToString();
                   trans.ScriptName = reader.GetValue(4).ToString();

                   process.ModelTaskList[i].Transations.Add(trans);
               }
               reader.Close();
           }

           sqlCommand = "select RoleSetID,ModelProcessID,Description from we_roleset where ModelProcessID=" + process.ProcessID + "";
           dbCommand = db.GetSqlStringCommand(sqlCommand);
           reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);
           ModelTemplate.ModelRoleSet roleset = new ModelRoleSet();
           while (reader.Read())
           {
               roleset.RolesetID = reader.GetValue(0)==DBNull.Value? 0: Convert.ToInt32(reader.GetValue(0));
               roleset.ModelProcessID = reader.GetValue(1)==DBNull.Value? 0: Convert.ToInt32(reader.GetValue(1));
               roleset.Description = reader.GetValue(2).ToString();
           }
           reader.Close();

           sqlCommand = "select RoleID,RoleSetID,RoleName,Description from we_roles where RoleSetID=" + roleset.RolesetID + "";
           dbCommand = db.GetSqlStringCommand(sqlCommand);
           reader = (System.Data.SqlClient.SqlDataReader)db.ExecuteReader(dbCommand);
          
           while (reader.Read())
           {
             ModelTemplate.Role role = new Role();
               role.RoleID = reader.GetValue(0)==DBNull.Value ?0: Convert.ToInt32(reader.GetValue(0));
               role.RoleSetID= reader.GetValue(1)==DBNull.Value? 0:Convert.ToInt32(reader.GetValue(1));
             role.RoleName = reader.GetValue(2).ToString();
             role.Description = reader.GetValue(3).ToString();
             roleset.Roles.Add(role);
           }
           reader.Close();

           process.ModelRoleSet = roleset;

           return process;
     
        }

        public int ImportData(string processname, string displayname, string version, string author, List<ModelTemplate.ModelTask> tasks)
        {
            int ret = 0;
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "";
            DbCommand dbCommand;
            int processid=0;
            int taskid=0;
            int rolsetid = 0;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {

                    // insert model process
                    sqlCommand = "insert into we_modelprocess(name,displayname,version,author) values(@name,@displayname,@version,@author)";
                    dbCommand = db.GetSqlStringCommand(sqlCommand);
                    DbDataReader dataReader = null;
                    db.AddInParameter(dbCommand, "@name", DbType.String, processname);
                    db.AddInParameter(dbCommand, "@displayname", DbType.String, displayname);
                    db.AddInParameter(dbCommand, "@version", DbType.String, version);
                    db.AddInParameter(dbCommand, "@author", DbType.String, author);
                    
                    ret=db.ExecuteNonQuery(dbCommand, transaction);
                    dbCommand.CommandText=  "SELECT   @@IDENTITY   AS   'Identity'";
                    dataReader = dbCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (dataReader.GetValue(0)!=DBNull.Value)
                            processid = Convert.ToInt32(dataReader.GetValue(0));
                    }
                    dataReader.Close();
                    //get model process id
                    //sqlCommand = "select ModelProcessID from we_modelprocess where name=@name and displayname=@displayname and version=@version and author=@author";
                    //dbCommand = db.GetSqlStringCommand(sqlCommand);
                    //db.AddInParameter(dbCommand, "@name", DbType.String, processname);
                    //db.AddInParameter(dbCommand, "@displayname", DbType.String, displayname);
                    //db.AddInParameter(dbCommand, "@version", DbType.String, version);
                    //db.AddInParameter(dbCommand, "@author", DbType.String, author);
                    ////setting model process id value
                    //processid = Convert.ToInt32(db.ExecuteScalar(dbCommand,transaction));
                    //insert roleset
                    sqlCommand = "insert into we_roleset(ModelProcessID,Description) values(@ModelProcessID,@Description)";
                    dbCommand = db.GetSqlStringCommand(sqlCommand);
                    db.AddInParameter(dbCommand, "@ModelProcessID", DbType.String, processid);
                    db.AddInParameter(dbCommand, "@Description", DbType.String, processname);
                    ret = db.ExecuteNonQuery(dbCommand, transaction);
                    // get roleset id
                    sqlCommand = "select RoleSetID from we_roleset where ModelProcessID=@ModelProcessID and Description=@Description";
                    dbCommand = db.GetSqlStringCommand(sqlCommand);
                    db.AddInParameter(dbCommand, "@ModelProcessID", DbType.String, processid);
                    db.AddInParameter(dbCommand, "@Description", DbType.String, processname);
                    //setting roleset id value
                    rolsetid = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));


                    for (int i = 0; i < tasks.Count; i++)
                    {
                        //insert model task
                        sqlCommand = "insert into we_modeltask(ModelProcessID,TaskName,TaskType,DeadlineQuantity,OpenType,RoleName,FormData,AutoExeActionName,TaskEndDateCount,FormName) " +
                                     "values(@ModelProcessID,@TaskName,@TaskType,@DeadlineQuantity,@OpenType,@RoleName,@FormData,@AutoExeActionName,@TaskEndDateCount,@FormName)";
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.AddInParameter(dbCommand, "@ModelProcessID", DbType.String, processid);
                        db.AddInParameter(dbCommand, "@TaskName", DbType.String, tasks[i].TaskName);
                        db.AddInParameter(dbCommand, "@TaskType", DbType.String, tasks[i].TaskType);
                        db.AddInParameter(dbCommand, "@DeadlineQuantity", DbType.String, tasks[i].DeadLineQuantity);
                        db.AddInParameter(dbCommand, "@OpenType", DbType.String, tasks[i].OpenType);
                        db.AddInParameter(dbCommand, "@RoleName", DbType.String, tasks[i].RoleName);
                        db.AddInParameter(dbCommand, "@FormData", DbType.String, tasks[i].FormData);
                        db.AddInParameter(dbCommand, "@AutoExeActionName", DbType.String, tasks[i].AutoExeActionName);
                        db.AddInParameter(dbCommand, "@TaskEndDateCount", DbType.String, tasks[i].TaskEndDateCount);
                        db.AddInParameter(dbCommand, "@FormName", DbType.String, tasks[i].FormName);
                        ret=db.ExecuteNonQuery(dbCommand, transaction);

                        //get modeltask id
                        sqlCommand = "select TaskID from we_modeltask where ModelProcessID=@ModelProcessID and TaskName=@TaskName and TaskType=@TaskType and DeadlineQuantity=@DeadlineQuantity";
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.AddInParameter(dbCommand, "@ModelProcessID", DbType.String, processid);
                        db.AddInParameter(dbCommand, "@TaskName", DbType.String, tasks[i].TaskName);
                        db.AddInParameter(dbCommand, "@TaskType", DbType.String, tasks[i].TaskType);
                        db.AddInParameter(dbCommand, "@DeadlineQuantity", DbType.String, tasks[i].DeadLineQuantity);
                        //setting modeltask id value
                        taskid = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));

                        //insert role
                        sqlCommand = "insert into we_roles(RoleSetID,ModelTaskID,RoleName,Description) values(@RoleSetID,@ModelTaskID,@RoleName,@Description)";
                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        db.AddInParameter(dbCommand, "@RoleSetID", DbType.String, rolsetid);
                        db.AddInParameter(dbCommand, "@ModelTaskID", DbType.String, taskid);
                        db.AddInParameter(dbCommand, "@RoleName", DbType.String, tasks[i].RoleName);
                        db.AddInParameter(dbCommand, "@Description", DbType.String, tasks[i].TaskName);
                        ret = db.ExecuteNonQuery(dbCommand, transaction);

                        for (int j = 0; j < tasks[i].Transations.Count; j++)
                        {
                            sqlCommand = "insert into we_transition(ModelTaskID,TransitionName,TransitionTo,ScriptName) values(@ModelTaskID,@TransitionName,@TransitionTo,@ScriptName)";
                            dbCommand = db.GetSqlStringCommand(sqlCommand);
                            db.AddInParameter(dbCommand, "@ModelTaskID", DbType.String, taskid);
                            db.AddInParameter(dbCommand, "@TransitionName", DbType.String, tasks[i].Transations[j].TransitionName);
                            db.AddInParameter(dbCommand, "@TransitionTo", DbType.String, tasks[i].Transations[j].TransitionTo);
                            db.AddInParameter(dbCommand, "@ScriptName", DbType.String, tasks[i].Transations[j].ScriptName);
                            ret = db.ExecuteNonQuery(dbCommand, transaction);
                        }

                    }
                    transaction.Commit();

                }
                catch(Exception ex)
                {
                   // Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            return processid;  
        }
    }
}
