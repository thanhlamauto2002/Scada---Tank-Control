using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace btl_scada_csharp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

           
        }

        private void btn1_Click(object sender, EventArgs e)
            
        {
           
            
             
               
            this.reportViewer1.RefreshReport();
        }
    }
}
