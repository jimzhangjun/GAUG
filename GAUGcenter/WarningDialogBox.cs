//===============================================================================================================
//  Project:    GAUG Center
//  Module:     WarningDialogBox.cs                                                                         
//  Author:     Jim Zhang
//  Date:       16/12/2019
//  
//  Details:    Display supplied text in a dialog box 
//                  
//  
//===============================================================================================================

using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace GAUGcenter
{
    public partial class WarningDialogBox : Form
    {
        //---------------------------------------------------------------------------------------------------------
        // CLASS VARIABLES
        //---------------------------------------------------------------------------------------------------------
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);
        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        private static extern int GetMenuItemCount(IntPtr hmenu);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);
        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        private static extern int DrawMenuBar(IntPtr hwnd);

        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;

        public WarningDialogBox()
        {
            InitializeComponent();
            DisableCloseButtom();
        }

        public WarningDialogBox(string Warning)
        {
            InitializeComponent();
            DisableCloseButtom();
            Warninglabel.Text = Warning;
        }

        private void DisableCloseButtom()
        {
            IntPtr hmenu = GetSystemMenu(this.Handle, 0);
            int cnt = GetMenuItemCount(hmenu);

            // remove 'close' action
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            // remove extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);

            DrawMenuBar(this.Handle);
        }

        //Prevent form closure from control box
        private void WarningDialogBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            CloseReason aCloseReason = e.CloseReason;
            //if (aCloseReason == CloseReason.UserClosing) e.Cancel = true;
        }

    }
}