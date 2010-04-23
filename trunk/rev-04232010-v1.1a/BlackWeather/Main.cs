using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using BlackRain.Common.Objects;
using BlackRain.Common;

namespace BlackWeather
{
    public partial class Main : Form
    {
        private readonly List<Process> _processes = new List<Process>();
        readonly Process[] _wowProc = Process.GetProcessesByName("Wow");
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, System.EventArgs e)
        {

            foreach (Process proc in _wowProc)
            {
                cmb_ProcessSelection.Items.Add(proc.MainWindowTitle + " | ID: " + proc.Id);
                Logging.Write(this, "Found WoW process with ID: " + proc.Id);
            }
        }

        private void btn_SelectProcess_Click(object sender, System.EventArgs e)
        {
            if (cmb_ProcessSelection.SelectedItem != null)
            {
                ObjectManager.Initialize(_wowProc[cmb_ProcessSelection.SelectedIndex].Id);

                if (ObjectManager.Initialized)
                {
                    lbl_AttachedTo.Text = "Attached to World of Warcraft with ID " +
                                          _wowProc[cmb_ProcessSelection.SelectedIndex].Id;

                    Logging.Write(this, "Local GUID: " + ObjectManager.LocalGUID);
                }
            }
        }

        private void btn_Refresh_Click(object sender, System.EventArgs e)
        {
            ObjectManager.Pulse();

            /*
            foreach (WowObject obj in ObjectManager.Objects)
            {
                lst_Objects.Items.Add(string.Format("GUID: {0} | Entry: {1} | Type: {2}", obj.GUID, obj.Entry, obj.Type));
            }
            */

            lst_Objects.Items.Add("Me Type: " + ObjectManager.Me.Type);
            lst_Objects.Items.Add(string.Format("Me BaseAddress: 0x{0:X}", ObjectManager.Me.BaseAddress));
            lst_Objects.Items.Add("Me GUID: " + ObjectManager.Me.GUID);
            lst_Objects.Items.Add(ObjectManager.Me.ToString());
            lst_Objects.Items.Add(ObjectManager.Me.Health);

            var location = ObjectManager.Me.X + "," + ObjectManager.Me.Y + "," + ObjectManager.Me.Z;
            Logging.Write(
                string.Format("Type: {0} <br/> BaseAddress: {1} <br/> GUID: {2} <br/> Health: {3} <br/> Location: {4}", ObjectManager.Me.Type, ObjectManager.Me.BaseAddress, ObjectManager.Me.GUID, ObjectManager.Me.Health, location));

            // Object X,Y,Z
            lst_Objects.Items.Add(string.Format("X: {0}, Y: {1}, Z: {2}", ObjectManager.Me.X, ObjectManager.Me.Y, ObjectManager.Me.Z) );
        }
    }
}
