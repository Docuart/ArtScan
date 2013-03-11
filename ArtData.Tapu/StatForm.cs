using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Library.Scan;

namespace ArtData
{
    public partial class StatForm : Form
    {
        public StatForm()
        {
            InitializeComponent();
            TimerTick(null, null);
        }

        private bool _refreshing;
        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                if (_refreshing) return;
                _refreshing = true;

                var dbo = new DbObject();
                var dsCiltDurum = dbo.DataSet(
                    @"
SELECT DECODE(CILT_DURUM, 'OK', 'TAMAMLANDI', 'KNT', 'KONTROL EDİLECEK', 'SCN', 'TEKRAR TARANACAK', 'BEK', 'BEKLEMEDE') AS ADI, COUNT(CILT_DURUM) AS SAYI
  FROM SCAN_CILT 
 GROUP BY CILT_DURUM");

                var dsIndeks =
                    dbo.DataSet(
                        @"
SELECT INDEKS AS ADI, COUNT(INDEKS) AS SAYI
  FROM SCAN_CILT
 WHERE INDEKS IS NOT NULL 
 GROUP BY INDEKS
");

                var dsHataSayi =
                    dbo.DataSet(
                        @"
SELECT INDEKS AS ADI, 
       COUNT(1) AS SAYI 
  FROM SCAN_HATA 
 GROUP BY INDEKS ORDER BY INDEKS
");

                dbo.Dispose();

                if (dsCiltDurum != null)
                {
                    chart1.Series.Clear();
                    chart1.Titles.Clear();
                    chart1.Titles.Add("CİLTLERE GÖRE INDEKSLEME DURUMLARI");
                    for (var i = 0; i < dsCiltDurum.Tables[0].Rows.Count; i++)
                    {
                        var series = chart1.Series.Add(dsCiltDurum.Tables[0].Rows[i]["ADI"] + "-" + dsCiltDurum.Tables[0].Rows[i]["SAYI"]);
                        series.MarkerStep = 1;
                        series.Points.Add(new[] { Convert.ToDouble(dsCiltDurum.Tables[0].Rows[i]["SAYI"].ToString()) });
                    }
                }

                if (dsIndeks != null)
                {
                    chart2.Series.Clear();
                    chart2.Titles.Clear();
                    chart2.Titles.Add("PERSONELE GÖRE INDEKSLEME SAYISI");

                    for (var i = 0; i < dsIndeks.Tables[0].Rows.Count; i++)
                    {
                        var series = chart2.Series.Add(dsIndeks.Tables[0].Rows[i]["ADI"] + "-" + dsIndeks.Tables[0].Rows[i]["SAYI"]);
                        series.MarkerStep = 1;
                        series.Points.Add(new[] { Convert.ToDouble(dsIndeks.Tables[0].Rows[i]["SAYI"].ToString()) });
                    }
                }

                if (dsHataSayi != null)
                {
                    chart3.Series.Clear();
                    chart3.Titles.Clear();
                    chart3.Titles.Add("PERSONELE GÖRE HATA SAYISI");

                    for (var i = 0; i < dsHataSayi.Tables[0].Rows.Count; i++)
                    {
                        var series = chart3.Series.Add(dsHataSayi.Tables[0].Rows[i]["ADI"] + "-" + dsHataSayi.Tables[0].Rows[i]["SAYI"]);
                        series.MarkerStep = 1;
                        series.Points.Add(new[] { Convert.ToDouble(dsHataSayi.Tables[0].Rows[i]["SAYI"].ToString()) });
                    }
                }
            }
            finally
            {
                _refreshing = false;
            }
        }

        private void StatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

    public static class EnumExtensions
    {

        public static List<string> GetFriendlyNames(this Enum enm)
        {
            var result = new List<string>();
            result.AddRange(Enum.GetNames(enm.GetType()).Select(s => s.ToFriendlyName()));
            return result;
        }

        public static string GetFriendlyName(this Enum enm)
        {
            return Enum.GetName(enm.GetType(), enm).ToFriendlyName();
        }

        private static string ToFriendlyName(this string orig)
        {
            return orig.Replace("_", " ");
        }
    }

}
