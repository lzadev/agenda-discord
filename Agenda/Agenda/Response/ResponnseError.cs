using System.Collections.Generic;

namespace Agenda.Response
{
    public class ResponseError
    {
        public IEnumerable<Error> Error { get; set; }

        public bool UnAuthorizedRequest { get; set; }
    }

    public partial class Error
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }
    }
}
