<?xml version="1.0" encoding="gb2312" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

  <!-- Department Modify XSLT file -->

  <xsl:template match="/">
    <ul STYLE="DISPLAY:block">
      <li>
        <a positionID="-1" privilegeID="-1">
          <nobr>
            <img width="16" height="15" src="../../images/fc.gif" />
            <span >   物料类别</span>
          </nobr>
        </a>
        <ul STYLE="DISPLAY:block">
          <xsl:apply-templates select="recordset/row">
            <xsl:sort select="@sort" />
          </xsl:apply-templates>
        </ul>
      </li>
    </ul>
  </xsl:template>

  <xsl:template match="row">
    <li>
      <a>
        <xsl:attribute name="positionID">
          <xsl:value-of select="@positionID" />
        </xsl:attribute>
        <xsl:attribute name="privilegeID">
          <xsl:value-of select="@privilegeID" />
        </xsl:attribute>
        <nobr>
          <img width="16" height="15">
            <xsl:if test="@type[.='1']">
              <xsl:attribute name="src">../../images/dc.gif</xsl:attribute>
            </xsl:if>
            <xsl:if test="@type[.='0']">
              <xsl:attribute name="src">../../images/fc.gif</xsl:attribute>
            </xsl:if>
          </img>
          <span unselectable="on" style="margin-left:4px;">
            <xsl:attribute name="title">
              <xsl:value-of select="@description" />
            </xsl:attribute>
            <xsl:value-of select="@name" />
          </span>
        </nobr>
      </a>
      <xsl:if test="row">
        <ul>
          <xsl:apply-templates>
            <xsl:sort select="@sort" />
          </xsl:apply-templates>
        </ul>
      </xsl:if>
    </li>
  </xsl:template>

</xsl:stylesheet>