using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.Services.Services
{
    public class JwtKeysServices : IJwtKeysServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public JwtKeysServices(ApplicationDbContext context)
        {
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public JwtKeys Delete(JwtKeys jwtKeys)
        {
            try
            {
                if (jwtKeys != null)
                {
                    utOfWork.JwtKeysRepository.Delete(jwtKeys);
                    utOfWork.Commit();
                    return jwtKeys;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public JwtKeys Create(JwtKeys jwtKeys)
        {
            try
            {
                utOfWork.JwtKeysRepository.Add(jwtKeys);
                utOfWork.Commit();
                return jwtKeys;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<JwtKeys> GetJwtKeysByKey(string jwtKey)
        {
            return utOfWork.JwtKeysRepository.GetMany(usr => usr.jwtKey.Equals(jwtKey)).ToList();
        }

        public JwtKeys GetByUserId(string userId)
        {
            return utOfWork.JwtKeysRepository.GetMany(d => d.userId == userId).LastOrDefault();
        }
        public JwtKeys Edit(JwtKeys JwtKey)
        {
            try
            {
                utOfWork.JwtKeysRepository.Update(JwtKey);
                utOfWork.Commit();
                return JwtKey;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
