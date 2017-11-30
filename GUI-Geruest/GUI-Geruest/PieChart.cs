using System;
using System.Diagnostics;
using System.IO;

namespace GUI_Geruest
{
    public class PieChart
    {
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        String drive;

        public PieChart(System.Windows.Forms.DataVisualization.Charting.Chart chart_in, String drive_in)
        {
            chart = chart_in;
            drive = drive_in;

            DriveInfo driveInfo = new DriveInfo(drive);

            chart.Titles.Add("Festplattenbelegung: " + drive);
            chart.Series[0].Points.Add(driveInfo.TotalFreeSpace);
            chart.Series[0].Points[0].Label = driveInfo.TotalFreeSpace / 1073741824 + " GiB frei";
            chart.Series[0].Points.Add(driveInfo.TotalSize - driveInfo.TotalFreeSpace);
            chart.Series[0].Points[1].Label = (driveInfo.TotalSize - driveInfo.TotalFreeSpace) / 1073741824 + " GiB belegt";
        }
    }
}
