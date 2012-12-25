using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace CapsLockIndicator
{
    public partial class Form1 : Form
    {
        private const int Offset = 120;
        private readonly Action _hideAction, _showAction;

        public Form1()
        {
            InitializeComponent();

            _hideAction = new Action(() => Visible = false);
            _showAction = new Action(() =>
                                       {
                                           SetPosition();
                                           Visible = true;
                                       });

            var checkCapsTimer = new Timer(delegate { CheckCaps(); });
            checkCapsTimer.Change(0, 10);
        }

        private void SetPosition()
        {
            // move to lower right corner 
            var screenSize = SystemInformation.VirtualScreen;
            Location = new Point(screenSize.Width - Offset, screenSize.Height - Offset);
        }

        private void CheckCaps()
        {
            if (IsKeyLocked(Keys.CapsLock) && Visible)
                return;

            var action = !IsKeyLocked(Keys.CapsLock)
                             ? _hideAction
                             : _showAction;

            if (InvokeRequired)
                Invoke(action);
            else 
                action.Invoke();
        }
    }
}