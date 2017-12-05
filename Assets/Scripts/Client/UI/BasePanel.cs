using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.UI
{
    abstract class BasePanel
    {
        public virtual IEnumerator Open()
        {
            return null;
        }

        public virtual IEnumerator Close()
        {
            return null;
        }
    }
}
