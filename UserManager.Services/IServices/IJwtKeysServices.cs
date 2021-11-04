using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
public    interface IJwtKeysServices
    {
        List<JwtKeys> GetJwtKeysByKey(string jwtKey);
        JwtKeys GetByUserId(string userId);
        JwtKeys Create(JwtKeys jwtKeys);
        JwtKeys Edit(JwtKeys JwtKey);
        JwtKeys Delete(JwtKeys jwtKeys);
    }
}
