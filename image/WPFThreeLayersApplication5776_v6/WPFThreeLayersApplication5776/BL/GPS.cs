using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
class GPS
{
        // some property and filds
        // ...

    protected GPS() { }

    protected GPS instance = null;

    public GPS GetInstance()
    {
        if (instance == null)
            instance = new GPS();
        return instance;
    }
}
}
