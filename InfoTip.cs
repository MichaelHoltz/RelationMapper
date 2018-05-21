using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelationMap.Models;
namespace RelationMap
{
    public partial class InfoTip : UserControl
    {
        public InfoTip()
        {
            InitializeComponent();
            ClearData();

        }
        public void ClearData()
        {
            //label1.Text = "VM Name:";
            //data1.Text = "";
            label2.Text = "Computer Name:";
            data2.Text = "";
            label3.Text = "IP Address:";
            data3.Text = "";
            label4.Text = "Description:";
            data4.Text = "";
            label5.Text = "OS:";
            data5.Text = "";
            label6.Text = "System:";
            data6.Text = "";
            label7.Text = "Template:";
            data7.Text = "";
            label8.Text = "State:";
            data8.Text = "";

        }
        public void SetData(object userData)
        {
            if (userData == null)
            {

            }
            //if (userData is Server)
            //{

            //    Server o = (userData as Server);
            //    //label1.Text = "VM Name:";
            //    //data1.Text = o.vm_name;
            //    label2.Text = "Computer Name:";
            //    data2.Text = o.computer_name;
            //    label3.Text = "IP Address:";
            //    data3.Text = o.ip_address;
            //    label4.Text = "Description:";
            //    data4.Text = o.description;
            //    label5.Text = "OS:";
            //    data5.Text = o.os;
            //    label6.Text = "System:";
            //    data6.Text = o.system;
            //    label7.Text = "Template:";
            //    data7.Text = o.vm_template;
            //    label8.Text = "State:";
            //    data8.Text = o.state.ToString();
            //}
            //if (userData is Gateway)
            //{
            //    Gateway o = (userData as Gateway);
            //    //label1.Text = "VM Name:";
            //    //data1.Text = o.vm_name;
            //    label2.Text = "Computer Name:";
            //    data2.Text = o.computer_name;
            //    label3.Text = "IP Address:";
            //    data3.Text = o.ip_address;
            //    label4.Text = "Description:";
            //    data4.Text = o.description;
            //    label5.Text = "OS:";
            //    data5.Text = o.os;
            //    //label6.Text = "System:";
            //    //data6.Text = o.system;
            //    label6.Text = "Template:";
            //    data6.Text = o.vm_template;
            //    label7.Text = "State:";
            //    data7.Text = o.state.ToString();
            //}
            //if (userData is Loader)
            //{
            //    Loader o = (userData as Loader);
            //    //label1.Text = "VM Name:";
            //    //data1.Text = o.vm_name;
            //    label2.Text = "Computer Name:";
            //    data2.Text = o.computer_name;
            //    label3.Text = "IP Address:";
            //    data3.Text = o.ip_address;
            //    label4.Text = "Description:";
            //    data4.Text = o.description;
            //    label5.Text = "OS:";
            //    data5.Text = o.os;
            //    label6.Text = "Type:";
            //    data6.Text = o.type;
            //    label7.Text = "Template:";
            //    data7.Text = o.vm_template;
            //    label8.Text = "State:";
            //    data8.Text = o.state.ToString();

            //}
            //if (userData is Template)
            //{
            //    Template o = (userData as Template);
            //    label2.Text = "Template:";
            //    data2.Text = o.vm_template;
            //    label3.Text = "Description:";
            //    data3.Text = o.description;
            //    label4.Text = "Source:";
            //    data4.Text = o.source;
            //    label5.Text = "Path:";
            //    data5.Text = o.path;
            //}
        }

    }
}
