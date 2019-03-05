#region À la vie, à la mort

// ###########################################
// 
// FILE:                      frmMain.cs 
// PROJECT:              Arbiter
// SOLUTION:           PolarOrbit
// 
// CREATED BY RAISTLIN TAO
// DEMONVK@GMAIL.COM
// 
// FILE CREATION: 5:44 PM  05/12/2015
// 
// ###########################################

#endregion

#region Embrace the code

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Timers;
using System.Windows.Forms;
using PolarOrbit;
using Timer = System.Timers.Timer;

#endregion

namespace Arbiter.UI
{
    public partial class frmMain : Form
    {
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        private readonly Timer tm = new Timer(24 * 3600 * 1000);
        private BackgroundWorker _BGWorker;
        private Cybernetics _service;

        private ServiceHost PolarOrbit;

        public frmMain()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void WriteLog(string Log)
        {
            lstLog.Items.Add(DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" +
                             DateTime.Now.Second.ToString("00") + "   " + Log);
            lstLog.SelectedIndex += 1;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            if (PolarOrbit != null)
            {
                PolarOrbit.Abort();
                PolarOrbit.Close();
                PolarOrbit = null;
            }

            if (_BGWorker != null)
            {
                //_BGWorker.CancelAsync();
                _BGWorker.Dispose();
                _BGWorker = null;
            }


            //HQServer.StopServer();
            Application.Exit();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            WriteLog("Try to start Jupiter Service...");
            tm.Enabled = true;
            tm.Elapsed += tm_Elapsed;
            _BGWorker = new BackgroundWorker();
            _BGWorker.RunWorkerCompleted += _RunWorkerCompleted;
            _BGWorker.DoWork += _DoWork;

            if (!_BGWorker.IsBusy) _BGWorker.RunWorkerAsync();
        }

        private void tm_Elapsed(object sender, ElapsedEventArgs e)
        {
            _service.CleanMessage();
            tm.Enabled = true;
        }

        private void _DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                PolarOrbit = new ServiceHost(_service);
                //IStarGate = new ServiceHost(typeof(D.E.M.O.N.Catalyst.GalaxyService.Observer));
                if (PolarOrbit.State != CommunicationState.Opened) PolarOrbit.Open();

                e.Result = "K";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void _RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                if (e.Result.ToString() == "K")
                {
                    WriteLog("Jupiter TCP Binding Service started successfully");
                    cmdStart.Enabled = false;
                }
                else
                {
                    WriteLog("Service Failed! Error:" + e.Result);
                }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            WriteLog("Program initialised");
            WriteLog("Try to check program files");
            WriteLog(File.Exists(Application.StartupPath + "\\PolarOrbit.dll")
                ? "Finished! All parts avaliable!"
                : "Warning! Some parts missed!");

            _service = new Cybernetics();
            _service.OnTransimitMessage += TransimitMessage;
            //_service.OnAccountRequested += RequestAccount;
        }

        private void TransimitMessage(MessageInfo iMessageInfo)
        {
            lstLog.Invoke(
                (MethodInvoker)
                delegate
                {
                    WriteLog(iMessageInfo.User + " to " + iMessageInfo.ToUser + "Transmitted:");
                    WriteLog(iMessageInfo.Message);
                });
            lblMessageNum.Invoke(
                (MethodInvoker)
                delegate { lblMessageNum.Text = _service.GetMessageNumber().ToString(CultureInfo.InvariantCulture); }
            );
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void cmdMini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
        }

        private void lstLog_DoubleClick(object sender, EventArgs e)
        {
            var xOpen = new frmMessage(lstLog.Items[lstLog.SelectedIndex].ToString());
            xOpen.ShowDialog();
        }
    }
}