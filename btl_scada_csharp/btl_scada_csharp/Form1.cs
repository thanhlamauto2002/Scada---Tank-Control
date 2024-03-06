using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SymbolFactoryDotNet;
using S7.Net;
using S7.Net.Types;
using System.Data.SqlClient;

namespace btl_scada_csharp
{
    public partial class Form1 : Form
    {
      // Hàm truyền dữ liệu về PLC
      private void writebit(string e, object f)
        {
            myplc.Write(e, f);
            myplc.Write(e, 0);
        }
        //Hàm đọc giá trị bit từ PLC về C#
        private int docbit (string b)
        {

            object _docbit = myplc.Read(b);
            int bit = Convert.ToInt16(_docbit);
            return bit;

        }
        // Hàm chuyển giá trị nguyên 32 bit dạng big-endian về số thực
        private float cvt (int db, int staddress, int cnt)
        {
            byte[] data = myplc.ReadBytes(DataType.DataBlock, db, staddress, cnt);
            float value = BitConverter.ToSingle(data.Reverse().ToArray(), 0);
            return value;
        }
        // Hàm đổi trạng thái cho các van, ống 
        private void doi_state(StandardControl c, int d)
        {
            if (d ==1 )
            {
                c.DiscreteValue2 = true;
                c.DiscreteValue1 = false;
            }
            else
            {
                c.DiscreteValue2 = false;
                c.DiscreteValue1 = true;
            }
        }
    
        Plc myplc = new Plc(CpuType.S71200, "192.168.0.10", 0, 1);

        public Form1()
        {
            InitializeComponent();
        }

        private void standardControl1_Load(object sender, EventArgs e)
        {

        }

