using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelationMap.Models;

namespace RelationMap.HelperForms
{
    public partial class frmStudioGroupMaker : Form
    {
        Universe u = null;
        //public frmStudioCollectionMaker()
        //{
        //    InitializeComponent();
        //}
        public frmStudioGroupMaker(Universe univ)
        {
            InitializeComponent();
            u = univ; // Initialize Universe

        }

        private void frmStudioCollectionMaker_Load(object sender, EventArgs e)
        {
            //List all Production companies in the universe
            lbProductionCompanies.Items.Clear();
            //lbProductionCompanies.Items.AddRange(u.ProductionCompanies.ToArray()); // Should add u.GetAllProductionCompanies

            lbStudioGroups.Items.Clear();
            lbStudioGroups.Items.AddRange(u.StudioGroups.ToArray());
        }

        private async void lbProductionCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbProductionCompanies.SelectedIndex >= 0)
            {
                ProductionCompany pc = u.GetProductionCompany(lbProductionCompanies.SelectedItem.ToString());
                Image img = await pc.GetLogo(TmdbWrapper.Utilities.LogoSize.w45);
                pbMasterLogo.BackgroundImage = img;

            }
        }
    }
}
