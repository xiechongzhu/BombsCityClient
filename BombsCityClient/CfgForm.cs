using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombsCityClient
{
    public partial class CfgForm : Form
    {
        public CfgForm()
        {
            InitializeComponent();
            GlobalConfig.LoadConfig();
            SetFlowCamList(GlobalConfig.GetInstance().FlowCamCfgList);
            textBoxClusterTag.Text = GlobalConfig.GetInstance().ClusterTag.ToString();
            textBoxResourceCode.Text = GlobalConfig.GetInstance().ResourceCode;
            textBoxRtPeopleUrl.Text = GlobalConfig.GetInstance().RtPeopleUrl;
            textBoxParkingUrl.Text = GlobalConfig.GetInstance().ParkingUrl;
        }

        private void btnAddFlowCam_Click(object sender, EventArgs e)
        {
            CamAddForm camAddForm = new CamAddForm();
            if(camAddForm.ShowDialog() == DialogResult.OK)
            {
                AddFlowCam(camAddForm.GetFlowCamCfg());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddFlowListViewItem(FlowCamCfg cfg)
        {
            ListViewItem item = new ListViewItem();
            item.Text = cfg.IpAddress;
            item.SubItems.Add(cfg.Port.ToString());
            item.SubItems.Add(cfg.UserName);
            item.SubItems.Add(cfg.Password);
            item.SubItems.Add(cfg.Description);
            listViewFlowCam.Items.Add(item);
        }

        private void AddFlowCam(FlowCamCfg flowCamCfg)
        {
            foreach(FlowCamCfg cfg in GetFlowCamCfgs())
            {
                if(cfg.IpAddress.Equals(flowCamCfg.IpAddress) && cfg.Port == flowCamCfg.Port)
                {
                    MessageBox.Show("设备地址和端口不能重复!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            AddFlowListViewItem(flowCamCfg);
        }

        public void SetFlowCamList(List<FlowCamCfg> flowCamCfgs)
        {
            listViewFlowCam.Items.Clear();
            foreach(FlowCamCfg cfg in flowCamCfgs)
            {
                AddFlowListViewItem(cfg);
            }
        }

        private void btnDelFlowCam_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewFlowCam.SelectedItems)
            {
                listViewFlowCam.Items.Remove(item);
            }
        }

        private List<FlowCamCfg> GetFlowCamCfgs()
        {
            List<FlowCamCfg> flowCamCfgs = new List<FlowCamCfg>();
            foreach(ListViewItem item in listViewFlowCam.Items)
            {
                FlowCamCfg cfg = new FlowCamCfg();
                cfg.IpAddress = item.Text;
                cfg.Port = UInt32.Parse(item.SubItems[1].Text);
                cfg.UserName = item.SubItems[2].Text;
                cfg.Password = item.SubItems[3].Text;
                cfg.Description = item.SubItems[4].Text;
                flowCamCfgs.Add(cfg);
            }
            return flowCamCfgs;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            GlobalConfig.GetInstance().FlowCamCfgList = GetFlowCamCfgs();
            try
            {
                GlobalConfig.GetInstance().ClusterTag = int.Parse(textBoxClusterTag.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("站点只能为数字", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GlobalConfig.GetInstance().ResourceCode = textBoxResourceCode.Text;
            GlobalConfig.GetInstance().RtPeopleUrl = textBoxRtPeopleUrl.Text;
            GlobalConfig.GetInstance().ParkingUrl = textBoxParkingUrl.Text;
            GlobalConfig.SaveConfig();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
