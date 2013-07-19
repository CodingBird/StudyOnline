using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogical.Concrete
{
    public class ServiceBase:IDisposable
    {
        public IList<IDisposable> DisposableObjects { get; private set; }

        public ServiceBase()
        {
            this.DisposableObjects = new List<IDisposable>();
        }
        protected void AddDisposableObjects(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        public void Dispose()
        {
            foreach (IDisposable obj in this.DisposableObjects)
            {
                if (obj != null)
                {
                    obj.Dispose();
                }
            }
        }

    }
}
