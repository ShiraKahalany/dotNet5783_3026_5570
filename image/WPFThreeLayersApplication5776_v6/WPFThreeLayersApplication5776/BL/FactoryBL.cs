using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
public class FactoryBL
{
    static IBL bl = null;
    public static IBL GetBL()
    {
        if (bl == null)
            bl = new BL_basic();
        return bl;
    }
}
}