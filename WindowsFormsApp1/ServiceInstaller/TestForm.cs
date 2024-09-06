using System;
using System.Windows.Forms;

namespace ServiceInstaller
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //var nDate = DateTime.Now.Date;
            //var startTime = nDate.AddDays(-nDate.Day + 1);
            //var endTime = nDate;
            //var sqlParams = new List<SqlParameter>();
            //sqlParams.Add(new SqlParameter("@startTime", startTime));
            //sqlParams.Add(new SqlParameter("@endTime", endTime));
            //var totalMinute = Convert.ToDecimal((endTime - startTime).TotalMinutes);
            //using (var DbConext = new VisitAppEntities())
            //{
            //    var test = DbConext.Database.SqlQuery<KaiGongInfo>("P_GetKaiGongCount @startTime,@endTime", sqlParams.ToArray()).ToList();
            //    var groupList = (from a in test
            //                     group a by a.设备号
            //                     into b
            //                     orderby b.Key
            //                     select new T_DeviceInfo { DeviceNum = b.Key, OperationRate = b.Sum(x => x.开工分钟数) / totalMinute, YearMonth = nDate,WorkRoom="合成二期" }
            //                     ).ToList();
            //    var nowMonthData = DbConext.T_DeviceInfo.Where(x => x.YearMonth == nDate);
            //    if (nowMonthData.Any())
            //    {
            //        foreach (var item in nowMonthData)
            //        {
            //            item.OperationRate = groupList.First(x => x.DeviceNum == item.DeviceNum).OperationRate;
            //        }
            //    }
            //    else
            //    {
            //        DbConext.T_DeviceInfo.AddRange(groupList.OrderBy(x=>x.DeviceNum));
            //    }
            //    DbConext.SaveChanges();
            //}
        }
    }
}
