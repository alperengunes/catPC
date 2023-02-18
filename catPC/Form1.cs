using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.DataFormats;

namespace catPc
{
    public partial class Form1 : Form
    {
        public static string keyboardStatus { get; set; }
        public static string mouseStatus { get; set; }
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            keyboardStatus = "Enabled";
            mouseStatus = "Enabled";
            label1.Parent  = pictureBox1; 
            label1.BackColor = Color.Transparent;
            label4.ForeColor = Color.Green;
            label5.ForeColor = Color.Green;
            UnhookWindowsHookEx(_hookID);
            _hookID = SetHook(_proc);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (keyboardStatus == "Enabled")
            {
                label4.Text  = "Disabled";
                label4.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                button1.Text = "Enabled";
                label5.Text = "Disabled";
            }
            else if(keyboardStatus == "Disabled")
            {

                label4.Text     = "Enabled";
                label4.ForeColor= Color.Green;
                button1.Text    = "Disabled";
                label5.Text     = "Enabled";
                label5.ForeColor= Color.Green;
            }
            keyboardStatus = label4.Text;
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (keyboardStatus == "Enabled")
                {
                    showWindow.modeOn();
                    if(vkCode.ToString() == "32")
                    {
                        MessageBox.Show("Cat Hack Attempt Blocked !");
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}