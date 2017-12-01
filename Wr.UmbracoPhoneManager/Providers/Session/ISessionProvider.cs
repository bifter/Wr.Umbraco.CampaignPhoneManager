using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers.Session
{
    public interface ISessionProvider
    {
        T GetSession<T>(string key = "") where T : class, new();
        bool SetSession<T>(T model, string key = "");
    }
}