        private void standardControl2_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            myplc.Open();
            timer1.Enabled = true;
            if (myplc.IsConnected)
            MessageBox.Show("Connected");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // room1
            float sp1 = cvt(3, 18, 4);
            float lv1 = cvt(3,2,4);
            float pw1 = cvt(3,34,4);
            float fl1 = cvt(3,50,4);
            label5.Text = sp1.ToString();
            label6.Text = lv1.ToString();
            label7.Text = pw1.ToString();
            label8.Text = fl1.ToString();
            float kp1 = cvt(3,66,4);
            float ki1 = cvt(3,70,4);
            float kd1 = cvt(3,74,4);
            label11.Text = kp1.ToString();
            label10.Text = ki1.ToString();
            label9.Text = kd1.ToString();
            //room2
            float sp2 = cvt(3, 22, 4);
            float lv2 = cvt(3, 6, 4);
            float pw2 = cvt(3, 38, 4);
            float fl2 = cvt(3, 54, 4);
            label38.Text = sp2.ToString();
            label33.Text = lv2.ToString();
            label25.Text = pw2.ToString();
            label26.Text = fl2.ToString();
            float kp2 = cvt(3,78,4);
            float ki2 = cvt(3,82,4);
            float kd2 = cvt(3,86,4);
            label28.Text = kp2.ToString();
            label30.Text = ki2.ToString();
            label32.Text = kd2.ToString();
            //room3
            float sp3 = cvt(3, 26, 4);
            float lv3 = cvt(3, 10, 4);
            float pw3 = cvt(3, 42, 4);
            float fl3 = cvt(3, 58, 4);
            label56.Text = sp3.ToString();
            label51.Text = lv3.ToString();
            label43.Text = pw3.ToString();
            label44.Text = fl3.ToString();
            float kp3 = cvt(3,90,4);
            float ki3 = cvt(3,94,4);
            float kd3 = cvt(3,98,4);
            label46.Text = kp3.ToString();
            label48.Text = ki3.ToString();
            label50.Text = kd3.ToString();
            //room4
            float sp4 = cvt(3, 30, 4);
            float lv4 = cvt(3, 14, 4);
            float pw4 = cvt(3, 46, 4);
            float fl4 = cvt(3, 62, 4);
            label74.Text = sp4.ToString();
            label69.Text = lv4.ToString();
            label61.Text = pw4.ToString();
            label62.Text = fl4.ToString();
            float kp4 = cvt(3,102,4);
            float ki4 = cvt(3,106,4);
            float kd4 = cvt(3,110,4);
            label64.Text = kp4.ToString();
            label66.Text = ki4.ToString();
            label68.Text = kd4.ToString();
            // các van cấp, ống cấp
            //room1
            int v1on = docbit("M4.6");
                doi_state(sd10, v1on);
                doi_state(sd11, v1on);
                doi_state(sdvon1, v1on);
            if (v1on == 1)
            {
                dencap1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else dencap1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            //room2
            int v2on = docbit("M4.7");
            doi_state(sd20, v2on);
            doi_state(sd21, v2on);
            doi_state(sdvon2, v2on);
            if (v2on == 1)
            {
                dencap2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else dencap2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            //room3
            int v3on = docbit("M5.0");
            doi_state(sd30, v3on);
            doi_state(sd31, v3on);
            doi_state(sdvon3, v3on);
            if (v3on == 1)
            {
                dencap3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else dencap3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            // room4
            int v4on = docbit("M5.1");
            doi_state(sd40, v4on);
            doi_state(sd41, v4on);
            doi_state(sdvon4, v4on);
            if (v4on == 1)
            {
                dencap4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else dencap4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            //label75.Text = v1on.ToString();
            // các van, ống xả
            int v1xa = docbit("M6.1");
            doi_state(sd12, v1xa);
            doi_state(sd13, v1xa);
            doi_state(sdvxa1, v1xa);
            if (v1xa == 1)
            {
                denxa1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else denxa1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            //room2
            int v2xa = docbit("M6.2");
            doi_state(sd22, v2xa);
            doi_state(sd23, v2xa);
            doi_state(sdvxa2, v2xa);
            if (v2xa == 1)
            {
                denxa2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else denxa2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            //room3
            int v3xa = docbit("M6.3");
            doi_state(sd32, v3xa);
            doi_state(sd33, v3xa);
            doi_state(sdvxa3, v3xa);
            if (v3xa == 1)
            {
                denxa3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else denxa3.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            // room4
            int v4xa = docbit("M6.4");
            doi_state(sd42, v4xa);
            doi_state(sd43, v4xa);
            doi_state(sdvxa4, v4xa);
            if (v4xa == 1)
            {
                denxa4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else denxa4.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
         // bảng điều khiển chung
            //start
         int start = docbit("M0.5");
            if (start == 1)
            {
                denstart.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            }
            else denstart.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            // stop
            int stop = docbit("M0.4");
            
            //auto
            int auto = docbit("M2.0");
            if (auto == 1)
            {
                denauto.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
                label20.Text = "Auto";
            }
            else
            {
                denauto.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;

            }
            //manu
            int manu = docbit("M2.1");
            if (manu == 1)
            {
                denmanu.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
                label20.Text = "Manual";

            }
            else
            {
                denmanu.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Transparent;
            }
            
            
           

        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form2 do_thi = new Form2();
            do_thi.Show();
        }
        // truyền dữ liệu từ các nút nhấn về PLC
            //bang dieu khien chung
        private void btnstart_Click(object sender, EventArgs e)
        {
            writebit("M0.3", 1);
        }


        private void btnauto_Click(object sender, EventArgs e)
        {
                writebit("M0.6", 1);
        }

        private void btnmanu_Click(object sender, EventArgs e)
        {
                writebit("M0.7", 1);
        }
            // nut nhan room1
        private void btnoffcap1_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M8.1", 1);
        }

        private void btnoncap1_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.5", 1);
        }

        private void btnonxa1_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M6.5", 1);
        }

        private void btnoffxa1_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.1", 1);
        }
        // nut nhan room2

        private void oncap2_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.6", 1);
        }

        private void offcap2_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M8.2", 1);
        }

        private void onxa2_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M6.6", 1);
        }

        private void offxa2_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.2", 1);
        }
        // nut nhan room3

        private void oncap3_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.7", 1);
        }

        private void offcap3_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M8.3", 1);
        }

        private void onxa3_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M6.7", 1);
        }

        private void offxa3_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.3", 1);
        }
        // nut nhan room4

        private void oncap4_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M8.0", 1);
        }

        private void offcap4_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M8.4", 1);
        }

        private void onxa4_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.0", 1);
        }

        private void offxa4_Click(object sender, EventArgs e)
        {
            if (docbit("M2.1") == 1)
                writebit("M7.4", 1);
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            Form2 form_rp = new Form2();
            form_rp.ShowDialog();
        }
           // nhan nut stop
        private void btnstop_Click(object sender, EventArgs e)
        {
            writebit("M14.0", 1);

        }
    }
}
