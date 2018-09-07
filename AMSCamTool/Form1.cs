using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSCamTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        string dirPath = Application.StartupPath + "\\GameData\\Vehicles\\";

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var file in Directory.GetFiles(dirPath, "*.cam", SearchOption.AllDirectories))
            {
                string fileContent = File.ReadAllText(file);
                //Regex r = new Regex(@"^(LocalCam=TVCOCKPIT[\s\S]*)(OrientationOffset.*)");
                Regex r = new Regex(@"(LocalCam=COCKPIT[\s{\w=()\.,-]*)(OrientationOffset=\([^)]*\))");
                Match m = r.Match(fileContent);

                if (m.Success)
                {
                    fileContent = r.Replace(fileContent, m.Groups[1] + "OrientationOffset=(" + textBoxPitch.Text + ", " + textBoxYaw.Text + ", " + textBoxRoll.Text + ")");
                }

                File.WriteAllText(file, fileContent);
            }
        }
    }
}
