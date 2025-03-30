using ASP.NetCoreWebApi.common.IService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreWebApi.src.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICrudService<UsersEntity, string, UsersDto, CreateUserDto, UpdateUserDto> _service;
        private readonly IMapper _mapper;

        public UsersController(
            ICrudService<UsersEntity, string, UsersDto, CreateUserDto, UpdateUserDto> service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{userNumber}")]
        public async Task<ActionResult<UsersDto>> GetByUserNumber(string userNumber)
        {
            var user = await _service.GetByIdAsync(userNumber);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UsersDto>> Create(CreateUserDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByUserNumber), new { userNumber = created.UserNumber }, created);
        }

        [HttpPut("{userNumber}")]
        public async Task<IActionResult> Update(string userNumber, UpdateUserDto dto)
        {
            return await _service.UpdateAsync(userNumber, dto)
                ? NoContent()
                : NotFound();
        }

        [HttpDelete("{userNumber}")]
        public async Task<IActionResult> Delete(string userNumber)
        {
            return await _service.DeleteAsync(userNumber)
                ? NoContent()
                : NotFound();
        }

    }
}
