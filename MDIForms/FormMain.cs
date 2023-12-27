using Microsoft.Extensions.DependencyInjection;

namespace MDIForms
{
    public partial class FormMain : Form
    {
        private MdiClient _mdiClient;
        internal MdiClient MdiClient
        {
            get
            {
                foreach (Control ctl in this.Controls)
                {
                    if (ctl is MdiClient)
                    {
                        _mdiClient = ctl as MdiClient;
                        break;
                    };
                    _mdiClient = null;
                }
                return _mdiClient;
            }
        }
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMain formMain = Program.ServiceProvider.GetService<FormMain>();
            FormChild formChild = Program.ServiceProvider.GetService<FormChild>();

            formMain.ShowFormInMdiContainer(formChild);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            //check if form has a child form
            if (this.MdiChildren.Length > 0)
            {
                //loop through all child forms
                foreach (Form childForm in this.MdiChildren)
                {

                    //resize child form to match parent form                    
                    childForm.Size = new Size(MdiClient.Width - 4, MdiClient.Height - 4);
                }
            }
        }

        public void ShowFormInMdiContainer(Form form)
        {
            var serviceProvider = Program.Services;
            foreach (Form mdiChild in this.MdiChildren)
            {
                if (mdiChild.GetType() == form.GetType())
                {
                    Type serviceType = mdiChild.GetType();
                    var serviceDescriptor = Program.Services.FirstOrDefault(descriptor => descriptor.ServiceType == serviceType);
                    if (serviceDescriptor != null)
                    {
                        var serviceLifetime = serviceDescriptor.Lifetime;
                        // Se o formulário é singleton ou scoped, oculta o formulário em vez de fechá-lo
                        if (serviceLifetime == ServiceLifetime.Singleton || serviceLifetime == ServiceLifetime.Scoped)
                        {
                            mdiChild.Hide();
                        }
                        else // Se o formulário é transient, fecha o formulário
                        {
                            mdiChild.Close();
                        }
                    }
                }
            }
            
            //verify if mdiChildren contain a instance type of form LINQ query
            var formChild = this.MdiChildren.FirstOrDefault(f => f.GetType() == form.GetType());
            
            if (formChild != null)
            {
                // Code to execute if the form is in this.MdiChildren
                form = formChild;
                form.MdiParent = this;
                form.BringToFront();
                form.Show();
                form.Size = new Size(this.MdiClient.Width - 4, this.MdiClient.Height - 4);
                form.Location = new Point(0, 0);
            }
            else
            {
                // Code to execute if the form is not in this.MdiChildren
                form.MdiParent = this;
                form.Show();
                form.Size = new Size(this.MdiClient.Width - 4, this.MdiClient.Height - 4);
                form.Location = new Point(0, 0);            
            }
        }
    }
}
