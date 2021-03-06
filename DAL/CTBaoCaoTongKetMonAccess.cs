﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;

namespace DAL
{
    public class CTBaoCaoTongKetMonAccess : DatabaseAccess
    {
        public CTBaoCaoTongKetMonAccess() : base()
        { }

        public List<CTBaoCaoTongKetMon> GetAllCTBaoCaoTongKetMon()
        {
            OpenConnection();

            List<CTBaoCaoTongKetMon> listCTBaoCaoTongKetMon = new List<CTBaoCaoTongKetMon>();

            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "Select * from CT_BCAOTONGKETMON";
            com.Connection = conn;

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                string MaBCTKM = reader.GetInt32(0)+"";
                string MaLop = reader.GetString(1);
                int SoLuongDatMon = reader.GetInt32(2);
                float TiLeDatMon = reader.GetFloat(3);
                CTBaoCaoTongKetMon CTBCTongKetMon = new CTBaoCaoTongKetMon(MaBCTKM, MaLop, SoLuongDatMon, TiLeDatMon);
                listCTBaoCaoTongKetMon.Add(CTBCTongKetMon);
            }

            reader.Close();
            CloseConnection();
            return listCTBaoCaoTongKetMon;
        }

        public bool XoaCTBaoCaoTongKetMon(string MaBCTKM, string MaLop)
        {
            OpenConnection();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "delete from CT_BCAOTONGKETMON where MaBCTKM=@MaBCTKM and MaLop=@MaLop";
            com.Parameters.Add("@MaBCTKM", SqlDbType.Int).Value =Int32.Parse( MaBCTKM);
            com.Parameters.Add("@MaLop", SqlDbType.VarChar).Value = MaLop;
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

        public bool ThemCTBaoCaoTongKetMon(string MaBCTKM, string MaLop, int SoLuongDatMon, float TiLeDatMon)
        {
            try
            {
                OpenConnection();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.CommandText = "insert into CT_BCAOTONGKETMON values(@MaBCTKM,@MaLop, @SoLuongDatMon, @TiLeDatMon)";
                com.Connection = conn;
                com.Parameters.Add("@MaBCTKM", SqlDbType.Int).Value =Int32.Parse( MaBCTKM);
                com.Parameters.Add("@MaLop", SqlDbType.VarChar).Value = MaLop;
                com.Parameters.Add("@SoLuongDatMon", SqlDbType.Int).Value = SoLuongDatMon;
                com.Parameters.Add("@TiLeDatMon", SqlDbType.Float).Value = TiLeDatMon;

                int result = com.ExecuteNonQuery();

                CloseConnection();
                if (result > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaCTBaoCaoTongKetMon(string MaBCTKM, string MaLop,  int SoLuongDatMon, float TiLeDatMon)
        {
            try
            {
                OpenConnection();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.CommandText = "update  CT_BCAOTONGKETMON set SoLuongDatMon=@SoLuongDatMon , TiLeDatMon=@TiLeDatMon where  MaBCTKM=@MaBCTKM and MaLop=@MaLop ";
                com.Connection = conn;
                com.Parameters.Add("@MaBCTKM", SqlDbType.Int).Value =Int32.Parse( MaBCTKM);
                com.Parameters.Add("@MaLop", SqlDbType.VarChar).Value = MaLop;
                com.Parameters.Add("@SoLuongDatMon", SqlDbType.Int).Value = SoLuongDatMon;
                com.Parameters.Add("@TiLeDatMon", SqlDbType.Float).Value = TiLeDatMon;

                int result = com.ExecuteNonQuery();

                CloseConnection();
                if (result > 0)
                    return true;
                return false;

            }
            catch
            {
                return false;
            }
        }
    }
}
