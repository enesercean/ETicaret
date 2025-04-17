using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exeptions
{
    internal class UserCreateFailedExeption : Exception
    {
        public UserCreateFailedExeption()
        {
        }

        public UserCreateFailedExeption(string? message) : base("Kullanıcı oluşturulurken beklenmeyen bir hata oluştu.")
        {
        }

        public UserCreateFailedExeption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserCreateFailedExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
