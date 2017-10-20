using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock.ViewModels
{
    public enum AlarmRecordViewModelEnum
    {
        SuccessNew,
        SuccessCancelAndUpdateExisting,
        SuccessUpdateExceptTime,
        SuccessUpdateNonexisting,
        FailureNew
    }
}
