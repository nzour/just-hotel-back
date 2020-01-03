using System;
using System.Data.Common;
using Newtonsoft.Json;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Infrastructure.NHibernate
{
    public class JsonArrayType<T> : IUserType where T : class
    {
        public bool IsMutable => false;
        public Type ReturnedType => typeof(T);
        public SqlType[] SqlTypes => new SqlType[] { new StringSqlType() };

        public new bool Equals(object x, object y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            var xDocX = JsonConvert.SerializeObject(x);
            var xDocY = JsonConvert.SerializeObject(y);

            return xDocY == xDocX;
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            if (names.Length != 1)
            {
                throw new InvalidOperationException("Only expecting one column...");
            }

            if (rs[names[0]] is string val && !string.IsNullOrWhiteSpace(val))
            {
                return JsonConvert.DeserializeObject<T>(val);
            }

            return null;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = cmd.Parameters[index];

            parameter.Value = null != value ? (object) JsonConvert.SerializeObject(value) : "[]";
        }

        public int GetHashCode(object x)
        {
            return null != x ? x.GetHashCode() : 0;
        }

        public object DeepCopy(object value)
        {
            if (value == null)
            {
                return null;
            }

            var serialized = JsonConvert.SerializeObject(value);

            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            var str = (string) cached;

            return string.IsNullOrWhiteSpace(str) ? null : JsonConvert.DeserializeObject<T>(str);
        }

        public object Disassemble(object value)
        {
            return value != null ? JsonConvert.SerializeObject(value) : null;
        }
    }
}
