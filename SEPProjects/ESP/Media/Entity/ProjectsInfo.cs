using System;
namespace ESP.Media.Entity
{
    [Serializable]
	public class ProjectsInfo
	{
		#region 构造函数
        public ProjectsInfo()
		{
			projectid = 0;//ProjectID
			projectname = string.Empty;//ProjectName
			productid = 0;//ProductID
			createdbyuserid = 0;//CreatedByUserID
			createdbyusername = string.Empty;//CreatedByUserName
			createddate = string.Empty;//CreatedDate
			status = 0;//Status
			begindate = string.Empty;//BeginDate
			enddate = string.Empty;//EndDate
			del = 0;//删除标记

			projectcode = string.Empty;//ProjectCode
			projectdescription = string.Empty;//ProjectDescription
			companyname = string.Empty;//companyname
			bankname = string.Empty;//bankname
			bankaccount = string.Empty;//bankaccount
			clientid = 0;//clientID
			teamleaderid = 0;//TeamLeaderID
			teamleadername = string.Empty;//TeamLeaderName
			departmentid = 0;//DepartmentID
			departmentname = string.Empty;//DepartmentName
			steps = 0;//steps

            //加客户名称及产品线名称（未持久化到数据库）
            productline_name = string.Empty;
            clientname = string.Empty;
            
		}
		#endregion


		#region 属性
		/// <summary>
		/// ProjectID
		/// </summary>
		private int projectid;
		public int Projectid
		{
			get
			{
				return projectid;
			}
			set
			{
				projectid = value ;
			}
		}


		/// <summary>
		/// ProjectName
		/// </summary>
		private string projectname;
		public string Projectname
		{
			get
			{
				return projectname;
			}
			set
			{
				projectname = value ;
			}
		}


		/// <summary>
		/// ProductID
		/// </summary>
		private int productid;
		public int Productid
		{
			get
			{
				return productid;
			}
			set
			{
				productid = value ;
			}
		}


		/// <summary>
		/// CreatedByUserID
		/// </summary>
		private int createdbyuserid;
		public int Createdbyuserid
		{
			get
			{
				return createdbyuserid;
			}
			set
			{
				createdbyuserid = value ;
			}
		}


		/// <summary>
		/// CreatedByUserName
		/// </summary>
		private string createdbyusername;
		public string Createdbyusername
		{
			get
			{
				return createdbyusername;
			}
			set
			{
				createdbyusername = value ;
			}
		}


		/// <summary>
		/// CreatedDate
		/// </summary>
		private string createddate;
		public string Createddate
		{
			get
			{
				return createddate;
			}
			set
			{
				createddate = value ;
			}
		}


		/// <summary>
		/// Status
		/// </summary>
		private int status;
		public int Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value ;
			}
		}


		/// <summary>
		/// BeginDate
		/// </summary>
		private string begindate;
		public string Begindate
		{
			get
			{
				return begindate;
			}
			set
			{
				begindate = value ;
			}
		}


		/// <summary>
		/// EndDate
		/// </summary>
		private string enddate;
		public string Enddate
		{
			get
			{
				return enddate;
			}
			set
			{
				enddate = value ;
			}
		}


		/// <summary>
		/// 删除标记

		/// </summary>
		private int del;
		public int Del
		{
			get
			{
				return del;
			}
			set
			{
				del = value ;
			}
		}


		/// <summary>
		/// ProjectCode
		/// </summary>
		private string projectcode;
		public string Projectcode
		{
			get
			{
				return projectcode;
			}
			set
			{
				projectcode = value ;
			}
		}


		/// <summary>
		/// ProjectDescription
		/// </summary>
		private string projectdescription;
		public string Projectdescription
		{
			get
			{
				return projectdescription;
			}
			set
			{
				projectdescription = value ;
			}
		}


		/// <summary>
		/// companyname
		/// </summary>
		private string companyname;
		public string Companyname
		{
			get
			{
				return companyname;
			}
			set
			{
				companyname = value ;
			}
		}


		/// <summary>
		/// bankname
		/// </summary>
		private string bankname;
		public string Bankname
		{
			get
			{
				return bankname;
			}
			set
			{
				bankname = value ;
			}
		}


		/// <summary>
		/// bankaccount
		/// </summary>
		private string bankaccount;
		public string Bankaccount
		{
			get
			{
				return bankaccount;
			}
			set
			{
				bankaccount = value ;
			}
		}


		/// <summary>
		/// clientID
		/// </summary>
		private int clientid;
		public int Clientid
		{
			get
			{
				return clientid;
			}
			set
			{
				clientid = value ;
			}
		}


		/// <summary>
		/// TeamLeaderID
		/// </summary>
		private int teamleaderid;
		public int Teamleaderid
		{
			get
			{
				return teamleaderid;
			}
			set
			{
				teamleaderid = value ;
			}
		}


		/// <summary>
		/// TeamLeaderName
		/// </summary>
		private string teamleadername;
		public string Teamleadername
		{
			get
			{
				return teamleadername;
			}
			set
			{
				teamleadername = value ;
			}
		}


		/// <summary>
		/// DepartmentID
		/// </summary>
		private int departmentid;
		public int Departmentid
		{
			get
			{
				return departmentid;
			}
			set
			{
				departmentid = value ;
			}
		}


		/// <summary>
		/// DepartmentName
		/// </summary>
		private string departmentname;
		public string Departmentname
		{
			get
			{
				return departmentname;
			}
			set
			{
				departmentname = value ;
			}
		}


		/// <summary>
		/// steps
		/// </summary>
		private int steps;
		public int Steps
		{
			get
			{
				return steps;
			}
			set
			{
				steps = value ;
			}
		}


        //加客户名称及产品线名称（未持久化到数据库）
        string clientname = string.Empty;

        public string ClientName
        {
            get { return clientname; }
            set { clientname = value; }
        }
        string productline_name = string.Empty;

        public string ProductlineName
        {
            get { return productline_name; }
            set { productline_name = value; }
        }

		#endregion
	}
}
