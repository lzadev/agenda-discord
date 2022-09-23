using Agenda.Response;
using System.Collections.Generic;

namespace Agenda
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public ResultBody<T> result { get; set; }
        public ResponseError Errors { get; set; }
    }

    public class ResultBody<T>
    {
        public int totalAcount { get; set; }
        public IReadOnlyList<T> items { get; set; }
    }
}
