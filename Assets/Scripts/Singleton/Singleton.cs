using System;
using UnityEngine;

public abstract class Singleton<T> where T : class, new()
{
	private static T minstance;
	public static T Instance
	{
		get
        {
            if (minstance == null)
		    {
			    minstance = Activator.CreateInstance<T>();
			    if (minstance != null)
			    {
                    (minstance as Singleton<T>).Init();
			    }
		    }

            return minstance;
        }
	}

	public static void Release()
	{
		if (minstance != null)
		{
			minstance = null;
		}
	}

	protected virtual void Init()
    {

    }

    public virtual void Startup()
    {

    }
    
    public virtual void Dispose()
    {
	    
    }

}
