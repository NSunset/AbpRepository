using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Frame.Common;
using System.Security.Claims;
using System.Linq;
using Abp.Runtime.Security;

namespace Frame.Domain
{
    public class UserManage
    {
        public static async Task<string> GetToken(ManageUserInfo user)
        {
            var claimsIdentity = new List<System.Security.Claims.ClaimsIdentity>() {
                new ClaimsIdentity(new List<Claim> {new Claim (AbpClaimTypes.UserId,user.Id.ToString())})
            };
            var principal = new System.Security.Claims.ClaimsPrincipal(claimsIdentity);
            return await GetToken(principal.Identity as ClaimsIdentity);
        }



        private static async Task<string> GetToken(System.Security.Claims.ClaimsIdentity claimsIdentity)
        {
            var claims = claimsIdentity.Claims.ToList();
            var claim = claims.First(a => a.Type == AbpClaimTypes.UserId);
            claims.AddRange(new System.Security.Claims.Claim[] {
                new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,claim.Value),
                new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat,DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                new System.Security.Claims.Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            });
            return await GenerateAccessToken(claims.ToArray());
        }


        private static async Task<string> GenerateAccessToken(System.Security.Claims.Claim[] claims)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(FrameCoreConsts.JwtSignKey));
            var now = DateTime.Now;
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: FrameCoreConsts.JwtIssUer,
                audience: FrameCoreConsts.JwtAudience,
                claims: claims,
                notBefore: now,
                expires: now.AddHours(int.Parse(FrameCoreConsts.JwtTokenTimeOutHours)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
