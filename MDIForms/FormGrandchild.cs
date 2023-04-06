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

namespace MDIForms
{
    public partial class FormGrandchild : Form
    {
        public FormGrandchild()
        {
            InitializeComponent();
        }

        private void FormGrandchild_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMain formMain = Program.ServiceProvider.GetService<FormMain>();
            FormChild formChild = Program.ServiceProvider.GetService<FormChild>();

            formMain.ShowFormInMdiContainer(formChild);

            //formChild.MdiParent = formMain;
            //formChild.Show();
            //formChild.Size = new Size(formMain.MdiClient.Width - 4, formMain.MdiClient.Height - 4);
            //this.Close();
            //formChild.Location = new Point(0, 0);

        }
    }
}
