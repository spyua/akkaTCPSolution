using AkkaSysBase;
using System.Windows.Forms;

namespace AkkaTCP
{
    public partial class Form1 : Form
    {

        private ISysAkkaManager _akkaManager;

        public Form1(ISysAkkaManager akkaManager)
        {
            InitializeComponent();

        }

    }
}
