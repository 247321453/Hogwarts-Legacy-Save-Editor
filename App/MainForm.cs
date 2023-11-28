using Common;
using HLSE.Game;
using SQLiteCompare.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLSE
{
    public partial class MainForm : Form
    {
        private CharacterSaveFileData mSaveData;
        private SQLiteHelper mDB;
        public MainForm()
        {
            UIThread.Init();
            InitializeComponent();
            SetButtonsEnable(false);
            btn_choose_sav.Enabled = true;
        }

        private void btn_choose_sav_Click(object sender, EventArgs e)
        {
            using(var dlg = new OpenFileDialog())
            {

                dlg.Title = "Choose Save File";
                dlg.InitialDirectory = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Hogwarts Legacy\Saved\SaveGames\";
                dlg.Filter = "Game Save|*.sav|All Files|*.*";
                if(dlg.ShowDialog() == DialogResult.OK ) {
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
                        MessageBox.Show("Load save file failed!");
                    } else
                    {
                        OnLoadSave(data);
                    }
                });
            });
        }

        private void OnLoadSave(CharacterSaveFileData data)
        {
         
            var db = new SQLiteHelper(data.TempSqliteFilePath);
            if (!db.Open())
            {
                MessageBox.Show("Game Data is bad!");
                db.Close();
                return;
            }
            if (mDB != null)
            {
                mDB.Close();
            }
            mDB = db;
            mSaveData = data;
            UpdateDataView();
        }

        private void UpdateDataView()
        {
            if(mDB == null)
            {
                return;
            }
            int exp = GameFunUtil.GetAllExp(mDB);
            tb_level.Text = CharacterUtil.GetLevel(exp).ToString();
            tb_talent.Text = GameFunUtil.GetTalentPoint(mDB).ToString();
        }

        private void btn_reset_talent_Click(object sender, EventArgs e)
        {
            if (mDB == null)
            {
                return;
            }
            GameFunUtil.ResetTalent(mDB);
            UpdateDataView();
        }

        private void btn_save_sav_Click(object sender, EventArgs e)
        {
            if(mSaveData != null)
            {
                if (CharacterSaveFile.Save(mSaveData))
                {
                    MessageBox.Show("Save file success!");
                } else
                {
                    MessageBox.Show("Save file failed!");
                }
            }
        }
    }
}
