using System;
using System.Drawing;
using System.Windows.Forms;
using Valve.VR;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using OpenGL;
using Tao.FreeGlut;

namespace ViveOverlayPaster {
    public partial class Form1 : Form {
        int m_nNotifyTime;
        HmdMatrix34_t m_PosMatrix;
        SolidBrush m_TextBrush;
        SolidBrush m_BackgroundBrush;
        int m_nFontSize;

        int m_hWndGL;
        Thread m_GLThread;
        CVRSystem m_HMD;
        ulong m_ulOverlayHandle;

        public Form1() {
            InitializeComponent();
            LoadConfig();
            btnStartVR.Enabled = false;
            if (!Init()) {
                btnStartVR.Enabled = true;
            }
            InitGL();
        }

        public void LoadConfig() {
            m_nNotifyTime = Convert.ToInt32(ConfigurationManager.AppSettings["NotifyTime"]);
            m_PosMatrix = CreateMatrix34(ConfigurationManager.AppSettings["NotificationPositionMatrix"]);

            txtPos.Text = ConfigurationManager.AppSettings["NotificationPositionMatrix"];

            m_TextBrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(ConfigurationManager.AppSettings["TextColor"].ToString(), 16)));
            m_BackgroundBrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(ConfigurationManager.AppSettings["BackgroundColor"].ToString(), 16)));
            m_nFontSize = Convert.ToInt32(ConfigurationManager.AppSettings["FontSize"]);
        }

        public HmdMatrix34_t CreateMatrix34(string strMatrix) {
            string[] matrix = strMatrix.Split(',');
            HmdMatrix34_t matrix34;
            matrix34.m0 = (float)Convert.ToDouble(matrix[0]);
            matrix34.m1 = (float)Convert.ToDouble(matrix[1]);
            matrix34.m2 = (float)Convert.ToDouble(matrix[2]);
            matrix34.m3 = (float)Convert.ToDouble(matrix[3]);
            matrix34.m4 = (float)Convert.ToDouble(matrix[4]);
            matrix34.m5 = (float)Convert.ToDouble(matrix[5]);
            matrix34.m6 = (float)Convert.ToDouble(matrix[6]);
            matrix34.m7 = (float)Convert.ToDouble(matrix[7]);
            matrix34.m8 = (float)Convert.ToDouble(matrix[8]);
            matrix34.m9 = (float)Convert.ToDouble(matrix[9]);
            matrix34.m10 = (float)Convert.ToDouble(matrix[10]);
            matrix34.m11 = (float)Convert.ToDouble(matrix[11]);
            return matrix34;
        }

        public bool Init() {
            EVRInitError peError = EVRInitError.None;
            m_HMD = OpenVR.Init(ref peError, EVRApplicationType.VRApplication_Overlay);
            if (peError != EVRInitError.None) {
                txtLog.AppendText("OpenVR啟動失敗: " + peError + '\n');
                return false;
            }
            if (OpenVR.Overlay != null) {
                EVROverlayError overlayError = OpenVR.Overlay.CreateOverlay("ViveOverlayPaster", "Paster", ref m_ulOverlayHandle);
                if (overlayError == EVROverlayError.None) {
                    OpenVR.Overlay.SetOverlayWidthInMeters(m_ulOverlayHandle, 1f);
                    OpenVR.Overlay.SetOverlayTransformTrackedDeviceRelative(m_ulOverlayHandle, OpenVR.k_unTrackedDeviceIndex_Hmd, ref m_PosMatrix);
                    OpenVR.Overlay.SetOverlayAlpha(m_ulOverlayHandle, 0.9f);
                    txtLog.AppendText("OpenVR啟動成功\n");
                    return true;
                }
            }
            return false;
        }

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern int FindWindow(string strclassName, string strWindowName);


        public void InitGL() {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGBA | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(1, 1);
            Glut.glutCreateWindow("VivePasterGL");
            m_GLThread = new Thread(() => {
                Glut.glutMainLoop();
            });
            m_hWndGL = FindWindow(null, "VivePasterGL");
            ShowWindow(m_hWndGL, 0);
        }


        public void ShutDown() {
            OpenVR.Shutdown();
        }

        private void btnStartVR_Click(object sender, EventArgs e) {
            btnStartVR.Enabled = false;
            if (!Init()) {
                btnStartVR.Enabled = true;
            }
        }

        void ShowNotification(string msg) {
            Bitmap bitmap = new Bitmap(2160, 1440);
            Graphics g = Graphics.FromImage(bitmap);
            Font textFont = new Font("Microsoft JhengHei", m_nFontSize);
            SizeF textSize = g.MeasureString(msg, textFont);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle rect = new Rectangle(0, 0, (int)textSize.Width + 20, (int)textSize.Height + 20);
            g.FillRectangle(m_BackgroundBrush, rect);
            RectangleF rectf = new RectangleF(10, 10, textSize.Width, textSize.Height);
            g.DrawString(msg, textFont, m_TextBrush, rectf);
            g.Flush();

            Texture glTexture = new Texture(bitmap);
            Gl.BindTexture(glTexture);
            Texture_t texture = new Texture_t();
            texture.eType = EGraphicsAPIConvention.API_OpenGL;
            texture.eColorSpace = EColorSpace.Auto;
            texture.handle = (IntPtr)glTexture.TextureID;

            EVROverlayError error = OpenVR.Overlay.ClearOverlayTexture(m_ulOverlayHandle);
            HmdMatrix34_t matrix34 = CreateMatrix34(txtPos.Text);
            OpenVR.Overlay.SetOverlayTransformTrackedDeviceRelative(m_ulOverlayHandle, OpenVR.k_unTrackedDeviceIndex_Hmd, ref matrix34);
            error = OpenVR.Overlay.SetOverlayTexture(m_ulOverlayHandle, ref texture);
            OpenVR.Overlay.ShowOverlay(m_ulOverlayHandle);
            
            //ClearAllTimeout();
            //SetTimeout(() => {
            //    OpenVR.Overlay.HideOverlay(m_ulOverlayHandle);
            //}, m_nNotifyTime);
        }

        #region SetTimeout/ClearTimeout Simulation
        static Dictionary<Guid, Thread> _setTimeoutHandles =
            new Dictionary<Guid, Thread>();
        static Guid SetTimeout(Action cb, int delay) {
            return SetTimeout(cb, delay, null);
        }
        static Guid SetTimeout(Action cb, int delay, Form uiForm) {
            Guid g = Guid.NewGuid();
            Thread t = new Thread(() => {
                Thread.Sleep(delay);
                _setTimeoutHandles.Remove(g);
                if (uiForm != null)
                    uiForm.Invoke(cb);
                else
                    cb();
            });
            _setTimeoutHandles.Add(g, t);
            t.Start();
            return g;
        }
        static void ClearTimeout(Guid g) {
            if (!_setTimeoutHandles.ContainsKey(g))
                return;
            _setTimeoutHandles[g].Abort();
            _setTimeoutHandles.Remove(g);
        }
        static void ClearAllTimeout() {
            foreach (var g in _setTimeoutHandles) {
                g.Value.Abort();
            }
            _setTimeoutHandles.Clear();
        }
        #endregion

        private void btnSend_Click(object sender, EventArgs e) {
            ShowNotification(txtMsg.Text);
            txtMsg.Text = "";
        }

        private void txtMsg_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return) {
                e.Handled = true;
                if(e.Modifiers != Keys.Shift) {
                    ShowNotification(txtMsg.Text);
                    txtMsg.Text = "";
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            ShutDown();
        }
    }
}
