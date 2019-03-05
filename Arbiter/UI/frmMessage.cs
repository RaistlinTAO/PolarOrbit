#region À la vie, à la mort

// ###########################################
// 
// FILE:                      frmMessage.cs 
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
using System.Windows.Forms;

#endregion

namespace Arbiter.UI
{
    public partial class frmMessage : Form
    {
        private readonly string _strMessage;

        public frmMessage(string Message)
        {
            InitializeComponent();
            _strMessage = Message;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            lblInfo.Text = _strMessage;
        }
    }
}