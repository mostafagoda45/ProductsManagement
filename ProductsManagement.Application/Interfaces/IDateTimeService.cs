using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsManagement.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
