<?xml version="1.0" encoding="gb2312" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<xsl:apply-templates select="RecordSet/Notify"></xsl:apply-templates>
	</xsl:template>

	<xsl:template match="Notify">
		<table style="width:100%;background-color:Transparent;">
			<tr>
				<td style="border:none;">
					<span>
						<xsl:if test="Level[.='1']">
							<xsl:attribute name="style">color:black;</xsl:attribute>
						</xsl:if>
						<xsl:if test="Level[.='2']">
							<xsl:attribute name="style">color:navy</xsl:attribute>
						</xsl:if>
						<xsl:if test="Level[.='3']">
							<xsl:attribute name="style">color:red</xsl:attribute>
						</xsl:if>
						<xsl:value-of select="Content"></xsl:value-of>
					</span>
				</td>
			</tr>
			<tr>
				<td align="right" style="border:none;">
					---	<xsl:value-of select="Employee/UserName"></xsl:value-of>
					<xsl:text> </xsl:text>
					<xsl:value-of select="Employee/PubTime"></xsl:value-of>
				</td>
			</tr>
		</table>
	</xsl:template>
</xsl:stylesheet>

  