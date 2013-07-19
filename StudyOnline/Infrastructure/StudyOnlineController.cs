using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyOnline.Infrastructure
{
    public class StudyOnlineController : Controller
    {
        public IList<IDisposable> DisposableObjects { get; private set; }

        public StudyOnlineController()
        {
            this.DisposableObjects = new List<IDisposable>();
        }
        public void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (null != disposable)
            {
                this.DisposableObjects.Add(disposable);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var obj in this.DisposableObjects)
                {
                    if (null != obj)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }

    }
}