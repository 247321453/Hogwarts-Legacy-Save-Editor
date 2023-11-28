using Common;
using HLSE.Game;
using SQLiteCompare.Data;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLSE
{
    public partial class MainForm : Form
    {
        private CharacterSaveFileData mSaveData;
        public MainForm()
        {
            UIThread.Init();
            InitializeComponent();
            SetButtonsEnable(false);
            btn_choose_sav.Enabled = true;
        }

        private void btn_choose_sav_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {

                dlg.Title = "请选择一个存档文件";
                dlg.InitialDirectory = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Hogwarts Legacy\Saved\SaveGames\";
                dlg.Filter = "游戏存档文件|*.sav|All Files|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tb_save_path.Text = dlg.FileName;
                    LoadSave(dlg.FileName);
                }
            }
        }

        private void SetButtonsEnable(bool enable)
        {
            btn_choose_sav.Enabled = enable;
            btn_reset_talent.Enabled = enable;
            btn_save_sav.Enabled = enable;
            btn_reset.Enabled = enable;
            btn_set_gold.Enabled = enable;
            btn_set_name.Enabled = enable;
            btn_set_pack_cap.Enabled = enable;
        }

        private void LoadSave(string path)
        {
            SetButtonsEnable(false);
            Task.Run(() =>
            {
                var data = CharacterSaveFile.Read(path);
                UIThread.Post(() =>
                {
                    SetButtonsEnable(true);
                    if (data == null)
                    {
                        MessageBox.Show("加载存档出错!");
                    }
                    else
                    {
                        OnLoadSave(data);
                    }
                });
            });
        }

        private void OnLoadSave(CharacterSaveFileData data)
        {
            if (!data.OpenSQLite())
            {
                MessageBox.Show("无法解析存档!");
                return;
            }
            mSaveData = data;
            UpdateDataView();
        }

        private void UpdateDataView()
        {
            if (mSaveData == null || !mSaveData.IsOpen())
            {
                return;
            }
            int exp = GameFunUtil.GetAllExp(mSaveData.DB);
            tb_exp.Text = exp.ToString();
            tb_level.Text = CharacterUtil.GetLevel(exp).ToString();
            tb_talent.Text = GameFunUtil.GetTalentPoint(mSaveData.DB).ToString();
            tb_first_name.Text = GameFunUtil.GetFirstName(mSaveData.DB);
            tb_last_name.Text = GameFunUtil.GetLastName(mSaveData.DB);
            tb_gold.Text = GameFunUtil.ReadPackItemCount(mSaveData.DB, GameItems.GOLD, 0).ToString();
            tb_pack_cap.Text = GameFunUtil.GetBaseInventoryCapacity(mSaveData.DB).ToString();
        }

        private void btn_reset_talent_Click(object sender, EventArgs e)
        {
            if (mSaveData == null || !mSaveData.IsOpen())
            {
                return;
            }
            GameFunUtil.ResetTalent(mSaveData.DB);
            UpdateDataView();
        }

        private void btn_save_sav_Click(object sender, EventArgs e)
        {
            if (mSaveData != null)
            {
                if (CharacterSaveFile.Save(mSaveData))
                {
                    MessageBox.Show("保存成功!");
                }
                else
                {
                    MessageBox.Show("保存失败!");
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_set_name_Click(object sender, EventArgs e)
        {
            if (mSaveData == null || !mSaveData.IsOpen())
            {
                return;
            }
            string fname = tb_first_name.Text;
            string lname = tb_last_name.Text;
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname))
            {
                MessageBox.Show("名字不能为空");
                return;
            }
            GameFunUtil.SetFirstName(mSaveData.DB, fname);
            GameFunUtil.SetLastName(mSaveData.DB, lname);
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (mSaveData != null)
            {
                if (!mSaveData.ResetSQLite())
                {
                    MessageBox.Show("重置数据出错");
                }
            }
        }

        private void btn_set_gold_Click(object sender, EventArgs e)
        {
            //TODO
            int gold;
            if(int.TryParse(tb_gold.Text, out gold))
            {
                GameFunUtil.SetPackItemCount(mSaveData.DB, GameItems.GOLD, gold);
            } else
            {
                MessageBox.Show("修改金币出错");
            }
        }

        private void btn_set_pack_cap_Click(object sender, EventArgs e)
        {
            int val;
            if (int.TryParse(tb_pack_cap.Text, out val))
            {
                val = Math.Max(20, val);
                GameFunUtil.SetBaseInventoryCapacity(mSaveData.DB, val);
            }
            else
            {
                MessageBox.Show("修改背包容量出错");
            }
        }
    }
}
