using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock.Pages.Views
{
    public class CheckedEventArgs
    {
        public bool Value { get; private set; }

        public CheckedEventArgs(bool value)
        {
            Value = value;
        }
    }
}
