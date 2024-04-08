using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_LTDT_PKMT_1
{
    internal class XuLyKieuString 
    {
        public static void TrangBiaDoAn()
        {
            Console.WriteLine(
                "      ĐẠI HỌC QUỐC GIA HỒ CHÍ MINH\n" +
                "    TRƯỜNG ĐẠI HỌC KHOA HỌC TỰ NHIÊN\n\n\n\n\n" +
                "   ĐỒ ÁN MÔN HỌC: LÝ THUYẾT ĐỒ THỊ\n" +
                "      PHẦN MỀM QUẢN LÝ CỬA HÀNG\n\n\n\n\n" +
                "GIẢNG VIÊN         : ...\n" +
                "HỌ VÀ TÊN NHÓM SINH VIÊN: PHƯƠNG + KHIÊM + MINH + TRUNG\n" +
                "MÃ SỐ SINH VIÊN    : ... \n\n\n\n\n\n" +
                "      TP HỒ CHÍ MINH, NĂM 2024\n" +
                "__________________________________________");

            Console.WriteLine("\n\nEnter để triển khai Đồ án");
            Console.ReadKey();
            Console.Clear();
        }
        //public static string NhapDuongDanTapTin()
        //{
        //    bool dieukien = true;
        //    string str = "";

        //    do
        //    {
        //        Console.Write($"\n>Nhập Tên Hàng: ");
        //        str = string.Concat(Console.ReadLine());
        //        if (string.IsNullOrEmpty(str))
        //        {
        //            Console.WriteLine($"*Ghi chú: Tên Hàng không được để trống");
        //            dieukien = false;
        //        }
        //        else
        //        {
        //            dieukien = true;
        //        }
        //    } while (dieukien == false);

        //    return str;
        //}

        public static void QuayLaiMENU()
        {
            Console.WriteLine("Enter để quay lại MENU");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
