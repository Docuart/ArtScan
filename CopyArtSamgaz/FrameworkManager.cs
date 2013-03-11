using System;
using System.Collections.Generic;
using System.Linq;
using Canon.Eos.Framework;

namespace CopyArt
{
    public sealed class FrameworkManager
    {
        public FrameworkManager()
        {
            _framework = new EosFramework();
        }

        private EosFramework _framework;

        public event EventHandler CameraAdded;

        public IEnumerable<EosCamera> GetCameras()
        {
            using (var cameras = _framework.GetCameraCollection())
                return cameras.ToArray();            
        }

        public void LoadFramework()
        {
            if (_framework == null)
            {
                _framework = new EosFramework();
                _framework.CameraAdded += HandleCameraAdded;
            }
        }

        public void ReleaseFramework()
        {
            if (_framework != null)
            {
                _framework.CameraAdded -= HandleCameraAdded;
                _framework.Dispose();
            }
        }

        private void HandleCameraAdded(object sender, EventArgs eventArgs)
        {
            if (CameraAdded != null)
                CameraAdded(this, eventArgs);
        }
    }
}
