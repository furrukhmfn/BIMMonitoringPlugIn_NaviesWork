using System.Windows.Forms;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Api.Timeliner;
using Nw = Autodesk.Navisworks.Api;
using Tl = Autodesk.Navisworks.Api.Timeliner;

namespace Bim_Inventory_Monitor_Plugin
{
    [Plugin("Bim Inventory Monitor Plugin", "CONN", DisplayName = "Bim Inventory Monitor Plugin")]
    public class Class1 : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            // Should be Deleted After the working of Plug i
            MessageBox.Show("Plug in Is Working Fine.", "Working Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form1 f1 = new Form1();
            f1.ShowDialog();
            return 0;
        }

    }
}
