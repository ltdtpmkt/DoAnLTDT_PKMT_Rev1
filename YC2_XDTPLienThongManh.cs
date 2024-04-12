using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DA_LTDT_PKMT_1
{
    internal class YC2_XDTPLienThongManh
    {
        public static int id = 0;
        public static int SoThanhPhanLienThong = 0;
        public static List<int>[]? ThanhPhanLienThong;
        
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

                // XÁC ĐỊNH THÀNH PHẦN LIÊN THÔNG
                int SoDinh = DanhSachKe.Length;
                ThanhPhanLienThong = new List<int>[SoDinh];
                
                TimThanhPhanLienThongManh(DanhSachKe);
                InThanhPhanLienThong();
                
                // Dua cac bien global ve gia tri ban dau
                id = 0;
                SoThanhPhanLienThong = 0;
                ThanhPhanLienThong = null;
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
        
        public static void ThuatToanThanhPhanLienThongManh(int u, int[] ids, int[] low, bool[] onStack, Stack<int> stack, List<int>[] DanhSachKe)
        {
            // Khoi tao id va low-link tai dinh u
            ids[u] = id;
            low[u] = id;
            id++;

            // Them dinh u vao stack
            stack.Push(u);
            onStack[u] = true;

            // Duyen qua cac dinh ke cua dinh u
            foreach (int v in DanhSachKe[u])
            {
                // Neu dinh v chua duoc tham thi goi ham ThuatToan tai dinh v
                // Sau do cap nhap gia tri low-link tai dinh u
                if (ids[v] == -1)
                {
                    ThuatToanThanhPhanLienThongManh(v, ids, low, onStack, stack, DanhSachKe);
                    low[u] = Math.Min(low[u], low[v]);
                }
                // Neu dinh v da duoc tham va dang co mat trong stack thi cap nhat low-link cua dinh u
                else if (onStack[v])
                {
                    low[u] = Math.Min(low[u], low[v]);
                }
            }

            // Neu gia tri low-link va id tai dinh u bang nhau thi xac dinh duoc 1 thanh phan lien thong manh
            // la cac dinh trong stack co gia tri low-link bang nhau
            int w = -1;
            if (low[u] == ids[u])
            {
                ThanhPhanLienThong[SoThanhPhanLienThong] = new List<int>();
                while (w != u)
                {
                    w = stack.Pop();
                    ThanhPhanLienThong[SoThanhPhanLienThong].Add(w);
                    onStack[w] = false;
                }
                SoThanhPhanLienThong++;
            }
        }

        public static void TimThanhPhanLienThongManh(List<int>[] DsKe)
        {
            int SoDinh = DsKe.Length;

            int[] ids = new int[SoDinh];
            int[] low = new int[SoDinh];
            bool[] onStack = new bool[SoDinh];
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < SoDinh; i++)
            {
                ids[i] = -1;
                low[i] = -1;
                onStack[i] = false;
            }

            for (int i = 0; i < SoDinh; i++)
            {
                if (ids[i] == -1)
                    ThuatToanThanhPhanLienThongManh(i, ids, low, onStack, stack, DsKe);
            }
        }

        public static void InThanhPhanLienThong()
        {
            if (SoThanhPhanLienThong == 0) return;
            int index = 1;
            while (index <= SoThanhPhanLienThong)
            {
                Console.Write("Thanh phan lien thong " + index + ": ");
                foreach (int i in ThanhPhanLienThong[index - 1])
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
                index++;
            }
        }
    }
}
