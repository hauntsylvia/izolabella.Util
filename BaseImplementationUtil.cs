using System.Reflection;

namespace izolabella.Util
{
    public class BaseImplementationUtil
    {
        public static List<T> GetItems<T>(Assembly? From = null)
        {
            List<T> R = new();
            foreach (Type Ty in (From ?? Assembly.GetCallingAssembly()).GetTypes().Where(Ty => typeof(T).IsAssignableFrom(Ty) && !Ty.IsInterface && !Ty.IsAbstract && Ty.GetConstructor(Type.EmptyTypes) != null))
            {
                object? O = Activator.CreateInstance(Ty);
                if (O is not null and T M)
                {
                    R.Add(M);
                }
            }
            return R;
        }
    }
}
