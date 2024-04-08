using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DA_LTDT_PKMT_1
{
    internal class YC1_NhanDienDoThi
    {
        public static void main()
        {
            string DuongDanTapTin = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC1_Taptin.txt");

            if (YC1_DieuKien(DuongDanTapTin) == false)
            {
                Console.WriteLine("Không phải là đồ thị vô hướng, không có cạnh bội và không có cạnh khuyên");
            }
            else
            {
                if (YC1_KiemTraDoThiCoiXayGio(DuongDanTapTin) == false)
                {
                    Console.WriteLine("Đồ thị Cối xay gió: Không");
                }
                else
                {
                    //Viết hàm tìm n trong Wd(3,n)                 
                }

                if (YC1_KiemTraDoThiBarbell(DuongDanTapTin) == false)
                {
                    Console.WriteLine("Đồ thị Barbell: Không");
                }
                else
                {
                    Console.WriteLine("Đồ thị Barbell: Có");
                }

                if (YC1_KiemTraDoThiK_Phan(DuongDanTapTin) == false)
                {
                    Console.WriteLine("Đồ thị K-Phân: Không");
                }
                else
                {
                    //Viết hàm tìm k và xác định chỉ mục các đỉnh   
                }
            }

        }

        public static bool YC1_DieuKien(string DuongDanTapTin)
        {

            int[,] MaTran_DoThi = XuLyChung.ChuyenTapTinThanhMatrix(DuongDanTapTin);
            XuLyChung.XuatMaTranDoThi(MaTran_DoThi);
            if (XuLyChung.DoThiCoHuong(MaTran_DoThi) == false || XuLyChung.DoThiCoCanhBoi(MaTran_DoThi) == false || XuLyChung.DoThiCoCanhKhuyen(MaTran_DoThi) == false)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static bool YC1_KiemTraDoThiCoiXayGio(string DuongDanTapTin)
        {
            //VIỆC CẦN LÀM
            return true;
        }

        public static bool YC1_KiemTraDoThiBarbell(string DuongDanTapTin)
        {
            //VIỆC CẦN LÀM
            return true;
        }
        public static bool YC1_KiemTraDoThiK_Phan(string DuongDanTapTin)
        {
            //VIỆC CẦN LÀM
            return true;
        }
    }
}
