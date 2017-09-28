using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTcpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Action action = new Action(Bind);
            action.BeginInvoke(new AsyncCallback((iar) =>
            {
                Action actionEnd = (Action)iar.AsyncState;
                actionEnd.EndInvoke(iar);
            }), action);
        }

        private void Bind()
        {
            StudentServiceClient client = new StudentServiceClient();
            IEnumerable<NetTcpServer.StudentInfo> x = client.GetStudentInfo(Int32.Parse(textBox1.Text));
            dataGridView1.Invoke((Action)(() => { dataGridView1.DataSource = x; }));
        }
    }
}
