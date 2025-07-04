using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
    public interface IGoogleMeetService
    {
        Task<string> CreateMeetingAsync(DateTime startTime, DateTime endTime);
    }
}
