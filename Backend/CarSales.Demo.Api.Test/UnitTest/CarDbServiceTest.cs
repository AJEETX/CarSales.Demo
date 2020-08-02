using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace CarSales.Demo.Api.Test.UnitTest
{
    public class CarDbServiceTest:IDisposable
    {


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
