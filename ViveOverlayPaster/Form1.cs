using System;
using System.Drawing;
using System.Windows.Forms;
using Valve.VR;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ViveOverlayPaster {
    public partial class Form1 : Form {
        int m_nNotifyTime;
        HmdMatrix34_t m_PosMatrix;
        SolidBrush m_TextBrush;
        SolidBrush m_BackgroundBrush;
        int m_nFontSize;

        CVRSystem m_HMD;
        ulong m_ulOverlayHandle;

        public Form1() {
            InitializeComponent();
            LoadConfig();
        }

        public void LoadConfig() {
            m_nNotifyTime = Convert.ToInt32(ConfigurationManager.AppSettings["NotifyTime"]);
            string[] matrix = ConfigurationManager.AppSettings["NotificationPositionMatrix"].Split(',');
            m_PosMatrix.m0 = (float)Convert.ToDouble(matrix[0]);
            m_PosMatrix.m1 = (float)Convert.ToDouble(matrix[1]);
            m_PosMatrix.m2 = (float)Convert.ToDouble(matrix[2]);
            m_PosMatrix.m3 = (float)Convert.ToDouble(matrix[3]);
            m_PosMatrix.m4 = (float)Convert.ToDouble(matrix[4]);
            m_PosMatrix.m5 = (float)Convert.ToDouble(matrix[5]);
            m_PosMatrix.m6 = (float)Convert.ToDouble(matrix[6]);
            m_PosMatrix.m7 = (float)Convert.ToDouble(matrix[7]);
            m_PosMatrix.m8 = (float)Convert.ToDouble(matrix[8]);
            m_PosMatrix.m9 = (float)Convert.ToDouble(matrix[9]);
            m_PosMatrix.m10 = (float)Convert.ToDouble(matrix[10]);
            m_PosMatrix.m11 = (float)Convert.ToDouble(matrix[11]);

            m_TextBrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(ConfigurationManager.AppSettings["TextColor"].ToString(), 16)));
            m_BackgroundBrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(ConfigurationManager.AppSettings["BackgroundColor"].ToString(), 16)));
            m_nFontSize = Convert.ToInt32(ConfigurationManager.AppSettings["FontSize"]);
        }
    }
}
