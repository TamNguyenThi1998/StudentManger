﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class BangDiemMonAccess:DatabaseAccess
    {

        public BangDiemMonAccess() : base() { }

        public List<BangDiemMon> GetAllBangDiemMon()
        {
            OpenConnection();

            List<BangDiemMon> listBD = new List<BangDiemMon>();

            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "Select * from BangDiemMon";
            com.Connection = conn;

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                string maBD = reader.GetString(0);
                string maQT = reader.GetString(1);
                string maMH = reader.GetString(2);
                string diemTB = reader.GetString(3);

                BangDiemMon bd = new BangDiemMon(maBD, maQT,maMH,float.Parse( diemTB));
                listBD.Add(bd);
            }

            reader.Close();
            CloseConnection();
            return listBD;
        }

        public BangDiemMon GetBangDiemMon(string maQTH,string mamh)
        {
            OpenConnection();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "Select * from MonHoc where maQTH=@ma and mamonhoc=@mamh";
            com.Parameters.Add("@ma", SqlDbType.VarChar).Value = maQTH;
            com.Parameters.Add("@mamh", SqlDbType.VarChar).Value = mamh;
            com.Connection = conn;

            SqlDataReader reader = com.ExecuteReader();
            BangDiemMon bd = null;
            while (reader.Read())
            {
                string maBD = reader.GetString(0);
                string maQT = reader.GetString(1);
                string maMH = reader.GetString(2);
                string diemTB = reader.GetString(3);

                bd = new BangDiemMon(maBD, maQT, maMH, float.Parse(diemTB));
            }

            reader.Close();
            CloseConnection();
            return bd;
        }

        public bool XoaBD(string maBD)
        {
            OpenConnection();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "delete from MonHoc where MaBangDiemMon=@ma";
            com.Parameters.Add("@ma", SqlDbType.VarChar).Value = maBD;
            com.Connection = conn;

            try
            {
                int check = com.ExecuteNonQuery();
                if (check > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        public ErrorType ThemBD(string maBD ,string maQT,string maMH)
        {
            try
            {
                OpenConnection();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.CommandText = "insert into MonHoc values(@maBD,@maQT,@maMH)";
                com.Connection = conn;

                com.Parameters.Add("@maBD", SqlDbType.VarChar).Value = maBD;
                com.Parameters.Add("@maQT", SqlDbType.NVarChar).Value = maQT;
                com.Parameters.Add("@maMH", SqlDbType.NVarChar).Value = maMH;

                int result = com.ExecuteNonQuery();

                CloseConnection();
                if (result > 0)
                    return ErrorType.THANH_CONG;
                return ErrorType.THAT_BAI;
            }
            catch
            {
                return ErrorType.THAT_BAI;
            }
        }

        public ErrorType SuaBD(string maBD, string maQT, string maMH)
        {
            try
            {
                OpenConnection();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.CommandText = "update  MonHoc set maQTH=@maQT ,mamonhoc=@maMH where  Mabangdiem=@maBD";
                com.Connection = conn;

                com.Parameters.Add("@maBD", SqlDbType.VarChar).Value = maBD;
                com.Parameters.Add("@maQT", SqlDbType.NVarChar).Value = maQT;
                com.Parameters.Add("@maMH", SqlDbType.NVarChar).Value = maMH;

                int result = com.ExecuteNonQuery();

                CloseConnection();
                if (result > 0)
                    return ErrorType.THANH_CONG;
                return ErrorType.THAT_BAI;
            }

            catch
            {
                return ErrorType.THAT_BAI;
            }
        }

        public string GetMaBDMMax()
        {
            OpenConnection();


            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = " select top 1 MaBangDiemMon from BANGDIEMMON order by MaBangDiemMon DESC";
            com.Connection = conn;

            SqlDataReader reader = com.ExecuteReader();
            string maBD = null;

            if (reader.Read())
            {
                 maBD = reader.GetString(0);
            }

            reader.Close();
            CloseConnection();
            return maBD;
        }
    }
}
