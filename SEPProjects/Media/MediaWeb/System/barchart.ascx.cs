using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_barchart : System.Web.UI.UserControl
{

    private String _sXAxisTitle;
    private String _sChartTitle;
    private Int32 _iUserWidth = 330;
    private Int32 _iUserHeight = 5;
    private String[] _sYAxisItems;
    private Int32[] _iYAxisValues;

    public Int32 UserWidth
    {
        get { return _iUserWidth; }
        set { _iUserWidth = value; }
    }

    public Int32 UserHeight
    {
        get { return _iUserHeight; }
        set { _iUserHeight = value; }
    }


    public Int32[] YAxisValues
    {
        get { return _iYAxisValues; }
        set { _iYAxisValues = value; }
    }

    public String[] YAxisItems
    {
        get { return _sYAxisItems; }
        set { _sYAxisItems = value; }
    }

    public String XAxisTitle
    {
        get { return _sXAxisTitle; }
        set { _sXAxisTitle = value; }
    }

    public String ChartTitle
    {
        get { return _sChartTitle; }
        set { _sChartTitle = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        // As long as we have values to display, do so
        if (_iYAxisValues != null)
        {

            // Color array
            String[] sColor = new String[10];
            sColor[0] = "../images/vbg1.gif";
            sColor[1] = "../images/vbg2.gif";
            sColor[2] = "../images/vbg3.gif";
            sColor[3] = "../images/vbg4.gif";
            sColor[4] = "../images/vbg5.gif";
            sColor[5] = "../images/vbg6.gif";
            sColor[6] = "../images/vbg1.gif";
            sColor[7] = "../images/vbg2.gif";
            sColor[8] = "../images/vbg3.gif";
            sColor[9] = "../images/vbg4.gif";

            // Initialize the color category
            Int32 iColor = 0;

            // Display the chart title
            lblChartTitle.Text = _sChartTitle;

            double sumval=0;
            for (int i = 0; i < _iYAxisValues.Length; i++)
            {
                sumval+= _iYAxisValues[i];

            }

            String sOut = "";
            double percentage;
            // Render a bar for each item
            for (int i = 0; i < _iYAxisValues.Length; i++)
            {
                // Only display this item if we have a value to display
                if (_iYAxisValues[i] > 0)
                {
                    percentage = _iYAxisValues[i] / sumval;
                  
                    sOut += "<tr><td align=left width=\"20%\">" + _sYAxisItems[i] + "</td>";

                    sOut += "<td align=\"left\" width=\"" + UserWidth + "\"><img src=\"" + sColor[i] + "\" width=\"" + Convert.ToInt32(percentage * UserWidth).ToString() + "\" height=\"" + UserHeight + "\"/></td>";

                    if (i == 0)
                        sOut += "<td align=left width=\"20%\"><img src=\"../images/N1.gif\" width=\"15px\" height=\"15px\"/>" + _iYAxisValues[i] + "</td></tr>";
                    else if (i == 1)
                        sOut += "<td align=left width=\"20%\"><img src=\"../images/N2.gif\"  width=\"15px\" height=\"15px\"/>" + _iYAxisValues[i] + "</td></tr>";
                    else if (i == 2)
                        sOut += "<td align=left width=\"20%\"><img src=\"../images/N3.gif\"  width=\"15px\" height=\"15px\"/>" + _iYAxisValues[i] + "</td></tr>";
                    else
                        sOut += "<td align=left width=\"20%\">" + _iYAxisValues[i] + "</td></tr>";
                  
                    sOut += "<tr><td colspan=3></td></tr>";
                    iColor++;

                    // If we have reached the end of our color array, start over
                    if (iColor > 10) iColor = 0;
                }
            }

            // Place the rendered string in the appropriate label
            lblItems.Text = sOut;

            // Drop in the Y Axis label
            lblXAxisTitle.Text = _sXAxisTitle;
        }
    }


}
