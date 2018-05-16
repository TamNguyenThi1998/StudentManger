﻿using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.FormNhapLieu
{
    public partial class ThemSuaHTKT : Form
    {
        string ten=null;
        string ma=null;
        string heso=null;

        public ThemSuaHTKT()
        {
            InitializeComponent();
        }

        public ThemSuaHTKT(string ma,string ten, string heso)
        {
            InitializeComponent();
            this.ten = ten;
            this.heso = heso;
            this.ma=ma;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            HinhThucKiemTraBLL htktbll = new HinhThucKiemTraBLL();
            ErrorType result;
            //them
            if (this.ten == null)
                result = htktbll.ThemHinhThucKiemTra(tbMa.Text, tbTenHTKT.Text, tbHeSo.Text);
            else //sua
                result = htktbll.SuaHinhThucKiemTra(tbMa.Text, tbTenHTKT.Text, tbHeSo.Text);

            switch ((int)result)
            {
                case (int)ErrorType.KI_TU_RONG:
                    MessageBox.Show("Thất bại, bạn phải điền đầy đủ thông tin để thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case (int)ErrorType.DA_TON_TAI:
                    MessageBox.Show("Không thể thêm, hình thức kiểm tra này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case (int)ErrorType.THAT_BAI:
                    MessageBox.Show("Thất bại, Xin kiểm tra lại kết nối CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    string infor;
                    if (this.ten == null)
                        infor = "Thêm thành công!";
                    else
                        infor = "Sửa thành công !";
                    MessageBox.Show(infor, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
