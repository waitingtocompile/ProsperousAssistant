using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	public class ScrollsafeCheckedListBox : CheckedListBox
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWn, int msg, IntPtr wp, IntPtr lp);
        const int WM_MOUSEWHEEL = 0x020A;


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
                //directly send the message to parent without processing it
                //according to https://stackoverflow.com/a/19618100
                SendMessage(this.Parent.Handle, m.Msg, m.WParam, m.LParam);
                m.Result = IntPtr.Zero;
            }
            else base.WndProc(ref m);
        }
    }
}
