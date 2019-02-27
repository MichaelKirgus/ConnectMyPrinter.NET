using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.ManagementConsole;

namespace Microsoft.ManagementConsole.SnapIns
{
    [NodeType(MyScopeNode.Guid, Description = MyScopeNode.Description)]
    public class MyScopeNode : ScopeNode
    {
        #region Constants
        #endregion
        public const string Guid = "{144fed39-54f9-4c08-94a9-5296f7f0ddf2}";
        public const string Description = "MMCSnapIn1 for MMC3.0";

        #region Construction
        #endregion
        public MyScopeNode()
        {
        }
    }
}