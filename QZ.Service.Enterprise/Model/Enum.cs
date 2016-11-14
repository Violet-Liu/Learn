using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Service.Enterprise
{
    /// <summary>
    /// Execution state of service
    /// </summary>
    public enum Service_EXE_State
    {
        /// <summary>
        /// normal state
        /// </summary>
        Normal,
        /// <summary>
        /// client cipher key error
        /// </summary>
        C_Key_Err,
        /// <summary>
        /// Server token error
        /// </summary>
        S_Token_Err,

    }

    public enum Validate_Lvl
    {
        Key=1,
        Token=2,
    }



    public enum Comment_Type
    {
        topic=0,
        reply=1
    }


}
