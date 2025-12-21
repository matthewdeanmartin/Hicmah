using System;
using System.Collections.Generic;
using System.Text;

namespace Ukadc.Diagnostics.Utils
{
    public class Disposer : IDisposable
    {
        private bool _disposed = false;
        private DisposeAction _onDisposal;

        public Disposer(DisposeAction onDisposal)
        {
            _onDisposal = onDisposal;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _onDisposal();
            }
        }
    }

    public delegate void DisposeAction();

}
