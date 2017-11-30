using System;
using System.Diagnostics;




namespace GUI_Geruest
{
    public class LineChart
    {
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private PerformanceCounter perfCounter;
        private String dataType;

        public LineChart(System.Windows.Forms.DataVisualization.Charting.Chart chart_in, PerformanceCounter perfCounter_in, String dataType_in, String title)
        {
            chart = chart_in;
            perfCounter = perfCounter_in;
            dataType = dataType_in;
            if (dataType == "Percentage")
            {
                chart.ChartAreas[0].AxisY.ScaleView.Zoom(0, 100);
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0}%";
                chart.Titles.Add(title);
            }
            else if (dataType == "KiB/s")
            {
                chart.Titles.Add(title + " [KiB/s]");
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";

            }
            else if (dataType == "MiB/s")
            {
                chart.Titles.Add(title + " [MiB/s]");
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";
            }
            chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false ;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            
        }

        public void refresh()
        {
            if (dataType == "Percentage")
            {
                chart.Series[0].Points.AddXY(DateTime.Now, perfCounter.NextValue());
            }
            else if (dataType == "KiB/s")
            {
                chart.Series[0].Points.AddXY(DateTime.Now, perfCounter.NextValue() / 1024);
            }
            else if (dataType == "MiB/s")
            {
                chart.Series[0].Points.AddXY(DateTime.Now, perfCounter.NextValue() / 1048576);
            }
            

            chart.ChartAreas[0].AxisX.ScaleView.Zoom(DateTime.Now.AddSeconds(-15).ToOADate(), DateTime.Now.ToOADate());
            
        }
    }
}
