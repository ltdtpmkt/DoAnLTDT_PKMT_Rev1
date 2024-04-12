using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DA_LTDT_PKMT_1
{
    internal class YC2_XDTPLienThongManh
    {
        public static void main()
        {
            string DuongDanTapTin = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC2_Taptin.txt");

            if (YC2_DieuKien(DuongDanTapTin) == false)
            {
                Console.WriteLine("Không phải là đồ thị có hướng, không có cạnh bội và không có cạnh khuyên");
            }
            else
            {
                if (XuLyChung.DoThiLienThong(XuLyChung.ChuyenTapTinThanhMatrix(DuongDanTapTin)) == false)
                {
                    Console.WriteLine("Đồ thị không liên thông");
                }
                else
                {
                    if (YC2_KiemTraDoThiLienThongManh(DuongDanTapTin) == true)
                    {
                        Console.WriteLine("Đồ thị liên thông mạnh");
                    }
                    else if (YC2_DoThiLienThongTungPhan(DuongDanTapTin) == true)
                    {
                        Console.WriteLine("Đồ thị liên thông từng phần");
                    }
                    else
                    {
                        Console.WriteLine("Đồ thị liên thông yếu");
                    }
                }                

                YC2_XacDinhThanhPhanLienThongManh(DuongDanTapTin);
            }
        }

        public static bool YC2_DieuKien(string DuongDanTapTin)
        {
            int[,] MaTran_DoThi = XuLyChung.ChuyenTapTinThanhMatrix(DuongDanTapTin);
            List<int> DanhSachKe = XuLyChung.ChuyenTapTinThanhDanhSachKe(DuongDanTapTin);
            XuLyChung.XuatMaTranDoThi(MaTran_DoThi);
            if (XuLyChung.DoThiCoHuong(MaTran_DoThi) == false || XuLyChung.DoThiCoCanhBoi(DanhSachKe) == true || XuLyChung.DoThiCoCanhKhuyen(MaTran_DoThi) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool YC2_KiemTraDoThiLienThongManh(string DuongDanTapTin)
        {
            int[,] MaTran = XuLyChung.ChuyenTapTinThanhMatrix(DuongDanTapTin);
            bool[] DaGheTham = new bool[MaTran.GetLength(0)];
            
            XuLyChung.DFS(MaTran, DaGheTham, 0);
            
            for (int i = 0; i < MaTran.GetLength(0); i++)
            {
                if (!DaGheTham[i])
                {
                    return false;
                }
            }
            return true;
        }
        
        public static bool YC2_DoThiLienThongTungPhan(string DuongDanTapTin)
        {
            int[,] MaTran = XuLyChung.ChuyenTapTinThanhMatrix(DuongDanTapTin);
            int[,] MaTranDao = XuLyChung.MaTranDao(MaTran);
            
            bool[] DaGheTham = new bool[MaTran.GetLength(0)];
            bool[] DaGheThamDao = new bool[MaTran.GetLength(0)];
            
            XuLyChung.DFS(MaTran, DaGheTham, 0);
            XuLyChung.DFS(MaTranDao, DaGheThamDao, 0);
            
            for (int i = 0; i < MaTran.GetLength(0); i++)
            {
                if (!DaGheTham[i] && !DaGheThamDao[i])
                {
                    return false;
                }
            }
            return true;
        }
        
        public static bool YC2_KiemTraDoThiLienThongYeu(string DuongDanTapTin)
        {
            //VIỆC CẦN LÀM: Viết hàm xử lý xác định có phải là đồ thị liên thông mạnh không ?
            return false;
        }
        
        public static void YC2_XacDinhThanhPhanLienThongManh(string DuongDanTapTin)
        {
            //VIỆC CẦN LÀM: Viết hàm xác định tất cả thành phần liên thông mạnh trong đồ thị;
        }
    }
}
