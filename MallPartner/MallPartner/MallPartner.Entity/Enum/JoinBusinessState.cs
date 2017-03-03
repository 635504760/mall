using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Entity.Enum
{
    /// <summary>
    /// 合作商状态
    /// </summary>
    public enum JoinBusinessState
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disabled = 0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enabled = 1,
    }
}
