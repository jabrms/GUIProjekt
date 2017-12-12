using System;
using System.Diagnostics;




namespace GUI_Geruest
{
    public class LineChart
    {
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private PerformanceCounter perfCounter;
        private PerformanceCounter secPerfCounter;
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
            else if (dataType == "GiB")
            {
                chart.Titles.Add(title + " [GiB]");
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";
            }
            chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false ;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            
        }


        public LineChart(System.Windows.Forms.DataVisualization.Charting.Chart chart_in, PerformanceCounter perfCounter_in, PerformanceCounter secPerfCounter_in, String dataType_in, String title)
        {
            chart = chart_in;
            perfCounter = perfCounter_in;
            secPerfCounter = secPerfCounter_in;
            dataType = dataType_in;

            chart.Series.Add("Schreiben");
            chart.Series[1].ChartArea = "chartArea";
            chart.Series[1].BorderWidth = 1;
            chart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            chart.Series[0].IsVisibleInLegend = true;

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
            else if (dataType == "GiB")
            {
                chart.Titles.Add(title + " [GiB]");
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "{0.0}";
            }
            chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;


        }


        public void changePerfCounter(PerformanceCounter newPerfCounter)
        {
            perfCounter = newPerfCounter;
        }

        public void changePerfCounter(PerformanceCounter newPerfCounterI, PerformanceCounter newPerfCounterII)
        {
            perfCounter = newPerfCounterI;
            secPerfCounter = newPerfCounterII;
        }

        public void refresh()
        {
            try
            {
                if (dataType == "Percentage")
                {
                    chart.Series[0].Points.AddXY(DateTime.Now, perfCounter.NextValue());
                    if (secPerfCounter != null)
                    {
                        chart.Series[1].Points.AddXY(DateTime.Now, secPerfCounter.NextValue());
                    }
                }
                else if (dataType == "KiB/s")
                {
                    chart.Series[0].Points.AddXY(DateTime.Now, perfCounter.NextValue() / 1024);
                    if (secPerfCounter != null)
                    {
                        chart.Series[1].Points.AddXY(DateTime.Now, secPerfCounter.NextValue() / 1024);
                    }
                }
                else if (dataType == "MiB/s")
                {
                    chart.Series[0].Points.AddXY(DateTime.Now, perfCounter.NextValue() / 1048576);
                    if (secPerfCounter != null)
                    {
                        chart.Series[1].Points.AddXY(DateTime.Now, secPerfCounter.NextValue() / 1048576);
                    }
                }
                else if (dataType == "GiB")
                {
                    chart.Series[0].Points.AddXY(DateTime.Now, perfCounter.NextValue() / 1073741824);
                    if (secPerfCounter != null)
                    {
                        chart.Series[1].Points.AddXY(DateTime.Now, secPerfCounter.NextValue() / 1073741824);
                    }
                }
                chart.ChartAreas[0].AxisX.ScaleView.Zoom(DateTime.Now.AddSeconds(-15).ToOADate(), DateTime.Now.ToOADate());
                }
            catch {}
        }
    }
}
