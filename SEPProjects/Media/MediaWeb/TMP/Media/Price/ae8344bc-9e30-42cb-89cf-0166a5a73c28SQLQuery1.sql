SELECT   
表名               =   CASE   WHEN   A.COLORDER=1   THEN   D.NAME   ELSE   ' '   END, 
表说明           =   CASE   WHEN   A.COLORDER=1   THEN   ISNULL(F.VALUE, ' ')   ELSE   ' '   END, 
表序号       =   A.COLORDER, 
字段名           =   A.NAME, colid = a.colid,
是否标示               =   CASE   WHEN   COLUMNPROPERTY(   A.ID,A.NAME, 'ISIDENTITY ')=1   THEN   '√ 'ELSE   ' '   END, 
主键               =   CASE   WHEN   EXISTS(SELECT   1   FROM   SYSOBJECTS   WHERE   XTYPE= 'PK '   AND   PARENT_OBJ=A.ID   AND   NAME   IN   ( 
SELECT   NAME   FROM   SYSINDEXES   WHERE   INDID   IN( 
SELECT   INDID   FROM   SYSINDEXKEYS   WHERE   ID   =   A.ID   AND   COLID=A.COLID)))   THEN   '√ '   ELSE   ' '   END, 
类型               =   B.NAME, 
长度   =   A.LENGTH, 
--長度               =   COLUMNPROPERTY(A.ID,A.NAME, 'PRECISION '), 
小数位       =   ISNULL(COLUMNPROPERTY(A.ID,A.NAME, 'SCALE '),0), 
允许空           =   CASE   WHEN   A.ISNULLABLE=1   THEN   '√ 'ELSE   ' '   END, 
默认职           =   ISNULL(E.TEXT, ' '), 
字段描述       =   ISNULL(G.[VALUE], ' ') 
FROM   
SYSCOLUMNS   A 
LEFT   JOIN   SYSTYPES   B   ON   A.XUSERTYPE=B.XUSERTYPE 
INNER   JOIN  SYSOBJECTS   D  ON  A.ID=D.ID     AND   D.XTYPE= 'U '   AND     D.NAME <> 'DTPROPERTIES ' 
LEFT   JOIN   SYSCOMMENTS   E  ON  A.CDEFAULT=E.ID 
LEFT   JOIN  sys.extended_properties   G   ON  A.ID=G.major_id   AND   A.COLID=G.minor_id     
LEFT   JOIN  sys.extended_properties   F   ON  D.ID=F.major_id   AND   F.minor_id=0 
ORDER   BY   A.ID,A.COLORDER 