using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MDIForms
{
    public partial class FormChild : Form
    {
        public FormChild()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            FormMain formMain = Program.ServiceProvider.GetService<FormMain>();
            FormGrandchild formGrandchild = Program.ServiceProvider.GetService<FormGrandchild>();

            formMain.ShowFormInMdiContainer(formGrandchild);

            //formGrandchild.MdiParent = formMain;
            //formGrandchild.Show();
            //formGrandchild.Size = new Size(formMain.MdiClient.Width - 4, formMain.MdiClient.Height - 4);
            //this.Close();   
            //formGrandchild.Location = new Point(0, 0);
        }

        private void FormChild_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide(); 
                e.Cancel = true;
            }
        }
    }
}
