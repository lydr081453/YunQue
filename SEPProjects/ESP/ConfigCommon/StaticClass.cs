namespace ESP.ConfigCommon
{
    /// <summary>
    /// StaticClass 的摘要说明
    /// </summary>
    public class StaticClass
    {
        public StaticClass()
        {
        }

        //上传文件名称
        public readonly static string upfile_repetition = "证件";
        public readonly static string upfile_cwbb = "财务报表";
        public readonly static string upfile_algp = "光盘";
        public readonly static string upfile_pjtx = "报价体系";

        public readonly static string sitePath = ESP.Configuration.ConfigurationManager.SafeAppSettings["ServerURL"];

        public readonly static string[] title = {
            "公司名称",
            "公司地址",
            "法人代表",
            "注册资金（万元）",
            "公司所有权属性",
            "公司成立时间",
            "邮件列表",
            "公司的历史背景及服务相关行业的年限",
            "公司员工构成及人数",
            "公司营业执照、税务证明、特种行业许可证等有效证件的复印件",  
            "请描述贵公司的主要核心竞争力",
            "公司是否有分支机构，若有，总公司与分支机构的隶属关系？",
            "请描述一下贵公司在行业内的主要竞争对手（3个）",
            "与竞争对手相比，贵公司提供的服务有什么优势？",
            "请确认贵公司与我司是否存在任何潜在的利益冲突可能，如贵公司管理层或员工与我司的业务决策人有亲属或股权关系？若存在一切可能的利益冲突的情况，须如实说明。",
            "贵公司近两年是否有合并、重组或者破产等情况发生或计划？",
            "贵公司是否获得任何行业及服务认证体系？",
            "贵公司在业务流程中，是否运用IT等相关技术？并描述其用途。",
            "贵公司是否有客户满意度评估体系？若有，请列举？",
            "请提供公司近两年的销售业绩增长及财务报表？",
            "请您提供贵公司的组织结构图",
            "贵公司短、中期的商业目标是什么？",
            "贵公司3～5年的商业战略是什么？",
            "请描述贵公司的员工培训和发展计划？",
            "请您列出贵公司当前服务的重要客户",
            "请列举贵公司关于互动网站建设的经典成功案例，案例获奖情况（附光盘提供公司的经典案例集）",
            "附光盘提供公司的经典案例集",
            "请贵公司列举项目实施的工作流程",
            "请贵公司提供互动网站建设所使用的工具（例如：使用何种软件，软件是否正版）",
            "请提供贵公司项目组成员名单及从业经历",
            "请贵公司通常一个项目的外协比率及质量控制体系",
            "请提供贵公司互动网站建设的付款帐期及付款流程",
            "贵公司是否有定期的项目培训、评估和沟通机制？若有，请列举？",
            "除互动网站建设代理业务外，贵公司是否还提供相关其他服务？",
            "请提供贵公司的互动网站建设购买报价体系。"
        };

        public readonly static string first_assessorname = "33";
    }
}