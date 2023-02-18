using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catPc
{
    public class AutoClosingMessageBox
    {
        public static void Show()
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show("Cat Hack Attempt Blocked !", "Cat Hack Attempt Blocked !", buttons);
            if (result == DialogResult.OK)
            {
                Application.Restart(); 
                Environment.Exit(0);
            }
        }
    }
}
