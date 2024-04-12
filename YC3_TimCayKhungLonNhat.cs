using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_LTDT_PKMT_1
{
    internal class YC3_TimCayKhungLonNhat
    {
		public static void main()
		{
			string DuongDanTapTin = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "YC3_Taptin.txt");
			var a = XuLyChung.ChuyenTapTinThanhDanhSachCanh(DuongDanTapTin);
			int soDinh = a.Item1;
			List<int[]> danhSachCanh = a.Item2;

			if (!XuLyChung.DoThiCoHuong(danhSachCanh) && XuLyChung.DoThiLienThong(soDinh, danhSachCanh))
			{
				Console.WriteLine("Là đồ thị vô hướng liên thông");
				List<int[]> ketQuaPrim = Prim(soDinh, danhSachCanh);
				InKetQua("Prim", ketQuaPrim);
				List<int[]> ketQuaKruskal = Kruskal(soDinh, danhSachCanh);
				InKetQua("Kruskal", ketQuaKruskal);
			}
			else
			{
				Console.WriteLine("Không phải là đồ thị vô hướng liên thông");
			}
		}

		public static List<int[]> Prim(int soDinh, List<int[]> danhSachCanh)
		{
			List<int[]> ketQua = new List<int[]>();
			bool[] dinhDaDuyet = new bool[soDinh];
			int dinhBatDau = 0;
			dinhDaDuyet[dinhBatDau] = true;

			// Tạo hàng đợi ưu tiên
			SortedSet<Tuple<int, int, int>> hdut = new SortedSet<Tuple<int, int, int>>();

			// Thêm các cạnh kề với đỉnh bắt đầu vào hdut, có dấu trừ trước trọng số để sắp xếp giảm dần
			foreach (var canh in danhSachCanh)
			{
				if (canh[0] == dinhBatDau)
					hdut.Add(new Tuple<int, int, int>(-canh[2], canh[0], canh[1]));
				else if (canh[1] == dinhBatDau)
					hdut.Add(new Tuple<int, int, int>(-canh[2], canh[1], canh[0]));
			}

			while (hdut.Count > 0)
			{
				// Lấy cạnh có trọng số lớn nhất từ hdut
				var canh = hdut.First();
				hdut.Remove(canh);
				int u = canh.Item2;
				int v = canh.Item3;
				int w = -canh.Item1;

				// Thêm cạnh đó vào cây khung nếu đỉnh kề chưa được thêm
				if (!dinhDaDuyet[v])
				{
					dinhDaDuyet[v] = true;
					ketQua.Add(new int[] { u, v, w });

					// Thêm các cạnh kề với đỉnh mới được thêm vào hdut
					foreach (var canhKe in danhSachCanh)
					{
						if ((canhKe[0] == v && !dinhDaDuyet[canhKe[1]]) || (canhKe[1] == v && !dinhDaDuyet[canhKe[0]]))
							hdut.Add(new Tuple<int, int, int>(-canhKe[2], canhKe[0], canhKe[1]));
					}
				}
			}
			return ketQua;
		}

		public static List<int[]> Kruskal(int soDinh, List<int[]> danhSachCanh)
		{
			// Tìm đỉnh gốc của cây con
			int TimDinhGoc(int[] cayCha, int i)
			{
				if (cayCha[i] == i)
					return i;
				return TimDinhGoc(cayCha, cayCha[i]);
			}

			List<int[]> ketQua = new List<int[]>();
			int soCanh = 0;
			int indexCanh = 0;
			danhSachCanh.Sort((x, y) => y[2].CompareTo(x[2])); // Sắp xếp các cạnh theo trọng số giảm dần

			int[] cayCha = new int[soDinh];
			int[] doSau = new int[soDinh];

			// Tạo cây con cho mỗi đỉnh
			for (int i = 0; i < soDinh; i++)
			{
				cayCha[i] = i;
				doSau[i] = 0;
			}

			while (soCanh < soDinh - 1)
			{
				int[] nextEdge = danhSachCanh[indexCanh++];
				int a = TimDinhGoc(cayCha, nextEdge[0]);
				int b = TimDinhGoc(cayCha, nextEdge[1]);

				// Nếu không tạo thành chu trình thì thêm cạnh vào cây khung và hợp thể 2 cây con
				if (a != b)
				{
					ketQua.Add(nextEdge);
					int aRoot = TimDinhGoc(cayCha, a);
					int bRoot = TimDinhGoc(cayCha, b);

					// So sánh độ sâu của 2 cây con
					if (doSau[aRoot] < doSau[bRoot])
						cayCha[aRoot] = bRoot;
					else if (doSau[aRoot] > doSau[bRoot])
						cayCha[bRoot] = aRoot;
					else
					{
						cayCha[bRoot] = aRoot;
						doSau[aRoot]++;
					}
					soCanh++;
				}
			}
			return ketQua;
		}

		public static void InKetQua(string giaiThuat, List<int[]> ketQua)
		{
			Console.WriteLine($"Giải thuật {giaiThuat}");
			Console.WriteLine("Tập cạnh của cây khung");
			int trongSoCay = 0;
			foreach (var canh in ketQua)
			{
				Console.WriteLine($"{canh[0]} - {canh[1]}: {canh[2]}");
				trongSoCay += canh[2];
			}
			Console.WriteLine($"Trọng số của cây khung: {trongSoCay}");
		}
	}
}
