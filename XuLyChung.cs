using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_LTDT_PKMT_1
{
    internal class XuLyChung
    {
        //ĐƯỜNG DẪN TẬP TIN THEO CÁC YÊU CẦU
        string DuongDanTapTinYC1 = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC1_Taptin.txt");
        string DuongDanTapTinYC2 = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC2_Taptin.txt");
        string DuongDanTapTinYC3 = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC3_Taptin.txt");
        string DuongDanTapTinYC4 = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC4_Taptin.txt");
        string DuongDanTapTinYC5 = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC5_Taptin.txt");

        //CONVERT TẬP TIN THÀNH MA TRẬN
        public static int [,] ChuyenTapTinThanhMatrix(string DuongDanTapTin)
        {
            string[] lines = File.ReadAllLines(DuongDanTapTin);
            int n = int.Parse(lines[0]);
            int[,] MaTran_DoThi = new int[n, n];

            for (int i = 2; i <= n; i++)
            {
                string[] Dinh = lines[i].Split(' ');
                // Kiểm tra xem mảng Dinh có đủ phần tử để truy cập không
                if (Dinh.Length == n)
                {
                    for (int j = 0; j < n; j++)
                    {
                        MaTran_DoThi[i - 1, j] = int.Parse(Dinh[j]);
                    }
                }
                else
                {
                    // Xử lý trường hợp mảng Dinh không có đủ phần tử
                    Console.WriteLine("Lỗi: Dòng thứ " + i + " không đủ phần tử.");
                    // Gán giá trị mặc định (ví dụ: 0) cho các phần tử còn thiếu
                    for (int j = 0; j < n; j++)
                    {
                        MaTran_DoThi[i - 1, j] = 0;
                    }
                }
            }
            return MaTran_DoThi;
        }

        //CONVERT TẬP TIN THÀNH DANH SÁCH KỀ
        public static List<int>[]? ChuyenTapTinThanhDanhSachKe(string DuongDanTapTin)
        {
            if (!File.Exists(DuongDanTapTin))
            {
                Console.WriteLine("Khong tim thay tap tin.");
                return null;
            }
        
            string? Dong;
            StreamReader sr = new StreamReader(DuongDanTapTin);
        
            // Đọc dòng đầu tiên của tập tin
            Dong = sr.ReadLine();
            int SoDinh = int.Parse(Dong);
        
            // Khởi tạo danh sách kề
            List<int>[] DanhSachKe = new List<int>[SoDinh];
            for (int i = 0; i < SoDinh; i++)
            {
                DanhSachKe[i] = new List<int>();
            }
        
            // Đọc dòng kế tiếp, tạo vòng lặp cho đến dòng cuối
            Dong = sr.ReadLine();
        
            int u = 0;
            while (Dong != null)
            {
                string[] list = Dong.Split(' ');
                int SoDinhKe = int.Parse(list[0]);
                if (SoDinhKe == 0)
                {
                    Dong = sr.ReadLine();
                    continue;
                }
                for (int i = 1; i < list.Length; i = i + 2)
                {
                    int v = int.Parse(list[i]);
                    DanhSachKe[u].Add(v);
                }
                
                u++;
                Dong = sr.ReadLine();
            }
        
            // Đóng tập tin
            sr.Close();
        
            return DanhSachKe;
        }

        //IN MA TRẬN RA MÀN HÌNH
        public static void XuatMaTranDoThi(int[,] MaTran_DoThi)
        {
            for (int i = 0; i < MaTran_DoThi.GetLength(0); i++)
            {
                for (int j = 0; j < MaTran_DoThi.GetLength(1); j++)
                {
                    Console.Write($"{MaTran_DoThi[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        //Duyệt theo chiều sâu  Depth-First Search
        public static void DFS(int[,] MaTran_DoThi, bool[] DaGheTham, int Dinh)
        {
            DaGheTham[Dinh] = true;
            int n = MaTran_DoThi.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                if (MaTran_DoThi[Dinh, i] == 1 && !DaGheTham[i])
                {
                    DFS(MaTran_DoThi, DaGheTham, i);
                }
            }
        }

        //Duyệt theo chiều rộng Breadth-first search
        public static void BFS(int[,] MaTran_DoThi, int DinhBatDau)
        {
            int n = MaTran_DoThi.GetLength(0);
            bool[] DaGheTham = new bool[n]; // Khai báo mảng đánh dấu các đỉnh đã ghé thăm
            Queue<int> queue = new Queue<int>(); // Khai báo hàng đợi đồ thị

            DaGheTham[DinhBatDau] = true; // Đánh dấu đỉnh bắt đầu đã ghé thăm
            queue.Enqueue(DinhBatDau); // Thêm đỉnh bắt đầu vào hàng đợi

            while (queue.Count > 0)
            {
                int Dinhhientai = queue.Dequeue(); // Lấy đỉnh đầu tiên ra khỏi hàng đợi
                Console.WriteLine("Đỉnh được duyệt: " + Dinhhientai);
                
                for (int i = 0; i < n; i++)
                {
                    if (MaTran_DoThi[Dinhhientai, i] == 1 && !DaGheTham[i])
                    {
                        DaGheTham[i] = true; // Đánh dấu đỉnh kề đã được ghé thăm
                        queue.Enqueue(i); // Thêm đỉnh tiếp theo vào hàng đợi để duyệt tiếp
                    }
                }
            }
        }

        //KIỂM TRA ĐỒ THỊ CÓ HƯỚNG HOẶC VÔ HƯỚNG
        public static bool DoThiCoHuong(int[,] MaTran_DoThi)
        {
            //Viết hàm kiểm tra
            return true;
        }

        //KIỂM TRA ĐỒ THỊ LIÊN THÔNG HOẶC KHÔNG LIÊN THÔNG
        public static bool DoThiLienThong(int[,] MaTran_DoThi)
        {
            //Viết hàm kiểm tra
            return true;
        }

        //KIỂM TRA ĐỒ THỊ CÓ CẠNH BỘI HAY KHÔNG CÓ CẠNH BỘI
        public static bool DoThiCoCanhBoi(int[,] MaTran_DoThi)
        {
            //Viết hàm kiểm tra
            return true;
        }

        //KIỂM TRA ĐỒ THỊ CÓ CẠNH KHUYÊN HAY KHÔNG CÓ CẠNH KHUYÊN
        public static bool DoThiCoCanhKhuyen(int[,] MaTran_DoThi)
        {
            //Viết hàm kiểm tra
            return true;
        }

        //XỬ LÝ KIỂU NHẬP SỐ
        public static int NhapSo(string tenBien)
        {
            int kq = 0;
            bool dieukien = false;
            string str = "";

            do
            {
                Console.Write($" >Nhập {tenBien}: ");
                str = string.Concat(Console.ReadLine());
                try
                {
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        kq = 0;
                        break;
                    }
                    else
                    {
                        kq = int.Parse(str);
                    }
                    dieukien = true;
                }
                catch (FormatException)
                {
                    Console.Write($"*Ghi chú: {tenBien} không phải dạng số. Đề nghị nhập lại: \n");
                }
            } while (!dieukien);

            return kq;
        }

    }
}
