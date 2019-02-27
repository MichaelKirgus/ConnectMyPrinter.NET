// Microsoft Management Console 3.0
// http://msdn.microsoft.com/en-us/library/ee663284(v=VS.85).aspx

// Created by Thomas van Veen
// thomas.vanVeen@gmx.net

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.ManagementConsole;

namespace Microsoft.ManagementConsole.SnapIns
{
    [SnapInSettings(MySnapIn.Guid,
        Vendor = MySnapIn.Vendor,
        Description = MySnapIn.Description,
        DisplayName = MySnapIn.DisplayName)]
    public class MySnapIn : SnapIn
    {
        #region Constants
        #endregion
        public const string Guid = "{7728f349-f3c1-44f6-a6c7-79a3e489911d}";
        public const string DisplayName = "MMCSnapIn1";
        public const string Description = "MMCSnapIn1 for MMC 3.0";
        public const string Vendor = "";

        #region Properties
        #endregion
        public List<object> PersistenceData { get; set; }

        #region Construction
        #endregion
        public MySnapIn()
        {
        }

        #region Events
        #endregion
        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.RootNode = new ScopeNode(new Guid(MyScopeNode.Guid), false);
            this.RootNode.DisplayName = "MMCSnapIn1";
        }
        protected override bool OnShowInitializationWizard()
        {
            return base.OnShowInitializationWizard();
        }
        protected override void OnShutdown(AsyncStatus status)
        {
            base.OnShutdown(status);
        }
        protected override void OnLoadCustomData(AsyncStatus status, byte[] persistenceData)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(persistenceData, 0, persistenceData.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);

                BinaryFormatter binaryFormatter = new BinaryFormatter();

                this.PersistenceData = (List<object>)binaryFormatter.Deserialize(memoryStream);
            }
        }
        protected override byte[] OnSaveCustomData(SyncStatus status)
        {
            if (this.PersistenceData == null)
            {
                this.PersistenceData = new List<object>();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, this.PersistenceData);

                return memoryStream.ToArray();
            }
        }
    }
}