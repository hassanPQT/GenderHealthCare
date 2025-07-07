using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enums
{
	public enum CycleDayType
	{
		Menstruation,     // Ngày có kinh nguyệt
		FertileStart,     // Ngày bắt đầu có khả năng thụ thai
		Fertile,          // Ngày có khả năng thụ thai
		HighFertility,    // Ngày có khả năng thụ thai cao
		Ovulation,        // Ngày rụng trứng (khả năng thụ thai cao nhất)
		RelativeSafe,     // Ngày an toàn tương đối
		AbsoluteSafe,     // Ngày an toàn tuyệt đối
		TakePill,        // Ngày cần uống thuốc tránh thai
		Unknown           // Không xác định
	}
}
