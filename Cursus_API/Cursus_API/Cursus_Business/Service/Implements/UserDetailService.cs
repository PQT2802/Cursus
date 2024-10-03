using Cursus_Business.Common;
using Cursus_Business.Exceptions.ErrorHandler;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserDetailRepository _userDetailRepository;

        public UserDetailService(IUserDetailRepository userDetailRepository) 
        {
            _userDetailRepository = userDetailRepository;
        }

        public Task<bool> RegisterInsDetailAsync(RegisterUserDTO registerUserDTO, string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUserDetailAsync(RegisterUserDTO registerUserDTO, string UserId)
        {      
            var userDetailId = await _userDetailRepository.AutoGenerateUserDetailID();
            var userDetail = new UserDetail
            {
                UserDetailID = userDetailId,
                UserID = UserId,
             
                DateOfBirth = registerUserDTO.DateOfBirth,
                //Avatar = registerUserDTO.Avatar,
                Address = registerUserDTO.Address,
                CreatedDate = DateTime.Now,
                IsActive = false
            };
            if(userDetail != null)
            {
                await _userDetailRepository.AddUserDetailAsync(userDetail);
                return true;
            }
           
            return false;
        }
        public async Task<Result> UpdateUserDetailAsync(UpdateUserDetailDTOcs UserId)
        {
            var userdetail = await _userDetailRepository.GetUserDetailByIdAsync(UserId.UserID);

            if (userdetail == null)
            {

                return Result.Failure(UserErrors.UserIsNotExist);

            }
            userdetail.Address = UserId.Address;
            userdetail.Avatar = UserId.Avatar;
            userdetail.DateOfBirth = UserId.DateOfBirth;


            await _userDetailRepository.UpdateUserDetailAsync(userdetail);
            return Result.Success();

        }
    }
}
