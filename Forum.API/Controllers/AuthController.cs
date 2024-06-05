﻿using Forum.Contracts;
using Forum.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private ApiResponse _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new ApiResponse();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegistrationRequestDto model)
        {
            try
            {
                await _authService.Register(model);

                _response.Result = model;
                _response.IsSuccess = true;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                _response.Message = "User registered successfully";
            }
            catch (Exception ex)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                _response.Message = ex.Message;
            }
            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromForm] LoginRequestDto model)
        {
            try
            {
                var loginResponse = await _authService.Login(model);

                if (loginResponse == null)
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                    _response.Message = "UserName or Password is incorrect";

                    return StatusCode(_response.StatusCode, _response);
                }

                _response.Result = loginResponse;
                _response.IsSuccess = true;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                _response.Message = "User logged in successfully";
            }
            catch (Exception ex)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                _response.Message = ex.Message;
            }
            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost("registeradmin")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RegisterAdmin([FromForm] RegistrationRequestDto model)
        {
            try
            {
                await _authService.RegisterAdmin(model);

                _response.Result = model;
                _response.IsSuccess = true;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                _response.Message = "Admin registered successfully";
            }
            catch (Exception ex)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                _response.Message = ex.Message;
            }
            return StatusCode(_response.StatusCode, _response);
        }
       
    }
}
