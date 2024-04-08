using DA_LTDT_PKMT_1;
using System.Security.Cryptography;

namespace LTDT_PKMT
{
    internal class Program
    {
        static void Main()
        {
            XuLyKieuString.TrangBiaDoAn();                    

            bool ChayChuongTrinh = true;
            while (ChayChuongTrinh == true)
            {
                Console.WriteLine(
                "________________________________\n" +
                "ĐỒ ÁN MÔN LÝ THUYẾT ĐỒ THỊ - MENU CHÍNH\n" +                             
                "1. NHẬN DIỆN MỘT SỐ DẠNG ĐỒ THỊ ĐẶC BIỆT\n" +
                "2. XÁC ĐỊNH THÀNH PHẦN LIÊN THÔNG MẠNH\n" +
                "3. TÌM CÂY KHUNG NHỎ NHẤT\n" +
                "4. TÌM ĐƯỜNG ĐI NGẮN NHẤT\n" +
                "5. TÌM CHU TRÌNH ĐƯỜNG ĐI EULER\n" +                
                "________________________________\n" +
                "0. THOÁT PHẦN MỀM\n" +
                "________________________________");
                Console.Write("Chọn chức năng (0-5): ");
                string LuaChon = string.Concat(Console.ReadLine());
                Console.Clear();

                switch (LuaChon)
                {
                    case "1":
                        Console.WriteLine("1. NHẬN DIỆN MỘT SỐ ĐỒ THỊ ĐẶC BIỆT");
                        YC1_NhanDienDoThi.main();                        
                        XuLyKieuString.QuayLaiMENU();
                        break;
                    case "2":
                        Console.WriteLine("2. XÁC ĐỊNH THÀNH PHẦN LIÊN THÔNG MẠNH");
                        YC2_XDTPLienThongManh.main();
                        XuLyKieuString.QuayLaiMENU();
                        break;
                    case "3":
                        Console.WriteLine("3. TÌM CÂY KHUNG NHỎ NHẤT");
                        //Đoạn này là code thực thi
                        XuLyKieuString.QuayLaiMENU();
                        break;
                    case "4":
                        Console.WriteLine("4. TÌM ĐƯỜNG ĐI NGẮN NHẤT");
                        //Đoạn này là code thực thi
                        XuLyKieuString.QuayLaiMENU();
                        break;
                    case "5":
                        Console.WriteLine("5. TÌM CHU TRÌNH ĐƯỜNG ĐI EULER");
                        //Đoạn này là code thực thi
                        XuLyKieuString.QuayLaiMENU();
                        break;
                    
                    case "0":
                        Console.WriteLine("\nĐÓNG ĐỒ ÁN");
                        ChayChuongTrinh = false;
                        break;
                    default:
                        Console.WriteLine("\nChức năng không hợp lệ.");
                        XuLyKieuString.QuayLaiMENU();
                        break;
                }
            }
        }
    }
}
