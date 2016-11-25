using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace PSCPortal.DB.Helper
{
    public static class SessionManager
    {
        private static readonly ISessionFactory SessionFactory;

        static SessionManager()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(SessionManager).Assembly);
            SessionFactory = configuration.BuildSessionFactory();
        }
        public static ISession Session
        {
            get
            {
                return SessionFactory.OpenSession();
            }
        }

        public static T Get<T>(Guid id, Action<T> cFunc = null)
        {
            T result;
            using (ISession session = Session)
            {
                result = session.Get<T>(id);
                if (cFunc != null)
                    cFunc.Invoke(result);
            }
            return result;
        }

        public static List<T> GetAll<T>(Func<ICriteria, ICriteria> cFunc = null)
        {
            List<T> list;
            using (ISession session = Session)
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                if (cFunc != null)
                    criteria = cFunc.Invoke(criteria);
                list = (List<T>)criteria.List<T>();
            }
            return list;
        }

        public static void Save(object obj)
        {
            using (ISession session = Session)
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    session.Save(obj);
                    trans.Commit();
                }
            }
        }

        public static void Update(object obj)
        {
            using (ISession session = Session)
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    session.Update(obj);
                    trans.Commit();
                }
            }
        }

        public static void SaveOrUpdate(object obj)
        {
            using (ISession session = Session)
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    session.SaveOrUpdate(obj);
                    trans.Commit();
                }
            }
        }

        public static void Delete(object obj)
        {
            using (ISession session = Session)
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    session.Delete(obj);
                    trans.Commit();
                }
            }
        }

        public static void DoWork(Action<ISession> work)
        {
            using (ISession session = Session)
            {
                using (ITransaction trans = session.BeginTransaction())
                {
                    work.Invoke(session);
                    trans.Commit();
                }
            }
        }

        public static void Query(Action<ISession> work)
        {
            using (ISession session = Session)
            {
                work.Invoke(session);
            }
        }
    }
}
