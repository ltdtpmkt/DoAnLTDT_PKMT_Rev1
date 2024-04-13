using System;
using System.Collections.Generic;
using System.IO;
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
            string[] lines = File.ReadAllLines(DuongDanTapTin); //Đọc dữ liệu từ tập tin
            int n = int.Parse(lines[0]); // Lấy số đỉnh từ dòng đầu tiên
            int[,] MaTran_DoThi = new int[n, n]; //Tạo ma trận kề [n x n]

            for (int i = 1; i <= n; i++)
            {
                string[] Dinh = lines[i].Split(' ');
                int soluongDinhke = int.Parse(Dinh[0]);
            
                for (int j = 1; j < Dinh.Length; j += 2)
                {
                    int Dinhke = int.Parse(Dinh[j]);
                    int Trongso = 1; //Mặc định trọng số là 1
                    if (j + 1 < Dinh.Length)
                    {
                        Trongso = int.Parse(Dinh[j + 1]);
                    }
                    MaTran_DoThi[i - 1, Dinhke] = Trongso; //Đặt trọng số
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

        // CONVERT TẬP TIN THÀNH (SỐ ĐỈNH, DANH SÁCH CẠNH)
        public static (int, List<int[]>) ChuyenTapTinThanhDanhSachCanh(string DuongDanTapTin)
        {
			StreamReader reader = new StreamReader(DuongDanTapTin);
			int soDinh = int.Parse(reader.ReadLine());
            List<int[]> danhSachCanh = new List<int[]>();
			for (int i = 0; i < soDinh; i++)
            {
				string[] dong = reader.ReadLine().Split();
				for (int j = 1; j < dong.Length; j += 2)
				{
					int v = int.Parse(dong[j]);
					int w = int.Parse(dong[j + 1]);
					danhSachCanh.Add(new int[] { i, v, w });
				}
			}
			reader.Close();
            return (soDinh, danhSachCanh);
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
            int soluongDinh = MaTran_DoThi.GetLength(0);
            for (int i = 0; i < soluongDinh; i++)
            {
                for (int j = 0; j < soluongDinh; j++)
                {
                    if ((MaTran_DoThi[i, j] == 0 && MaTran_DoThi[j, i] != 0) || (MaTran_DoThi[i, j] != 0 && MaTran_DoThi[j, i] == 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

		public static bool DoThiCoHuong(List<int[]> danhSachCanh)
		{
			var daKiemTra = new List<(int, int)>();
			foreach (var canh in danhSachCanh)
            {
				int u = canh[0];
				int v = canh[1];
				if (!daKiemTra.Contains((v, u)))
                {
					daKiemTra.Add((u, v));
					if (!danhSachCanh.Any(canh2 => canh2[0] == v && canh2[1] == u))
						return true;
				}
			}
            return false;
		}

		//KIỂM TRA ĐỒ THỊ LIÊN THÔNG HOẶC KHÔNG LIÊN THÔNG
		public static bool DoThiLienThong(int[,] MaTran_DoThi)
        {
            int soluongDinh = MaTran_DoThi.GetLength(0);
            bool[] DaGheTham = new bool[soluongDinh];
            
            DFS(MaTran_DoThi, DaGheTham, 0);
            
            for (int i = 0; i < soluongDinh; i++)
            {
                if (!DaGheTham[i])
                {
                    return false; //Nếu tồn tại ít nhất một đỉnh không được duyệt, đồ thị không liên thông
                }
            }
            return true;
        }

		public static bool DoThiLienThong(int soDinh, List<int[]> danhSachCanh)
		{
			void DFS(int dinh, bool[] dinhDaTham)
			{
				dinhDaTham[dinh] = true;
				foreach (int[] canh in danhSachCanh)
				{
					if (canh[0] == dinh)
					{
						int dinhKe = canh[1];
						if (!dinhDaTham[dinhKe])
							DFS(dinhKe, dinhDaTham);
					}
					else if (canh[1] == dinh)
					{
						int dinhKe = canh[0];
						if (!dinhDaTham[dinhKe])
							DFS(dinhKe, dinhDaTham);
					}
				}
			}

			bool[] dinhDaTham = new bool[soDinh];
			DFS(0, dinhDaTham);
			for (int i = 0; i < soDinh; ++i)
			{
				if (!dinhDaTham[i])
					return false;
			}
			return true;
		}

		//KIỂM TRA ĐỒ THỊ CÓ CẠNH BỘI HAY KHÔNG CÓ CẠNH BỘI
		public static bool DoThiCoCanhBoi(List<int>[] DanhSachKe)
        {
            int SoDinh = DanhSachKe.Length;
            for (int i = 0; i < SoDinh; i++)
            {
                DanhSachKe[i].Sort();
                for (int j = 0; j < DanhSachKe[i].Count - 1; j++)
                {
                    if (DanhSachKe[i][j] == DanhSachKe[i][j + 1])
                        return true;
                }
            }
            return false;
        }

		//KIỂM TRA ĐỒ THỊ CÓ CẠNH KHUYÊN HAY KHÔNG CÓ CẠNH KHUYÊN
		public static bool DoThiCoCanhKhuyen(int[,] MaTran_DoThi)
		{
			int SoDinh = MaTran_DoThi.GetLength(0);
			for (int i = 0; i < SoDinh; i++)
			{
				if (MaTran_DoThi[i, i] != 0)
				{
					return true;
				}
			}
			return false;
		}

		// TẠO MA TRẬN CỦA ĐỒ THỊ NGHỊCH ĐẢO ĐỒ THỊ TRONG TẬP TIN
	    public static int[,] MaTranDao(int[,] MaTran)
		{
		    int SoDinh = MaTran.GetLength(0);
		    int[,] MaTranDao = new int[SoDinh, SoDinh];
		    for (int i = 0; i < SoDinh - 1; i++)
		    {
		        for (int j = i + 1; j < SoDinh; j++)
		        {
		            if (MaTran[i, j] != 0)
		            {
		                if (MaTran[j, i] == 0)
		                {
		                    MaTranDao[i, j] = 0;
		                    MaTranDao[j, i] = MaTran[i, j];
		                }
		            } 
		            else
		            {
		                if (MaTran[j, i] != 0)
		                {
		                    MaTranDao[i, j] = MaTran[j, i];
		                    MaTranDao[j, i] = 0;
		                }
		            }
		        }
		    }
		    return MaTranDao;
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
