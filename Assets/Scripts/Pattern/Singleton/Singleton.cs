using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Pattern.Singleton
{
    public abstract class Singleton<T> where T : class
    {
        private static T instance;
        private static object initLock = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = CreateInstance();
                return instance;
            }
        }

        protected static T CreateInstance()
        {
            lock (initLock)
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    ConstructorInfo[] ctors = t.GetConstructors();

                    if (ctors.Length > 0)
                    {
                        throw new InvalidOperationException(string.Format("{0} has at least one accessible ctor making it " +
                            "impossible to enforce singleton behaviour", t.Name));
                    }
                    return (T)Activator.CreateInstance(t, true);
                }
            }
            throw new InvalidOperationException(string.Format("Something must be wrong when creating instance of type: {0}", typeof(T).Name));
        }
    }
}